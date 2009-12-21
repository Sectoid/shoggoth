using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth.VM
{

public static class Assert
{
    public static void TypeIs(Object obj, Type type)
    {
      if(obj.GetType() != type)
      {
        throw new TypeError(obj, type);
      }
    }
}

}
