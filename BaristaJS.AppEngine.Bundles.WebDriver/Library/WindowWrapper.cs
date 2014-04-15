namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using System.Drawing;
  using OpenQA.Selenium;

  public class WindowWrapper : IWindow
  {
    private readonly IWindow m_window;

    public WindowWrapper(IWindow window)
    {
      if (window == null)
        throw new ArgumentNullException("window");

      m_window = window;
    }

    [ScriptMember("maximize")]
    public void Maximize()
    {
      m_window.Maximize();
    }

    public Point Position
    {
      get
      {
        return m_window.Position;
      }
      set
      {
        m_window.Position = value;
      }
    }

    public Size Size
    {
      get
      {
        return m_window.Size;
      }
      set
      {
        m_window.Size = value;
      }
    }
  }
}
