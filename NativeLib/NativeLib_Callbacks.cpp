#include <thread>
#include <chrono>

#include "Common.h"
#include "Structs.h"
#include "Callbacks.h"

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

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

    DllExport void ExecuteStringCallback(StringCallback callback, const char* str, int n)
    {
        callback(str, n);
    }

    DllExport void ExecuteStructCallback(StructCallback callback, const Vec2& v)
    {
        callback(v);
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

    DllExport void ExecuteCallbackInThread(VoidCallback callback)
    {
        std::thread other([&]
        {
            // copy callback locally because parameter could get
            // garbage collected by C#
            auto callbackInThread = callback;
            std::this_thread::sleep_for(std::chrono::seconds(1));
            callbackInThread();
        });

        other.detach();
    }

#ifdef __cplusplus
}
#endif
