namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using OpenQA.Selenium;

  public class OptionsInstance : IOptions
  {
    private readonly IOptions m_options;

    public OptionsInstance(IOptions options)
    {
      if (options == null)
        throw new ArgumentNullException("options");

      m_options = options;
    }

    [ScriptMember("cookies")]
    public ICookieJar Cookies
    {
      get { return new CookieJarInstance(m_options.Cookies); }
    }

    [ScriptMember("window")]
    public IWindow Window
    {
      get { return new WindowInstance(m_options.Window); }
    }

    [ScriptMember("timeouts")]
    public ITimeouts Timeouts()
    {
      return new TimeoutsInstance(m_options.Timeouts());
    }
  }
}
