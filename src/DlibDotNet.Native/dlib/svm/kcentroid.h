#ifndef _CPP_SVM_KCENTROID_H_
#define _CPP_SVM_KCENTROID_H_

#include "../export.h"
#include <dlib/svm/kcentroid.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define kcentroid_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
*ret = new dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(k, tolerance, max_dictionary_size, remove_oldest_first);

#define kcentroid_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_new_template_sub, __VA_ARGS__)

#define kcentroid_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)obj);

#define kcentroid_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_delete_template_sub, __VA_ARGS__)

#define kcentroid_get_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kcentroid);\
*ret = new KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(k->get_kernel());

#define kcentroid_get_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_get_kernel_template_sub, __VA_ARGS__)

#define kcentroid_dictionary_size_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kcentroid);\
*ret = k->dictionary_size();

#define kcentroid_dictionary_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_dictionary_size_template_sub, __VA_ARGS__)

#define kcentroid_operator_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto& kcentroid = *static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(obj);\
auto& s = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
*((__TYPE__*)ret) = kcentroid(s);

#define kcentroid_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_operator_template_sub, __VA_ARGS__)

#define kcentroid_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& kcentroid = *static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(obj);\
auto& s = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
kcentroid.train(s);

#define kcentroid_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kcentroid_train_template_sub, __VA_ARGS__)

#define MAKE_FUNC(__TTYPE__, __TTYPENAME__)\
DLLEXPORT int kcentroid_operator_##__TTYPENAME__(kernel_type kernel_type,\
                                                 matrix_element_type type,\
                                                 const int templateRows,\
                                                 const int templateColumns,\
                                                 void* obj,\
                                                 void* sample,\
                                                 __TTYPE__* ret)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size_column1or0_template,\
                            kcentroid_operator_template,\
                            templateRows,\
                            templateColumns,\
                            kernel_type,\
                            obj,\
                            sample,\
                            ret);\
\
    return error;\
}\
\
DLLEXPORT int kcentroid_train_##__TTYPENAME__(kernel_type kernel_type,\
                                              matrix_element_type type,\
                                              const int templateRows,\
                                              const int templateColumns,\
                                              void* obj,\
                                              void* sample)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size_column1or0_template,\
                            kcentroid_train_template,\
                            templateRows,\
                            templateColumns,\
                            kernel_type,\
                            obj,\
                            sample);\
\
    return error;\
}\

#pragma endregion

DLLEXPORT int kcentroid_new(kernel_type kernel_type,
                            matrix_element_type type,
                            const int templateRows,
                            const int templateColumns,
                            void* kernel,
                            double tolerance,
                            unsigned long max_dictionary_size,
                            bool remove_oldest_first,
                            void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kcentroid_new_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kernel,
                            tolerance,
                            max_dictionary_size,
                            remove_oldest_first,
                            ret);

    return error;
}

DLLEXPORT void kcentroid_delete(kernel_type kernel_type,
                                matrix_element_type type,
                                const int templateRows,
                                const int templateColumns,
                                void* obj)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kcentroid_delete_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            obj);
}

DLLEXPORT int kcentroid_get_kernel(kernel_type kernel_type,
                                   matrix_element_type type,
                                   const int templateRows,
                                   const int templateColumns,
                                   void* kcentroid,
                                   void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kcentroid_get_kernel_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kcentroid,
                            ret);

    return error;
}

DLLEXPORT int kcentroid_dictionary_size(kernel_type kernel_type,
                                        matrix_element_type type,
                                        const int templateRows,
                                        const int templateColumns,
                                        void* kcentroid,
                                        unsigned long* ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kcentroid_dictionary_size_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kcentroid,
                            ret);

    return error;
}

MAKE_FUNC(double, double)
MAKE_FUNC(float, float)

#endif