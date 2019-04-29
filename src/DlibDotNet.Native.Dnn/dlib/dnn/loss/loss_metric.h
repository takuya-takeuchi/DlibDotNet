#ifndef _CPP_LOSS_METRIC_H_
#define _CPP_LOSS_METRIC_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "template.h"
#include "../trainer.h"
#include "loss_metric_defines.h"

using namespace dlib;
using namespace std;

#pragma region template

#define loss_metric_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(anet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
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

#define clone_template(__SRC_NET_TYPE__, dst_type, obj, new_net, err) \
do {\
    switch(dst_type)\
    {\
        case 0:\
            {\
                __SRC_NET_TYPE__& net = *static_cast<__SRC_NET_TYPE__*>(obj);\
                *new_net = new anet_type(net);\
            }\
            break;\
        default:\
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int loss_metric_clone(void* obj, const int src_type, const int dst_type, void** new_net)
{
    int err = ERR_OK;

    if (src_type != dst_type)
        return ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;

    // Check type argument and cast to the proper type
    switch(src_type)
    {
        case 0:
            clone_template(anet_type, dst_type, obj, new_net, err);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma region layer_details

DLLEXPORT int loss_metric_layer_details_set_num_filters(void* layer, const int type, long num)
{
    int error = ERR_OK;
    // loss_metric_template(type,
    //                      error,
    //                      loss_layer_details_set_num_filters,
    //                      subnet,
    //                      ret);
    return error;
}

#pragma endregion layer_details

// layers
MAKE_FUNC(loss_metric, loss_metric)

#endif