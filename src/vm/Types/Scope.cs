using System;
using System.Collections;
using System.Collections.Generic;

namespace CLnet {
namespace VM {
namespace Types {

public class UnboundSymbolException : VM.Exception
{
  public UnboundSymbolException(Symbol sym, String slot)
    : base(String.Format("Symbol's {0} slot {1} is unbound!", sym, slot))
  {}
}

public sealed class Scope
{
    public static readonly Scope Null = new Scope();

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
        if(_Parent != Scope.Null)
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
      return (_Bindings.ContainsKey(key) || ((_Parent != Scope.Null) && _Parent.IsBound(sym, slot)));
    }

    public Scope NewScope()
    {
      return new Scope(this);
    }
    
    public Scope EndScope()
    {
      return this._Parent;
    }

    private Scope(Scope parent)
    {
      _Parent = parent;
    }

    private Scope()
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
    
    private Scope _Parent = null;
    private static Dictionary<Key, Object> _Bindings = new Dictionary<Key, Object>();
}
}}}
