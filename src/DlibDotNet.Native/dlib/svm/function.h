#ifndef _CPP_SVM_FUNCTION_H_
#define _CPP_SVM_FUNCTION_H_

#include "../export.h"
#include <dlib/svm/function.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#pragma region decision_function

#define decision_function_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)function);

#define decision_function_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, decision_function_delete_template_sub, __VA_ARGS__)

#define serialize_decision_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& df = *static_cast<dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(function);\
dlib::serialize(std::string(file_name, file_name_length)) << df;

#define serialize_decision_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, serialize_decision_function_template_sub, __VA_ARGS__)

#define deserialize_decision_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto df = new dlib::decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>();\
dlib::deserialize(std::string(file_name, file_name_length)) >> *df;\
*ret = df;

#define deserialize_decision_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, deserialize_decision_function_template_sub, __VA_ARGS__)

#pragma endregion decision_function

#pragma region probabilistic_decision_function

#define probabilistic_decision_function_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((dlib::probabilistic_decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)function);

#define probabilistic_decision_function_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, probabilistic_decision_function_delete_template_sub, __VA_ARGS__)

#define serialize_probabilistic_decision_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& df = *static_cast<dlib::probabilistic_decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(function);\
dlib::serialize(std::string(file_name, file_name_length)) << df;

#define serialize_probabilistic_decision_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, serialize_probabilistic_decision_function_template_sub, __VA_ARGS__)

#define deserialize_probabilistic_decision_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto df = new dlib::probabilistic_decision_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>();\
dlib::deserialize(std::string(file_name, file_name_length)) >> *df;\
*ret = df;

#define deserialize_probabilistic_decision_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, deserialize_probabilistic_decision_function_template_sub, __VA_ARGS__)

#pragma endregion probabilistic_decision_function

#pragma region projection_function

#define projection_function_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((dlib::projection_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)function);

#define projection_function_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, projection_function_delete_template_sub, __VA_ARGS__)

#define serialize_projection_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& df = *static_cast<dlib::projection_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(function);\
dlib::serialize(std::string(file_name, file_name_length)) << df;

#define serialize_projection_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, serialize_projection_function_template_sub, __VA_ARGS__)

#define deserialize_projection_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto df = new dlib::projection_function<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>();\
dlib::deserialize(std::string(file_name, file_name_length)) >> *df;\
*ret = df;

#define deserialize_projection_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, deserialize_projection_function_template_sub, __VA_ARGS__)

#pragma endregion projection_function

#pragma region function

#define MAKE_FUNCTION_FUNC(__FUNCTION_NAME__)\
DLLEXPORT void __FUNCTION_NAME__##_delete(svm_kernel_type kernel_type,\
                                          matrix_element_type type,\
                                          const int templateRows,\
                                          const int templateColumns,\
                                          void* function)\
{\
    int error = ERR_OK;\
    \
    matrix_decimal_template(type,\
                            error,\
                            matrix_template_size_column1or0_template,\
                            __FUNCTION_NAME__##_delete_template,\
                            templateRows,\
                            templateColumns,\
                            kernel_type,\
                            function);\
}\
\
DLLEXPORT int serialize_##__FUNCTION_NAME__(svm_kernel_type kernel_type,\
                                            matrix_element_type type,\
                                            const int templateRows,\
                                            const int templateColumns,\
                                            void* function,\
                                            const char* file_name,\
                                            const int file_name_length,\
                                            std::string** error_message)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        matrix_decimal_template(type,\
                                error,\
                                matrix_template_size_column1or0_template,\
                                serialize_##__FUNCTION_NAME__##_template,\
                                templateRows,\
                                templateColumns,\
                                kernel_type,\
                                function,\
                                file_name,\
                                file_name_length);\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
