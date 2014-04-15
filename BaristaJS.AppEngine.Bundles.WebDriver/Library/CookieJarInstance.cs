namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using OpenQA.Selenium;

  public class CookieJarInstance : ICookieJar
  {
    private readonly ICookieJar m_cookieJar;

    public CookieJarInstance(ICookieJar cookieJar)
    {
      if (cookieJar == null)
        throw new ArgumentNullException("cookieJar");

      m_cookieJar = cookieJar;
    }

    #region Properties

    [ScriptMember(Name = "allCookies")]
    public ReadOnlyCollection<CookieInstance> AllCookies
    {
      get
      {
        var result = m_cookieJar
          .AllCookies
          .Select(c => new CookieInstance(c))
          .ToList();

        return new ReadOnlyCollection<CookieInstance>(result);
      }
    }

    ReadOnlyCollection<Cookie> ICookieJar.AllCookies
    {
      get { return m_cookieJar.AllCookies; }
    }
    #endregion

    [ScriptMember(Name = "addCookie")]
    public void AddCookie(CookieInstance cookie)
    {
      m_cookieJar.AddCookie(cookie.Cookie);
    }

    void ICookieJar.AddCookie(Cookie cookie)
    {
      m_cookieJar.AddCookie(cookie);
    }

    [ScriptMember(Name = "deleteAllCookies")]
    public void DeleteAllCookies()
    {
      m_cookieJar.DeleteAllCookies();
    }

    [ScriptMember(Name = "deleteCookie")]
    public void DeleteCookie(CookieInstance cookie)
    {
      m_cookieJar.DeleteCookie(cookie.Cookie);
    }

    void ICookieJar.DeleteCookie(Cookie cookie)
    {
      m_cookieJar.DeleteCookie(cookie);
    }

    [ScriptMember(Name = "deleteCookieNamed")]
    public void DeleteCookieNamed(string name)
    {
      m_cookieJar.DeleteCookieNamed(name);
    }

    [ScriptMember(Name = "getCookieNamed")]
    public CookieInstance GetCookieNamed(string name)
    {
      return new CookieInstance(m_cookieJar.GetCookieNamed(name));
    }

    Cookie ICookieJar.GetCookieNamed(string name)
    {
      return m_cookieJar.GetCookieNamed(name);
    }
  }
}
