#ifndef _CPP_LOSS_MMOD_H_
#define _CPP_LOSS_MMOD_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "common.h"
#include "../template.h"
#include "../../trainer.h"
#include "defines.h"

using namespace dlib;
using namespace std;

DLLEXPORT int loss_mmod_new2(const int type, mmod_options* option, void** net)
{
    int error = ERR_OK;
    loss_mmod_template(type,
                       error,
                       loss_mmod_new2_template,
                       option,
                       net);
    return error;
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
MAKE_FUNC2(loss_mmod, loss_mmod)

#endif