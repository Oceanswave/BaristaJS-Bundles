namespace BaristaJS.AppEngine.Bundles
{
    using System.Collections.Generic;
    using BaristaJS.AppEngine.Library;

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
            m_instance.Metadata = this.Metadata;
            engine.AddHostType("Size", typeof(SizeInstance));
            engine.AddHostType("Point", typeof(PointInstance));
            engine.AddHostType("Base64ByteArray", typeof(Base64EncodedByteArrayInstance));

            engine.AddRestrictedHostObject("barista", m_instance);
            
            engine.Evaluate("var require = barista.require");

            return Undefined.Value;
        }
    }
}
