using System;
using System.Collections;
using System.Collections.Generic;

namespace CLnet {
namespace VM {

static class TypeResolver
{
    public static String GetTypeRef(Type t)
    {
      return _Types[t];
    }

    private static Dictionary<Type, String> InitTypeCache()
    {
      var retVal = new Dictionary<Type, String>();
      retVal.Add(typeof(Types.Symbol), "symbol");
      retVal.Add(typeof(Types.Cons), "cons");
      retVal.Add(typeof(String), "string");
      retVal.Add(typeof(int), "int32");
      return retVal;
    }

    static Dictionary<Type, String> _Types = InitTypeCache();
}
}}
