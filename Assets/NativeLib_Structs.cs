using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/*
 * NativeLib_Structs.cs (this file) shows the marshalling between C# and C++ for
 * structs with simple data type members,
 * structs with struct members,
 * structs with pointers to struct members,
 * structs with fixed array of struct members,
 * structs with dynamic array of struct members,
 * passed as const references or references.
 * 
 * This class is purely in C# and can be used outside of Unity.
 */
public partial class NativeLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Line
    {
        public Vec2 start;
        public Vec2 end;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LineWithPtrs
    {
        [MarshalAs(UnmanagedType.LPStruct)]
        public Vec2 start;
        [MarshalAs(UnmanagedType.LPStruct)]
        public Vec2 end;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Vec2[] edge;
    }

    public struct Path
    {
        public Vec2[] edge;
        public int count;
    }

    private partial class Wrapper
    {
        // since these are structs,
        // C# treats them as Value-type

        [StructLayout(LayoutKind.Sequential)]
        public struct Path
        {
            public IntPtr edge; // Vec2[]
            public int count;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vec2 SwapCoords(Vec2 i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwapCoordsPtr(ref Vec2 i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwapCoordsRef(ref Vec2 i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVecArray(Vec2[] arr, int n, float v);

        // alternate version
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVecArray(ref Vec2 arr, int n, float v);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vec2 GetVecArraySum(Vec2[] arr, int n);

        // alternate version
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vec2 GetVecArraySum(in Vec2 arr, int n);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetDistance(in Vec2 a, in Vec2 b);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLineLength(in Line line);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLineFromVecs(ref Line line, in Vec2 start, in Vec2 end);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLineWithPtrsLength(in LineWithPtrs line);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLineWithPtrsFromVecs(ref LineWithPtrs line, in Vec2 start, in Vec2 end);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetTriangleArea(in Triangle triangle);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTriangleFromVecs(ref Triangle triangle, in Vec2 a, in Vec2 b, in Vec2 c);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetPathLength(in Path p);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetPathFromVecs(ref Path p, Vec2[] arr, int n);
    }

    public static Vec2 SwapCoords(Vec2 i)
    {
        return Wrapper.SwapCoords(i);
    }

    public static void SwapCoordsPtr(ref Vec2 i)
    {
        Wrapper.SwapCoordsPtr(ref i);
    }

    public static void SwapCoordsRef(ref Vec2 i)
    {
        Wrapper.SwapCoordsRef(ref i);
    }

    public static void SetVecArray(Vec2[] arr, int n, float v)
    {
        if (!useAlternate)
        {
            Wrapper.SetVecArray(arr, n, v);
        }
        else
        {
            Wrapper.SetVecArray(ref arr[0], n, v);
        }
    }

    public static Vec2 GetVecArraySum(Vec2[] arr, int n)
    {
        if (!useAlternate)
        {
            return Wrapper.GetVecArraySum(arr, n);
        }
        else
        {
            return Wrapper.GetVecArraySum(in arr[0], n);
        }
    }

    public static float GetDistance(Vec2 a, Vec2 b)
    {
        return Wrapper.GetDistance(a, b);
    }

    public static float GetLineLength(Line line)
    {
        return Wrapper.GetLineLength(in line);
    }

    public static void GetLineFromVecs(ref Line line, Vec2 start, Vec2 end)
    {
        Wrapper.GetLineFromVecs(ref line, in start, in end);
    }

    public static float GetLineWithPtrsLength(LineWithPtrs line)
    {
        return Wrapper.GetLineWithPtrsLength(in line);
    }

    public static void GetLineWithPtrsFromVecs(ref LineWithPtrs line, Vec2 start, Vec2 end)
    {
        Wrapper.GetLineWithPtrsFromVecs(ref line, in start, in end);
    }

    public static float GetTriangleArea(Triangle triangle)
    {
        return Wrapper.GetTriangleArea(in triangle);
    }

    public static void GetTriangleFromVecs(ref Triangle triangle, Vec2 a, Vec2 b, Vec2 c)
    {
        Wrapper.GetTriangleFromVecs(ref triangle, in a, in b, in c);
    }

    public static float GetPathLength(in Path p)
    {
        var path = new Wrapper.Path();
        path.count = p.count;
        path.edge = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vec2)) * p.count); // new/malloc

        // copy from managed to unmanaged
        for (var i = 0; i < p.count; ++i)
        {
            var edgePtr = new IntPtr(path.edge.ToInt64() + i * Marshal.SizeOf(typeof(Vec2)));
            Marshal.StructureToPtr(p.edge[i], edgePtr, false);
        }

        var pathLength = Wrapper.GetPathLength(in path);

        Marshal.FreeHGlobal(path.edge); // delete/free

        return pathLength;
    }

    public static void GetPathFromVecs(ref Path p, Vec2 [] arr)
    {
        var path = new Wrapper.Path();
        path.count = p.count;
        path.edge = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vec2)) * p.count); // new/malloc

        Wrapper.GetPathFromVecs(ref path, arr, arr.Length);

        if (p.edge == null || p.edge.Length < arr.Length)
        {
            p.edge = new Vec2[arr.Length];
        }
        p.count = path.count;

        // copy from unmanaged to managed
        for (var i = 0; i < p.count; ++i)
        {
            var edgePtr = new IntPtr(path.edge.ToInt64() + i * Marshal.SizeOf(typeof(Vec2)));
            Marshal.PtrToStructure(edgePtr, p.edge[i]);
        }

        Marshal.FreeHGlobal(path.edge); // delete/free
    }
}
