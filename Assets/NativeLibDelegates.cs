using System.Runtime.InteropServices;

public partial class NativeLib
{
    public delegate void VoidCallback();
    public delegate byte CharCallback(char a);
    public delegate short ShortCallback(short a);
    public delegate int IntCallback(int a);
    public delegate long LongCallback(long a);
    public delegate float FloatCallback(float a);
    public delegate double DoubleCallback(double a);

    private partial class Wrapper
    {
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ExecuteVoidCallback(VoidCallback callback);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern short ExecuteShortCallback(ShortCallback callback, short param);
    }

    public static void ExecuteVoidCallback(VoidCallback callback)
    {
        Wrapper.ExecuteVoidCallback(callback);
    }

    public static short ExecuteShortCallback(ShortCallback callback, short param)
    {
        return Wrapper.ExecuteShortCallback(callback, param);
    }
}
