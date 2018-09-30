#ifndef _CPP_MATRIX_H_
#define _CPP_MATRIX_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/geometry.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>
#include "../shared.h"

#include "matrix_common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_new4_template(__TYPE__, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new dlib::matrix<__TYPE__>();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new dlib::matrix<__TYPE__, 0, 1>();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        return new dlib::matrix<__TYPE__, 1, 3>();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        return new dlib::matrix<__TYPE__, 31, 1>();\
    }\
    return nullptr;\
} while (0)

#define matrix_new2_template(__TYPE__, num_rows, num_cols, src) \
do { \
    auto m = new matrix<__TYPE__>(num_rows, num_cols);\
    auto &d = *m;\
    __TYPE__* s = static_cast<__TYPE__*>(src);\
    for (int32_t r = 0; r < num_rows; r++)\
    for (int32_t c = 0, step = r * num_cols; c < num_cols; c++)\
        d(r, c) = s[step + c];\
    return m;\
} while (0)

#define matrix_operator_array_template(__TYPE__, matrix, array) \
do { \
    dlib::matrix<__TYPE__>& mat = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
    __TYPE__* src = static_cast<__TYPE__*>(array);\
    const long row = mat.nr();\
    const long column = mat.nc();\
    for (long r = 0; r < row; ++r)\
        for (long c = 0; c < column; ++c)\
            mat(r, c) = src[r * column + c];\
} while (0)

#define matrix_operator_add_template(__TYPE__, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret)\
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0> l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l += r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l += r;\
        }\
        else if (rightTemplateRows == 1 && rightTemplateColumns == 3)\
        {\
            dlib::matrix<__TYPE__, 1, 3>& r = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(rhs));\
            l += r;\
        }\
        else if (rightTemplateRows == 31 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 31, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(rhs));\
            l += r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l += r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l += r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l += r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l += r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l += r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_negative_template(__TYPE__, matrix, templateRows, templateColumns, ret, err)\
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& m = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(matrix));\
        auto tmp = -m;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(tmp);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& m = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        auto tmp = -m;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(tmp);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& m = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        auto tmp = -m;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(tmp);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& m = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        auto tmp = -m;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(tmp);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_subtract_template(__TYPE__, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret)\
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0> l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l -= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l -= r;\
        }\
        else if (rightTemplateRows == 1 && rightTemplateColumns == 3)\
        {\
            dlib::matrix<__TYPE__, 1, 3>& r = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(rhs));\
            l -= r;\
        }\
        else if (rightTemplateRows == 31 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 31, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(rhs));\
            l -= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l -= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l -= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l -= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l -= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l -= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_subtract_right_template(__TYPE__, lhs, templateRows, templateColumns, __RIGHT_TYPE__ ,rhs, ret)\
do {\
    __RIGHT_TYPE__& r = *static_cast<__RIGHT_TYPE__*>(rhs);\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 0> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 1> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& l = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 2> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 2, 2>(m);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        dlib::matrix<__TYPE__, 1, 3> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(m);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        dlib::matrix<__TYPE__, 31, 1> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_subtract_right_vector_2_template(__TYPE__, lhs, templateRows, templateColumns, __RIGHT_TYPE__ ,rhs, ret)\
do {\
    dlib::vector<__RIGHT_TYPE__,2>& r = *static_cast<dlib::vector<__RIGHT_TYPE__,2>*>(rhs);\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 0> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 2, 1>& l = *(static_cast<dlib::matrix<__TYPE__, 2, 1>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 1> m = l - r;\
        *ret = new dlib::matrix<__TYPE__, 2, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_multiply_template(__TYPE__, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret)\
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0> l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l *= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l *= r;\
        }\
        else if (rightTemplateRows == 1 && rightTemplateColumns == 3)\
        {\
            dlib::matrix<__TYPE__, 1, 3>& r = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(rhs));\
            l *= r;\
        }\
        else if (rightTemplateRows == 31 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 31, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(rhs));\
            l *= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l *= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l *= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l *= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l *= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l *= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_multiply_left_template(__LEFT_TYPE__, lhs, __TYPE__, rhs, templateRows, templateColumns, ret)\
do {\
    __LEFT_TYPE__& l = *static_cast<__LEFT_TYPE__*>(lhs);\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
        dlib::matrix<__TYPE__, 0, 0> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
        dlib::matrix<__TYPE__, 0, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& r = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(rhs));\
        dlib::matrix<__TYPE__, 2, 2> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 2, 2>(m);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& r = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(rhs));\
        dlib::matrix<__TYPE__, 1, 3> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(m);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(rhs));\
        dlib::matrix<__TYPE__, 31, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_multiply_left_numeric_template(__NUMERIC_TYPE__, lhs, type ,rhs, templateRows, templateColumns, ret)\
