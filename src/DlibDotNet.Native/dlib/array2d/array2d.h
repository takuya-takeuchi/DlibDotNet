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
    switch(type)
    {
        case matrix_element_type::UInt8:
            delete ((dlib::array2d<matrix<uint8_t>>*)array);
            break;
        case matrix_element_type::UInt16:
            delete ((dlib::array2d<matrix<uint16_t>>*)array);
            break;
        case matrix_element_type::UInt32:
            delete ((dlib::array2d<matrix<uint32_t>>*)array);
            break;
        case matrix_element_type::Int8:
            delete ((dlib::array2d<matrix<int8_t>>*)array);
            break;
        case matrix_element_type::Int16:
            delete ((dlib::array2d<matrix<int16_t>>*)array);
            break;
        case matrix_element_type::Int32:
            delete ((dlib::array2d<matrix<int32_t>>*)array);
            break;
        case matrix_element_type::Float:
            delete ((dlib::array2d<matrix<float>>*)array);
            break;
        case matrix_element_type::Double:
            delete ((dlib::array2d<matrix<double>>*)array);
            break;
        case matrix_element_type::RgbPixel:
            delete ((dlib::array2d<matrix<rgb_pixel>>*)array);
            break;
        case matrix_element_type::HsiPixel:
            delete ((dlib::array2d<matrix<hsi_pixel>>*)array);
            break;
        case matrix_element_type::RgbAlphaPixel:
            delete ((dlib::array2d<matrix<rgb_alpha_pixel>>*)array);
            break;
    }
}

#pragma endregion matrix

#endif