namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using OpenQA.Selenium;

  public class NavigationInstance : INavigation
  {
    private readonly INavigation m_navigation;

    public NavigationInstance(INavigation navigation)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");

      m_navigation = navigation;
    }

    [ScriptMember(Name="back")]
    public void Back()
    {
      m_navigation.Back();
    }

    [ScriptMember(Name = "forward")]
    public void Forward()
    {
      m_navigation.Forward();
    }

    //public void GoToUrl(Uri url)
    //{
    //  m_navigation.GoToUrl(url);
    //}

    [ScriptMember(Name = "goToUrl")]
    public void GoToUrl(string url)
    {
      m_navigation.GoToUrl(url);
    }

    [ScriptMember(Name = "refresh")]
    public void Refresh()
    {
      m_navigation.Refresh();
    }
  }
}
