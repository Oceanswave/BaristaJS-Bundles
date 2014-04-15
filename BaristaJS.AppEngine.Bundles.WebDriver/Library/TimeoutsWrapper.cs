namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using OpenQA.Selenium;

  public class TimeoutsWrapper : ITimeouts
  {
    private readonly ITimeouts m_timeouts;

    public TimeoutsWrapper(ITimeouts timeouts)
    {
      if (timeouts == null)
        throw new ArgumentNullException("timeouts");

      m_timeouts = timeouts;
    }

    [ScriptMember(Name = "implicitlyWait")]
    public ITimeouts ImplicitlyWait(string timeToWait)
    {
      var ts = TimeSpan.Parse(timeToWait);
      return new TimeoutsWrapper(m_timeouts.ImplicitlyWait(ts));
    }

    ITimeouts ITimeouts.ImplicitlyWait(TimeSpan timeToWait)
    {
      return m_timeouts.ImplicitlyWait(timeToWait);
    }

    [ScriptMember(Name = "setPageLoadTimeout")]
    public ITimeouts SetPageLoadTimeout(string timeToWait)
    {
      var ts = TimeSpan.Parse(timeToWait);
      return new TimeoutsWrapper(m_timeouts.SetPageLoadTimeout(ts));
    }

    ITimeouts ITimeouts.SetPageLoadTimeout(TimeSpan timeToWait)
    {
      return m_timeouts.SetPageLoadTimeout(timeToWait);
    }

    [ScriptMember(Name = "setScriptTimeout")]
    public ITimeouts SetScriptTimeout(string timeToWait)
    {
      var ts = TimeSpan.Parse(timeToWait);
      return new TimeoutsWrapper(m_timeouts.SetPageLoadTimeout(ts));
    }

    public ITimeouts SetScriptTimeout(TimeSpan timeToWait)
    {
      return m_timeouts.SetPageLoadTimeout(timeToWait);
    }
  }
}
