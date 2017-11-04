#ifndef _CPP_MATRIX_H_
#define _CPP_MATRIX_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define matrix_operator_array_template(matrix, array) \
do { \
    dlib::matrix<ELEMENT>& mat = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
    ELEMENT* src = static_cast<ELEMENT*>(array);\
    const long row = mat.nr();\
    const long column = mat.nc();\
    for (long r = 0; r < row; ++r)\
        for (long c = 0; c < column; ++c)\
            mat(r, c) = src[r * column + c];\
} while (0)

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
        case matrix_element_type::Int8:
            return new matrix<int8_t>();
        case matrix_element_type::Int16:
            return new matrix<int16_t>();
        case matrix_element_type::Int32:
            return new matrix<int32_t>();
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
        case matrix_element_type::Int8:
            return new matrix<int8_t>(num_rows, num_cols);
        case matrix_element_type::Int16:
            return new matrix<int16_t>(num_rows, num_cols);
        case matrix_element_type::Int32:
            return new matrix<int32_t>(num_rows, num_cols);
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

DLLEXPORT int matrix_nc(matrix_element_type type, void* matrix, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::matrix<uint8_t>*)matrix)->nc();
            break;
        case matrix_element_type::UInt16:
            *ret = ((dlib::matrix<uint16_t>*)matrix)->nc();
            break;
        case matrix_element_type::UInt32:
            *ret = ((dlib::matrix<uint32_t>*)matrix)->nc();
            break;
        case matrix_element_type::Int8:
            *ret = ((dlib::matrix<int8_t>*)matrix)->nc();
            break;
        case matrix_element_type::Int16:
            *ret = ((dlib::matrix<int16_t>*)matrix)->nc();
            break;
        case matrix_element_type::Int32:
            *ret = ((dlib::matrix<int32_t>*)matrix)->nc();
            break;
        case matrix_element_type::Float:
            *ret = ((dlib::matrix<float>*)matrix)->nc();
            break;
        case matrix_element_type::Double:
            *ret = ((dlib::matrix<double>*)matrix)->nc();
            break;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::matrix<rgb_pixel>*)matrix)->nc();
            break;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::matrix<hsi_pixel>*)matrix)->nc();
            break;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::matrix<rgb_alpha_pixel>*)matrix)->nc();
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int matrix_nr(matrix_element_type type, void* matrix, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::matrix<uint8_t>*)matrix)->nr();
            break;
        case matrix_element_type::UInt16:
            *ret = ((dlib::matrix<uint16_t>*)matrix)->nr();
            break;
        case matrix_element_type::UInt32:
            *ret = ((dlib::matrix<uint32_t>*)matrix)->nr();
            break;
        case matrix_element_type::Int8:
            *ret = ((dlib::matrix<int8_t>*)matrix)->nr();
            break;
        case matrix_element_type::Int16:
            *ret = ((dlib::matrix<int16_t>*)matrix)->nr();
            break;
        case matrix_element_type::Int32:
            *ret = ((dlib::matrix<int32_t>*)matrix)->nr();
            break;
        case matrix_element_type::Float:
            *ret = ((dlib::matrix<float>*)matrix)->nr();
            break;
        case matrix_element_type::Double:
            *ret = ((dlib::matrix<double>*)matrix)->nr();
            break;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::matrix<rgb_pixel>*)matrix)->nr();
            break;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::matrix<hsi_pixel>*)matrix)->nr();
            break;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::matrix<rgb_alpha_pixel>*)matrix)->nr();
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int matrix_size(matrix_element_type type, void* matrix, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::matrix<uint8_t>*)matrix)->size();
            break;
        case matrix_element_type::UInt16:
            *ret = ((dlib::matrix<uint16_t>*)matrix)->size();
            break;
        case matrix_element_type::UInt32:
            *ret = ((dlib::matrix<uint32_t>*)matrix)->size();
            break;
        case matrix_element_type::Int8:
            *ret = ((dlib::matrix<int8_t>*)matrix)->size();
            break;
        case matrix_element_type::Int16:
            *ret = ((dlib::matrix<int16_t>*)matrix)->size();
            break;
        case matrix_element_type::Int32:
            *ret = ((dlib::matrix<int32_t>*)matrix)->size();
            break;
        case matrix_element_type::Float:
            *ret = ((dlib::matrix<float>*)matrix)->size();
            break;
        case matrix_element_type::Double:
            *ret = ((dlib::matrix<double>*)matrix)->size();
            break;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::matrix<rgb_pixel>*)matrix)->size();
            break;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::matrix<hsi_pixel>*)matrix)->size();
            break;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::matrix<rgb_alpha_pixel>*)matrix)->size();
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT void matrix_delete(matrix_element_type type, void* matrix)
{
    // switch(type)
    // {
    //     case matrix_element_type::UInt8:
    //         delete ((dlib::matrix<uint8_t>*)matrix);
    //         break;
    //     case matrix_element_type::UInt16:
    //         delete ((dlib::matrix<uint16_t>*)matrix);
    //         break;
    //     case matrix_element_type::UInt32:
    //         delete ((dlib::matrix<uint32_t>*)matrix);
    //         break;
    //     case matrix_element_type::Int8:
    //         delete ((dlib::matrix<int8_t>*)matrix);
    //         break;
    //     case matrix_element_type::Int16:
    //         delete ((dlib::matrix<int16_t>*)matrix);
    //         break;
    //     case matrix_element_type::Int32:
    //         delete ((dlib::matrix<int32_t>*)matrix);
    //         break;
    //     case matrix_element_type::Float:
    //         delete ((dlib::matrix<float>*)matrix);
    //         break;
    //     case matrix_element_type::Double:
    //         delete ((dlib::matrix<double>*)matrix);
    //         break;
    //     case matrix_element_type::RgbPixel:
    //         delete ((dlib::matrix<rgb_pixel>*)matrix);
    //         break;
    //     case matrix_element_type::HsiPixel:
    //         delete ((dlib::matrix<hsi_pixel>*)matrix);
    //         break;
    //     case matrix_element_type::RgbAlphaPixel:
    //         delete ((dlib::matrix<rgb_alpha_pixel>*)matrix);
    //         break;
    // }
    // dlib::matrix is template type.
    // Ex. dlib::matrix<float, 31, 1> does NOT equal to dlib::matrix<float>
    // Template argument is decided in compile time rather than runtime.
    // So we can not specify template argument as variable.
    // In other words, we have to call delete for void* because we do not known exact type.
    // Fortunately, dlib::matrix does not implement destructor.
    // So it is could be no problem when delete void* and destructor is not called.
    delete matrix;
}

