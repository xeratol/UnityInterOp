﻿using System;
using UnityEngine;

public class NativeLibStructsTester : TesterBehavior
{
    private const int NUM_TESTS = 1000;

    void Start()
    {
        var random = new System.Random();

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var a = new NativeLib.Vec2();
                a.x = (float)random.NextDouble();
                a.y = (float)random.NextDouble();
                var b = NativeLib.SwapCoords(a);
                Debug.Assert(a.x == b.y, "NativeLib.SwapCoords()");
                Debug.Assert(a.y == b.x, "NativeLib.SwapCoords()");

                NativeLib.SwapCoordsPtr(ref b);
                Debug.Assert(a.x == b.x, "NativeLib.SwapCoordsPtr()");
                Debug.Assert(a.y == b.y, "NativeLib.SwapCoordsPtr()");

                NativeLib.SwapCoordsRef(ref b);
                Debug.Assert(a.x == b.y, "NativeLib.SwapCoords()");
                Debug.Assert(a.y == b.x, "NativeLib.SwapCoords()");
            }

            LogComplete("NativeLib.SwapCoords()");
        }

        {
            Action testCase = () =>
            {
                for (var i = 0; i < NUM_TESTS; ++i)
                {
                    var vecArr = new NativeLib.Vec2[random.Next(2, 10)];
                    var val = (float)random.NextDouble();
                    NativeLib.SetVecArray(vecArr, val);
                    foreach (var v in vecArr)
                    {
                        Debug.Assert(v.x == val, "NativeLib.SetVecArray()");
                        Debug.Assert(v.y == val, "NativeLib.SetVecArray()");
                    }
                }
            };
            TestWithAlternate(testCase);

            LogComplete("NativeLib.SetVecArray()");
        }

        {
            Action testCase = () =>
            {
                for (var i = 0; i < NUM_TESTS; ++i)
                {
                    var vecArr = new NativeLib.Vec2[random.Next(2, 10)];
                    var sum = new NativeLib.Vec2();
                    for (var j = 0; j < vecArr.Length; ++j)
                    {
                        vecArr[j].x = (float)random.NextDouble();
                        vecArr[j].y = (float)random.NextDouble();

                        sum.x += vecArr[j].x;
                        sum.y += vecArr[j].y;
                    }
                    var arrSum = NativeLib.GetVecArraySum(vecArr);
                    Test("NativeLib.GetVecArraySum()", sum, arrSum);
                }
            };
            TestWithAlternate(testCase);

            LogComplete("NativeLib.GetVecArraySum()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var a = new NativeLib.Vec2();
                a.x = (float)random.NextDouble();
                a.y = (float)random.NextDouble();

                var b = new NativeLib.Vec2();
                b.x = (float)random.NextDouble();
                b.y = (float)random.NextDouble();

                var dist = NativeLib.GetDistance(a, b);

                var xDeltaSq = (a.x - b.x) * (a.x - b.x);
                var yDeltaSq = (a.y - b.y) * (a.y - b.y);
                var expDist = Mathf.Sqrt(xDeltaSq + yDeltaSq);

                Test("NativeLib.GetDistance()", expDist, dist);
            }

            LogComplete("NativeLib.GetDistance()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var line = new NativeLib.Line();
                line.start.x = (float)random.NextDouble();
                line.start.y = (float)random.NextDouble();
                line.end.x = (float)random.NextDouble();
                line.end.y = (float)random.NextDouble();
                var lineLength = NativeLib.GetLineLength(line);

                var xDeltaSq = (line.start.x - line.end.x) * (line.start.x - line.end.x);
                var yDeltaSq = (line.start.y - line.end.y) * (line.start.y - line.end.y);
                var expLength = Mathf.Sqrt(xDeltaSq + yDeltaSq);

                Test("NativeLib.GetLineLength()", expLength, lineLength);
            }

            LogComplete("NativeLib.GetLineLength()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var a = new NativeLib.Vec2();
                a.x = (float)random.NextDouble();
                a.y = (float)random.NextDouble();
                var b = new NativeLib.Vec2();
                b.x = (float)random.NextDouble();
                b.y = (float)random.NextDouble();

                var line = new NativeLib.Line();
                NativeLib.GetLineFromVecs(ref line, a, b);

                Test("NativeLib.GetLineFromVecs()", line.start, a);
                Test("NativeLib.GetLineFromVecs()", line.end, b);
            }

            LogComplete("NativeLib.GetLineFromVecs()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var line = new NativeLib.Line();
                line.start.x = (float)random.NextDouble();
                line.start.y = (float)random.NextDouble();
                line.end.x = (float)random.NextDouble();
                line.end.y = (float)random.NextDouble();
                var lineLength = NativeLib.GetLineWithPtrsLength(line);

                var xDeltaSq = (line.start.x - line.end.x) * (line.start.x - line.end.x);
                var yDeltaSq = (line.start.y - line.end.y) * (line.start.y - line.end.y);
                var expLength = Mathf.Sqrt(xDeltaSq + yDeltaSq);

                Test("NativeLib.GetLineWithPtrsLength()", expLength, lineLength);
            }

            LogComplete("NativeLib.GetLineWithPtrsLength()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var a = new NativeLib.Vec2();
                a.x = (float)random.NextDouble();
                a.y = (float)random.NextDouble();
                var b = new NativeLib.Vec2();
                b.x = (float)random.NextDouble();
                b.y = (float)random.NextDouble();

                var line = new NativeLib.Line();
                NativeLib.GetLineWithPtrsFromVecs(ref line, a, b);

                Test("NativeLib.GetLineWithPtrsFromVecs()", line.start, a);
                Test("NativeLib.GetLineWithPtrsFromVecs()", line.end, b);
            }

            LogComplete("NativeLib.GetLineWithPtrsFromVecs()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var a = new NativeLib.Vec2();
                a.x = (float)random.NextDouble();
                a.y = (float)random.NextDouble();
                var b = new NativeLib.Vec2();
                b.x = (float)random.NextDouble();
                b.y = (float)random.NextDouble();
                var c = new NativeLib.Vec2();
                c.x = (float)random.NextDouble();
                c.y = (float)random.NextDouble();

                var triangle = new NativeLib.Triangle();
                NativeLib.GetTriangleFromVecs(ref triangle, a, b, c);

                Test("NativeLib.GetTriangleFromVecs()", triangle.edge[0], a);
                Test("NativeLib.GetTriangleFromVecs()", triangle.edge[1], b);
                Test("NativeLib.GetTriangleFromVecs()", triangle.edge[2], c);
            }

            LogComplete("NativeLib.GetTriangleFromVecs()");
        }

        Debug.Log("Test Complete");
    }
}
