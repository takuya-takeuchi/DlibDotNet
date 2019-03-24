#ifndef _CPP_SVM_KERNEL_H_
#define _CPP_SVM_KERNEL_H_

#include "../export.h"
#include <dlib/svm/kernel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define linear_kernel_new_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, ret) \
ret = new dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define linear_kernel_new_template(__TYPE__, __ROWS__, __COLUMNS__) \
do {\
    int error = ERR_OK;\
    void* ret = nullptr;\
    matrix_template_size_one_column_vector_arg1_template(__TYPE__, __ROWS__, __COLUMNS__, linear_kernel_new_template_sub, error, ret);\
    return ret;\
} while (0)

#define linear_kernel_delete_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, kernel) \
auto k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#define linear_kernel_delete_template(__TYPE__, __ROWS__, __COLUMNS__, kernel) \
do {\
    int error = ERR_OK;\
    matrix_template_size_one_column_vector_arg1_template(__TYPE__, __ROWS__, __COLUMNS__, linear_kernel_delete_template_sub, error, kernel);\
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
            linear_kernel_delete_template(uint8_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::UInt16:
            linear_kernel_delete_template(uint16_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::UInt32:
            linear_kernel_delete_template(uint32_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::Int8:
            linear_kernel_delete_template(int8_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::Int16:
            linear_kernel_delete_template(int16_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::Int32:
            linear_kernel_delete_template(int32_t, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::Float:
            linear_kernel_delete_template(float, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::Double:
            linear_kernel_delete_template(double, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::RgbPixel:
            linear_kernel_delete_template(rgb_pixel, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::HsiPixel:
            linear_kernel_delete_template(hsi_pixel, templateRows, templateColumns, kernel);
            break;
        case matrix_element_type::RgbAlphaPixel:
            linear_kernel_delete_template(rgb_alpha_pixel, templateRows, templateColumns, kernel);
            break;
    }
}

#pragma endregion linear_kernel

#endif