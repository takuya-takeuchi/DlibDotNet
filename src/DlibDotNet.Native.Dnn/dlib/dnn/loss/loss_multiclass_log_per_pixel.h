#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "template.h"
#include "../trainer.h"
#include "loss_multiclass_log_per_pixel_defines.h"

using namespace dlib;
using namespace std;

#pragma region template

#define loss_multiclass_log_per_pixel_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(anet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 1:\
        {\
            __FUNC__(net_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 2:\
        {\
            __FUNC__(ubnet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 3:\
        {\
            __FUNC__(uanet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
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

#define operator_matrixs_template(element_type, __NET_TYPE__, obj, matrix_vector, batch_size, ret) \
do {\
    __NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
    switch(element_type)\
    {\
        case matrix_element_type::UInt8:\
            operator_template(net, uint8_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::UInt16:\
            operator_template(net, uint16_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::UInt32:\
            operator_template(net, uint32_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int8:\
            operator_template(net, int8_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int16:\
            operator_template(net, int16_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int32:\
            operator_template(net, int32_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Float:\
            operator_template(net, float, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Double:\
            operator_template(net, double, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::RgbPixel:\
            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::HsiPixel:\
            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int loss_multiclass_log_per_pixel_clone(void* obj, const int src_type, const int dst_type, void** new_net)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(src_type)
    {
        case 0:
            {
                anet_type& net = *static_cast<anet_type*>(obj);
                switch(dst_type)
                {
                    case 0:
                        *new_net = new anet_type(net);
                        break;
                    default:
                        err = ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;
                        break;
                }
                break;
            }
            break;
        case 1:
            {
                net_type& net = *static_cast<net_type*>(obj);
                switch(dst_type)
                {
                    case 0:
                        *new_net = new anet_type(net);
                        break;
                    case 1:
                        *new_net = new net_type(net);
                        break;
                    default:
                        err = ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;
                        break;
                }
                break;
            }
            break;
        case 2:
            {
                ubnet_type& ubnet = *static_cast<ubnet_type*>(obj);
                switch(dst_type)
                {
                    case 2:
                        *new_net = new ubnet_type(ubnet);
                        break;
                    case 3:
                        *new_net = new uanet_type(ubnet);
                        break;
                    default:
                        err = ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;
                        break;
                }
                break;
            }
        case 3:
            {
                uanet_type& net = *static_cast<uanet_type*>(obj);
                switch(dst_type)
                {
                    case 0:
                        *new_net = new uanet_type(net);
                        break;
                    default:
                        err = ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;
                        break;
                }
                break;
            }
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT uint16_t loss_multiclass_log_per_pixel_get_label_to_ignore()
{
    return loss_multiclass_log_per_pixel_::label_to_ignore;
}

#pragma region layer_details

DLLEXPORT int loss_multiclass_log_per_pixel_layer_details_set_num_filters(void* layer, const int type, long num)
{
    int error = ERR_OK;
    loss_multiclass_log_per_pixel_template(type,
                                           error,
                                           loss_layer_details_set_num_filters,
                                           subnet,
                                           ret);
    return error;
}

#pragma endregion layer_details

// layers
MAKE_FUNC(loss_multiclass_log_per_pixel, loss_multiclass_log_per_pixel)

#endif