using System;
using System.Collections;
using System.Collections.Generic;
using Shoggoth.VM.Types;

namespace Shoggoth.VM {

public sealed class DynamicScope
{
    public static readonly Scope Instance = Scope.Null;
    private DynamicScope() {} // instantiation is abbandoned
}

}