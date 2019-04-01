#define DllExport __declspec (dllexport)

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

    typedef void(_stdcall *VoidCallback)(void);
    typedef bool(_stdcall *BoolCallback)(bool);
    typedef char(_stdcall *CharCallback)(char);
    typedef short(_stdcall *ShortCallback)(short);
    typedef int(_stdcall *IntCallback)(int);
    typedef long(_stdcall *LongCallback)(long);
    typedef float(_stdcall *FloatCallback)(float);
    typedef double(_stdcall *DoubleCallback)(double);

    DllExport void ExecuteVoidCallback(VoidCallback callback)
    {
        callback();
    }

    DllExport bool ExecuteBoolCallback(BoolCallback callback, bool param)
    {
        return callback(param);
    }

    DllExport char ExecuteCharCallback(CharCallback callback, char param)
    {
        return callback(param);
    }

    DllExport short ExecuteShortCallback(ShortCallback callback, short param)
    {
        return callback(param);
    }

    DllExport int ExecuteIntCallback(IntCallback callback, int param)
    {
        return callback(param);
    }

    DllExport long ExecuteLongCallback(LongCallback callback, long param)
    {
        return callback(param);
    }

    DllExport float ExecuteFloatCallback(FloatCallback callback, float param)
    {
        return callback(param);
    }

    DllExport double ExecuteDoubleCallback(DoubleCallback callback, double param)
    {
        return callback(param);
    }

    DllExport int ExecuteIntCallbackByIndex(IntCallback callback[], int parameter, int index)
    {
        return callback[index](parameter);
    }

    static IntCallback storedCallback = nullptr;

    DllExport void StoreIntCallbackForLater(IntCallback callback)
    {
        storedCallback = callback;
    }

    DllExport int ExecuteStoredIntCallback(int parameter)
    {
        if (storedCallback != nullptr)
        {
            return storedCallback(parameter);
        }
        return -1;
    }

    static struct StructWithCallbacks
    {
        IntCallback eventA = nullptr;
        IntCallback eventB = nullptr;
    } storedStructWithCallbacks;

    DllExport void StoreStructWithCallbacksForLater(StructWithCallbacks* callbacks)
    {
        storedStructWithCallbacks.eventA = (callbacks == nullptr) ? nullptr : callbacks->eventA;
        storedStructWithCallbacks.eventB = (callbacks == nullptr) ? nullptr : callbacks->eventB;
    }

    DllExport int ExecuteStoredStructWithCallbacksEventA(int param)
    {
        return (storedStructWithCallbacks.eventA == nullptr) ? -1 : storedStructWithCallbacks.eventA(param);
    }

    DllExport int ExecuteStoredStructWithCallbacksEventB(int param)
    {
        return (storedStructWithCallbacks.eventB == nullptr) ? -1 : storedStructWithCallbacks.eventB(param);
    }

    // TODO Callbacks with arrays of simple data type parameters
    // TODO Callbacks with string parameters
    // TODO Callbacks with struct parameters

#ifdef __cplusplus
}
#endif
