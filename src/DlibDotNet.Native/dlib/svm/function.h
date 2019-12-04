#ifndef _CPP_SVM_FUNCTION_H_
#define _CPP_SVM_FUNCTION_H_

#include "../export.h"
#include <dlib/svm/function.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

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

#pragma endregion

DLLEXPORT void decision_function_delete(svm_kernel_type kernel_type,
                                        matrix_element_type type,
                                        const int templateRows,
                                        const int templateColumns,
                                        void* function)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            decision_function_delete_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            function);
}

DLLEXPORT int serialize_decision_function(svm_kernel_type kernel_type,
                                          matrix_element_type type,
                                          const int templateRows,
                                          const int templateColumns,
                                          void* function,
                                          const char* file_name,
                                          const int file_name_length,
                                          std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size_column1or0_template,
                                serialize_decision_function_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                function,
                                file_name,
                                file_name_length);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

DLLEXPORT int deserialize_decision_function(const char* file_name,
                                            const int file_name_length,
                                            svm_kernel_type kernel_type,
                                            matrix_element_type type,
                                            const int templateRows,
                                            const int templateColumns,
                                            void** ret,
                                            std::string** error_message)
{
    int error = ERR_OK;
    *ret = nullptr;

    try
    {
        matrix_decimal_template(type,
                                error,
                                matrix_template_size_column1or0_template,
                                deserialize_decision_function_template,
                                templateRows,
                                templateColumns,
                                kernel_type,
                                file_name,
                                file_name_length,
                                ret);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

#endif