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

#define matrix_round_template(_ELEMENT_, matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<_ELEMENT_>& mat = *static_cast<dlib::matrix<_ELEMENT_>*>(matrix);\
        dlib::matrix<_ELEMENT_> m = dlib::round(mat);\
        *ret = new dlib::matrix<_ELEMENT_>(m);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<_ELEMENT_, 0, 1>& mat = *static_cast<dlib::matrix<_ELEMENT_, 0, 1>*>(matrix);\
        dlib::matrix<_ELEMENT_, 0, 1> m = dlib::round(mat);\
        *ret = new dlib::matrix<_ELEMENT_, 0, 1>(m);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<_ELEMENT_, 31, 1>& mat = *static_cast<dlib::matrix<_ELEMENT_, 31, 1>*>(matrix);\
        dlib::matrix<_ELEMENT_, 31, 1> m = dlib::round(mat);\
        *ret = new dlib::matrix<_ELEMENT_, 31, 1>(m);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int matrix_round(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret) 
{ 
    int err = ERR_OK; 
    switch(type) 
    { 
        case matrix_element_type::UInt8:
            matrix_round_template(uint8_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::UInt16:
            matrix_round_template(uint16_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::UInt32:
            matrix_round_template(uint32_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::Int8:
            matrix_round_template(int8_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::Int16:
            matrix_round_template(int16_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::Int32:
            matrix_round_template(int32_t, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::Float:
            matrix_round_template(float, matrix, templateRows, templateColumns, ret);
            break; 
        case matrix_element_type::Double:
            matrix_round_template(double, matrix, templateRows, templateColumns, ret);
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

#endif