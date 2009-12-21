using System;
using System.Linq;
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
      if(obj == null)
      {
        throw new ArgumentException("obj");
      }

      if(obj.GetType() != type)
      {
        throw new TypeError(obj, type);
      }
    }

    public static void Implements(Object obj, Type iface)
    {
      if(obj == null)
      {
        throw new ArgumentException("obj");
      }

      if(!obj.GetType().GetInterfaces().Contains(iface))
      {
        throw new TypeError(obj, iface);
      }
    }
}

}
