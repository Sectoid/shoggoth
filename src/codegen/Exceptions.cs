using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;

namespace Shoggoth.Codegen {

public class CompilerError : System.Exception
{
  public CompilerError()
    : base(String.Format("Internal compiler error: Unkown error"))
  {}

  public CompilerError(string message)
    : base(String.Format("Internal compiler error: {0}"))
  {}

  public CompilerError(string message, System.Exception innerException)
    : base(String.Format("Internal compiler error: {0}"), innerException)
  {}

  public CompilerError(String format, params object[] args)
    : base(String.Format(format, args))
  {}
}

public class FunctionNotExists : VM.Exception
{
  public FunctionNotExists(string name)
    : base(String.Format("The function does not exists: {0}", name))
  {}
}
}