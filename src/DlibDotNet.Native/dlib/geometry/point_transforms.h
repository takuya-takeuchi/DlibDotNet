#ifndef _CPP_POINT_TRANSFORMS_H_
#define _CPP_POINT_TRANSFORMS_H_

#include <dlib/geometry/point_transforms.h>
#include <dlib/image_transforms.h>
 
using namespace dlib;
using namespace std;

#pragma region point_rotator

extern "C" __declspec(dllexport) void* point_rotator_new()
{
    return new dlib::point_rotator();
}

extern "C" __declspec(dllexport) void* point_rotator_new1(double angle)
{
    return new dlib::point_rotator(angle);
}

extern "C" __declspec(dllexport) void* point_rotator_get_m(void* obj)
{
    auto matrix = ((dlib::point_rotator*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

extern "C" __declspec(dllexport) void* point_rotator_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double,2>*)vector;
    auto t = (dlib::point_rotator*)obj;
    dlib::vector<double,2> ret = (*t)(*v);
    return new dlib::vector<double,2>(ret);
}

extern "C" _declspec(dllexport) void point_rotator_delete(void* obj)
{
	delete obj;
}

#pragma endregion point_rotator

#pragma region point_transform

extern "C" __declspec(dllexport) void* point_transform_new()
{
    return new dlib::point_transform();
}

extern "C" __declspec(dllexport) void* point_transform_new1(double angle, void* translate)
{
    auto vector = (dlib::vector<double,2>*)translate;
    return new dlib::point_transform(angle, *vector);
}

extern "C" __declspec(dllexport) void* point_transform_get_b(void* obj)
{
    auto vector = ((dlib::point_transform*)obj)->get_b();
    return new dlib::vector<double>(vector);
}

extern "C" __declspec(dllexport) void* point_transform_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

extern "C" __declspec(dllexport) void* point_transform_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double,2>*)vector;
    auto t = (dlib::point_transform*)obj;
    dlib::vector<double,2> ret = (*t)(*v);
    return new dlib::vector<double,2>(ret);
}

extern "C" _declspec(dllexport) void point_transform_delete(void* obj)
{
	delete obj;
}

#pragma endregion point_transform

#pragma region point_transform_affine

extern "C" __declspec(dllexport) void* point_transform_affine_new()
{
    return new dlib::point_transform_affine();
}

extern "C" __declspec(dllexport) void* point_transform_affine_new1(void* matrix, void* vector)
{
    auto m = (dlib::matrix<double,2,2>*)matrix;
    auto b = (dlib::vector<double>*)vector;
    return new dlib::point_transform_affine(*m, *b);
}

extern "C" __declspec(dllexport) void* point_transform_affine_get_b(void* obj)
{
    auto vector = ((dlib::point_transform_affine*)obj)->get_b();
    return new dlib::vector<double>(vector);
}

extern "C" __declspec(dllexport) void* point_transform_affine_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform_affine*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

extern "C" __declspec(dllexport) void* point_transform_affine_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double>*)vector;
    auto t = (dlib::point_transform_affine*)obj;
    dlib::vector<double> ret = (*t)(*v);
    return new dlib::vector<double>(ret);
}

extern "C" _declspec(dllexport) void point_transform_affine_delete(void* obj)
{
	delete obj;
}

#pragma endregion point_transform_affine

#pragma region rectangle_transform

extern "C" __declspec(dllexport) void* rectangle_transform_new()
{
    return new dlib::rectangle_transform();
}

extern "C" __declspec(dllexport) void* rectangle_transform_new1(void* transform)
{
    auto p = (dlib::point_transform_affine*)transform;
    return new dlib::rectangle_transform(*p);
}

extern "C" __declspec(dllexport) void* rectangle_transform_get_tform(void* obj)
{
    dlib::point_transform_affine tform = ((dlib::rectangle_transform*)obj)->get_tform();
    return new dlib::point_transform_affine(tform);
}

extern "C" __declspec(dllexport) void* rectangle_transform_operator(void* obj, void* rect)
{
    auto r = (dlib::rectangle*)rect;
    auto t = (dlib::rectangle_transform*)obj;
    dlib::rectangle ret = (*t)(*r);
    return new dlib::rectangle(ret);
}

extern "C" __declspec(dllexport) void* rectangle_transform_operator_d(void* obj, void* rect)
{
    auto r = (dlib::drectangle*)rect;
    auto t = (dlib::rectangle_transform*)obj;
    dlib::drectangle ret = (*t)(*r);
    return new dlib::drectangle(ret);
}

extern "C" _declspec(dllexport) void rectangle_transform_delete(void* obj)
{
	delete obj;
}

#pragma endregion rectangle_transform

#pragma region point_transform_projective

extern "C" __declspec(dllexport) void* point_transform_projective_new()
{
    return new dlib::point_transform_projective();
}

extern "C" __declspec(dllexport) void* point_transform_projective_new1(void* matrix)
{
    auto m = (dlib::matrix<double,3,3>*)matrix;
    return new dlib::point_transform_projective(*m);
}

extern "C" __declspec(dllexport) void* point_transform_projective_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform_projective*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

extern "C" __declspec(dllexport) void* point_transform_projective_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double>*)vector;
    auto t = (dlib::point_transform_projective*)obj;
    dlib::vector<double> ret = (*t)(*v);
    return new dlib::vector<double>(ret);
}

extern "C" _declspec(dllexport) void point_transform_projective_delete(void* obj)
{
	delete obj;
}

#pragma endregion point_transform_projective

extern "C" __declspec(dllexport) dlib::point* rotate_point(dlib::point* center, dlib::point* p, const double angle)
{
    dlib::point ret = dlib::rotate_point(*center, *p, angle);
    return new dlib::point(ret);
}

extern "C" __declspec(dllexport) dlib::dpoint* rotate_dpoint(dlib::dpoint* center, dlib::dpoint* p, const double angle)
{
    dlib::dpoint ret = dlib::rotate_point(*center, *p, angle);
    return new dlib::dpoint(ret);
}

#endif