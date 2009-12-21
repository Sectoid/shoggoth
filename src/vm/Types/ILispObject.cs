using System;

namespace Shoggoth.VM
{
public interface ILispObject
{
  void SaveToIL(LispCore core);
}
}
