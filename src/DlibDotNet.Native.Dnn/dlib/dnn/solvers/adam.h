#ifndef _CPP_DNN_SOLVERS_ADAM_H_
#define _CPP_DNN_SOLVERS_ADAM_H_

#include <dlib/dnn.h>
#include "../../common.h"

using namespace dlib;
using namespace std;

DLLEXPORT adam* adam_new(float weight_decay,
                         float momentum1, 
                         float momentum2)
{                       
    return new adam(weight_decay, momentum1, momentum2);
}

DLLEXPORT void adam_delete(adam* adam)
{
    delete adam;
}

#endif