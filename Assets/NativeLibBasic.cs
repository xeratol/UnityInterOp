using System;
using System.Runtime.InteropServices;
using System.Text;

/*
 * NativeLibBasic.cs (this file) shows the marshalling between C# and C++ for simple data 
 * types (bool, char, short, int, long, float, double) passed by value, as a pointer, and
 * by reference.
 * 
 * This class is purely in C# and can be used outside of Unity.
 */
public partial class NativeLib
{
    public static bool useAlternate = false;

    private partial class Wrapper
    {
        const string dll = "NativeLib";

        // C# bool is 4 bytes
        // C bool is 1 byte
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool GetBool([MarshalAs(UnmanagedType.I1)] bool b);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBoolPtr([MarshalAs(UnmanagedType.I1)] ref bool b);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBoolRef([MarshalAs(UnmanagedType.I1)] ref bool b);

        // C# char is 2 bytes
        // C char is 1 byte
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetChar(byte c);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetCharPtr(ref byte c);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetCharRef(ref byte c);

        // character array, not necessarily a C-string
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetCharArrRev(byte[] arr, int n);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern short GetShort(short s);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetShortPtr(ref short s);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetShortRef(ref short s);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetInt(int i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetIntPtr(ref int i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetIntRef(ref int i);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetLong(long l);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLongPtr(ref long l);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLongRef(ref long l);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetFloat(float f);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetFloatPtr(ref float f);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetFloatRef(ref float f);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double GetDouble(double d);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDoublePtr(ref double d);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetDoubleRef(ref double d);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetIntArray(int[] arr, int n);

        // this is an alternate declaration for the same method above
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetIntArray(ref int arr, int n);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIntArraySum(int[] arr, int n);

        // this is an alternate declaration for the same method above
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIntArraySum(in int arr, int n);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetConstantString(int selector);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetStringLength(string s, int maxIndex = 255);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void GetStringReverse(StringBuilder str, int length);
    }

    public static bool GetBool(bool b)
    {
        return Wrapper.GetBool(b);
    }

    public static void GetBoolPtr(ref bool b)
    {
        Wrapper.GetBoolPtr(ref b);
    }

    public static void GetBoolRef(ref bool b)
    {
        Wrapper.GetBoolRef(ref b);
    }

    public static char GetChar(char c)
    {
        return Convert.ToChar(Wrapper.GetChar(Convert.ToByte(c)));
    }

    public static void GetCharPtr(ref char c)
    {
        byte cb = Convert.ToByte(c);
        Wrapper.GetCharPtr(ref cb);
        c = Convert.ToChar(cb);
    }

    public static void GetCharRef(ref char c)
    {
        byte cb = Convert.ToByte(c);
        Wrapper.GetCharPtr(ref cb);
        c = Convert.ToChar(cb);
    }

    public static void GetCharArrRev(char[] arr)
    {
        var bArr = new byte[arr.Length];
        for (var i = 0; i < arr.Length; ++i)
        {
            bArr[i] = Convert.ToByte(arr[i]);
        }
        Wrapper.GetCharArrRev(bArr, bArr.Length);
        for (var i = 0; i < arr.Length; ++i)
        {
            arr[i] = Convert.ToChar(bArr[i]);
        }
    }

    public static short GetShort(short s)
    {
        return Wrapper.GetShort(s);
    }

    public static void GetShortPtr(ref short s)
    {
        Wrapper.GetShortPtr(ref s);
    }

    public static void GetShortRef(ref short s)
    {
        Wrapper.GetShortPtr(ref s);
    }

    public static int GetInt(int i)
    {
        return Wrapper.GetInt(i);
    }

    public static void GetIntPtr(ref int i)
    {
        Wrapper.GetIntPtr(ref i);
    }

    public static void GetIntRef(ref int i)
    {
        Wrapper.GetIntPtr(ref i);
    }

    public static long GetLong(long l)
    {
        return Wrapper.GetLong(l);
    }

    public static void GetLongPtr(ref long l)
    {
        Wrapper.GetLongPtr(ref l);
    }

    public static void GetLongRef(ref long l)
    {
        Wrapper.GetLongRef(ref l);
    }

    public static float GetFloat(float f)
    {
        return Wrapper.GetFloat(f);
    }

    public static void GetFloatPtr(ref float f)
    {
        Wrapper.GetFloatPtr(ref f);
    }

    public static void GetFloatRef(ref float f)
    {
        Wrapper.GetFloatRef(ref f);
    }

    public static double GetDouble(double d)
    {
        return Wrapper.GetDouble(d);
    }

    public static void GetDoublePtr(ref double d)
    {
        Wrapper.GetDoublePtr(ref d);
    }

    public static void GetDoubleRef(ref double d)
    {
        Wrapper.GetDoubleRef(ref d);
    }

    public static void GetIntArray(int[] arr)
    {
        if (!useAlternate)
        {
            Wrapper.GetIntArray(arr, arr.Length);
        }
        else
        {
            Wrapper.GetIntArray(ref arr[0], arr.Length);
        }
    }

    public static int GetIntArraySum(int [] arr)
    {
        if (!useAlternate)
        {
            return Wrapper.GetIntArraySum(arr, arr.Length);
        }
        else
        {
            return Wrapper.GetIntArraySum(in arr[0], arr.Length);
        }
    }

    public static string GetConstantString(int selector)
    {
        return Marshal.PtrToStringAnsi(Wrapper.GetConstantString(selector));
    }

    public static int GetStringLength(string s)
    {
        return Wrapper.GetStringLength(s);
    }

    public static void GetStringReverse(ref string s)
    {
        var sb = new StringBuilder(s);
        Wrapper.GetStringReverse(sb, sb.Length);
        s = sb.ToString();
    }
}
