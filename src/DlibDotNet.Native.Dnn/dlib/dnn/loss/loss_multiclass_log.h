#ifndef _CPP_LOSS_MULTICLASS_LOG_H_
#define _CPP_LOSS_MULTICLASS_LOG_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../trainer.h"
#include "loss_multiclass_log_defines.h"
#include "../layers/layers.h"
#include "../../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define loss_multiclass_log_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(net_type, matrix_element_type::UInt8, uint8_t, error, __VA_ARGS__);\
        }\
        break;\
    case 1:\
        {\
            __FUNC__(net_1000_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 2:\
        {\
            __FUNC__(anet_1000_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
        break;\
}

#define train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, sub_template) \
do {\
    std::vector<matrix<__TYPE__>*>& tmp_data = *(static_cast<std::vector<matrix<__TYPE__>*>*>(data));\
    std::vector<matrix<__TYPE__>> in_tmp_data;\
    for (int i = 0; i< tmp_data.size(); i++)\
    {\
        matrix<__TYPE__>& mat = *tmp_data[i];\
        in_tmp_data.push_back(mat);\
    }\
\
    std::vector<train_label_type>& tmp_label = *(static_cast<std::vector<train_label_type>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
        in_tmp_label.push_back(tmp_label[i]);\
\
    dnn_trainer_train_one_step_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#define test_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_test_one_step_template);\

#define train_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_template);\

#define train_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_one_step_template);\

#define clone_template(__SRC_NET_TYPE__, dst_type, obj, new_net, error) \
do {\
    switch(dst_type)\
    {\
        case 0:\
            {\
                __SRC_NET_TYPE__& net = *static_cast<__SRC_NET_TYPE__*>(obj);\
                *new_net = new net_type(net);\
            }\
            break;\
        default:\
            error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
            break;\
    }\
} while (0)

#pragma region function template

#define loss_multiclass_log_new_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
*net = new __NET_TYPE__();

#define loss_multiclass_log_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
delete (__NET_TYPE__*)obj;

#define loss_multiclass_log_operator_matrixs_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        {\
            __NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
            operator_template(net, __ELEMENT_TYPE__, matrix_vector, batch_size, ret);\
        }\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define loss_multiclass_log_deserialize_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
__NET_TYPE__* net = new __NET_TYPE__();\
dlib::deserialize(file_name) >> (*net);\
*ret = net;

#define loss_multiclass_log_deserialize_proxy_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);\
__NET_TYPE__* net = new __NET_TYPE__();\
p >> (*net);\
*ret = net;

#define loss_multiclass_log_serialize_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
dlib::serialize(file_name) << (*net);

#define loss_multiclass_log_num_layers_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
return __NET_TYPE__::num_layers;

#define loss_multiclass_log_subnet_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
__NET_TYPE__::subnet_type& sn = net->subnet();\
*subnet = &sn;

#define loss_multiclass_log_clean_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
((__NET_TYPE__*)obj)->clean();

#define loss_multiclass_log_input_tensor_to_output_tensor_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
auto rp = dlib::input_tensor_to_output_tensor(net, *p);\
*ret = new dlib::dpoint(rp);

#define loss_multiclass_log_subnet_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto sb = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
delete sb;

#define loss_multiclass_log_subnet_get_output_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
const dlib::tensor& tensor = net->get_output();\
return &tensor;

#define loss_multiclass_log_subnet_get_layer_details_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
__NET_TYPE__::subnet_type::layer_details_type& layer_details = net->layer_details();\
return &layer_details;

#define loss_multiclass_log_operator_left_shift_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
__NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
*stream << net;

#define dnn_trainer_loss_multiclass_log_new_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_new_template(__NET_TYPE__, net);

#define dnn_trainer_loss_multiclass_log_new_sgd_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_new_template2(__NET_TYPE__, net, *sgd);

#define dnn_trainer_loss_multiclass_log_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_delete_template(__NET_TYPE__, trainer);

#define dnn_trainer_loss_multiclass_log_set_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_multiclass_log_get_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_get_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_multiclass_log_set_min_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_min_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_multiclass_log_set_mini_batch_size_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_mini_batch_size_template(__NET_TYPE__, trainer, size);

#define dnn_trainer_loss_multiclass_log_be_verbose_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_be_verbose_template(__NET_TYPE__, trainer);

#define dnn_trainer_loss_multiclass_log_set_synchronization_file_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_synchronization_file_template(__NET_TYPE__, trainer, filename, std::chrono::seconds(second));

#define dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_iterations_without_progress_threshold(__NET_TYPE__, trainer, thresh);

#define dnn_trainer_loss_multiclass_log_set_test_iterations_without_progress_threshold_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_test_iterations_without_progress_threshold(__NET_TYPE__, trainer, thresh);

#define dnn_trainer_loss_multiclass_log_test_one_step_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        test_one_step_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_multiclass_log_train_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        train_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_multiclass_log_train_one_step_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        train_one_step_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_multiclass_log_get_net_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_get_net_template(__NET_TYPE__, trainer, ret);

#define dnn_trainer_loss_multiclass_log_operator_left_shift_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_operator_left_shift_template(__NET_TYPE__, trainer, stream);

#define set_all_bn_running_stats_window_sizes_loss_multiclass_log_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
set_all_bn_running_stats_window_sizes_template(__NET_TYPE__, obj, new_window_size);

#pragma endregion function template

#pragma endregion template

DLLEXPORT int loss_multiclass_log_new(const int type, void** net)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_new_template,
                                 net);
    return error;
}

DLLEXPORT void loss_multiclass_log_delete(void* obj, const int type)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_delete_template,
                                 obj);
}

