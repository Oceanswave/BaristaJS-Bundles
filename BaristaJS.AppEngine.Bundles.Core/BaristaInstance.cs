namespace BaristaJS.AppEngine.Bundles
{
    using BaristaJS.AppEngine;
    using BaristaJS.AppEngine.FileSystems;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BaristaInstance : IScriptableObject
  {
    private IScriptEngine m_engine;

    public BaristaInstance(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env, IFileSystem fileSystem)
    {
      if (bundleManager == null)
        throw new ArgumentNullException("bundleManager");

      if (String.IsNullOrWhiteSpace(packageSource))
        throw new ArgumentNullException("packageSource");

      if (env == null)
        throw new ArgumentNullException("env");

      if (fileSystem == null)
          throw new ArgumentNullException("fileSystem");

        BundleManager = bundleManager;
        PackageSource = packageSource;
        Environment = env;
        FileSystem = fileSystem;
    }

    #region Properties

    [NoScriptAccess]
    public IScriptEngine ScriptEngine
    {
        get { return m_engine; }
        internal set { m_engine = value; }
    }

    [NoScriptAccess]
    public IBundleMetadata Metadata
    {
      get;
      set;
    }

    [NoScriptAccess]
    public IBundleManager BundleManager
    {
      get;
      private set;
    }

    [NoScriptAccess]
    public IDictionary<string, object> Environment
    {
      get;
      private set;
    }

    [NoScriptAccess]
    public IFileSystem FileSystem
    {
        get;
        private set;
    }

    [NoScriptAccess]
    public string PackageSource
    {
      get;
      set;
    }

    [ScriptMember(Name = "version")]
    public string Version
    {
      get { return Metadata.Version.ToString(); }
    }
    #endregion

    [ScriptMember(Name = "isArray")]
    public bool IsArray(object value)
    {
      return TypeUtilities.IsArray(value);
    }

    [ScriptMember(Name = "isDate")]
    public bool IsDate(object value)
    {
      return TypeUtilities.IsDate(value);
    }

    [ScriptMember(Name = "isDefined")]
    public bool IsDefined(object value)
    {
      return !TypeUtilities.IsUndefined(value);
    }

    [ScriptMember(Name = "isFunction")]
    public bool IsFunction(object value)
    {
      return TypeUtilities.IsFunction(value);
    }

    [ScriptMember(Name = "isNumber")]
    public bool IsNumber(object value)
    {
      return TypeUtilities.IsNumeric(value);
    }

    [ScriptMember(Name = "isObject")]
    public bool IsObject(object value)
    {
      return TypeUtilities.IsObject(value);
    }

    [ScriptMember(Name = "isString")]
    public bool IsString(object value)
    {
      return TypeUtilities.IsString(value);
    }

    [ScriptMember(Name = "isUndefined")]
    public bool IsUndefined(object value)
    {
      return TypeUtilities.IsUndefined(value);
    }

    [ScriptMember(Name = "lowercase")]
    public string Lowercase(object value)
    {
      return Convert.ToString(value).ToLowerInvariant();
    }

    [ScriptMember(Name = "uppercase")]
    public string Uppercase(object value)
    {
      return Convert.ToString(value).ToUpperInvariant();
    }

    [ScriptMember("installedBundles")]
    public IPropertyBag InstalledBundles
    {
      get
      {
        var result = new PropertyBag();
        foreach (var bundle in BundleManager.InstalledBundles)
        {
          result.Add(bundle.Metadata.Id, bundle.Metadata);
        }

        return result;
      }
    }

        //From http://nodejs.org/api/modules.html
        //require(X) from module at path Y
        //1. If X is a core module,
        //   a. return the core module
        //   b. STOP
        //2. If X begins with './' or '/' or '../'
        //   a. LOAD_AS_FILE(Y + X)
        //   b. LOAD_AS_DIRECTORY(Y + X)
        //3. LOAD_NODE_MODULES(X, dirname(Y))
        //4. THROW "not found"

        //LOAD_AS_FILE(X)
        //1. If X is a file, load X as JavaScript text.  STOP
        //2. If X.js is a file, load X.js as JavaScript text.  STOP
        //3. If X.node is a file, load X.node as binary addon.  STOP

        //LOAD_AS_DIRECTORY(X)
        //1. If X/package.json is a file,
        //   a. Parse X/package.json, and look for "main" field.
        //   b. let M = X + (json main field)
        //   c. LOAD_AS_FILE(M)
        //2. If X/index.js is a file, load X/index.js as JavaScript text.  STOP
        //3. If X/index.node is a file, load X/index.node as binary addon.  STOP

        //LOAD_NODE_MODULES(X, START)
        //1. let DIRS=NODE_MODULES_PATHS(START)
        //2. for each DIR in DIRS:
        //   a. LOAD_AS_FILE(DIR/X)
        //   b. LOAD_AS_DIRECTORY(DIR/X)

        //NODE_MODULES_PATHS(START)
        //1. let PARTS = path split(START)
        //2. let ROOT = index of first instance of "node_modules" in PARTS, or 0
        //3. let I = count of PARTS - 1
        //4. let DIRS = []
        //5. while I > ROOT,
        //   a. if PARTS[I] = "node_modules" CONTINUE
        //   c. DIR = path join(PARTS[0 .. I] + "node_modules")
        //   b. DIRS = DIRS + DIR
        //   c. let I = I - 1
        //6. return DIRS

        [ScriptMember("require")]
        public object Require(string path, string version = null)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ScriptEngineException("path must be a string.");

            if (path.StartsWith("./", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("../", StringComparison.OrdinalIgnoreCase))
            {
                //No "Core" bundles... except for this one, currently.

                object result;
                if (TryLoadAsFile(path, out result))
                    return result;

                if (TryLoadAsDirectory(path, out result))
                    return result;

                throw new ScriptEngineException("Cannot find module: '" + path + "'");
            }
            else
            {
                object result;
                if (TryLoadAsBaristaBundle(path, version, out result))
                    return result;

                throw new ScriptEngineException("Cannot find module: '" + path + "'");
            }
        }

        private IFileInfo GetFileInfo(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            var getFileInfoTask = FileSystem.GetFileInfoAsync(fileName);
            getFileInfoTask.Wait();
            return getFileInfoTask.Result;
        }

        private object EvaluateScriptFromFile(IFileInfo fileInfo)
        {
            string script;
            using (var fileStream = fileInfo.Get())
            using (var sr = new StreamReader(fileStream))
            {
                script = sr.ReadToEnd();
            }

            return m_engine.Evaluate(script);
        }

        private bool TryLoadAsFile(string fileName, out object result)
        {
            var fileInfo = GetFileInfo(fileName);
            if (fileInfo != null && fileInfo.Exists)
            {
                result = EvaluateScriptFromFile(fileInfo);
                return true;
            }

            fileInfo = GetFileInfo(fileName + "." + ScriptEngine.FileNameExtension);
            if (fileInfo != null && fileInfo.Exists)
            {
                result = EvaluateScriptFromFile(fileInfo);
                return true;
            }

            fileInfo = GetFileInfo(fileName + ".dll");
            if (fileInfo != null && fileInfo.Exists)
            {
                try
                {
                    var bundle = BundleManager.GetBundle<IBundle>(Environment, fileName, fileInfo.Get(), null);
                    if (bundle == null)
                    {
                        result = null;
                        return false;
                    }

                    result = bundle.InstallBundle(m_engine);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new ScriptEngineException("An error occurred while attempting to require a bundle: " + ex.Message, ex);
                }
            }

            result = null;
            return false;
        }

        private bool TryLoadAsDirectory(string subPath, out object result)
        {
            var fileInfo = GetFileInfo(subPath + "\\package.json");
            if (fileInfo != null && fileInfo.Exists)
            {
                string package;
                using (var fileStream = fileInfo.Get())
                using (var sr = new StreamReader(fileStream))
                {
                    package = sr.ReadToEnd();
                }

                //TODO: implement this.
                //var x = JsonConvert.Deserialize<Package>(package);
                //if (String.IsNullOrWhitespace(x.Main) == false)
                //return TryLoadAsFile(packageId + "\\" + main, out result);
                throw new NotImplementedException();
            }

            fileInfo = GetFileInfo(subPath + "\\index.js");
            if (fileInfo != null && fileInfo.Exists)
            {
                result = EvaluateScriptFromFile(fileInfo);
                return true;
            }

            fileInfo = GetFileInfo(subPath + "\\index.dll");
            if (fileInfo != null && fileInfo.Exists)
            {
                try
                {
                    var bundle = BundleManager.GetBundle<IBundle>(Environment, subPath, fileInfo.Get(), null);
                    if (bundle == null)
                    {
                        result = null;
                        return false;
                    }

                    result = bundle.InstallBundle(m_engine);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new ScriptEngineException("An error occurred while attempting to require a bundle: " + ex.Message, ex);
                }
            }

            result = null;
            return false;
        }

        private bool TryLoadAsBaristaBundle(string packageId, string version, out object result)
        {
            try
            {
                var bundle = BundleManager.GetBundle<IBundle>(Environment, PackageSource, packageId, version);
                if (bundle == null)
                {
                    result = null;
                    return false;
                }
                
                result = bundle.InstallBundle(m_engine);
                return true;
            }
            catch (Exception ex)
            {
                throw new ScriptEngineException("An error occurred while attempting to require a bundle: " + ex.Message, ex);
            }
        }

        [NoScriptAccess]
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
  }
}
