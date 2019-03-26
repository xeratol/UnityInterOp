#define DllExport __declspec (dllexport)

#include <cmath>

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

    // Struct with simple data types
    struct Vec2
    {
        float x = 0.0f;
        float y = 0.0f;
    };

    // Struct with structs
    struct Line
    {
        Vec2 start;
        Vec2 end;
    };

    // Struct with pointers to struct
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
        Vec2* edge = nullptr;
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

    DllExport void GetTriangleFromVecs(Triangle& triangle, const Vec2& a, const Vec2& b, const Vec2& c)
    {
        triangle.edge[0] = a;
        triangle.edge[1] = b;
        triangle.edge[2] = c;
    }

    DllExport float GetTrianglePerimeter(const Triangle& triangle)
    {
        return GetDistance(triangle.edge[0], triangle.edge[1]) +
            GetDistance(triangle.edge[1], triangle.edge[2]) +
            GetDistance(triangle.edge[2], triangle.edge[0]);
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

#ifdef __cplusplus
}
#endif
