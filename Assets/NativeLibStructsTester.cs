using System;
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
                var triangle = new NativeLib.Triangle();
                triangle.edge = new NativeLib.Vec2[3];
                for (var j = 0; j < 3; ++j)
                {
                    triangle.edge[j].x = (float)random.NextDouble();
                    triangle.edge[j].y = (float)random.NextDouble();
                }

                var p = NativeLib.GetTrianglePerimeter(triangle);

                // Heron's Formula
                var a = NativeLib.GetDistance(triangle.edge[0], triangle.edge[1]);
                var b = NativeLib.GetDistance(triangle.edge[1], triangle.edge[2]);
                var c = NativeLib.GetDistance(triangle.edge[2], triangle.edge[0]);
                var p2 = a + b + c;

                Test("NativeLib.GetTrianglePerimeter()", p, p2);
            }

            LogComplete("NativeLib.GetTrianglePerimeter()");
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

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var p = new NativeLib.Path();
                p.count = random.Next(2, 100);
                p.edge = new NativeLib.Vec2[p.count];
                for (var j = 0; j < p.count; ++j)
                {
                    var v = new NativeLib.Vec2();
                    v.x = (float)random.NextDouble();
                    v.y = (float)random.NextDouble();
                    p.edge[j] = v;
                }

                var pathLength = NativeLib.GetPathLength(p);

                var pathLength2 = 0.0f;
                for (var j = 1; j < p.count; ++j)
                {
                    pathLength2 += NativeLib.GetDistance(p.edge[j - 1], p.edge[j]);
                }

                Test("NativeLib.GetPathLength()", pathLength, pathLength2);
            }

            LogComplete("NativeLib.GetPathLength()");
        }

        {
            for (var i = 0; i < NUM_TESTS; ++i)
            {
                var count = random.Next(1, 100);
                var vec = new NativeLib.Vec2[count];
                for (var j = 0; j < vec.Length; ++j)
                {
                    vec[j] = new NativeLib.Vec2();
                    vec[j].x = (float)random.NextDouble();
                    vec[j].y = (float)random.NextDouble();
                }

                var path = new NativeLib.Path();
                NativeLib.GetPathFromVecs(ref path, vec);

                for (var j = 0; j < path.count; ++j)
                {
                    Test("NativeLib.GetPathFromVecs()", path.edge[j], vec[j]);
                }
            }

            LogComplete("NativeLib.GetPathFromVecs()");
        }

        Debug.Log("NativeLibStructsTester Test Complete");
    }
}
