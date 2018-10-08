#ifndef _CPP_GEOMETRY_COMMON_H_
#define _CPP_GEOMETRY_COMMON_H_

#include "../shared.h"

#pragma region template

#define MAKE_LEFT_FUNC(__LEFT_TYPE__, __RIGHT_TYPE__, __OP__, __FUNC_PREFIX__, __OP_NAME__, __LEFT_NAME__, __RIGHT_NAME__)\
DLLEXPORT __LEFT_TYPE__* __FUNC_PREFIX__##_operator_##__OP_NAME__##_##__LEFT_NAME__##_##__RIGHT_NAME__(__LEFT_TYPE__* left, __RIGHT_TYPE__ right)\
{\
    const __LEFT_TYPE__ result = (*left) __OP__ (right);\
    return new __LEFT_TYPE__(result);\
}\

#define MAKE_RIGHT_FUNC(__LEFT_TYPE__, __RIGHT_TYPE__, __OP__, __FUNC_PREFIX__, __OP_NAME__, __LEFT_NAME__, __RIGHT_NAME__)\
DLLEXPORT __RIGHT_TYPE__* __FUNC_PREFIX__##_operator_##__OP_NAME__##_##__LEFT_NAME__##_##__RIGHT_NAME__(__LEFT_TYPE__ left, __RIGHT_TYPE__* right)\
{\
    const __RIGHT_TYPE__ result = (left) __OP__ (*right);\
    return new __RIGHT_TYPE__(result);\
}\

#pragma region template

#endif