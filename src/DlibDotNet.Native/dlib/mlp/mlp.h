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
            {
                auto& mlp_kernel = *static_cast<mlp_kernel_c<mlp_kernel_1>*>(kernel);
                matrix<double>& mat = *(static_cast<matrix<double>*>(in));
                matrix<double> ret = mlp_kernel(mat);
                *ret_mat = new matrix<double>(ret);
            }
            break;
        default:
            err = ERR_MLP_KERNEL_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int mlp_kernel_train(mlp_kernel_type kernel_type, mlp::kernel_1a_c* kernel, void* example_in, double example_out)
{
    int err = ERR_OK;

    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            {
                auto mlp_kernel = static_cast<mlp_kernel_c<mlp_kernel_1>*>(kernel);
                matrix<double>& mat = *(static_cast<matrix<double>*>(example_in));
                mlp_kernel->train(mat, example_out);
            }
            break;
        default:
            err = ERR_MLP_KERNEL_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int mlp_kernel_train_matrix(mlp_kernel_type kernel_type, mlp::kernel_1a_c* kernel, void* example_in, void* example_out)
{
    int err = ERR_OK;

    switch(kernel_type)
    {
        case mlp_kernel_type::Kernel1:
            {
                auto mlp_kernel = static_cast<mlp_kernel_c<mlp_kernel_1>*>(kernel);
                matrix<double>& in = *(static_cast<matrix<double>*>(example_in));
                matrix<double>& out = *(static_cast<matrix<double>*>(example_out));
                mlp_kernel->train(in, out);
            }
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