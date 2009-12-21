using System;

namespace Shoggoth.VM
{
public class LispCore
{
    public static LispCore Instance = LoadCore();


    private static LispCore LoadCore()
    {
      return new LispCore(); // STUB!
    }
}
}
