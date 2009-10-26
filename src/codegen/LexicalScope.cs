using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;

namespace Shoggoth.Codegen {

public sealed class LexicalScope
{
    public static readonly LexicalScope Null = new LexicalScope();

    public LocalBuilder Bind(Symbol sym, String slot, LocalBuilder localVar)
    {
      Key key = new Key(sym, slot);
      _Bindings[key] = localVar;
      return localVar;
    }

    public LocalBuilder Value(Symbol sym, String slot)
    {
      Key key = new Key(sym, slot);
      LocalBuilder retVal;
      _Bindings.TryGetValue(key, out retVal);
      if(retVal == null)
      {
        if(_Parent != LexicalScope.Null)
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
      return (_Bindings.ContainsKey(key) || ((_Parent != LexicalScope.Null) && _Parent.IsBound(sym, slot)));
    }

    public LexicalScope BeginScope()
    {
      return new LexicalScope(this);
    }
    
    public LexicalScope EndScope()
    {
      return this._Parent;
    }

    private LexicalScope()
    {}

    private LexicalScope(LexicalScope parent)
    {
      _Parent = parent;
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

    private Dictionary<Key, LocalBuilder> _Bindings = new Dictionary<Key, LocalBuilder>();
    private LexicalScope _Parent = null;
}

}