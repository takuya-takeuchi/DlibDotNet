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

DLLEXPORT int mat_array2d(array2d_type type, void* array, void** mat_op)
{
    int err = ERR_OK;

    switch(type)
    {
		case array2d_type::UInt8:
			{
                auto ret = mat(*((array2d<uint8_t>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<uint8_t>>>(ret);
			}
			break;
        case array2d_type::UInt16:
			{
                auto ret = mat(*((array2d<uint16_t>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<uint16_t>>>(ret);
			}
			break;
        case array2d_type::Int32:
			{
                auto ret = mat(*((array2d<int32_t>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<int32_t>>>(ret);
			}
			break;
        case array2d_type::Float:
			{
                auto ret = mat(*((array2d<float>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<float>>>(ret);
			}
			break;
        case array2d_type::Double:
			{
                auto ret = mat(*((array2d<double>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<double>>>(ret);
			}
			break;
        case array2d_type::RgbPixel:
			{
                auto ret = mat(*((array2d<rgb_pixel>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<rgb_pixel>>>(ret);
			}
			break;
        case array2d_type::HsiPixel:
			{
                auto ret = mat(*((array2d<hsi_pixel>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<hsi_pixel>>>(ret);
			}
			break;
        case array2d_type::RgbAlphaPixel:
			{
                auto ret = mat(*((array2d<rgb_alpha_pixel>*)array));
				*mat_op = new matrix_op<op_array2d_to_mat<array2d<rgb_alpha_pixel>>>(ret);
			}
			break;
        default:
			err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;
    }

    return err;
}

#endif