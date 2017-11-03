#ifndef _CPP_ARRAY2D_H_
#define _CPP_ARRAY2D_H_

#include "../export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;

DLLEXPORT void* array2d_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array2d<uint8_t>();
        case array2d_type::UInt16:
			return new dlib::array2d<uint16_t>();
        case array2d_type::Int16:
			return new dlib::array2d<int16_t>();
        case array2d_type::Int32:
			return new dlib::array2d<int32_t>();
        case array2d_type::Float:
			return new dlib::array2d<float>();
        case array2d_type::Double:
			return new dlib::array2d<double>();
        case array2d_type::RgbPixel:
			return new dlib::array2d<rgb_pixel>();
        case array2d_type::HsiPixel:
			return new dlib::array2d<hsi_pixel>();
        case array2d_type::RgbAlphaPixel:
			return new dlib::array2d<rgb_alpha_pixel>();
        default:
			return nullptr;
    }
}

DLLEXPORT void* array2d_new1(array2d_type type, int rows, int cols)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array2d<uint8_t>(rows, cols);
        case array2d_type::UInt16:
			return new dlib::array2d<uint16_t>(rows, cols);
        case array2d_type::Int16:
			return new dlib::array2d<int16_t>(rows, cols);
        case array2d_type::Int32:
			return new dlib::array2d<int32_t>(rows, cols);
        case array2d_type::Float:
			return new dlib::array2d<float>(rows, cols);
        case array2d_type::Double:
			return new dlib::array2d<double>(rows, cols);
        case array2d_type::RgbPixel:
			return new dlib::array2d<rgb_pixel>(rows, cols);
        case array2d_type::HsiPixel:
			return new dlib::array2d<hsi_pixel>(rows, cols);
        case array2d_type::RgbAlphaPixel:
			return new dlib::array2d<rgb_alpha_pixel>(rows, cols);
        default:
			return nullptr;
    }
}

DLLEXPORT bool array2d_nc(array2d_type type, void* array, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->nc();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->nc();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->nc();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->nc();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->nc();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->nc();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->nc();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->nc();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->nc();
			return true;
        default:
			return false;
    }
}

DLLEXPORT bool array2d_nr(array2d_type type, void* array, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->nr();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->nr();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->nr();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->nr();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->nr();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->nr();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->nr();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->nr();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->nr();
			return true;
        default:
			return false;
    }
}

DLLEXPORT bool array2d_size(array2d_type type, void* array, uint64_t* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->size();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->size();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->size();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->size();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->size();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->size();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->size();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->size();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->size();
			return true;
        default:
			return false;
    }
}

DLLEXPORT void array2d_delete(array2d_type type, void* array)
{
    switch(type)
    {
        case array2d_type::UInt8:
			delete ((array2d<uint8_t>*)array);
			break;
        case array2d_type::UInt16:
			delete ((array2d<uint16_t>*)array);
			break;
        case array2d_type::Int16:
			delete ((array2d<int16_t>*)array);
			break;
        case array2d_type::Int32:
			delete ((array2d<int32_t>*)array);
			break;
        case array2d_type::Float:
			delete ((array2d<float>*)array);
			break;
        case array2d_type::Double:
			delete ((array2d<double>*)array);
			break;
        case array2d_type::RgbPixel:
			delete ((array2d<rgb_pixel>*)array);
			break;
        case array2d_type::HsiPixel:
			delete ((array2d<hsi_pixel>*)array);
			break;
        case array2d_type::RgbAlphaPixel:
			delete ((array2d<rgb_alpha_pixel>*)array);
			break;
    }
}

#pragma region row

DLLEXPORT int array2d_row(array2d_type type, void* array, int32_t row, void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto tmp = static_cast<dlib::array2d<uint8_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<uint8_t>::row(r);
            }
            break;
        case array2d_type::UInt16:
            {
                auto tmp = static_cast<dlib::array2d<uint16_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<uint16_t>::row(r);
            }
            break;
        case array2d_type::Int32:
            {
                auto tmp = static_cast<dlib::array2d<int32_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<int32_t>::row(r);
            }
            break;
        case array2d_type::Float:
            {
                auto tmp = static_cast<dlib::array2d<float>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<float>::row(r);
            }
            break;
        case array2d_type::Double:
            {
                auto tmp = static_cast<dlib::array2d<double>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<double>::row(r);
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<rgb_pixel>::row(r);
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto tmp = static_cast<dlib::array2d<hsi_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<hsi_pixel>::row(r);
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_alpha_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<rgb_alpha_pixel>::row(r);
            }
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region array2d_get_row_column

