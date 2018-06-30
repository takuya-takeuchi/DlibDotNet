#ifndef _CPP_LOSS_METRIC_H_
#define _CPP_LOSS_METRIC_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../common.h"

using namespace dlib;
using namespace std;

// Output types
// typedef float output_label_type;
// typedef matrix<float,0,1> output_label_type;
// typedef matrix<float> output_label_type;
// typedef matrix<uint16_t> output_label_type;
// typedef std::vector<mmod_rect> output_label_type;
// typedef unsigned long output_label_type;

#pragma region matrix<float,0,1>

DLLEXPORT void dnn_output_stdvector_float_1_1_delete(std::vector<dlib::matrix<float, 0, 1>>* vector)
{
    delete vector;
}

DLLEXPORT dlib::matrix<float, 0, 1>* dnn_output_stdvector_float_1_1_getItem(std::vector<dlib::matrix<float, 0, 1>>* vector, const int index)
{
    return &(vector->at(index));
}

DLLEXPORT size_t dnn_output_stdvector_float_1_1_getSize(std::vector<dlib::matrix<float, 0, 1>>* vector)
{
    return vector->size();
}

#pragma endregion matrix<float,0,1>

#endif