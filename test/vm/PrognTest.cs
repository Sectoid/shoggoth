using System;
using System.Collections;
using System.Collections.Generic;
using CLnet.VM;
using CLnet.VM.Types;

namespace CLnet.VM.Test {
public static class PrognTest
{
    static List<KeyValuePair<object, object>> TestPatterns = Init();

    static List<KeyValuePair<object, object>> Init()
    {
      List<KeyValuePair<object, object>> retVal = new List<KeyValuePair<object, object>>(new KeyValuePair<object, object>[1]
        {
          new KeyValuePair<object, object>(Evaluator.list(new Symbol("progn"), 1, 2, 3, 4), 4),
        });
      return retVal;
    }

    static public void Test1()
    {
      String result = "ok";
      try
      {
        foreach(var pattern in TestPatterns)
        {
          var evalResult = Evaluator.eval(pattern.Key);
          if(pattern.Value != evalResult)
          {
            result = String.Format("failed, got {0} but expected {1} when evaling {2}", evalResult, pattern.Value, pattern.Key);
            break;
          }
          
        }
      }
      catch(VM.Exception e)
      {
        result = "failed, got VM's exception: " + e.Message;
      }
      catch(System.Exception e)
      {
        result = "failed, got unknown exception: " + e.Message;
      }

      Console.WriteLine("[PrognTest.Test1] result: " + result);
    }
}

}