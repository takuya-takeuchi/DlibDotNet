#ifndef _CPP_DNN_SOLVERS_SGD_H_
#define _CPP_DNN_SOLVERS_SGD_H_

#include <dlib/dnn.h>
#include "../../common.h"

using namespace dlib;
using namespace std;

DLLEXPORT sgd* sgd_new(float weight_decay,
                       float momentum)
{
    return new sgd(weight_decay, momentum);
}

DLLEXPORT void sgd_delete(sgd* sgd)
{
    delete sgd;
}

#endif