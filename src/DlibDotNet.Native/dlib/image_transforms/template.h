#ifndef _CPP_IMAGE_TRANSFORMS_TEMPLATE_H_
#define _CPP_IMAGE_TRANSFORMS_TEMPLATE_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define FUNCTION function
#define ELEMENT_IN element
#define out_type element
#undef FUNCTION
#undef ELEMENT_IN
#undef out_type

#define image_transforms_inout_array2d__template(ret, in_type, in_img, out_type, out_img) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            {\
                array2d<uint8_t>& in = *(array2d<uint8_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::UInt16:\
            {\
                array2d<uint16_t>& in = *(array2d<uint16_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::UInt32:\
            {\
                array2d<uint32_t>& in = *(array2d<uint32_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Int8:\
            {\
                array2d<int8_t>& in = *(array2d<int8_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Int16:\
            {\
                array2d<int16_t>& in = *(array2d<int16_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Int32:\
            {\
                array2d<int32_t>& in = *(array2d<int32_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Float:\
            {\
                array2d<float>& in = *(array2d<float>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Double:\
            {\
                array2d<double>& in = *(array2d<double>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::RgbPixel:\
            {\
                array2d<rgb_pixel>& in = *(array2d<rgb_pixel>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::HsiPixel:\
            {\
                array2d<hsi_pixel>& in = *(array2d<hsi_pixel>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
            {\
                array2d<rgb_alpha_pixel>& in = *(array2d<rgb_alpha_pixel>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        default:\
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

// unsigned type and sizeof(in_type) <= 2
#define image_transforms_in_U_2_array2d__template(ret, in_type, in_img) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            {\
                array2d<uint8_t>& in = *(array2d<uint8_t>*)in_img;\
                dlib::FUNCTION(in);\
            }\
            break;\
        case array2d_type::UInt16:\
            {\
                array2d<uint16_t>& in = *(array2d<uint16_t>*)in_img;\
                dlib::FUNCTION(in);\
            }\
            break;\
        case array2d_type::RgbPixel:\
            {\
                array2d<rgb_pixel>& in = *(array2d<rgb_pixel>*)in_img;\
                dlib::FUNCTION(in);\
            }\
            break;\
        case array2d_type::HsiPixel:\
            {\
                array2d<hsi_pixel>& in = *(array2d<hsi_pixel>*)in_img;\
                dlib::FUNCTION(in);\
            }\
            break;\
        case array2d_type::Int8:\
        case array2d_type::Int16:\
        case array2d_type::Int32:\
        case array2d_type::UInt32:\
        case array2d_type::Float:\
        case array2d_type::Double:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

// unsigned type and sizeof(in_type) <= 2
#define image_transforms_inout_U_2_array2d__template(ret, in_type, in_img, out_type, out_img) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            {\
                array2d<uint8_t>& in = *(array2d<uint8_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::UInt16:\
            {\
                array2d<uint16_t>& in = *(array2d<uint16_t>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::RgbPixel:\
            {\
                array2d<rgb_pixel>& in = *(array2d<rgb_pixel>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::HsiPixel:\
            {\
                array2d<hsi_pixel>& in = *(array2d<hsi_pixel>*)in_img;\
                array2d<out_type>& out = *(array2d<out_type>*)out_img;\
                dlib::FUNCTION(in, out);\
            }\
            break;\
        case array2d_type::Int8:\
        case array2d_type::Int16:\
        case array2d_type::Int32:\
        case array2d_type::UInt32:\
        case array2d_type::Float:\
        case array2d_type::Double:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#pragma endregion template

#endif