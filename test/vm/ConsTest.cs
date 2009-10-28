using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Shoggoth.VM.Test {

[TestFixture]
public class ConsTest
{    
    [Test]
    public void NilTest()
    {
      Assert.That(Cons.Nil, Is.TypeOf(typeof(Cons)));
    }

    [Test]
    public void DefaultTest()
    {
      var defCons = new Cons();
      Assert.That(defCons.Head, Is.EqualTo(Cons.Nil));
      Assert.That(defCons.Tail, Is.EqualTo(Cons.Nil));
    }

    [Test]
    public void ObjEqualtyTest()
    {
      var strCons = new Cons { Head = "Test",  Tail = Cons.Nil };
      Assert.That(strCons.Head, Is.EqualTo("Test"));      
    }

}

}
