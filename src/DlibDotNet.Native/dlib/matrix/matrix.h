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

#define matrix_template_size_arg1_local_template(__TYPE__, __ROWS__, __COLUMNS__, __FUNC__, error, arg1) \
do {\
    if (__ROWS__ == 0 && __COLUMNS__ == 0)\
    {\
        __FUNC__(__TYPE__, 0, 0, error, arg1);\
    }\
    else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 0, 1, error, arg1);\
    }\
    else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
    {\
        __FUNC__(__TYPE__, 1, 3, error, arg1);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 2, 1, error, arg1);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 2)\
    {\
        __FUNC__(__TYPE__, 2, 2, error, arg1);\
    }\
    else if (__ROWS__ == 5 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 5, 1, error, arg1);\
    }\
    else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 31, 1, error, arg1);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, __FUNC__, error, arg1, arg2) \
do {\
    if (__ROWS__ == 0 && __COLUMNS__ == 0)\
    {\
        __FUNC__(__TYPE__, 0, 0, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 0, 1, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
    {\
        __FUNC__(__TYPE__, 1, 3, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 2, 1, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 2)\
    {\
        __FUNC__(__TYPE__, 2, 2, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 5 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 5, 1, error, arg1, arg2);\
    }\
    else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 31, 1, error, arg1, arg2);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_template_size_arg4_local_template(__TYPE__, __ROWS__, __COLUMNS__, __FUNC__, error, arg1, arg2, arg3, arg4) \
do {\
    if (__ROWS__ == 0 && __COLUMNS__ == 0)\
    {\
        __FUNC__(__TYPE__, 0, 0, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 0, 1, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
    {\
        __FUNC__(__TYPE__, 1, 3, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 2, 1, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 2)\
    {\
        __FUNC__(__TYPE__, 2, 2, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 5 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 5, 1, error, arg1, arg2, arg3, arg4);\
    }\
    else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 31, 1, error, arg1, arg2, arg3, arg4);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_template_size_one_row_column_arg3_local_template(__TYPE__, __ROWS__, __COLUMNS__, __FUNC__, error, arg1, arg2, arg3) \
do {\
    if (__ROWS__ == 0 && __COLUMNS__ == 0)\
    {\
        __FUNC__(__TYPE__, 0, 0, error, arg1, arg2, arg3);\
    }\
    else if (__ROWS__ == 0 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 0, 1, error, arg1, arg2, arg3);\
    }\
    else if (__ROWS__ == 1 && __COLUMNS__ == 3)\
    {\
        __FUNC__(__TYPE__, 1, 3, error, arg1, arg2, arg3);\
    }\
    else if (__ROWS__ == 2 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 2, 1, error, arg1, arg2, arg3);\
    }\
    else if (__ROWS__ == 5 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 5, 1, error, arg1, arg2, arg3);\
    }\
    else if (__ROWS__ == 31 && __COLUMNS__ == 1)\
    {\
        __FUNC__(__TYPE__, 31, 1, error, arg1, arg2, arg3);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_new4_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, ret) \
ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>();\

#define matrix_new4_template(__TYPE__, __ROWS__, __COLUMNS__, ret) \
do {\
    int error = ERR_OK;\
    matrix_template_size_arg1_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_new4_template_sub, error, ret);\
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

#define matrix_operator_array_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, array) \
do {\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
    __TYPE__* src = static_cast<__TYPE__*>(array);\
    const long row = mat.nr();\
    const long column = mat.nc();\
    for (long r = 0; r < row; ++r)\
        for (long c = 0; c < column; ++c)\
            mat(r, c) = src[r * column + c];\
} while (0)

#define matrix_operator_array_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, array) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_array_template_sub, error, matrix, array);\
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

#define matrix_operator_negative_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto tmp = -m;\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(tmp);\

#define matrix_operator_negative_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_negative_template_sub, error, matrix, ret);\
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

#define matrix_operator_subtract_right_vector_2_template_sub(__LEFT_TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__ ,rhs, ret) \
dlib::vector<__RIGHT_TYPE__,2>& r = *static_cast<dlib::vector<__RIGHT_TYPE__,2>*>(rhs);\
dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>& l = *(static_cast<dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>*>(lhs));\
dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__> m = l - r;\
*ret = new dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>(m);\

#define matrix_operator_subtract_right_vector_2_template(__TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__ ,rhs, ret) \
do {\
    matrix_template_size_vector_2_arg4_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_subtract_right_vector_2_template_sub, error, lhs, __RIGHT_TYPE__ ,rhs, ret);\
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

#define matrix_operator_multiply_left_template_sub(__LEFT_TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__, rhs, ret)\
__LEFT_TYPE__& l = *static_cast<__LEFT_TYPE__*>(lhs);\
dlib::matrix<__RIGHT_TYPE__, __ROWS__, __COLUMNS__>& r = *(static_cast<dlib::matrix<__RIGHT_TYPE__, __ROWS__, __COLUMNS__>*>(rhs));\
dlib::matrix<__RIGHT_TYPE__, __ROWS__, __COLUMNS__> m = l * r;\
*ret = new dlib::matrix<__RIGHT_TYPE__, __ROWS__, __COLUMNS__>(m);\

#define matrix_operator_multiply_left_template(__LEFT_TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__, rhs, ret)\
do {\
    matrix_template_size_arg4_local_template(__LEFT_TYPE__, __ROWS__, __COLUMNS__, matrix_operator_multiply_left_template_sub, error, lhs, __RIGHT_TYPE__, rhs, ret);\
} while (0)

#define matrix_operator_multiply_left_numeric_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, type ,rhs, ret)\
do {\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, uint8_t, rhs, ret);\
            break;\
        case matrix_element_type::UInt16:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, uint16_t, rhs, ret);\
            break;\
        case matrix_element_type::UInt32:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, uint32_t, rhs, ret);\
            break;\
        case matrix_element_type::UInt64:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, uint64_t, rhs, ret);\
            break;\
        case matrix_element_type::Int8:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, int8_t, rhs, ret);\
            break;\
        case matrix_element_type::Int16:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, int16_t, rhs, ret);\
            break;\
        case matrix_element_type::Int32:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, int32_t, rhs, ret);\
            break;\
        case matrix_element_type::Int64:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, int64_t, rhs, ret);\
            break;\
        case matrix_element_type::Float:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, float, rhs, ret);\
            break;\
        case matrix_element_type::Double:\
            matrix_operator_multiply_left_template(__NUMERIC_TYPE__, templateRows, templateColumns, error, lhs, double, rhs, ret);\
            break;\
        default:\
            error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_operator_multiply_right_template_sub(__LEFT_TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__, rhs, ret)\
