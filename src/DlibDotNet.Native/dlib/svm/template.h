#ifndef _CPP_SVM_TEMPLATE_H_
#define _CPP_SVM_TEMPLATE_H_

#include "../export.h"
#include <dlib/svm/svm_c_trainer.h>
#include <dlib/svm/kernel.h>
#include <dlib/svm/sparse_kernel.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;

#define convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y) \
const auto& tmp_x = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(x);\
dst_x.reserve(tmp_x.size());\
for (int index = 0; index < tmp_x.size(); index++)\
{\
    auto& tmp = *tmp_x.at(index);\
    dst_x.push_back(tmp);\
}\
const auto& tmp_y = *static_cast<std::vector<__TYPE__>*>(y);\
dst_y.reserve(tmp_y.size());\
for (int index = 0; index < tmp_y.size(); index++)\
{\
    auto& tmp = tmp_y.at(index);\
    dst_y.push_back(tmp);\
}\

#define kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case svm_kernel_type::HistogramIntersection:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::histogram_intersection_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Linear:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::linear_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Polynomial:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::polynomial_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::RadialBasis:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::radial_basis_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Sigmoid:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sigmoid_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#define sparse_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case svm_kernel_type::SparseHistogramIntersection:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sparse_histogram_intersection_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::SparseLinear:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sparse_linear_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::SparsePolynomial:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sparse_polynomial_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::SparseRadialBasis:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sparse_radial_basis_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::SparseSigmoid:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sparse_sigmoid_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#define kernel_no_histgram_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case svm_kernel_type::Linear:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::linear_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Polynomial:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::polynomial_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::RadialBasis:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::radial_basis_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Sigmoid:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sigmoid_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#define kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case svm_kernel_type::HistogramIntersection:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::histogram_intersection_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Polynomial:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::polynomial_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::RadialBasis:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::radial_basis_kernel, __VA_ARGS__);\
        }\
        break;\
    case svm_kernel_type::Sigmoid:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::sigmoid_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#define kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, __KERNEL_FUNC__, ...) \
switch(kernel_type)\
{\
    case svm_kernel_type::Linear:\
        {\
            __KERNEL_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::linear_kernel, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_KERNEL_NOT_SUPPORT;\
        break;\
}

#define function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, __FUNCTION_FUNC__, ...) \
switch(function_type)\
{\
    case svm_function_type::Decision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::ProbabilisticDecision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::probabilistic_decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::Distance:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::distance_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::Projection:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::projection_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::MulticlassLinearDecision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::multiclass_linear_decision_function, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_FUNCTION_NOT_SUPPORT;\
        break;\
}

#define function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, __FUNCTION_FUNC__, ...) \
switch(function_type)\
{\
    case svm_function_type::Decision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::ProbabilisticDecision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::probabilistic_decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::Distance:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::distance_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::Projection:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::projection_function, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_FUNCTION_NOT_SUPPORT;\
        break;\
}

#define function_no_linear_template2(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, __FUNCTION_FUNC__, ...) \
switch(function_type)\
{\
    case svm_function_type::Decision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::ProbabilisticDecision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::probabilistic_decision_function, __VA_ARGS__);\
        }\
        break;\
    case svm_function_type::Projection:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::projection_function, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_FUNCTION_NOT_SUPPORT;\
        break;\
}

#define function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, __FUNCTION_FUNC__, ...) \
switch(function_type)\
{\
    case svm_function_type::MulticlassLinearDecision:\
        {\
            __FUNCTION_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::multiclass_linear_decision_function, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_FUNCTION_NOT_SUPPORT;\
        break;\
}

#define svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, __TRAINER_FUNC__, ...) \
switch(trainer_type)\
{\
    case svm_trainer_type::C:\
        {\
            __TRAINER_FUNC__(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, dlib::svm_c_trainer, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_SVM_TRAINER_NOT_SUPPORT;\
        break;\
}

#endif