namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;
  using BaristaJS.AppEngine.Bundles.Library;
  using OpenQA.Selenium;

  public class ScreenshotInstance : IOwinScriptObject
  {
    private readonly Screenshot m_screenshot;

    public ScreenshotInstance(Screenshot screenshot)
    {
      if (screenshot == null)
        throw new ArgumentNullException("screenshot");

      m_screenshot = screenshot;
    }

    [ScriptMember(Name = "asBase64EncodedString")]
    public string AsBase64EncodedString()
    {
      return m_screenshot.AsBase64EncodedString;
    }

    [ScriptMember(Name = "asBase64EncodedByteArray")]
    public Base64EncodedByteArrayInstance AsBase64EncodedByteArray()
    {
      return new Base64EncodedByteArrayInstance(m_screenshot.AsByteArray);
    }

    public async Task Invoke(IDictionary<string, object> env)
    {
      OwinHelpers.SetHeaderIfNotExist(env, "Content-Type", "image/png");
      //OwinHelpers.SetHeaderIfNotExist(env, "Content-Length", m_screenshot.AsByteArray.Length.ToString(CultureInfo.InvariantCulture));

      using (var writer = new StreamWriter((Stream)env["owin.ResponseBody"], Encoding.UTF8))
      {
        await writer.WriteAsync(Encoding.UTF8.GetChars(m_screenshot.AsByteArray));
      }
    }
  }
}
