#ifndef _CPP_SVM_TEMPLATE_H_
#define _CPP_SVM_TEMPLATE_H_

#include "../export.h"
#include <dlib/svm/kernel.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;

#define kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case kernel_type::HistogramIntersection:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::histogram_intersection_kernel, __VA_ARGS__);\
        }\
        break;\
    case kernel_type::Linear:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::linear_kernel, __VA_ARGS__);\
        }\
        break;\
    case kernel_type::Polynomial:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::polynomial_kernel, __VA_ARGS__);\
        }\
        break;\
    case kernel_type::RadialBasis:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::radial_basis_kernel, __VA_ARGS__);\
        }\
        break;\
    case kernel_type::Sigmoid:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sigmoid_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#endif