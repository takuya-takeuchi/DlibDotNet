#ifndef _CPP_MATRIX_UTILITIES_H_
#define _CPP_MATRIX_UTILITIES_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_utilities.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT void* linspace(double start, double end, int num)
{
    matrix_range_exp<double> ret = dlib::linspace(start, end, num);
    return new matrix_range_exp<double>(ret);
}

DLLEXPORT int matrix_max_point(matrix_element_type type, void* matrix, dlib::point** ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto mat_op = static_cast<matrix_op<op_array2d_to_mat<array2d<uint8_t>>>*>(matrix);
                auto p = dlib::max_point(*mat_op);
                *ret = new dlib::point(p);;
            }
            break;
        case array2d_type::UInt16:
            {
                auto mat_op = static_cast<matrix_op<op_array2d_to_mat<array2d<uint16_t>>>*>(matrix);
                auto p = dlib::max_point(*mat_op);
                *ret = new dlib::point(p);;
            }
            break;
        case array2d_type::Int32:
            {
                auto mat_op = static_cast<matrix_op<op_array2d_to_mat<array2d<int32_t>>>*>(matrix);
                auto p = dlib::max_point(*mat_op);
                *ret = new dlib::point(p);;
            }
            break;
        case array2d_type::Float:
            {
                auto mat_op = static_cast<matrix_op<op_array2d_to_mat<array2d<float>>>*>(matrix);
                auto p = dlib::max_point(*mat_op);
                *ret = new dlib::point(p);;
            }
            break;
        case array2d_type::Double:
            {
                auto mat_op = static_cast<matrix_op<op_array2d_to_mat<array2d<double>>>*>(matrix);
                auto p = dlib::max_point(*mat_op);
                *ret = new dlib::point(p);;
            }
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_trans(matrix_element_type type, void* matrix, void** ret) 
{ 
    int err = ERR_OK; 
    switch(type) 
    { 
        case matrix_element_type::UInt8: 
            { 
                auto mat = static_cast<dlib::matrix<uint8_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<uint8_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::UInt16: 
            { 
                auto mat = static_cast<dlib::matrix<uint16_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<uint16_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::UInt32: 
            { 
                auto mat = static_cast<dlib::matrix<uint32_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<uint32_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::Int8: 
            { 
                auto mat = static_cast<dlib::matrix<int8_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<int8_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::Int16: 
            { 
                auto mat = static_cast<dlib::matrix<int16_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<int16_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::Int32: 
            { 
                auto mat = static_cast<dlib::matrix<int32_t>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<int32_t>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::Float: 
            { 
                auto mat = static_cast<dlib::matrix<float>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<float>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::Double: 
            { 
                auto mat = static_cast<dlib::matrix<double>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<double>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::RgbPixel: 
            { 
                auto mat = static_cast<dlib::matrix<rgb_pixel>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<rgb_pixel>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::HsiPixel: 
            { 
                auto mat = static_cast<dlib::matrix<hsi_pixel>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<hsi_pixel>>>(transedMat); 
            } 
            break; 
        case matrix_element_type::RgbAlphaPixel: 
            { 
                auto mat = static_cast<dlib::matrix<rgb_alpha_pixel>*>(matrix); 
                auto transedMat = dlib::trans(*mat); 
                *ret = new matrix_op<op_trans<dlib::matrix<rgb_alpha_pixel>>>(transedMat); 
            } 
            break; 
        default: 
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT; 
            break; 
    } 
 
    return err; 
}

#endif