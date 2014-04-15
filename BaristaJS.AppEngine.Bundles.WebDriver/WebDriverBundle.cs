namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using BaristaJS.AppEngine.Bundles.WebDriver.Library;
  using OpenQA.Selenium.PhantomJS;

  public class WebDriverBundle : IBundle
  {
    public IBundleMetadata Metadata
    {
      get;
      set;
    }

    public object InstallBundle(IScriptEngine engine)
    {
      //TODO: Add all the necessary types.
      //engine.AddHostObject("CookieJar", new CookieJarWrapper());
      engine.AddHostType("By", typeof(ByInstance));
      engine.AddHostType("Cookie", typeof (CookieInstance));
      engine.AddHostType("PhantomJSDriver", typeof (WebDriverInstance<PhantomJSDriver>));
      return Undefined.Value;
    }
  }
}
