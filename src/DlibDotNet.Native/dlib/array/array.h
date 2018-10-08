#ifndef _CPP_ARRAY_H_
#define _CPP_ARRAY_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/array2d.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define array_array2d_pushback_template(ELEMENT) \
do {\
	auto a = static_cast<dlib::array<array2d<ELEMENT>>*>(array);\
	array2d<ELEMENT>& i = *(static_cast<array2d<ELEMENT>*>(item));\
	a->push_back(i);\
} while (0)

#define array_matrix_pushback_template(ELEMENT) \
do {\
	auto a = static_cast<dlib::array<matrix<ELEMENT>>*>(array);\
	matrix<ELEMENT>& i = *(static_cast<matrix<ELEMENT>*>(item));\
	a->push_back(i);\
} while (0)

#pragma endregion template

DLLEXPORT void* array_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array<uint8_t>();
        case array2d_type::UInt16:
			return new dlib::array<uint16_t>();
        case array2d_type::UInt32:
			return new dlib::array<uint32_t>();
        case array2d_type::Int8:
			return new dlib::array<int8_t>();
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
        case array2d_type::UInt32:
			return new dlib::array<uint32_t>(new_size);
        case array2d_type::Int8:
			return new dlib::array<int8_t>(new_size);
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
        case array2d_type::UInt32:
			return new dlib::array<array2d<uint32_t>>();
        case array2d_type::Int8:
			return new dlib::array<array2d<int8_t>>();
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
        case array2d_type::UInt32:
			return new dlib::array<array2d<uint32_t>>(new_size);
        case array2d_type::Int8:
			return new dlib::array<array2d<int8_t>>(new_size);
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

#pragma region push_back

