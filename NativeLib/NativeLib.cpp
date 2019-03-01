#define DllExport __declspec (dllexport)

#include <cmath>

#ifdef __cplusplus
// Use C-based naming convention - No Name Mangling!
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

    DllExport __int16 GetShort(__int16 s)
    {
        return s + 1;
    }

    DllExport void GetShortPtr(__int16* s)
    {
        (*s) += 1;
    }

    DllExport void GetShortRef(__int16& s)
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
        return i * 10;
    }

    DllExport void GetLongPtr(__int64* i)
    {
        (*i) *= 10;
    }

    DllExport void GetLongRef(__int64& i)
    {
        i *= 10;
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
            return "000";
        case 1:
            return "911";
        case 2:
            return "1.2.3";
        default:
            return "some string";
        }
    }

    DllExport void GetStringReverse(char* const str, int length)
    {
        for (auto i = 0; i * 2 <= length - 1; ++i)
        {
            auto c = str[i];
            str[i] = str[length - i - 1];
            str[length - i - 1] = c;
        }
    }

    struct Vec2
    {
        float x = 0.0f;
        float y = 0.0f;
    };

    DllExport Vec2 GetVec(Vec2 in)
    {
        auto t = in.x;
        in.x = in.y;
        in.y = t;
        return in;
    }

    DllExport void GetVec2Ptr(Vec2* in)
    {
        auto t = in->x;
        in->x = in->y;
        in->y = t;
    }

    DllExport void GetVec2Ref(Vec2& in)
    {
        auto t = in.x;
        in.x = in.y;
        in.y = t;
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

    DllExport void SetVecArray(Vec2 arr[], const int n, const float v)
    {
        for (auto i = 0; i < n; ++i)
        {
            arr[i].x = v;
            arr[i].y = v;
        }
    }

    DllExport float GetDistance(const Vec2& a, const Vec2& b)
    {
        auto x = b.x - a.x;
        auto y = b.y - a.y;
        auto xSq = x * x;
        auto ySq = y * y;
        return sqrtf(xSq + ySq);
    }

    struct LineSegment
    {
        Vec2 start;
        Vec2 end;
    };

    DllExport float GetLineSegmentLength(const LineSegment& line)
    {
        return GetDistance(line.start, line.end);
    }

    struct LineSegments
    {
        Vec2* points = nullptr;
        int numPoints = 0;
    };

    // use FreeLine to deallocate
    DllExport void AllocLine(LineSegments** const line, const int n)
    {
        if ((*line) != nullptr)
        {
            return;
        }

        (*line) = new LineSegments();
        (*line)->points = new Vec2[n];
        (*line)->numPoints = n;
    }

    DllExport float GetLineSegmentsLength(const LineSegments& line)
    {
        float sum = 0;
        for (auto i = 0; i < line.numPoints - 1; ++i)
        {
            sum += GetDistance(line.points[i], line.points[i + 1]);
        }
        return sum;
    }

    // use AllocLine to allocate
    DllExport void FreeLine(LineSegments** const line)
    {
        delete [] (*line)->points;
        delete (*line);
        (*line) = nullptr;
    }

    // TODO Function Pointers

#ifdef __cplusplus
}
#endif
