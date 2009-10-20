using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM.Types;

namespace Shoggoth.VM {

public class TypeError : VM.Exception
{
  public TypeError(object x, Type t)
    : base(String.Format("Object {0} is not of type {1}", x, t))
  {
    Obj = x;
    Type = t;
  }

  public Object Obj { get; private set; }
  public Type Type { get; private set; }
}

public static class Evaluator
{
    static Scope _DynScope = Scope.Null;

    static object fold(Cons cons, System.Func<object, object, object> fn, object x)
    {
      return (cons == Cons.Nil) ?      
        x :
        fold(((Cons)cons.Tail), fn, fn(x, cons.Head));
    }

    static object map(Cons cons, System.Func<object, object> fn)
    {
      return (cons == Cons.Nil) ? Cons.Nil : new Cons{ Head = fn(cons.Head), Tail = map((Cons)(cons.Tail), fn)};
    }

    public static Cons list(params object[] param)
    {
      Cons retVal = Cons.Nil;
      for(int i = param.Length; i > 0; i--)
      {
        var x = new Cons{ Head = param[i-1], Tail = retVal };
        retVal = x;
      }
      return retVal;
    }

    public static bool GenBoolToBool(object x)
    {
      return (x == Cons.Nil);
    }

    static object print(object x)
    {
       Console.WriteLine("{0}", x);
       return x;
    }

    static void dump(Cons cons)
    {
      Console.WriteLine("({0} . {1})", cons.Head, cons.Tail);
    }

    static object evalLet(object form, Scope lexScope)
    {
      var newScope = lexScope.NewScope();
      Cons op = (Cons)form;
      Cons args = (Cons)(op.Head);
      while(args != Cons.Nil)
      {
        object arg = args.Head;
        String type = TypeResolver.GetTypeRef(arg.GetType());
        switch(type)
        {
          case "symbol":
            lexScope.Bind(arg as Symbol, "value", Cons.Nil);
            break;
          case "cons":
            Cons argSpec = op as Cons;
            Symbol sym = argSpec.Head as Symbol;
            if(sym == null)
            {
              throw new TypeError(argSpec.Head, typeof(Symbol));
            }
            Cons specTail = argSpec.Tail as Cons;
            if(specTail == null)
            {
              throw new TypeError(specTail.Tail, typeof(Cons));
            }
            object value = specTail.Head;
            lexScope.Bind(sym, "value", value);
            break;
          default:
            throw new TypeError(arg, typeof(Symbol));
        }

        Cons newArgs = args.Tail as Cons;
        if(newArgs == null)
        {
          throw new TypeError(newArgs, typeof(Cons));
        }
        args = newArgs;
      }

      Cons forms = (Cons)(op.Tail);
      Cons prognForm = new Cons();
      prognForm.Head = new Symbol("progn");
      prognForm.Tail = forms;
      return evalProgn(prognForm, newScope);
    }

    static object evalProgn(Cons formList, Scope lexScope)
    {
      if(formList == Cons.Nil)
      {
        return Cons.Nil;
      }

      for(;;)
      {
        object car = formList.Head;
        Cons cdr = formList.Tail as Cons;
        if(cdr == null)
        {
          throw new TypeError(formList.Tail, typeof(Cons));
        }

        if(cdr == Cons.Nil)
        {
          return eval(car, lexScope);
        }
        else
        {
          eval(car, lexScope);
          formList = cdr;
        }        
      }
    }

    static object evalVariable(Symbol var, Scope lexScope)
    {      
      return Cons.Nil;      
    }
    
    static object evalCompoundForm(Cons form, Scope lexScope)
    {
      Symbol op = (Symbol)(form.Head);
      switch(op.Name)
      {
        case "let":
          return evalLet(form.Tail, lexScope);
        case "progn":
          return evalProgn((Cons)form.Tail, lexScope);
        default:
          throw new ArgumentException("Unkown form!");
      }

    }

    public static object eval(Object form)
    {
      return eval(form, Scope.Null);
    }

    static object eval(Object form, Scope lexScope)
    {
      String typeRef = TypeResolver.GetTypeRef(form.GetType());

      switch(typeRef)
      {
        case "string":
        case "int32":
          return form;
        case "symbol":
          return evalVariable((Symbol)form, lexScope);
        case "cons":
          if(form == Cons.Nil)
          {
            return form;
          }
          else
          {
            return evalCompoundForm((Cons)form, lexScope);
          }
        default:
          throw new ArgumentException("Unkown type specifier!");
      }
    }
}
}