do {\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, uint8_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::UInt16:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, uint16_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::UInt32:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, uint32_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::UInt64:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, uint64_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int8:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, int8_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int16:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, int16_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int32:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, int32_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int64:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, int64_t, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Float:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, float, rhs, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Double:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, lhs, double, rhs, templateRows, templateColumns, ret);\
            break;\
    }\
} while (0)

#define matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, __RIGHT_TYPE__ , rhs, ret)\
do {\
    __RIGHT_TYPE__& r = *static_cast<__RIGHT_TYPE__*>(rhs);\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 0> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& l = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 2> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 2, 2>(m);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        dlib::matrix<__TYPE__, 1, 3> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(m);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        dlib::matrix<__TYPE__, 31, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_multiply_right_vector_2_template(__TYPE__, lhs, templateRows, templateColumns, __RIGHT_TYPE__ , rhs, ret)\
do {\
    dlib::vector<__RIGHT_TYPE__,2>& r = *static_cast<dlib::vector<__RIGHT_TYPE__,2>*>(rhs);\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        dlib::matrix<__TYPE__, 0, 0> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 2, 0>& l = *(static_cast<dlib::matrix<__TYPE__, 2, 0>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 2, 1>(m);\
    }\
    else if (templateRows == 0 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 0, 2>& l = *(static_cast<dlib::matrix<__TYPE__, 0, 2>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 2, 1>(m);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& l = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(lhs));\
        dlib::matrix<__TYPE__, 2, 1> m = l * r;\
        *ret = new dlib::matrix<__TYPE__, 2, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_multiply_right_numeric_template(__TYPE__, lhs, templateRows, templateColumns, numeric_type ,rhs, ret)\
do {\
    switch(numeric_type)\
    {\
        case numeric_type::UInt8:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, uint8_t, rhs, ret);\
            break;\
        case numeric_type::UInt16:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, uint16_t, rhs, ret);\
            break;\
        case numeric_type::UInt32:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, uint32_t, rhs, ret);\
            break;\
        case numeric_type::UInt64:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, uint64_t, rhs, ret);\
            break;\
        case numeric_type::Int8:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, int8_t, rhs, ret);\
            break;\
        case numeric_type::Int16:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, int16_t, rhs, ret);\
            break;\
        case numeric_type::Int32:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, int32_t, rhs, ret);\
            break;\
        case numeric_type::Int64:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, int64_t, rhs, ret);\
            break;\
        case numeric_type::Float:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, float, rhs, ret);\
            break;\
        case numeric_type::Double:\
            matrix_operator_multiply_right_template(__TYPE__, lhs, templateRows, templateColumns, double, rhs, ret);\
            break;\
    }\
} while (0)

#define matrix_operator_divide_template(__TYPE__, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret)\
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0> l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l /= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l /= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l /= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l /= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l /= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
        {\
            dlib::matrix<__TYPE__, 0, 0>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(rhs));\
            l /= r;\
        }\
        else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
        {\
            dlib::matrix<__TYPE__, 0, 1>& r = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(rhs));\
            l /= r;\
        }\
        else\
        {\
            err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
            break;\
        }\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_primitive_template(__TYPE__, __OPERAND__, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret) \
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<__TYPE__, 0, 0> l = *(static_cast<dlib::matrix<__TYPE__, 0, 0>*>(lhs));\
        l __OPERAND__= rhs;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        l __OPERAND__= rhs;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        l __OPERAND__= rhs;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        l __OPERAND__= rhs;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_begin_template(__TYPE__, matrix, templateRows, templateColumns, ret, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__>*)matrix)->begin();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->begin();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 1, 3>*)matrix)->begin();\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 2, 2>*)matrix)->begin();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 31, 1>*)matrix)->begin();\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_end_template(__TYPE__, matrix, templateRows, templateColumns, ret, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__>*)matrix)->end();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->end();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 1, 3>*)matrix)->end();\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 2, 2>*)matrix)->end();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, 31, 1>*)matrix)->end();\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_nc_template(__TYPE__, matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<__TYPE__>*)matrix)->nc();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->nc();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 1, 3>*)matrix)->nc();\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 2, 2>*)matrix)->nc();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 31, 1>*)matrix)->nc();\
    }\
} while (0)

