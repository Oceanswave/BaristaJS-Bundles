namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using BaristaJS.AppEngine.Bundles.WebDriver.Library;
  using OpenQA.Selenium;
  using OpenQA.Selenium.PhantomJS;

  public class WebDriverInstance<T> : IWebDriver
    where T : class, IWebDriver, new()
  {
    private readonly T m_webDriver;

    public WebDriverInstance()
    {

      if (typeof (T) == typeof(PhantomJSDriver))
      {
        var newDriver = new PhantomJSDriver(WebDriverBundle.PhantomJSDriverService);
        m_webDriver = newDriver as T;
      }
      else
      {
        m_webDriver = new T();
      }
    }

    public WebDriverInstance(T webDriver)
    {
      if (webDriver == null)
        throw new ArgumentNullException("webDriver");

      m_webDriver = webDriver;
    }

    #region Properties
    [NoScriptAccess]
    public T WebDriver 
    {
      get { return m_webDriver; }
    }

    [ScriptMember(Name = "currentWindowHandle")]
    public string CurrentWindowHandle
    {
      get { return m_webDriver.CurrentWindowHandle; }
    }

    [ScriptMember(Name = "pageSource")]
    public string PageSource
    {
      get { return m_webDriver.PageSource; }
    }

    [ScriptMember(Name = "title")]
    public string Title
    {
      get { return m_webDriver.Title; }
    }

    [ScriptMember(Name = "url")]
    public string Url
    {
      get { return m_webDriver.Url; }
      set { m_webDriver.Url = value; }
    }

    //TODO: Figure out what to do with this...
    public ReadOnlyCollection<string> WindowHandles
    {
      get { return m_webDriver.WindowHandles; }
    }
    #endregion

    [ScriptMember(Name="close")]
    public void Close()
    {
      m_webDriver.Close();
    }

    [ScriptMember(Name = "keyboard")]
    public object Keyboard()
    {
      var inputDevices = m_webDriver as OpenQA.Selenium.IHasInputDevices;
        
      if (inputDevices == null)
        return Undefined.Value;

      return new KeyboardInstance(inputDevices.Keyboard);
    }

    [ScriptMember(Name = "manage")]
    public IOptions Manage()
    {
      return new OptionsInstance(m_webDriver.Manage());
    }

    [ScriptMember(Name = "mouse")]
    public object Mouse()
    {
      var inputDevices = m_webDriver as OpenQA.Selenium.IHasInputDevices;

      if (inputDevices == null)
        return Undefined.Value;

      return new MouseInstance(inputDevices.Mouse);
    }

    [ScriptMember(Name = "navigate")]
    public INavigation Navigate()
    {
      return new NavigationInstance(m_webDriver.Navigate());
    }

    [ScriptMember(Name = "quit")]
    public void Quit()
    {
      m_webDriver.Quit();
    }

    [ScriptMember(Name = "switchTo")]
    public ITargetLocator SwitchTo()
    {
      return new TargetLocatorInstance(m_webDriver.SwitchTo());
    }

    [ScriptMember(Name = "findElement")]
    public IWebElement FindElement(By by)
    {
      return new WebElementInstance(m_webDriver.FindElement(by));
    }

    [ScriptMember(Name = "findElements")]
    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
      var result = new List<IWebElement>();
      result.AddRange(m_webDriver.FindElements(by));
      return result.AsReadOnly();
    }

    [ScriptMember(Name = "getScreenshot")]
    public ScreenshotInstance GetScreenshot()
    {
      var takesScreenshot = m_webDriver as ITakesScreenshot;
      if (takesScreenshot == null)
        return null;

      return new ScreenshotInstance(takesScreenshot.GetScreenshot());
    }

    [ScriptMember(Name = "executeScript")]
    public object ExecuteScript(string script, params object[] args)
    {
      var executor = m_webDriver as OpenQA.Selenium.IJavaScriptExecutor;
      return executor != null
        ? executor.ExecuteScript(script, args)
        : Undefined.Value;
    }

    public void Dispose()
    {
      m_webDriver.Dispose();
    }
  }
}