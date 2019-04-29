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
#include "../template.h"

#include "matrix_common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::matrix<__TYPE__>();

#define matrix_new1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::matrix<__TYPE__>(num_rows, num_cols);

#define matrix_new2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto m = new matrix<__TYPE__>(num_rows, num_cols);\
auto &d = *m;\
__TYPE__* s = static_cast<__TYPE__*>(src);\
for (int32_t r = 0; r < num_rows; r++)\
for (int32_t c = 0, step = r * num_cols; c < num_cols; c++)\
    d(r, c) = s[step + c];\
return m;

#define matrix_new4_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>();

#define matrix_new5_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& v = *static_cast<std::vector<__TYPE__>*>(vector);\
std::initializer_list<__TYPE__> il(v.data(), v.data() + v.size());\
ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(il);

#define matrix_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
delete ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix);

#define matrix_clone_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
const auto& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);

#define matrix_begin_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->begin();\

#define matrix_end_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*((__TYPE__**)ret) = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->end();\

#define matrix_nc_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->nc();

#define matrix_nr_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->nr();

#define matrix_operator_array_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
__TYPE__* src = static_cast<__TYPE__*>(array);\
const long row = mat.nr();\
const long column = mat.nc();\
for (long r = 0; r < row; ++r)\
    for (long c = 0; c < column; ++c)\
        mat(r, c) = src[r * column + c];

#define matrix_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->size();

#define matrix_set_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->set_size(length);

#define matrix_set_size2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)matrix)->set_size(rows, cols);

#define matrix_deserialize_matrix_proxy_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);\
auto* matrix = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>();\
p >> (*matrix);\
*ret = matrix;

// Check templateRows and templateColumns
// COMPILE_TIME_ASSERT(NC == 1 || NC == 0 || NR == 1 || NR == 0);
#define matrix_operator_get_one_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*((__TYPE__*)ret) = tmp(index);

#define matrix_operator_set_one_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
tmp(index) = *((__TYPE__*)value);\

#define matrix_operator_get_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*((__TYPE__*)ret) = tmp(row, column);

#define matrix_operator_set_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& tmp = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
tmp(row, column) = *((__TYPE__*)value);

#define matrix_operator_negative_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto tmp = -m;\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(tmp);

#define matrix_operator_left_shift_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
*stream << mat;

#define matrix_operator_add_template(l, r) \
l += r

#define matrix_operator_subtract_template(l, r) \
l -= r

#define matrix_operator_multiply_template(l, r) \
l *= r

#define matrix_operator_divide_template(l, r) \
l /= r

#define matrix_operator_divide_primitive_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__> l = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(lhs));\
l /= rhs;\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(l);

#define matrix_operator_subtract_right_vector_2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::vector<__TYPE__,2>& r = *static_cast<dlib::vector<__TYPE__,2>*>(rhs);\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& l = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(lhs));\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__> m = l - r;\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);\

#define matrix_operator_multiply_right_vector_2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::vector<__TYPE__,2>& r = *static_cast<dlib::vector<__TYPE__,2>*>(rhs);\
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

#pragma endregion template

#pragma region matrix

DLLEXPORT void* matrix_new(matrix_element_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new_template,
                    0,
                    0,
                    num_rows,
                    num_cols);

    return ret;
}

DLLEXPORT void* matrix_new1(matrix_element_type type, int num_rows, int num_cols)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new1_template,
                    0,
                    0,
                    num_rows,
                    num_cols);

    return ret;
}

DLLEXPORT void* matrix_new2(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new2_template,
                    0,
                    0,
                    num_rows,
                    num_cols,
                    src);

    return ret;
}

DLLEXPORT void* matrix_new3(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new2_template,
                    0,
                    0,
                    num_rows,
                    num_cols,
                    src);

    return ret;
}

DLLEXPORT void* matrix_new4(matrix_element_type type, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new4_template,
                    templateRows,
                    templateColumns,
                    ret);

    return ret;
}

DLLEXPORT void* matrix_new5(matrix_element_type type, const int templateRows, const int templateColumns, void* vector)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_new5_template,
                    templateRows,
                    templateColumns,
                    vector,
                    ret);

    return ret;
}

DLLEXPORT int matrix_begin(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_begin_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
}

DLLEXPORT int matrix_end(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_end_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
}

DLLEXPORT int matrix_nc(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_nc_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
}

DLLEXPORT int matrix_nr(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_nr_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
}

DLLEXPORT int matrix_set_size(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int length)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size2_template,
                    matrix_set_size_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    length);

    return error;
}

