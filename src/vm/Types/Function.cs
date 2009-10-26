using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace Shoggoth.VM.Types {

public class Function
{
    public bool IsForeign { get; set; }
    public MethodInfo MethodInfo { get; set; }

    public object Invoke(params object[] parameters)
    {
      if(this.MethodInfo.IsStatic)
      {
        return this.MethodInfo.Invoke(null, parameters);
      }
      else
      {
        object obj = parameters[0];
        Object[] realParam = new Object[parameters.Length - 1];
        Array.Copy(parameters, 1, realParam, 0, parameters.Length - 1);
        return this.MethodInfo.Invoke(obj, realParam);
      }
    }
}

}