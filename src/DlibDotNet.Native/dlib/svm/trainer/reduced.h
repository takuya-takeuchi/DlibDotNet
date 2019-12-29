#ifndef _CPP_SVM_TRAINER_REDUCED_H_
#define _CPP_SVM_TRAINER_REDUCED_H_

#include "../../export.h"
#include <dlib/svm/reduced.h>
#include "../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#pragma region reduced_decision_function_trainer2

#define reduced2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto r = dlib::reduced2(t, num_bv, eps);\
*ret = new dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>(r);

#define reduced2_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, reduced2_template_sub, __VA_ARGS__)

#define reduced2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, reduced2_template_trainer_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*ret = new dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>(t, num_bv, eps);

#define reduced_decision_function_trainer2_new_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, reduced_decision_function_trainer2_new_template_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, reduced_decision_function_trainer2_new_template_trainer_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
auto t = static_cast<dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
delete t;

#define reduced_decision_function_trainer2_delete_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, reduced_decision_function_trainer2_delete_template_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, reduced_decision_function_trainer2_delete_template_trainer_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto t = static_cast<dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
const auto r = t->train(dst_x, dst_y);\
*ret = new dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(r);\

#define reduced_decision_function_trainer2_train_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, reduced_decision_function_trainer2_train_template_sub, __VA_ARGS__)

#define reduced_decision_function_trainer2_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_histgram_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, reduced_decision_function_trainer2_train_template_trainer_sub, __VA_ARGS__)

#define MAKE_REDUCED_DECISION_FUNCTION_TRAINER2_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int reduced_decision_function_trainer2_train_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                                      const matrix_element_type type,\
                                                                      const int templateRows,\
                                                                      const int templateColumns,\
                                                                      const svm_trainer_type trainer_type,\
                                                                      void* trainer,\
                                                                      void* x,\
                                                                      void* y,\
                                                                      void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_double_template(type,\
                           error,\
                           matrix_template_size00_template,\
                           reduced_decision_function_trainer2_train_template,\
                           templateRows,\
                           templateColumns,\
                           kernel_type,\
                           trainer_type,\
                           trainer,\
                           x,\
                           y,\
                           ret);\
\
    return error;\
}\

#pragma endregion

DLLEXPORT int reduced2(const svm_kernel_type kernel_type,
                       const matrix_element_type type,
                       const int templateRows,
                       const int templateColumns,
                       const svm_trainer_type trainer_type,
                       void* trainer,
                       const unsigned long num_bv,
                       const double eps,
                       void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            reduced2_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            trainer_type,
                            trainer,
                            num_bv,
                            eps,
                            ret);

    return error;
}

DLLEXPORT int reduced_decision_function_trainer2_new(const svm_kernel_type kernel_type,
                                                     const matrix_element_type type,
                                                     const int templateRows,
                                                     const int templateColumns,
                                                     const svm_trainer_type trainer_type,
                                                     void* trainer,
                                                     const unsigned long num_bv,
                                                     const double eps,
                                                     void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            reduced_decision_function_trainer2_new_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            trainer_type,
                            trainer,
                            num_bv,
                            eps,
                            ret);

    return error;
}

DLLEXPORT void reduced_decision_function_trainer2_delete(const svm_kernel_type kernel_type,
                                                         const matrix_element_type type,
                                                         const int templateRows,
                                                         const int templateColumns,
                                                         const svm_trainer_type trainer_type,
                                                         void* trainer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            reduced_decision_function_trainer2_delete_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            trainer_type,
                            trainer);
}

MAKE_REDUCED_DECISION_FUNCTION_TRAINER2_FUNC(double, double)

#endif