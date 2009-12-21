using System;
using System.Reflection.Emit;

namespace Shoggoth.VM
{
public interface ICoreWriter
{
    ILGenerator GetILGenerator();
    // STUB!
}
}
