using System;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth {
namespace VM {
namespace Types {

public sealed class Cons
{
    public object Head { get; set; }
    public object Tail { get; set; }
    public static Cons Nil = new Cons();
}


public sealed class Symbol
{
    public Symbol(String name)
    {
      Name = name;
    }

    public String Name { get; set; }
}

}}}
