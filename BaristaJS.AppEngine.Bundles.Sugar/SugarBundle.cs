namespace BaristaJS.AppEngine.Bundles.Sugar
{
    public class SugarBundle
    {
        public IBundleMetadata Metadata
        {
            get;
            set;
        }

        public object InstallBundle(IScriptEngine engine)
        {
            var lodash = Properties.Resources.sugar_full_development;

            return engine.Evaluate(lodash);
        }
    }
}
