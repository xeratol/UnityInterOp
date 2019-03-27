#define DllExport __declspec (dllexport)

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

    typedef void(*VoidCallback)(void);
    typedef char(*CharCallback)(char);
    typedef short(*ShortCallback)(short);
    typedef int(*IntCallback)(int);
    typedef long(*LongCallback)(long);
    typedef float(*FloatCallback)(float);
    typedef double(*DoubleCallback)(double);

    DllExport void ExecuteVoidCallback(VoidCallback callback)
    {
        callback();
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
