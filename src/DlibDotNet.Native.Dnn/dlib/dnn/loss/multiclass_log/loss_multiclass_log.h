#ifndef _CPP_LOSS_MULTICLASS_LOG_H_
#define _CPP_LOSS_MULTICLASS_LOG_H_

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

#endif