#ifndef _CPP_SVM_KRLS_H_
#define _CPP_SVM_KRLS_H_

#include "../export.h"
#include <dlib/svm/krls.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define krls_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
*ret = new dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(k, tolerance, max_dictionary_size);

#define krls_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_new_template_sub, __VA_ARGS__)

#define krls_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)obj);

#define krls_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_delete_template_sub, __VA_ARGS__)

#define krls_get_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(krls);\
*ret = new KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(k->get_kernel());

#define krls_get_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_get_kernel_template_sub, __VA_ARGS__)

#define krls_dictionary_size_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(krls);\
*ret = k->dictionary_size();

#define krls_dictionary_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_dictionary_size_template_sub, __VA_ARGS__)

#define krls_operator_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
const auto& krls = *static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(obj);\
auto& s = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
*((__TYPE__*)ret) = krls(s);

#define krls_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_operator_template_sub, __VA_ARGS__)

#define krls_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& krls = *static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(obj);\
auto& in_x = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(x);\
krls.train(in_x, y);

#define krls_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_train_template_sub, __VA_ARGS__)

#define MAKE_FUNC(__TTYPE__, __TTYPENAME__)\
DLLEXPORT int krls_operator_##__TTYPENAME__(svm_kernel_type kernel_type,\
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
                            krls_operator_template,\
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
DLLEXPORT int krls_train_##__TTYPENAME__(svm_kernel_type kernel_type,\
                                         matrix_element_type type,\
                                         const int templateRows,\
                                         const int templateColumns,\
                                         void* obj,\
                                         void* x,\
                                         __TTYPE__ y)\
{\
    int error = ERR_OK;\
\
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size_column1or0_template,\
                            krls_train_template,\
                            templateRows,\
                            templateColumns,\
                            kernel_type,\
                            obj,\
                            x,\
                            y);\
\
    return error;\
}\

#define serialize_krls_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(krls);\
dlib::serialize(file_name) << k;

#define serialize_krls_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, serialize_krls_template_sub, __VA_ARGS__)

#define deserialize_krls_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(krls);\
dlib::deserialize(file_name) >> k;\

#define deserialize_krls_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, deserialize_krls_template_sub, __VA_ARGS__)

#define krls_get_decision_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::krls<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(krls);\
auto r = k->get_decision_function();\
*ret = new decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(r);

#define krls_get_decision_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, krls_get_decision_function_template_sub, __VA_ARGS__)

#pragma endregion

DLLEXPORT int krls_new(svm_kernel_type kernel_type,
                       matrix_element_type type,
                       const int templateRows,
                       const int templateColumns,
                       void* kernel,
                       double tolerance,
                       unsigned long max_dictionary_size,
                       void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            krls_new_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kernel,
                            tolerance,
                            max_dictionary_size,
                            ret);

    return error;
}

DLLEXPORT void krls_delete(svm_kernel_type kernel_type,
                           matrix_element_type type,
                           const int templateRows,
                           const int templateColumns,
                           void* obj)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            krls_delete_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            obj);
}

DLLEXPORT int krls_get_kernel(svm_kernel_type kernel_type,
                              matrix_element_type type,
                              const int templateRows,
                              const int templateColumns,
                              void* krls,
                              void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            krls_get_kernel_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            krls,
                            ret);

    return error;
}

DLLEXPORT int krls_dictionary_size(svm_kernel_type kernel_type,
                                   matrix_element_type type,
                                   const int templateRows,
                                   const int templateColumns,
                                   void* krls,
                                   unsigned long* ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            krls_dictionary_size_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            krls,
                            ret);

    return error;
}

MAKE_FUNC(double, double)
MAKE_FUNC(float, float)

DLLEXPORT int serialize_krls(svm_kernel_type kernel_type,
                             matrix_element_type type,
                             const int templateRows,
                             const int templateColumns,
                             void* krls,
                             const char* file_name,
                             std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size_column1or0_template,
                                serialize_krls_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                krls,
                                file_name);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

DLLEXPORT int deserialize_krls(const char* file_name,
                               svm_kernel_type kernel_type,
                               matrix_element_type type,
                               const int templateRows,
                               const int templateColumns,
                               void* krls,
                               std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size_column1or0_template,
                                deserialize_krls_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                file_name,
                                krls);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

DLLEXPORT int krls_get_decision_function(svm_kernel_type kernel_type,
                                         matrix_element_type type,
                                         const int templateRows,
                                         const int templateColumns,
                                         void* krls,
                                         void** ret)
{
    int error = ERR_OK;
    *ret = nullptr;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            krls_get_decision_function_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            krls,
                            ret);

    return error;
}

#endif