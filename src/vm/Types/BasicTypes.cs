using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;

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

public sealed class Cons : ILispObject
{
    public Cons()
    {
      Head = Nil;
      Tail = Nil;
    }
    public object Head { get; set; }
    public object Tail { get; set; }
    public static readonly Cons Nil = new Cons();
    
    public void Save(ICoreWriter writer)
    {
      Assert.Implements(Head, typeof(ILispObject));
      Assert.Implements(Tail, typeof(ILispObject));

      var thisType = typeof(Cons);
      var ilGen = writer.GetILGenerator();
      ilGen.Emit(OpCodes.Newobj, thisType.GetConstructor(Type.EmptyTypes));
      ilGen.Emit(OpCodes.Dup);
      (Head as ILispObject).Save(writer);
      // pop Head 
      ilGen.Emit(OpCodes.Dup);
      (Head as ILispObject).Save(writer);
      // pop Tail
      
    }
}


public sealed class Symbol : ILispObject
{
    public static readonly String ValueSlot = "value";
    public static readonly String FunctionSlot = "function";

    public Symbol(String name)
    {
      Name = name;
    }

    public String Name { get; private set; }
    public void Save(ICoreWriter writer)
    {
      var thisType = typeof(Symbol);
      var ilGen = writer.GetILGenerator();
      ilGen.Emit(OpCodes.Ldstr, this.Name);
      ilGen.Emit(OpCodes.Newobj, thisType.GetConstructor(new Type[] { typeof(String), }));
    }
    
}

public sealed class Package : ILispObject
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
    public void Save(ICoreWriter writer)
    {
      throw new NotImplementedException();
    }

    private Dictionary<String,Symbol> _Symbols = new Dictionary<String,Symbol>();
}

}
