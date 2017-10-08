#ifndef _CPP_MATRIX_H_
#define _CPP_MATRIX_H_

#include <dlib/pixel.h>
#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix

extern "C" _declspec(dllexport) void* matrix_new(matrix_element_type type)
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

extern "C" _declspec(dllexport) void* matrix_new1(matrix_element_type type, int num_rows, int num_cols)
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

extern "C" _declspec(dllexport) int matrix_nc(matrix_element_type type, void* matrix, int* ret)
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

extern "C" _declspec(dllexport) int matrix_nr(matrix_element_type type, void* matrix, int* ret)
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

extern "C" _declspec(dllexport) int matrix_size(matrix_element_type type, void* matrix, int* ret)
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

extern "C" _declspec(dllexport) void matrix_delete(void* obj)
{
	delete obj;
}

extern "C" _declspec(dllexport) int matrix_operator_array(matrix_element_type type, void* matrix, void* array, int array_num)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            {
                dlib::matrix<uint8_t> tmp = *((dlib::matrix<uint8_t>*)matrix);
                tmp = *((uint8_t*)array);
            }
            break;
        case matrix_element_type::UInt16:
            {
                dlib::matrix<uint16_t> tmp = *((dlib::matrix<uint16_t>*)matrix);
                tmp = *((uint16_t*)array);
            }
            break;
        case matrix_element_type::UInt32:
            {
                dlib::matrix<uint32_t> tmp = *((dlib::matrix<uint32_t>*)matrix);
                tmp = *((uint32_t*)array);
            }
            break;
        case matrix_element_type::Int8:
            {
                dlib::matrix<int8_t> tmp = *((dlib::matrix<int8_t>*)matrix);
                tmp = *((int8_t*)array);
            }
            break;
        case matrix_element_type::Int16:
            {
                dlib::matrix<int16_t> tmp = *((dlib::matrix<int16_t>*)matrix);
                tmp = *((int16_t*)array);
            }
            break;
        case matrix_element_type::Int32:
            {
                dlib::matrix<int32_t> tmp = *((dlib::matrix<int32_t>*)matrix);
                tmp = *((int32_t*)array);
            }
            break;
        case matrix_element_type::Float:
            {
                dlib::matrix<float> tmp = *((dlib::matrix<float>*)matrix);
                tmp = *((float*)array);
            }
            break;
        case matrix_element_type::Double:
            {
                dlib::matrix<double> tmp = *((dlib::matrix<double>*)matrix);
                tmp = *((double*)array);
            }
            break;
        case matrix_element_type::RgbPixel:
            {
                dlib::matrix<rgb_pixel> tmp = *((dlib::matrix<rgb_pixel>*)matrix);
                tmp = *((rgb_pixel*)array);
            }
            break;
        case matrix_element_type::HsiPixel:
            {
                dlib::matrix<hsi_pixel> tmp = *((dlib::matrix<hsi_pixel>*)matrix);
                tmp = *((hsi_pixel*)array);
            }
            break;
        case matrix_element_type::RgbAlphaPixel:
            {
                dlib::matrix<rgb_alpha_pixel> tmp = *((dlib::matrix<rgb_alpha_pixel>*)matrix);
                tmp = *((rgb_alpha_pixel*)array);
            }
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma endregion matrix

#endif