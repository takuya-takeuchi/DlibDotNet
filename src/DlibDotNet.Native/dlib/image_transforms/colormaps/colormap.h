#ifndef _CPP_COLORMAPS_COLORMAPS_H_
#define _CPP_COLORMAPS_COLORMAPS_H_

#include "../../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT_IN element
#define OP op
#define FUNCTION function
#undef FUNCTION
#undef OP
#undef ELEMENT_IN

#define colormaps_apply_array2d_template(type, img, r, c, ret, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                OP<array2d<uint8_t>>& op = *static_cast<OP<array2d<uint8_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                OP<array2d<uint16_t>>& op = *static_cast<OP<array2d<uint16_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                OP<array2d<uint32_t>>& op = *static_cast<OP<array2d<uint32_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                OP<array2d<int8_t>>& op = *static_cast<OP<array2d<int8_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                OP<array2d<int16_t>>& op = *static_cast<OP<array2d<int16_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                OP<array2d<int32_t>>& op = *static_cast<OP<array2d<int32_t>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::Float:\
			{\
                OP<array2d<float>>& op = *static_cast<OP<array2d<float>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::Double:\
			{\
                OP<array2d<double>>& op = *static_cast<OP<array2d<double>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::RgbPixel:\
			{\
                OP<array2d<rgb_pixel>>& op = *static_cast<OP<array2d<rgb_pixel>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::HsiPixel:\
			{\
                OP<array2d<hsi_pixel>>& op = *static_cast<OP<array2d<hsi_pixel>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
			{\
                OP<array2d<rgb_alpha_pixel>>& op = *static_cast<OP<array2d<rgb_alpha_pixel>>*>(img);\
                *ret = op.apply(r, c);\
            }\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_max_val_array2d_template(type, img, ret, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                OP<array2d<uint8_t>>& op = *static_cast<OP<array2d<uint8_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                OP<array2d<uint16_t>>& op = *static_cast<OP<array2d<uint16_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                OP<array2d<uint32_t>>& op = *static_cast<OP<array2d<uint32_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                OP<array2d<int8_t>>& op = *static_cast<OP<array2d<int8_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                OP<array2d<int16_t>>& op = *static_cast<OP<array2d<int16_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                OP<array2d<int32_t>>& op = *static_cast<OP<array2d<int32_t>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::Float:\
			{\
                OP<array2d<float>>& op = *static_cast<OP<array2d<float>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::Double:\
			{\
                OP<array2d<double>>& op = *static_cast<OP<array2d<double>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::RgbPixel:\
			{\
                OP<array2d<rgb_pixel>>& op = *static_cast<OP<array2d<rgb_pixel>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::HsiPixel:\
			{\
                OP<array2d<hsi_pixel>>& op = *static_cast<OP<array2d<hsi_pixel>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
			{\
                OP<array2d<rgb_alpha_pixel>>& op = *static_cast<OP<array2d<rgb_alpha_pixel>>*>(img);\
                *ret = op.max_val;\
            }\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_min_val_array2d_template(type, img, ret, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                OP<array2d<uint8_t>>& op = *static_cast<OP<array2d<uint8_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                OP<array2d<uint16_t>>& op = *static_cast<OP<array2d<uint16_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                OP<array2d<uint32_t>>& op = *static_cast<OP<array2d<uint32_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                OP<array2d<int8_t>>& op = *static_cast<OP<array2d<int8_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                OP<array2d<int16_t>>& op = *static_cast<OP<array2d<int16_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                OP<array2d<int32_t>>& op = *static_cast<OP<array2d<int32_t>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::Float:\
			{\
                OP<array2d<float>>& op = *static_cast<OP<array2d<float>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::Double:\
			{\
                OP<array2d<double>>& op = *static_cast<OP<array2d<double>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::RgbPixel:\
			{\
                OP<array2d<rgb_pixel>>& op = *static_cast<OP<array2d<rgb_pixel>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::HsiPixel:\
			{\
                OP<array2d<hsi_pixel>>& op = *static_cast<OP<array2d<hsi_pixel>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
			{\
                OP<array2d<rgb_alpha_pixel>>& op = *static_cast<OP<array2d<rgb_alpha_pixel>>*>(img);\
                *ret = op.min_val;\
            }\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_nc_array2d_template(type, img, ret, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                OP<array2d<uint8_t>>& op = *static_cast<OP<array2d<uint8_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                OP<array2d<uint16_t>>& op = *static_cast<OP<array2d<uint16_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                OP<array2d<uint32_t>>& op = *static_cast<OP<array2d<uint32_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                OP<array2d<int8_t>>& op = *static_cast<OP<array2d<int8_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                OP<array2d<int16_t>>& op = *static_cast<OP<array2d<int16_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                OP<array2d<int32_t>>& op = *static_cast<OP<array2d<int32_t>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::Float:\
			{\
                OP<array2d<float>>& op = *static_cast<OP<array2d<float>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::Double:\
			{\
                OP<array2d<double>>& op = *static_cast<OP<array2d<double>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::RgbPixel:\
			{\
                OP<array2d<rgb_pixel>>& op = *static_cast<OP<array2d<rgb_pixel>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::HsiPixel:\
			{\
                OP<array2d<hsi_pixel>>& op = *static_cast<OP<array2d<hsi_pixel>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
			{\
                OP<array2d<rgb_alpha_pixel>>& op = *static_cast<OP<array2d<rgb_alpha_pixel>>*>(img);\
                *ret = op.nc();\
            }\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_nc_matrix_template(img, templateRows, templateColumns, ret) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 0, 0>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 0, 0>>*>(img);\
        *ret = op.nc();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 0, 1>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 0, 1>>*>(img);\
        *ret = op.nc();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 31, 1>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 31, 1>>*>(img);\
        *ret = op.nc();\
    }\
} while (0)

#define colormaps_nr_array2d_template(type, img, ret, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                OP<array2d<uint8_t>>& op = *static_cast<OP<array2d<uint8_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                OP<array2d<uint16_t>>& op = *static_cast<OP<array2d<uint16_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                OP<array2d<uint32_t>>& op = *static_cast<OP<array2d<uint32_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                OP<array2d<int8_t>>& op = *static_cast<OP<array2d<int8_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                OP<array2d<int16_t>>& op = *static_cast<OP<array2d<int16_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                OP<array2d<int32_t>>& op = *static_cast<OP<array2d<int32_t>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::Float:\
			{\
                OP<array2d<float>>& op = *static_cast<OP<array2d<float>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::Double:\
			{\
                OP<array2d<double>>& op = *static_cast<OP<array2d<double>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::RgbPixel:\
			{\
                OP<array2d<rgb_pixel>>& op = *static_cast<OP<array2d<rgb_pixel>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::HsiPixel:\
			{\
                OP<array2d<hsi_pixel>>& op = *static_cast<OP<array2d<hsi_pixel>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        case array2d_type::RgbAlphaPixel:\
			{\
                OP<array2d<rgb_alpha_pixel>>& op = *static_cast<OP<array2d<rgb_alpha_pixel>>*>(img);\
                *ret = op.nr();\
            }\
            break;\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_nr_matrix_template(img, templateRows, templateColumns, ret) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 0, 0>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 0, 0>>*>(img);\
        *ret = op.nr();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 0, 1>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 0, 1>>*>(img);\
        *ret = op.nr();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        OP<dlib::matrix<ELEMENT_IN, 31, 1>>& op = *static_cast<OP<dlib::matrix<ELEMENT_IN, 31, 1>>*>(img);\
        *ret = op.nr();\
    }\
} while (0)

#define colormaps_function_array2d_template(type, img, matrix, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                array2d<uint8_t>& array = *static_cast<dlib::array2d<uint8_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint8_t>>>(ret);\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                array2d<uint16_t>& array = *static_cast<dlib::array2d<uint16_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint16_t>>>(ret);\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                array2d<uint32_t>& array = *static_cast<dlib::array2d<uint32_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint32_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                array2d<int8_t>& array = *static_cast<dlib::array2d<int8_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int8_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                array2d<int16_t>& array = *static_cast<dlib::array2d<int16_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int16_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                array2d<int32_t>& array = *static_cast<dlib::array2d<int32_t>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int32_t>>>(ret);\
            }\
            break;\
        case array2d_type::Float:\
			{\
                array2d<float>& array = *static_cast<dlib::array2d<float>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<float>>>(ret);\
            }\
            break;\
        case array2d_type::Double:\
			{\
                array2d<double>& array = *static_cast<dlib::array2d<double>*>(img);\
                auto ret = dlib::FUNCTION(array);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<double>>>(ret);\
            }\
            break;\
        case array2d_type::RgbPixel:\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_function2_array2d_template(type, img, matrix, max_val, min_val, err) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			{\
                array2d<uint8_t>& array = *static_cast<dlib::array2d<uint8_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint8_t>>>(ret);\
            }\
            break;\
        case array2d_type::UInt16:\
			{\
                array2d<uint16_t>& array = *static_cast<dlib::array2d<uint16_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint16_t>>>(ret);\
            }\
            break;\
        case array2d_type::UInt32:\
			{\
                array2d<uint32_t>& array = *static_cast<dlib::array2d<uint32_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<uint32_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int8:\
			{\
                array2d<int8_t>& array = *static_cast<dlib::array2d<int8_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int8_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int16:\
			{\
                array2d<int16_t>& array = *static_cast<dlib::array2d<int16_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int16_t>>>(ret);\
            }\
            break;\
        case array2d_type::Int32:\
			{\
                array2d<int32_t>& array = *static_cast<dlib::array2d<int32_t>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<int32_t>>>(ret);\
            }\
            break;\
        case array2d_type::Float:\
			{\
                array2d<float>& array = *static_cast<dlib::array2d<float>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<float>>>(ret);\
            }\
            break;\
        case array2d_type::Double:\
			{\
                array2d<double>& array = *static_cast<dlib::array2d<double>*>(img);\
                auto ret = dlib::FUNCTION(array, max_val, min_val);\
                *matrix = new dlib::matrix_op<OP<dlib::array2d<double>>>(ret);\
            }\
            break;\
        case array2d_type::RgbPixel:\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define colormaps_function_matrix_template(img, templateRows, templateColumns, matrix) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 0>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 0>*>(img);\
        auto ret = dlib::FUNCTION(m);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 0, 0>>>(ret);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 1>*>(img);\
        auto ret = dlib::FUNCTION(m);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 0, 1>>>(ret);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 31, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 31, 1>*>(img);\
        auto ret = dlib::FUNCTION(m);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 31, 1>>>(ret);\
    }\
} while (0)

#define colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 0>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 0>*>(img);\
        auto ret = dlib::FUNCTION(m, max_val, min_val);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 0, 0>>>(ret);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 1>*>(img);\
        auto ret = dlib::FUNCTION(m, max_val, min_val);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 0, 1>>>(ret);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 31, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 31, 1>*>(img);\
        auto ret = dlib::FUNCTION(m, max_val, min_val);\
        *matrix = new dlib::matrix_op<OP<dlib::matrix<ELEMENT_IN, 31, 1>>>(ret);\
    }\
} while (0)

#pragma endregion template

#endif