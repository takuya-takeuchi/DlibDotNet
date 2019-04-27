#ifndef _CPP_SVM_KERNEL_HISTOGRAM_INTERSECTION_KERNEL_H_
#define _CPP_SVM_KERNEL_HISTOGRAM_INTERSECTION_KERNEL_H_

#include "../../export.h"
#include <dlib/svm/kernel.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define histogram_intersection_kernel_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::histogram_intersection_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();\

#define histogram_intersection_kernel_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto k = static_cast<dlib::histogram_intersection_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
delete k;\

#pragma endregion

#pragma region histogram_intersection_kernel

DLLEXPORT void* histogram_intersection_kernel_new(matrix_element_type type,
                                                  const int templateRows,
                                                  const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_column1or0_template,
                    histogram_intersection_kernel_new_template,
                    templateRows,
                    templateColumns);

    return ret;
}

DLLEXPORT void histogram_intersection_kernel_delete(matrix_element_type type,
                                                    void* kernel,
                                                    const int templateRows,
                                                    const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_column1or0_template,
                    histogram_intersection_kernel_delete_template,
                    templateRows,
                    templateColumns,
                    matrix);
}

#pragma endregion histogram_intersection_kernel

#endif