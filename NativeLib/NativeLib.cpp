#define DllExport __declspec (dllexport)

#include <cmath>

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
        (*s) += 1;
    }

    DllExport void GetShortRef(short& s)
    {
        s += 1;
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

    // arr is a pre-allocated integer array of size n
    DllExport void GetIntArray(int arr[], const int n)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i] = i + 1;
        }
    }

    // arr is a pre-allocated integer array of size n
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

    DllExport int GetStringLength(const char * str, int maxIndex = 255)
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

    DllExport void GetStringReverse(char* str, int length)
    {
        for (auto i = 0; i * 2 <= length - 1; ++i)
        {
            auto c = str[i];
            str[i] = str[length - i - 1];
            str[length - i - 1] = c;
        }
    }

    // Struct with simple data types
    struct Vec2
    {
        float x = 0.0f;
        float y = 0.0f;
    };

    // Struct with a struct
    struct Line
    {
        Vec2 start;
        Vec2 end;
    };

    // Struct with pointer to struct
    struct LineWithPtrs
    {
        Vec2* start = nullptr;
        Vec2* end = nullptr;
    };

    // Struct with fixed array of structs
    struct Triangle
    {
        Vec2 edge[3];
    };

    // Struct with dynamic array of structs
    struct Path
    {
        Vec2* edge;
        int count;
    };

    DllExport Vec2 SwapCoords(Vec2 in)
    {
        auto t = in.x;
        in.x = in.y;
        in.y = t;
        return in;
    }

    DllExport void SwapCoordsPtr(Vec2* in)
    {
        auto t = in->x;
        in->x = in->y;
        in->y = t;
    }

    DllExport void SwapCoordsRef(Vec2& in)
    {
        auto t = in.x;
        in.x = in.y;
        in.y = t;
    }

    DllExport void SetVecArray(Vec2 arr[], const int n, const float v)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i].x = v;
            arr[i].y = v;
        }
    }

    // arr is a pre-allocated Vec2[] with at least length n
    DllExport Vec2 GetVecArraySum(const Vec2 arr[], const int n)
    {
        Vec2 sum;
        for (auto i = 0; i < n; ++i)
        {
            sum.x += arr[i].x;
            sum.y += arr[i].y;
        }
        return sum;
    }

    DllExport float GetDistance(const Vec2& a, const Vec2& b)
    {
        auto x = b.x - a.x;
        auto y = b.y - a.y;
        auto xSq = x * x;
        auto ySq = y * y;
        return sqrtf(xSq + ySq);
    }

    DllExport float GetLineLength(const Line& line)
    {
        return GetDistance(line.start, line.end);
    }

    DllExport void GetLineFromVecs(Line& line, const Vec2& start, const Vec2& end)
    {
        line.start = start;
        line.end = end;
    }

    DllExport float GetLineWithPtrsLength(const LineWithPtrs& line)
    {
        return GetDistance(*(line.start), *(line.end));
    }

    // line.start and line.end should allocated prior
    DllExport void GetLineWithPtrsFromVecs(LineWithPtrs& line, const Vec2& start, const Vec2& end)
    {
        *(line.start) = start;
        *(line.end) = end;
    }

    DllExport float GetTriangleArea(const Triangle& triangle)
    {
        // Heron's Formula
        const auto a = GetDistance(triangle.edge[0], triangle.edge[1]);
        const auto b = GetDistance(triangle.edge[1], triangle.edge[2]);
        const auto c = GetDistance(triangle.edge[2], triangle.edge[0]);
        const auto s = (a + b + c) * 0.5f;
        return sqrtf(s * (s - a) * (s - b) * (s - c));
    }

    DllExport void GetTriangleFromVecs(Triangle& triangle, const Vec2& a, const Vec2& b, const Vec2& c)
    {
        triangle.edge[0] = a;
        triangle.edge[1] = b;
        triangle.edge[2] = c;
    }

    DllExport float GetPathLength(const Path& path)
    {
        auto length = 0.0f;
        for (auto i = 0; i < path.count - 1; ++i)
        {
            length += GetDistance(path.edge[i], path.edge[i + 1]);
        }
        return length;
    }

    // path should be allocated prior
    // points should be at least length n
    DllExport void GetPathFromVecs(Path& path, const Vec2 points[], const int n)
    {
        path.count = n;
        for (auto i = 0; i < n; ++i)
        {
            path.edge[i] = points[i];
        }
    }

    // TODO Array of strings
    // TODO Function Pointers

#ifdef __cplusplus
}
#endif
