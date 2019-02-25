#define DllExport __declspec (dllexport)

#ifdef __cplusplus
// Use C-based naming convention - No Name Mangling!
extern "C"
{
#endif

    DllExport bool GetBool()
    {
        return true;
    }

    DllExport char GetChar()
    {
        return 'c';
    }

    DllExport __int16 GetShort()
    {
        return 16;
    }

    DllExport __int32 GetInt()
    {
        return 42;
    }

    DllExport __int64 GetLong()
    {
        return 9223372036854775807l; // 9223372036854775808 = 0x8000000000000000
    }

    DllExport float GetFloat()
    {
        return 0.1528535047e6f;
    }

    DllExport double GetDouble()
    {
        return 0.1528535047e6;
    }

    // arr is a pre-allocated int[] with at least length n
    DllExport void GetDynamicSizeArray(int arr[], const int n)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i] = i + 1;
        }
    }

    // arr is a pre-allocated int[] with at least length 10
    DllExport void GetFixedSizeArray(int arr[])
    {
        GetDynamicSizeArray(arr, 10);
    }

    DllExport const char* GetString()
    {
        return "1.2.3";
    }

    struct Vec2
    {
        float x = 0.0f;
        float y = 0.0f;
    };

    DllExport void GetVec2Ptr(Vec2* v)
    {
        v->x = 2.0f;
        v->y = 3.0f;
    }

    // Note: In C, there's no reference operator (&)
    // hence, we don't have a pass-by-reference
    // no GetVec2Ref(Vec2& v)

    // arr is a pre-allocated Vec2[] with at least length n
    DllExport void GetVec2Arr(Vec2 arr[], const int n)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i].x = static_cast<float>(i + 1);
            arr[i].y = i + 1.0f + n;
        }
    }

    struct Line
    {
        Vec2* points = nullptr;
        int count = 0;
    };

    // use FreeLine to deallocate
    DllExport void AllocLine(Line** const line, const int n)
    {
        if ((*line) != nullptr)
        {
            return;
        }

        (*line) = new Line();
        (*line)->points = new Vec2[n];
        for (auto i = 0; i < n; ++i)
        {
            GetVec2Arr((*line)->points, n);
        }
        (*line)->count = n;
    }

    // use AllocLine to allocate
    DllExport void FreeLine(Line** const line)
    {
        delete [] (*line)->points;
        delete (*line);
        (*line) = nullptr;
    }

#ifdef __cplusplus
}
#endif
