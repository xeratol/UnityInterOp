#include "Common.h"

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

    DllExport bool GetBool(bool b)
    {
        return !b;
    }

    DllExport void GetBoolPtr(bool* b)
    {
        (*b) = !(*b);
    }

    DllExport void GetBoolRef(bool& b)
    {
        b = !b;
    }

    // technically, a char is 8-bit unsigned int
    DllExport char GetChar(char c)
    {
        return c + 1;
    }

    DllExport void GetCharPtr(char* c)
    {
        (*c) += 1;
    }

    DllExport void GetCharRef(char& c)
    {
        c += 1;
    }

    DllExport void GetCharArrRev(char arr[], const int n)
    {
        for (auto i = 0; i * 2 <= n - 1; ++i)
        {
            auto c = arr[i];
            arr[i] = arr[n - i - 1];
            arr[n - i - 1] = c;
        }
    }

    DllExport short GetShort(short s)
    {
        return s + 1;
    }

    DllExport void GetShortPtr(short* s)
    {
        (*s)++;
    }

    DllExport void GetShortRef(short& s)
    {
        s++;
    }

    DllExport __int32 GetInt(__int32 i)
    {
        return i + 42;
    }

    DllExport void GetIntPtr(__int32* i)
    {
        (*i) += 42;
    }

    DllExport void GetIntRef(__int32& i)
    {
        i += 42;
    }

    DllExport __int64 GetLong(__int64 i)
    {
        return i * 10LL;
    }

    DllExport void GetLongPtr(__int64* i)
    {
        (*i) *= 10LL;
    }

    DllExport void GetLongRef(__int64& i)
    {
        i *= 10LL;
    }

    DllExport float GetFloat(float f)
    {
        return f / 9.0f;
    }

    DllExport void GetFloatPtr(float* f)
    {
        (*f) /= 9.0f;
    }

    DllExport void GetFloatRef(float& f)
    {
        f /= 9.0f;
    }

    DllExport double GetDouble(double d)
    {
        return d * 1e6;
    }

    DllExport void GetDoublePtr(double* d)
    {
        (*d) *= 1e6;
    }

    DllExport void GetDoubleRef(double& d)
    {
        d *= 1e6;
    }

    // arr is a pre-allocated integer array of size of at least n
    DllExport void GetIntArray(int arr[], const int n)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i] = i + 1;
        }
    }

    // arr is a pre-allocated integer array of size of at least n
    DllExport int GetIntArraySum(const int arr[], const int n)
    {
        int sum = 0;
        for (auto i = 0; i < n; ++i)
        {
            sum += arr[i];
        }
        return sum;
    }

    DllExport const char* GetConstantString(int x)
    {
        switch (x)
        {
        case 0:
            return "007";
        case 1:
            return "911";
        case 2:
            return "1.2.3";
        default:
            return "some string";
        }
    }

    // strlen(str) <= 255
    DllExport int GetStringLength(const char* str, int maxIndex = 255)
    {
        for (auto i = 0; i <= maxIndex; ++i)
        {
            if (str[i] == 0)
            {
                return i;
            }
        }
        return -1;
    }

    // strlen(str) >= length
    DllExport void GetStringReverse(char* str, int length)
    {
        for (auto i = 0; i * 2 <= length - 1; ++i)
        {
            auto c = str[i];
            str[i] = str[length - i - 1];
            str[length - i - 1] = c;
        }
    }

    // TODO Array of strings

#ifdef __cplusplus
}
#endif
