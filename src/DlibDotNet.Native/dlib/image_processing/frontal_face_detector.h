#ifndef _CPP_FRONTAL_FACE_DETECTOR_H_
#define _CPP_FRONTAL_FACE_DETECTOR_H_

#include "../export.h"
#include <dlib/image_processing/frontal_face_detector.h>
#include <dlib/image_io.h>
#include <dlib/pixel.h>
#include <iostream>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define frontal_face_detector_operator_template(__TYPE__, error, type, ...) \
auto& d = *detector;\
std::vector<rectangle> result = d(*((array2d<__TYPE__>*)img), adjust_threshold);\
for(int index = 0; index < result.size(); index++)\
    dets->push_back(new rectangle(result[index]));\

#define frontal_face_detector_matrix_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& d = *detector;\
auto result = d(*((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)img), adjust_threshold);\
for(int index = 0; index < result.size(); index++)\
    dets->push_back(new rectangle(result[index]));\

#define frontal_face_detector_matrix_operator2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& d = *detector;\
std::vector<rect_detection> final_dets;\
d(*((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)img), final_dets, adjust_threshold);\
for(int index = 0; index < final_dets.size(); index++)\
    dets->push_back(new rect_detection(final_dets[index]));\

#pragma endregion template

DLLEXPORT frontal_face_detector* get_frontal_face_detector()
{
    frontal_face_detector ret = dlib::get_frontal_face_detector();
    return new dlib::frontal_face_detector(ret);
}

DLLEXPORT int frontal_face_detector_operator(frontal_face_detector* detector,
                                             array2d_type type,
                                             void* img,
                                             double adjust_threshold,
                                             std::vector<rectangle*> *dets)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              frontal_face_detector_operator_template,
                              detector,
                              img,
                              adjust_threshold,
                              dets);

    return error;
}

DLLEXPORT int frontal_face_detector_matrix_operator(frontal_face_detector* detector,
                                                    matrix_element_type type,
                                                    void* img,
                                                    double adjust_threshold,
                                                    std::vector<rectangle*> *dets)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             frontal_face_detector_matrix_operator_template,
                             0,
                             0,
                             detector,
                             img,
                             adjust_threshold,
                             dets);

    return error;
}

DLLEXPORT int frontal_face_detector_matrix_operator2(frontal_face_detector* detector,
                                                     matrix_element_type type,
                                                     void* img,
                                                     double adjust_threshold,
                                                     std::vector<rect_detection*> *dets)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             frontal_face_detector_matrix_operator2_template,
                             0,
                             0,
                             detector,
                             img,
                             adjust_threshold,
                             dets);

    return error;
}

DLLEXPORT void frontal_face_detector_delete(frontal_face_detector* obj)
{
	delete obj;
}

#endif