using System;
using Shoggoth.VM.Types;

namespace Shoggoth.VM
{
public class State : ILispObject
{
  public static State Instance = new State();

  public DummyVector<Package> Packages = new DummyVector<Package>();
  
  private State() {} // Yeah, it's a singleton

  public void Save(ICoreWriter writer)
  {
    //STUB!
  }
  
}
}
