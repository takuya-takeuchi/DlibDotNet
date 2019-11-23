#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_DEFINES_NET2_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_DEFINES_NET2_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../LossMulticlassLogPerPixel.h"
#include "../template.h"

using namespace dlib;
using namespace std;

extern std::map<int, LossMulticlassLogPerPixelBase*> LossMulticlassLogPerPixelRegistry;

MAKE_LOSSMULTICLASSLOGPERPIXEL_FUNC(LossMulticlassLogPerPixelUBNet, unet,                  ubnet_type,     matrix_element_type::RgbPixel, rgb_pixel, matrix_element_type::UInt16, loss_multiclass_log_per_pixel_train_label_type, 2)

#endif