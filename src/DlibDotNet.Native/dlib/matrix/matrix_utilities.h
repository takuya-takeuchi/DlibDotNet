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

#define matrix_length_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::length(mat);\

#define matrix_length_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_length_template_sub, error, matrix, ret);\
} while (0)

#define matrix_length_squared_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::length_squared(mat);\

#define matrix_length_squared_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_length_squared_template_sub, error, matrix, ret);\
} while (0)

#define matrix_max_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::max(mat);\

#define matrix_max_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_max_template_sub, error, matrix, ret);\
} while (0)

#define matrix_min_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
*((__TYPE__*)ret) = dlib::min(mat);\

#define matrix_min_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_min_template_sub, error, matrix, ret);\
} while (0)

#define matrix_mean_op_std_vect_to_mat_template(type, matrix, templateRows, templateColumns, ret, err) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        switch(type)\
        {\
            case matrix_element_type::UInt8:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint8_t>, allocator<dlib::matrix<uint8_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint8_t>(r);\
                }\
                break;\
            case matrix_element_type::UInt16:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint16_t>, allocator<dlib::matrix<uint16_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint16_t>(r);\
                }\
                break;\
            case matrix_element_type::UInt32:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint32_t>, allocator<dlib::matrix<uint32_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint32_t>(r);\
                }\
                break;\
            case matrix_element_type::Int8:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int8_t>, allocator<dlib::matrix<int8_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int8_t>(r);\
                }\
                break;\
            case matrix_element_type::Int16:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int16_t>, allocator<dlib::matrix<int16_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int16_t>(r);\
                }\
                break;\
            case matrix_element_type::Int32:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int32_t>, allocator<dlib::matrix<int32_t>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int32_t>(r);\
                }\
                break;\
            case matrix_element_type::Float:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<float>, allocator<dlib::matrix<float>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<float>(r);\
                }\
                break;\
            case matrix_element_type::Double:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<double>, allocator<dlib::matrix<double>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<double>(r);\
                }\
                break;\
            case matrix_element_type::RgbPixel:\
            case matrix_element_type::HsiPixel:\
            case matrix_element_type::RgbAlphaPixel:\
            default:\
                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
                break;\
        }\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        switch(type)\
        {\
            case matrix_element_type::UInt8:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint8_t, 0, 1>, allocator<dlib::matrix<uint8_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint8_t>(r);\
                }\
                break;\
            case matrix_element_type::UInt16:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint16_t, 0, 1>, allocator<dlib::matrix<uint16_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint16_t>(r);\
                }\
                break;\
            case matrix_element_type::UInt32:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<uint32_t, 0, 1>, allocator<dlib::matrix<uint32_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<uint32_t>(r);\
                }\
                break;\
            case matrix_element_type::Int8:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int8_t, 0, 1>, allocator<dlib::matrix<int8_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int8_t>(r);\
                }\
                break;\
            case matrix_element_type::Int16:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int16_t, 0, 1>, allocator<dlib::matrix<int16_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int16_t>(r);\
                }\
                break;\
            case matrix_element_type::Int32:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<int32_t, 0, 1>, allocator<dlib::matrix<int32_t, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<int32_t>(r);\
                }\
                break;\
            case matrix_element_type::Float:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<float, 0, 1>, allocator<dlib::matrix<float, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<float>(r);\
                }\
                break;\
            case matrix_element_type::Double:\
                {\
                    typedef dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<double, 0, 1>, allocator<dlib::matrix<double, 0, 1>>>>> op;\
                    op& mat = *static_cast<op*>(matrix);\
                    auto r = dlib::mean(mat);\
                    *ret = new dlib::matrix<double>(r);\
                }\
                break;\
            case matrix_element_type::RgbPixel:\
            case matrix_element_type::HsiPixel:\
            case matrix_element_type::RgbAlphaPixel:\
            default:\
                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
                break;\
        }\
    }\
} while (0)

#define matrix_max_point_template(__TYPE__, error, type, ...) \
auto& mat_op = *static_cast<matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>*>(matrix);\
auto p = dlib::max_point(mat_op);\
*ret = new dlib::point(p);\

#define matrix_max_pointwise_matrix_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix1, matrix2, ret) \
matrix<__TYPE__, __ROWS__, __COLUMNS__>& m1 = *static_cast<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix1);\
matrix<__TYPE__, __ROWS__, __COLUMNS__>& m2 = *static_cast<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix2);\
auto p = dlib::max_pointwise(m1, m2);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(p);\

#define matrix_max_pointwise_matrix_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix1, matrix2, ret) \
do {\
    matrix_template_size_arg3_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_max_pointwise_matrix_template_sub, error, matrix1, matrix2, ret);\
} while (0)

