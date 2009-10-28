using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM;
using Shoggoth.VM.Types;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Shoggoth.VM.Test {

[TestFixture]
public class SymbolTest
{    
    [Test]
    public void CreationTest()
    {
      Symbol sym = new Symbol("testSymbol");      
      Assert.That(sym.Name, Is.EqualTo("testSymbol"));      
    }

    [Test]
    public void EqualtyTest()
    {
      Symbol sym1 = new Symbol("test");
      Symbol sym2 = new Symbol("test");
      
      Assert.That(sym1, Is.EqualTo(sym1)); // self-equalty test
      Assert.That(sym1, Is.Not.EqualTo(sym2)); // objects non-equalty test      
    }    
}

[TestFixture]
public class PackageTest
{
    [Test]
    public void CreationTest()
    {
      String name = "TestPackage";
      Package pack = new Package(name);
      Assert.That(pack.Name, Is.EqualTo(name));
    }

    [Test]
    public void EqualtyTest()
    {
      Package pack1 = new Package("test");
      Package pack2 = new Package("test");
      
      Assert.That(pack1, Is.EqualTo(pack1)); // self-equalty test
      Assert.That(pack1, Is.Not.EqualTo(pack2)); // objects non-equalty test      
    }

    [Test]
    [ExpectedException( typeof(ArgumentException) )]
    public void InternNullTest()
    {
      Package pack = new Package("Test");
      pack.InternSymbol(null);
    }

    [Test]
    [ExpectedException( typeof(ArgumentException) )]
    public void FindSymbolNullTest()
    {
      Package pack = new Package("Test");
      pack.FindSymbol(null);
    }

    [Test]
    [ExpectedException( typeof(SymbolDoesNotExist) )]
    public void FindSymbolNotFoundTest()
    {
      Package pack = new Package("Test");
      pack.FindSymbol("test");
      
    }

    [Test]
    public void InternTest()
    {
      String symName = "Symbol";
      Package pack = new Package("Test");

      var internedSymbol = pack.InternSymbol(symName);
      Assert.That(internedSymbol.Name, Is.EqualTo(symName));

      var foundSymbol = pack.FindSymbol(symName);
      Assert.That(foundSymbol.Name, Is.EqualTo(symName));
      Assert.That(internedSymbol, Is.EqualTo(foundSymbol));
      
    }
}

}
