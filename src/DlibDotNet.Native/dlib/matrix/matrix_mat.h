#ifndef _CPP_MATRIX_MAT_H_
#define _CPP_MATRIX_MAT_H_

#include "../export.h"
#include <dlib/matrix.h>
#include "../template.h"
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

#define mat_mat_OpStdVectToMat_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& tmp = *(static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(vec));\
auto ret = dlib::mat(tmp);\
*mat_op = new dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>, allocator<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>>(ret);\

#define mat_array2d_template(__TYPE__, error, type, ...) \
auto ret = dlib::mat(*((array2d<__TYPE__>*)array));\
*mat_op = new matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>(ret);\

#define mat_matrix_template(rc, dst) \
do {\
    dlib::array2d<ELEMENT_IN>& array = *static_cast<dlib::array2d<ELEMENT_IN>*>(src);\
    dlib::matrix<ELEMENT_OUT> tmp = mat(array);\
	*dst = new dlib::matrix<ELEMENT_OUT>(tmp);\
} while (0)

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT void mat_StdVect_##__TYPENAME__(void* vec, void** mat_op)\
{\
    /* dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>> stores reference of vector. */ \
    /* In other words, called must hold instance or reference of vector. */ \
    auto& v = *static_cast<std::vector<__TYPE__>*>(vec);\
    auto ret = dlib::mat(v);\
	*mat_op = new dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>>(ret);\
}\

#pragma endregion template

DLLEXPORT int mat_array2d(array2d_type type, void* array, void** mat_op)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     mat_array2d_template,
                     array,
                     mat_op);

    return error;
}

MAKE_FUNC(int8_t, int8_t)
MAKE_FUNC(int16_t, int16_t)
MAKE_FUNC(int32_t, int32_t)
MAKE_FUNC(uint8_t, uint8_t)
MAKE_FUNC(uint16_t, uint16_t)
MAKE_FUNC(uint32_t, uint32_t)
MAKE_FUNC(float, float)
MAKE_FUNC(double, double)
MAKE_FUNC(dlib::rgb_pixel, rgb_pixel)
MAKE_FUNC(dlib::hsi_pixel, hsi_pixel)
MAKE_FUNC(dlib::rgb_alpha_pixel, rgb_alpha_pixel)

DLLEXPORT int mat_mat_OpStdVectToMat(matrix_element_type type, void* vec, int templateRows, int templateColumns, void** mat_op)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    mat_mat_OpStdVectToMat_template,
                    templateRows,
                    templateColumns,
                    mat_op);

    return error;
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