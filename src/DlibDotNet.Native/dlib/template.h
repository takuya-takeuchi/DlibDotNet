#ifndef _CPP_TEMPLATE_H_
#define _CPP_TEMPLATE_H_

#include "export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include "shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix

#define matrix_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::UInt8:\
        __FUNC__(uint8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt16:\
        __FUNC__(uint16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt32:\
        __FUNC__(uint32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt64:\
        __FUNC__(uint64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int8:\
        __FUNC__(int8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int16:\
        __FUNC__(int16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int32:\
        __FUNC__(int32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int64:\
        __FUNC__(int64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Float:\
        __FUNC__(float, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbPixel:\
        __FUNC__(rgb_pixel, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::HsiPixel:\
        __FUNC__(hsi_pixel, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbAlphaPixel:\
        __FUNC__(rgb_alpha_pixel, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_nonalpha_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::UInt8:\
        __FUNC__(uint8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt16:\
        __FUNC__(uint16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt32:\
        __FUNC__(uint32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt64:\
        __FUNC__(uint64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int8:\
        __FUNC__(int8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int16:\
        __FUNC__(int16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int32:\
        __FUNC__(int32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int64:\
        __FUNC__(int64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Float:\
        __FUNC__(float, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbPixel:\
        __FUNC__(rgb_pixel, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::HsiPixel:\
        __FUNC__(hsi_pixel, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbAlphaPixel:\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_numeric_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::UInt8:\
        __FUNC__(uint8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt16:\
        __FUNC__(uint16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt32:\
        __FUNC__(uint32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt64:\
        __FUNC__(uint64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int8:\
        __FUNC__(int8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int16:\
        __FUNC__(int16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int32:\
        __FUNC__(int32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int64:\
        __FUNC__(int64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Float:\
        __FUNC__(float, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbPixel:\
    case matrix_element_type::HsiPixel:\
    case matrix_element_type::RgbAlphaPixel:\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_integer_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::UInt8:\
        __FUNC__(uint8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt16:\
        __FUNC__(uint16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt32:\
        __FUNC__(uint32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt64:\
        __FUNC__(uint64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int8:\
        __FUNC__(int8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int16:\
        __FUNC__(int16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int32:\
        __FUNC__(int32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int64:\
        __FUNC__(int64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Float:\
    case matrix_element_type::Double:\
    case matrix_element_type::RgbPixel:\
    case matrix_element_type::HsiPixel:\
    case matrix_element_type::RgbAlphaPixel:\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_decimal_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::Float:\
        __FUNC__(float, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt8:\
    case matrix_element_type::UInt16:\
    case matrix_element_type::UInt32:\
    case matrix_element_type::UInt64:\
    case matrix_element_type::Int8:\
    case matrix_element_type::Int16:\
    case matrix_element_type::Int32:\
    case matrix_element_type::Int64:\
    case matrix_element_type::RgbPixel:\
    case matrix_element_type::HsiPixel:\
    case matrix_element_type::RgbAlphaPixel:\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_double_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt8:\
    case matrix_element_type::UInt16:\
    case matrix_element_type::UInt32:\
    case matrix_element_type::UInt64:\
    case matrix_element_type::Int8:\
    case matrix_element_type::Int16:\
    case matrix_element_type::Int32:\
    case matrix_element_type::Int64:\
    case matrix_element_type::Float:\
    case matrix_element_type::RgbPixel:\
    case matrix_element_type::HsiPixel:\
    case matrix_element_type::RgbAlphaPixel:\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_numerictype_template(type, error, __FUNC__, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
switch(type)\
{\
    case ::numeric_type::UInt8:\
        __FUNC__(uint8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::UInt16:\
        __FUNC__(uint16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::UInt32:\
        __FUNC__(uint32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::UInt64:\
        __FUNC__(uint64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Int8:\
        __FUNC__(int8_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Int16:\
        __FUNC__(int16_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Int32:\
        __FUNC__(int32_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Int64:\
        __FUNC__(int64_t, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Float:\
        __FUNC__(float, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    case ::numeric_type::Double:\
        __FUNC__(double, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, __VA_ARGS__);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_nosize_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case matrix_element_type::UInt8:\
        __FUNC__(uint8_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt16:\
        __FUNC__(uint16_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt32:\
        __FUNC__(uint32_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::UInt64:\
        __FUNC__(uint64_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int8:\
        __FUNC__(int8_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int16:\
        __FUNC__(int16_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int32:\
        __FUNC__(int32_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Int64:\
        __FUNC__(int64_t, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Float:\
        __FUNC__(float, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::Double:\
        __FUNC__(double, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbPixel:\
        __FUNC__(rgb_pixel, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::HsiPixel:\
        __FUNC__(hsi_pixel, error, type, __VA_ARGS__);\
        break;\
    case matrix_element_type::RgbAlphaPixel:\
        __FUNC__(rgb_alpha_pixel, error, type, __VA_ARGS__);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define matrix_template_size_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 1, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 3) { __SUB_FUNC__(__TYPE__, error, type, 1, 3, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 2, 1, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 2) { __SUB_FUNC__(__TYPE__, error, type, 2, 2, __VA_ARGS__); }\
else if (__ROWS__ == 5 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 5, 1, __VA_ARGS__); }\
else if (__ROWS__ == 31 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 31, 1, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size2_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 1, 0, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size3_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 1, 0, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

// either colums or rows shoould be 1 or 0
#define matrix_template_size4_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 1, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 3) { __SUB_FUNC__(__TYPE__, error, type, 1, 3, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 2, 1, __VA_ARGS__); }\
else if (__ROWS__ == 5 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 5, 1, __VA_ARGS__); }\
else if (__ROWS__ == 31 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 31, 1, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size5_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 2, 1, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size_column1or0_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 0, 1, __VA_ARGS__); }\
else if (__ROWS__ == 1 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 1, 1, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 2, 1, __VA_ARGS__); }\
else if (__ROWS__ == 5 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 5, 1, __VA_ARGS__); }\
else if (__ROWS__ == 31 && __COLUMNS__ == 1) { __SUB_FUNC__(__TYPE__, error, type, 31, 1, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size2x2_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 0, 0, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 0) { __SUB_FUNC__(__TYPE__, error, type, 2, 0, __VA_ARGS__); }\
else if (__ROWS__ == 0 && __COLUMNS__ == 2) { __SUB_FUNC__(__TYPE__, error, type, 0, 2, __VA_ARGS__); }\
else if (__ROWS__ == 2 && __COLUMNS__ == 2) { __SUB_FUNC__(__TYPE__, error, type, 2, 2, __VA_ARGS__); }\
else { error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT; }

#define matrix_template_size_asm_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 1 && rightTemplateColumns == 3)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 31 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
}\
else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
}\
else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
}\
else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
}\
else\
{\
    error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
}

#define matrix_template_size_d_template(__TYPE__, error, type, __SUB_FUNC__, __ROWS__, __COLUMNS__, ...) \
if (__ROWS__ == 0 && __COLUMNS__ == 0)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
}\
else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
}\
else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
}\
else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
{\
    auto l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        auto& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs)); __SUB_FUNC__(l, r);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
        break;\
    }\
    *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
}\
else\
{\
    error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
}

#pragma endregion matrix

#pragma region array2d

#define array2d_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case array2d_type::UInt8:\
        { __FUNC__(uint8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt16:\
        { __FUNC__(uint16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt32:\
        { __FUNC__(uint32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int8:\
        { __FUNC__(int8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int16:\
        { __FUNC__(int16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int32:\
        { __FUNC__(int32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Float:\
        { __FUNC__(float, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Double:\
        { __FUNC__(double, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbPixel:\
        { __FUNC__(rgb_pixel, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::HsiPixel:\
        { __FUNC__(hsi_pixel, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbAlphaPixel:\
        { __FUNC__(rgb_alpha_pixel, error, type, __VA_ARGS__); }\
        break;\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}

#define array2d_numeric_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case array2d_type::UInt8:\
        { __FUNC__(uint8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt16:\
        { __FUNC__(uint16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt32:\
        { __FUNC__(uint32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int8:\
        { __FUNC__(int8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int16:\
        { __FUNC__(int16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int32:\
        { __FUNC__(int32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Float:\
        { __FUNC__(float, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Double:\
        { __FUNC__(double, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbPixel:\
    case array2d_type::HsiPixel:\
    case array2d_type::RgbAlphaPixel:\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}

#define array2d_nonalpha_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case array2d_type::UInt8:\
        { __FUNC__(uint8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt16:\
        { __FUNC__(uint16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::UInt32:\
        { __FUNC__(uint32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int8:\
        { __FUNC__(int8_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int16:\
        { __FUNC__(int16_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Int32:\
        { __FUNC__(int32_t, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Float:\
        { __FUNC__(float, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::Double:\
        { __FUNC__(double, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbPixel:\
        { __FUNC__(rgb_pixel, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::HsiPixel:\
        { __FUNC__(hsi_pixel, error, type, __VA_ARGS__); }\
        break;\
    case array2d_type::RgbAlphaPixel:\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}

#pragma endregion array2d

#endif