#define matrix_nr_template(__TYPE__, matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<__TYPE__>*)matrix)->nr();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->nr();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 1, 3>*)matrix)->nr();\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 2, 2>*)matrix)->nr();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 31, 1>*)matrix)->nr();\
    }\
} while (0)

#define matrix_size_template(__TYPE__, matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<__TYPE__>*)matrix)->size();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->size();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 1, 3>*)matrix)->size();\
    }\
    else if (templateRows == 2 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 2, 1>*)matrix)->size();\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 2, 2>*)matrix)->size();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<__TYPE__, 31, 1>*)matrix)->size();\
    }\
} while (0)

#define matrix_delete_template(__TYPE__, matrix, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        delete ((dlib::matrix<__TYPE__>*)matrix);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        delete ((dlib::matrix<__TYPE__, 0, 1>*)matrix);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        delete ((dlib::matrix<__TYPE__, 1, 3>*)matrix);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        delete ((dlib::matrix<__TYPE__, 2, 2>*)matrix);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        delete ((dlib::matrix<__TYPE__, 31, 1>*)matrix);\
    }\
} while (0)

// Check templateRows and templateColumns
// COMPILE_TIME_ASSERT(NC == 1 || NC == 0 || NR == 1 || NR == 0);
#define matrix_operator_get_one_row_column_template(__TYPE__, matrix, index, templateRows, templateColumns, ret, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& tmp = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
        *((__TYPE__*)ret) = tmp(index);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        *((__TYPE__*)ret) = tmp(index);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& tmp = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        *((__TYPE__*)ret) = tmp(index);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        *((__TYPE__*)ret) = tmp(index);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

