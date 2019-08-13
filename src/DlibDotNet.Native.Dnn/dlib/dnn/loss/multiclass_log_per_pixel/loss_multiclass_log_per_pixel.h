#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_

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
MAKE_FUNC2(loss_multiclass_log_per_pixel, loss_multiclass_log_per_pixel)

#endif