DLLEXPORT void array2d_get_row_column_uint8_t(void* row, int32_t column, uint8_t* ret)
{
    dlib::array2d<uint8_t>::row& tmp = *(static_cast<dlib::array2d<uint8_t>::row*>(row));
    *((uint8_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_uint16_t(void* row, int32_t column, uint16_t* ret)
{
    dlib::array2d<uint16_t>::row& tmp = *(static_cast<dlib::array2d<uint16_t>::row*>(row));
    *((uint16_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_int32_t(void* row, int32_t column, int32_t* ret)
{
    dlib::array2d<int32_t>::row& tmp = *(static_cast<dlib::array2d<int32_t>::row*>(row));
    *((int32_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_double(void* row, int32_t column, double* ret)
{
    dlib::array2d<double>::row& tmp = *(static_cast<dlib::array2d<double>::row*>(row));
    *((double*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_float(void* row, int32_t column, float* ret)
{
    dlib::array2d<float>::row& tmp = *(static_cast<dlib::array2d<float>::row*>(row));
    *((float*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_rgb_pixel(void* row, int32_t column, rgb_pixel* ret)
{
    dlib::array2d<rgb_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_pixel>::row*>(row));
    *((rgb_pixel*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_rgb_alpha_pixel(void* row, int32_t column, rgb_alpha_pixel* ret)
{
    dlib::array2d<rgb_alpha_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row));
    *((rgb_alpha_pixel*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_hsi_pixel(void* row, int32_t column, hsi_pixel* ret)
{
    dlib::array2d<hsi_pixel>::row& tmp = *(static_cast<dlib::array2d<hsi_pixel>::row*>(row));
    *((hsi_pixel*)ret) = tmp[column];
}

#pragma endregion array2d_get_row_column

#pragma region array2d_set_row_column

DLLEXPORT void array2d_set_row_column_uint8_t(void* row, int32_t column, uint8_t ret)
{
    dlib::array2d<uint8_t>::row& tmp = *(static_cast<dlib::array2d<uint8_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_uint16_t(void* row, int32_t column, uint16_t ret)
{
    dlib::array2d<uint16_t>::row& tmp = *(static_cast<dlib::array2d<uint16_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_int32_t(void* row, int32_t column, int32_t ret)
{
    dlib::array2d<int32_t>::row& tmp = *(static_cast<dlib::array2d<int32_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_double(void* row, int32_t column, double ret)
{
    dlib::array2d<double>::row& tmp = *(static_cast<dlib::array2d<double>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_float(void* row, int32_t column, float ret)
{
    dlib::array2d<float>::row& tmp = *(static_cast<dlib::array2d<float>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_rgb_pixel(void* row, int32_t column, rgb_pixel ret)
{
    dlib::array2d<rgb_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_pixel>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_rgb_alpha_pixel(void* row, int32_t column, rgb_alpha_pixel ret)
{
    dlib::array2d<rgb_alpha_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_hsi_pixel(void* row, int32_t column, hsi_pixel ret)
{
    dlib::array2d<hsi_pixel>::row& tmp = *(static_cast<dlib::array2d<hsi_pixel>::row*>(row));
    tmp[column] = ret;
}

#pragma endregion array2d_set_row_column

DLLEXPORT int array2d_row_delete(array2d_type type, void* row)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto tmp = static_cast<dlib::array2d<uint8_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::UInt16:
            {
                auto tmp = static_cast<dlib::array2d<uint16_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Int32:
            {
                auto tmp = static_cast<dlib::array2d<int32_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Float:
            {
                auto tmp = static_cast<dlib::array2d<float>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Double:
            {
                auto tmp = static_cast<dlib::array2d<double>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_pixel>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto tmp = static_cast<dlib::array2d<hsi_pixel>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row);
                delete tmp;
            }
            break;
    }

    return err;
}

#pragma endregion row

#pragma region matrix

DLLEXPORT void* array2d_matrix_new(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new dlib::array2d<matrix<uint8_t>>();
        case matrix_element_type::UInt16:
            return new dlib::array2d<matrix<uint16_t>>();
        case matrix_element_type::UInt32:
            return new dlib::array2d<matrix<uint32_t>>();
        case matrix_element_type::Int8:
            return new dlib::array2d<matrix<int8_t>>();
        case matrix_element_type::Int16:
            return new dlib::array2d<matrix<int16_t>>();
        case matrix_element_type::Int32:
            return new dlib::array2d<matrix<int32_t>>();
        case matrix_element_type::Float:
            return new dlib::array2d<matrix<float>>();
        case matrix_element_type::Double:
            return new dlib::array2d<matrix<double>>();
        case matrix_element_type::RgbPixel:
            return new dlib::array2d<matrix<rgb_pixel>>();
        case matrix_element_type::HsiPixel:
            return new dlib::array2d<matrix<hsi_pixel>>();
        case matrix_element_type::RgbAlphaPixel:
            return new dlib::array2d<matrix<rgb_alpha_pixel>>();
        default:
            return nullptr;
    }
}

DLLEXPORT void* array2d_matrix_new1(matrix_element_type type, int rows, int cols)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new dlib::array2d<matrix<uint8_t>>(rows, cols);
        case matrix_element_type::UInt16:
            return new dlib::array2d<matrix<uint16_t>>(rows, cols);
        case matrix_element_type::UInt32:
            return new dlib::array2d<matrix<uint32_t>>(rows, cols);
        case matrix_element_type::Int8:
            return new dlib::array2d<matrix<int8_t>>(rows, cols);
        case matrix_element_type::Int16:
            return new dlib::array2d<matrix<int16_t>>(rows, cols);
        case matrix_element_type::Int32:
            return new dlib::array2d<matrix<int32_t>>(rows, cols);
        case matrix_element_type::Float:
            return new dlib::array2d<matrix<float>>(rows, cols);
        case matrix_element_type::Double:
            return new dlib::array2d<matrix<double>>(rows, cols);
        case matrix_element_type::RgbPixel:
            return new dlib::array2d<matrix<rgb_pixel>>(rows, cols);
        case matrix_element_type::HsiPixel:
            return new dlib::array2d<matrix<hsi_pixel>>(rows, cols);
        case matrix_element_type::RgbAlphaPixel:
            return new dlib::array2d<matrix<rgb_alpha_pixel>>(rows, cols);
        default:
            return nullptr;
    }
}

DLLEXPORT bool array2d_matrix_nc(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::array2d<matrix<uint8_t>>*)array)->nc();
			return true;
        case matrix_element_type::UInt16:
            *ret = ((dlib::array2d<matrix<uint16_t>>*)array)->nc();
			return true;
        case matrix_element_type::UInt32:
            *ret = ((dlib::array2d<matrix<uint32_t>>*)array)->nc();
			return true;
        case matrix_element_type::Int8:
            *ret = ((dlib::array2d<matrix<int8_t>>*)array)->nc();
			return true;
        case matrix_element_type::Int16:
            *ret = ((dlib::array2d<matrix<int16_t>>*)array)->nc();
			return true;
        case matrix_element_type::Int32:
            *ret = ((dlib::array2d<matrix<int32_t>>*)array)->nc();
			return true;
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float>>*)array)->nc();
			return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double>>*)array)->nc();
			return true;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::array2d<matrix<rgb_pixel>>*)array)->nc();
			return true;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::array2d<matrix<hsi_pixel>>*)array)->nc();
			return true;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::array2d<matrix<rgb_alpha_pixel>>*)array)->nc();
			return true;
        default:
            return false;
    }
}

DLLEXPORT bool array2d_matrix_nr(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::array2d<matrix<uint8_t>>*)array)->nr();
			return true;
        case matrix_element_type::UInt16:
            *ret = ((dlib::array2d<matrix<uint16_t>>*)array)->nr();
			return true;
        case matrix_element_type::UInt32:
            *ret = ((dlib::array2d<matrix<uint32_t>>*)array)->nr();
			return true;
        case matrix_element_type::Int8:
            *ret = ((dlib::array2d<matrix<int8_t>>*)array)->nr();
			return true;
        case matrix_element_type::Int16:
            *ret = ((dlib::array2d<matrix<int16_t>>*)array)->nr();
			return true;
        case matrix_element_type::Int32:
            *ret = ((dlib::array2d<matrix<int32_t>>*)array)->nr();
			return true;
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float>>*)array)->nr();
			return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double>>*)array)->nr();
			return true;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::array2d<matrix<rgb_pixel>>*)array)->nr();
			return true;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::array2d<matrix<hsi_pixel>>*)array)->nr();
			return true;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::array2d<matrix<rgb_alpha_pixel>>*)array)->nr();
			return true;
        default:
            return false;
    }
}

DLLEXPORT bool array2d_matrix_size(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            *ret = ((dlib::array2d<matrix<uint8_t>>*)array)->size();
			return true;
        case matrix_element_type::UInt16:
            *ret = ((dlib::array2d<matrix<uint16_t>>*)array)->size();
			return true;
        case matrix_element_type::UInt32:
            *ret = ((dlib::array2d<matrix<uint32_t>>*)array)->size();
			return true;
        case matrix_element_type::Int8:
            *ret = ((dlib::array2d<matrix<int8_t>>*)array)->size();
			return true;
        case matrix_element_type::Int16:
            *ret = ((dlib::array2d<matrix<int16_t>>*)array)->size();
			return true;
        case matrix_element_type::Int32:
            *ret = ((dlib::array2d<matrix<int32_t>>*)array)->size();
			return true;
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float>>*)array)->size();
			return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double>>*)array)->size();
			return true;
        case matrix_element_type::RgbPixel:
            *ret = ((dlib::array2d<matrix<rgb_pixel>>*)array)->size();
			return true;
        case matrix_element_type::HsiPixel:
            *ret = ((dlib::array2d<matrix<hsi_pixel>>*)array)->size();
			return true;
        case matrix_element_type::RgbAlphaPixel:
            *ret = ((dlib::array2d<matrix<rgb_alpha_pixel>>*)array)->size();
			return true;
        default:
            return false;
    }
}

