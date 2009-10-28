using System;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth.VM.Types {

public class SymbolDoesNotExist : VM.Exception
{
  public SymbolDoesNotExist(String name, Package pkg)
    : base(String.Format("Symbol '{0}' does not exists in the package '{1}'", name, pkg.Name))
  {
    SymbolName = name;
    Package = pkg;
  }

  public String SymbolName { get; private set; }
  public Package Package { get; private set; }
}

public sealed class Cons
{
    public Cons()
    {
      Head = Nil;
      Tail = Nil;
    }
    public object Head { get; set; }
    public object Tail { get; set; }
    public static readonly Cons Nil = new Cons();
}


public sealed class Symbol
{
    public static readonly String ValueSlot = "value";
    public static readonly String FunctionSlot = "function";

    public Symbol(String name)
    {
      Name = name;
    }

    public String Name { get; private set; }
}

public sealed class Package
{
    public Package(String name)
    {
      Name = name;
    }
    
    public Symbol InternSymbol(String name)
    {
      if(name == null)
      {
        throw new ArgumentException("Symbol's name can't be null!");
      }

      if(!_Symbols.ContainsKey(name))
      {
        _Symbols[name] = new Symbol(name);
      }

      return _Symbols[name];
    }

    public Symbol FindSymbol(String name)
    {
      if(name == null)
      {
        throw new ArgumentException("Symbol's name can't be null!");
      }

      Symbol retVal;
      _Symbols.TryGetValue(name, out retVal);
      if(retVal == null)
      {
        throw new SymbolDoesNotExist(name, this);
      }

      return retVal;
    }

    public String Name { get; private set; }

    private Dictionary<String,Symbol> _Symbols = new Dictionary<String,Symbol>();
}

}
