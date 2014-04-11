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

        //TODO: Um, get rid of these properties, somehow!!! (Injection, something? I'm too tired to think.)
        public string Description
        {
            get { return "It's barista!!"; }
        }

        public string Id
        {
            get { return "Barista"; }
        }

        public string Title
        {
            get { return "Barista Core"; }
        }

        public SemanticVersion Version
        {
            get { return SemanticVersion.Parse("1.0.0.0"); }
        }


        public object InstallBundle(IScriptEngine engine)
        {
            engine.AddHostObject("barista", m_instance);
            return Undefined.Value;
        }
    }
}
