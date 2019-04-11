#pragma once

#include "Common.h"

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

    DllExport Vec2 SwapCoords(Vec2 in);
    DllExport void SwapCoordsPtr(Vec2* in);
    DllExport void SwapCoordsRef(Vec2& in);
    DllExport void SetVecArray(Vec2 arr[], const int n, const float v);
    DllExport Vec2 GetVecArraySum(const Vec2 arr[], const int n);
    DllExport float GetDistance(const Vec2& a, const Vec2& b);
    DllExport float GetLineLength(const Line& line);
    DllExport void GetLineFromVecs(Line& line, const Vec2& start, const Vec2& end);
    DllExport float GetLineWithPtrsLength(const LineWithPtrs& line);
    DllExport void GetLineWithPtrsFromVecs(LineWithPtrs& line, const Vec2& start, const Vec2& end);
    DllExport void GetTriangleFromVecs(Triangle& triangle, const Vec2& a, const Vec2& b, const Vec2& c);
    DllExport float GetTrianglePerimeter(const Triangle& triangle);
    DllExport float GetPathLength(const Path& path);
    DllExport void GetPathFromVecs(Path& path, const Vec2 points[], const int n);

#ifdef __cplusplus
}
#endif
