#ifndef _CPP_ASSIGN_IMAGE_H_
#define _CPP_ASSIGN_IMAGE_H_

#include "../export.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/image_transforms/assign_image.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ELEMENT_IN element
#undef ELEMENT_IN

#define assign_all_pixels_template(err, out_type, out_img, in_pixel) \
do { \
    err = ERR_OK;\
    switch(out_type)\
    {\
        case array2d_type::UInt8:\
            dlib::assign_all_pixels(*((array2d<uint8_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::UInt16:\
            dlib::assign_all_pixels(*((array2d<uint16_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::UInt32:\
            dlib::assign_all_pixels(*((array2d<uint32_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::Int8:\
            dlib::assign_all_pixels(*((array2d<int8_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::Int16:\
            dlib::assign_all_pixels(*((array2d<int16_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::Int32:\
            dlib::assign_all_pixels(*((array2d<int32_t>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::Float:\
            dlib::assign_all_pixels(*((array2d<float>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::Double:\
            dlib::assign_all_pixels(*((array2d<double>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::assign_all_pixels(*((array2d<rgb_pixel>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::assign_all_pixels(*((array2d<hsi_pixel>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            dlib::assign_all_pixels(*((array2d<rgb_alpha_pixel>*)out_img), *((ELEMENT_IN*)in_pixel));\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

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

DLLEXPORT int assign_all_pixels(array2d_type out_type, void* out_img, array2d_type in_type, void* in_pixel)
{
   int err = ERR_OK;

   switch(in_type)
   {
       case array2d_type::UInt8:
           #define ELEMENT_IN uint8_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::UInt16:
           #define ELEMENT_IN uint16_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::UInt32:
           #define ELEMENT_IN uint32_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::Int8:
           #define ELEMENT_IN int8_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::Int16:
           #define ELEMENT_IN int16_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::Int32:
           #define ELEMENT_IN int32_t
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::Float:
           #define ELEMENT_IN float
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::Double:
           #define ELEMENT_IN double
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::RgbPixel:
           #define ELEMENT_IN rgb_pixel
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::HsiPixel:
           #define ELEMENT_IN hsi_pixel
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       case array2d_type::RgbAlphaPixel:
           #define ELEMENT_IN rgb_alpha_pixel
           assign_all_pixels_template(err, out_type, out_img, in_pixel);
           #undef ELEMENT_IN
           break;
       default:
           err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
           break;
   }

   return err;
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