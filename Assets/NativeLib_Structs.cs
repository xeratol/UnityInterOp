using System;
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
    private partial class Wrapper
    {
        // since these are structs,
        // C# treats them as Value-type

        [StructLayout(LayoutKind.Sequential)]
        struct Vec2
        {
            public float x;
            public float y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Line
        {
            public Vec2 start;
            public Vec2 end;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LineWithPtrs
        {
            [MarshalAs(UnmanagedType.LPStruct)]
            public Vec2 start;
            [MarshalAs(UnmanagedType.LPStruct)]
            public Vec2 end;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Triangle
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Vec2 [] edge;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Path
        {
            public IntPtr edge;
            int count;
        }
    }
}
