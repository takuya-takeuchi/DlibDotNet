#ifndef _CPP_LOSS_MULTICLASS_LOG_API_H_
#define _CPP_LOSS_MULTICLASS_LOG_API_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMulticlassLog.h"
#include "template.h"

using namespace dlib;
using namespace std;

extern std::map<int, LossMulticlassLogBase*> LossMulticlassLogRegistry;

MAKE_LOSSMULTICLASSLOG_FUNC(net_type,       matrix_element_type::UInt8,    uint8_t,   matrix_element_type::UInt32, loss_multiclass_log_train_label_type, 0)
MAKE_LOSSMULTICLASSLOG_FUNC(net_1000_type,  matrix_element_type::RgbPixel, rgb_pixel, matrix_element_type::UInt32, loss_multiclass_log_train_label_type, 1)
MAKE_LOSSMULTICLASSLOG_FUNC(anet_1000_type, matrix_element_type::RgbPixel, rgb_pixel, matrix_element_type::UInt32, loss_multiclass_log_train_label_type, 2)
MAKE_LOSSMULTICLASSLOG_FUNC(net_type2,      matrix_element_type::UInt8,    uint8_t,   matrix_element_type::UInt32, loss_multiclass_log_train_label_type, 3)

DLLEXPORT int LossMulticlassLog_new(const int id, void** ret)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->create(ret);
}

DLLEXPORT void LossMulticlassLog_delete(const int id, void* obj)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return;

    return LossMulticlassLogRegistry[id]->destroy(obj);
}

DLLEXPORT int LossMulticlassLog_operator_matrixs(const int id,
                                                 void* obj,
                                                 matrix_element_type element_type,
                                                 void* matrix_vector,
                                                 int templateRows,
                                                 int templateColumns,
                                                 size_t batch_size,
                                                 std::vector<loss_multiclass_log_out_type>** ret)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->operator_matrixs(obj,
                                                           element_type,
                                                           matrix_vector,
                                                           templateRows,
                                                           templateColumns,
                                                           batch_size,
                                                           ret);
}

DLLEXPORT int LossMulticlassLog_deserialize(const int id,
                                            const char* file_name,
                                            void** ret,
                                            std::string** error_message)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->deserialize(file_name,
                                                      ret,
                                                      error_message);
}

DLLEXPORT int LossMulticlassLog_deserialize_proxy(const int id,
                                                  proxy_deserialize* proxy,
                                                  void** ret,
                                                  std::string** error_message)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->deserialize_proxy(proxy,
                                                            ret,
                                                            error_message);
}

DLLEXPORT int LossMulticlassLog_serialize(const int id,
                                          void* obj,
                                          const char* file_name,
                                          std::string** error_message)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->serialize(obj,
                                                    file_name,
                                                    error_message);
}

DLLEXPORT int LossMulticlassLog_get_num_layers(const int id)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogRegistry[id]->get_num_layers();
}

DLLEXPORT int LossMulticlassLog_layer_details_set_num_filters(const int id, void* layer, long num)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->layer_details_set_num_filters(layer, num);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_subnet(const int id, void* obj, void** subnet)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->get_subnet(obj, subnet);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_subnet_get_layer_details(const int id, void* subnet, void** ret)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    *ret = LossMulticlassLogRegistry[id]->subnet_get_layer_details(subnet);
    return ERR_OK;
}

DLLEXPORT const dlib::tensor* LossMulticlassLog_subnet_get_output(const int id,
                                                                  void* subnet,
                                                                  int* ret)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
    {
        *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
        return nullptr;
    }

    return LossMulticlassLogRegistry[id]->subnet_get_output(subnet, ret);
}

DLLEXPORT int LossMulticlassLog_clean(const int id, void* obj)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->clean(obj);
    return ERR_OK;
}

// fc_ does not have input_tensor_to_output_tensor
DLLEXPORT int LossMulticlassLog_input_tensor_to_output_tensor(const int id, void* obj, dlib::dpoint* p, dlib::dpoint** ret)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->input_tensor_to_output_tensor(obj, p, ret);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_net_to_xml(const int id, void* obj, const char* filename)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->net_to_xml(obj, filename);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_operator_left_shift(const int id, void* trainer, std::ostringstream* stream)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT void* LossMulticlassLog_trainer_new(const int id, void* net)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return nullptr;

    return LossMulticlassLogRegistry[id]->trainer_new(net);
}

DLLEXPORT void* LossMulticlassLog_trainer_new2(const int id, void* net, sgd* sgd)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return nullptr;

    return LossMulticlassLogRegistry[id]->trainer_new_sgd(net, sgd);
}

DLLEXPORT void LossMulticlassLog_trainer_delete(const int id, void* trainer)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return;

    LossMulticlassLogRegistry[id]->trainer_delete(trainer);
}

DLLEXPORT int LossMulticlassLog_trainer_set_learning_rate(const int id, void* trainer, const double lr)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_get_learning_rate(const int id, void* trainer, double* lr)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_get_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_get_average_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_get_average_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_get_average_test_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_get_average_test_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_set_min_learning_rate(const int id, void* trainer, const double lr)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_min_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_set_mini_batch_size(const int id, void* trainer, const unsigned long size)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_mini_batch_size(trainer, size);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_be_verbose(const int id, void* trainer)\
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_be_verbose(trainer);
    return ERR_OK;
}


DLLEXPORT int LossMulticlassLog_trainer_set_synchronization_file(const int id,
                                                                 void* trainer, 
                                                                 const char* filename,
                                                                 const unsigned long second)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_synchronization_file(trainer, filename, second);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_set_iterations_without_progress_threshold(const int id, 
                                                                                  void* trainer,
                                                                                  const unsigned long thresh)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_set_test_iterations_without_progress_threshold(const int id,
                                                                                       void* trainer,
                                                                                       const unsigned long thresh)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_set_test_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_get_net(const int id,
                                                void* trainer,
                                                void** ret)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogRegistry[id]->trainer_get_net(trainer, ret);
    }
    catch(std::exception)
    {
        error = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return error;
}

DLLEXPORT int LossMulticlassLog_trainer_operator_left_shift(const int id,
                                                            void* trainer,
                                                            std::ostringstream* stream)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->trainer_operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_set_all_bn_running_stats_window_sizes(const int id,
                                                                      void* obj,
                                                                      unsigned long new_window_size)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->set_all_bn_running_stats_window_sizes(obj, new_window_size);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_subnet_delete(const int id, void* subnet)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogRegistry[id]->subnet_delete(subnet);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLog_trainer_test_one_step(const int id,
                                                      void* trainer,
                                                      matrix_element_type data_element_type,
                                                      void* data,
                                                      matrix_element_type label_element_type,
                                                      void* labels)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogRegistry[id]->trainer_test_one_step(trainer,
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

DLLEXPORT int LossMulticlassLog_trainer_train(const int id,
                                              void* trainer,
                                              matrix_element_type data_element_type,
                                              void* data,
                                              matrix_element_type label_element_type,
                                              void* labels)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogRegistry[id]->trainer_train(trainer,
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

DLLEXPORT int LossMulticlassLog_trainer_train_one_step(const int id,
                                                       void* trainer,
                                                       matrix_element_type data_element_type,
                                                       void* data,
                                                       matrix_element_type label_element_type,
                                                       void* labels)
{
    auto iter = LossMulticlassLogRegistry.find(id);
    if (iter == end(LossMulticlassLogRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogRegistry[id]->trainer_train_one_step(trainer,
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