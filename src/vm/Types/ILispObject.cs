using System;

namespace Shoggoth.VM
{
public interface ILispObject
{
  void Save(ICoreWriter writer);
}
}
