namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Drawing;
  using OpenQA.Selenium;

  public class WebElementWrapper : IWebElement
  {
    private readonly IWebElement m_webElement;

    public WebElementWrapper(IWebElement element)
    {
      if (element == null)
        throw new ArgumentNullException("element");

      m_webElement = element;
    }

    #region Properties
    [ScriptMember(Name = "displayed")]
    public bool Displayed
    {
      get { return m_webElement.Displayed; }
    }

    [ScriptMember(Name = "enabled")]
    public bool Enabled
    {
      get { return m_webElement.Enabled; }
    }

    public Point Location
    {
      get { throw new NotImplementedException(); }
    }

    [ScriptMember(Name = "selected")]
    public bool Selected
    {
      get { return m_webElement.Selected; }
    }

    public Size Size
    {
      get { throw new NotImplementedException(); }
    }

    [ScriptMember(Name = "tagName")]
    public string TagName
    {
      get { return m_webElement.TagName; }
    }

    [ScriptMember(Name = "text")]
    public string Text
    {
      get { return m_webElement.Text; }
    }
    #endregion

    [ScriptMember(Name = "clear")]
    public void Clear()
    {
      m_webElement.Clear();
    }

    [ScriptMember(Name = "click")]
    public void Click()
    {
      m_webElement.Click();
    }

    [ScriptMember(Name = "getAttribute")]
    public string GetAttribute(string attributeName)
    {
      return m_webElement.GetAttribute(attributeName);
    }

    [ScriptMember(Name = "getCssValue")]
    public string GetCssValue(string propertyName)
    {
      return m_webElement.GetCssValue(propertyName);
    }

    [ScriptMember(Name = "sendKeys")]
    public void SendKeys(string text)
    {
      m_webElement.SendKeys(text);
    }

    [ScriptMember(Name = "submit")]
    public void Submit()
    {
      m_webElement.Submit();
    }

    [ScriptMember(Name = "submit")]
    public IWebElement FindElement(By by)
    {
      return new WebElementWrapper(m_webElement.FindElement(by));
    }

    [ScriptMember(Name = "findElements")]
    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
      var result = new List<IWebElement>();
      result.AddRange(m_webElement.FindElements(by));
      return result.AsReadOnly();
    }
  }
}
