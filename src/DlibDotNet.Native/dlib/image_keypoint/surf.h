#ifndef _CPP_SURF_H_
#define _CPP_SURF_H_

#include "../export.h"
#include <dlib/image_keypoint/surf.h>
#include <dlib/pixel.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;

#pragma region template

#define get_surf_points_template(__TYPE__, error, type, ...) \
array2d<__TYPE__>& image = *(static_cast<array2d<__TYPE__>*>(img));\
std::vector<surf_point> points = dlib::get_surf_points(image, max_points, detection_threshold);\
for (int index = 0; index < points.size(); index++)\
    ret->push_back(new dlib::surf_point(points[index]));

#pragma endregion template

DLLEXPORT int get_surf_points(array2d_type type,
                              void* img,
                              long max_points,
                              double detection_threshold,
                              std::vector<surf_point*>* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     get_surf_points_template,
                     img,
                     max_points,
                     detection_threshold,
                     ret);

    return error;
}

#pragma region surf_point

DLLEXPORT double surf_point_get_angle(dlib::surf_point* sp)
{
    return sp->angle;
}

DLLEXPORT interest_point* surf_point_get_p(dlib::surf_point* sp)
{
    return &(sp->p);
}

DLLEXPORT matrix<double,64,1>* surf_point_get_des(dlib::surf_point* sp)
{
    return &(sp->des);
}

DLLEXPORT void surf_point_delete(dlib::surf_point* sp)
{
    delete sp;
}

#pragma region surf_point_des

DLLEXPORT void surf_point_des_matrix_operator_left_shift(void* matrix, std::ostringstream* stream)
{
    dlib::matrix<double,64,1>& mat = *(static_cast<dlib::matrix<double,64,1>*>(matrix));
    *stream << mat;
}

DLLEXPORT int surf_point_des_matrix_nc(void* matrix, int* ret)
{
    int err = ERR_OK;
    dlib::matrix<double,64,1>& mat = *(static_cast<dlib::matrix<double,64,1>*>(matrix));
    *ret = mat.nc();
    return err;
}

DLLEXPORT int surf_point_des_matrix_nr(void* matrix, int* ret)
{
    int err = ERR_OK;
    dlib::matrix<double,64,1>& mat = *(static_cast<dlib::matrix<double,64,1>*>(matrix));
    *ret = mat.nr();
    return err;
}

#pragma endregion surf_point_des

#pragma endregion surf_point

#endif