namespace BaristaJS.AppEngine.Bundles
{
    using System.Collections.Generic;
    using BaristaJS.AppEngine.Bundles.Library;
    using BaristaJS.AppEngine.FileSystems;
    using PointInstance = BaristaJS.AppEngine.Bundles.Library.PointInstance;
    using SizeInstance = BaristaJS.AppEngine.Bundles.Library.SizeInstance;

  public class BaristaBundle : IBootstrapBundle
    {
        private readonly BaristaInstance m_instance;

        public BaristaBundle(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env, IFileSystem fileSystem)
        {
            m_instance = new BaristaInstance(bundleManager, packageSource, env, fileSystem);
        }

        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public object InstallBundle(IScriptEngine engine)
        {
            m_instance.Metadata = Metadata;
            m_instance.ScriptEngine = engine;

            engine.AddHostType("Size", typeof(SizeInstance));
            engine.AddHostType("Point", typeof(PointInstance));
            engine.AddHostType("Base64EncodedByteArray", typeof(Base64EncodedByteArrayInstance));

            engine.AddRestrictedHostObject("barista", m_instance);
            
            engine.Evaluate("var require = barista.require;");

            return Undefined.Value;
        }
    }
}
