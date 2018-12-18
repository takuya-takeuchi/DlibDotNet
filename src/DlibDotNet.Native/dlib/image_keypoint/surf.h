#ifndef _CPP_SURF_H_
#define _CPP_SURF_H_

#include "../export.h"
#include <dlib/image_keypoint/surf.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ARRAY2D_ELEMENT element
#undef ARRAY2D_ELEMENT

#define get_surf_points_template(err, img, max_points, detection_threshold, ret)\
do {\
    array2d<ARRAY2D_ELEMENT>& image = *(static_cast<array2d<ARRAY2D_ELEMENT>*>(img));\
    std::vector<surf_point> points = dlib::get_surf_points(image, max_points, detection_threshold);\
    for (int index = 0; index < points.size(); index++)\
        ret->push_back(new dlib::surf_point(points[index]));\
} while (0)

#pragma endregion template

DLLEXPORT int get_surf_points(
    array2d_type img_type,
    void* img,
    long max_points,
    double detection_threshold,
    std::vector<surf_point*>* ret)
{
    int err = ERR_OK;
    switch(img_type)
    {
        case array2d_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Int16:
            #define ARRAY2D_ELEMENT int16_t
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Int32:
            #define ARRAY2D_ELEMENT int32_t
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Float:
            #define ARRAY2D_ELEMENT float
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Double:
            #define ARRAY2D_ELEMENT double
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ARRAY2D_ELEMENT rgb_pixel
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ARRAY2D_ELEMENT hsi_pixel
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ARRAY2D_ELEMENT rgb_alpha_pixel
            get_surf_points_template(err, img, max_points, detection_threshold, ret);
            #undef ARRAY2D_ELEMENT
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
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