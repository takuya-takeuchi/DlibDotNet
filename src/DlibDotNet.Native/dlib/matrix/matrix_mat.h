#ifndef _CPP_MATRIX_MAT_H_
#define _CPP_MATRIX_MAT_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_mat.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define FUNCTION function
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef FUNCTION
#undef ELEMENT_IN
#undef ELEMENT_OUT

#define mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
		std::vector<dlib::matrix<ELEMENT_IN>>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT_IN>>*>(vec));\
		auto ret = dlib::mat(tmp);\
		*mat_op = new dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<ELEMENT_IN>, allocator<dlib::matrix<ELEMENT_IN>>>>>(ret);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
		std::vector<dlib::matrix<ELEMENT_IN, 0, 1>>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT_IN, 0, 1>>*>(vec));\
		auto ret = dlib::mat(tmp);\
		*mat_op = new dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<ELEMENT_IN, 0, 1>, allocator<dlib::matrix<ELEMENT_IN, 0, 1>>>>>(ret);\
    }\
} while (0)

#define mat_array2d_template(array, mat_op) \
do {\
    auto ret = mat(*((array2d<ELEMENT_IN>*)array));\
	*mat_op = new matrix_op<op_array2d_to_mat<array2d<ELEMENT_IN>>>(ret);\
} while (0)

#define mat_matrix_template(rc, dst) \
do {\
    dlib::array2d<ELEMENT_IN>& array = *static_cast<dlib::array2d<ELEMENT_IN>*>(src);\
    dlib::matrix<ELEMENT_OUT> tmp = mat(array);\
	*dst = new dlib::matrix<ELEMENT_OUT>(tmp);\
} while (0)

#pragma endregion template

DLLEXPORT int mat_array2d(array2d_type type, void* array, void** mat_op)
{
    int err = ERR_OK;

    switch(type)
    {
		case array2d_type::UInt8:
            #define ELEMENT_IN uint8_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::UInt16:
            #define ELEMENT_IN uint16_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::UInt32:
            #define ELEMENT_IN uint32_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
		case array2d_type::Int8:
            #define ELEMENT_IN int8_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::Int16:
            #define ELEMENT_IN int16_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::Int32:
            #define ELEMENT_IN int32_t
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::Float:
            #define ELEMENT_IN float
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::Double:
            #define ELEMENT_IN double
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            mat_array2d_template(array, mat_op);
            #undef ELEMENT_IN
			break;
        default:
			err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;
    }

    return err;
}

DLLEXPORT int mat_mat_OpStdVectToMat(matrix_element_type type, void* vec, int templateRows, int templateColumns, void** mat_op)
{
    int err = ERR_OK;

    switch(type)
    {
		case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Float:
            #define ELEMENT_IN float
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Double:
            #define ELEMENT_IN double
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            mat_mat_OpStdVectToMat_template(vec, templateRows, templateColumns, mat_op);
            #undef ELEMENT_IN
			break;
        default:
			err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
			break;
    }

    return err;
}

DLLEXPORT int mat_matrix(array2d_type srcType, void* src, const int templateRows, const int templateColumns, matrix_element_type dstType, void** dst)
{
    int err = ERR_OK;

    switch(dstType)
    {
		case matrix_element_type::UInt8:
            if (srcType == array2d_type::UInt8)
            {
                #define ELEMENT_IN uint8_t
                #define ELEMENT_OUT uint8_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::UInt16:
            if (srcType == array2d_type::UInt16)
            {
                #define ELEMENT_IN uint16_t
                #define ELEMENT_OUT uint16_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::UInt32:
            if (srcType == array2d_type::UInt32)
            {
                #define ELEMENT_IN uint32_t
                #define ELEMENT_OUT uint32_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::Int8:
            if (srcType == array2d_type::Int8)
            {
                #define ELEMENT_IN int8_t
                #define ELEMENT_OUT int8_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::Int16:
            if (srcType == array2d_type::Int16)
            {
                #define ELEMENT_IN int16_t
                #define ELEMENT_OUT int16_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::Int32:
            if (srcType == array2d_type::Int32)
            {
                #define ELEMENT_IN int32_t
                #define ELEMENT_OUT int32_t
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::Float:
            if (srcType == array2d_type::Float)
            {
                #define ELEMENT_IN float
                #define ELEMENT_OUT float
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::Double:
            if (srcType == array2d_type::Double)
            {
                #define ELEMENT_IN double
                #define ELEMENT_OUT double
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::RgbPixel:
            if (srcType == array2d_type::RgbPixel)
            {
                #define ELEMENT_IN rgb_pixel
                #define ELEMENT_OUT rgb_pixel
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::HsiPixel:
            if (srcType == array2d_type::HsiPixel)
            {
                #define ELEMENT_IN hsi_pixel
                #define ELEMENT_OUT hsi_pixel
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
		case matrix_element_type::RgbAlphaPixel:
            if (srcType == array2d_type::RgbAlphaPixel)
            {
                #define ELEMENT_IN rgb_alpha_pixel
                #define ELEMENT_OUT rgb_alpha_pixel
                mat_matrix_template(src, dst);
                #undef ELEMENT_OUT
                #undef ELEMENT_IN
            }
			break;
        default:
			err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
			break;
    }

    return err;
}

#endif