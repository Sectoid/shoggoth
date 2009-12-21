using System;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth.VM {

public class Exception : System.Exception
{
  public Exception()
    : base() {}

  public Exception(String message)
    : base(message) {}

  public Exception(String message, System.Exception innerException)
    : base(message, innerException) {}

}

public class TypeError : VM.Exception
{
  public TypeError(Object value, Type type)
    : base(String.Format("{0} is of type {1}", value, type.Name))
  {
    Value = value;
    Type = type;
  }

  public Object Value { get; private set; }
  public Type Type { get; private set; }  
}

}