DLLEXPORT int matrix_operator_array(matrix_element_type type, void* matrix, void* array)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma region 

#pragma region matrix_operator_get_one_row_column

DLLEXPORT void matrix_operator_get_one_row_column_uint8_t(void* matrix, int index, uint8_t* ret)
{
    dlib::matrix<uint8_t>& tmp = *(static_cast<dlib::matrix<uint8_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_uint16_t(void* matrix, int index, uint16_t* ret)
{
    dlib::matrix<uint16_t>& tmp = *(static_cast<dlib::matrix<uint16_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_uint32_t(void* matrix, int index, uint32_t* ret)
{
    dlib::matrix<uint32_t>& tmp = *(static_cast<dlib::matrix<uint32_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_int8_t(void* matrix, int index, int8_t* ret)
{
    dlib::matrix<int8_t>& tmp = *(static_cast<dlib::matrix<int8_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_int16_t(void* matrix, int index, int16_t* ret)
{
    dlib::matrix<int16_t>& tmp = *(static_cast<dlib::matrix<int16_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_int32_t(void* matrix, int index, int32_t* ret)
{
    dlib::matrix<int32_t>& tmp = *(static_cast<dlib::matrix<int32_t>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_double(void* matrix, int index, double* ret)
{
    dlib::matrix<double>& tmp = *(static_cast<dlib::matrix<double>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_float(void* matrix, int index, float* ret)
{
    dlib::matrix<float>& tmp = *(static_cast<dlib::matrix<float>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_rgb_pixel(void* matrix, int index, rgb_pixel* ret)
{
    dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_rgb_alpha_pixel(void* matrix, int index, rgb_alpha_pixel* ret)
{
    dlib::matrix<rgb_alpha_pixel>& tmp = *(static_cast<dlib::matrix<rgb_alpha_pixel>*>(matrix));
    *ret = tmp(index);
}

DLLEXPORT void matrix_operator_get_one_row_column_hsi_pixel(void* matrix, int index, hsi_pixel* ret)
{
    dlib::matrix<hsi_pixel>& tmp = *(static_cast<dlib::matrix<hsi_pixel>*>(matrix));
    *ret = tmp(index);
}

#pragma endregion

#pragma region matrix_operator_set_one_row_column

DLLEXPORT void matrix_operator_set_one_row_column_uint8_t(void* matrix, int index, uint8_t value)
{
    dlib::matrix<uint8_t>& tmp = *(static_cast<dlib::matrix<uint8_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_uint16_t(void* matrix, int index, uint16_t value)
{
    dlib::matrix<uint16_t>& tmp = *(static_cast<dlib::matrix<uint16_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_uint32_t(void* matrix, int index, uint32_t value)
{
    dlib::matrix<uint32_t>& tmp = *(static_cast<dlib::matrix<uint32_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_int8_t(void* matrix, int index, int8_t value)
{
    dlib::matrix<int8_t>& tmp = *(static_cast<dlib::matrix<int8_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_int16_t(void* matrix, int index, int16_t value)
{
    dlib::matrix<int16_t>& tmp = *(static_cast<dlib::matrix<int16_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_int32_t(void* matrix, int index, int32_t value)
{
    dlib::matrix<int32_t>& tmp = *(static_cast<dlib::matrix<int32_t>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_double(void* matrix, int index, double value)
{
    dlib::matrix<double>& tmp = *(static_cast<dlib::matrix<double>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_float(void* matrix, int index, float value)
{
    dlib::matrix<float>& tmp = *(static_cast<dlib::matrix<float>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_rgb_pixel(void* matrix, int index, rgb_pixel value)
{

    dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_rgb_alpha_pixel(void* matrix, int index, rgb_alpha_pixel value)
{
    dlib::matrix<rgb_alpha_pixel>& tmp = *(static_cast<dlib::matrix<rgb_alpha_pixel>*>(matrix));
    tmp(index) = value;
}

DLLEXPORT void matrix_operator_set_one_row_column_hsi_pixel(void* matrix, int index, hsi_pixel value)
{
    dlib::matrix<hsi_pixel>& tmp = *(static_cast<dlib::matrix<hsi_pixel>*>(matrix));
    tmp(index) = value;
}

#pragma endregion

#pragma region matrix_operator_get_row_column

DLLEXPORT void matrix_operator_get_row_column_uint8_t(void* matrix, int row, int column, uint8_t* ret)
{
    dlib::matrix<uint8_t>& tmp = *(static_cast<dlib::matrix<uint8_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_uint16_t(void* matrix, int row, int column, uint16_t* ret)
{
    dlib::matrix<uint16_t>& tmp = *(static_cast<dlib::matrix<uint16_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_uint32_t(void* matrix, int row, int column, uint32_t* ret)
{
    dlib::matrix<uint32_t>& tmp = *(static_cast<dlib::matrix<uint32_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_int8_t(void* matrix, int row, int column, int8_t* ret)
{
    dlib::matrix<int8_t>& tmp = *(static_cast<dlib::matrix<int8_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_int16_t(void* matrix, int row, int column, int16_t* ret)
{
    dlib::matrix<int16_t>& tmp = *(static_cast<dlib::matrix<int16_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_int32_t(void* matrix, int row, int column, int32_t* ret)
{
    dlib::matrix<int32_t>& tmp = *(static_cast<dlib::matrix<int32_t>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_double(void* matrix, int row, int column, double* ret)
{
    dlib::matrix<double>& tmp = *(static_cast<dlib::matrix<double>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_float(void* matrix, int row, int column, float* ret)
{
    dlib::matrix<float>& tmp = *(static_cast<dlib::matrix<float>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_rgb_pixel(void* matrix, int row, int column, rgb_pixel* ret)
{
    dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_rgb_alpha_pixel(void* matrix, int row, int column, rgb_alpha_pixel* ret)
{
    dlib::matrix<rgb_alpha_pixel>& tmp = *(static_cast<dlib::matrix<rgb_alpha_pixel>*>(matrix));
    *ret = tmp(row, column);
}

DLLEXPORT void matrix_operator_get_row_column_hsi_pixel(void* matrix, int row, int column, hsi_pixel* ret)
{
    dlib::matrix<hsi_pixel>& tmp = *(static_cast<dlib::matrix<hsi_pixel>*>(matrix));
    *ret = tmp(row, column);
}

#pragma endregion

#pragma region matrix_operator_set_row_column

DLLEXPORT void matrix_operator_set_row_column_uint8_t(void* matrix, int row, int column, uint8_t value)
{
    dlib::matrix<uint8_t>& tmp = *(static_cast<dlib::matrix<uint8_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_uint16_t(void* matrix, int row, int column, uint16_t value)
{
    dlib::matrix<uint16_t>& tmp = *(static_cast<dlib::matrix<uint16_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_uint32_t(void* matrix, int row, int column, uint32_t value)
{
    dlib::matrix<uint32_t>& tmp = *(static_cast<dlib::matrix<uint32_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_int8_t(void* matrix, int row, int column, int8_t value)
{
    dlib::matrix<int8_t>& tmp = *(static_cast<dlib::matrix<int8_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_int16_t(void* matrix, int row, int column, int16_t value)
{
    dlib::matrix<int16_t>& tmp = *(static_cast<dlib::matrix<int16_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_int32_t(void* matrix, int row, int column, int32_t value)
{
    dlib::matrix<int32_t>& tmp = *(static_cast<dlib::matrix<int32_t>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_double(void* matrix, int row, int column, double value)
{
    dlib::matrix<double>& tmp = *(static_cast<dlib::matrix<double>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_float(void* matrix, int row, int column, float value)
{
    dlib::matrix<float>& tmp = *(static_cast<dlib::matrix<float>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_rgb_pixel(void* matrix, int row, int column, rgb_pixel value)
{
    
    dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_rgb_alpha_pixel(void* matrix, int row, int column, rgb_alpha_pixel value)
{
    dlib::matrix<rgb_alpha_pixel>& tmp = *(static_cast<dlib::matrix<rgb_alpha_pixel>*>(matrix));
    tmp(row, column) = value;
}

DLLEXPORT void matrix_operator_set_row_column_hsi_pixel(void* matrix, int row, int column, hsi_pixel value)
{
    dlib::matrix<hsi_pixel>& tmp = *(static_cast<dlib::matrix<hsi_pixel>*>(matrix));
    tmp(row, column) = value;
}

#pragma endregion

#pragma endregion

DLLEXPORT int matrix_operator_left_shift(matrix_element_type type, void* matrix, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            {
                dlib::matrix<uint8_t>& mat = *(static_cast<dlib::matrix<uint8_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::UInt16:
            {
                dlib::matrix<uint16_t>& mat = *(static_cast<dlib::matrix<uint16_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::UInt32:
            {
                dlib::matrix<uint32_t>& mat = *(static_cast<dlib::matrix<uint32_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::Int8:
            {
                dlib::matrix<int8_t>& mat = *(static_cast<dlib::matrix<int8_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::Int16:
            {
                dlib::matrix<int16_t>& mat = *(static_cast<dlib::matrix<int16_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::Int32:
            {
                dlib::matrix<int32_t>& mat = *(static_cast<dlib::matrix<int32_t>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::Float:
            {
                dlib::matrix<float>& mat = *(static_cast<dlib::matrix<float>*>(matrix));
                *stream << mat;
            }
            break;
        case matrix_element_type::Double:
            {
                dlib::matrix<double>& mat = *(static_cast<dlib::matrix<double>*>(matrix));
                *stream << mat;
            }
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

#pragma endregion matrix

#endif