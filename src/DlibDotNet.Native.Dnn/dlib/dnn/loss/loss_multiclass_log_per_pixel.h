#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../trainer.h"
#include "loss_multiclass_log_per_pixel_defines.h"
#include "../../common.h"

using namespace dlib;
using namespace std;

#pragma region template

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
    std::vector<train_label_type*>& tmp_label = *(static_cast<std::vector<train_label_type*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type& mat = *static_cast<train_label_type*>(tmp_label[i]);\
        in_tmp_label.push_back(mat);\
    }\
\
    sub_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#define test_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_test_one_step_template);\

#define train_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_template);\

#define train_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_one_step_template);\

#pragma endregion template

DLLEXPORT int loss_multiclass_log_per_pixel_new(const int type, void** net)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net =  new anet_type();
            break;
        case 1:
            *net =  new net_type();
            break;
    }

    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_multiclass_log_per_pixel_operator_matrixs(void* obj,
                                                             const int type,
                                                             matrix_element_type element_type,
                                                             void* matrix_vector,
                                                             int templateRows,
                                                             int templateColumns,
                                                             size_t batch_size,
                                                             std::vector<out_type>** ret)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    anet_type& net = *(static_cast<anet_type*>(obj));
                    switch(element_type)
                    {
                        case matrix_element_type::UInt8:
                            operator_template(net, uint8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt16:
                            operator_template(net, uint16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt32:
                            operator_template(net, uint32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int8:
                            operator_template(net, int8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int16:
                            operator_template(net, int16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int32:
                            operator_template(net, int32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Float:
                            operator_template(net, float, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Double:
                            operator_template(net, double, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::HsiPixel:
                            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbAlphaPixel:
                            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);
                            break;
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
            case 1:
                {
                    net_type& net = *(static_cast<net_type*>(obj));
                    switch(element_type)
                    {
                        case matrix_element_type::UInt8:
                            operator_template(net, uint8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt16:
                            operator_template(net, uint16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt32:
                            operator_template(net, uint32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int8:
                            operator_template(net, int8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int16:
                            operator_template(net, int16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int32:
                            operator_template(net, int32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Float:
                            operator_template(net, float, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Double:
                            operator_template(net, double, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::HsiPixel:
                            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbAlphaPixel:
                            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);
                            break;
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT void loss_multiclass_log_per_pixel_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            delete (anet_type*)obj;
            break;
        case 1:
            delete (net_type*)obj;
            break;
    }
}

DLLEXPORT uint16_t loss_multiclass_log_per_pixel_get_label_to_ignore()
{
    return loss_multiclass_log_per_pixel_::label_to_ignore;
}

DLLEXPORT int loss_multiclass_log_per_pixel_deserialize(const char* file_name, const int type, void** ret)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    anet_type* net = new anet_type();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    net_type* net = new net_type();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            default:
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int loss_multiclass_log_per_pixel_deserialize_proxy(proxy_deserialize* proxy, const int type, void** ret)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    anet_type* net = new anet_type();
                    p >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type* net = new net_type();
                    p >> (*net);
                    *ret = net;
                }
                break;
            default:
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT void loss_multiclass_log_per_pixel_serialize(void* obj, const int type, const char* file_name)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        default:
            break;
    }
}

DLLEXPORT int loss_multiclass_log_per_pixel_num_layers(const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            return anet_type::num_layers;
        case 1:
            return net_type::num_layers;
    }

    return 0;
}

DLLEXPORT int loss_multiclass_log_per_pixel_subnet(void* obj, const int type, void** subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                anet_type::subnet_type& sn = net->subnet();
                *subnet = &sn;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                net_type::subnet_type& sn = net->subnet();
                *subnet = &sn;
            }
            break;
    }

    return 0;
}

DLLEXPORT void loss_multiclass_log_per_pixel_clean(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            ((anet_type*)obj)->clean();
            break;
        case 1:
            ((net_type*)obj)->clean();
            break;
    }
}

DLLEXPORT void loss_multiclass_log_per_pixel_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
    }
}

#pragma region subnet

DLLEXPORT void loss_multiclass_log_per_pixel_subnet_delete(const int type, void* subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto sb = static_cast<anet_type::subnet_type*>(subnet);
                delete sb;
            }
            break;
        case 1:
            {
                auto sb = static_cast<net_type::subnet_type*>(subnet);
                delete sb;
            }
            break;
    }
}

DLLEXPORT const dlib::tensor* loss_multiclass_log_per_pixel_subnet_get_output(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

DLLEXPORT void* loss_multiclass_log_per_pixel_subnet_get_layer_details(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type::subnet_type*>(subnet);
                anet_type::subnet_type::layer_details_type& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
                net_type::subnet_type::layer_details_type& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

#pragma region layer_details

DLLEXPORT void loss_multiclass_log_per_pixel_layer_details_set_num_filters(void* layer, const int type, long num)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto ld = static_cast<anet_type::subnet_type::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 1:
            {
                auto ld = static_cast<net_type::subnet_type::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
    }
}

#pragma endregion layer_details

#pragma region operator

DLLEXPORT int loss_multiclass_log_per_pixel_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                anet_type& anet = *(static_cast<anet_type*>(obj));
                *stream << anet;
            }
            break;
        case 1:
            {
                net_type& anet = *(static_cast<net_type*>(obj));
                *stream << anet;
            }
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma region dnn_trainer

DLLEXPORT void* dnn_trainer_loss_multiclass_log_per_pixel_new(void* net, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template(anet_type, net);
            break;
        case 1:
            dnn_trainer_new_template(net_type, net);
            break;
    }

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_delete(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_delete_template(anet_type, trainer);
            break;
        case 1:
            dnn_trainer_delete_template(net_type, trainer);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_learning_rate_template(anet_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_get_learning_rate(void* trainer, const int type, double* lr)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_get_learning_rate_template(anet_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_get_learning_rate_template(net_type, trainer, lr);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_min_learning_rate_template(anet_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_min_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_mini_batch_size_template(anet_type, trainer, size);
            break;
        case 1:
            dnn_trainer_set_mini_batch_size_template(net_type, trainer, size);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_be_verbose_template(anet_type, trainer);
            break;
        case 1:
            dnn_trainer_be_verbose_template(net_type, trainer);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_synchronization_file_template(anet_type, trainer, filename, std::chrono::seconds(second));
            break;
        case 1:
            dnn_trainer_set_synchronization_file_template(net_type, trainer, filename, std::chrono::seconds(second));
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_iterations_without_progress_threshold(anet_type, trainer, thresh);
            break;
        case 1:
            dnn_trainer_set_iterations_without_progress_threshold(net_type, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_set_test_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_test_iterations_without_progress_threshold(anet_type, trainer, thresh);
            break;
        case 1:
            dnn_trainer_set_test_iterations_without_progress_threshold(net_type, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_test_one_step(void* trainer,
                                                                      const int type,
                                                                      matrix_element_type data_element_type,
                                                                      void* data,
                                                                      matrix_element_type label_element_type,
                                                                      void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        switch(data_element_type)
        {
            case matrix_element_type::RgbPixel:
                switch(type)
                {
                    case 0:
                        test_one_step_template(anet_type, trainer, rgb_pixel, data, labels);
                        break;
                    case 1:
                        test_one_step_template(net_type, trainer, rgb_pixel, data, labels);
                        break;
                }
                break;
            case matrix_element_type::UInt8:
            case matrix_element_type::UInt16:
            case matrix_element_type::UInt32:
            case matrix_element_type::Int8:
            case matrix_element_type::Int16:
            case matrix_element_type::Int32:
            case matrix_element_type::Float:
            case matrix_element_type::Double:
            case matrix_element_type::HsiPixel:
            case matrix_element_type::RgbAlphaPixel:
            default:
                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_train(void* trainer,
                                                              const int type,
                                                              matrix_element_type data_element_type,
                                                              void* data,
                                                              matrix_element_type label_element_type,
                                                              void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        switch(data_element_type)
        {
            case matrix_element_type::RgbPixel:
                switch(type)
                {
                    case 0:
                        train_template(anet_type, trainer, rgb_pixel, data, labels);
                        break;
                    case 1:
                        train_template(net_type, trainer, rgb_pixel, data, labels);
                        break;
                }
                break;
            case matrix_element_type::UInt8:
            case matrix_element_type::UInt16:
            case matrix_element_type::UInt32:
            case matrix_element_type::Int8:
            case matrix_element_type::Int16:
            case matrix_element_type::Int32:
            case matrix_element_type::Float:
            case matrix_element_type::Double:
            case matrix_element_type::HsiPixel:
            case matrix_element_type::RgbAlphaPixel:
            default:
                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_train_one_step(void* trainer,
                                                                       const int type,
                                                                       matrix_element_type data_element_type,
                                                                       void* data,
                                                                       matrix_element_type label_element_type,
                                                                       void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;

    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
        switch(data_element_type)
        {
            case matrix_element_type::RgbPixel:
                switch(type)
                {
                    case 0:
                        train_one_step_template(anet_type, trainer, rgb_pixel, data, labels);
                        break;
                    case 1:
                        train_one_step_template(net_type, trainer, rgb_pixel, data, labels);
                        break;
                }
                break;
            case matrix_element_type::UInt8:
            case matrix_element_type::UInt16:
            case matrix_element_type::UInt32:
            case matrix_element_type::Int8:
            case matrix_element_type::Int16:
            case matrix_element_type::Int32:
            case matrix_element_type::Float:
            case matrix_element_type::Double:
            case matrix_element_type::HsiPixel:
            case matrix_element_type::RgbAlphaPixel:
            default:
                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_get_net(void* trainer,
                                                                const int type,
                                                                void** ret)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;

    try
    {
        switch(type)
        {
            case 0:
                dnn_trainer_get_net_template(anet_type, trainer, ret);
                break;
            case 1:
                dnn_trainer_get_net_template(net_type, trainer, ret);
                break;
            default:
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(std::exception)
    {
        err = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_operator_left_shift(void* trainer, const int type, std::ostringstream* stream)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_operator_left_shift_template(anet_type, trainer, stream);
            break;
        case 1:
            dnn_trainer_operator_left_shift_template(net_type, trainer, stream);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion dnn_trainer

#endif