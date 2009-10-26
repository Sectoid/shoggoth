using System;
using System.Collections;
using System.Collections.Generic;

namespace Shoggoth.VM {

public class MalformedList : VM.Exception
{
}

public static class ConsAux
{
    public delegate TResult Func<T, TResult>(T x);
    public delegate TResult Func<T1, T2, TResult>(T1 lhs, T2 rhs);

    public static T Reduce<T>(Types.Cons list, Func<Object, T, T> fn, T initialValue)
    {
      if(list == Types.Cons.Nil)
      {
        return initialValue;
      }
      else
      {
        var tail = list.Tail as Types.Cons;
        if(tail == null)
        {
          throw new MalformedList();
        }

        return Reduce(tail, fn, fn(list.Head, initialValue));
      }
    }

    public static List<T> MapToList<T>(Types.Cons list, Func<Object, T> fn)
    {
      return Reduce(list, (x, locals) => { locals.Add(fn(x)); return locals; } , new List<T>());
    }

    public static int Count(Types.Cons list)
    {
      return Reduce(list, (x, counter) => counter + 1, 0);
    }

}

}