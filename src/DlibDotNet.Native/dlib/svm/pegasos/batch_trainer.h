#ifndef _CPP_SVM_PEGASOS_BATCH_TRAINER_H_
#define _CPP_SVM_PEGASOS_BATCH_TRAINER_H_

#include "../../export.h"
#include <dlib/svm/pegasos.h>
#include "../template.h"
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;

#pragma region template

#pragma region batch_trainer

#define batch_trainer_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
*ret = new dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>();

#define batch_trainer_new_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_trainer_new_template_sub, __VA_ARGS__)

#define batch_trainer_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_trainer_new_template_trainer_sub, __VA_ARGS__)

#define batch_trainer_new2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
*ret = new dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>(t,\
                                                                                               min_learning_rate,\
                                                                                               verbose,\
                                                                                               use_cache,\
                                                                                               cache_size);\

#define batch_trainer_new2_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_trainer_new2_template_sub, __VA_ARGS__)

#define batch_trainer_new2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_trainer_new2_template_trainer_sub, __VA_ARGS__)

#define batch_trainer_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
auto t = static_cast<dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
delete t;

#define batch_trainer_delete_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_trainer_delete_template_sub, __VA_ARGS__)

#define batch_trainer_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_trainer_delete_template_trainer_sub, __VA_ARGS__)

#define batch_cached_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto bt = dlib::batch_cached(t, min_learning_rate, cache_size);\
*ret = new dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>(bt);\

#define batch_cached_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_cached_template_sub, __VA_ARGS__)

#define batch_cached_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_cached_template_trainer_sub, __VA_ARGS__)

#define verbose_batch_cached_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
const auto bt = dlib::verbose_batch_cached(t, min_learning_rate, cache_size);\
*ret = new dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>(bt);\

#define verbose_batch_cached_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, verbose_batch_cached_template_sub, __VA_ARGS__)

#define verbose_batch_cached_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, verbose_batch_cached_template_trainer_sub, __VA_ARGS__)

#define batch_trainer_get_min_learning_rate_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto t = static_cast<dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
*ret = t->get_min_learning_rate();\

#define batch_trainer_get_min_learning_rate_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_trainer_get_min_learning_rate_template_sub, __VA_ARGS__)

#define batch_trainer_get_min_learning_rate_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_trainer_get_min_learning_rate_template_trainer_sub, __VA_ARGS__)

#define batch_trainer_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto t = static_cast<dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
const auto r = t->train(dst_x, dst_y);\
*ret = new dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(r);\

#define batch_trainer_train_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, batch_trainer_train_template_sub, __VA_ARGS__)

#define batch_trainer_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, batch_trainer_train_template_trainer_sub, __VA_ARGS__)

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int batch_trainer_new2_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                const matrix_element_type type,\
                                                const svm_batch_trainer_type trainer_type,\
                                                void* trainer,\
                                                const __TYPE__ min_learning_rate,\
                                                const bool verbose,\
                                                const bool use_cache,\
                                                const int32_t cache_size,\
                                                void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_double_template(type,\
                           error,\
                           matrix_template_size00_template,\
                           batch_trainer_new2_template,\
                           kernel_type,\
                           trainer_type,\
                           trainer,\
                           min_learning_rate,\
                           verbose,\
                           use_cache,\
                           cache_size,\
                           ret);\
\
    return error;\
}\
\
DLLEXPORT int batch_trainer_get_min_learning_rate_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                                 const matrix_element_type type,\
                                                                 const svm_batch_trainer_type trainer_type,\
                                                                 void* trainer,\
                                                                 __TYPE__* ret)\
{\
    int error = ERR_OK;\
\
    matrix_double_template(type,\
                           error,\
                           matrix_template_size00_template,\
                           batch_trainer_get_min_learning_rate_template,\
                           kernel_type,\
                           trainer_type,\
                           trainer,\
                           ret);\
\
    return error;\
}\
\
DLLEXPORT int batch_cached_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                          const matrix_element_type type,\
                                          const svm_batch_trainer_type trainer_type,\
                                          void* trainer,\
                                          const __TYPE__ min_learning_rate,\
                                          const int32_t cache_size,\
                                          void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_double_template(type,\
                           error,\
                           matrix_template_size00_template,\
                           batch_cached_template,\
                           kernel_type,\
                           trainer_type,\
                           trainer,\
                           min_learning_rate,\
                           cache_size,\
                           ret);\
\
    return error;\
}\
\
DLLEXPORT int verbose_batch_cached_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                  const matrix_element_type type,\
                                                  const svm_batch_trainer_type trainer_type,\
                                                  void* trainer,\
                                                  const __TYPE__ min_learning_rate,\
                                                  const int32_t cache_size,\
                                                  void** ret)\
{\
    int error = ERR_OK;\
\
    matrix_double_template(type,\
                           error,\
                           matrix_template_size00_template,\
                           verbose_batch_cached_template,\
                           kernel_type,\
                           trainer_type,\
                           trainer,\
                           min_learning_rate,\
                           cache_size,\
                           ret);\
\
    return error;\
}\

#pragma endregion

DLLEXPORT int batch_trainer_new(const svm_kernel_type kernel_type,
                                const matrix_element_type type,
                                const svm_batch_trainer_type trainer_type,
                                void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            batch_trainer_new_template,
                            kernel_type,
                            trainer_type,
                            ret);

    return error;
}

DLLEXPORT void batch_trainer_delete(const svm_kernel_type kernel_type,
                                    const matrix_element_type type,
                                    const svm_batch_trainer_type trainer_type,
                                    void* trainer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            batch_trainer_delete_template,
                            kernel_type,
                            trainer_type,
                            trainer);
}

DLLEXPORT int batch_trainer_train(const svm_kernel_type kernel_type,
                                  const matrix_element_type type,
                                  const svm_batch_trainer_type trainer_type,
                                  void* trainer,
                                  void* x,
                                  void* y,
                                  void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            batch_trainer_train_template,
                            kernel_type,
                            trainer_type,
                            trainer,
                            x,
                            y,
                            ret);

    return error;
}

MAKE_FUNC(double, double)
MAKE_FUNC(float, float)

#endif