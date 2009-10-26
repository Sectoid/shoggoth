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
        default:
          throw new CompilerError("Unknown special form {0}", formSym.Name);
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