\
    return error;\
}\
\
DLLEXPORT int deserialize_##__FUNCTION_NAME__(const char* file_name,\
                                              const int file_name_length,\
                                              svm_kernel_type kernel_type,\
                                              matrix_element_type type,\
                                              const int templateRows,\
                                              const int templateColumns,\
                                              void** ret,\
                                              std::string** error_message)\
{\
    int error = ERR_OK;\
    *ret = nullptr;\
\
    try\
    {\
        matrix_decimal_template(type,\
                                error,\
                                matrix_template_size_column1or0_template,\
                                deserialize_##__FUNCTION_NAME__##_template,\
                                templateRows,\
                                templateColumns,\
                                kernel_type,\
                                file_name,\
                                file_name_length,\
                                ret);\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
\
    return error;\
}

#pragma endregion function

#pragma region normalized_function

#define normalized_function_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
ret = new dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>();

#define normalized_function_new_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_new_template_sub, __VA_ARGS__)

#define normalized_function_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_new_template_function_sub, __VA_ARGS__)

#define normalized_function_new_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_new_template_sub, __VA_ARGS__)

#define normalized_function_new_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_new_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
delete ((dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*)function);

#define normalized_function_delete_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_delete_template_sub, __VA_ARGS__)

#define normalized_function_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_delete_template_function_sub, __VA_ARGS__)

#define normalized_function_delete_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_delete_template_sub, __VA_ARGS__)

#define normalized_function_delete_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_delete_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_set_normalizer_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
auto f = static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
switch(normalizer_type)\
{\
    case normalizer_type::Vector:\
        {\
            const auto& tmp_normalizer = *static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
            f->normalizer = tmp_normalizer;\
        }\
        break;\
}\

#define normalized_function_set_normalizer_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_set_normalizer_template_sub, __VA_ARGS__)

#define normalized_function_set_normalizer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_set_normalizer_template_function_sub, __VA_ARGS__)

#define normalized_function_set_normalizer_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_set_normalizer_template_sub, __VA_ARGS__)

#define normalized_function_set_normalizer_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_set_normalizer_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_get_normalizer_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
auto f = static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
auto n = &(f->normalizer);\
*ret = n;\

#define normalized_function_get_normalizer_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_get_normalizer_template_sub, __VA_ARGS__)

#define normalized_function_get_normalizer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_get_normalizer_template_function_sub, __VA_ARGS__)

#define normalized_function_get_normalizer_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_get_normalizer_template_sub, __VA_ARGS__)

#define normalized_function_get_normalizer_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_get_normalizer_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_get_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
auto f = static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
*ret = &(f->function);\

#define normalized_function_get_function_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_get_function_template_sub, __VA_ARGS__)

#define normalized_function_get_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_get_function_template_function_sub, __VA_ARGS__)

#define normalized_function_get_function_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_get_function_template_sub, __VA_ARGS__)

#define normalized_function_get_function_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_get_function_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_set_function_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
auto f = static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
const auto& tmp_function = *static_cast<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(sub_function);\
f->function = tmp_function;\

#define normalized_function_set_function_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_set_function_template_sub, __VA_ARGS__)

#define normalized_function_set_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_set_function_template_function_sub, __VA_ARGS__)

#define normalized_function_set_function_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_set_function_template_sub, __VA_ARGS__)

#define normalized_function_set_function_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_set_function_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_operator_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
auto f = static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
const auto& s = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
*ret = f->operator()(s);\

#define normalized_function_operator_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_operator_template_sub, __VA_ARGS__)

#define normalized_function_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_operator_template_function_sub, __VA_ARGS__)

#define normalized_function_operator_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_operator_template_sub, __VA_ARGS__)

#define normalized_function_operator_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_operator_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_deserialize_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
try\
{\
    std::string str(file_name, file_name_length);\
    auto function = new dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>();\
    dlib::deserialize(str) >> (*function);\
    *ret = function;\
}\
catch (serialization_error& e)\
{\
    error = ERR_GENERAL_SERIALIZATION;\
    *error_message = new std::string(e.what());\
}\

#define normalized_function_deserialize_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
/* dlib::distance_function members are private. check dlib/svm/function.h(539)*/ \
function_no_linear_template2(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_deserialize_template_sub, __VA_ARGS__)

#define normalized_function_deserialize_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_deserialize_template_function_sub, __VA_ARGS__)

#define normalized_function_deserialize_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_deserialize_template_sub, __VA_ARGS__)

#define normalized_function_deserialize_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_deserialize_template_linear_function_sub, __VA_ARGS__)

#define normalized_function_serialize_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, FUNC, ...) \
try\
{\
    const auto& f = *static_cast<dlib::normalized_function<FUNC<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>>*>(function);\
    dlib::serialize(std::string(file_name, file_name_length)) << f;\
}\
catch (serialization_error& e)\
{\
    error = ERR_GENERAL_SERIALIZATION;\
    *error_message = new std::string(e.what());\
}\

#define normalized_function_serialize_template_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
/* dlib::distance_function members are private. check dlib/svm/function.h(539)*/ \
function_no_linear_template2(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_serialize_template_sub, __VA_ARGS__)

#define normalized_function_serialize_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_no_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_serialize_template_function_sub, __VA_ARGS__)

#define normalized_function_serialize_template_linear_function_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
function_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, function_type, normalized_function_serialize_template_sub, __VA_ARGS__)

#define normalized_function_serialize_template_linear(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_linear_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, normalized_function_serialize_template_linear_function_sub, __VA_ARGS__)

#define MAKE_NORMALIZED_FUNCTION_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int normalized_function_operator_##__TYPENAME__(const svm_kernel_type kernel_type,\
                                                          const matrix_element_type type,\
                                                          const int templateRows,\
                                                          const int templateColumns,\
                                                          const svm_function_type function_type,\
                                                          void* function,\
                                                          void* sample,\
                                                          __TYPE__* ret)\
{\
    int error = ERR_OK;\
\
    if (function_type == svm_function_type::MulticlassLinearDecision)\
    {\
        matrix_decimal_template(type,\
                                error,\
                                matrix_template_size00_template,\
                                normalized_function_operator_template_linear,\
                                templateRows,\
                                templateColumns,\
                                kernel_type,\
                                function_type,\
                                function,\
                                sample,\
                                ret);\
    }\
    else\
    {\
        matrix_decimal_template(type,\
                                error,\
                                matrix_template_size00_template,\
                                normalized_function_operator_template,\
                                templateRows,\
                                templateColumns,\
                                kernel_type,\
                                function_type,\
                                function,\
                                sample,\
                                ret);\
    }\
\
    return error;\
}\

