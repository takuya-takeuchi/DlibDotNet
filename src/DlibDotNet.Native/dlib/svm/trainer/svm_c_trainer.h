#ifndef _CPP_SVM_TRAINER_SVM_C_TRAINER_H_
#define _CPP_SVM_TRAINER_SVM_C_TRAINER_H_

#include "../../export.h"
#include <dlib/svm.h>
#include <dlib/svm/svm_c_trainer.h>
#include "../template.h"
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#define svm_c_trainer_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
*ret = new dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>();

#define svm_c_trainer_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_new_template_sub, __VA_ARGS__)

#define svm_c_trainer_new_template2_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
*ret = new dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(k, c);

#define svm_c_trainer_new_template2(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_new_template2_sub, __VA_ARGS__)

#define svm_c_trainer_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)trainer);

#define svm_c_trainer_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_delete_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_c_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_c(c);

#define svm_c_trainer_set_c_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_c_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_c_class1_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_c_class1(c);

#define svm_c_trainer_set_c_class1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_c_class1_template_sub, __VA_ARGS__)

#define svm_c_trainer_get_c_class1_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*c = t->get_c_class1();

#define svm_c_trainer_get_c_class1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_get_c_class1_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_c_class2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_c_class2(c);

#define svm_c_trainer_set_c_class2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_c_class2_template_sub, __VA_ARGS__)

#define svm_c_trainer_get_c_class2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*c = t->get_c_class2();

#define svm_c_trainer_get_c_class2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_get_c_class2_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_epsilon_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_epsilon(epsilon);

#define svm_c_trainer_set_epsilon_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_epsilon_template_sub, __VA_ARGS__)

#define svm_c_trainer_get_epsilon_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*epsilon = t->get_epsilon();

#define svm_c_trainer_get_epsilon_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_get_epsilon_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_cache_size_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
t->set_cache_size(cache_size);

#define svm_c_trainer_set_cache_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_cache_size_template_sub, __VA_ARGS__)

#define svm_c_trainer_get_cache_size_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*cache_size = t->get_cache_size();

#define svm_c_trainer_get_cache_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_get_cache_size_template_sub, __VA_ARGS__)

#define svm_c_trainer_set_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
t->set_kernel(k);

#define svm_c_trainer_set_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_set_kernel_template_sub, __VA_ARGS__)

#define svm_c_trainer_get_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*ret = new KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(t->get_kernel());

#define svm_c_trainer_get_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_get_kernel_template_sub, __VA_ARGS__)

#define svm_c_trainer_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto t = static_cast<dlib::svm_c_trainer<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
const auto r = t->train(dst_x, dst_y);\
*ret = new dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(r);\

#define svm_c_trainer_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, svm_c_trainer_train_template_sub, __VA_ARGS__)

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int svm_c_trainer_new2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                const matrix_element_type type,\
                                                void* kernel,\
                                                const __TYPE__ c,\
                                                void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_new_template2,\
                            kernel_type,\
                            kernel,\
                            c,\
                            ret);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_set_c_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                 const matrix_element_type type,\
                                                 void* trainer,\
                                                 const __TYPE__ c)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_set_c_template,\
                            kernel_type,\
                            trainer,\
                            c);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_set_c_class1_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                        const matrix_element_type type,\
                                                        void* trainer,\
                                                        const __TYPE__ c)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_set_c_class1_template,\
                            kernel_type,\
                            trainer,\
                            c);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_get_c_class1_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                        const matrix_element_type type,\
                                                        void* trainer,\
                                                        __TYPE__* c)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_get_c_class1_template,\
                            kernel_type,\
                            trainer,\
                            c);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_set_c_class2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                        const matrix_element_type type,\
                                                        void* trainer,\
                                                        const __TYPE__ c)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_set_c_class2_template,\
                            kernel_type,\
                            trainer,\
                            c);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_get_c_class2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                        const matrix_element_type type,\
                                                        void* trainer,\
                                                        __TYPE__* c)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_get_c_class2_template,\
                            kernel_type,\
                            trainer,\
                            c);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_set_epsilon_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                       const matrix_element_type type,\
                                                       void* trainer,\
                                                       __TYPE__ epsilon)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_set_epsilon_template,\
                            kernel_type,\
                            trainer,\
                            epsilon);\
\
    return error;\
}\
\
DLLEXPORT int svm_c_trainer_get_epsilon_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                       const matrix_element_type type,\
                                                       void* trainer,\
                                                       __TYPE__* epsilon)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size00_template,\
                            svm_c_trainer_get_epsilon_template,\
                            kernel_type,\
                            trainer,\
                            epsilon);\
\
    return error;\
}\

#pragma endregion

DLLEXPORT int svm_c_trainer_new(const svm_kernel_type kernel_type,
                                const matrix_element_type type,
                                void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_new_template,
                            kernel_type,
                            ret);

    return error;
}

DLLEXPORT void svm_c_trainer_delete(const svm_kernel_type kernel_type,
                                    const matrix_element_type type,
                                    void* trainer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_delete_template,
                            kernel_type,
                            obj);
}

DLLEXPORT int svm_c_trainer_set_cache_size(const svm_kernel_type kernel_type,
                                           const matrix_element_type type,
                                           void* trainer,
                                           const int32_t cache_size)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_set_cache_size_template,
                            kernel_type,
                            trainer,
                            cache_size);

    return error;
}

DLLEXPORT int svm_c_trainer_get_cache_size(const svm_kernel_type kernel_type,
                                           const matrix_element_type type,
                                           void* trainer,
                                           int32_t* cache_size)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_get_cache_size_template,
                            kernel_type,
                            trainer,
                            cache_size);

    return error;
}

DLLEXPORT int svm_c_trainer_set_kernel(const svm_kernel_type kernel_type,
                                       const matrix_element_type type,
                                       void* trainer,
                                       void* kernel)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_set_kernel_template,
                            kernel_type,
                            trainer,
                            kernel);

    return error;
}

DLLEXPORT int svm_c_trainer_get_kernel(const svm_kernel_type kernel_type,
                                       const matrix_element_type type,
                                       void* trainer,
                                       void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_get_kernel_template,
                            kernel_type,
                            trainer,
                            ret);

    return error;
}

DLLEXPORT int svm_c_trainer_train(const svm_kernel_type kernel_type,
                                  const matrix_element_type type,
                                  void* trainer,
                                  void* x,
                                  void* y,
                                  void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            svm_c_trainer_train_template,
                            kernel_type,
                            trainer,
                            x,
                            y,
                            ret);

    return error;
}

MAKE_FUNC(double, double)
MAKE_FUNC(float, float)

#endif