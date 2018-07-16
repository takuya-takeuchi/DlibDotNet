#ifndef _CPP_ARRAY_H_
#define _CPP_ARRAY_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/array2d.h>
#include "../shared.h"

using namespace dlib;

DLLEXPORT void* array_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<uint8_t>();
        case array2d_type::UInt16:
			return new dlib::array<uint16_t>();
        case array2d_type::Int16:
			return new dlib::array<int16_t>();
        case array2d_type::Int32:
			return new dlib::array<int32_t>();
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

DLLEXPORT void* array_new1(array2d_type type, uint32_t new_size)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<uint8_t>(new_size);
        case array2d_type::UInt16:
			return new dlib::array<uint16_t>(new_size);
        case array2d_type::Int16:
			return new dlib::array<uint16_t>(new_size);
        case array2d_type::Int32:
			return new dlib::array<uint32_t>(new_size);
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

DLLEXPORT void* array_array2d_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<array2d<uint8_t>>();
        case array2d_type::UInt16:
			return new dlib::array<array2d<uint16_t>>();
        case array2d_type::Int16:
			return new dlib::array<array2d<int16_t>>();
        case array2d_type::Int32:
			return new dlib::array<array2d<int32_t>>();
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

DLLEXPORT void* array_array2d_new1(array2d_type type, uint32_t new_size)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<array2d<uint8_t>>(new_size);
        case array2d_type::UInt16:
			return new dlib::array<array2d<uint16_t>>(new_size);
        case array2d_type::Int16:
			return new dlib::array<array2d<int16_t>>(new_size);
        case array2d_type::Int32:
			return new dlib::array<array2d<int32_t>>(new_size);
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

DLLEXPORT void* array_matrix_new(matrix_element_type type)
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

DLLEXPORT void* array_matrix_new1(matrix_element_type type, uint32_t new_size)
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

DLLEXPORT void array_delete_pixel(array2d_type type, void* array)
{
    switch(type)
    {
        case array2d_type::UInt8:
			delete ((dlib::array<uint8_t>*)array);
			break;
        case array2d_type::UInt16:
			delete ((dlib::array<uint16_t>*)array);
			break;
        case array2d_type::UInt32:
			delete ((dlib::array<uint32_t>*)array);
			break;
        case array2d_type::Int8:
			delete ((dlib::array<int8_t>*)array);
			break;
        case array2d_type::Int16:
			delete ((dlib::array<int16_t>*)array);
			break;
        case array2d_type::Int32:
			delete ((dlib::array<int32_t>*)array);
			break;
        case array2d_type::Float:
			delete ((dlib::array<float>*)array);
			break;
        case array2d_type::Double:
			delete ((dlib::array<double>*)array);
			break;
        case array2d_type::RgbPixel:
			delete ((dlib::array<rgb_pixel>*)array);
			break;
        case array2d_type::HsiPixel:
			delete ((dlib::array<hsi_pixel>*)array);
			break;
        case array2d_type::RgbAlphaPixel:
			delete ((dlib::array<rgb_alpha_pixel>*)array);
			break;
    }
}

DLLEXPORT void array_delete_array2d(array2d_type type, void* array)
{
    switch(type)
    {
        case array2d_type::UInt8:
			delete ((dlib::array<array2d<uint8_t>>*)array);
			break;
        case array2d_type::UInt16:
			delete ((dlib::array<array2d<uint16_t>>*)array);
			break;
        case array2d_type::UInt32:
			delete ((dlib::array<array2d<uint32_t>>*)array);
			break;
        case array2d_type::Int8:
			delete ((dlib::array<array2d<int8_t>>*)array);
			break;
        case array2d_type::Int16:
			delete ((dlib::array<array2d<int16_t>>*)array);
			break;
        case array2d_type::Int32:
			delete ((dlib::array<array2d<int32_t>>*)array);
			break;
        case array2d_type::Float:
			delete ((dlib::array<array2d<float>>*)array);
			break;
        case array2d_type::Double:
			delete ((dlib::array<array2d<double>>*)array);
			break;
        case array2d_type::RgbPixel:
			delete ((dlib::array<array2d<rgb_pixel>>*)array);
			break;
        case array2d_type::HsiPixel:
			delete ((dlib::array<array2d<hsi_pixel>>*)array);
			break;
        case array2d_type::RgbAlphaPixel:
			delete ((dlib::array<array2d<rgb_alpha_pixel>>*)array);
			break;
    }
}

