#pragma once

#include "Common.h"
#include "Structs.h"

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
    typedef void(_stdcall *StringCallback)(const char*, int);
    typedef void(_stdcall *StructCallback)(const Vec2&);
    struct StructWithCallbacks;

    DllExport void ExecuteVoidCallback(VoidCallback callback);
    DllExport bool ExecuteBoolCallback(BoolCallback callback, bool param);
    DllExport char ExecuteCharCallback(CharCallback callback, char param);
    DllExport short ExecuteShortCallback(ShortCallback callback, short param);
    DllExport int ExecuteIntCallback(IntCallback callback, int param);
    DllExport long ExecuteLongCallback(LongCallback callback, long param);
    DllExport float ExecuteFloatCallback(FloatCallback callback, float param);
    DllExport double ExecuteDoubleCallback(DoubleCallback callback, double param);
    DllExport int ExecuteIntCallbackByIndex(IntCallback callback[], int parameter, int index);
    DllExport void ExecuteStringCallback(StringCallback callback, const char* str, int n);
    DllExport void ExecuteStructCallback(StructCallback callback, const Vec2& v);
    DllExport void StoreIntCallbackForLater(IntCallback callback);
    DllExport int ExecuteStoredIntCallback(int parameter);
    DllExport void StoreStructWithCallbacksForLater(StructWithCallbacks* callbacks);
    DllExport int ExecuteStoredStructWithCallbacksEventA(int param);
    DllExport int ExecuteStoredStructWithCallbacksEventB(int param);
    DllExport void ExecuteCallbackInThread(VoidCallback callback);

#ifdef __cplusplus
}
#endif
