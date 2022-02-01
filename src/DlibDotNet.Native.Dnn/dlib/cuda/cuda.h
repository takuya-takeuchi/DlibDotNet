#ifndef _CPP_CUDA_H_
#define _CPP_CUDA_H_

#include "../common.h"

#ifdef DLIB_USE_CUDA
#include "cuda_runtime_api.h"
#include "driver_types.h"
#endif

DLLEXPORT bool cuda_dnn_cudaDriverGetVersion(int* version)
{
    bool b = false;
    *version = 0;

#ifdef DLIB_USE_CUDA
    int ret = 0;
    auto error = ::cudaDriverGetVersion(&ret);
    if (error == cudaSuccess)
    {
        *version = ret;
        b = true;
    }
#else
    *version = -1;
#endif

    return b;
}

DLLEXPORT bool cuda_dnn_cudaRuntimeGetVersion(int* version)
{
    bool b = false;
    *version = 0;

#ifdef DLIB_USE_CUDA
    int ret = 0;
    auto error = ::cudaRuntimeGetVersion(&ret);
    if (error == cudaSuccess)
    {
        *version = ret;
        b = true;
    }
#else
    *version = -1;
#endif

    return b;
}

DLLEXPORT const char* cuda_dnn_cudaGetErrorName(int code)
{
#ifdef DLIB_USE_CUDA
    return ::cudaGetErrorName((::cudaError_t)code);
#endif

    return nullptr;
}

DLLEXPORT const char* cuda_dnn_cudaGetErrorString(int code)
{
#ifdef DLIB_USE_CUDA
    return ::cudaGetErrorString((::cudaError_t)code);
#endif

    return nullptr;
}

#endif