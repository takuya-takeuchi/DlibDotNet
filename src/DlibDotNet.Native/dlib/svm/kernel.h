#ifndef _CPP_SVM_KERNEL_H_
#define _CPP_SVM_KERNEL_H_

#include "../export.h"
#include <dlib/svm/kernel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define linear_kernel_new_template(__TYPE__, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new dlib::linear_kernel<dlib::matrix<__TYPE__>>();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<__TYPE__, 0, 1>>();\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<__TYPE__, 5, 1>>();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<__TYPE__, 31, 1>>();\
    }\
    return nullptr;\
} while (0)

#define linear_kernel_delete_template(__TYPE__, kernel, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::linear_kernel<dlib::matrix<__TYPE__>>* k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<__TYPE__, 0, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, 0, 1>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<__TYPE__, 5, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, 5, 1>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<__TYPE__, 31, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, 31, 1>>*>(kernel);\
        delete k;\
    }\
} while (0)

#pragma endregion

#pragma region linear_kernel

DLLEXPORT void* linear_kernel_new(matrix_element_type type,
                                  const int templateRows,
                                  const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            linear_kernel_new_template(uint8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            linear_kernel_new_template(uint16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            linear_kernel_new_template(uint32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            linear_kernel_new_template(int8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            linear_kernel_new_template(int16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            linear_kernel_new_template(int32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            linear_kernel_new_template(float, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            linear_kernel_new_template(double, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            linear_kernel_new_template(rgb_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            linear_kernel_new_template(hsi_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            linear_kernel_new_template(rgb_alpha_pixel, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void linear_kernel_delete(matrix_element_type type,
                                    void* kernel,
                                    const int templateRows,
                                    const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            linear_kernel_delete_template(uint8_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            linear_kernel_delete_template(uint16_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            linear_kernel_delete_template(uint32_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            linear_kernel_delete_template(int8_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            linear_kernel_delete_template(int16_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            linear_kernel_delete_template(int32_t, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            linear_kernel_delete_template(float, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            linear_kernel_delete_template(double, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            linear_kernel_delete_template(rgb_pixel, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            linear_kernel_delete_template(hsi_pixel, kernel, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            linear_kernel_delete_template(rgb_alpha_pixel, kernel, templateRows, templateColumns);
            break;
    }
}

#pragma endregion linear_kernel

#endif