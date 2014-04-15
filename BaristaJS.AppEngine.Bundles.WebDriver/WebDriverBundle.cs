namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using BaristaJS.AppEngine.Bundles.WebDriver.Library;
  using OpenQA.Selenium.PhantomJS;

  public class WebDriverBundle : IBundle
  {
    private const string PhantomJSExecutableFileName = "phantomjs.exe";
    private readonly string m_bundleInstallPath;

    public IBundleMetadata Metadata
    {
      get;
      set;
    }

    public WebDriverBundle(string bundleInstallPath)
    {
      m_bundleInstallPath = bundleInstallPath;
    }

    public object InstallBundle(IScriptEngine engine)
    {
      //TODO: Add all the necessary types.
      //engine.AddHostObject("CookieJar", new CookieJarWrapper());
      engine.AddHostType("By", typeof(ByInstance));
      engine.AddHostType("Cookie", typeof (CookieInstance));
      engine.AddHostType("Screenshot", typeof(ScreenshotInstance));

      var service = PhantomJSDriverService.CreateDefaultService(m_bundleInstallPath, PhantomJSExecutableFileName);
      var phantomJS = new PhantomJSDriver(service);

      engine.AddHostObject("phantomJSDriver", new WebDriverInstance<PhantomJSDriver>(phantomJS));
      return Undefined.Value;
    }
  }
}
