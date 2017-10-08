#ifndef _CPP_ARRAY_H_
#define _CPP_ARRAY_H_

#include <dlib/array.h>
#include <dlib/array2d.h>
#include "../shared.h"

using namespace dlib;

extern "C" __declspec(dllexport) void* array_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<uint8_t>();
        case array2d_type::UInt16:
			return new dlib::array<uint16_t>();
        case array2d_type::Float:
			return new dlib::array<float>();
        case array2d_type::Double:
			return new dlib::array<double>();
        case array2d_type::RgbPixel:
			return new dlib::array<rgb_pixel>();
        case array2d_type::HsiPixel:
			return new dlib::array<hsi_pixel>();
        case array2d_type::RgbAlphaPixel:
			return new dlib::array<rgb_alpha_pixel>();
        default:
			return nullptr;
    }
}

extern "C" __declspec(dllexport) void* array_new1(array2d_type type, uint32_t new_size)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<uint8_t>(new_size);
        case array2d_type::UInt16:
			return new dlib::array<uint16_t>(new_size);
        case array2d_type::Float:
			return new dlib::array<float>(new_size);
        case array2d_type::Double:
			return new dlib::array<double>(new_size);
        case array2d_type::RgbPixel:
			return new dlib::array<rgb_pixel>(new_size);
        case array2d_type::HsiPixel:
			return new dlib::array<hsi_pixel>(new_size);
        case array2d_type::RgbAlphaPixel:
			return new dlib::array<rgb_alpha_pixel>(new_size);
        default:
			return nullptr;
    }
}

extern "C" __declspec(dllexport) void* array_array2d_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<array2d<uint8_t>>();
        case array2d_type::UInt16:
			return new dlib::array<array2d<uint16_t>>();
        case array2d_type::Float:
			return new dlib::array<array2d<float>>();
        case array2d_type::Double:
			return new dlib::array<array2d<double>>();
        case array2d_type::RgbPixel:
			return new dlib::array<array2d<rgb_pixel>>();
        case array2d_type::HsiPixel:
			return new dlib::array<array2d<hsi_pixel>>();
        case array2d_type::RgbAlphaPixel:
			return new dlib::array<array2d<rgb_alpha_pixel>>();
        default:
			return nullptr;
    }
}

extern "C" __declspec(dllexport) void* array_array2d_new1(array2d_type type, uint32_t new_size)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<array2d<uint8_t>>(new_size);
        case array2d_type::UInt16:
			return new dlib::array<array2d<uint16_t>>(new_size);
        case array2d_type::Float:
			return new dlib::array<array2d<float>>(new_size);
        case array2d_type::Double:
			return new dlib::array<array2d<double>>(new_size);
        case array2d_type::RgbPixel:
			return new dlib::array<array2d<rgb_pixel>>(new_size);
        case array2d_type::HsiPixel:
			return new dlib::array<array2d<hsi_pixel>>(new_size);
        case array2d_type::RgbAlphaPixel:
			return new dlib::array<array2d<rgb_alpha_pixel>>(new_size);
        default:
			return nullptr;
    }
}

extern "C" __declspec(dllexport) void* array_matrix_new(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
			return new dlib::array<matrix<uint8_t>>();
        case matrix_element_type::UInt16:
			return new dlib::array<matrix<uint16_t>>();
        case matrix_element_type::UInt32:
			return new dlib::array<matrix<uint32_t>>();
        case matrix_element_type::Int8:
			return new dlib::array<matrix<int8_t>>();
        case matrix_element_type::Int16:
			return new dlib::array<matrix<int16_t>>();
        case matrix_element_type::Int32:
			return new dlib::array<matrix<int32_t>>();
        case matrix_element_type::Float:
			return new dlib::array<matrix<float>>();
        case matrix_element_type::Double:
			return new dlib::array<matrix<double>>();
        case matrix_element_type::RgbPixel:
			return new dlib::array<matrix<rgb_pixel>>();
        case matrix_element_type::HsiPixel:
			return new dlib::array<matrix<hsi_pixel>>();
        case matrix_element_type::RgbAlphaPixel:
			return new dlib::array<matrix<rgb_alpha_pixel>>();
        default:
            return nullptr;
    }
}

extern "C" __declspec(dllexport) void* array_matrix_new1(matrix_element_type type, uint32_t new_size)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
			return new dlib::array<matrix<uint8_t>>(new_size);
        case matrix_element_type::UInt16:
			return new dlib::array<matrix<uint16_t>>(new_size);
        case matrix_element_type::UInt32:
			return new dlib::array<matrix<uint32_t>>(new_size);
        case matrix_element_type::Int8:
			return new dlib::array<matrix<int8_t>>(new_size);
        case matrix_element_type::Int16:
			return new dlib::array<matrix<int16_t>>(new_size);
        case matrix_element_type::Int32:
			return new dlib::array<matrix<int32_t>>(new_size);
        case matrix_element_type::Float:
			return new dlib::array<matrix<float>>(new_size);
        case matrix_element_type::Double:
			return new dlib::array<matrix<double>>(new_size);
        case matrix_element_type::RgbPixel:
			return new dlib::array<matrix<rgb_pixel>>(new_size);
        case matrix_element_type::HsiPixel:
			return new dlib::array<matrix<hsi_pixel>>(new_size);
        case matrix_element_type::RgbAlphaPixel:
			return new dlib::array<matrix<rgb_alpha_pixel>>(new_size);
        default:
            return nullptr;
    }
}

extern "C" __declspec(dllexport) void array_delete(void* obj)
{
    delete obj;
}

#endif