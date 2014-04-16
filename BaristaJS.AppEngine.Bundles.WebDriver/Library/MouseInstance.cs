namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using OpenQA.Selenium;
  using OpenQA.Selenium.Interactions.Internal;

  public class MouseInstance : IMouse
  {
    private readonly IMouse m_mouse;

    public MouseInstance(IMouse mouse)
    {
      if (mouse == null)
        throw new ArgumentNullException("mouse");

      m_mouse = mouse;
    }

    public void Click(ICoordinates where)
    {
      throw new NotImplementedException();
    }

    public void ContextClick(ICoordinates where)
    {
      throw new NotImplementedException();
    }

    public void DoubleClick(ICoordinates where)
    {
      throw new NotImplementedException();
    }

    public void MouseDown(ICoordinates where)
    {
      throw new NotImplementedException();
    }

    public void MouseMove(ICoordinates where, int offsetX, int offsetY)
    {
      throw new NotImplementedException();
    }

    public void MouseMove(ICoordinates where)
    {
      throw new NotImplementedException();
    }

    public void MouseUp(ICoordinates where)
    {
      throw new NotImplementedException();
    }
  }
}