DLLEXPORT int loss_multiclass_log_clone(void* obj, const int src_type, const int dst_type, void** new_net)
{
    int error = ERR_OK;

    if (src_type != dst_type)
        return ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;

    // Check type argument and cast to the proper type
    switch(src_type)
    {
        case 0:
            clone_template(net_type, dst_type, obj, new_net, error);
            break;
        default:
            error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return error;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_multiclass_log_operator_matrixs(void* obj,
                                                   const int type,
                                                   matrix_element_type element_type,
                                                   void* matrix_vector,
                                                   int templateRows,
                                                   int templateColumns,
                                                   size_t batch_size,
                                                   std::vector<out_type>** ret)
{
    int error = ERR_OK;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     loss_multiclass_log_operator_matrixs_template,
                                     obj,
                                     element_type,
                                     matrix_vector,
                                     templateRows,
                                     templateColumns,
                                     batch_size,
                                     ret);
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int loss_multiclass_log_deserialize(const char* file_name,
                                              const int type,
                                              void** ret,
                                              std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     loss_multiclass_log_deserialize_template,
                                     file_name,
                                     ret);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int loss_multiclass_log_deserialize_proxy(proxy_deserialize* proxy,
                                                    const int type,
                                                    void** ret,
                                                    std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     loss_multiclass_log_deserialize_proxy_template,
                                     proxy,
                                     ret);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int loss_multiclass_log_serialize(void* obj,
                                            const int type,
                                            const char* file_name,
                                            std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     loss_multiclass_log_serialize_template,
                                     obj,
                                     file_name);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

DLLEXPORT int loss_multiclass_log_num_layers(const int type)
{    
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_num_layers_template);
    return 0;
}

DLLEXPORT int loss_multiclass_log_subnet(void* obj, const int type, void** subnet)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_subnet_template,
                                 obj,
                                 subnet);
    return 0;
}

DLLEXPORT void loss_multiclass_log_clean(void* obj, const int type)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_clean_template);
}

DLLEXPORT void loss_multiclass_log_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_input_tensor_to_output_tensor_template,
                                 obj,
                                 p,
                                 ret);
}

#pragma region subnet

DLLEXPORT void loss_multiclass_log_subnet_delete(const int type, void* subnet)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_subnet_delete_template,
                                 subnet);
}

DLLEXPORT const dlib::tensor* loss_multiclass_log_subnet_get_output(void* subnet, const int type, int* ret)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_subnet_get_output_template,
                                 subnet,
                                 ret);

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

DLLEXPORT void* loss_multiclass_log_subnet_get_layer_details(void* subnet, const int type, int* ret)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_subnet_get_layer_details_template,
                                 subnet,
                                 ret);

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

