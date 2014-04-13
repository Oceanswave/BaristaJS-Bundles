namespace BaristaJS.AppEngine.Bundles
{
    using System.Collections.Generic;

    public class BaristaBundle : IBootstrapBundle
    {
        private readonly BaristaInstance m_instance;

        public BaristaBundle(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env)
        {
            m_instance = new BaristaInstance(bundleManager, packageSource, env, this.Metadata);
        }

        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public object InstallBundle(IScriptEngine engine)
        {
            engine.AddRestrictedHostObject("barista", m_instance);
            engine.Evaluate("var require = barista.require");
            return Undefined.Value;
        }
    }
}
