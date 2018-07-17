#ifndef _CPP_TENSOR_H_
#define _CPP_TENSOR_H_

#include <dlib/dnn.h>
#include <vector>

#include "../common.h"

using namespace dlib;
using namespace std;

DLLEXPORT int tensor_k(void* tensor)
{
    const dlib::tensor& t = *static_cast<dlib::tensor*>(tensor);
    return t.k();
}

DLLEXPORT dlib::matrix<float>* image_plane(void* tensor, int sample, int k)
{
    const dlib::tensor& t = *static_cast<dlib::tensor*>(tensor);
    auto ret = image_plane(t, sample, k);
    return new dlib::matrix<float>(ret);
}

#pragma region resizable_tensor

DLLEXPORT resizable_tensor* resizable_tensor_new()
{
    return new resizable_tensor();
}

DLLEXPORT void resizable_tensor_delete(resizable_tensor* tensor)
{
    delete tensor;
}

#pragma endregion resizable_tensor

#endif