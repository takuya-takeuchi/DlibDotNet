#ifndef _CPP_GEOMETRY_DPOINT_H_
#define _CPP_GEOMETRY_DPOINT_H_

#include <dlib/geometry/vector.h>

using namespace dlib;

extern "C" __declspec(dllexport) dpoint* dpoint_new()
{
    return new dpoint();
}

extern "C" __declspec(dllexport) dpoint* dpoint_new1(double x, double y)
{
    return new dpoint(x, y);
}

extern "C" __declspec(dllexport) void dpoint_delete(const dpoint* dpoint)
{
    delete dpoint;
}

extern "C" __declspec(dllexport) double dpoint_length(const dpoint* dpoint)
{
    return dpoint->length();
}

extern "C" __declspec(dllexport) double dpoint_length_squared(const dpoint* dpoint)
{
    return dpoint->length_squared();
}

extern "C" __declspec(dllexport) double dpoint_x(const dpoint* dpoint)
{
    return dpoint->x();
}

extern "C" __declspec(dllexport) double dpoint_y(const dpoint* dpoint)
{
    return dpoint->y();
}

#pragma region operator

extern "C" __declspec(dllexport) void* dpoint_operator_add(dpoint* point, dpoint* rhs)
{
    const dlib::dpoint result = (*point) + (*rhs);
    return new dlib::dpoint(result);
}

extern "C" __declspec(dllexport) void* dpoint_operator_sub(dpoint* point, dpoint* rhs)
{
    const dlib::dpoint result = (*point) - (*rhs);
    return new dlib::dpoint(result);
}

extern "C" __declspec(dllexport) void* dpoint_operator_mul(dlib::dpoint* point, double rhs)
{
    const dlib::dpoint result = (*point) * (rhs);
    return new dlib::dpoint(result);
}

extern "C" __declspec(dllexport) void* dpoint_operator_div(dlib::dpoint* point, double rhs)
{
    const dlib::dpoint result = (*point) / (rhs);
    return new dlib::dpoint(result);
}

extern "C" __declspec(dllexport) bool dpoint_operator_equal(dpoint* point, dpoint* rhs)
{
    return *point == *rhs;
}

#pragma endregion operator

#endif