#ifndef _CPP_MATRIX_UTILITIES_H_
#define _CPP_MATRIX_UTILITIES_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_utilities.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#define matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, destType, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& mat = *static_cast<dlib::matrix<__TYPE__>*>(matrix);\
        *ret = new dlib::matrix<destType>(dlib::matrix_cast<destType>(mat));\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& mat = *static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix);\
        *ret = new dlib::matrix<destType, 0, 1>(dlib::matrix_cast<destType>(mat));\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& mat = *static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix);\
        *ret = new dlib::matrix<destType, 31, 1>(dlib::matrix_cast<destType>(mat));\
    }\
} while (0)

#define matrix_cast_template(__TYPE__, desttype, matrix, templateRows, templateColumns, ret, err) \
do {\
    switch(desttype)\
    {\
        case matrix_element_type::UInt8:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, uint8_t, ret);\
            break;\
        case matrix_element_type::UInt16:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, uint16_t, ret);\
            break;\
        case matrix_element_type::UInt32:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, uint32_t, ret);\
            break;\
        case matrix_element_type::UInt64:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, uint64_t, ret);\
            break;\
        case matrix_element_type::Int8:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, int8_t, ret);\
            break;\
        case matrix_element_type::Int16:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, int16_t, ret);\
            break;\
        case matrix_element_type::Int32:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, int32_t, ret);\
            break;\
        case matrix_element_type::Int64:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, int64_t, ret);\
            break;\
        case matrix_element_type::Float:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, float, ret);\
            break;\
        case matrix_element_type::Double:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, double, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_cast_template_rgb_pixel(__TYPE__, desttype, matrix, templateRows, templateColumns, ret, err) \
do {\
    switch(desttype)\
    {\
        case matrix_element_type::RgbPixel:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, rgb_pixel, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_cast_template_bgr_pixel(__TYPE__, desttype, matrix, templateRows, templateColumns, ret, err) \
do {\
    switch(desttype)\
    {\
        case matrix_element_type::BgrPixel:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, bgr_pixel, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_cast_template_hsi_pixel(__TYPE__, desttype, matrix, templateRows, templateColumns, ret, err) \
do {\
    switch(desttype)\
    {\
        case matrix_element_type::HsiPixel:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, hsi_pixel, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_cast_template_rgb_alpha_pixel(__TYPE__, desttype, matrix, templateRows, templateColumns, ret, err) \
do {\
    switch(desttype)\
    {\
        case matrix_element_type::RgbAlphaPixel:\
            matrix_cast_template_sub(__TYPE__, matrix, templateRows, templateColumns, rgb_alpha_pixel, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define matrix_length_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::length(mat);\

#define matrix_length_squared_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::length_squared(mat);\

#define matrix_max_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::max(mat);\

#define matrix_min_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::min(mat);\

#define matrix_mean_op_std_vect_to_mat_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>, allocator<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>> op;\
op& mat = *static_cast<op*>(matrix);\
auto r = dlib::mean(mat);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(r);\

#define matrix_max_point_template(__TYPE__, error, type, ...) \
auto& mat_op = *static_cast<matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>*>(matrix);\
auto p = dlib::max_point(mat_op);\
*ret = new dlib::point(p);\

#define matrix_max_pointwise_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m1 = *static_cast<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix1);\
auto& m2 = *static_cast<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix2);\
auto p = dlib::max_pointwise(m1, m2);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(p);\

#define matrix_join_rows_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat1 = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix1);\
auto& mat2 = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix2);\
auto joinedMat = dlib::join_rows(mat1, mat2);\
*ret = new matrix_op<op_join_rows<matrix<__TYPE__, __ROWS__, __COLUMNS__>, matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(joinedMat);\

#define matrix_trans_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
auto transedMat = dlib::trans(mat);\
*ret = new matrix_op<op_trans<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(transedMat);\

#define fliplr_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
auto m = dlib::fliplr(mat);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);\

#pragma endregion template

DLLEXPORT void* linspace(double start, double end, int num)
{
    dlib::matrix<double> ret = dlib::linspace(start, end, num);
    return new dlib::matrix<double>(ret);
}

DLLEXPORT int matrix_cast(matrix_element_type type, void* matrix, int templateRows, int templateColumns, matrix_element_type desttype, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_cast_template(uint8_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt16:
            matrix_cast_template(uint16_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt32:
            matrix_cast_template(uint32_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::UInt64:
            matrix_cast_template(uint64_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int8:
            matrix_cast_template(int8_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int16:
            matrix_cast_template(int16_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int32:
            matrix_cast_template(int32_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Int64:
            matrix_cast_template(int64_t, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Float:
            matrix_cast_template(float, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::Double:
            matrix_cast_template(double, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbPixel:
            matrix_cast_template_rgb_pixel(rgb_pixel, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::BgrPixel:
            matrix_cast_template_bgr_pixel(bgr_pixel, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::HsiPixel:
            matrix_cast_template_hsi_pixel(hsi_pixel, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_cast_template_rgb_alpha_pixel(rgb_alpha_pixel, desttype, matrix, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_length(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_length_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

DLLEXPORT void matrix_length_dpoint(void* point, double* ret)
{
    dlib::dpoint& p = *static_cast<dlib::dpoint*>(point);
    *ret = dlib::length(p);
}

DLLEXPORT void matrix_length_point(void* point, int* ret)
{
    dlib::point& p = *static_cast<dlib::point*>(point);
    *ret = dlib::length(p);
}

DLLEXPORT int matrix_length_squared(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_length_squared_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

DLLEXPORT int matrix_join_rows(matrix_element_type type, void* matrix1, void* matrix2, int templateRows, int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_join_rows_template,
                    templateRows,
                    templateColumns,
                    matrix1,
                    matrix2,
                    ret);

    return error;
}

DLLEXPORT int matrix_max(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_max_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

DLLEXPORT int matrix_min(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_min_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

DLLEXPORT int matrix_max_point(array2d_type type, void* matrix, dlib::point** ret)
{
    int error = ERR_OK;

    array2d_numeric_template(type,
                             error,
                             matrix_max_point_template,
                             matrix,
                             ret);

    return error;
}

DLLEXPORT int matrix_max_pointwise_matrix(matrix_element_type type, void* matrix1, void* matrix2, int templateRows, int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_max_pointwise_matrix_template,
                            templateRows,
                            templateColumns,
                            matrix1,
                            matrix2,
                            ret);

    return error;
}

DLLEXPORT int matrix_mean(matrix_element_type type, void* matrix, int templateRows, int templateColumns, element_type opType, void** ret)
{
    int error = ERR_OK;

    switch(opType)
    {
        case element_type::OpStdVectToMat:
            matrix_numeric_template(type,
                                    error,
                                    matrix_template_size_template,
                                    matrix_mean_op_std_vect_to_mat_template,
                                    templateRows,
                                    templateColumns,
                                    matrix,
                                    ret);
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }

    return error;
}

DLLEXPORT int matrix_trans(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_trans_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

DLLEXPORT int fliplr(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    fliplr_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    ret);

    return error;
}

#endif