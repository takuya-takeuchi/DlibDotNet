#ifndef _CPP_CUDA_H_
#define _CPP_CUDA_H_

#include "../export.h"

#ifdef DLIB_USE_CUDA
#include "cuda_runtime_api.h"
#include "driver_types.h"
#endif

using namespace std;

DLLEXPORT bool cuda_cudaDriverGetVersion(int* version)
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

DLLEXPORT bool cuda_cudaRuntimeGetVersion(int* version)
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

#endif