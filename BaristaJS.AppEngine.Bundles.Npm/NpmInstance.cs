namespace BaristaJS.AppEngine.Bundles.Npm
{
    using BaristaJS.Appengine.Bundles.Npm.NodeNpm;

    public class NpmInstance : IScriptableObject
    {
        private readonly NpmApi m_npmApi;
        private IScriptEngine m_engine;

        public NpmInstance(string workingDirectory, string nodeInstallPath)
        {
            m_npmApi = new NpmApi(workingDirectory);
            if (m_npmApi == null)
            {
                throw new ScriptEngineException("Failed to create NpmApi");
            }

            m_npmApi.NpmClient.InstallPath = nodeInstallPath;
        }

        [ScriptMember(Name = "version")]
        public string Version
        {
            get { return m_npmApi.GetInstalledVersion(); }
        }

        public void OnExposedToScriptCode(IScriptEngine engine)
        {
            m_engine = engine;
        }
    }
}
