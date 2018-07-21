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

#define ELEMENT_OUT element
#undef ELEMENT_OUT

#define gaussian_blur_template(ret, in_type, in_img, out_img, sigma, max_size) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::gaussian_blur(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::UInt16:\
            dlib::gaussian_blur(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::UInt32:\
            dlib::gaussian_blur(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int8:\
            dlib::gaussian_blur(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int16:\
            dlib::gaussian_blur(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Int32:\
            dlib::gaussian_blur(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Float:\
            dlib::gaussian_blur(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::Double:\
            dlib::gaussian_blur(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::RgbPixel:\
            dlib::gaussian_blur(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::HsiPixel:\
            dlib::gaussian_blur(*((array2d<hsi_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), sigma, max_size);\
            break;\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define sum_filter_template(ret, in_type, in_img, out_img, rect) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::sum_filter(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), *rect);\
            break;\
        case array2d_type::UInt16:\
            dlib::sum_filter(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), *rect);\
            break;\
        case array2d_type::Float:\
            dlib::sum_filter(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), *rect);\
            break;\
        case array2d_type::Double:\
            dlib::sum_filter(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), *rect);\
            break;\
        case array2d_type::RgbPixel:\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
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
            #define ELEMENT_OUT uint8_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:
            #define ELEMENT_OUT double
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_OUT hsi_pixel
            gaussian_blur_template(err, in_type, in_img, out_img, sigma, max_size);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
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
            #define ELEMENT_OUT uint8_t
            sum_filter_template(err, in_type, in_img, out_img, rect);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            sum_filter_template(err, in_type, in_img, out_img, rect);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            sum_filter_template(err, in_type, in_img, out_img, rect);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:
            #define ELEMENT_OUT double
            sum_filter_template(err, in_type, in_img, out_img, rect);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion sum_filter

#endif