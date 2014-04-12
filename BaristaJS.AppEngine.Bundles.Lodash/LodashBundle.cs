namespace BaristaJS.AppEngine.Bundles.Lodash
{
  public class LodashBundle : IBundle
  {
    public IBundleMetadata Metadata
    {
      get;
      set;
    }

    public object InstallBundle(IScriptEngine engine)
    {
        var lodash = Properties.Resources.lodash;

        return engine.Evaluate(lodash);
    }
  }
}