DLLEXPORT void array_delete_matrix(matrix_element_type type, void* array)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
			delete ((dlib::array<matrix<uint8_t>>*)array);
			break;
        case matrix_element_type::UInt16:
			delete ((dlib::array<matrix<uint16_t>>*)array);
			break;
        case matrix_element_type::UInt32:
			delete ((dlib::array<matrix<uint32_t>>*)array);
			break;
        case matrix_element_type::Int8:
			delete ((dlib::array<matrix<int8_t>>*)array);
			break;
        case matrix_element_type::Int16:
			delete ((dlib::array<matrix<int16_t>>*)array);
			break;
        case matrix_element_type::Int32:
			delete ((dlib::array<matrix<int32_t>>*)array);
			break;
        case matrix_element_type::Float:
			delete ((dlib::array<matrix<float>>*)array);
			break;
        case matrix_element_type::Double:
			delete ((dlib::array<matrix<double>>*)array);
			break;
        case matrix_element_type::RgbPixel:
			delete ((dlib::array<matrix<rgb_pixel>>*)array);
			break;
        case matrix_element_type::HsiPixel:
			delete ((dlib::array<matrix<hsi_pixel>>*)array);
			break;
        case matrix_element_type::RgbAlphaPixel:
			delete ((dlib::array<matrix<rgb_alpha_pixel>>*)array);
			break;
    }
}

DLLEXPORT int array_pixel_size(const array2d_type type, void* array, unsigned long* size)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
			*size = ((dlib::array<uint8_t>*)array)->size();
			break;
        case array2d_type::UInt16:
			*size = ((dlib::array<uint16_t>*)array)->size();
			break;
        case array2d_type::UInt32:
			*size = ((dlib::array<uint32_t>*)array)->size();
			break;
        case array2d_type::Int8:
			*size = ((dlib::array<int8_t>*)array)->size();
			break;
        case array2d_type::Int16:
			*size = ((dlib::array<int16_t>*)array)->size();
			break;
        case array2d_type::Int32:
			*size = ((dlib::array<int32_t>*)array)->size();
			break;
        case array2d_type::Float:
			*size = ((dlib::array<float>*)array)->size();
			break;
        case array2d_type::Double:
			*size = ((dlib::array<double>*)array)->size();
			break;
        case array2d_type::RgbPixel:
			*size = ((dlib::array<rgb_pixel>*)array)->size();
			break;
        case array2d_type::HsiPixel:
			*size = ((dlib::array<hsi_pixel>*)array)->size();
			break;
        case array2d_type::RgbAlphaPixel:
			*size = ((dlib::array<rgb_alpha_pixel>*)array)->size();
			break;
		default:
			err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_array2d_size(const array2d_type type, void* array, unsigned long* size)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
			*size = ((dlib::array<array2d<uint8_t>>*)array)->size();
			break;
        case array2d_type::UInt16:
			*size = ((dlib::array<array2d<uint16_t>>*)array)->size();
			break;
        case array2d_type::UInt32:
			*size = ((dlib::array<array2d<uint32_t>>*)array)->size();
			break;
        case array2d_type::Int8:
			*size = ((dlib::array<array2d<int8_t>>*)array)->size();
			break;
        case array2d_type::Int16:
			*size = ((dlib::array<array2d<int16_t>>*)array)->size();
			break;
        case array2d_type::Int32:
			*size = ((dlib::array<array2d<int32_t>>*)array)->size();
			break;
        case array2d_type::Float:
			*size = ((dlib::array<array2d<float>>*)array)->size();
			break;
        case array2d_type::Double:
			*size = ((dlib::array<array2d<double>>*)array)->size();
			break;
        case array2d_type::RgbPixel:
			*size = ((dlib::array<array2d<rgb_pixel>>*)array)->size();
			break;
        case array2d_type::HsiPixel:
			*size = ((dlib::array<array2d<hsi_pixel>>*)array)->size();
			break;
        case array2d_type::RgbAlphaPixel:
			*size = ((dlib::array<array2d<rgb_alpha_pixel>>*)array)->size();
			break;
		default:
			err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_matrix_size(const matrix_element_type type, void* array, unsigned long* size)
{
	int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
			*size = ((dlib::array<matrix<uint8_t>>*)array)->size();
			break;
        case matrix_element_type::UInt16:
			*size = ((dlib::array<matrix<uint16_t>>*)array)->size();
			break;
        case matrix_element_type::UInt32:
			*size = ((dlib::array<matrix<uint32_t>>*)array)->size();
			break;
        case matrix_element_type::Int8:
			*size = ((dlib::array<matrix<int8_t>>*)array)->size();
			break;
        case matrix_element_type::Int16:
			*size = ((dlib::array<matrix<int16_t>>*)array)->size();
			break;
        case matrix_element_type::Int32:
			*size = ((dlib::array<matrix<int32_t>>*)array)->size();
			break;
        case matrix_element_type::Float:
			*size = ((dlib::array<matrix<float>>*)array)->size();
			break;
        case matrix_element_type::Double:
			*size = ((dlib::array<matrix<double>>*)array)->size();
			break;
        case matrix_element_type::RgbPixel:
			*size = ((dlib::array<matrix<rgb_pixel>>*)array)->size();
			break;
        case matrix_element_type::HsiPixel:
			*size = ((dlib::array<matrix<hsi_pixel>>*)array)->size();
			break;
        case matrix_element_type::RgbAlphaPixel:
			*size = ((dlib::array<matrix<rgb_alpha_pixel>>*)array)->size();
			break;
		default:
			err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

#endif