__RIGHT_TYPE__& r = *static_cast<__RIGHT_TYPE__*>(rhs);\
dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>& l = *(static_cast<dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>*>(lhs));\
dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__> m = l * r;\
*ret = new dlib::matrix<__LEFT_TYPE__, __ROWS__, __COLUMNS__>(m);\

#define matrix_operator_multiply_right_template(__LEFT_TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__, rhs, ret)\
do {\
    matrix_template_size_arg4_local_template(__LEFT_TYPE__, __ROWS__, __COLUMNS__, matrix_operator_multiply_right_template_sub, error, lhs, __RIGHT_TYPE__, rhs, ret);\
} while (0)

#define matrix_operator_multiply_right_vector_2_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__ , rhs, ret)\
dlib::vector<__RIGHT_TYPE__,2>& r = *static_cast<dlib::vector<__RIGHT_TYPE__,2>*>(rhs);\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& l = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(lhs));\
if (__ROWS__ == 0 && __COLUMNS__ == 0)\
{\
    dlib::matrix<__TYPE__, 0, 0> m = l * r;\
    *ret = new dlib::matrix<__TYPE__, 0, 0>(m);\
}\
else\
{\
    dlib::matrix<__TYPE__, 2, 1> m = l * r;\
    *ret = new dlib::matrix<__TYPE__, 2, 1>(m);\
}\

#define matrix_operator_multiply_right_vector_2_template(__TYPE__, __ROWS__, __COLUMNS__, error, lhs, __RIGHT_TYPE__ , rhs, ret)\
do {\
    matrix_template_size_vector_2x2_arg4_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_multiply_right_vector_2_template_sub, error, lhs, __RIGHT_TYPE__ , rhs, ret);\
} while (0)