// Check templateRows and templateColumns
// COMPILE_TIME_ASSERT(NC == 1 || NC == 0 || NR == 1 || NR == 0);
#define matrix_operator_set_one_row_column_template(__TYPE__, matrix, index, templateRows, templateColumns, value, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& tmp = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
        tmp(index) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        tmp(index) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& tmp = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        tmp(index) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        tmp(index) = *((__TYPE__*)value);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_get_row_column_template(__TYPE__, matrix, row, column, templateRows, templateColumns, ret, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& tmp = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
        *((__TYPE__*)ret) = tmp(row, column);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        *((__TYPE__*)ret) = tmp(row, column);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& tmp = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        *((__TYPE__*)ret) = tmp(row, column);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& tmp = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(matrix));\
        *((__TYPE__*)ret) = tmp(row, column);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        *((__TYPE__*)ret) = tmp(row, column);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_set_row_column_template(__TYPE__, matrix, row, column, templateRows, templateColumns, value, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& tmp = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
        tmp(row, column) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        tmp(row, column) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& tmp = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        tmp(row, column) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& tmp = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(matrix));\
        tmp(row, column) = *((__TYPE__*)value);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& tmp = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        tmp(row, column) = *((__TYPE__*)value);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_operator_left_shift_template(__TYPE__, matrix, templateRows, templateColumns, stream, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& mat = *(static_cast<dlib::matrix<__TYPE__>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& mat = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 2 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 2, 1>& mat = *(static_cast<dlib::matrix<__TYPE__, 2, 1>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 2 && templateColumns == 2)\
    {\
        dlib::matrix<__TYPE__, 2, 2>& mat = *(static_cast<dlib::matrix<__TYPE__, 2, 2>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3>& mat = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& mat = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix));\
        *stream << mat;\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#pragma endregion template

#pragma region matrix

DLLEXPORT void* matrix_new(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new matrix<uint8_t>();
        case matrix_element_type::UInt16:
            return new matrix<uint16_t>();
        case matrix_element_type::UInt32:
            return new matrix<uint32_t>();
        case matrix_element_type::UInt64:
            return new matrix<uint64_t>();
        case matrix_element_type::Int8:
            return new matrix<int8_t>();
        case matrix_element_type::Int16:
            return new matrix<int16_t>();
        case matrix_element_type::Int32:
            return new matrix<int32_t>();
        case matrix_element_type::Int64:
            return new matrix<int64_t>();
        case matrix_element_type::Float:
            return new matrix<float>();
        case matrix_element_type::Double:
            return new matrix<double>();
        case matrix_element_type::RgbPixel:
            return new matrix<rgb_pixel>();
        case matrix_element_type::HsiPixel:
            return new matrix<hsi_pixel>();
        case matrix_element_type::RgbAlphaPixel:
            return new matrix<rgb_alpha_pixel>();
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new1(matrix_element_type type, int num_rows, int num_cols)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new matrix<uint8_t>(num_rows, num_cols);
        case matrix_element_type::UInt16:
            return new matrix<uint16_t>(num_rows, num_cols);
        case matrix_element_type::UInt32:
            return new matrix<uint32_t>(num_rows, num_cols);
        case matrix_element_type::UInt64:
            return new matrix<uint64_t>(num_rows, num_cols);
        case matrix_element_type::Int8:
            return new matrix<int8_t>(num_rows, num_cols);
        case matrix_element_type::Int16:
            return new matrix<int16_t>(num_rows, num_cols);
        case matrix_element_type::Int32:
            return new matrix<int32_t>(num_rows, num_cols);
        case matrix_element_type::Int64:
            return new matrix<int64_t>(num_rows, num_cols);
        case matrix_element_type::Float:
            return new matrix<float>(num_rows, num_cols);
        case matrix_element_type::Double:
            return new matrix<double>(num_rows, num_cols);
        case matrix_element_type::RgbPixel:
            return new matrix<rgb_pixel>(num_rows, num_cols);
        case matrix_element_type::HsiPixel:
            return new matrix<hsi_pixel>(num_rows, num_cols);
        case matrix_element_type::RgbAlphaPixel:
            return new matrix<rgb_alpha_pixel>(num_rows, num_cols);
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new2(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_new2_template(uint8_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt16:
            matrix_new2_template(uint16_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt32:
            matrix_new2_template(uint32_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt64:
            matrix_new2_template(uint64_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int8:
            matrix_new2_template(int8_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int16:
            matrix_new2_template(int16_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int32:
            matrix_new2_template(int32_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int64:
            matrix_new2_template(int64_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Float:
            matrix_new2_template(float, num_rows, num_cols, src);
            break;
        case matrix_element_type::Double:
            matrix_new2_template(double, num_rows, num_cols, src);
            break;
        case matrix_element_type::RgbPixel:
            matrix_new2_template(rgb_pixel, num_rows, num_cols, src);
            break;
        case matrix_element_type::HsiPixel:
            matrix_new2_template(hsi_pixel, num_rows, num_cols, src);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_new2_template(rgb_alpha_pixel, num_rows, num_cols, src);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new3(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_new2_template(uint8_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt16:
            matrix_new2_template(uint16_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt32:
            matrix_new2_template(uint32_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::UInt64:
            matrix_new2_template(uint64_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int8:
            matrix_new2_template(int8_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int16:
            matrix_new2_template(int16_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int32:
            matrix_new2_template(int32_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Int64:
            matrix_new2_template(int64_t, num_rows, num_cols, src);
            break;
        case matrix_element_type::Float:
            matrix_new2_template(float, num_rows, num_cols, src);
            break;
        case matrix_element_type::Double:
            matrix_new2_template(double, num_rows, num_cols, src);
            break;
        case matrix_element_type::RgbPixel:
            matrix_new2_template(rgb_pixel, num_rows, num_cols, src);
            break;
        case matrix_element_type::HsiPixel:
            matrix_new2_template(hsi_pixel, num_rows, num_cols, src);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_new2_template(rgb_alpha_pixel, num_rows, num_cols, src);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new4(matrix_element_type type, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_new4_template(uint8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            matrix_new4_template(uint16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            matrix_new4_template(uint32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt64:
            matrix_new4_template(uint64_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            matrix_new4_template(int8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            matrix_new4_template(int16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            matrix_new4_template(int32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int64:
            matrix_new4_template(int64_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            matrix_new4_template(float, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            matrix_new4_template(double, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            matrix_new4_template(rgb_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            matrix_new4_template(hsi_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_new4_template(rgb_alpha_pixel, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT int matrix_begin(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_begin_template(uint8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_begin_template(uint16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_begin_template(uint32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_begin_template(uint64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_begin_template(int8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_begin_template(int16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_begin_template(int32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_begin_template(int64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_begin_template(float, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_begin_template(double, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_begin_template(rgb_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_begin_template(hsi_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_begin_template(rgb_alpha_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_end(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_end_template(uint8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_end_template(uint16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_end_template(uint32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_end_template(uint64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_end_template(int8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_end_template(int16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_end_template(int32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_end_template(int64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_end_template(float, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_end_template(double, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_end_template(rgb_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_end_template(hsi_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_end_template(rgb_alpha_pixel, matrix, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_nc(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_nc_template(uint8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_nc_template(uint16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_nc_template(uint32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_nc_template(uint64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_nc_template(int8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_nc_template(int16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_nc_template(int32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_nc_template(int64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_nc_template(float, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_nc_template(double, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_nc_template(rgb_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_nc_template(hsi_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_nc_template(rgb_alpha_pixel, matrix, templateRows, templateColumns, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_nr(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_nr_template(uint8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_nr_template(uint16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_nr_template(uint32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_nr_template(uint64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_nr_template(int8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_nr_template(int16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_nr_template(int32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_nr_template(int64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_nr_template(float, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_nr_template(double, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_nr_template(rgb_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_nr_template(hsi_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_nr_template(rgb_alpha_pixel, matrix, templateRows, templateColumns, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_size(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_size_template(uint8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_size_template(uint16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_size_template(uint32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_size_template(uint64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_size_template(int8_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_size_template(int16_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_size_template(int32_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_size_template(int64_t, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_size_template(float, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_size_template(double, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_size_template(rgb_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_size_template(hsi_pixel, matrix, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_size_template(rgb_alpha_pixel, matrix, templateRows, templateColumns, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void matrix_delete(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns)
{
    // dlib::matrix is template type.
    // Ex. dlib::matrix<float, 31, 1> does NOT equal to dlib::matrix<float>
    // Template argument is decided in compile time rather than runtime.
    // So we can not specify template argument as variable.
    // In other words, we have to call delete for void* because we do not known exact type.
    // Fortunately, dlib::matrix does not implement destructor.
    // So it is could be no problem when delete void* and destructor is not called.
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_delete_template(uint8_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            matrix_delete_template(uint16_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            matrix_delete_template(uint32_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt64:
            matrix_delete_template(uint64_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            matrix_delete_template(int8_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            matrix_delete_template(int16_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            matrix_delete_template(int32_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Int64:
            matrix_delete_template(int64_t, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            matrix_delete_template(float, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            matrix_delete_template(double, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            matrix_delete_template(rgb_pixel, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            matrix_delete_template(hsi_pixel, matrix, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_delete_template(rgb_alpha_pixel, matrix, templateRows, templateColumns);
            break;
    }
}

DLLEXPORT int matrix_operator_array(matrix_element_type type, void* matrix, void* array)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_array_template(uint8_t, matrix, array);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_array_template(uint16_t, matrix, array);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_array_template(uint32_t, matrix, array);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_array_template(uint64_t, matrix, array);
            break;
        case matrix_element_type::Int8:
            matrix_operator_array_template(int8_t, matrix, array);
            break;
        case matrix_element_type::Int16:
            matrix_operator_array_template(int16_t, matrix, array);
            break;
        case matrix_element_type::Int32:
            matrix_operator_array_template(int32_t, matrix, array);
            break;
        case matrix_element_type::Int64:
            matrix_operator_array_template(int64_t, matrix, array);
            break;
        case matrix_element_type::Float:
            matrix_operator_array_template(float, matrix, array);
            break;
        case matrix_element_type::Double:
            matrix_operator_array_template(double, matrix, array);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_array_template(rgb_pixel, matrix, array);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_array_template(hsi_pixel, matrix, array);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_array_template(rgb_alpha_pixel, matrix, array);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_get_one_row_column(matrix_element_type type, void* matrix, int index, int templateRows, int templateColumns, void* ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_get_one_row_column_template(uint8_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_get_one_row_column_template(uint16_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_get_one_row_column_template(uint32_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_get_one_row_column_template(uint64_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_get_one_row_column_template(int8_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_get_one_row_column_template(int16_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_get_one_row_column_template(int32_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_get_one_row_column_template(int64_t, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_get_one_row_column_template(float, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_get_one_row_column_template(double, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_get_one_row_column_template(rgb_pixel, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_get_one_row_column_template(hsi_pixel, matrix, index, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_get_one_row_column_template(rgb_alpha_pixel, matrix, index, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_set_one_row_column(matrix_element_type type, void* matrix, int index, int templateRows, int templateColumns, void* value)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_set_one_row_column_template(uint8_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_set_one_row_column_template(uint16_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_set_one_row_column_template(uint32_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_set_one_row_column_template(uint64_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_set_one_row_column_template(int8_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_set_one_row_column_template(int16_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_set_one_row_column_template(int32_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_set_one_row_column_template(int64_t, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_set_one_row_column_template(float, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_set_one_row_column_template(double, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_set_one_row_column_template(rgb_pixel, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_set_one_row_column_template(hsi_pixel, matrix, index, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_set_one_row_column_template(rgb_alpha_pixel, matrix, index, templateRows, templateColumns, value, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_get_row_column(matrix_element_type type, void* matrix, int row, int column, int templateRows, int templateColumns, void* ret)
{    
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_get_row_column_template(uint8_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_get_row_column_template(uint16_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_get_row_column_template(uint32_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_get_row_column_template(uint64_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_get_row_column_template(int8_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_get_row_column_template(int16_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_get_row_column_template(int32_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_get_row_column_template(int64_t, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_get_row_column_template(float, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_get_row_column_template(double, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_get_row_column_template(rgb_pixel, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_get_row_column_template(hsi_pixel, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_get_row_column_template(rgb_alpha_pixel, matrix, row, column, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_set_row_column(matrix_element_type type, void* matrix, int row, int column, int templateRows, int templateColumns, void* value)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_set_row_column_template(uint8_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_set_row_column_template(uint16_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_set_row_column_template(uint32_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_set_row_column_template(uint64_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_set_row_column_template(int8_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_set_row_column_template(int16_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_set_row_column_template(int32_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_set_row_column_template(int64_t, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_set_row_column_template(float, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_set_row_column_template(double, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_set_row_column_template(rgb_pixel, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_set_row_column_template(hsi_pixel, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_set_row_column_template(rgb_alpha_pixel, matrix, row, column, templateRows, templateColumns, value, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion

#pragma region operator

DLLEXPORT int matrix_operator_left_shift(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_left_shift_template(int8_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_left_shift_template(uint16_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_left_shift_template(uint32_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_left_shift_template(uint64_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_left_shift_template(int8_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_left_shift_template(int16_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_left_shift_template(int32_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_left_shift_template(int64_t, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_left_shift_template(float, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_left_shift_template(double, matrix, templateRows, templateColumns, stream, err);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_add(matrix_element_type type,
                                  void* lhs,
                                  void* rhs,
                                  const int leftTemplateRows,
                                  const int leftTemplateColumns,
                                  const int rightTemplateRows,
                                  const int rightTemplateColumns,
                                  void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_add_template(uint8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_add_template(uint16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_add_template(uint32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_add_template(uint64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_add_template(int8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_add_template(int16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_add_template(int32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_add_template(int64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_add_template(float, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_add_template(double, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_negative(matrix_element_type type,
                                       void* matrix,
                                       const int templateRows,
                                       const int templateColumns,
                                       void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_negative_template(uint8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_negative_template(uint16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_negative_template(uint32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_negative_template(uint64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_operator_negative_template(int8_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_operator_negative_template(int16_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_operator_negative_template(int32_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_operator_negative_template(int64_t, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_operator_negative_template(float, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_operator_negative_template(double, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_subtract(matrix_element_type type,
                                       void* lhs,
                                       void* rhs,
                                       const int leftTemplateRows,
                                       const int leftTemplateColumns,
                                       const int rightTemplateRows,
                                       const int rightTemplateColumns,
                                       void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_subtract_template(uint8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_subtract_template(uint16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_subtract_template(uint32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_subtract_template(uint64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_subtract_template(int8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_subtract_template(int16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_subtract_template(int32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_subtract_template(int64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_subtract_template(float, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_subtract_template(double, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_subtract_dpoint(matrix_element_type type,
                                              void* lhs,
                                              dlib::dpoint* rhs,
                                              const int templateRows,
                                              const int templateColumns,
                                              void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::Double:
            matrix_operator_subtract_right_vector_2_template(double, lhs, templateRows, templateColumns, double, rhs, ret);
            break;
        case matrix_element_type::UInt8:
        case matrix_element_type::UInt16:
        case matrix_element_type::UInt32:
        case matrix_element_type::UInt64:
        case matrix_element_type::Int8:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Int64:
        case matrix_element_type::Float:
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply(matrix_element_type type,
                                       void* lhs,
                                       void* rhs,
                                       const int leftTemplateRows,
                                       const int leftTemplateColumns,
                                       const int rightTemplateRows,
                                       const int rightTemplateColumns,
                                       void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_multiply_template(uint8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_multiply_template(uint16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_multiply_template(uint32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_multiply_template(uint64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_multiply_template(int8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_multiply_template(int16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_multiply_template(int32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_multiply_template(int64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_multiply_template(float, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_multiply_template(double, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply_dpoint(matrix_element_type type,
                                              void* lhs,
                                              dlib::dpoint* rhs,
                                              const int templateRows,
                                              const int templateColumns,
                                              void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::Double:
            matrix_operator_multiply_right_vector_2_template(double, lhs, templateRows, templateColumns, double, rhs, ret);
            break;
        case matrix_element_type::UInt8:
        case matrix_element_type::UInt16:
        case matrix_element_type::UInt32:
        case matrix_element_type::UInt64:
        case matrix_element_type::Int8:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Int64:
        case matrix_element_type::Float:
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply_left_numeric(numeric_type numeric_type,
                                                    void* lhs,
                                                    matrix_element_type type,
                                                    void* rhs,
                                                    const int templateRows,
                                                    const int templateColumns,
                                                    void** ret)
{
    int err = ERR_OK;

    switch(numeric_type)
    {
        case numeric_type::UInt8:
            matrix_operator_multiply_left_numeric_template(uint8_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::UInt16:
            matrix_operator_multiply_left_numeric_template(uint16_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::UInt32:
            matrix_operator_multiply_left_numeric_template(uint32_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::UInt64:
            matrix_operator_multiply_left_numeric_template(uint64_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Int8:
            matrix_operator_multiply_left_numeric_template(int8_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Int16:
            matrix_operator_multiply_left_numeric_template(int16_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Int32:
            matrix_operator_multiply_left_numeric_template(int32_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Int64:
            matrix_operator_multiply_left_numeric_template(int64_t, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Float:
            matrix_operator_multiply_left_numeric_template(float, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        case numeric_type::Double:
            matrix_operator_multiply_left_numeric_template(double, lhs, type, rhs, templateRows, templateColumns, ret);
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply_right_numeric(matrix_element_type type,
                                                     void* lhs,
                                                     const int templateRows,
                                                     const int templateColumns,
                                                     numeric_type numeric_type,
                                                     void* rhs,
                                                     void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_multiply_right_numeric_template(uint8_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_multiply_right_numeric_template(uint16_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_multiply_right_numeric_template(uint32_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_multiply_right_numeric_template(uint64_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_multiply_right_numeric_template(int8_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_multiply_right_numeric_template(int16_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_multiply_right_numeric_template(int32_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_multiply_right_numeric_template(int64_t, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_multiply_right_numeric_template(float, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_multiply_right_numeric_template(double, lhs, templateRows, templateColumns, numeric_type, rhs, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_divide(matrix_element_type type,
                                     void* lhs,
                                     void* rhs,
                                     const int leftTemplateRows,
                                     const int leftTemplateColumns,
                                     const int rightTemplateRows,
                                     const int rightTemplateColumns,
                                     void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_divide_template(uint8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_divide_template(uint16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_divide_template(uint32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_divide_template(uint64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_divide_template(int8_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_divide_template(int16_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_divide_template(int32_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_divide_template(int64_t, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_divide_template(float, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_divide_template(double, lhs, leftTemplateRows, leftTemplateColumns, rhs, rightTemplateRows, rightTemplateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}


DLLEXPORT int matrix_operator_divide_double(matrix_element_type type,
                                            void* lhs,
                                            double rhs,
                                            const int leftTemplateRows,
                                            const int leftTemplateColumns,
                                            void** ret)
{
    int err = ERR_OK;
    
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_primitive_template(uint8_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_primitive_template(uint16_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_primitive_template(uint32_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_primitive_template(uint64_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_primitive_template(int8_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_primitive_template(int16_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_primitive_template(int32_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_primitive_template(int64_t, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_primitive_template(float, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_primitive_template(double, /, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma endregion matrix

#endif