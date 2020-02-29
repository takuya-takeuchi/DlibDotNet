#ifndef _CPP_SVM_PEGASOS_SVM_PEGASOS_H_
#define _CPP_SVM_PEGASOS_SVM_PEGASOS_H_

#include "../../export.h"
#include <dlib/svm/pegasos.h>
#include "../template.h"
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define svm_pegasos_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
*ret = new dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>();

#define svm_pegasos_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_new_template_sub, __VA_ARGS__)

#define svm_pegasos_new_template2_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
*ret = new dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(k, lambda, tolerance, max_num_sv);

#define svm_pegasos_new_template2(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_new_template2_sub, __VA_ARGS__)

#define svm_pegasos_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)trainer);

#define svm_pegasos_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_delete_template_sub, __VA_ARGS__)

#define svm_pegasos_set_lambda_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_lambda(lambda);

#define svm_pegasos_set_lambda_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_lambda_template_sub, __VA_ARGS__)

#define svm_pegasos_set_lambda_class1_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_lambda_class1(lambda);

#define svm_pegasos_set_lambda_class1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_lambda_class1_template_sub, __VA_ARGS__)

#define svm_pegasos_get_lambda_class1_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*lambda = t->get_lambda_class1();

#define svm_pegasos_get_lambda_class1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_lambda_class1_template_sub, __VA_ARGS__)

#define svm_pegasos_set_lambda_class2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_lambda_class2(lambda);

#define svm_pegasos_set_lambda_class2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_lambda_class2_template_sub, __VA_ARGS__)

#define svm_pegasos_get_lambda_class2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*lambda = t->get_lambda_class2();

#define svm_pegasos_get_lambda_class2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_lambda_class2_template_sub, __VA_ARGS__)

#define svm_pegasos_set_tolerance_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_tolerance(tolerance);

#define svm_pegasos_set_tolerance_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_tolerance_template_sub, __VA_ARGS__)

#define svm_pegasos_get_tolerance_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*tolerance = t->get_tolerance();

#define svm_pegasos_get_tolerance_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_tolerance_template_sub, __VA_ARGS__)

#define svm_pegasos_set_max_num_sv_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_max_num_sv(max_num_sv);

#define svm_pegasos_set_max_num_sv_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_max_num_sv_template_sub, __VA_ARGS__)

#define svm_pegasos_get_max_num_sv_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*max_num_sv = t->get_max_num_sv();

#define svm_pegasos_get_max_num_sv_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_max_num_sv_template_sub, __VA_ARGS__)

#define svm_pegasos_set_train_count_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_train_count(train_count);

#define svm_pegasos_set_train_count_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_train_count_template_sub, __VA_ARGS__)

#define svm_pegasos_get_train_count_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*train_count = t->get_train_count();

#define svm_pegasos_get_train_count_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_train_count_template_sub, __VA_ARGS__)

#define svm_pegasos_set_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
t->set_kernel(k);

#define svm_pegasos_set_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_set_kernel_template_sub, __VA_ARGS__)

#define svm_pegasos_get_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*ret = new KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(t->get_kernel());

#define svm_pegasos_get_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_get_kernel_template_sub, __VA_ARGS__)

#define svm_pegasos_operator_sub_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
*ret = t->operator()(m);

#define svm_pegasos_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_operator_sub_template, __VA_ARGS__)

#define svm_pegasos_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto& in_x = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(x);\
*ret = t->train(in_x, y);

#define svm_pegasos_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_train_template_sub, __VA_ARGS__)

#define svm_pegasos_clear_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_pegasos<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->clear();