#define matrix_operator_multiply_right_numeric_template(__TYPE__, templateRows, templateColumns, error, lhs, numeric_type ,rhs, ret)\
do {\
    switch(numeric_type)\
    {\
        case ::numeric_type::UInt8:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, uint8_t, rhs, ret);\
            break;\
        case ::numeric_type::UInt16:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, uint16_t, rhs, ret);\
            break;\
        case ::numeric_type::UInt32:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, uint32_t, rhs, ret);\
            break;\
        case ::numeric_type::UInt64:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, uint64_t, rhs, ret);\
            break;\
        case ::numeric_type::Int8:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, int8_t, rhs, ret);\
            break;\
        case ::numeric_type::Int16:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, int16_t, rhs, ret);\
            break;\
        case ::numeric_type::Int32:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, int32_t, rhs, ret);\
            break;\
        case ::numeric_type::Int64:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, int64_t, rhs, ret);\
            break;\
        case ::numeric_type::Float:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, float, rhs, ret);\
            break;\
        case ::numeric_type::Double:\
            matrix_operator_multiply_right_template(__TYPE__, templateRows, templateColumns, error, lhs, double, rhs, ret);\
            break;\
        default:\
            error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
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
        l __OPERAND__ rhs;\
        *ret = new dlib::matrix<__TYPE__, 0, 0>(l);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1> l = *(static_cast<dlib::matrix<__TYPE__, 0, 1>*>(lhs));\
        l __OPERAND__ rhs;\
        *ret = new dlib::matrix<__TYPE__, 0, 1>(l);\
    }\
    else if (leftTemplateRows == 1 && leftTemplateColumns == 3)\
    {\
        dlib::matrix<__TYPE__, 1, 3> l = *(static_cast<dlib::matrix<__TYPE__, 1, 3>*>(lhs));\
        l __OPERAND__ rhs;\
        *ret = new dlib::matrix<__TYPE__, 1, 3>(l);\
    }\
    else if (leftTemplateRows == 31 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1> l = *(static_cast<dlib::matrix<__TYPE__, 31, 1>*>(lhs));\
        l __OPERAND__ rhs;\
        *ret = new dlib::matrix<__TYPE__, 31, 1>(l);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_begin_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
*((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->begin();\

#define matrix_begin_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_begin_template_sub, error, matrix, ret);\
} while (0)

#define matrix_end_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
*((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->end();\

#define matrix_end_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_end_template_sub, error, matrix, ret);\
} while (0)

#define matrix_nc_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->nc();\

#define matrix_nc_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_nc_template_sub, error, matrix, ret);\
} while (0)

#define matrix_nr_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->nr();\

#define matrix_nr_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_nr_template_sub, error, matrix, ret);\
} while (0)

#define matrix_size_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->size();\

#define matrix_size_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_size_template_sub, error, matrix, ret);\
} while (0)

#define matrix_set_size_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, value) \
do {\
    if (__ROWS__ == 0 && __COLUMNS__ == 1)\
    {\
        ((dlib::matrix<__TYPE__, 0, 1>*)matrix)->set_size(value);\
    }\
    else if (__ROWS__ == 1 && __COLUMNS__ == 0)\
    {\
        ((dlib::matrix<__TYPE__, 1, 0>*)matrix)->set_size(value);\
    }\
    else\
    {\
        error = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define matrix_delete_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix) \
delete ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix);\

#define matrix_delete_template(__TYPE__, __ROWS__, __COLUMNS__, matrix) \
do {\
    int error = ERR_OK;\
    matrix_template_size_arg1_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_delete_template_sub, error, matrix);\
} while (0)

// Check templateRows and templateColumns
// COMPILE_TIME_ASSERT(NC == 1 || NC == 0 || NR == 1 || NR == 0);
#define matrix_operator_get_one_row_column_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, index, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*((__TYPE__*)ret) = tmp(index);\

