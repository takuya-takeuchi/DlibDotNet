#ifndef _CPP_SVM_KERNEL_H_
#define _CPP_SVM_KERNEL_H_

#include "../export.h"
#include <dlib/svm/kernel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define linear_kernel_new_template(templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new dlib::linear_kernel<dlib::matrix<ELEMENT>>();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<ELEMENT, 0, 1>>();\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<ELEMENT, 5, 1>>();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        return new dlib::linear_kernel<dlib::matrix<ELEMENT, 31, 1>>();\
    }\
    return nullptr;\
} while (0)

#define linear_kernel_delete_template(kernel, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::linear_kernel<dlib::matrix<ELEMENT>>* k = static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<ELEMENT, 0, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT, 0, 1>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<ELEMENT, 5, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT, 5, 1>>*>(kernel);\
        delete k;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::linear_kernel<dlib::matrix<ELEMENT, 31, 1>>* k = static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT, 31, 1>>*>(kernel);\
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
            #define ELEMENT uint8_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            linear_kernel_new_template(templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            linear_kernel_delete_template(kernel, templateRows, templateColumns);
            #undef ELEMENT
            break;
    }
}

#pragma endregion linear_kernel

#endif