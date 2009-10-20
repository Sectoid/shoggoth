using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;

namespace Shoggoth.Codegen {
public static class Compiler 
{
    public delegate object CompiledForm();

    public static CompiledForm CompileForm(object form, Scope lexScope)
    {
      String type = TypeResolver.GetTypeRef(form.GetType());
      return delegate {return null;};
    }
}

}