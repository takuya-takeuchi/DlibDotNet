#ifndef _CPP_SVM_KERNEL_LINEAR_KERNEL_H_
#define _CPP_SVM_KERNEL_LINEAR_KERNEL_H_

#include "../../export.h"
#include <dlib/svm/kernel.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define linear_kernel_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define linear_kernel_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto k = static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#pragma endregion

#pragma region linear_kernel

DLLEXPORT void* linear_kernel_new(matrix_element_type type,
                                  const int templateRows,
                                  const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_column1or0_template,
                    linear_kernel_new_template,
                    templateRows,
                    templateColumns);

    return ret;
}

DLLEXPORT void linear_kernel_delete(matrix_element_type type,
                                    void* kernel,
                                    const int templateRows,
                                    const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_column1or0_template,
                    linear_kernel_delete_template,
                    templateRows,
                    templateColumns,
                    matrix);
}

#pragma endregion linear_kernel

#endif