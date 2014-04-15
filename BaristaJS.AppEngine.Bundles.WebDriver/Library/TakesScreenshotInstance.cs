namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using OpenQA.Selenium;

  public class TakesScreenshotInstance : ITakesScreenshot
  {
    private readonly ITakesScreenshot m_takesScreenshot;

    public TakesScreenshotInstance(ITakesScreenshot takesScreenshot)
    {
      if (takesScreenshot == null)
        throw new ArgumentNullException("takesScreenshot");

      m_takesScreenshot = takesScreenshot;
    }

    public ScreenshotInstance GetScreenshot()
    {
      return new ScreenshotInstance(m_takesScreenshot.GetScreenshot());
    }

    Screenshot ITakesScreenshot.GetScreenshot()
    {
      return m_takesScreenshot.GetScreenshot();
    }
  }
}
