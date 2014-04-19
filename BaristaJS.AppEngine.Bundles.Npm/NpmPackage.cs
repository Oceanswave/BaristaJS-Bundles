namespace BaristaJS.AppEngine.Bundles.Npm
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class NpmPackage
    {
        [JsonProperty("_id")]
        [ScriptMember("id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("name")]
        [ScriptMember("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("description")]
        [ScriptMember("description")]
        public string Description
        {
            get;
            set;
        }

        //dist-tags (dict<string, version>
        [JsonProperty("versions")]
        [ScriptMember("versions")]
        public IDictionary<string, object> Versions
        {
            get;
            set;
        }

        //versions
        //maintainers (array)
        //time (obj)
        //author (obj)
        [JsonProperty("repository")]
        [ScriptMember("repository")]
        public PackageRepository Repository
        {
            get;
            set;
        }

        //users

        [JsonProperty("readme")]
        [ScriptMember("readme")]
        public string Readme
        {
            get;
            set;
        }
        
        [JsonProperty("homepage")]
        [ScriptMember("homepage")]
        public string Homepage
        {
            get;
            set;
        }

        [JsonProperty("main")]
        [ScriptMember("main")]
        public string Main
        {
            get;
            set;
        }

        //keywords (obj)
        //contributors
        //bugs
        //license
        //readmeFilename
        //_attachments
    }
}
