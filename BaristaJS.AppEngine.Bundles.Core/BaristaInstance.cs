namespace BaristaJS.AppEngine.Bundles
{
  using BaristaJS.AppEngine;
  using System;
  using System.Collections.Generic;

  public class BaristaInstance : IScriptableObject
  {
    private IScriptEngine m_engine;

    public BaristaInstance(IBundleManager bundleManager, string packageSource, IDictionary<string, object> env)
    {
      if (bundleManager == null)
        throw new ArgumentNullException("bundleManager");

      if (String.IsNullOrWhiteSpace(packageSource))
        throw new ArgumentNullException("packageSource");

      if (env == null)
        throw new ArgumentNullException("env");

      BundleManager = bundleManager;
      PackageSource = packageSource;
      Environment = env;
    }

    #region Properties

    [NoScriptAccess]
    public IBundleMetadata Metadata
    {
      get;
      set;
    }

    [NoScriptAccess]
    public IBundleManager BundleManager
    {
      get;
      private set;
    }

    [NoScriptAccess]
    public IDictionary<string, object> Environment
    {
      get;
      private set;
    }

    [NoScriptAccess]
    public string PackageSource
    {
      get;
      set;
    }

    [ScriptMember(Name = "happyPuppy")]
    public string HappyPuppy
    {
      get;
      set;
    }

    [ScriptMember(Name = "version")]
    public string Version
    {
      get { return this.Metadata.Version.ToString(); }
    }
    #endregion

    [ScriptMember(Name = "isArray")]
    public bool IsArray(object value)
    {
      return TypeUtilities.IsArray(value);
    }

    [ScriptMember(Name = "isDate")]
    public bool IsDate(object value)
    {
      return TypeUtilities.IsDate(value);
    }

    [ScriptMember(Name = "isDefined")]
    public bool IsDefined(object value)
    {
      return !TypeUtilities.IsUndefined(value);
    }

    [ScriptMember(Name = "isFunction")]
    public bool IsFunction(object value)
    {
      return TypeUtilities.IsFunction(value);
    }

    [ScriptMember(Name = "isNumber")]
    public bool IsNumber(object value)
    {
      return TypeUtilities.IsNumeric(value);
    }

    [ScriptMember(Name = "isObject")]
    public bool IsObject(object value)
    {
      return TypeUtilities.IsObject(value);
    }

    [ScriptMember(Name = "isString")]
    public bool IsString(object value)
    {
      return TypeUtilities.IsString(value);
    }

    [ScriptMember(Name = "isUndefined")]
    public bool IsUndefined(object value)
    {
      return TypeUtilities.IsUndefined(value);
    }

    [ScriptMember(Name = "lowercase")]
    public string Lowercase(object value)
    {
      return Convert.ToString(value).ToLowerInvariant();
    }

    [ScriptMember(Name = "uppercase")]
    public string Uppercase(object value)
    {
      return Convert.ToString(value).ToUpperInvariant();
    }

    [ScriptMember("installedBundles")]
    public IList<IBundle> InstalledBundles
    {
      get { return BundleManager.InstalledBundles; }
    }

    [ScriptMember("require")]
    public object Require(string packageId, string version = null)
    {
      var bundle = BundleManager.GetBundle<IBundle>(Environment, PackageSource, packageId, version);
      return bundle.InstallBundle(m_engine);
    }

    [NoScriptAccess]
    public void OnExposedToScriptCode(IScriptEngine engine)
    {
      if (engine == null)
        throw new ArgumentNullException("engine");

      if (m_engine == null)
      {
        m_engine = engine;
        return;
      }

      if (engine != m_engine)
      {
        throw new ArgumentException("Invalid script engine", "engine");
      }
    }
  }
}
