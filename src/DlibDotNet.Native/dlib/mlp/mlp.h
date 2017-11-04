#ifndef _CPP_MLP_H_
#define _CPP_MLP_H_

#include "../export.h"
#include <dlib/matrix/matrix.h>
#include <dlib/mlp.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

/*
typedef mlp_kernel_1 kernel_1a;
typedef dlib::mlp_kernel_c<kernel_1a> kernel_1a_c; 
*/

#pragma region template

#define KERNEL_ELEMENT element
#undef KERNEL_ELEMENT

#define mlp_kernel_operator_template(err, ret_mat, kernel, type, in)\
do { \
    err = ERR_OK;\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<uint8_t>& mat = *(static_cast<matrix<uint8_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::UInt16:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<uint16_t>& mat = *(static_cast<matrix<uint16_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::UInt32:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<uint32_t>& mat = *(static_cast<matrix<uint32_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::Int8:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<int8_t>& mat = *(static_cast<matrix<int8_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::Int16:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<int16_t>& mat = *(static_cast<matrix<int16_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::Int32:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<int32_t>& mat = *(static_cast<matrix<int32_t>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::Float:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<float>& mat = *(static_cast<matrix<float>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::Double:\
            {\
                mlp_kernel_c<KERNEL_ELEMENT>& mlp_kernel = *(static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel));\
                matrix<double>& mat = *(static_cast<matrix<double>*>(in));\
                matrix<double> ret = mlp_kernel(mat);\
                *ret_mat = new matrix<double>(ret);\
            }\
            break;\
        case matrix_element_type::RgbPixel:\
        case matrix_element_type::HsiPixel:\
        case matrix_element_type::RgbAlphaPixel:\
        default:\
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#define mlp_kernel_train_template(ret, kernel, type, example_in, example_out)\
do { \
    err = ERR_OK;\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<uint8_t>& mat = *(static_cast<matrix<uint8_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::UInt16:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<uint16_t>& mat = *(static_cast<matrix<uint16_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::UInt32:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<uint32_t>& mat = *(static_cast<matrix<uint32_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::Int8:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<int8_t>& mat = *(static_cast<matrix<int8_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::Int16:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<int16_t>& mat = *(static_cast<matrix<int16_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::Int32:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<int32_t>& mat = *(static_cast<matrix<int32_t>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::Float:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<float>& mat = *(static_cast<matrix<float>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::Double:\
            {\
                auto mlp_kernel = static_cast<mlp_kernel_c<KERNEL_ELEMENT>*>(kernel);\
                matrix<double>& mat = *(static_cast<matrix<double>*>(example_in));\
                mlp_kernel->train(mat, example_out);\
            }\
            break;\
        case matrix_element_type::RgbPixel:\
        case matrix_element_type::HsiPixel:\
        case matrix_element_type::RgbAlphaPixel:\
        default:\
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT void* mlp_kernel_new(
    mlp_kernel_type kernel_type,
    int nodes_in_input_layer,
    int nodes_in_first_hidden_layer, 
    int nodes_in_second_hidden_layer, 
    int nodes_in_output_layer,
    double alpha,
    double momentum)
{
    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            return new mlp_kernel_c<mlp_kernel_1>(
                nodes_in_input_layer,
                nodes_in_first_hidden_layer, 
                nodes_in_second_hidden_layer, 
                nodes_in_output_layer,
                alpha,
                momentum);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT int mlp_kernel_operator(mlp_kernel_type kernel_type, mlp::kernel_1a_c* kernel, matrix_element_type type, void* in, matrix<double>** ret_mat)
{
    int err = ERR_OK;

    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            #define KERNEL_ELEMENT mlp_kernel_1
            mlp_kernel_operator_template(err, ret_mat, kernel, type, in);
            #undef KERNEL_ELEMENT
            break;
        default:
            err = ERR_MLP_KERNEL_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int mlp_kernel_train(mlp_kernel_type kernel_type, mlp::kernel_1a_c* kernel, matrix_element_type type, void* example_in, double example_out)
{
    int err = ERR_OK;

    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            #define KERNEL_ELEMENT mlp_kernel_1
            mlp_kernel_train_template(err, kernel, type, example_in, example_out);
            #undef KERNEL_ELEMENT
            break;
        default:
            err = ERR_MLP_KERNEL_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void mlp_kernel_delete(mlp_kernel_type kernel_type, mlp::kernel_1a_c* kernel)
{
    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            {
                mlp_kernel_c<mlp_kernel_1>* k = static_cast<mlp_kernel_c<mlp_kernel_1>*>(kernel);
                delete k;
            }
            break;
    }
}

#endif