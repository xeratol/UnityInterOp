using System.Collections;
using UnityEngine;

public class NativeLibDelegatesTester : TesterBehavior
{
    private const int NUM_TESTS = 1000;
    private bool _dummyBool = false;
    private string _dummyString = "before";
    private NativeLib.Vec2 _dummyVec2;
    private bool _dummySynchronizingBool = false;
    private static readonly object _synchronizationObject = new object();

    IEnumerator Start()
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
                var param = random.Next();
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

        {
            var callbacks = new NativeLib.IntCallback [] { DecrementInt, IncrementInt };
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var param = random.Next();
                var val = NativeLib.ExecuteCallback(callbacks, param, 0);
                Test("NativeLib.ExecuteIntCallbackByIndex()", callbacks[0](param), val);
            }

            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var param = random.Next();
                var val = NativeLib.ExecuteCallback(callbacks, param, 1);
                Test("NativeLib.ExecuteIntCallbackByIndex()", callbacks[1](param), val);
            }

            LogComplete("NativeLib.ExecuteIntCallbackByIndex()");
        }

        {
            var callback = new NativeLib.StringCallback(SetDummyString);

            NativeLib.ExecuteCallback(callback, "after");
            Test("NativeLib.ExecuteStringCallback()", "after", _dummyString);

            NativeLib.ExecuteCallback(callback, "aser3f@#$!3fsdrt");
            Test("NativeLib.ExecuteStringCallback()", "aser3f@#$!3fsdrt", _dummyString);

            NativeLib.ExecuteCallback(callback, "");
            Test("NativeLib.ExecuteStringCallback()", "", _dummyString);

            LogComplete("NativeLib.ExecuteStringCallback()");
        }

        {
            var callback = new NativeLib.StructCallback(SetDummyVec2);
            var newVec2 = new NativeLib.Vec2();

            for (var i = 0; i < NUM_TESTS; ++i)
            {
                newVec2.x = random.Next();
                newVec2.y = random.Next();
                NativeLib.ExecuteCallback(callback, newVec2);
                Test("NativeLib.ExecuteStructCallback()", newVec2, _dummyVec2);
            }

            LogComplete("NativeLib.ExecuteStructCallback()");
        }

        {
            var callback1 = new NativeLib.IntCallback(DecrementInt);
            var callback2 = new NativeLib.IntCallback(IncrementInt);

            NativeLib.StoreIntCallbackForLater(callback1);
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var param = random.Next();
                var val = NativeLib.ExecuteStoredIntCallback(param);
                Test("NativeLib.ExecuteStoredIntCallback()", callback1(param), val);
            }

            NativeLib.StoreIntCallbackForLater(callback2);
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var param = random.Next();
                var val = NativeLib.ExecuteStoredIntCallback(param);
                Test("NativeLib.ExecuteStoredIntCallback()", callback2(param), val);
            }

            NativeLib.StoreIntCallbackForLater(null);
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var val = NativeLib.ExecuteStoredIntCallback(random.Next());
                Test("NativeLib.ExecuteStoredIntCallback()", -1, val);
            }

            LogComplete("NativeLib.StoreIntCallbackForLater(),\n" +
                "NativeLib.ExecuteStoredIntCallback()");
        }

        {
            var structWithCallbacks = new NativeLib.StructWithCallbacks();
            structWithCallbacks.eventA = IncrementInt;
            structWithCallbacks.eventB = DecrementInt;

            NativeLib.StoreStructWithCallbacksForLater(structWithCallbacks);
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var param = random.Next();
                var val = NativeLib.ExecuteStoredStructWithCallbacksEventA(param);
                Test("NativeLib.ExecuteStoredStructWithCallbacksEventA()",
                    structWithCallbacks.eventA(param), val);

                val = NativeLib.ExecuteStoredStructWithCallbacksEventB(param);
                Test("NativeLib.ExecuteStoredStructWithCallbacksEventB()",
                    structWithCallbacks.eventB(param), val);
            }

            structWithCallbacks.eventA = null;
            structWithCallbacks.eventB = null;
            NativeLib.StoreStructWithCallbacksForLater(structWithCallbacks);
            {
                var param = random.Next();
                var val = NativeLib.ExecuteStoredStructWithCallbacksEventA(param);
                Test("NativeLib.ExecuteStoredStructWithCallbacksEventA()",
                    -1, val);

                val = NativeLib.ExecuteStoredStructWithCallbacksEventB(param);
                Test("NativeLib.ExecuteStoredStructWithCallbacksEventB()",
                    -1, val);
            }

            LogComplete("NativeLib.StoreStructWithCallbacksForLater(),\n" +
                "NativeLib.ExecuteStoredStructWithCallbacksEventA(),\n" +
                "NativeLib.ExecuteStoredStructWithCallbacksEventB()");
        }

        {
            _dummySynchronizingBool = false;
            var callback = new NativeLib.VoidCallback(ToggleSynchronizingBool);
            NativeLib.ExecuteCallbackInThread(callback);

            var timeStart = Time.time;
            var timeOutSeconds = 5.0f;
            while (Time.time < timeStart + timeOutSeconds) // blocking execution
            {
                var val = false;
                lock(_synchronizationObject)
                {
                    val = _dummySynchronizingBool;
                }
                if (val)
                {
                    break;
                }
                yield return null;
            }

            Test("NativeLib.ExecuteCallbackInThread()", () => { return Time.time < timeStart + timeOutSeconds; });

            LogComplete("NativeLib.ExecuteCallbackInThread()");
        }

        Debug.Log("<b>NativeLibDelegatesTester Test Complete</b>");
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

    public int IncrementInt(int param)
    {
        return param + 1;
    }

    public float MultiplyByTwoFloat(float param)
    {
        return param * 2.0f;
    }

    public double MultiplyByTwoDouble(double param)
    {
        return param * 2.0;
    }

    public void SetDummyString(string s, int n)
    {
        _dummyString = s;
    }

    public void SetDummyVec2(in NativeLib.Vec2 v)
    {
        _dummyVec2 = v;
    }

    public void ToggleSynchronizingBool()
    {
        lock (_synchronizationObject)
        {
            _dummySynchronizingBool = !_dummySynchronizingBool;
        }
    }
}
