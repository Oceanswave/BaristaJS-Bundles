namespace BaristaJS.AppEngine.Bundles
{
  using System;

  /// <summary>
  /// Contains type-related functionality that isn't conversion or comparison.
  /// </summary>
  public static class TypeUtilities
  {
    /// <summary>
    /// Returns <c>true</c> if the given value is undefined.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is undefined; <c>false</c> otherwise. </returns>
    public static bool IsUndefined(object obj)
    {
      return obj == null || obj == Undefined.Value;
    }

    /// <summary>
    /// Returns <c>true</c> if the given is an array.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is undefined; <c>false</c> otherwise. </returns>
    public static bool IsArray(object obj)
    {
      //return obj is ArrayInstance;

      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns <c>true</c> if the given value is a supported numeric type.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is a supported numeric type; <c>false</c>
    /// otherwise. </returns>
    public static bool IsNumeric(object obj)
    {
      return obj is double || obj is SByte || obj is Int16 || obj is Int32 || obj is Int64 || obj is byte || obj is UInt16 || obj is UInt32 || obj is UInt64;
    }

    /// <summary>
    /// Returns <c>true</c> if the given value is a function.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is a function; <c>false</c>
    /// otherwise. </returns>
    public static bool IsFunction(object obj)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns <c>true</c> if the given value is a supported string type.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is a supported string type; <c>false</c>
    /// otherwise. </returns>
    public static bool IsString(object obj)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns <c>true</c> if the given value is a supported date type.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is a supported date type; <c>false</c>
    /// otherwise. </returns>
    public static bool IsDate(object obj)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns <c>true</c> if the given value is an object.
    /// </summary>
    /// <param name="obj"> The object to check. </param>
    /// <returns> <c>true</c> if the given value is an object; <c>false</c>
    /// otherwise. </returns>
    public static bool IsObject(object obj)
    {
      throw new NotImplementedException();
    }
  }
}
