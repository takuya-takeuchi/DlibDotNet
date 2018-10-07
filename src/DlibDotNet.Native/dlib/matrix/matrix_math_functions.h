#ifndef _CPP_MATRIX_MATH_FUNCTIONS_H_
#define _CPP_MATRIX_MATH_FUNCTIONS_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_math_functions.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_round_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__> m = dlib::round(mat);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);\

#define matrix_round_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, ret) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, matrix_round_template_sub, error, matrix, ret);\
} while (0)

#pragma endregion template

DLLEXPORT int matrix_round(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret) 
{ 
    int error = ERR_OK; 
    switch(type) 
    { 
        case matrix_element_type::UInt8:
            matrix_round_template(uint8_t, templateRows, templateColumns, error, matrix, ret);
            break;
        case matrix_element_type::UInt16:
            matrix_round_template(uint16_t, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::UInt32:
            matrix_round_template(uint32_t, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::Int8:
            matrix_round_template(int8_t, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::Int16:
            matrix_round_template(int16_t, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::Int32:
            matrix_round_template(int32_t, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::Float:
            matrix_round_template(float, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::Double:
            matrix_round_template(double, templateRows, templateColumns, error, matrix, ret);
            break; 
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default: 
            error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT; 
            break; 
    } 
 
    return error; 
}

#endif