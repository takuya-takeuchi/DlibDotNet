#ifndef _CPP_LOSS_MULTICLASS_LOG_H_
#define _CPP_LOSS_MULTICLASS_LOG_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "template.h"
#include "../trainer.h"
#include "loss_multiclass_log_defines.h"

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
    case 3:\
        {\
            __FUNC__(net_type2, matrix_element_type::UInt8, uint8_t, error, __VA_ARGS__);\
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
    sub_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
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

#pragma endregion template

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

#pragma region layer_details

DLLEXPORT int loss_multiclass_log_layer_details_set_num_filters(void* layer, const int type, long num)
{
    int error = ERR_OK;
    // loss_multiclass_log_template(type,
    //                              error,
    //                              loss_layer_details_set_num_filters,
    //                              subnet,
    //                              ret);
    return error;
}

#pragma endregion layer_details

// layers
MAKE_FUNC(loss_multiclass_log, loss_multiclass_log)
MAKE_TRAINER_FUNC(loss_multiclass_log, loss_multiclass_log)

#endif