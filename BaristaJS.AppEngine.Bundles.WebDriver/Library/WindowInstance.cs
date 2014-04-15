namespace BaristaJS.AppEngine.Bundles.WebDriver
{
  using System;
  using System.Drawing;
  using BaristaJS.AppEngine.Bundles.Library;
  using OpenQA.Selenium;

  public class WindowInstance : IWindow
  {
    private readonly IWindow m_window;

    public WindowInstance(IWindow window)
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

    [ScriptMember("position")]
    public PointInstance Position
    {
      get
      {
        return new PointInstance(m_window.Position);
      }
      set
      {
        m_window.Position = value.Point;
      }
    }

    Point IWindow.Position
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

    [ScriptMember("size")]
    public SizeInstance Size
    {
      get
      {
        return new SizeInstance(m_window.Size);
      }
      set
      {
        m_window.Size = value.Size;
      }
    }

    Size IWindow.Size
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
