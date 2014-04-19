namespace BaristaJS.AppEngine.Bundles.Npm
{
    using Newtonsoft.Json;

    public class PackageRepository
    {
        [JsonProperty("type")]
        [ScriptMember("type")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty("url")]
        [ScriptMember("url")]
        public string Url
        {
            get;
            set;
        }
    }
}
