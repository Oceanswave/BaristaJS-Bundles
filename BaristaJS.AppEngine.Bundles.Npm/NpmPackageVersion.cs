namespace BaristaJS.AppEngine.Bundles.Npm
{
    using Newtonsoft.Json;

    public class NpmPackageVersion
    {
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        //tags
        //version
        //author
        //directories
        //repository
        //bugs
        //engines
        //main
        //_id
        //_nodeSupported
        //_npmVersion
        //_nodeVersion
        //dist
    }
}
