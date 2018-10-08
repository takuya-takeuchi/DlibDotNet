#ifndef _CPP_GEOMETRY_DPOINT_H_
#define _CPP_GEOMETRY_DPOINT_H_

#include "../export.h"
#include <dlib/geometry/vector.h>
#include "common.h"

using namespace dlib;

DLLEXPORT dpoint* dpoint_new()
{
    return new dpoint();
}

DLLEXPORT dpoint* dpoint_new1(double x, double y)
{
    return new dpoint(x, y);
}

DLLEXPORT void dpoint_delete(const dpoint* dpoint)
{
    delete dpoint;
}

DLLEXPORT double dpoint_length(const dpoint* dpoint)
{
    return dpoint->length();
}

DLLEXPORT double dpoint_length_squared(const dpoint* dpoint)
{
    return dpoint->length_squared();
}

DLLEXPORT double dpoint_x(const dpoint* dpoint)
{
    return dpoint->x();
}

DLLEXPORT double dpoint_y(const dpoint* dpoint)
{
    return dpoint->y();
}

#pragma region operator

DLLEXPORT void* dpoint_operator_add(dpoint* point, dpoint* rhs)
{
    const dlib::dpoint result = (*point) + (*rhs);
    return new dlib::dpoint(result);
}

DLLEXPORT void* dpoint_operator_sub(dpoint* point, dpoint* rhs)
{
    const dlib::dpoint result = (*point) - (*rhs);
    return new dlib::dpoint(result);
}

MAKE_LEFT_FUNC(dlib::dpoint, double, *, dpoint, mul, dpoint, double)
MAKE_RIGHT_FUNC(int, dlib::dpoint, *, dpoint, mul, int32, dpoint)
MAKE_RIGHT_FUNC(double, dlib::dpoint, *, dpoint, mul, double, dpoint)

DLLEXPORT void* dpoint_operator_div(dlib::dpoint* point, double rhs)
{
    const dlib::dpoint result = (*point) / (rhs);
    return new dlib::dpoint(result);
}

DLLEXPORT bool dpoint_operator_equal(dpoint* point, dpoint* rhs)
{
    return *point == *rhs;
}

#pragma endregion operator

#endif