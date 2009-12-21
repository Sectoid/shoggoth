using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;

using Shoggoth.VM;
using Shoggoth.VM.Types;

namespace Shoggoth.Codegen {

public partial class Compiler 
{
    private bool IsSpecialForm(Symbol sym)
    {     
      switch(sym.Name)
      {
        case "progn":
          return true;
        case "let":
          return true;
        default:
          return false;
      }
    }

    private void CompileSpecialForm(Cons form, LexicalScope lexScope)
    {
      Symbol formSym = form.Head as Symbol;

      switch(formSym.Name)
      {
        case "let":
          CompileLet(form.Tail as Cons, lexScope);
          return;
        case "progn":
          CompileProgn(form.Tail as Cons, lexScope);
          return;
        default:
          throw new CompilerError("Unknown special form {0}", formSym.Name);
      }
    }

    private void CompileProgn(Cons formList, LexicalScope lexScope)
    {
      if(formList.Head != Cons.Nil)
      {
        while (formList.Tail != Cons.Nil)
        {
          CompileForm(formList.Head, lexScope);
          _gen.Emit(OpCodes.Pop);
          Assert.TypeIs(formList.Tail, typeof(Cons));
          formList = formList.Tail as Cons;
        }
        CompileForm(formList.Head, lexScope);
      }
      else
      {
        var fldInfo = typeof(Cons).GetField("Nil", BindingFlags.Public | BindingFlags.Static);
        _gen.Emit(OpCodes.Ldsfld, fldInfo);
      }      
    }
    
    private void CompileLet(object form, LexicalScope lexScope)
    {
      throw new NotImplementedException();
      // Cons consForm = form as Cons;
      // if(consForm == null)
      // {
      //   throw new CompilerError("{0} is not let-form!", form);
      // }

      // Cons formArgs = consForm.Tail as Cons;
      // if(formArgs == null)
      // {
      //   throw new CompilerError("{0} is not let-form!", form);
      // }

      // Cons bindingList = formArgs.Head as Cons;
      // if(bindingList == null)
      // {
      //   throw new CompilerError("{0} is not let-form!", form);
      // }

      // while(bindingList != Cons.Nil)
      // {
      //   object arg = bindingList.Head;
      //   String type = TypeResolver.GetTypeRef(arg.GetType()); 
      //   switch(type)
      //   {
      //     case "symbol":
      //       lexScope.Bind(arg as Symbol, "value", Cons.Nil);
      //       break;

      //     case "cons":
      //       Cons argSpec = arg as Cons;
      //       Symbol sym = argSpec.Head as Symbol;
      //       if(sym == null)
      //       {
      //         throw new TypeError(argSpec.Head, typeof(Symbol));
      //       }
      //       Cons specTail = argSpec.Tail as Cons;
      //       if(specTail == null)
      //       {
      //         throw new TypeError(specTail.Tail, typeof(Cons));
      //       }
      //       object value = specTail.Head;
      //       lexScope.Bind(sym, "value", value);
      //       break;
      //     default:
      //       throw new TypeError(arg, typeof(Symbol));
      //   }

      //   Cons newArgs = bindingList.Tail as Cons;
      //   if(newArgs == null)
      //   {
      //     throw new TypeError(newArgs, typeof(Cons));
      //   }
      //   bindingList = newArgs;
      // }
    }
}
}