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
        case matrix_element_type::UInt8:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<uint8_t>*)matrix)));
            break;
        case matrix_element_type::UInt16:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<uint16_t>*)matrix)));
            break;
        case matrix_element_type::UInt32:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<uint32_t>*)matrix)));
            break;
        case matrix_element_type::Int8:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<int8_t>*)matrix)));
            break;
        case matrix_element_type::Int16:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<int16_t>*)matrix)));
            break;
        case matrix_element_type::Int32:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<int32_t>*)matrix)));
            break;
        case matrix_element_type::Float:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<float>*)matrix)));
            break;
        case matrix_element_type::Double:
            *ret = new dlib::point(dlib::max_point(*((dlib::matrix<double>*)matrix)));
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int matrix_max_point2(array2d_type type, void* matrix, dlib::point** ret)
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

#endif