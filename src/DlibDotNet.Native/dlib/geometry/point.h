#ifndef _CPP_GEOMETRY_POINT_H_
#define _CPP_GEOMETRY_POINT_H_

#include <dlib/geometry/vector.h>

using namespace dlib;

extern "C" __declspec(dllexport) point* point_new()
{
    return new point();
}

extern "C" __declspec(dllexport) point* point_new1(int x, int y)
{
    return new point(x, y);
}

extern "C" __declspec(dllexport) void point_delete(const point* point)
{
    delete point;
}

extern "C" __declspec(dllexport) double point_length(const point* point)
{
    return point->length();
}

extern "C" __declspec(dllexport) double point_length_squared(const point* point)
{
    return point->length_squared();
}

extern "C" __declspec(dllexport) int point_x(const point* point)
{
    return point->x();
}

extern "C" __declspec(dllexport) int point_y(const point* point)
{
    return point->y();
}

#pragma region operator

extern "C" __declspec(dllexport) void* point_operator_add(dlib::point* point, dlib::point* rhs)
{
    const dlib::point result = (*point) + (*rhs);
    return new dlib::point(result);
}

extern "C" __declspec(dllexport) void* point_operator_sub(dlib::point* point, dlib::point* rhs)
{
    const dlib::point result = (*point) - (*rhs);
    return new dlib::point(result);
}

extern "C" __declspec(dllexport) void* point_operator_mul(dlib::point* point, int rhs)
{
    const dlib::point result = (*point) * (rhs);
    return new dlib::point(result);
}

extern "C" __declspec(dllexport) void* point_operator_div(dlib::point* point, int rhs)
{
    const dlib::point result = (*point) / (rhs);
    return new dlib::point(result);
}

extern "C" __declspec(dllexport) bool point_operator_equal(dlib::point* point, dlib::point* rhs)
{
    return *point == *rhs;
}

#pragma endregion operator

#endif