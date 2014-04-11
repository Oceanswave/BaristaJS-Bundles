namespace BaristaJS.AppEngine.Bundles
{
    using BaristaJS.AppEngine;
    using System;
    using System.Collections.Generic;

    public class BaristaInstance : IScriptableObject
    {
        private IScriptEngine m_engine;

        public BaristaInstance(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env)
        {
            if (bundleManager == null)
                throw new ArgumentNullException("bundleManager");

            if (String.IsNullOrWhiteSpace(packageSource))
                throw new ArgumentNullException("packageSource");

            if (env == null)
                throw new ArgumentNullException("env");

            BundleManager = bundleManager;
            PackageSource = packageSource;
            Environment = env;
        }

        #region Properties
        public IBundleManager BundleManager
        {
            get;
            private set;
        }

        public IDictionary<string, object> Environment
        {
            get;
            private set;
        }

        public string PackageSource
        {
            get;
            set;
        }
        #endregion

        [ScriptMember("installedBundles")]
        public IList<IBundle> InstalledBundles
        {
            get { return BundleManager.InstalledBundles; }
        }
        
        [ScriptMember("require")]
        public object Require(string packageId, string version = null)
        {
            var bundle = BundleManager.GetBundle<IBundle>(Environment, PackageSource, packageId, version);

            return bundle;
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
    }
}