#pragma region layer_details

DLLEXPORT void loss_multiclass_log_layer_details_set_num_filters(void* layer, const int type, long num)
{
    // Check type argument and cast to the proper type
    // switch(type)
    // {
    //     case 0:
    //         {
    //             auto ld = static_cast<layer_details::layer_details_type*>(layer);
    //             ld->set_num_filters(num);
    //         }
    //         break;
    // }
}

#pragma endregion layer_details

#pragma region operator

DLLEXPORT int loss_multiclass_log_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 loss_multiclass_log_operator_left_shift_template,
                                 obj,
                                 stream);

    return error;
}

#pragma endregion operator

#pragma region dnn_trainer

DLLEXPORT void* dnn_trainer_loss_multiclass_log_new(void* net, const int type)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_new_template,
                                 net);

    return nullptr;
}

DLLEXPORT void* dnn_trainer_loss_multiclass_log_new_sgd(void* net, const int type, sgd* sgd)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_new_template,
                                 net,
                                 sgd);

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_delete(void* trainer, const int type)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_delete_template,
                                 trainer);
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_learning_rate(void* trainer, const int type, const double lr)
{
    int error = ERR_OK;
    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_learning_rate_template,
                                 trainer,
                                 lr);
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_get_learning_rate(void* trainer, const int type, double* lr)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_get_learning_rate_template,
                                 trainer,
                                 lr);

    return error;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_min_learning_rate_template,
                                 trainer,
                                 lr);
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_mini_batch_size_template,
                                 trainer,
                                 size);
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_be_verbose(void* trainer, const int type)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_be_verbose_template,
                                 trainer);
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_synchronization_file_template,
                                 trainer,
                                 filename,
                                 second);

    return error;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold_template,
                                 trainer,
                                 thresh);

    return error;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_set_test_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_set_test_iterations_without_progress_threshold_template,
                                 trainer,
                                 thresh);

    return error;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_test_one_step(void* trainer,
                                                            const int type,
                                                            matrix_element_type data_element_type,
                                                            void* data,
                                                            matrix_element_type label_element_type,
                                                            void* labels)
{
    // Check type argument and cast to the proper type
    int error = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     dnn_trainer_loss_multiclass_log_test_one_step_template,
                                     trainer,
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

DLLEXPORT int dnn_trainer_loss_multiclass_log_train(void* trainer,
                                                    const int type,
                                                    matrix_element_type data_element_type,
                                                    void* data,
                                                    matrix_element_type label_element_type,
                                                    void* labels)
{
    // Check type argument and cast to the proper type
    int error = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     dnn_trainer_loss_multiclass_log_train_template,
                                     trainer,
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

DLLEXPORT int dnn_trainer_loss_multiclass_log_train_one_step(void* trainer,
                                                             const int type,
                                                             matrix_element_type data_element_type,
                                                             void* data,
                                                             matrix_element_type label_element_type,
                                                             void* labels)
{
    // Check type argument and cast to the proper type
    int error = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     dnn_trainer_loss_multiclass_log_train_one_step_template,
                                     trainer,
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

DLLEXPORT int dnn_trainer_loss_multiclass_log_get_net(void* trainer,
                                                      const int type,
                                                      void** ret)
{
    int error = ERR_OK;

    try
    {
        loss_multiclass_log_template(type,
                                     error,
                                     dnn_trainer_loss_multiclass_log_get_net_template,
                                     trainer,
                                     ret);
    }
    catch(std::exception)
    {
        error = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return error;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_operator_left_shift(void* trainer, const int type, std::ostringstream* stream)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 dnn_trainer_loss_multiclass_log_operator_left_shift_template,
                                 trainer,
                                 stream);

    return error;
}

#pragma endregion dnn_trainer

#pragma region layers

DLLEXPORT int set_all_bn_running_stats_window_sizes_loss_multiclass_log(void* obj, const int type, unsigned long new_window_size)
{
    int error = ERR_OK;

    loss_multiclass_log_template(type,
                                 error,
                                 set_all_bn_running_stats_window_sizes_loss_multiclass_log_template,
                                 obj,
                                 new_window_size);

    return error;
}

#pragma endregion layers

#endif