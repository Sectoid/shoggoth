using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Shoggoth.VM.Test {

[TestFixture]
public class ConsAuxTest
{    
    [Test]
    public void ReduceNilTest()
    {
      Object marker = new Object();
      Assert.That(ConsAux.Reduce(Cons.Nil, (x, rhs) => rhs, marker), Is.EqualTo(marker));
      ConsAux.Reduce(Cons.Nil, (x, rhs) => {throw new Exception("Test failed -- function called");}, marker);
    }

    [Test]
    [ExpectedException( typeof(VM.MalformedList) )]
    public void ReduceMalformedTest()
    {
      Cons malformedList = new Cons { Head = new Object(), Tail = new Object() };
      ConsAux.Reduce(malformedList, (x, rhs) => rhs, new Object());
    }

    [Test]
    public void ReduceListTest()
    {
      // (list 1 2 3)
      Cons list = new Cons { Head = 1, Tail = new Cons { Head = 2, Tail = new Cons { Head = 3, Tail = Cons.Nil } } };
      // (reduce #'+ list :initial-value 0)
      Assert.That(ConsAux.Reduce(list, (x, rhs) => (int)x + rhs, 0), Is.EqualTo(6));
    }

}


}