#define matrix_join_rows_template(__TYPE__, matrix1, matrix2, templateRows, templateColumns, ret)\
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<__TYPE__>& mat1 = *static_cast<dlib::matrix<__TYPE__>*>(matrix1);\
        dlib::matrix<__TYPE__>& mat2 = *static_cast<dlib::matrix<__TYPE__>*>(matrix2);\
        auto joinedMat = dlib::join_rows(mat1, mat2);\
        *ret = new matrix_op<op_join_rows<matrix<__TYPE__>, matrix<__TYPE__>>>(joinedMat);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 0, 1>& mat1 = *static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix1);\
        dlib::matrix<__TYPE__, 0, 1>& mat2 = *static_cast<dlib::matrix<__TYPE__, 0, 1>*>(matrix2);\
        auto joinedMat = dlib::join_rows(mat1, mat2);\
        *ret = new matrix_op<op_join_rows<matrix<__TYPE__, 0, 1>, matrix<__TYPE__, 0, 1>>>(joinedMat);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<__TYPE__, 31, 1>& mat1 = *static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix1);\
        dlib::matrix<__TYPE__, 31, 1>& mat2 = *static_cast<dlib::matrix<__TYPE__, 31, 1>*>(matrix2);\
        auto joinedMat = dlib::join_rows(mat1, mat2);\
        *ret = new matrix_op<op_join_rows<matrix<__TYPE__, 31, 1>, matrix<__TYPE__, 31, 1>>>(joinedMat);\
    }\
} while (0)

#define matrix_trans_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
auto transedMat = dlib::trans(mat);\
*ret = new matrix_op<op_trans<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(transedMat);\

#define matrix_trans_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_trans_template_sub, error, matrix, ret);\
} while (0)

#define fliplr_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
auto m = dlib::fliplr(mat);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);\

#define fliplr_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, fliplr_template_sub, error, matrix, ret);\
} while (0)

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
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_length_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_length_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_length_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_length_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_length_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_length_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_length_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_length_template(double, templateRows, templateColumns, err, matrix, ret);
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
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_length_squared_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_length_squared_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_length_squared_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_length_squared_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_length_squared_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_length_squared_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_length_squared_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_length_squared_template(double, templateRows, templateColumns, err, matrix, ret);
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

DLLEXPORT int matrix_join_rows(matrix_element_type type, void* matrix1, void* matrix2, int templateRows, int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_join_rows_template(uint8_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_join_rows_template(uint16_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_join_rows_template(uint32_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int8:
            matrix_join_rows_template(int8_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int16:
            matrix_join_rows_template(int16_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Int32:
            matrix_join_rows_template(int32_t, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Float:
            matrix_join_rows_template(float, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::Double:
            matrix_join_rows_template(double, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbPixel:
            matrix_join_rows_template(rgb_pixel, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::HsiPixel:
            matrix_join_rows_template(hsi_pixel, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            matrix_join_rows_template(rgb_alpha_pixel, matrix1, matrix2, templateRows, templateColumns, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_max(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_max_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_max_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_max_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_max_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_max_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_max_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_max_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_max_template(double, templateRows, templateColumns, err, matrix, ret);
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

DLLEXPORT int matrix_min(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_min_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_min_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_min_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_min_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_min_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_min_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_min_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_min_template(double, templateRows, templateColumns, err, matrix, ret);
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

DLLEXPORT int matrix_max_point(matrix_element_type type, void* matrix, dlib::point** ret)
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
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_max_pointwise_matrix_template(uint8_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_max_pointwise_matrix_template(uint16_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_max_pointwise_matrix_template(uint32_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::Int8:
            matrix_max_pointwise_matrix_template(int8_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::Int16:
            matrix_max_pointwise_matrix_template(int16_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::Int32:
            matrix_max_pointwise_matrix_template(int32_t, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::Float:
            matrix_max_pointwise_matrix_template(float, templateRows, templateColumns, err, matrix1, matrix2, ret);
            break;
        case matrix_element_type::Double:
            matrix_max_pointwise_matrix_template(double, templateRows, templateColumns, err, matrix1, matrix2, ret);
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

DLLEXPORT int matrix_mean(matrix_element_type type, void* matrix, int templateRows, int templateColumns, element_type opType, void** ret)
{
    int err = ERR_OK;
    switch(opType)
    {
        case element_type::OpStdVectToMat:
            matrix_mean_op_std_vect_to_mat_template(type, matrix, templateRows, templateColumns, ret, err);
            break;
        default:
            err = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_trans(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            matrix_trans_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_trans_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            matrix_trans_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            matrix_trans_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            matrix_trans_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            matrix_trans_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            matrix_trans_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            matrix_trans_template(double, templateRows, templateColumns, err, matrix, ret);
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

DLLEXPORT int fliplr(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            fliplr_template(uint8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            fliplr_template(uint16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::UInt32:
            fliplr_template(uint32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int8:
            fliplr_template(int8_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int16:
            fliplr_template(int16_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Int32:
            fliplr_template(int32_t, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Float:
            fliplr_template(float, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::Double:
            fliplr_template(double, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbPixel:
            fliplr_template(rgb_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::HsiPixel:
            fliplr_template(hsi_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        case matrix_element_type::RgbAlphaPixel:
            fliplr_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, ret);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif