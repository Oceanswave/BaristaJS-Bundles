namespace BaristaJS.AppEngine.Bundles.Library
{
  using System.Globalization;
  using System.IO;
  using System.Threading.Tasks;
  using BaristaJS.AppEngine.Extensions;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  [Serializable]
  public class Base64EncodedByteArrayInstance : IOwinScriptObject
  {
    private readonly List<Byte> m_data = new List<byte>();

    public Base64EncodedByteArrayInstance()
    {
      this.MimeType = "application/octet-stream";
    }

    public Base64EncodedByteArrayInstance(byte[] data)
      : this()
    {
      if (data != null && data.Length > 0)
        this.m_data = new List<byte>(data);
    }

    public Base64EncodedByteArrayInstance(string data)
      : this()
    {
      if (String.IsNullOrWhiteSpace(data) == false && data.Length > 0)
        this.m_data = new List<byte>(data.ToByteArray());
    }

    public byte[] Data
    {
      get { return m_data.ToArray(); }
    }

    /// <summary>
    /// Overwrites the data contained in the current byte array with data contained in the specified array.
    /// </summary>
    /// <param name="data"></param>
    public void Copy(byte[] data)
    {
      this.m_data.Clear();
      m_data.AddRange(data);
    }

    [ScriptMember(Name = "mimeType")]
    public string MimeType
    {
      get;
      set;
    }

    [ScriptMember(Name = "fileName")]
    public string FileName
    {
      get;
      set;
    }

    [ScriptMember(Name = "length")]
    public double Length
    {
      get { return m_data.LongCount(); }
    }

    [ScriptMember(Name = "append")]
    public void Append(string data)
    {
      m_data.AddRange(data.ToByteArray());
    }

    [ScriptMember(Name = "getByteAt")]
    public string GetByteAt(int index)
    {
      var bits = new[] {m_data[index]};
      return bits.AsHexString();
    }

    [ScriptMember(Name = "setByteAt")]
    public void SetByteAt(int index, string data)
    {
      var byteData = data.ToByteArray();

      m_data[index] = byteData[0];
    }

    [ScriptMember(Name = "toAsciiString")]
    public string ToAsciiString()
    {
      return Encoding.ASCII.GetString(m_data.ToArray());
    }

    [ScriptMember(Name = "toUtf8String")]
    public string ToUtf8String()
    {
      return Encoding.UTF8.GetString(m_data.ToArray());
    }

    [ScriptMember(Name = "toUnicodeString")]
    public string ToUnicodeString()
    {
      return Encoding.Unicode.GetString(m_data.ToArray());
    }

    [ScriptMember(Name = "toBase64String")]
    public string ToBase64String()
    {
      return Convert.ToBase64String(m_data.ToArray());
    }

    [ScriptMember(Name = "toString")]
    public override string ToString()
    {
      return (m_data.ToArray()).AsHexString();
    }

    public async Task Invoke(IDictionary<string, object> env)
    {
      OwinHelpers.SetHeaderIfNotExist(env, "Content-Disposition", "attachment; filename=\"" + FileName + "\"");
      OwinHelpers.SetHeaderIfNotExist(env, "Content-Type", MimeType);
      OwinHelpers.SetHeaderIfNotExist(env, "Content-Length", Length.ToString(CultureInfo.InvariantCulture));
      
      using (var writer = new StreamWriter((Stream)env["owin.ResponseBody"], Encoding.UTF8))
      {
        await writer.WriteAsync(Encoding.UTF8.GetChars(m_data.ToArray()));
      }
    }

    [ScriptMember(Name = "createFromString")]
    public static Base64EncodedByteArrayInstance CreateFromString(string data)
    {
      return new Base64EncodedByteArrayInstance(Encoding.UTF8.GetBytes(data));
    }
  }
}