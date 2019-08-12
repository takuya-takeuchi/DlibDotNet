#ifndef _CPP_LOSS_MULTICLASS_LOG_TRAINER_H_
#define _CPP_LOSS_MULTICLASS_LOG_TRAINER_H_

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

// layers
MAKE_TRAINER_FUNC(loss_multiclass_log, loss_multiclass_log)

#endif