using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;

namespace Shoggoth.Codegen {

public sealed partial class Compiler 
{
    public void CompileForm(object form, LexicalScope lexScope)
    {
      if(form == null)
      {
        throw new CompilerError("form can't be null!");
      }

      if(lexScope == null)
      {
        throw new CompilerError("lexScope can't be null!");
      }

      Type formType = form.GetType();

      if(formType.IsValueType)
      {
        // Is value type
        GenBoxedValue(form);
        return;
      }

      if(formType == typeof(String))
      {
        // Is string
        GenStringValue(form as String);
        return;
      }

      if(formType == typeof(Symbol))
      {
        // Is variable
        Symbol sym = form as Symbol;
        CompileVariable(sym, lexScope);
        return;
      }
      
      if(formType == typeof(Cons))
      {
        // Is compound
        Cons consForm = form as Cons;
        Symbol fnSym = consForm.Head as Symbol;
        
        if(IsSpecialForm(fnSym))
        {
          CompileSpecialForm(consForm, lexScope);
          return;
        }
        else
        {
          Cons paramList = consForm.Tail as Cons;
          if(paramList == null)
          {
            throw new CompilerError("paramList is not a list!");
          }
          CompileFuncall(fnSym, paramList, lexScope);
          return;
        }

      }
      
      throw new CompilerError("Unknown type of form");
    }

    
    private void CompileVariable(Symbol sym, LexicalScope lexScope)
    {
      if(lexScope.IsBound(sym, Symbol.ValueSlot))
      {
        GenLexSymValue(lexScope, sym, Symbol.ValueSlot);
      }
      else
      {
        FieldInfo valueSlotFld = typeof(Symbol).GetField("ValueSlot");
        if(valueSlotFld == null)
        {
          throw new CompilerError("Unable to find VM.Types.Symbol.FunctionSlot field");
        }

        GenDynSymValue(sym, valueSlotFld);
      }
    }

    private void CompileFuncall(Symbol fnSym, Cons paramList, LexicalScope lexScope)
    {
      if(lexScope.IsBound(fnSym, Symbol.FunctionSlot))
      {
        GenLexSymValue(lexScope, fnSym, Symbol.FunctionSlot);
      }
      else
      {
        FieldInfo functionSlotFld = typeof(Symbol).GetField("FunctionSlot");
        if(functionSlotFld == null)
        {
          throw new CompilerError("Unable to find VM.Types.Symbol.FunctionSlot field");
        }

        GenDynSymValue(fnSym, functionSlotFld);
      }

      MethodInfo invokeMethInfo = typeof(Function).GetMethod("Invoke");
      if(invokeMethInfo == null)
      {
        throw new CompilerError("Unable to find VM.Function.Invoke method");
      }

      var paramCount = ConsAux.Reduce(paramList, (x, counter) => counter + 1, 0);
      _gen.Emit(OpCodes.Newarr, paramCount);
      
      ConsAux.Reduce(paramList, (x, counter) =>
        {
          _gen.Emit(OpCodes.Dup);
          _gen.Emit(OpCodes.Ldc_I4, counter + 1);
          CompileForm(x, lexScope);
          _gen.Emit(OpCodes.Stelem_Ref);
          return counter + 1;
        }, 0);

      _gen.Emit(OpCodes.Call, invokeMethInfo);
    }

    private void GenStringValue(String str)
    {
      _gen.Emit(OpCodes.Ldstr, str);      
    }

    private void GenBoxedValue(object form)
    {
      if(form == null)
      {
        throw new CompilerError("form can't be null");
      }

      String typeStr = TypeResolver.GetTypeRef(form.GetType());
      switch(typeStr)
      {
        case "int32":
          _gen.Emit(OpCodes.Ldc_I4, (int)form);
          _gen.Emit(OpCodes.Box, typeof(int));
          break;
        case "bool":
          _gen.Emit(OpCodes.Ldc_I4, ((bool)form) ? 1 : 0);
          _gen.Emit(OpCodes.Box, typeof(int));
          break;
        default:
          break;
      }
    }

    private void GenLexSymValue(LexicalScope lexScope, Symbol varSym, String slot)
    {
      LocalBuilder lexVar = lexScope.Value(varSym, slot);
      _gen.Emit(OpCodes.Ldloc, lexVar);
    }

    private void GenDynSymValue(Symbol sym, FieldInfo slotDesignator)
    {
      PropertyInfo currentCtxProp = typeof(VM.Context).GetProperty("Current");
      if(currentCtxProp == null)
      {
        throw new CompilerError("Unable to find VM.Context.Current property");
      }

      MethodInfo curCtxGetter = currentCtxProp.GetGetMethod();
      if(curCtxGetter == null)
      {
        throw new CompilerError("Unable to find VM.Context.Current.get accessor");
      }

      PropertyInfo dynScopeProp = typeof(VM.Context).GetProperty("DynamicScope");
      if(dynScopeProp == null)
      {
        throw new CompilerError("Unable to find VM.Context.DynamicScope property");
      }

      MethodInfo dynScopeGetter = dynScopeProp.GetGetMethod();
      if(curCtxGetter == null)
      {
        throw new CompilerError("Unable to find VM.Context.DynamicScope.get accessor");
      }

      MethodInfo valueGetter = typeof(DynamicScope).GetMethod("Value");
      if(valueGetter == null)
      {
        throw new CompilerError("Unable to find VM.Scope.Value method");
      }
      
      _gen.Emit(OpCodes.Call, curCtxGetter);
      _gen.Emit(OpCodes.Call, dynScopeGetter);
      _gen.Emit(OpCodes.Ldstr, sym.Name);
      _gen.Emit(OpCodes.Ldstr, slotDesignator);
      _gen.Emit(OpCodes.Call, valueGetter);
    }
  
    private ILGenerator _gen = null;
}

}