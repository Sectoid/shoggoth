using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM.Types;

namespace Shoggoth.VM {

public class PackageAlreadyExists : VM.Exception
{
  public PackageAlreadyExists(String name)
    : base(String.Format("Package named '{0}' already exists", name))
  {}
}

public class PackageDoesNotExist : VM.Exception
{
  public PackageDoesNotExist(String name)
    : base(String.Format("Package named '{0}' does exist", name))
  {} 
}

public class Context
{    
    public static Context Current 
    { 
      get 
      { 
        return _Current; 
      } 
    }

    public DynamicScope DynamicScope { get; private set; }
    public Package CurrentPackage { get; private set; }
    
    public Symbol FindSymbol(String name, String pkgName)
    {
      Package pkg = null;
      _Packages.TryGetValue(name, out pkg);
      if(pkg == null)
      {
        throw new PackageDoesNotExist(name);
      }

      return pkg.FindSymbol(name);
    }

    public Types.Package MakePackage(String name)
    {
      Package pkg = null;
      _Packages.TryGetValue(name, out pkg);
      if(pkg != null)
      {
        throw new PackageAlreadyExists(name);
      }

      pkg = new Package(name);
      _Packages[name] = pkg;
      return pkg;
    }


    private Context()
    {      
      DynamicScope = new DynamicScope();
    }

    private static Context _Current = new Context();
    private Dictionary<String,Types.Package> _Packages = new Dictionary<String,Types.Package>();
}

}
