#ifndef _CPP_LOSS_MMOD_API_H_
#define _CPP_LOSS_MMOD_API_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMmod.h"

using namespace dlib;
using namespace std;

extern std::map<int, LossMmodBase*> LossMmodRegistry;

DLLEXPORT int LossMmod_new(const int id, void** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->create(ret);
}

DLLEXPORT int LossMmod_new2(const int id, mmod_options* option, void** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->create2(option, ret);
}

DLLEXPORT void LossMmod_delete(const int id, void* obj)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return;

    return LossMmodRegistry[id]->destroy(obj);
}

DLLEXPORT int LossMmod_operator_matrixs(const int id,
                                        void* obj,
                                        matrix_element_type element_type,
                                        void* matrix_vector,
                                        int templateRows,
                                        int templateColumns,
                                        size_t batch_size,
                                        std::vector<loss_mmod_out_type>** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->operator_matrixs(obj,
                                                  element_type,
                                                  matrix_vector,
                                                  templateRows,
                                                  templateColumns,
                                                  batch_size,
                                                  ret);
}

DLLEXPORT int LossMmod_deserialize(const int id,
                                   const char* file_name,
                                   void** ret,
                                   std::string** error_message)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->deserialize(file_name,
                                             ret,
                                             error_message);
}

DLLEXPORT int LossMmod_deserialize_proxy(const int id,
                                         proxy_deserialize* proxy,
                                         void** ret,
                                         std::string** error_message)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->deserialize_proxy(proxy,
                                                   ret,
                                                   error_message);
}

DLLEXPORT int LossMmod_serialize(const int id,
                                 void* obj,
                                 const char* file_name,
                                 std::string** error_message)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->serialize(obj,
                                           file_name,
                                           error_message);
}

DLLEXPORT int LossMmod_get_input_layer(const int id, void* obj, void** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return LossMmodRegistry[id]->get_input_layer(obj, ret);
}

DLLEXPORT int LossMmod_get_num_layers(const int id)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMmodRegistry[id]->get_num_layers();
}

DLLEXPORT int LossMmod_layer_details_set_num_filters(const int id, void* layer, long num)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->layer_details_set_num_filters(layer, num);
    return ERR_OK;
}

DLLEXPORT int LossMmod_subnet(const int id, void* obj, void** subnet)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->get_subnet(obj, subnet);
    return ERR_OK;
}

DLLEXPORT int LossMmod_subnet_get_layer_details(const int id, void* subnet, void** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    *ret = LossMmodRegistry[id]->subnet_get_layer_details(subnet);
    return ERR_OK;
}

DLLEXPORT const dlib::tensor* LossMmod_subnet_get_output(const int id,
                                                         void* subnet,
                                                         int* ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
    {
        *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
        return nullptr;
    }

    return LossMmodRegistry[id]->subnet_get_output(subnet, ret);
}

DLLEXPORT int LossMmod_clean(const int id, void* obj)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->clean(obj);
    return ERR_OK;
}

// fc_ does not have input_tensor_to_output_tensor
DLLEXPORT int LossMmod_input_tensor_to_output_tensor(const int id, void* obj, dlib::dpoint* p, dlib::dpoint** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->input_tensor_to_output_tensor(obj, p, ret);
    return ERR_OK;
}

DLLEXPORT int LossMmod_net_to_xml(const int id, void* obj, const char* filename)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->net_to_xml(obj, filename);
    return ERR_OK;
}

DLLEXPORT int LossMmod_operator_left_shift(const int id, void* trainer, std::ostringstream* stream)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT void* LossMmod_trainer_new(const int id, void* net)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return nullptr;

    return LossMmodRegistry[id]->trainer_new(net);
}

DLLEXPORT void* LossMmod_trainer_new2(const int id, void* net, sgd* sgd)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return nullptr;

    return LossMmodRegistry[id]->trainer_new_sgd(net, sgd);
}

DLLEXPORT void LossMmod_trainer_delete(const int id, void* trainer)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return;

    LossMmodRegistry[id]->trainer_delete(trainer);
}

DLLEXPORT int LossMmod_trainer_set_learning_rate(const int id, void* trainer, const double lr)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_get_learning_rate(const int id, void* trainer, double* lr)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_get_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_get_average_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_get_average_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_get_average_test_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_get_average_test_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_set_min_learning_rate(const int id, void* trainer, const double lr)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_min_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_set_mini_batch_size(const int id, void* trainer, const unsigned long size)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_mini_batch_size(trainer, size);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_be_verbose(const int id, void* trainer)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_be_verbose(trainer);
    return ERR_OK;
}


DLLEXPORT int LossMmod_trainer_set_synchronization_file(const int id,
                                                          void* trainer, 
                                                          const char* filename,
                                                          const unsigned long second)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_synchronization_file(trainer, filename, second);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_set_iterations_without_progress_threshold(const int id, 
                                                                           void* trainer,
                                                                           const unsigned long thresh)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_set_test_iterations_without_progress_threshold(const int id,
                                                                                void* trainer,
                                                                                const unsigned long thresh)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_set_test_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_get_net(const int id,
                                         void* trainer,
                                         void** ret)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    int error = ERR_OK;

    try
    {
        LossMmodRegistry[id]->trainer_get_net(trainer, ret);
    }
    catch(std::exception)
    {
        error = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return error;
}

DLLEXPORT int LossMmod_trainer_operator_left_shift(const int id,
                                                     void* trainer,
                                                     std::ostringstream* stream)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->trainer_operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT int LossMmod_set_all_bn_running_stats_window_sizes(const int id,
                                                             void* obj,
                                                             unsigned long new_window_size)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->set_all_bn_running_stats_window_sizes(obj, new_window_size);
    return ERR_OK;
}

DLLEXPORT int LossMmod_subnet_delete(const int id, void* subnet)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMmodRegistry[id]->subnet_delete(subnet);
    return ERR_OK;
}

DLLEXPORT int LossMmod_trainer_test_one_step(const int id,
                                               void* trainer,
                                               matrix_element_type data_element_type,
                                               void* data,
                                               matrix_element_type label_element_type,
                                               void* labels)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMmodRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMmodRegistry[id]->trainer_test_one_step(trainer,
                                                      data_element_type,
                                                      data,
                                                      label_element_type,
                                                      labels);
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int LossMmod_trainer_train(const int id,
                                       void* trainer,
                                       matrix_element_type data_element_type,
                                       void* data,
                                       matrix_element_type label_element_type,
                                       void* labels)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMmodRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMmodRegistry[id]->trainer_train(trainer,
                                              data_element_type,
                                              data,
                                              label_element_type,
                                              labels);
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int LossMmod_trainer_train_one_step(const int id,
                                                void* trainer,
                                                matrix_element_type data_element_type,
                                                void* data,
                                                matrix_element_type label_element_type,
                                                void* labels)
{
    auto iter = LossMmodRegistry.find(id);
    if (iter == end(LossMmodRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMmodRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMmodRegistry[id]->trainer_train_one_step(trainer,
                                                     data_element_type,
                                                     data,
                                                     label_element_type,
                                                     labels);
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

#endif