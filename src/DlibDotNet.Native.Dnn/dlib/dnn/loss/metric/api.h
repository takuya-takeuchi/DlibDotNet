#ifndef _CPP_LOSS_METRIC_API_H_
#define _CPP_LOSS_METRIC_API_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMetric.h"
#include "template.h"

using namespace dlib;
using namespace std;

extern std::map<int, LossMetricBase*> LossMetricRegistry;

MAKE_LOSSMETRIC_FUNC(anet_type,       matrix_element_type::RgbPixel, rgb_pixel, 0, 0, matrix_element_type::UInt32, loss_metric_train_label_type, 0)
MAKE_LOSSMETRIC_FUNC(metric_net_type, matrix_element_type::Double,   double,    0, 1, matrix_element_type::UInt32, loss_metric_train_label_type, 1)

DLLEXPORT int LossMetric_new(const int id, void** ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->create(ret);
}

DLLEXPORT void LossMetric_delete(const int id, void* obj)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return;

    return LossMetricRegistry[id]->destroy(obj);
}

DLLEXPORT int LossMetric_operator_matrixs(const int id,
                                          void* obj,
                                          const matrix_element_type element_type,
                                          void* matrix_array,
                                          const int matrix_array_len,
                                          const int templateRows,
                                          const int templateColumns,
                                          const uint32_t batch_size,
                                          std::vector<loss_metric_out_type>** ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->operator_matrixs(obj,
                                                    element_type,
                                                    matrix_array,
                                                    matrix_array_len,
                                                    templateRows,
                                                    templateColumns,
                                                    batch_size,
                                                    ret);
}

DLLEXPORT int LossMetric_deserialize(const int id,
                                     const char* file_name,
                                     const int file_name_length,
                                     void** ret,
                                     std::string** error_message)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->deserialize(file_name,
                                               file_name_length,
                                               ret,
                                               error_message);
}

DLLEXPORT int LossMetric_deserialize2(const int id,
                                      const char* item,
                                      const int item_length,
                                      void** ret,
                                      std::string** error_message)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->deserialize2(item,
                                                item_length,
                                                ret,
                                                error_message);
}

DLLEXPORT int LossMetric_deserialize_proxy(const int id,
                                           proxy_deserialize* proxy,
                                           void** ret,
                                           std::string** error_message)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->deserialize_proxy(proxy,
                                                     ret,
                                                     error_message);
}

DLLEXPORT int LossMetric_serialize(const int id,
                                   void* obj,
                                   const char* file_name,
                                   const int file_name_length,
                                   std::string** error_message)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->serialize(obj,
                                             file_name,
                                             file_name_length,
                                             error_message);
}

DLLEXPORT int LossMetric_serialize_proxy(const int id,
                                         proxy_serialize* proxy,
                                         void* obj,
                                         std::string** error_message)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->serialize_proxy(proxy,
                                                   obj,
                                                   error_message);
}

DLLEXPORT int LossMetric_get_num_layers(const int id)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    return LossMetricRegistry[id]->get_num_layers();
}

DLLEXPORT int LossMetric_layer_details_set_num_filters(const int id, void* layer, long num)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    // LossMetricRegistry[id]->layer_details_set_num_filters(layer, num);
    return ERR_GENERAL_NOT_SUPPORT;
}

DLLEXPORT int LossMetric_subnet(const int id, void* obj, void** subnet)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->get_subnet(obj, subnet);
    return ERR_OK;
}

DLLEXPORT int LossMetric_subnet_get_layer_details(const int id, void* subnet, void** ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    *ret = LossMetricRegistry[id]->subnet_get_layer_details(subnet);
    return ERR_OK;
}

DLLEXPORT const dlib::tensor* LossMetric_subnet_get_output(const int id,
                                                           void* subnet,
                                                           int* ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
    {
        *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
        return nullptr;
    }

    return LossMetricRegistry[id]->subnet_get_output(subnet, ret);
}

DLLEXPORT int LossMetric_clean(const int id, void* obj)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->clean(obj);
    return ERR_OK;
}

// fc_ does not have input_tensor_to_output_tensor
DLLEXPORT int LossMetric_input_tensor_to_output_tensor(const int id, void* obj, dlib::dpoint* p, dlib::dpoint** ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    // LossMetricRegistry[id]->input_tensor_to_output_tensor(obj, p, ret);
    return ERR_GENERAL_NOT_SUPPORT;
}

DLLEXPORT int LossMetric_net_to_xml(const int id, void* obj, const char* filename, const int file_name_length)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->net_to_xml(obj, filename, file_name_length);
    return ERR_OK;
}

DLLEXPORT int LossMetric_operator_left_shift(const int id, void* trainer, std::ostringstream* stream)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->operator_left_shift(trainer, stream);
    return ERR_OK;
}

DLLEXPORT void* LossMetric_trainer_new(const int id, void* net)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return nullptr;

    return LossMetricRegistry[id]->trainer_new(net);
}

DLLEXPORT void* LossMetric_trainer_new2(const int id, void* net, const int32_t optimizer_id, void* optimizer)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return nullptr;

    return LossMetricRegistry[id]->trainer_new_optimizer(net, optimizer_id, optimizer);
}

