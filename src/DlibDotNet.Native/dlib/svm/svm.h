#ifndef _CPP_SVM_H_
#define _CPP_SVM_H_

#include "../export.h"
#include <dlib/svm/svm.h>
#include <dlib/svm/svm_c_trainer.h>
#include <dlib/svm/pegasos.h>
#include <dlib/svm/reduced.h>
#include "template.h"
#include "../template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define cross_validate_trainer_svm_trainer_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
auto r = dlib::cross_validate_trainer(t, dst_x, dst_y, folds);\
*ret = new dlib::matrix<double, 1, 2>(r);

#define cross_validate_trainer_svm_trainer_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, cross_validate_trainer_svm_trainer_template_sub, __VA_ARGS__)

#define cross_validate_trainer_svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, cross_validate_trainer_svm_trainer_template_trainer_sub, __VA_ARGS__)

#define cross_validate_trainer_batch_trainer_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<dlib::batch_trainer<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
auto r = dlib::cross_validate_trainer(t, dst_x, dst_y, folds);\
*ret = new dlib::matrix<double, 1, 2>(r);

#define cross_validate_trainer_batch_trainer_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, cross_validate_trainer_batch_trainer_template_sub, __VA_ARGS__)

#define cross_validate_trainer_batch_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_histgram_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, cross_validate_trainer_batch_trainer_template_trainer_sub, __VA_ARGS__)

#define cross_validate_trainer_reduced_decision_function_trainer2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
auto r = dlib::cross_validate_trainer(t, dst_x, dst_y, folds);\
*ret = new dlib::matrix<double, 1, 2>(r);

#define cross_validate_trainer_reduced_decision_function_trainer2_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, cross_validate_trainer_reduced_decision_function_trainer2_template_sub, __VA_ARGS__)

#define cross_validate_trainer_reduced_decision_function_trainer2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_histgram_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, cross_validate_trainer_reduced_decision_function_trainer2_template_trainer_sub, __VA_ARGS__)

#define train_probabilistic_decision_function_svm_trainer_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
auto r = dlib::train_probabilistic_decision_function(t, dst_x, dst_y, folds);\
*ret = new dlib::probabilistic_decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(r);\

#define train_probabilistic_decision_function_svm_trainer_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, train_probabilistic_decision_function_svm_trainer_template_sub, __VA_ARGS__)

#define train_probabilistic_decision_function_svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, train_probabilistic_decision_function_svm_trainer_template_trainer_sub, __VA_ARGS__)

#define train_probabilistic_decision_function_reduced_decision_function_trainer2_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, TRAINER, ...) \
const auto& t = *static_cast<dlib::reduced_decision_function_trainer2<TRAINER<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(trainer);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> dst_x;\
std::vector<__TYPE__> dst_y;\
convert_sample_and_labels(__TYPE__, __ROWS__, __COLUMNS__, x, y, dst_x, dst_y)\
auto r = dlib::train_probabilistic_decision_function(t, dst_x, dst_y, folds);\
*ret = new dlib::matrix<double, 1, 2>(r);

#define train_probabilistic_decision_function_reduced_decision_function_trainer2_template_trainer_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
svm_trainer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, trainer_type, train_probabilistic_decision_function_reduced_decision_function_trainer2_template_sub, __VA_ARGS__) 

#define train_probabilistic_decision_function_reduced_decision_function_trainer2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_histgram_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, train_probabilistic_decision_function_reduced_decision_function_trainer2_template_trainer_sub, __VA_ARGS__) 

#pragma endregion

DLLEXPORT int cross_validate_trainer_svm_trainer(const svm_kernel_type kernel_type,
                                                 const matrix_element_type type,
                                                 const svm_trainer_type trainer_type,
                                                 void* trainer,
                                                 const int32_t templateRows,
                                                 const int32_t templateColumns,
                                                 void* x,
                                                 void* y,
                                                 const int32_t folds,
                                                 void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            cross_validate_trainer_svm_trainer_template,
                            kernel_type,
                            trainer_type,
                            trainer,
                            x,
                            y,
                            folds,
                            ret);

    return error;
}

DLLEXPORT int cross_validate_trainer_batch_trainer(const svm_kernel_type kernel_type,
                                                   const matrix_element_type type,
                                                   const svm_batch_trainer_type trainer_type,
                                                   void* trainer,
                                                   const int32_t templateRows,
                                                   const int32_t templateColumns,
                                                   void* x,
                                                   void* y,
                                                   const int32_t folds,
                                                   void** ret)
{
    int error = ERR_OK;
    
    matrix_double_template(type,
                           error,
                           matrix_template_size00_template,
                           cross_validate_trainer_batch_trainer_template,
                           kernel_type,
                           trainer_type,
                           trainer,
                           x,
                           y,
                           folds,
                           ret);

    return error;
}

DLLEXPORT int cross_validate_trainer_reduced_decision_function_trainer2(const svm_kernel_type kernel_type,
                                                                        const matrix_element_type type,
                                                                        const svm_trainer_type trainer_type,
                                                                        void* trainer,
                                                                        const int32_t templateRows,
                                                                        const int32_t templateColumns,
                                                                        void* x,
                                                                        void* y,
                                                                        const int32_t folds,
                                                                        void** ret)
{
    int error = ERR_OK;
    
    matrix_double_template(type,
                           error,
                           matrix_template_size00_template,
                           cross_validate_trainer_reduced_decision_function_trainer2_template,
                           kernel_type,
                           trainer_type,
                           trainer,
                           x,
                           y,
                           folds,
                           ret);

    return error;
}

DLLEXPORT int randomize_samples_pointer(void* vector)
{
    int error = ERR_OK;
    
    auto& v = *static_cast<std::vector<void*>*>(vector);
    dlib::randomize_samples(v);

    return error;
}

DLLEXPORT int randomize_samples_value(void* vector)
{
    int error = ERR_OK;
    
    auto& v = *static_cast<std::vector<int>*>(vector);
    dlib::randomize_samples(v);

    return error;
}

DLLEXPORT int train_probabilistic_decision_function_svm_trainer(const svm_kernel_type kernel_type,
                                                                const matrix_element_type type,
                                                                const svm_trainer_type trainer_type,
                                                                void* trainer,
                                                                const int32_t templateRows,
                                                                const int32_t templateColumns,
                                                                void* x,
                                                                void* y,
                                                                const int32_t folds,
                                                                void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            train_probabilistic_decision_function_svm_trainer_template,
                            kernel_type,
                            trainer_type,
                            trainer,
                            x,
                            y,
                            folds,
                            ret);

    return error;
}
 
DLLEXPORT int train_probabilistic_decision_function_reduced_decision_function_trainer2(const svm_kernel_type kernel_type, 
                                                                                       const matrix_element_type type, 
                                                                                       const svm_trainer_type trainer_type, 
                                                                                       void* trainer, 
                                                                                       const int32_t templateRows, 
                                                                                       const int32_t templateColumns, 
                                                                                       void* x, 
                                                                                       void* y, 
                                                                                       const int32_t folds, 
                                                                                       void** ret) 
{ 
    int error = ERR_OK; 
     
    matrix_double_template(type, 
                           error, 
                           matrix_template_size00_template, 
                           cross_validate_trainer_reduced_decision_function_trainer2_template, 
                           kernel_type, 
                           trainer_type, 
                           trainer, 
                           x, 
                           y, 
                           folds, 
                           ret); 
 
    return error; 
}

#endif