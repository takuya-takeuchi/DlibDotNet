#ifndef _CPP_ARRAY_H_
#define _CPP_ARRAY_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/array2d.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;

#pragma region template

#define array_new_template(__TYPE__, error, type, ...) \
ret = new dlib::array<__TYPE__>();

#define array_new1_template(__TYPE__, error, type, ...) \
ret = new dlib::array<__TYPE__>(new_size);

#define array_array2d_new_template(__TYPE__, error, type, ...) \
ret = new dlib::array<array2d<__TYPE__>>();

#define array_array2d_new1_template(__TYPE__, error, type, ...) \
ret = new dlib::array<array2d<__TYPE__>>(new_size);

#define array_matrix_new_template(__TYPE__, error, type, ...) \
ret = new dlib::array<matrix<__TYPE__>>();

#define array_matrix_new1_template(__TYPE__, error, type, ...) \
ret = new dlib::array<matrix<__TYPE__>>(new_size);

#define array_delete_pixel_template(__TYPE__, error, type, ...) \
delete ((dlib::array<__TYPE__>*)array);

#define array_delete_array2d_template(__TYPE__, error, type, ...) \
delete ((dlib::array<array2d<__TYPE__>>*)array);

#define array_delete_matrix_template(__TYPE__, error, type, ...) \
delete ((dlib::array<matrix<__TYPE__>>*)array);

#define array_pixel_size_template(__TYPE__, error, type, ...) \
*size = ((dlib::array<__TYPE__>*)array)->size();

#define array_array2d_size_template(__TYPE__, error, type, ...) \
*size = ((dlib::array<array2d<__TYPE__>>*)array)->size();

#define array_matrix_size_template(__TYPE__, error, type, ...) \
*size = ((dlib::array<matrix<__TYPE__>>*)array)->size();

#define array_array2d_getitem_template(__TYPE__, error, type, ...) \
*item = ((dlib::array<array2d<__TYPE__>>*)array)->begin() + index;

#define array_matrix_getitem_template(__TYPE__, error, type, ...) \
*item = ((dlib::array<matrix<__TYPE__>>*)array)->begin() + index;

#define array_array2d_pushback_template(__TYPE__, error, type, ...) \
auto a = static_cast<dlib::array<array2d<__TYPE__>>*>(array);\
array2d<__TYPE__>& i = *(static_cast<array2d<__TYPE__>*>(item));\
a->push_back(i);

#define array_matrix_pushback_template(__TYPE__, error, type, ...) \
auto a = static_cast<dlib::array<matrix<__TYPE__>>*>(array);\
matrix<__TYPE__>& i = *(static_cast<matrix<__TYPE__>*>(item));\
a->push_back(i);

#define MAKE_PUSHBACK_FUNC(__TYPE__, __TYPENAME__, __TYPENAME2__)\
DLLEXPORT int array_pixel_pushback_##__TYPENAME__(const array2d_type type, void* array, __TYPE__ item)\
{\
    int error = ERR_OK;\
\
    switch(type)\
    {\
        case array2d_type::__TYPENAME2__:\
            ((dlib::array<__TYPE__>*)array)->push_back(item);\
            break;\
        default:\
            error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
            break;\
    }\
\
    return error;\
}

#define MAKE_GETITEM_FUNC(__TYPE__, __TYPENAME__, __TYPENAME2__)\
DLLEXPORT int array_pixel_getitem_##__TYPENAME__(const array2d_type type, void* array, const unsigned int index, __TYPE__* item)\
{\
	int error = ERR_OK;\
\
    switch(type)\
    {\
        case array2d_type::__TYPENAME2__:\
			*item = *(((dlib::array<__TYPE__>*)array)->begin() + index);\
			break;\
		default:\
			error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
\
	return error;\
}

#pragma endregion template

DLLEXPORT void* array_new(array2d_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array_new_template,
                     ret);

    return ret;
}

DLLEXPORT void* array_new1(array2d_type type, uint32_t new_size)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array_new1_template,
                     new_size,
                     ret);

    return ret;
}

DLLEXPORT void* array_array2d_new(array2d_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array_array2d_new_template,
                     ret);

    return ret;
}

DLLEXPORT void* array_array2d_new1(array2d_type type, uint32_t new_size)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array_array2d_new1_template,
                     new_size,
                     ret);

    return ret;
}

