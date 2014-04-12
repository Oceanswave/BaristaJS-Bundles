namespace BaristaJS.AppEngine.Bundles.Npm
{
    using System;

    public class NpmInstance : IScriptableObject
    {
        private IScriptEngine m_engine;

        [ScriptMember(Name = "search")]
        public object Search(string name)
        {
            //TODO: Scrape the npmjs.org website and get results.
            throw new NotImplementedException();
        }

        public void OnExposedToScriptCode(IScriptEngine engine)
        {
            m_engine = engine;
        }
    }
}
