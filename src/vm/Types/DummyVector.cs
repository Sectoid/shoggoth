using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Shoggoth.VM.Types
{
public class DummyVector<T> : List<T>, ILispObject
  where T : ILispObject
{
  public void Save(ICoreWriter writer)
  {
    var thisType = typeof(DummyVector<T>);
    var ilGen = writer.GetILGenerator();

    ilGen.Emit(OpCodes.Newobj, thisType.GetConstructor(Type.EmptyTypes));

    foreach (ILispObject item in this)
    {
      ilGen.Emit(OpCodes.Dup); // duplicate instance reference
      item.Save(writer); // insert item creation code -- it should
                         // leave reference to item on the top of the stack
      ilGen.Emit(OpCodes.Callvirt, thisType.GetMethod("Add")); // add item
    }
  }
}
}