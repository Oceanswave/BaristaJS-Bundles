namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using OpenQA.Selenium;

  public class ByInstance
  {
    private readonly By m_by;

    public ByInstance(By by)
    {
      if (by == null)
        throw new ArgumentNullException("by");

      m_by = by;
    }
  }
}
