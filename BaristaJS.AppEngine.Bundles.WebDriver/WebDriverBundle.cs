namespace BaristaJS.AppEngine.Bundles.WebDriver
{
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
      engine.AddHostType("Cookie", typeof (CookieInstance));
      return new WebDriverWrapper<PhantomJSDriver>(new PhantomJSDriver());
    }
  }
}