DLLEXPORT int array_pixel_pushback_uint8(const array2d_type type, void* array, uint8_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            ((dlib::array<uint8_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_uint16(const array2d_type type, void* array, uint16_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt16:
            ((dlib::array<uint16_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_uint32(const array2d_type type, void* array, uint32_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt32:
            ((dlib::array<uint32_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_int8(const array2d_type type, void* array, int8_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int8:
            ((dlib::array<int8_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_int16(const array2d_type type, void* array, int16_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int16:
            ((dlib::array<int16_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_int32(const array2d_type type, void* array, int32_t item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int32:
            ((dlib::array<int32_t>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_float(const array2d_type type, void* array, float item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Float:
            ((dlib::array<float>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_double(const array2d_type type, void* array, double item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Double:
            ((dlib::array<double>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_rgb_pixel(const array2d_type type, void* array, rgb_pixel item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::RgbPixel:
            ((dlib::array<rgb_pixel>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_hsi_pixel(const array2d_type type, void* array, hsi_pixel item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::HsiPixel:
            ((dlib::array<hsi_pixel>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_pixel_pushback_rgb_alpha_pixel(const array2d_type type, void* array, rgb_alpha_pixel item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::RgbAlphaPixel:
            ((dlib::array<rgb_alpha_pixel>*)array)->push_back(item);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_array2d_pushback(const array2d_type type, void* array, void* item)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            array_array2d_pushback_template(uint8_t);
            break;
        case array2d_type::UInt16:
            array_array2d_pushback_template(uint16_t);
            break;
        case array2d_type::UInt32:
            array_array2d_pushback_template(uint32_t);
            break;
        case array2d_type::Int8:
            array_array2d_pushback_template(int8_t);
            break;
        case array2d_type::Int16:
            array_array2d_pushback_template(int16_t);
            break;
        case array2d_type::Int32:
            array_array2d_pushback_template(int32_t);
            break;
        case array2d_type::Float:
            array_array2d_pushback_template(float);
            break;
        case array2d_type::Double:
            array_array2d_pushback_template(double);
            break;
        case array2d_type::RgbPixel:
            array_array2d_pushback_template(rgb_pixel);
            break;
        case array2d_type::HsiPixel:
            array_array2d_pushback_template(hsi_pixel);
            break;
        case array2d_type::RgbAlphaPixel:
            array_array2d_pushback_template(rgb_alpha_pixel);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int array_matrix_pushback(const matrix_element_type type, void* array, void* item)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            array_matrix_pushback_template(uint8_t);
            break;
        case matrix_element_type::UInt16:
            array_matrix_pushback_template(uint16_t);
            break;
        case matrix_element_type::UInt32:
            array_matrix_pushback_template(uint32_t);
            break;
        case matrix_element_type::Int8:
            array_matrix_pushback_template(int8_t);
            break;
        case matrix_element_type::Int16:
            array_matrix_pushback_template(int16_t);
            break;
        case matrix_element_type::Int32:
            array_matrix_pushback_template(int32_t);
            break;
        case matrix_element_type::Float:
            array_matrix_pushback_template(float);
            break;
        case matrix_element_type::Double:
            array_matrix_pushback_template(double);
            break;
        case matrix_element_type::RgbPixel:
            array_matrix_pushback_template(rgb_pixel);
            break;
        case matrix_element_type::HsiPixel:
            array_matrix_pushback_template(hsi_pixel);
            break;
        case matrix_element_type::RgbAlphaPixel:
            array_matrix_pushback_template(rgb_alpha_pixel);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion push_back

#pragma region size

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
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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

#pragma endregion size

#pragma region getitem

DLLEXPORT int array_pixel_getitem_uint8(const array2d_type type, void* array, const unsigned int index, uint8_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
			*item = *(((dlib::array<uint8_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_uint16(const array2d_type type, void* array, const unsigned int index, uint16_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt16:
			*item = *(((dlib::array<uint16_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_uint32(const array2d_type type, void* array, const unsigned int index, uint32_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt32:
			*item = *(((dlib::array<uint32_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_int8(const array2d_type type, void* array, const unsigned int index, int8_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int8:
			*item = *(((dlib::array<int8_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_int16(const array2d_type type, void* array, const unsigned int index, int16_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int16:
			*item = *(((dlib::array<int16_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_int32(const array2d_type type, void* array, const unsigned int index, int32_t* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Int32:
			*item = *(((dlib::array<int32_t>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_float(const array2d_type type, void* array, const unsigned int index, float* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Float:
			*item = *(((dlib::array<float>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_double(const array2d_type type, void* array, const unsigned int index, double* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::Double:
			*item = *(((dlib::array<double>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_rgb_pixel(const array2d_type type, void* array, const unsigned int index, rgb_pixel* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::RgbPixel:
			*item = *(((dlib::array<rgb_pixel>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_hsi_pixel(const array2d_type type, void* array, const unsigned int index, hsi_pixel* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::HsiPixel:
			*item = *(((dlib::array<hsi_pixel>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_pixel_getitem_rgb_alpha_pixel(const array2d_type type, void* array, const unsigned int index, rgb_alpha_pixel* item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::RgbAlphaPixel:
			*item = *(((dlib::array<rgb_alpha_pixel>*)array)->begin() + index);
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_array2d_getitem(const array2d_type type, void* array, const unsigned int index, void** item)
{
	int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
			*item = ((dlib::array<array2d<uint8_t>>*)array)->begin() + index;
			break;
        case array2d_type::UInt16:
			*item = ((dlib::array<array2d<uint16_t>>*)array)->begin() + index;
			break;
        case array2d_type::UInt32:
			*item = ((dlib::array<array2d<uint32_t>>*)array)->begin() + index;
			break;
        case array2d_type::Int8:
			*item = ((dlib::array<array2d<int8_t>>*)array)->begin() + index;
			break;
        case array2d_type::Int16:
			*item = ((dlib::array<array2d<int16_t>>*)array)->begin() + index;
			break;
        case array2d_type::Int32:
			*item = ((dlib::array<array2d<int32_t>>*)array)->begin() + index;
			break;
        case array2d_type::Float:
			*item = ((dlib::array<array2d<float>>*)array)->begin() + index;
			break;
        case array2d_type::Double:
			*item = ((dlib::array<array2d<double>>*)array)->begin() + index;
			break;
        case array2d_type::RgbPixel:
			*item = ((dlib::array<array2d<rgb_pixel>>*)array)->begin() + index;
			break;
        case array2d_type::HsiPixel:
			*item = ((dlib::array<array2d<hsi_pixel>>*)array)->begin() + index;
			break;
        case array2d_type::RgbAlphaPixel:
			*item = ((dlib::array<array2d<rgb_alpha_pixel>>*)array)->begin() + index;
			break;
		default:
			err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

DLLEXPORT int array_matrix_getitem(const matrix_element_type type, void* array, const unsigned int index, void** item)
{
	int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
			*item = ((dlib::array<matrix<uint8_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::UInt16:
			*item = ((dlib::array<matrix<uint16_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::UInt32:
			*item = ((dlib::array<matrix<uint32_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::Int8:
			*item = ((dlib::array<matrix<int8_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::Int16:
			*item = ((dlib::array<matrix<int16_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::Int32:
			*item = ((dlib::array<matrix<int32_t>>*)array)->begin() + index;
			break;
        case matrix_element_type::Float:
			*item = ((dlib::array<matrix<float>>*)array)->begin() + index;
			break;
        case matrix_element_type::Double:
			*item = ((dlib::array<matrix<double>>*)array)->begin() + index;
			break;
        case matrix_element_type::RgbPixel:
			*item = ((dlib::array<matrix<rgb_pixel>>*)array)->begin() + index;
			break;
        case matrix_element_type::HsiPixel:
			*item = ((dlib::array<matrix<hsi_pixel>>*)array)->begin() + index;
			break;
        case matrix_element_type::RgbAlphaPixel:
			*item = ((dlib::array<matrix<rgb_alpha_pixel>>*)array)->begin() + index;
			break;
		default:
			err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
			break;
    }

	return err;
}

#pragma endregion getitem

#endif