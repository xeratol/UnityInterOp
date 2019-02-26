using System;
using System.Runtime.InteropServices;

public class NativeLib
{
    private class Wrapper
    {
        const string dll = "NativeLib";

        [DllImport(dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        // C# bool is 4 bytes vs C bool is 1 byte
        public static extern bool GetBool();

        [DllImport(dll)]
        // C# char is 2 bytes vs C char is 1 byte
        public static extern byte GetChar();

        [DllImport(dll)]
        public static extern short GetShort();

        [DllImport(dll)]
        public static extern int GetInt();

        [DllImport(dll)]
        public static extern long GetLong();

        [DllImport(dll)]
        public static extern float GetFloat();

        [DllImport(dll)]
        public static extern double GetDouble();

        [DllImport(dll)]
        public static extern int GetSum(in int[] arr, int n);

        [DllImport(dll)]
        public static extern IntPtr GetString();
    }

    public static bool GetBool()
    {
        return Wrapper.GetBool();
    }

    public static char GetChar()
    {
        return Convert.ToChar(Wrapper.GetChar());
    }

    public static short GetShort()
    {
        return Wrapper.GetShort();
    }

    public static int GetInt()
    {
        return Wrapper.GetInt();
    }

    public static long GetLong()
    {
        return Wrapper.GetLong();
    }

    public static float GetFloat()
    {
        return Wrapper.GetFloat();
    }

    public static double GetDouble()
    {
        return Wrapper.GetDouble();
    }

    public static int GetSum(in int [] arr)
    {
        return Wrapper.GetSum(arr, arr.Length);
    }

    public static string GetString()
    {
        return Marshal.PtrToStringAnsi(Wrapper.GetString());
    }
}