#define svm_pegasos_clear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_pegasos_clear_template_sub, __VA_ARGS__)

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int svm_pegasos_new2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                              const matrix_element_type type,\
                                              void* kernel,\
                                              const __TYPE__ lambda,\
                                              const __TYPE__ tolerance,\
                                              const uint32_t max_num_sv,\
                                              void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_new_template2,\
                            kernel_type,\
                            kernel,\
                            lambda,\
                            tolerance,\
                            max_num_sv,\
                            ret);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_set_lambda_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                    const matrix_element_type type,\
                                                    void* trainer,\
                                                    const __TYPE__ lambda)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_set_lambda_template,\
                            kernel_type,\
                            trainer,\
                            lambda);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_set_lambda_class1_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                           const matrix_element_type type,\
                                                           void* trainer,\
                                                           const __TYPE__ lambda)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_set_lambda_class1_template,\
                            kernel_type,\
                            trainer,\
                            lambda);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_get_lambda_class1_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                           const matrix_element_type type,\
                                                           void* trainer,\
                                                           __TYPE__* lambda)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_get_lambda_class1_template,\
                            kernel_type,\
                            trainer,\
                            lambda);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_set_lambda_class2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                           const matrix_element_type type,\
                                                           void* trainer,\
                                                           const __TYPE__ lambda)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_set_lambda_class2_template,\
                            kernel_type,\
                            trainer,\
                            lambda);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_get_lambda_class2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                           const matrix_element_type type,\
                                                           void* trainer,\
                                                           __TYPE__* lambda)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_get_lambda_class2_template,\
                            kernel_type,\
                            trainer,\
                            lambda);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_set_tolerance_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                       const matrix_element_type type,\
                                                       void* trainer,\
                                                       __TYPE__ tolerance)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_set_tolerance_template,\
                            kernel_type,\
                            trainer,\
                            tolerance);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_get_tolerance_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                       const matrix_element_type type,\
                                                       void* trainer,\
                                                       __TYPE__* tolerance)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_get_tolerance_template,\
                            kernel_type,\
                            trainer,\
                            tolerance);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_operator_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                  const matrix_element_type type,\
                                                  void* trainer,\
                                                  void* sample,\
                                                  __TYPE__* ret)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_operator_template,\
                            kernel_type,\
                            trainer,\
                            sample,\
                            ret);\
\
    return error;\
}\
\
DLLEXPORT int svm_pegasos_train_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                               const matrix_element_type type,\
                                               void* trainer,\
                                               void* x,\
                                               __TYPE__ y,\
                                               __TYPE__* ret)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_pegasos_train_template,\
                            kernel_type,\
                            trainer,\
                            x,\
                            y,\
                            ret);\
\
    return error;\
}\

#pragma endregion

DLLEXPORT int svm_pegasos_new(const svm_kernel_type kernel_type,
                              const matrix_element_type type,
                              void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_new_template,
                            kernel_type,
                            ret);

    return error;
}

DLLEXPORT void svm_pegasos_delete(const svm_kernel_type kernel_type,
                                  const matrix_element_type type,
                                  void* trainer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_delete_template,
                            kernel_type,
                            obj);
}

DLLEXPORT int svm_pegasos_set_max_num_sv(const svm_kernel_type kernel_type,
                                         const matrix_element_type type,
                                         void* trainer,
                                         const int32_t max_num_sv)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_set_max_num_sv_template,
                            kernel_type,
                            trainer,
                            max_num_sv);

    return error;
}

DLLEXPORT int svm_pegasos_get_max_num_sv(const svm_kernel_type kernel_type,
                                         const matrix_element_type type,
                                         void* trainer,
                                         uint32_t* max_num_sv)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_get_max_num_sv_template,
                            kernel_type,
                            trainer,
                            max_num_sv);

    return error;
}

DLLEXPORT int svm_pegasos_get_train_count(const svm_kernel_type kernel_type,
                                          const matrix_element_type type,
                                          void* trainer,
                                          uint32_t* train_count)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_get_train_count_template,
                            kernel_type,
                            trainer,
                            train_count);

    return error;
}

DLLEXPORT int svm_pegasos_set_kernel(const svm_kernel_type kernel_type,
                                     const matrix_element_type type,
                                     void* trainer,
                                     void* kernel)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_set_kernel_template,
                            kernel_type,
                            trainer,
                            kernel);

    return error;
}

DLLEXPORT int svm_pegasos_get_kernel(const svm_kernel_type kernel_type,
                                     const matrix_element_type type,
                                     void* trainer,
                                     void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_get_kernel_template,
                            kernel_type,
                            trainer,
                            ret);

    return error;
}

DLLEXPORT int svm_pegasos_clear(const svm_kernel_type kernel_type,
                                const matrix_element_type type,
                                void* trainer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_pegasos_clear_template,
                            kernel_type,
                            trainer);

    return error;
}

MAKE_FUNC(double, double)
MAKE_FUNC(float, float)

#endif