namespace BaristaJS.AppEngine.Bundles.Npm
{
    public class NpmBundle : IBundle
    {
        private readonly NpmInstance m_instance;

        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public NpmBundle()
        {
            m_instance = new NpmInstance();
        }

        public object InstallBundle(IScriptEngine engine)
        {
            return m_instance;
        }
    }
}
