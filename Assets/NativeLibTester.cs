﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class NativeLibTester : MonoBehaviour
{
    void Start()
    {
        var random = new System.Random();

        {
            Debug.Assert(NativeLib.GetBool(true) == false, "NativeLib.GetBool()");
            Debug.Assert(NativeLib.GetBool(false) == true, "NativeLib.GetBool()");
            var b = true;
            NativeLib.GetBoolPtr(ref b); // b is now false
            Debug.Assert(!b, "NativeLib.GetBoolPtr()");
            NativeLib.GetBoolPtr(ref b); // b is now true
            Debug.Assert(b,  "NativeLib.GetBoolPtr()");
            NativeLib.GetBoolRef(ref b); // b is now false
            Debug.Assert(!b, "NativeLib.GetBoolRef()");
            NativeLib.GetBoolRef(ref b); // b is now true
            Debug.Assert(b,  "NativeLib.GetBoolRef()");
        }

        {
            Debug.Assert(NativeLib.GetChar('a') == 'b', "NativeLib.GetChar()");
            Debug.Assert(NativeLib.GetChar('G') == 'H', "NativeLib.GetChar()");
            Debug.Assert(NativeLib.GetChar('8') == '9', "NativeLib.GetChar()");
            var c = 'd';
            NativeLib.GetCharPtr(ref c);
            Debug.Assert(c == 'e', "NativeLib.GetCharPtr()");
            NativeLib.GetCharPtr(ref c);
            Debug.Assert(c == 'f', "NativeLib.GetCharPtr()");
            NativeLib.GetCharRef(ref c);
            Debug.Assert(c == 'g', "NativeLib.GetCharRef()");
            NativeLib.GetCharRef(ref c);
            Debug.Assert(c == 'h', "NativeLib.GetCharRef()");
        }

        {
            for (var i = 0; i < 1000; ++i)
            {
                var val = (short)random.Next(short.MinValue + 1, short.MaxValue);
                Test("NativeLib.GetShort()", val + 1, NativeLib.GetShort(val));

                var old = val = (short)random.Next(short.MinValue + 1, short.MaxValue);
                NativeLib.GetShortPtr(ref val);
                Test("NativeLib.GetShortPtr()", old + 1, val);

                old = val = (short)random.Next(short.MinValue + 1, short.MaxValue);
                NativeLib.GetShortRef(ref val);
                Test("NativeLib.GetShortRef()", old + 1, val);
            }
        }

        {
            for (var i = 0; i < 1000; ++i)
            {
                var val = random.Next(int.MinValue, int.MaxValue - 42);
                Test("NativeLib.GetInt()", val + 42, NativeLib.GetInt(val));

                var old = val = random.Next(int.MinValue, int.MaxValue - 42);
                NativeLib.GetIntPtr(ref val);
                Test("NativeLib.GetIntPtr()", old + 42, val);

                old = val = random.Next(int.MinValue, int.MaxValue - 42);
                NativeLib.GetIntRef(ref val);
                Test("NativeLib.GetIntRef()", old + 42, val);
            }
        }

        {
            for (var i = 0; i < 1000; ++i)
            {
                var val = random.Next(int.MaxValue / 100, int.MaxValue) * 10000L;
                Test("NativeLib.GetLong()", val * 10L, NativeLib.GetLong(val));

                var old = val = random.Next(int.MaxValue / 100, int.MaxValue) * 10000L;
                NativeLib.GetLongPtr(ref val);
                Test("NativeLib.GetLongPtr()", old * 10L, val);

                old = val = random.Next(int.MaxValue / 100, int.MaxValue) * 10000L;
                NativeLib.GetLongRef(ref val);
                Test("NativeLib.GetLongRef()", old * 10L, val);
            }
        }

        {
            for (var i = 0; i < 1000; ++i)
            {
                var val = (float)random.NextDouble();
                Test("NativeLib.GetFloat()", val / 9.0f, NativeLib.GetFloat(val));

                var old = val = (float)random.NextDouble();
                NativeLib.GetFloatPtr(ref val);
                Test("NativeLib.GetFloatPtr()", old / 9.0f, val);

                old = val = (float)random.NextDouble();
                NativeLib.GetFloatRef(ref val);
                Test("NativeLib.GetFloatRef()", old / 9.0f, val);
            }
        }

        {
            for (var i = 0; i < 1000; ++i)
            {
                var val = random.NextDouble();
                Test("NativeLib.GetDouble()", val * 1e6, NativeLib.GetDouble(val));

                var old = val = (float)random.NextDouble();
                NativeLib.GetDoublePtr(ref val);
                Test("NativeLib.GetDoublePtr()", old * 1e6, val);

                old = val = (float)random.NextDouble();
                NativeLib.GetDoubleRef(ref val);
                Test("NativeLib.GetDoubleRef()", old * 1e6, val);
            }
        }

        {
            for (var i = 0; i < 100; ++i)
            {
                var length = random.Next(1, 5);
                var arr = new int[length];
                var sum = 0;
                for (var j = 0; j < length; ++j)
                {
                    var val = random.Next(-10000, 10000);
                    arr[j] = val;
                    sum += val;
                }
                Test("NativeLib.GetIntArraySum()", sum, NativeLib.GetIntArraySum(arr));
            }
        }

        Debug.Log("Test Complete");
    }

    private void Test<T>(string function, T expected, T output)
    {
        Debug.Assert(expected.Equals(output), ErrorMessage(function, expected, output));
    }

    private string ErrorMessage<T>(string function, T expected, T output)
    {
        return string.Format("{0}: expected {1}, output {2}", function, expected, output);
    }
}