DLLEXPORT void LossMetric_trainer_delete(const int id, void* trainer, const int32_t optimizer_id)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return;

    LossMetricRegistry[id]->trainer_delete(trainer, optimizer_id);
}

DLLEXPORT int LossMetric_trainer_set_learning_rate(const int id, void* trainer, const int32_t optimizer_id, const double lr)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_learning_rate(trainer, optimizer_id, lr);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_get_learning_rate(const int id, void* trainer, const int32_t optimizer_id, double* lr)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_get_learning_rate(trainer, optimizer_id, lr);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_get_average_loss(const int id, void* trainer, const int32_t optimizer_id, double* loss)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_get_average_loss(trainer, optimizer_id, loss);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_get_average_test_loss(const int id, void* trainer, const int32_t optimizer_id, double* loss)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_get_average_test_loss(trainer, optimizer_id, loss);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_set_min_learning_rate(const int id, void* trainer, const int32_t optimizer_id, const double lr)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_min_learning_rate(trainer, optimizer_id, lr);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_set_mini_batch_size(const int id, void* trainer, const int32_t optimizer_id, const unsigned long size)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_mini_batch_size(trainer, optimizer_id, size);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_be_verbose(const int id, void* trainer, const int32_t optimizer_id)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_be_verbose(trainer, optimizer_id);
    return ERR_OK;
}


DLLEXPORT int LossMetric_trainer_set_synchronization_file(const int id,
                                                          void* trainer,
                                                          const int32_t optimizer_id,
                                                          const char* filename,
                                                          const int filename_length,
                                                          const unsigned long second)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_synchronization_file(trainer, optimizer_id, filename, filename_length, second);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_set_iterations_without_progress_threshold(const int id,
                                                                           void* trainer,
                                                                           const int32_t optimizer_id,
                                                                           const unsigned long thresh)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_iterations_without_progress_threshold(trainer, optimizer_id, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_set_test_iterations_without_progress_threshold(const int id,
                                                                                void* trainer,
                                                                                const int32_t optimizer_id,
                                                                                const unsigned long thresh)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_set_test_iterations_without_progress_threshold(trainer, optimizer_id, thresh);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_get_net(const int id,
                                         void* trainer,
                                         const int32_t optimizer_id,
                                         void** ret)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    int error = ERR_OK;

    try
    {
        LossMetricRegistry[id]->trainer_get_net(trainer, optimizer_id, ret);
    }
    catch(std::exception)
    {
        error = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return error;
}

DLLEXPORT int LossMetric_trainer_operator_left_shift(const int id,
                                                     void* trainer,
                                                     const int32_t optimizer_id,
                                                     std::ostringstream* stream)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->trainer_operator_left_shift(trainer, optimizer_id, stream);
    return ERR_OK;
}

DLLEXPORT int LossMetric_set_all_bn_running_stats_window_sizes(const int id,
                                                               void* obj,
                                                               unsigned long new_window_size)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->set_all_bn_running_stats_window_sizes(obj, new_window_size);
    return ERR_OK;
}

DLLEXPORT int LossMetric_get_loss_details(const int id,
                                          void* obj,
                                          void** loss_details)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->get_loss_details(obj, loss_details);
    return ERR_OK;
}

DLLEXPORT int LossMetric_loss_details_get_distance_threshold(const int id,
                                                             void* obj,
                                                             float* distance_threshol)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->loss_details_get_distance_threshold(obj, distance_threshol);
    return ERR_OK;
}

DLLEXPORT int LossMetric_subnet_delete(const int id, void* subnet)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    LossMetricRegistry[id]->subnet_delete(subnet);
    return ERR_OK;
}

DLLEXPORT int LossMetric_trainer_test_one_step(const int id,
                                               void* trainer,
                                               const int32_t optimizer_id,
                                               matrix_element_type data_element_type,
                                               void* data,
                                               matrix_element_type label_element_type,
                                               void* labels)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMetricRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMetricRegistry[id]->trainer_test_one_step(trainer,
                                                      optimizer_id,
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

DLLEXPORT int LossMetric_trainer_train(const int id,
                                       void* trainer,
                                       const int32_t optimizer_id,
                                       matrix_element_type data_element_type,
                                       void* data,
                                       matrix_element_type label_element_type,
                                       void* labels)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMetricRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMetricRegistry[id]->trainer_train(trainer,
                                              optimizer_id,
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

DLLEXPORT int LossMetric_trainer_train_one_step(const int id,
                                                void* trainer,
                                                const int32_t optimizer_id,
                                                matrix_element_type data_element_type,
                                                void* data,
                                                matrix_element_type label_element_type,
                                                void* labels)
{
    auto iter = LossMetricRegistry.find(id);
    if (iter == end(LossMetricRegistry))
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    if (label_element_type != LossMetricRegistry[id]->get_label_type())
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    int error = ERR_OK;

    try
    {
        LossMetricRegistry[id]->trainer_train_one_step(trainer,
                                                       optimizer_id,
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