namespace BaristaJS.AppEngine.Bundles.Npm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NpmBundle : IBundle
    {
        private readonly NpmInstance m_instance;

        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public NpmBundle(string workingDirectory, IEnumerable<string> contentFiles)
        {
            m_instance = new NpmInstance(workingDirectory, contentFiles.FirstOrDefault(cf => cf.EndsWith("node.exe", StringComparison.OrdinalIgnoreCase)));
        }

        public object InstallBundle(IScriptEngine engine)
        {
            return m_instance;
        }
    }
}
