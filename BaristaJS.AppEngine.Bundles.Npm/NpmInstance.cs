namespace BaristaJS.AppEngine.Bundles.Npm
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Octokit;

    public class NpmInstance : IScriptableObject
    {
        private IScriptEngine m_engine;

        public NpmInstance()
        {
            NpmRegistryBaseUrl = "http://registry.npmjs.org/";
        }

        [ScriptMember(Name = "npmRegistryBaseUrl")]
        public object NpmRegistryBaseUrl
        {
            get;
            set;
        }

        [ScriptMember(Name = "search")]
        public object Search(string name)
        {
            //TODO: Scrape the npmjs.org website and get results.
            throw new NotImplementedException();
        }

        [ScriptMember(Name = "get")]
        public NpmPackage Get(string name)
        {
            var task = GetNpmPackage(name);
            task.Wait();
            return task.Result;
        }

        [ScriptMember(Name = "require")]
        public object Require(string name)
        {
            var task = GetNpmPackage(name);
            task.Wait();
            return task.Result;
        }

        private async Task<NpmPackage> GetNpmPackage(string name)
        {
            var client = new HttpClient();
            var result = await client.GetAsync(NpmRegistryBaseUrl + name);
            if (result.StatusCode == HttpStatusCode.NotFound)
                throw new ScriptEngineException("A npm package with the specified name was not found: " + name);

            //Convert the json result to a pretty object.
            var packageJson = await result.Content.ReadAsStringAsync();
            var package = JsonConvert.DeserializeObject<NpmPackage>(packageJson);

            if (package.Main == null)
            {
                if (package.Repository != null && package.Repository.Type == "git" && package.Repository.Url.ToLowerInvariant().StartsWith("http://github.com"))
                {
                    var repoUri = new Uri(package.Repository.Url);
                    var user = repoUri.Segments[0];
                    var repoName = repoUri.Segments[1].Replace(".git", "");

                    var ghClient = new GitHubClient(new ProductHeaderValue("BaristaJS"));
                    var repo = await ghClient.Repository.Get(user, repoName);

                }
            }

            return package;
        }

        [ScriptMember(Name = "hasAnEngine")]
        public bool HasAnEngine
        {
            get { return m_engine == null; }
        }

        public void OnExposedToScriptCode(IScriptEngine engine)
        {
            m_engine = engine;
        }
    }
}
