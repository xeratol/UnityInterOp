using System;
using UnityEngine;

public class TesterBehavior : MonoBehaviour
{
    protected void LogComplete(string function)
    {
        Debug.LogFormat("Test <color=green>{0}</color> complete", function);
    }

    protected void Test(string function, Func<bool> checker)
    {
        if (!checker())
        {
            Debug.LogError(function);
        }
    }

    protected void Test<T>(string function, T expected, T output)
    {
        if (!output.Equals(expected))
        {
            Debug.LogErrorFormat("{0}: expected {1}, output {2}", function, expected, output);
        }
    }

    protected void Test(string function, float expected, float output)
    {
        if (!Mathf.Approximately(expected, output))
        {
            Debug.LogErrorFormat("{0}: expected {1}, output {2}", function, expected, output);
        }
    }

    protected void TestWithAlternate(Action testCase)
    {
        var useAlt = NativeLib.useAlternate;

        NativeLib.useAlternate = false;
        testCase();

        NativeLib.useAlternate = true;
        testCase();

        NativeLib.useAlternate = useAlt;
    }
}
