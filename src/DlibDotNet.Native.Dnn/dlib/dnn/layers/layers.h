#ifndef _CPP_DNN_LAYERS_LAYERS_H_
#define _CPP_DNN_LAYERS_LAYERS_H_

#include <dlib/dnn.h>
#include "../../common.h"

using namespace dlib;
using namespace std;

// some functions of dli/dnn/layers.h are defines in dnn/loss directory

#define set_all_bn_running_stats_window_sizes_template(__NET_TYPE__, net, size) \
do {\
    __NET_TYPE__& n = *static_cast<__NET_TYPE__*>(net);\
    dlib::set_all_bn_running_stats_window_sizes(n, size);\
} while (0)

#endif