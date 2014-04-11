namespace BaristaJS.AppEngine.Bundles
{
    using BaristaJS.AppEngine;
    using Microsoft.Owin;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Planning.Bindings;
    using NuGet;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Versioning;

    public class BaristaInstance : IAppEngineBootstrap, IScriptableObject
    {
        private IScriptEngine m_engine;

        public BaristaInstance(string packageSource, IDictionary<string, object> env, IKernel kernel)
        {
            if (String.IsNullOrWhiteSpace(packageSource))
                throw new ArgumentNullException("packageSource");

            if (env == null)
                throw new ArgumentNullException("env");

            if (kernel == null)
                throw new ArgumentNullException("kernel");

            PackageSource = packageSource;
            Environment = env;
            Kernel = kernel;
        }

        public IDictionary<string, object> Environment
        {
            get;
            private set;
        }

        public IKernel Kernel
        {
            get;
            private set;
        }

        public string PackageSource
        {
            get;
            set;
        }

        [ScriptMember("listInstalledBundles")]
        public IList<IBundle> ListInstalledBundles()
        {
            var bindings = Kernel
                .GetAll<IBundle>()
                .ToList();

            return bindings;
        }
        
        [ScriptMember("require")]
        public object Require(string packageName, string version = null)
        {
            var repo = PackageRepositoryFactory.Default.CreateRepository(PackageSource);

            var packagesPath = GetPackagesPath();

            //Download and unzip the package
            var packageManager = new PackageManager(repo, packagesPath);
            if (String.IsNullOrWhiteSpace(version) == false)
            {
                var ver = new NuGet.SemanticVersion(version);
                packageManager.InstallPackage(packageName, ver, false, true);
            }
            else
                packageManager.InstallPackage(packageName);

            var currentFramework = Assembly.GetExecutingAssembly().GetCustomAttribute<TargetFrameworkAttribute>();
            var currentFrameworkName = new FrameworkName(currentFramework.FrameworkName);

            var bundlesCoreLibs = packageManager.LocalRepository
                .FindPackage(packageName)
                .GetLibFiles()
                .OfType<PhysicalPackageFile>()
                .Where(pf => pf.SupportedFrameworks.Any(sf => sf == currentFrameworkName))
                .ToList();

            if (bundlesCoreLibs.Any() == false)
                throw new ScriptEngineException(m_engine.Name, "Unable to load specifed package.", "A package with the specified name could not be obtained from the package source or is not of the current framework.", 400, true, null);

            //Scan assemblies in packages path.
            var files = bundlesCoreLibs.Select(l => l.SourcePath);

            var context = new OwinContext(Environment);

            //TODO: "Make Safe" the request and response by cloning them. (Assuming this works)

            Kernel.Bind(scanner => scanner.From(files)
                .SelectAllClasses()
                .InheritedFrom<IBundle>()
                .BindAllInterfaces()
                .ConfigureFor<IHttpBundle>(b =>
                    b.WithConstructorArgument("request", context.Request)
                     .WithConstructorArgument("response", context.Response)
                )
                );

            var result = Kernel.GetAll<IBundle>().ToList();

            if (result.Count() == 1)
                return result.First();

            return result;
        }

        public void OnExposedToScriptCode(IScriptEngine engine)
        {
            if (engine == null)
                throw new ArgumentNullException("engine");

            if (m_engine == null)
            {
                m_engine = engine;
                return;
            }

            if (engine != m_engine)
            {
                throw new ArgumentException("Invalid script engine", "engine");
            }
        }

        private static string GetPackagesPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;

            var uri = new UriBuilder(codeBase);

            var path = Uri.UnescapeDataString(uri.Path);

            if (String.IsNullOrEmpty(path))
                throw new InvalidOperationException("The current path is null.");

            // ReSharper disable once AssignNullToNotNullAttribute
            path = Path.Combine(Path.GetDirectoryName(path), "runtimePackages");

            return path;
        }
    }
}
