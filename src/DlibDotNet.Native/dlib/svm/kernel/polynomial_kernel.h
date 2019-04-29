#ifndef _CPP_SVM_KERNEL_POLYNOMIAL_KERNEL_H_
#define _CPP_SVM_KERNEL_POLYNOMIAL_KERNEL_H_

#include "../../export.h"
#include <dlib/svm/kernel.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define polynomial_kernel_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = new dlib::polynomial_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define polynomial_kernel_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto k = static_cast<dlib::polynomial_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#pragma endregion

#pragma region polynomial_kernel

DLLEXPORT int polynomial_kernel_new(matrix_element_type type,
                                    const int templateRows,
                                    const int templateColumns,
                                    void** ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            polynomial_kernel_new_template,
                            templateRows,
                            templateColumns,
                            ret);

    return error;
}

DLLEXPORT void polynomial_kernel_delete(matrix_element_type type,
                                        void* kernel,
                                        const int templateRows,
                                        const int templateColumns)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            polynomial_kernel_delete_template,
                            templateRows,
                            templateColumns,
                            matrix);
}

#pragma endregion polynomial_kernel

#endif