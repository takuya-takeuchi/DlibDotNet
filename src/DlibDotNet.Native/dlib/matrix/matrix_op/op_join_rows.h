#ifndef _CPP_MATRIX_OP_OPJOINROWS_H_
#define _CPP_MATRIX_OP_OPJOINROWS_H_

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

#define matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 0>, matrix<ELEMENT_IN, 0, 0>>>*)obj);\
        delete mat;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 1>, matrix<ELEMENT_IN, 0, 1>>>*)obj);\
        delete mat;\
    }\
} while (0)

#define matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 0>, matrix<ELEMENT_IN, 0, 0>>>*)obj);\
        *ret = mat->nr();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 1>, matrix<ELEMENT_IN, 0, 1>>>*)obj);\
        *ret = mat->nr();\
    }\
} while (0)

#define matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 0>, matrix<ELEMENT_IN, 0, 0>>>*)obj);\
        *ret = mat->nc();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
	    auto mat= ((matrix_op<op_join_rows<matrix<ELEMENT_IN, 0, 1>, matrix<ELEMENT_IN, 0, 1>>>*)obj);\
        *ret = mat->nc();\
    }\
} while (0)

#pragma endregion template

DLLEXPORT void matrix_op_op_join_rows_delete(matrix_element_type type, void* obj, const int templateRows, const int templateColumns)
{
    switch(type)
    {
		case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_join_rows_delete_template(obj, templateRows, templateColumns);
            #undef ELEMENT_IN
			break;
    }
}

DLLEXPORT int matrix_op_op_join_rows_nc(matrix_element_type type, void* obj, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
		case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_join_rows_nc_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_op_op_join_rows_nr(matrix_element_type type, void* obj, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
		case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Float:
            #define ELEMENT_IN float
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Double:
            #define ELEMENT_IN double
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            matrix_op_op_join_rows_nr_template(obj, templateRows, templateColumns, ret);
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