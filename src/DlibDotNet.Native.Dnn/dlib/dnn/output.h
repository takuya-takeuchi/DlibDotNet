#ifndef _CPP_OUTPUT_H_
#define _CPP_OUTPUT_H_

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

// training_label_type
// typedef float training_label_type;
// typedef matrix<float,0,1> training_label_type;
// typedef matrix<float> training_label_type;
// typedef matrix<uint16_t> training_label_type;
// typedef matrix<weighted_label> training_label_type;
// typedef std::vector<mmod_rect> training_label_type;
// typedef unsigned long training_label_type;

// input_type
// typedef matrix<rgb_pixel> input_type;
// typedef matrix<T,NR,NC,MM,L> input_type;
// typedef std::array<matrix<T,NR,NC,MM,L>,K> input_type;
// typedef array2d<T,MM> input_type;
// typedef matrix<rgb_pixel> input_type;

// unsigned long is size 4 in windows and size 8 in linux
#pragma region std::vector<unsigned long>

DLLEXPORT void dnn_output_uint32_t_delete(std::vector<uint32_t>* vector)
{
    delete vector;
}

DLLEXPORT uint32_t dnn_output_uint32_t_getItem(std::vector<uint32_t>* vector, const int index)
{
    return vector->at(index);
}

DLLEXPORT size_t dnn_output_uint32_t_getSize(std::vector<uint32_t>* vector)
{
    return vector->size();
}

#pragma endregion std::vector<matrix<uint16_t>>

#pragma region std::vector<matrix<float,0,1>>

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

#pragma endregion std::vector<matrix<float,0,1>>

#pragma region std::vector<matrix<uint16_t>>

DLLEXPORT void dnn_output_stdvector_uint16_delete(std::vector<dlib::matrix<uint16_t>>* vector)
{
    delete vector;
}

DLLEXPORT dlib::matrix<uint16_t>* dnn_output_stdvector_uint16_getItem(std::vector<dlib::matrix<uint16_t>>* vector, const int index)
{
    return &(vector->at(index));
}

DLLEXPORT size_t dnn_output_stdvector_uint16_getSize(std::vector<dlib::matrix<uint16_t>>* vector)
{
    return vector->size();
}

#pragma endregion std::vector<matrix<uint16_t>>

#pragma region std::vector<std::vector<mmod_rect>>

DLLEXPORT void dnn_output_stdvector_stdvector_mmod_rect_delete(std::vector<std::vector<mmod_rect>>* vector)
{
    delete vector;
}

DLLEXPORT std::vector<mmod_rect>* dnn_output_stdvector_stdvector_mmod_rect_getItem(std::vector<std::vector<mmod_rect>>* vector, const int index)
{
    return &(vector->at(index));
}

DLLEXPORT size_t dnn_output_stdvector_stdvector_mmod_rect_getSize(std::vector<std::vector<mmod_rect>>* vector)
{
    return vector->size();
}

DLLEXPORT void dnn_output_stdvector_mmod_rect_delete(std::vector<mmod_rect>* vector)
{
    delete vector;
}

DLLEXPORT mmod_rect* dnn_output_stdvector_mmod_rect_getItem(std::vector<mmod_rect>* vector, const int index)
{
    return &(vector->at(index));
}

DLLEXPORT size_t dnn_output_stdvector_mmod_rect_getSize(std::vector<mmod_rect>* vector)
{
    return vector->size();
}

#pragma endregion std::vector<std::vector<mmod_rect>>

#endif