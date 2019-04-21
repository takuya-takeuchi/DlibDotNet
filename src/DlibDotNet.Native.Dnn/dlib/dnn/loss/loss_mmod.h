#ifndef _CPP_LOSS_MMOD_H_
#define _CPP_LOSS_MMOD_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "template.h"
#include "../trainer.h"
#include "loss_mmod_defines.h"

using namespace dlib;
using namespace std;

#pragma region template

#define loss_mmod_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(net_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 1:\
        {\
            __FUNC__(net_type_1, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 2:\
        {\
            __FUNC__(net_type_2, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 3:\
        {\
            __FUNC__(net_type_3, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
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
    std::vector<train_label_type_pointer*>& tmp_label = *(static_cast<std::vector<train_label_type_pointer*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type_pointer& v = *(tmp_label[i]);\
        train_label_type tmp_v;\
        for (int j = 0; j < v.size(); j++)\
        {\
            mmod_rect& r = *(v[j]);\
            tmp_v.push_back(r);\
        }\
        in_tmp_label.push_back(tmp_v);\
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

DLLEXPORT int loss_mmod_new2(const int type, mmod_options* option, void** net)
{
    int err = ERR_OK;

    mmod_options& o = *option;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net = new net_type(o);
            break;
        // case 1:
        //     *net = new net_type_1(o);
        //     break;
        // case 2:
        //     *net = new net_type_2(o);
        //     break;
        // case 3:
        //     *net = new net_type_3(o);
        //     break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int loss_mmod_clone(void* obj, const int src_type, const int dst_type, void** new_net)
{
    int err = ERR_OK;

    if (src_type != dst_type)
        return ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;

    // Check type argument and cast to the proper type
    switch(src_type)
    {
        case 0:
            // Only 0 to 0
            {                
                net_type& net = *static_cast<net_type*>(obj);\
                *new_net = new net_type(net);\
            }
            break;
        case 1:
            // Only 1 to 1
            {                
                net_type_1& net = *static_cast<net_type_1*>(obj);\
                *new_net = new net_type_1(net);\
            }
            break;
        case 2:
            // Only 2 to 2
            {                
                net_type_2& net = *static_cast<net_type_2*>(obj);\
                *new_net = new net_type_2(net);\
            }
            break;
        case 3:
            // Only 3 to 3
            {                
                net_type_3& net = *static_cast<net_type_3*>(obj);\
                *new_net = new net_type_3(net);\
            }
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT void loss_mmod_input_layer(void* obj, const int type, void** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                net_type& net = *static_cast<net_type*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 1:
            {
                net_type_1& net = *static_cast<net_type_1*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 2:
            {
                net_type_2& net = *static_cast<net_type_2*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 3:
            {
                net_type_3& net = *static_cast<net_type_3*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
    }
}

#pragma region layer_details

DLLEXPORT int loss_mmod_layer_details_set_num_filters(void* layer, const int type, long num)
{
    int error = ERR_OK;
    loss_mmod_template(type,
                       error,
                       loss_layer_details_set_num_filters,
                       subnet,
                       ret);
    return error;
}

#pragma endregion layer_details

// layers
MAKE_FUNC(loss_mmod, loss_mmod)

#endif