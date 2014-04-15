namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using OpenQA.Selenium;

  public class TargetLocatorWrapper : ITargetLocator
  {
    private readonly ITargetLocator m_targetLocator;

    public TargetLocatorWrapper(ITargetLocator targetLocator)
    {
        if (targetLocator == null)
          throw new ArgumentNullException("targetLocator");

        m_targetLocator = targetLocator;
    }

    public IWebElement ActiveElement()
    {
      throw new NotImplementedException();
    }

    public IAlert Alert()
    {
      throw new NotImplementedException();
    }

    public IWebDriver DefaultContent()
    {
      throw new NotImplementedException();
    }

    public IWebDriver Frame(IWebElement frameElement)
    {
      throw new NotImplementedException();
    }

    public IWebDriver Frame(string frameName)
    {
      throw new NotImplementedException();
    }

    public IWebDriver Frame(int frameIndex)
    {
      throw new NotImplementedException();
    }

    public IWebDriver Window(string windowName)
    {
      throw new NotImplementedException();
    }
  }
}
