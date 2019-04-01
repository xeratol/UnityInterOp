using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeLibDelegatesTester : TesterBehavior
{
    private const int NUM_TESTS = 1000;
    private bool _dummyBool = false;

    void Start()
    {
        var random = new System.Random();

        {
            _dummyBool = false;
            var voidCallback = new NativeLib.VoidCallback(ToggleDummy);
            NativeLib.ExecuteVoidCallback(voidCallback);
            Test("NativeLib.ExecuteVoidCallback()", true, _dummyBool);
            NativeLib.ExecuteVoidCallback(voidCallback);
            Test("NativeLib.ExecuteVoidCallback()", false, _dummyBool);

            LogComplete("NativeLib.ExecuteVoidCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var shortCallback = new NativeLib.ShortCallback(MultiplyShortByTwo);
                var param = (short)random.Next();
                var val = NativeLib.ExecuteShortCallback(shortCallback, param);
                Test("NativeLib.ExecuteShortCallback()", MultiplyShortByTwo(param), val);
            }

            LogComplete("NativeLib.ExecuteShortCallback()");
        }

        Debug.Log("NativeLibDelegatesTester Test Complete");
    }

    public void ToggleDummy()
    {
        _dummyBool = !_dummyBool;
    }

    public short MultiplyShortByTwo(short param)
    {
        return (short)(param * 2);
    }
}
