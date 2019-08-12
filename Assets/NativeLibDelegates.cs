using System;
using System.Runtime.InteropServices;

public partial class NativeLib
{
    public delegate void VoidCallback();
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool BoolCallback([MarshalAs(UnmanagedType.I1)] bool b);
    public delegate byte ByteCallback(byte a); // char in C# is UTF-16, 2 bytes
    public delegate short ShortCallback(short a);
    public delegate int IntCallback(int a);
    public delegate long LongCallback(long a);
    public delegate float FloatCallback(float a);
    public delegate double DoubleCallback(double a);
    public delegate void StringCallback(string s, int n);
    public delegate void StructCallback(in Vec2 v);

    [StructLayout(LayoutKind.Sequential)]
    public struct StructWithCallbacks
    {
        public IntCallback eventA;
        public IntCallback eventB;
    }

    private partial class Wrapper
    {
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ExecuteVoidCallback(VoidCallback callback);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ExecuteBoolCallback(BoolCallback callback, [MarshalAs(UnmanagedType.I1)] bool b);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ExecuteCharCallback(ByteCallback callback, byte param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern short ExecuteShortCallback(ShortCallback callback, short param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExecuteIntCallback(IntCallback callback, int param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern long ExecuteLongCallback(LongCallback callback, long param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ExecuteFloatCallback(FloatCallback callback, float param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ExecuteDoubleCallback(DoubleCallback callback, double param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExecuteIntCallbackByIndex(IntPtr callbacks, int param, int index);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void ExecuteStringCallback(StringCallback callback, string s, int n);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ExecuteStructCallback(StructCallback callback, in Vec2 v);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StoreIntCallbackForLater(IntCallback callback);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExecuteStoredIntCallback(int parameter);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void StoreStructWithCallbacksForLater(in StructWithCallbacks callbacks);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExecuteStoredStructWithCallbacksEventA(int param);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExecuteStoredStructWithCallbacksEventB(int param);
    }

    public static void ExecuteCallback(VoidCallback callback)
    {
        Wrapper.ExecuteVoidCallback(callback);
    }

    public static bool ExecuteCallback(BoolCallback callback, bool param)
    {
        return Wrapper.ExecuteBoolCallback(callback, param);
    }

    public static byte ExecuteCallback(ByteCallback callback, byte param)
    {
        return Wrapper.ExecuteCharCallback(callback, param);
    }

    public static short ExecuteCallback(ShortCallback callback, short param)
    {
        return Wrapper.ExecuteShortCallback(callback, param);
    }

    public static int ExecuteCallback(IntCallback callback, int param)
    {
        return Wrapper.ExecuteIntCallback(callback, param);
    }

    public static float ExecuteCallback(FloatCallback callback, float param)
    {
        return Wrapper.ExecuteFloatCallback(callback, param);
    }

    public static double ExecuteCallback(DoubleCallback callback, double param)
    {
        return Wrapper.ExecuteDoubleCallback(callback, param);
    }

    public static int ExecuteCallback(IntCallback [] callbacks, int param, int index)
    {
        // C# has no default marshaling behavior for a delegate array
        // need to do it manually
        var callbackArray = new IntPtr();
        callbackArray = Marshal.AllocHGlobal(callbacks.Length * IntPtr.Size);
        for (var i = 0; i < callbacks.Length; ++i)
        {
            var ptr = callbackArray + i * IntPtr.Size;
            Marshal.WriteIntPtr(ptr, Marshal.GetFunctionPointerForDelegate(callbacks[i]));
        }
        var val = Wrapper.ExecuteIntCallbackByIndex(callbackArray, param, index);
        Marshal.FreeHGlobal(callbackArray);
        return val;
    }

    public static void ExecuteCallback(StringCallback callback, string param)
    {
        Wrapper.ExecuteStringCallback(callback, param, param.Length);
    }

    public static void ExecuteCallback(StructCallback callback, in Vec2 v)
    {
        Wrapper.ExecuteStructCallback(callback, v);
    }

    public static void StoreIntCallbackForLater(IntCallback callback)
    {
        Wrapper.StoreIntCallbackForLater(callback);
    }

    public static int ExecuteStoredIntCallback(int param)
    {
        return Wrapper.ExecuteStoredIntCallback(param);
    }

    public static void StoreStructWithCallbacksForLater(in StructWithCallbacks callbacks)
    {
        Wrapper.StoreStructWithCallbacksForLater(callbacks);
    }

    public static int ExecuteStoredStructWithCallbacksEventA(int param)
    {
        return Wrapper.ExecuteStoredStructWithCallbacksEventA(param);
    }

    public static int ExecuteStoredStructWithCallbacksEventB(int param)
    {
        return Wrapper.ExecuteStoredStructWithCallbacksEventB(param);
    }
}
