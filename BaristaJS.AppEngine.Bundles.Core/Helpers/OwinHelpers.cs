namespace BaristaJS.AppEngine.Bundles
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public static class OwinHelpers
  {
    public static void SetHeaderIfNotExist(IDictionary<string, object> env, string headerKey, params string[] values)
    {
      var headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];

      var existingValue =
        headers.FirstOrDefault(h => String.Equals(h.Key, headerKey, StringComparison.Ordinal));

      if (!Object.Equals(default(KeyValuePair<string, string[]>), existingValue))
        return;

      existingValue = new KeyValuePair<string, string[]>(headerKey, values);
      headers.Add(existingValue);
    }
  }
}
