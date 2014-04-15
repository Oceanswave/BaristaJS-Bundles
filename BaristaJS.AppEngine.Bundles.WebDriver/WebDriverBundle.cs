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
      engine.Execute("var WebDriver = {};");
      engine.AddHostType("WebDriver.By", typeof(ByInstance));
      engine.AddHostType("WebDriver.Cookie", typeof (CookieInstance));
      engine.AddHostType("WebDriver.CookieJar", typeof(CookieJarInstance));
      engine.AddHostType("WebDriver.Navigation", typeof(NavigationInstance));
      engine.AddHostType("WebDriver.Options", typeof(OptionsInstance));
      engine.AddHostType("WebDriver.Screenshot", typeof(ScreenshotInstance));
      engine.AddHostType("WebDriver.SearchContext", typeof(SearchContextInstance));
      engine.AddHostType("WebDriver.TakesScreenshot", typeof(TakesScreenshotInstance));
      engine.AddHostType("WebDriver.TargetLocator", typeof(TargetLocatorInstance));
      engine.AddHostType("WebDriver.Timeouts", typeof(TimeoutsInstance));
      engine.AddHostType("WebDriver.WebElement", typeof(WebElementInstance));
      engine.AddHostType("WebDriver.Window", typeof(WindowInstance));
      engine.AddHostType("WebDriver.PhantomJSDriver", typeof(WebDriverInstance<PhantomJSDriver>));

      WebDriverBundle.PhantomJSDriverService = PhantomJSDriverService.CreateDefaultService(m_bundleInstallPath, PhantomJSExecutableFileName);

      engine.AddHostObject("phantomJSDriver", new WebDriverInstance<PhantomJSDriver>(new PhantomJSDriver(WebDriverBundle.PhantomJSDriverService)));
      return Undefined.Value;
    }

    public static PhantomJSDriverService PhantomJSDriverService
    {
      get;
      set;
    }
  }
}