DLLEXPORT int matrix_set_size2(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, const int rows, const int cols)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size3_template,
                    matrix_set_size2_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    rows,
                    cols);

    return error;
}

DLLEXPORT int matrix_size(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size3_template,
                    matrix_size_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
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
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_delete_template,
                    templateRows,
                    templateColumns,
                    matrix);
}

DLLEXPORT void* matrix_clone(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_clone_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return ret;
}

DLLEXPORT int matrix_operator_array(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, void* array)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_operator_array_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    array);

    return error;
}

DLLEXPORT int matrix_operator_get_one_row_column(matrix_element_type type, void* matrix, int index, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size4_template,
                    matrix_operator_get_one_row_column_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    index,
                    ret);

    return error;
}

DLLEXPORT int matrix_operator_set_one_row_column(matrix_element_type type, void* matrix, int index, int templateRows, int templateColumns, void* value)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size4_template,
                    matrix_operator_set_one_row_column_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    index,
                    ret);

    return error;
}

DLLEXPORT int matrix_operator_get_row_column(matrix_element_type type, void* matrix, int row, int column, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_operator_get_row_column_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    row,
                    column,
                    ret);

    return error;
}

DLLEXPORT int matrix_operator_set_row_column(matrix_element_type type, void* matrix, int row, int column, int templateRows, int templateColumns, void* value)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_operator_set_row_column_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    row,
                    column,
                    value);

    return error;
}

DLLEXPORT int matrix_deserialize_matrix_proxy(void* proxy,
                                              matrix_element_type type,
                                              int templateRows,
                                              int templateColumns,
                                              void** ret,
                                              std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        matrix_template(type,
                        error,
                        matrix_template_size_template,
                        matrix_deserialize_matrix_proxy_template,
                        templateRows,
                        templateColumns,
                        proxy,
                        ret);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

#pragma endregion

#pragma region operator

DLLEXPORT int matrix_operator_left_shift(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, std::ostringstream* stream)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_operator_left_shift_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            stream);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_asm_template,
                            matrix_operator_add_template,
                            leftTemplateRows,
                            leftTemplateColumns,
                            lhs,
                            rhs,
                            rightTemplateRows,
                            rightTemplateColumns,
                            ret);

    return error;
}

DLLEXPORT int matrix_operator_negative(matrix_element_type type,
                                       void* matrix,
                                       const int templateRows,
                                       const int templateColumns,
                                       void** ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_operator_negative_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_asm_template,
                            matrix_operator_subtract_template,
                            leftTemplateRows,
                            leftTemplateColumns,
                            lhs,
                            rhs,
                            rightTemplateRows,
                            rightTemplateColumns,
                            ret);

    return error;
}

DLLEXPORT int matrix_operator_subtract_dpoint(matrix_element_type type,
                                              void* lhs,
                                              dlib::dpoint* rhs,
                                              const int templateRows,
                                              const int templateColumns,
                                              void** ret)
{
    int error = ERR_OK;

    matrix_double_template(type,
                           error,
                           matrix_template_size5_template,
                           matrix_operator_subtract_right_vector_2_template,
                           templateRows,
                           templateColumns,
                           lhs,
                           rhs,
                           ret);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_asm_template,
                            matrix_operator_multiply_template,
                            leftTemplateRows,
                            leftTemplateColumns,
                            lhs,
                            rhs,
                            rightTemplateRows,
                            rightTemplateColumns,
                            ret);

    return error;
}

DLLEXPORT int matrix_operator_multiply_dpoint(matrix_element_type type,
                                              void* lhs,
                                              dlib::dpoint* rhs,
                                              const int templateRows,
                                              const int templateColumns,
                                              void** ret)
{
    int error = ERR_OK;

    matrix_double_template(type,
                           error,
                           matrix_template_size2x2_template,
                           matrix_operator_multiply_right_vector_2_template,
                           templateRows,
                           templateColumns,
                           lhs,
                           rhs,
                           ret);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_d_template,
                            matrix_operator_divide_template,
                            leftTemplateRows,
                            leftTemplateColumns,
                            lhs,
                            rhs,
                            rightTemplateRows,
                            rightTemplateColumns,
                            ret);

    return error;
}


DLLEXPORT int matrix_operator_divide_double(matrix_element_type type,
                                            void* lhs,
                                            double rhs,
                                            const int leftTemplateRows,
                                            const int leftTemplateColumns,
                                            void** ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_operator_divide_primitive_template,
                            leftTemplateRows,
                            leftTemplateColumns,
                            lhs,
                            rhs,
                            ret);

    return error;
}

#pragma endregion operator

#pragma endregion matrix

#endif