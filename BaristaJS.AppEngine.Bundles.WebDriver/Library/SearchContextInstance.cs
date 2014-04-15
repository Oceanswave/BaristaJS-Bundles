namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using OpenQA.Selenium;

  public class SearchContextInstance : ISearchContext
  {
    private readonly ISearchContext m_searchContext;

    public SearchContextInstance(ISearchContext searchContext)
    {
      if (searchContext == null)
        throw new ArgumentNullException("searchContext");
      
      m_searchContext = searchContext;
    }

    public IWebElement FindElement(ByInstance by)
    {
      return new WebElementInstance(m_searchContext.FindElement(by.By));
    }

    public ReadOnlyCollection<IWebElement> FindElements(ByInstance by)
    {
      var result = new List<IWebElement>();
      result.AddRange(m_searchContext.FindElements(by.By));
      return result.AsReadOnly();
    }

    IWebElement ISearchContext.FindElement(By by)
    {
      return m_searchContext.FindElement(by);
    }

    ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
    {
      return m_searchContext.FindElements(by);
    }
  }
}