DLLEXPORT void* array_matrix_new(matrix_element_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_matrix_new_template,
                    0,
                    0,
                    ret);

    return ret;
}

DLLEXPORT void* array_matrix_new1(matrix_element_type type, uint32_t new_size)
{
    int error = ERR_OK;
    void* ret = nullptr;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_matrix_new1_template,
                    0,
                    0,
                    new_size,
                    ret);

    return ret;
}

DLLEXPORT void array_delete_pixel(array2d_type type, void* array)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_delete_pixel_template,
                     array);
}

DLLEXPORT void array_delete_array2d(array2d_type type, void* array)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_delete_array2d_template,
                     array);
}

DLLEXPORT void array_delete_matrix(matrix_element_type type, void* array)
{
    int error = ERR_OK;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_delete_matrix_template,
                    0,
                    0,
                    array);
}

#pragma region push_back

MAKE_PUSHBACK_FUNC(uint8_t, uint8, UInt8)
MAKE_PUSHBACK_FUNC(uint16_t, uint16, UInt16)
MAKE_PUSHBACK_FUNC(uint32_t, uint32, UInt32)
MAKE_PUSHBACK_FUNC(int8_t, int8, Int8)
MAKE_PUSHBACK_FUNC(int16_t, int16, Int16)
MAKE_PUSHBACK_FUNC(int32_t, int32, Int32)
MAKE_PUSHBACK_FUNC(float, float, Float)
MAKE_PUSHBACK_FUNC(double, double, Double)
MAKE_PUSHBACK_FUNC(rgb_pixel, rgb_pixel, RgbPixel)
MAKE_PUSHBACK_FUNC(bgr_pixel, bgr_pixel, BgrPixel)
MAKE_PUSHBACK_FUNC(hsi_pixel, hsi_pixel, HsiPixel)
MAKE_PUSHBACK_FUNC(rgb_alpha_pixel, rgb_alpha_pixel, RgbAlphaPixel)

DLLEXPORT int array_array2d_pushback(const array2d_type type, void* array, void* item)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_array2d_pushback_template,
                     array,
                     item);

    return error;
}

DLLEXPORT int array_matrix_pushback(const matrix_element_type type, void* array, void* item)
{
    int error = ERR_OK;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_matrix_pushback_template,
                    0,
                    0,
                    array,
                    item);

    return error;
}

#pragma endregion push_back

#pragma region size

DLLEXPORT int array_pixel_size(const array2d_type type, void* array, unsigned long* size)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_pixel_size_template,
                     size);

    return error;
}

DLLEXPORT int array_array2d_size(const array2d_type type, void* array, unsigned long* size)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_array2d_size_template,
                     size);

    return error;
}

DLLEXPORT int array_matrix_size(const matrix_element_type type, void* array, unsigned long* size)
{
    int error = ERR_OK;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_matrix_size_template,
                    0,
                    0,
                    array,
                    size);

    return error;
}

#pragma endregion size

#pragma region getitem

MAKE_GETITEM_FUNC(uint8_t, uint8, UInt8)
MAKE_GETITEM_FUNC(uint16_t, uint16, UInt16)
MAKE_GETITEM_FUNC(uint32_t, uint32, UInt32)
MAKE_GETITEM_FUNC(int8_t, int8, Int8)
MAKE_GETITEM_FUNC(int16_t, int16, Int16)
MAKE_GETITEM_FUNC(int32_t, int32, Int32)
MAKE_GETITEM_FUNC(float, float, Float)
MAKE_GETITEM_FUNC(double, double, Double)
MAKE_GETITEM_FUNC(rgb_pixel, rgb_pixel, RgbPixel)
MAKE_GETITEM_FUNC(hsi_pixel, hsi_pixel, HsiPixel)
MAKE_GETITEM_FUNC(rgb_alpha_pixel, rgb_alpha_pixel, RgbAlphaPixel)

DLLEXPORT int array_array2d_getitem(const array2d_type type, void* array, const unsigned int index, void** item)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array_array2d_getitem_template,
                     array,
                     index,
                     item);

    return error;
}

DLLEXPORT int array_matrix_getitem(const matrix_element_type type, void* array, const unsigned int index, void** item)
{
    int error = ERR_OK;

    // not support template row and column
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array_matrix_getitem_template,
                    0,
                    0,
                    array,
                    size);

    return error;
}

#pragma endregion getitem

#endif