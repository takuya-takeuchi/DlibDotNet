#ifndef _CPP_GEOMETRY_POINT_H_
#define _CPP_GEOMETRY_POINT_H_

#include "../export.h"
#include <dlib/geometry/vector.h>
#include "common.h"

using namespace dlib;

DLLEXPORT point* point_new()
{
    return new point();
}

DLLEXPORT point* point_new1(int x, int y)
{
    return new point(x, y);
}

DLLEXPORT void point_delete(const point* point)
{
    delete point;
}

DLLEXPORT double point_length(const point* point)
{
    return point->length();
}

DLLEXPORT double point_length_squared(const point* point)
{
    return point->length_squared();
}

DLLEXPORT int point_x(const point* point)
{
    return point->x();
}

DLLEXPORT int point_y(const point* point)
{
    return point->y();
}

#pragma region operator

DLLEXPORT void* point_operator_add(dlib::point* point, dlib::point* rhs)
{
    const dlib::point result = (*point) + (*rhs);
    return new dlib::point(result);
}

DLLEXPORT void* point_operator_sub(dlib::point* point, dlib::point* rhs)
{
    const dlib::point result = (*point) - (*rhs);
    return new dlib::point(result);
}

MAKE_LEFT_FUNC(dlib::point, int, *, point, mul, point, int32)

MAKE_RIGHT_FUNC(int, dlib::point, *, point, mul, int32, point)
MAKE_RIGHT_FUNC(double, dlib::point, *, point, mul, double, point)

DLLEXPORT void* point_operator_div(dlib::point* point, int rhs)
{
    const dlib::point result = (*point) / (rhs);
    return new dlib::point(result);
}

DLLEXPORT bool point_operator_equal(dlib::point* point, dlib::point* rhs)
{
    return *point == *rhs;
}

DLLEXPORT void point_operator_left_shift(dlib::point* point, std::ostringstream* stream)
{
    dlib::point& p = *point;
    *stream << p;
}

#pragma endregion operator

#endif