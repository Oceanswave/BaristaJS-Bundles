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
        public string PackageSource
        {
            get;
            set;
        }
        
        [ScriptMember(Name = "happyPuppy")]
        public string HappyPuppy
        {
            get;
            set;
        }

        [ScriptMember(Name = "version")]
        public string Version
        {
            get { return m_baristaInstanceMetadata.Version.ToString(); }
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
            return bundle.InstallBundle(m_engine);
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
