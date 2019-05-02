#ifndef _CPP_ASSIGN_IMAGE_H_
#define _CPP_ASSIGN_IMAGE_H_

#include "../export.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/image_transforms/assign_image.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT_IN element
#undef ELEMENT_IN

#define array2d_inout_out_template(__TYPE__, error, type, __SUB_FUNC__, subtype, ...) \
switch(subtype)\
{\
    case array2d_type::UInt8:\
        { __SUB_FUNC__(__TYPE__, error, type, uint8_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt16:\
        { __SUB_FUNC__(__TYPE__, error, type, uint16_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt32:\
        { __SUB_FUNC__(__TYPE__, error, type, uint32_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int8:\
        { __SUB_FUNC__(__TYPE__, error, type, int8_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int16:\
        { __SUB_FUNC__(__TYPE__, error, type, int16_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int32:\
        { __SUB_FUNC__(__TYPE__, error, type, int32_t, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Float:\
        { __SUB_FUNC__(__TYPE__, error, type, float, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Double:\
        { __SUB_FUNC__(__TYPE__, error, type, double, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbPixel:\
        { __SUB_FUNC__(__TYPE__, error, type, rgb_pixel, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::HsiPixel:\
        { __SUB_FUNC__(__TYPE__, error, type, hsi_pixel, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbAlphaPixel:\
        { __SUB_FUNC__(__TYPE__, error, type, rgb_alpha_pixel, subtype, __VA_ARGS__); }\
        break;\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}\

#define array2d_inout_in_template(type, error, __FUNC__, __SUB_FUNC__, subtype, ...) \
switch(type)\
{\
    case array2d_type::UInt8:\
        { __FUNC__(uint8_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt16:\
        { __FUNC__(uint16_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt32:\
        { __FUNC__(uint32_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int8:\
        { __FUNC__(int8_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int16:\
        { __FUNC__(int16_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Int32:\
        { __FUNC__(int32_t, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Float:\
        { __FUNC__(float, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::Double:\
        { __FUNC__(double, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbPixel:\
        { __FUNC__(rgb_pixel, error, type, __SUB_FUNC__, subtype, __VA_ARGS__); }\
        break;\
    case array2d_type::HsiPixel:\
        { __FUNC__(hsi_pixel, error, type, __SUB_FUNC__, subtype,  __VA_ARGS__); }\
        break;\
    case array2d_type::RgbAlphaPixel:\
        { __FUNC__(rgb_alpha_pixel, error, type, __SUB_FUNC__, subtype,  __VA_ARGS__); }\
        break;\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}\

#define assign_all_pixels_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
dlib::assign_all_pixels(*((dlib::array2d<__SUBTYPE__>*)out_img), *((__TYPE__*)in_pixel));\

#define assign_image_template(err, out_type, out_img, in_img) \
do { \
    err = ERR_OK;\
    switch(out_type)\
    {\
        case array2d_type::UInt8:\
            dlib::assign_image(*((array2d<uint8_t>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::UInt16:\
            dlib::assign_image(*((array2d<uint16_t>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::Int32:\
            dlib::assign_image(*((array2d<int32_t>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::Float:\
            dlib::assign_image(*((array2d<float>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::Double:\
            dlib::assign_image(*((array2d<double>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::assign_image(*((array2d<rgb_pixel>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::assign_image(*((array2d<hsi_pixel>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            dlib::assign_image(*((array2d<rgb_alpha_pixel>*)out_img), *((array2d<ELEMENT_IN>*)in_img));\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define assign_image_matrix_template(err, out_type, out_img, in_img) \
do { \
    err = ERR_OK;\
    switch(out_type)\
    {\
        case matrix_element_type::UInt8:\
            dlib::assign_image(*((matrix<uint8_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::UInt16:\
            dlib::assign_image(*((matrix<uint16_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::UInt32:\
            dlib::assign_image(*((matrix<uint32_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::Int8:\
            dlib::assign_image(*((matrix<int8_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::Int16:\
            dlib::assign_image(*((matrix<int16_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::Int32:\
            dlib::assign_image(*((matrix<int32_t>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::Float:\
            dlib::assign_image(*((matrix<float>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::Double:\
            dlib::assign_image(*((matrix<double>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::RgbPixel:\
            dlib::assign_image(*((matrix<rgb_pixel>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::HsiPixel:\
            dlib::assign_image(*((matrix<hsi_pixel>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            dlib::assign_image(*((matrix<rgb_alpha_pixel>*)out_img), *((matrix<ELEMENT_IN>*)in_img));\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int assign_all_pixels(array2d_type out_type, void* out_img, array2d_type in_type, void* in_pixel)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_inout_in_template(type,
                              error,
                              array2d_inout_out_template,
                              assign_all_pixels_template,
                              subtype,
                              in_pixel,
                              out_img);
    return error;
}

DLLEXPORT int assign_image(array2d_type out_type, void* out_img, array2d_type in_type, void* in_img)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_IN uint8_t
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::UInt16:
            #define ELEMENT_IN uint16_t
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::Int32:
            #define ELEMENT_IN int32_t
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::Float:
            #define ELEMENT_IN float
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::Double:
            #define ELEMENT_IN double
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            assign_image_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int assign_image_matrix(matrix_element_type out_type, void* out_img, matrix_element_type in_type, void* in_img)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:
            #define ELEMENT_IN double
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            assign_image_matrix_template(err, out_type, out_img, in_img);
            #undef ELEMENT_IN
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif