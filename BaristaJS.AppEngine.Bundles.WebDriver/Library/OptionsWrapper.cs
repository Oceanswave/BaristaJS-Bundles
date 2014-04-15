namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using OpenQA.Selenium;

  public class OptionsWrapper : IOptions
  {
    private readonly IOptions m_options;

    public OptionsWrapper(IOptions options)
    {
      if (options == null)
        throw new ArgumentNullException("options");

      m_options = options;
    }

    [ScriptMember("cookies")]
    public ICookieJar Cookies
    {
      get { return new CookieJarWrapper(m_options.Cookies); }
    }

    [ScriptMember("window")]
    public IWindow Window
    {
      get { return new WindowWrapper(m_options.Window); }
    }

    [ScriptMember("timeouts")]
    public ITimeouts Timeouts()
    {
      return new TimeoutsWrapper(m_options.Timeouts());
    }
  }
}
