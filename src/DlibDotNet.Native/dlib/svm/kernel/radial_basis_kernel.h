#ifndef _CPP_SVM_KERNEL_RADIAL_BASIS__KERNEL_H_
#define _CPP_SVM_KERNEL_RADIAL_BASIS__KERNEL_H_

#include "../../export.h"
#include <dlib/svm/kernel.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define radial_basis_kernel_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::radial_basis_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define radial_basis_kernel_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto k = static_cast<dlib::radial_basis_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#pragma endregion

#pragma region radial_basis_kernel

DLLEXPORT void* radial_basis_kernel_new(matrix_element_type type,
                                        const int templateRows,
                                        const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            radial_basis_kernel_new_template,
                            templateRows,
                            templateColumns);

    return ret;
}

DLLEXPORT void radial_basis_kernel_delete(matrix_element_type type,
                                          void* kernel,
                                          const int templateRows,
                                          const int templateColumns)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            radial_basis_kernel_delete_template,
                            templateRows,
                            templateColumns,
                            matrix);
}

#pragma endregion radial_basis_kernel

#endif