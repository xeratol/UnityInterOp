using System;
using UnityEngine;

public class TesterBehavior : MonoBehaviour
{
    protected void LogComplete(string function)
    {
        Debug.LogFormat("Test <color=green>{0}</color> complete", function);
    }

    protected void LogFailedTest<T>(string function, T expected, T output)
    {
        Debug.LogErrorFormat(
            "<color=red>{0}</color>: expected <b>{1}</b>, output <b>{2}</b>",
            function, expected, output);
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
            LogFailedTest(function, expected, output);
        }
    }

    protected void Test(string function, float expected, float output)
    {
        if (!Mathf.Approximately(expected, output))
        {
            LogFailedTest(function, expected, output);
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
