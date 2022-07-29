#ifndef _CPP_LOSS_REGISTRY_H_
#define _CPP_LOSS_REGISTRY_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "metric/LossMetricBase.h"
#include "mmod/LossMmodBase.h"
#include "multiclass_log/LossMulticlassLogBase.h"
#include "multiclass_log_per_pixel/LossMulticlassLogPerPixelBase.h"

using namespace dlib;
using namespace std;

#pragma region template

#define MAKE_REGISTRY_FUNC(__CLASS__)\
std::map<int, __CLASS__##Base*> __CLASS__##Registry;\
\
DLLEXPORT bool __CLASS__##Registry_add(__CLASS__##Base* base)\
{\
    auto iter = __CLASS__##Registry.find(base->get_id());\
    if (iter != end(__CLASS__##Registry))\
        return false;\
\
    __CLASS__##Registry.insert(std::make_pair(base->get_id(), base));\
    return true;\
}\
\
DLLEXPORT void __CLASS__##Registry_remove(__CLASS__##Base* base)\
{\
    __CLASS__##Registry.erase(base->get_id());\
}\
\
DLLEXPORT bool __CLASS__##Registry_contains(const int id)\
{\
    auto iter = __CLASS__##Registry.find(id);\
    if (iter == end(__CLASS__##Registry))\
        return false;\
    return true;\
}\

#pragma endregion template

#ifndef DLIB_NO_LOSSMETRIC_SUPPORT
MAKE_REGISTRY_FUNC(LossMetric)
#endif
#ifndef DLIB_NO_LOSSMMODSUPPORT
MAKE_REGISTRY_FUNC(LossMmod)
#endif
#ifndef DLIB_NO_LOSSMULTICLASSLOG_SUPPORT
MAKE_REGISTRY_FUNC(LossMulticlassLog)
#endif
#ifndef DLIB_NO_LOSSMULTICLASSLOGPERPIXEL_SUPPORT
MAKE_REGISTRY_FUNC(LossMulticlassLogPerPixel)
#endif

#endif