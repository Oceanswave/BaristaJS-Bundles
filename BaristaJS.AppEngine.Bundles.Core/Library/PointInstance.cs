namespace BaristaJS.AppEngine.Bundles.Library
{
  using System;
  using System.Drawing;

  [Serializable]
  public class PointInstance
  {
    private Point m_point;

    public PointInstance()
    {
      m_point = new Point();
    }

    public PointInstance(int dw)
    {
      m_point = new Point(dw);
    }

    public PointInstance(Point point)
    {
      if (point == null)
        throw new ArgumentNullException("point");

      m_point = point;
    }

    public PointInstance(int x, int y)
    {
      m_point = new Point(x, y);
    }

    public PointInstance(SizeInstance size)
    {
      m_point = new Point(size.Size);
    }

    public Point Point
    {
      get { return m_point; }
    }

    [ScriptMember(Name = "x")]
    public int X
    {
      get { return m_point.X; }
      set { m_point.X = value; }
    }

    [ScriptMember(Name = "y")]
    public int Y
    {
      get { return m_point.Y; }
      set { m_point.Y = value; }
    }

    [ScriptMember(Name = "isEmpty")]
    public bool IsEmpty
    {
      get { return m_point.IsEmpty; }
    }

    [ScriptMember(Name = "offset")]
    public void Offset(int dx, int dy)
    {
      m_point.Offset(dx, dy);
    }

    [ScriptMember(Name = "toString")]
    public override string ToString()
    {
      return m_point.ToString();
    }
  }
}