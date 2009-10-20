using System;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth {
namespace VM {

public class Exception : System.Exception
{
  public Exception()
    : base() {}

  public Exception(String message)
    : base(message) {}

  public Exception(String message, System.Exception innerException)
    : base(message, innerException) {}

}

}}