#define matrix_operator_get_one_row_column_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, index, ret) \
do {\
    matrix_template_size_one_row_column_arg3_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_get_one_row_column_template_sub, error, matrix, index, ret);\
} while (0)

// Check templateRows and templateColumns
// COMPILE_TIME_ASSERT(NC == 1 || NC == 0 || NR == 1 || NR == 0);
#define matrix_operator_set_one_row_column_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, index, value) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
tmp(index) = *((__TYPE__*)value);\

#define matrix_operator_set_one_row_column_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, index, value) \
do {\
    matrix_template_size_one_row_column_arg3_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_set_one_row_column_template_sub, error, matrix, index, value);\
} while (0)

#define matrix_operator_get_row_column_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, row, column, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*((__TYPE__*)ret) = tmp(row, column);\

#define matrix_operator_get_row_column_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, row, column, ret) \
do {\
    matrix_template_size_arg4_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_get_row_column_template_sub, error, matrix, row, column, ret);\
} while (0)

#define matrix_operator_set_row_column_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, row, column, value) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
tmp(row, column) = *((__TYPE__*)value);\

#define matrix_operator_set_row_column_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, row, column, value) \
do {\
    matrix_template_size_arg4_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_set_row_column_template_sub, error, matrix, row, column, value);\
} while (0)

#define matrix_operator_left_shift_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, stream) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*stream << mat;\

#define matrix_operator_left_shift_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, stream) \
do {\
    matrix_template_size_arg2_local_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_operator_left_shift_template_sub, error, matrix, stream);\
} while (0)

#define matrix_deserialize_matrix_proxy_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, proxy, ret) \
proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);\
auto* matrix = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>();\
p >> (*matrix);\
*ret = matrix;\

