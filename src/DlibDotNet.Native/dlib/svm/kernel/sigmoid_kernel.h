#ifndef _CPP_SVM_KERNEL_RADIAL_BASIS__KERNEL_H_
#define _CPP_SVM_KERNEL_RADIAL_BASIS__KERNEL_H_

#include "../../export.h"
#include <dlib/svm/kernel.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define sigmoid_kernel_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::sigmoid_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define sigmoid_kernel_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto k = static_cast<dlib::sigmoid_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#pragma endregion

#pragma region sigmoid_kernel

DLLEXPORT void* sigmoid_kernel_new(matrix_element_type type,
                                   const int templateRows,
                                   const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            sigmoid_kernel_new_template,
                            templateRows,
                            templateColumns);

    return ret;
}

DLLEXPORT void sigmoid_kernel_delete(matrix_element_type type,
                                     void* kernel,
                                     const int templateRows,
                                     const int templateColumns)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            sigmoid_kernel_delete_template,
                            templateRows,
                            templateColumns,
                            matrix);
}

#pragma endregion sigmoid_kernel

#endif