#pragma endregion normalized_function

#pragma endregion

#pragma region function

MAKE_FUNCTION_FUNC(decision_function)
MAKE_FUNCTION_FUNC(probabilistic_decision_function)
MAKE_FUNCTION_FUNC(projection_function)

#pragma endregion function

#pragma region normalized_function

DLLEXPORT void* normalized_function_new(const svm_kernel_type kernel_type,
                                        const matrix_element_type type,
                                        const int templateRows,
                                        const int templateColumns,
                                        const svm_function_type function_type)
{
    int error = ERR_OK;
    void* ret = nullptr;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_new_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                ret);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_new_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                ret);
    }

    return ret;
}

DLLEXPORT void normalized_function_delete(const svm_kernel_type kernel_type,
                                          const matrix_element_type type,
                                          const int templateRows,
                                          const int templateColumns,
                                          const svm_function_type function_type,
                                          void* function)
{
    int error = ERR_OK;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_delete_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_delete_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function);
    }
}

DLLEXPORT int normalized_function_get_normalizer(const svm_kernel_type kernel_type,
                                                 const matrix_element_type type,
                                                 const int templateRows,
                                                 const int templateColumns,
                                                 const svm_function_type function_type,
                                                 void* function,
                                                 void** ret)
{
    int error = ERR_OK;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_get_normalizer_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                ret);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_get_normalizer_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                ret);
    }

    return error;
}

DLLEXPORT int normalized_function_set_normalizer(const svm_kernel_type kernel_type,
                                                 const matrix_element_type type,
                                                 const int templateRows,
                                                 const int templateColumns,
                                                 const svm_function_type function_type,
                                                 void* function,
                                                 const normalizer_type normalizer_type,
                                                 void* normalizer)
{
    int error = ERR_OK;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_set_normalizer_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                normalizer_type,
                                normalizer);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_set_normalizer_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                normalizer_type,
                                normalizer);
    }

    return error;
}

DLLEXPORT int normalized_function_get_function(const svm_kernel_type kernel_type,
                                               const matrix_element_type type,
                                               const int templateRows,
                                               const int templateColumns,
                                               const svm_function_type function_type,
                                               void* function,
                                               void** ret)
{
    int error = ERR_OK;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_get_function_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                ret);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_get_function_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                ret);
    }

    return error;
}

DLLEXPORT int normalized_function_set_function(const svm_kernel_type kernel_type,
                                               const matrix_element_type type,
                                               const int templateRows,
                                               const int templateColumns,
                                               const svm_function_type function_type,
                                               void* function,
                                               void* sub_function)
{
    int error = ERR_OK;
    
    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_set_function_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                sub_function);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_set_function_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                sub_function);
    }

    return error;
}

DLLEXPORT int normalized_function_deserialize(const svm_kernel_type kernel_type,
                                              const matrix_element_type type,
                                              const int templateRows,
                                              const int templateColumns,
                                              const svm_function_type function_type,
                                              const char* file_name,
                                              const int file_name_length,
                                              void** ret,
                                              std::string** error_message)
{
    int error = ERR_OK;

    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_deserialize_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                file_name,
                                file_name_length,
                                ret,
                                error_message);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_deserialize_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                file_name,
                                file_name_length,
                                ret,
                                error_message);
    }

    return error;
}

DLLEXPORT int normalized_function_serialize(const svm_kernel_type kernel_type,
                                            const matrix_element_type type,
                                            const int templateRows,
                                            const int templateColumns,
                                            const svm_function_type function_type,
                                            void* function,
                                            const char* file_name,
                                            const int file_name_length,
                                            std::string** error_message)
{
    int error = ERR_OK;

    if (function_type == svm_function_type::MulticlassLinearDecision)
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_serialize_template_linear,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                file_name,
                                file_name_length,
                                error_message);
    }
    else
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size00_template,
                                normalized_function_serialize_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function_type,
                                function,
                                file_name,
                                file_name_length,
                                error_message);
    }

    return error;
}

MAKE_NORMALIZED_FUNCTION_FUNC(int8_t, int8_t)
MAKE_NORMALIZED_FUNCTION_FUNC(uint8_t, uint8_t)
MAKE_NORMALIZED_FUNCTION_FUNC(int16_t, int16_t)
MAKE_NORMALIZED_FUNCTION_FUNC(uint16_t, uint16_t)
MAKE_NORMALIZED_FUNCTION_FUNC(int32_t, int32_t)
MAKE_NORMALIZED_FUNCTION_FUNC(uint32_t, uint32_t)
MAKE_NORMALIZED_FUNCTION_FUNC(int64_t, int64_t)
MAKE_NORMALIZED_FUNCTION_FUNC(uint64_t, uint64_t)
MAKE_NORMALIZED_FUNCTION_FUNC(double, double)
MAKE_NORMALIZED_FUNCTION_FUNC(float, float)

#pragma endregion normalized_function

#endif