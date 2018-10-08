#ifndef _CPP_SPATIAL_FILTERING_H_
#define _CPP_SPATIAL_FILTERING_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_processing/full_object_detection_abstract.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/spatial_filtering.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define gaussian_blur_template(__TYPE__, ret, in_type, in_img, out_img, sigma, max_size) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::gaussian_blur(*((array2d<uint8_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::UInt16:\
            dlib::gaussian_blur(*((array2d<uint16_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::UInt32:\
            dlib::gaussian_blur(*((array2d<uint32_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int8:\
            dlib::gaussian_blur(*((array2d<int8_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int16:\
            dlib::gaussian_blur(*((array2d<int16_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int32:\
            dlib::gaussian_blur(*((array2d<int32_t>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Float:\
            dlib::gaussian_blur(*((array2d<float>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Double:\
            dlib::gaussian_blur(*((array2d<double>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::RgbPixel:\
            dlib::gaussian_blur(*((array2d<rgb_pixel>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::HsiPixel:\
            dlib::gaussian_blur(*((array2d<hsi_pixel>*)in_img), *((array2d<__TYPE__>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define sum_filter_template(__TYPE__, ret, in_type, in_img, out_img, rect) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::sum_filter(*((array2d<uint8_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::UInt16:\
            dlib::sum_filter(*((array2d<uint16_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::UInt32:\
            dlib::sum_filter(*((array2d<uint32_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::Int8:\
            dlib::sum_filter(*((array2d<int8_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::Int16:\
            dlib::sum_filter(*((array2d<int16_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::Int32:\
            dlib::sum_filter(*((array2d<int32_t>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::Float:\
            dlib::sum_filter(*((array2d<float>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::Double:\
            dlib::sum_filter(*((array2d<double>*)in_img), *((array2d<__TYPE__>*)out_img), *rect);\
            break;\
        case array2d_type::RgbPixel:\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#pragma region gaussian_blur

DLLEXPORT int gaussian_blur(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double sigma, int max_size)
{
    int err = ERR_OK;
    switch(out_type)
    {
        case array2d_type::UInt8:
            gaussian_blur_template(uint8_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::UInt16:
            gaussian_blur_template(uint16_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::UInt32:
            gaussian_blur_template(uint32_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::Int8:
            gaussian_blur_template(int8_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::Int16:
            gaussian_blur_template(int16_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::Int32:
            gaussian_blur_template(int32_t, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::Float:
            gaussian_blur_template(float, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::Double:
            gaussian_blur_template(double, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::RgbPixel:
            gaussian_blur_template(rgb_pixel, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::HsiPixel:
            gaussian_blur_template(hsi_pixel, err, in_type, in_img, out_img, sigma, max_size);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region gaussian_blur

#pragma endregion sum_filter

DLLEXPORT int sum_filter(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, rectangle* rect)
{
    int err = ERR_OK;
    switch(out_type)
    {
        case array2d_type::UInt8:
            sum_filter_template(uint8_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::UInt16:
            sum_filter_template(uint16_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::UInt32:
            sum_filter_template(uint32_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::Int8:
            sum_filter_template(int8_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::Int16:
            sum_filter_template(int16_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::Int32:
            sum_filter_template(int32_t, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::Float:
            sum_filter_template(float, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::Double:
            sum_filter_template(double, err, in_type, in_img, out_img, rect);
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion sum_filter

#endif