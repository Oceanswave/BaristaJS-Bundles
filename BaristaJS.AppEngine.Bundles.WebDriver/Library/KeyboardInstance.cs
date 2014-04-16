namespace BaristaJS.AppEngine.Bundles.WebDriver.Library
{
  using OpenQA.Selenium;
  using System;

  public class KeyboardInstance : IKeyboard
  {
    private readonly IKeyboard m_keyboard;

    public KeyboardInstance(IKeyboard keyboard)
    {
      if (keyboard == null)
        throw new ArgumentNullException("keyboard");

      m_keyboard = keyboard;
    }

    [ScriptMember("pressKey")]
    public void PressKey(string keyToPress)
    {
      m_keyboard.PressKey(keyToPress);
    }

    [ScriptMember("releaseKey")]
    public void ReleaseKey(string keyToRelease)
    {
      m_keyboard.ReleaseKey(keyToRelease);
    }

    [ScriptMember("sendKeys")]
    public void SendKeys(string keySequence)
    {
      m_keyboard.SendKeys(keySequence);
    }
  }
}
