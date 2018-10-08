#ifndef _CPP_MATRIX_OP_OPARRAY2DTOMAT_H_
#define _CPP_MATRIX_OP_OPARRAY2DTOMAT_H_

#include "../../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT_IN element
#undef ELEMENT_IN

#define matrix_op_op_array2d_to_mat_delete_template(obj) \
do {\
    auto mat = static_cast<matrix_op<op_array2d_to_mat<array2d<ELEMENT_IN>>>*>(obj);\
    delete mat;\
} while (0)

#define matrix_op_op_array2d_to_mat_nc_template(obj, ret) \
do {\
	*ret = ((matrix_op<op_array2d_to_mat<array2d<ELEMENT_IN>>>*)obj)->nc();\
} while (0)

#define matrix_op_op_array2d_to_mat_nr_template(obj, ret) \
do {\
	*ret = ((matrix_op<op_array2d_to_mat<array2d<ELEMENT_IN>>>*)obj)->nr();\
} while (0)

#pragma endregion template

DLLEXPORT void matrix_op_op_array2d_to_mat_delete(array2d_type type, void* obj)
{
    switch(type)
    {
		case array2d_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_array2d_to_mat_delete_template(obj);
            #undef ELEMENT_IN
			break;
    }
}

DLLEXPORT int matrix_op_op_array2d_to_mat_nc(array2d_type type, void* obj, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
		case array2d_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_array2d_to_mat_nc_template(obj, ret);
            #undef ELEMENT_IN
			break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_op_op_array2d_to_mat_nr(array2d_type type, void* obj, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
		case array2d_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
		case array2d_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_array2d_to_mat_nr_template(obj, ret);
            #undef ELEMENT_IN
			break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region operator

#pragma endregion oprator

#endif