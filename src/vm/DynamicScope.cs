using System;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM.Types;

namespace Shoggoth.VM {

public class UnboundSymbolException : VM.Exception
{
  public UnboundSymbolException(Symbol sym, String slot)
    : base(String.Format("Symbol's {0} slot {1} is unbound!", sym.Name, slot))
  {
    Symbol = sym;
    Slot = slot;
  }

  public Symbol Symbol { get; private set; }
  public String Slot { get; private set; }
}

public sealed class DynamicScope
{
    public static readonly DynamicScope Null = new DynamicScope();

    public void Bind(Symbol sym, String slot, object value)
    {
      Key key = new Key(sym, slot);
      _Bindings[key] = value;
    }

    public object Value(Symbol sym, String slot)
    {     
      Key key = new Key(sym, slot);
      object retVal;
      _Bindings.TryGetValue(key, out retVal);
      if(retVal == null)
      {
        if(_Parent != DynamicScope.Null)
        {
          return _Parent.Value(sym, slot);
        }
        else throw new UnboundSymbolException(sym, slot);
      }
      return retVal;
    }

    public bool IsBound(Symbol sym, String slot)
    {
      Key key = new Key(sym, slot);
      return (_Bindings.ContainsKey(key) || ((_Parent != DynamicScope.Null) && _Parent.IsBound(sym, slot)));
    }

    public DynamicScope BeginScope()
    {
      return new DynamicScope(this);
    }
    
    public DynamicScope EndScope()
    {
      return this._Parent;
    }

    public DynamicScope(DynamicScope parent)
    {
      _Parent = parent;
    }

    public DynamicScope()
    {
    }

    private class Key : IEquatable<Key>
    {
      public Key(Symbol sym, String slot)
      {
        Sym = sym;
        Slot = slot;
      }

      public bool Equals(Key other)
      {
        return ((Sym == other.Sym) && (Slot == other.Slot));
      }

      public readonly Symbol Sym;
      public readonly String Slot;
    }
    
    private DynamicScope _Parent = null;
    private static Dictionary<Key, Object> _Bindings = new Dictionary<Key, Object>();
}
}