DLLEXPORT void array2d_matrix_delete(matrix_element_type type, void* array)
{
    // switch(type)
    // {
    //     case matrix_element_type::UInt8:
    //         delete ((dlib::array2d<matrix<uint8_t>>*)array);
    //         break;
    //     case matrix_element_type::UInt16:
    //         delete ((dlib::array2d<matrix<uint16_t>>*)array);
    //         break;
    //     case matrix_element_type::UInt32:
    //         delete ((dlib::array2d<matrix<uint32_t>>*)array);
    //         break;
    //     case matrix_element_type::Int8:
    //         delete ((dlib::array2d<matrix<int8_t>>*)array);
    //         break;
    //     case matrix_element_type::Int16:
    //         delete ((dlib::array2d<matrix<int16_t>>*)array);
    //         break;
    //     case matrix_element_type::Int32:
    //         delete ((dlib::array2d<matrix<int32_t>>*)array);
    //         break;
    //     case matrix_element_type::Float:
    //         delete ((dlib::array2d<matrix<float>>*)array);
    //         break;
    //     case matrix_element_type::Double:
    //         delete ((dlib::array2d<matrix<double>>*)array);
    //         break;
    //     case matrix_element_type::RgbPixel:
    //         delete ((dlib::array2d<matrix<rgb_pixel>>*)array);
    //         break;
    //     case matrix_element_type::HsiPixel:
    //         delete ((dlib::array2d<matrix<hsi_pixel>>*)array);
    //         break;
    //     case matrix_element_type::RgbAlphaPixel:
    //         delete ((dlib::array2d<matrix<rgb_alpha_pixel>>*)array);
    //         break;
    // }
    // dlib::array2d is template type.
    // Ex. dlib::array2d<dlib::matrix<float, 31, 1>> does NOT equal to dlib::array2d<dlib::matrix<float>>
    // Template argument is decided in compile time rather than runtime.
    // So we can not specify template argument as variable.
    // In other words, we have to call delete for void* because we do not known exact type.
    // Unfortunately, dlib::matrix implements destructor.
    // What should we do?
    delete array;
}

#pragma region row



#pragma endregion row

#pragma endregion matrix

#endif