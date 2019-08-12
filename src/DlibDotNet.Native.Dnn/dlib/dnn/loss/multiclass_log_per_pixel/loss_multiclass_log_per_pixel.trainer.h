#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_TRAINER_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_TRAINER_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "loss_multiclass_log_per_pixel.common.h"
#include "../template.h"
#include "../../trainer.h"
#include "loss_multiclass_log_per_pixel_defines.h"

using namespace dlib;
using namespace std;

// layers
MAKE_TRAINER_FUNC(loss_multiclass_log_per_pixel, loss_multiclass_log_per_pixel)

#endif