#define matrix_deserialize_matrix_proxy_template(__TYPE__, __ROWS__, __COLUMNS__, error, proxy, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_deserialize_matrix_proxy_template_sub, error, proxy, ret);\
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
    void* ret = nullptr;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_new4_template(uint8_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_new4_template(uint16_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_new4_template(uint32_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_new4_template(uint64_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_new4_template(int8_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_new4_template(int16_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_new4_template(int32_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int64:
            matrix_new4_template(int64_t, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_new4_template(float, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_new4_template(double, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_new4_template(rgb_pixel, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_new4_template(hsi_pixel, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_new4_template(rgb_alpha_pixel, templateRows, templateColumns, ret);
            break;
        default:
            break;
    }

    return ret;
}

DLLEXPORT int matrix_begin(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_begin_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_begin_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_begin_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_begin_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_begin_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_begin_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_begin_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_begin_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_begin_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_begin_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_begin_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_begin_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_begin_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
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
            matrix_end_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_end_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_end_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_end_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_end_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_end_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_end_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_end_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_end_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_end_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_end_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_end_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_end_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
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
            matrix_nc_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_nc_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_nc_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_nc_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_nc_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_nc_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_nc_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_nc_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_nc_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_nc_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_nc_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_nc_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_nc_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
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
            matrix_nr_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_nr_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_nr_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_nr_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_nr_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_nr_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_nr_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_nr_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_nr_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_nr_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_nr_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_nr_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_nr_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_set_size(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int length)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_set_size_template(uint8_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::UInt16:
            matrix_set_size_template(uint16_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::UInt32:
            matrix_set_size_template(uint32_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::UInt64:
            matrix_set_size_template(uint64_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Int8:
            matrix_set_size_template(int8_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Int16:
            matrix_set_size_template(int16_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Int32:
            matrix_set_size_template(int32_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Int64:
            matrix_set_size_template(int64_t, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Float:
            matrix_set_size_template(float, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::Double:
            matrix_set_size_template(double, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::RgbPixel:
            matrix_set_size_template(rgb_pixel, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::HsiPixel:
            matrix_set_size_template(hsi_pixel, templateRows, templateColumns, err, matrix, length);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_set_size_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, length);
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
            matrix_size_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_size_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_size_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_size_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_size_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_size_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_size_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_size_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_size_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_size_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_size_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_size_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_size_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
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
            matrix_delete_template(uint8_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::UInt16:
            matrix_delete_template(uint16_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::UInt32:
            matrix_delete_template(uint32_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::UInt64:
            matrix_delete_template(uint64_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Int8:
            matrix_delete_template(int8_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Int16:
            matrix_delete_template(int16_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Int32:
            matrix_delete_template(int32_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Int64:
            matrix_delete_template(int64_t, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Float:
            matrix_delete_template(float, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::Double:
            matrix_delete_template(double, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::RgbPixel:
            matrix_delete_template(rgb_pixel, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::HsiPixel:
            matrix_delete_template(hsi_pixel, templateRows, templateColumns, matrix);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_delete_template(rgb_alpha_pixel, templateRows, templateColumns, matrix);
            break;
    }
}

DLLEXPORT int matrix_operator_array(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void* array)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_array_template(uint8_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_array_template(uint16_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_array_template(uint32_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_array_template(uint64_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Int8:
            matrix_operator_array_template(int8_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Int16:
            matrix_operator_array_template(int16_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Int32:
            matrix_operator_array_template(int32_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Int64:
            matrix_operator_array_template(int64_t, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Float:
            matrix_operator_array_template(float, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::Double:
            matrix_operator_array_template(double, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_array_template(rgb_pixel, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_array_template(hsi_pixel, templateRows, templateColumns, err, matrix, array);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_array_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, array);
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
            matrix_operator_get_one_row_column_template(uint8_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_get_one_row_column_template(uint16_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_get_one_row_column_template(uint32_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_get_one_row_column_template(uint64_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_get_one_row_column_template(int8_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_get_one_row_column_template(int16_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_get_one_row_column_template(int32_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_get_one_row_column_template(int64_t, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_get_one_row_column_template(float, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_get_one_row_column_template(double, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_get_one_row_column_template(rgb_pixel, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_get_one_row_column_template(hsi_pixel, templateRows, templateColumns, err, matrix, index, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_get_one_row_column_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, index, ret);
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
            matrix_operator_set_one_row_column_template(uint8_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_set_one_row_column_template(uint16_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_set_one_row_column_template(uint32_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_set_one_row_column_template(uint64_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Int8:
            matrix_operator_set_one_row_column_template(int8_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Int16:
            matrix_operator_set_one_row_column_template(int16_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Int32:
            matrix_operator_set_one_row_column_template(int32_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Int64:
            matrix_operator_set_one_row_column_template(int64_t, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Float:
            matrix_operator_set_one_row_column_template(float, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::Double:
            matrix_operator_set_one_row_column_template(double, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_set_one_row_column_template(rgb_pixel, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_set_one_row_column_template(hsi_pixel, templateRows, templateColumns, err, matrix, index, value);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_set_one_row_column_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, index, value);
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
            matrix_operator_get_row_column_template(uint8_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_get_row_column_template(uint16_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_get_row_column_template(uint32_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_get_row_column_template(uint64_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_get_row_column_template(int8_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_get_row_column_template(int16_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_get_row_column_template(int32_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_get_row_column_template(int64_t, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_get_row_column_template(float, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_get_row_column_template(double, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_get_row_column_template(rgb_pixel, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_get_row_column_template(hsi_pixel, templateRows, templateColumns, err, matrix, row, column, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_get_row_column_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, row, column, ret);
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
            matrix_operator_set_row_column_template(uint8_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_set_row_column_template(uint16_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_set_row_column_template(uint32_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_set_row_column_template(uint64_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Int8:
            matrix_operator_set_row_column_template(int8_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Int16:
            matrix_operator_set_row_column_template(int16_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Int32:
            matrix_operator_set_row_column_template(int32_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Int64:
            matrix_operator_set_row_column_template(int64_t, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Float:
            matrix_operator_set_row_column_template(float, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::Double:
            matrix_operator_set_row_column_template(double, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::RgbPixel:
            matrix_operator_set_row_column_template(rgb_pixel, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::HsiPixel:
            matrix_operator_set_row_column_template(hsi_pixel, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_operator_set_row_column_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, row, column, value);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_deserialize_matrix_proxy(void* proxy, matrix_element_type type, int templateRows, int templateColumns, void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_deserialize_matrix_proxy_template(uint8_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_deserialize_matrix_proxy_template(uint16_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_deserialize_matrix_proxy_template(uint32_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_deserialize_matrix_proxy_template(uint64_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Int8:
            matrix_deserialize_matrix_proxy_template(int8_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Int16:
            matrix_deserialize_matrix_proxy_template(int16_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Int32:
            matrix_deserialize_matrix_proxy_template(int32_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Int64:
            matrix_deserialize_matrix_proxy_template(int64_t, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Float:
            matrix_deserialize_matrix_proxy_template(float, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::Double:
            matrix_deserialize_matrix_proxy_template(double, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_deserialize_matrix_proxy_template(rgb_pixel, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_deserialize_matrix_proxy_template(hsi_pixel, templateRows, templateColumns, err, proxy, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_deserialize_matrix_proxy_template(rgb_alpha_pixel, templateRows, templateColumns, err, proxy, ret);
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
            matrix_operator_left_shift_template(int8_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_left_shift_template(uint16_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_left_shift_template(uint32_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_left_shift_template(uint64_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Int8:
            matrix_operator_left_shift_template(int8_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Int16:
            matrix_operator_left_shift_template(int16_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Int32:
            matrix_operator_left_shift_template(int32_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Int64:
            matrix_operator_left_shift_template(int64_t, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Float:
            matrix_operator_left_shift_template(float, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::Double:
            matrix_operator_left_shift_template(double, templateRows, templateColumns, err, matrix, stream);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            matrix_operator_negative_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_negative_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_negative_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_negative_template(uint64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_negative_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_negative_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_negative_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_negative_template(int64_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_negative_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_negative_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            matrix_operator_subtract_right_vector_2_template(double, templateRows, templateColumns, err, lhs, double, rhs, ret);
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            matrix_operator_multiply_right_vector_2_template(double, templateRows, templateColumns, err, lhs, double, rhs, ret);
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply_left_numeric(::numeric_type numeric_type,
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
        case ::numeric_type::UInt8:
            matrix_operator_multiply_left_numeric_template(uint8_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::UInt16:
            matrix_operator_multiply_left_numeric_template(uint16_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::UInt32:
            matrix_operator_multiply_left_numeric_template(uint32_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::UInt64:
            matrix_operator_multiply_left_numeric_template(uint64_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Int8:
            matrix_operator_multiply_left_numeric_template(int8_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Int16:
            matrix_operator_multiply_left_numeric_template(int16_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Int32:
            matrix_operator_multiply_left_numeric_template(int32_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Int64:
            matrix_operator_multiply_left_numeric_template(int64_t, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Float:
            matrix_operator_multiply_left_numeric_template(float, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        case ::numeric_type::Double:
            matrix_operator_multiply_left_numeric_template(double, templateRows, templateColumns, err, lhs, type, rhs, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_operator_multiply_right_numeric(matrix_element_type type,
                                                     void* lhs,
                                                     const int templateRows,
                                                     const int templateColumns,
                                                     ::numeric_type numeric_type,
                                                     void* rhs,
                                                     void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_operator_multiply_right_numeric_template(uint8_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_multiply_right_numeric_template(uint16_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_multiply_right_numeric_template(uint32_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_multiply_right_numeric_template(uint64_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_multiply_right_numeric_template(int8_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_multiply_right_numeric_template(int16_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_multiply_right_numeric_template(int32_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_multiply_right_numeric_template(int64_t, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_multiply_right_numeric_template(float, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_multiply_right_numeric_template(double, templateRows, templateColumns, err, lhs, numeric_type, rhs, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            matrix_operator_primitive_template(uint8_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_operator_primitive_template(uint16_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_operator_primitive_template(uint32_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::UInt64:
            matrix_operator_primitive_template(uint64_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int8:
            matrix_operator_primitive_template(int8_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int16:
            matrix_operator_primitive_template(int16_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int32:
            matrix_operator_primitive_template(int32_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Int64:
            matrix_operator_primitive_template(int64_t, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Float:
            matrix_operator_primitive_template(float, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::Double:
            matrix_operator_primitive_template(double, /=, lhs, leftTemplateRows, leftTemplateColumns, rhs, ret);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma endregion matrix

#endif