namespace BaristaJS.AppEngine.Bundles.Library
{
  using System;
  using System.Drawing;

  [Serializable]
  public class SizeInstance
  {
    private Size m_size;

    public SizeInstance()
    {
      m_size = new Size();
    }

    public SizeInstance(Size size)
    {
      if (size == null)
        throw new ArgumentNullException("size");

      m_size = size;
    }

    public SizeInstance(int width, int height)
    {
      m_size = new Size(width, height);
    }

    public SizeInstance(Bundles.Library.PointInstance point)
    {
      m_size = new Size(point.Point);
    }

    public Size Size
    {
      get { return m_size; }
    }

    [ScriptMember(Name = "height")]
    public int Height
    {
      get { return m_size.Height; }
      set { m_size.Height = value; }
    }

    [ScriptMember(Name = "Width")]
    public int Width
    {
      get { return m_size.Width; }
      set { m_size.Width = value; }
    }

    [ScriptMember(Name = "isEmpty")]
    public bool IsEmpty
    {
      get { return m_size.IsEmpty; }
    }

    [ScriptMember(Name = "toString")]
    public override string ToString()
    {
      return m_size.ToString();
    }
  }
}