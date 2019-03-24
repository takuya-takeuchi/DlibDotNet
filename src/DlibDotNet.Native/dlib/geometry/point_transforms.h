#ifndef _CPP_POINT_TRANSFORMS_H_
#define _CPP_POINT_TRANSFORMS_H_

#include "../export.h"
#include <dlib/geometry/point_transforms.h>
#include <dlib/image_transforms.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region point_rotator

DLLEXPORT void* point_rotator_new()
{
    return new dlib::point_rotator();
}

DLLEXPORT void* point_rotator_new1(double angle)
{
    return new dlib::point_rotator(angle);
}

DLLEXPORT void* point_rotator_get_m(void* obj)
{
    auto matrix = ((dlib::point_rotator*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

DLLEXPORT void* point_rotator_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double,2>*)vector;
    auto t = (dlib::point_rotator*)obj;
    dlib::vector<double,2> ret = (*t)(*v);
    return new dlib::vector<double,2>(ret);
}

DLLEXPORT void point_rotator_delete(dlib::point_rotator* obj)
{
	delete obj;
}

#pragma endregion point_rotator

#pragma region point_transform

DLLEXPORT void* point_transform_new()
{
    return new dlib::point_transform();
}

DLLEXPORT void* point_transform_new1(double angle, void* translate)
{
    auto vector = (dlib::vector<double,2>*)translate;
    return new dlib::point_transform(angle, *vector);
}

DLLEXPORT void* point_transform_get_b(void* obj)
{
    auto vector = ((dlib::point_transform*)obj)->get_b();
    return new dlib::vector<double>(vector);
}

DLLEXPORT void* point_transform_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

DLLEXPORT void* point_transform_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double,2>*)vector;
    auto t = (dlib::point_transform*)obj;
    dlib::vector<double,2> ret = (*t)(*v);
    return new dlib::vector<double,2>(ret);
}

DLLEXPORT void point_transform_delete(dlib::point_transform* obj)
{
	delete obj;
}

#pragma endregion point_transform

#pragma region point_transform_affine

DLLEXPORT void* point_transform_affine_new()
{
    return new dlib::point_transform_affine();
}

DLLEXPORT void* point_transform_affine_new1(void* matrix, void* vector)
{
    auto m = (dlib::matrix<double,2,2>*)matrix;
    auto b = (dlib::vector<double>*)vector;
    return new dlib::point_transform_affine(*m, *b);
}

DLLEXPORT void* point_transform_affine_get_b(void* obj)
{
    auto vector = ((dlib::point_transform_affine*)obj)->get_b();
    return new dlib::vector<double>(vector);
}

DLLEXPORT void* point_transform_affine_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform_affine*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

DLLEXPORT void* point_transform_affine_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double>*)vector;
    auto t = (dlib::point_transform_affine*)obj;
    dlib::vector<double> ret = (*t)(*v);
    return new dlib::vector<double>(ret);
}

DLLEXPORT void point_transform_affine_delete(dlib::point_transform_affine* obj)
{
	delete obj;
}

#pragma endregion point_transform_affine

#pragma region rectangle_transform

DLLEXPORT void* rectangle_transform_new()
{
    return new dlib::rectangle_transform();
}

DLLEXPORT void* rectangle_transform_new1(void* transform)
{
    auto p = (dlib::point_transform_affine*)transform;
    return new dlib::rectangle_transform(*p);
}

DLLEXPORT void* rectangle_transform_get_tform(void* obj)
{
    dlib::point_transform_affine tform = ((dlib::rectangle_transform*)obj)->get_tform();
    return new dlib::point_transform_affine(tform);
}

DLLEXPORT void* rectangle_transform_operator(void* obj, void* rect)
{
    auto r = (dlib::rectangle*)rect;
    auto t = (dlib::rectangle_transform*)obj;
    dlib::rectangle ret = (*t)(*r);
    return new dlib::rectangle(ret);
}

DLLEXPORT void* rectangle_transform_operator_d(void* obj, void* rect)
{
    auto r = (dlib::drectangle*)rect;
    auto t = (dlib::rectangle_transform*)obj;
    dlib::drectangle ret = (*t)(*r);
    return new dlib::drectangle(ret);
}

DLLEXPORT void rectangle_transform_delete(dlib::rectangle_transform* obj)
{
	delete obj;
}

#pragma endregion rectangle_transform

#pragma region point_transform_projective

DLLEXPORT void* point_transform_projective_new()
{
    return new dlib::point_transform_projective();
}

DLLEXPORT void* point_transform_projective_new1(void* matrix)
{
    auto m = (dlib::matrix<double,3,3>*)matrix;
    return new dlib::point_transform_projective(*m);
}

DLLEXPORT void* point_transform_projective_get_m(void* obj)
{
    auto matrix = ((dlib::point_transform_projective*)obj)->get_m();
    return new dlib::matrix<double>(matrix);
}

DLLEXPORT void* point_transform_projective_operator(void* obj, void* vector)
{
    auto v = (dlib::vector<double>*)vector;
    auto t = (dlib::point_transform_projective*)obj;
    dlib::vector<double> ret = (*t)(*v);
    return new dlib::vector<double>(ret);
}

DLLEXPORT void point_transform_projective_delete(dlib::point_transform_projective* obj)
{
	delete obj;
}

#pragma endregion point_transform_projective

DLLEXPORT dlib::point_transform_affine* find_similarity_transform_dpoint(std::vector<dlib::dpoint*>* from_points,
                                                                         std::vector<dlib::dpoint*>* to_points)
{
    std::vector<dlib::dpoint> in_from, in_to;
    new_instance_vector_to_instance(dlib::dpoint, from_points, in_from);
    new_instance_vector_to_instance(dlib::dpoint, to_points, in_to);

    auto result = dlib::find_similarity_transform(in_from, in_to);
    return new dlib::point_transform_affine(result);
}

DLLEXPORT dlib::point_transform_affine* find_similarity_transform_point(std::vector<dlib::point*>* from_points,
                                                                        std::vector<dlib::point*>* to_points)
{
    std::vector<dlib::point> in_from, in_to;
    new_instance_vector_to_instance(dlib::point, from_points, in_from);
    new_instance_vector_to_instance(dlib::point, to_points, in_to);

    auto result = dlib::find_similarity_transform(in_from, in_to);
    return new dlib::point_transform_affine(result);
}

DLLEXPORT dlib::point* rotate_point(dlib::point* center, dlib::point* p, const double angle)
{
    dlib::point ret = dlib::rotate_point(*center, *p, angle);
    return new dlib::point(ret);
}

DLLEXPORT dlib::dpoint* rotate_dpoint(dlib::dpoint* center, dlib::dpoint* p, const double angle)
{
    dlib::dpoint ret = dlib::rotate_point(*center, *p, angle);
    return new dlib::dpoint(ret);
}

DLLEXPORT void* rotation_matrix(const double angle)
{
    auto ret = dlib::rotation_matrix(angle);
    return new dlib::matrix<double, 2, 2>(ret);
}

#endif