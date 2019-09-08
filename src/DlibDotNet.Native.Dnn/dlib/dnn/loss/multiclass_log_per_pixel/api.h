#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_API_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_API_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMulticlassLogPerPixel.h"

using namespace dlib;
using namespace std;

extern std::map<int, LossMulticlassLogPerPixelBase*> LossMulticlassLogPerPixelRegistry;

DLLEXPORT int LossMulticlassLogPerPixel_new(const int id, void** ret)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->create(ret);
}

DLLEXPORT void LossMulticlassLogPerPixel_delete(const int id, void* obj)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return;

    return LossMulticlassLogPerPixelRegistry[id]->destroy(obj);
}
    
DLLEXPORT uint16_t LossMulticlassLogPerPixel_get_label_to_ignore()
{
    return loss_multiclass_log_per_pixel_::label_to_ignore;
}

DLLEXPORT int LossMulticlassLogPerPixel_operator_matrixs(const int id,
                                                         void* obj,
                                                         matrix_element_type element_type,
                                                         void* matrix_vector,
                                                         int templateRows,
                                                         int templateColumns,
                                                         size_t batch_size,
                                                         std::vector<loss_multiclass_log_per_pixel_out_type>** ret)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->operator_matrixs(obj,
                                                                   element_type,
                                                                   matrix_vector,
                                                                   templateRows,
                                                                   templateColumns,
                                                                   batch_size,
                                                                   ret);
}

DLLEXPORT int LossMulticlassLogPerPixel_deserialize(const int id,
                                                    const char* file_name,
                                                    void** ret,
                                                    std::string** error_message)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->deserialize(file_name,
                                                              ret,
                                                              error_message);
}

DLLEXPORT int LossMulticlassLogPerPixel_deserialize_proxy(const int id,
                                                          proxy_deserialize* proxy,
                                                          void** ret,
                                                          std::string** error_message)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->deserialize_proxy(proxy,
                                                                    ret,
                                                                    error_message);
}

DLLEXPORT int LossMulticlassLogPerPixel_serialize(const int id,
                                                  void* obj,
                                                  const char* file_name,
                                                  std::string** error_message)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->serialize(obj,
                                                            file_name,
                                                            error_message);
}

DLLEXPORT int LossMulticlassLogPerPixel_get_num_layers(const int id)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->get_num_layers();
}

DLLEXPORT int LossMulticlassLogPerPixel_layer_details_set_num_filters(const int id, void* layer, long num)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->layer_details_set_num_filters(layer, num);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_subnet(const int id, void* obj, void** subnet)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->get_subnet(obj, subnet);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_subnet_get_layer_details(const int id, void* subnet, void** ret)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    *ret = LossMulticlassLogPerPixelRegistry[id]->subnet_get_layer_details(subnet);
    return ERR_OK;
}

DLLEXPORT const dlib::tensor* LossMulticlassLogPerPixel_subnet_get_output(const int id,
                                                                          void* subnet,
                                                                          int* ret)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
    {
        *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
        return nullptr;
    }

    return LossMulticlassLogPerPixelRegistry[id]->subnet_get_output(subnet, ret);
}

DLLEXPORT int LossMulticlassLogPerPixel_clean(const int id, void* obj)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->clean(obj);
    return ERR_OK;
}

// fc_ does not have input_tensor_to_output_tensor
DLLEXPORT int LossMulticlassLogPerPixel_input_tensor_to_output_tensor(const int id, void* obj, dlib::dpoint* p, dlib::dpoint** ret)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->input_tensor_to_output_tensor(obj, p, ret);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_net_to_xml(const int id, void* obj, const char* filename)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->net_to_xml(obj, filename);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_operator_left_shift(const int id, void* trainer, std::ostringstream* stream)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_cloneAs(const int id, void* obj, const int dst_id, void** ret)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMulticlassLogPerPixelRegistry[id]->cloneAs(obj, dst_id, ret);
}

DLLEXPORT void* LossMulticlassLogPerPixel_trainer_new(const int id, void* net)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return nullptr;

    return LossMulticlassLogPerPixelRegistry[id]->trainer_new(net);
}

DLLEXPORT void* LossMulticlassLogPerPixel_trainer_new2(const int id, void* net, sgd* sgd)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return nullptr;

    return LossMulticlassLogPerPixelRegistry[id]->trainer_new_sgd(net, sgd);
}

DLLEXPORT void LossMulticlassLogPerPixel_trainer_delete(const int id, void* trainer)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return;

    LossMulticlassLogPerPixelRegistry[id]->trainer_delete(trainer);
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_learning_rate(const int id, void* trainer, const double lr)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_get_learning_rate(const int id, void* trainer, double* lr)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_get_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_get_average_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_get_average_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_get_average_test_loss(const int id, void* trainer, double* loss)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_get_average_test_loss(trainer, loss);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_min_learning_rate(const int id, void* trainer, const double lr)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_min_learning_rate(trainer, lr);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_mini_batch_size(const int id, void* trainer, const unsigned long size)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_mini_batch_size(trainer, size);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_be_verbose(const int id, void* trainer)\
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_be_verbose(trainer);
    return ERR_OK;
}


DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_synchronization_file(const int id,
                                                                         void* trainer, 
                                                                         const char* filename,
                                                                         const unsigned long second)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_synchronization_file(trainer, filename, second);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_iterations_without_progress_threshold(const int id, 
                                                                                          void* trainer,
                                                                                          const unsigned long thresh)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_set_test_iterations_without_progress_threshold(const int id,
                                                                                               void* trainer,
                                                                                               const unsigned long thresh)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_set_test_iterations_without_progress_threshold(trainer, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_get_net(const int id,
                                                        void* trainer,
                                                        void** ret)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogPerPixelRegistry[id]->trainer_get_net(trainer, ret);
    }
    catch(std::exception)
    {
        error = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return error;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_operator_left_shift(const int id,
                                                                    void* trainer,
                                                                    std::ostringstream* stream)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->trainer_operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_set_all_bn_running_stats_window_sizes(const int id,
                                                                              void* obj,
                                                                              unsigned long new_window_size)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->set_all_bn_running_stats_window_sizes(obj, new_window_size);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_subnet_delete(const int id, void* subnet)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMulticlassLogPerPixelRegistry[id]->subnet_delete(subnet);
    return ERR_OK;
}

DLLEXPORT int LossMulticlassLogPerPixel_trainer_test_one_step(const int id,
                                                              void* trainer,
                                                              matrix_element_type data_element_type,
                                                              void* data,
                                                              matrix_element_type label_element_type,
                                                              void* labels)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogPerPixelRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogPerPixelRegistry[id]->trainer_test_one_step(trainer,
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

DLLEXPORT int LossMulticlassLogPerPixel_trainer_train(const int id,
                                              void* trainer,
                                              matrix_element_type data_element_type,
                                              void* data,
                                              matrix_element_type label_element_type,
                                              void* labels)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogPerPixelRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogPerPixelRegistry[id]->trainer_train(trainer,
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

DLLEXPORT int LossMulticlassLogPerPixel_trainer_train_one_step(const int id,
                                                               void* trainer,
                                                               matrix_element_type data_element_type,
                                                               void* data,
                                                               matrix_element_type label_element_type,
                                                               void* labels)
{
    auto iter = LossMulticlassLogPerPixelRegistry.find(id);
    if (iter == end(LossMulticlassLogPerPixelRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMulticlassLogPerPixelRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMulticlassLogPerPixelRegistry[id]->trainer_train_one_step(trainer,
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