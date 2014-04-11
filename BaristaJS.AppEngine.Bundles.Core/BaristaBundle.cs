namespace BaristaJS.AppEngine.Bundles
{
    using System.Collections.Generic;

    public class BaristaBundle : IBootstrapBundle
    {
        private readonly BaristaInstance m_instance;

        public BaristaBundle(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env)
        {
            m_instance = new BaristaInstance(bundleManager, packageSource, env);
        }

        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public object InstallBundle(IScriptEngine engine)
        {
            engine.AddHostObject("barista", m_instance);
            return Undefined.Value;
        }
    }
}
