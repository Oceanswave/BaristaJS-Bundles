namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using System.Collections.Generic;
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

    /// <summary>
    /// By By!! Tee Hee Hee!
    /// </summary>
    public By By
    {
      get
      {
        return m_by;
      }
    }

    [ScriptMember(Name = "findElement")]
    public IWebElement FindElement(ISearchContext searchContext)
    {
      return new WebElementInstance(m_by.FindElement(searchContext));
    }

    [ScriptMember(Name = "findElements")]
    public IReadOnlyCollection<IWebElement> FindElements(ISearchContext searchContext)
    {
      var result = new List<IWebElement>();
      result.AddRange(m_by.FindElements(searchContext));
      return result.AsReadOnly();
    }

    [ScriptMember(Name = "equals")]
    public override bool Equals(object obj)
    {
      var byInstance = obj as ByInstance;
      return byInstance != null && byInstance.m_by.Equals(m_by);
    }

    [ScriptMember(Name = "getHashCode")]
    public override int GetHashCode()
    {
      return m_by.GetHashCode();
    }

    [ScriptMember(Name = "toString")]
    public override string ToString()
    {
      return m_by.ToString();
    }

    [ScriptMember(Name="className")]
    public static ByInstance ClassName(string classNameToFind)
    {
      return new ByInstance(By.ClassName(classNameToFind));
    }

    [ScriptMember(Name = "cssSelector")]
    public static ByInstance CssSelector(string cssSelectorToFind)
    {
      return new ByInstance(By.CssSelector(cssSelectorToFind));
    }

    [ScriptMember(Name = "id")]
    public static ByInstance Id(string idToFind)
    {
      return new ByInstance(By.Id(idToFind));
    }

    [ScriptMember(Name = "linkText")]
    public static ByInstance LinkText(string linkTextToFind)
    {
      return new ByInstance(By.LinkText(linkTextToFind));
    }

    [ScriptMember(Name = "name")]
    public static ByInstance Name(string nameToFind)
    {
      return new ByInstance(By.Name(nameToFind));
    }

    [ScriptMember(Name = "partialLinkText")]
    public static ByInstance PartialLinkText(string partialLinkTextToFind)
    {
      return new ByInstance(By.PartialLinkText(partialLinkTextToFind));
    }

    [ScriptMember(Name = "tagName")]
    public static ByInstance TagName(string tagNameToFind)
    {
      return new ByInstance(By.TagName(tagNameToFind));
    }

    [ScriptMember(Name = "xPath")]
    public static ByInstance XPath(string xpathToFind)
    {
      return new ByInstance(By.XPath(xpathToFind));
    }
  }
}
