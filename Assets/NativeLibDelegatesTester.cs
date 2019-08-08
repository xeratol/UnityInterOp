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
            NativeLib.ExecuteCallback(voidCallback);
            Test("NativeLib.ExecuteVoidCallback()", true, _dummyBool);
            NativeLib.ExecuteCallback(voidCallback);
            Test("NativeLib.ExecuteVoidCallback()", false, _dummyBool);

            LogComplete("NativeLib.ExecuteVoidCallback()");
        }

        {
            var boolCallback = new NativeLib.BoolCallback(ToggleBool);
            var val = NativeLib.ExecuteCallback(boolCallback, false);
            Test("NativeLib.ExecuteBoolCallback()", true, val);
            val = NativeLib.ExecuteCallback(boolCallback, true);
            Test("NativeLib.ExecuteBoolCallback()", false, val);

            LogComplete("NativeLib.ExecuteBoolCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var byteCallback = new NativeLib.ByteCallback(DecrementByte);
                var param = (byte)random.Next();
                var val = NativeLib.ExecuteCallback(byteCallback, param);
                Test("NativeLib.ExecuteCharCallback()", DecrementByte(param), val);
            }

            LogComplete("NativeLib.ExecuteCharCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var shortCallback = new NativeLib.ShortCallback(DecrementShort);
                var param = (short)random.Next();
                var val = NativeLib.ExecuteCallback(shortCallback, param);
                Test("NativeLib.ExecuteShortCallback()", DecrementShort(param), val);
            }

            LogComplete("NativeLib.ExecuteShortCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var intCallback = new NativeLib.IntCallback(DecrementInt);
                var param = (int)random.Next();
                var val = NativeLib.ExecuteCallback(intCallback, param);
                Test("NativeLib.ExecuteIntCallback()", DecrementInt(param), val);
            }

            LogComplete("NativeLib.ExecuteIntCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var floatCallback = new NativeLib.FloatCallback(MultiplyByTwoFloat);
                var param = (float)random.Next();
                var val = NativeLib.ExecuteCallback(floatCallback, param);
                Test("NativeLib.ExecuteFloatCallback()", MultiplyByTwoFloat(param), val);
            }

            LogComplete("NativeLib.ExecuteFloatCallback()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var doubleCallback = new NativeLib.DoubleCallback(MultiplyByTwoDouble);
                var param = (double)random.Next();
                var val = NativeLib.ExecuteCallback(doubleCallback, param);
                Test("NativeLib.ExecuteDoubleCallback()", MultiplyByTwoDouble(param), val);
            }

            LogComplete("NativeLib.ExecuteDoubleCallback()");
        }

        Debug.Log("NativeLibDelegatesTester Test Complete");
    }

    public void ToggleDummy()
    {
        _dummyBool = !_dummyBool;
    }

    public bool ToggleBool(bool b)
    {
        return !b;
    }

    public byte DecrementByte(byte param)
    {
        return (byte)(param - 1);
    }

    public short DecrementShort(short param)
    {
        return (short)(param -1);
    }

    public int DecrementInt(int param)
    {
        return param - 1;
    }

    public float MultiplyByTwoFloat(float param)
    {
        return param * 2.0f;
    }

    public double MultiplyByTwoDouble(double param)
    {
        return param * 2.0;
    }
}
