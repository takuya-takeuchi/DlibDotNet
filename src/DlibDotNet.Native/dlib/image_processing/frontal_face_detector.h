#ifndef _CPP_FRONTAL_FACE_DETECTOR_H_
#define _CPP_FRONTAL_FACE_DETECTOR_H_

#include "../export.h"
#include <dlib/image_processing/frontal_face_detector.h>
#include <dlib/image_io.h>
#include <dlib/pixel.h>
#include <iostream>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

#pragma region template

#define frontal_face_detector_operator_template(__TYPE__, img, adjust_threshold, dets) \
do {\
    std::vector<rectangle> result = ((*detector)(*((array2d<__TYPE__>*)img), adjust_threshold));\
    for(int index = 0; index < result.size(); index++)\
        dets->push_back(new rectangle(result[index]));\
} while (0)

#define frontal_face_detector_matrix_operator_template(__TYPE__, img, adjust_threshold, dets) \
do {\
    std::vector<rectangle> result = ((*detector)(*((matrix<__TYPE__>*)img), adjust_threshold));\
    for(int index = 0; index < result.size(); index++)\
        dets->push_back(new rectangle(result[index]));\
} while (0)

#define frontal_face_detector_matrix_operator2_template(__TYPE__, img, adjust_threshold, dets) \
do {\
    std::vector<rect_detection> final_dets;\
    ((*detector)(*((matrix<__TYPE__>*)img), final_dets, adjust_threshold));\
    for(int index = 0; index < final_dets.size(); index++)\
        dets->push_back(new rect_detection(final_dets[index]));\
} while (0)

#pragma endregion template

DLLEXPORT frontal_face_detector* get_frontal_face_detector()
{
    frontal_face_detector ret = dlib::get_frontal_face_detector();
    return new dlib::frontal_face_detector(ret);
}

DLLEXPORT int frontal_face_detector_operator(
    frontal_face_detector* detector,
    array2d_type img_type,
    void* img,
    double adjust_threshold,
    std::vector<rectangle*> *dets)
{
    int err = ERR_OK;

    std::vector<rectangle>* ret = nullptr;
    switch(img_type)
    {
        case array2d_type::UInt8:
            frontal_face_detector_operator_template(uint8_t, img, adjust_threshold, dets);
            break;
        case array2d_type::UInt16:
            frontal_face_detector_operator_template(uint16_t, img, adjust_threshold, dets);
            break;
        case array2d_type::UInt32:
            frontal_face_detector_operator_template(uint32_t, img, adjust_threshold, dets);
            break;
        case array2d_type::Int8:
            frontal_face_detector_operator_template(int8_t, img, adjust_threshold, dets);
            break;
        case array2d_type::Int16:
            frontal_face_detector_operator_template(int16_t, img, adjust_threshold, dets);
            break;
        case array2d_type::Int32:
            frontal_face_detector_operator_template(int32_t, img, adjust_threshold, dets);
            break;
        case array2d_type::Float:
            frontal_face_detector_operator_template(float, img, adjust_threshold, dets);
            break;
        case array2d_type::Double:
            frontal_face_detector_operator_template(double, img, adjust_threshold, dets);
            break;
        case array2d_type::RgbPixel:
            frontal_face_detector_operator_template(rgb_pixel, img, adjust_threshold, dets);
            break;
        case array2d_type::HsiPixel:
            frontal_face_detector_operator_template(hsi_pixel, img, adjust_threshold, dets);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int frontal_face_detector_matrix_operator(frontal_face_detector* detector,
                                                    matrix_element_type img_type,
                                                    void* img,
                                                    double adjust_threshold,
                                                    std::vector<rectangle*> *dets)
{
    int err = ERR_OK;

    std::vector<rectangle>* ret = nullptr;
    switch(img_type)
    {
        case matrix_element_type::UInt8:
            frontal_face_detector_matrix_operator_template(uint8_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::UInt16:
            frontal_face_detector_matrix_operator_template(uint16_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::UInt32:
            frontal_face_detector_matrix_operator_template(uint32_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int8:
            frontal_face_detector_matrix_operator_template(int8_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int16:
            frontal_face_detector_matrix_operator_template(int16_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int32:
            frontal_face_detector_matrix_operator_template(int32_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Float:
            frontal_face_detector_matrix_operator_template(float, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Double:
            frontal_face_detector_matrix_operator_template(double, img, adjust_threshold, dets);
            break;
        case matrix_element_type::RgbPixel:
            frontal_face_detector_matrix_operator_template(rgb_pixel, img, adjust_threshold, dets);
            break;
        case matrix_element_type::HsiPixel:
            frontal_face_detector_matrix_operator_template(hsi_pixel, img, adjust_threshold, dets);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int frontal_face_detector_matrix_operator2(frontal_face_detector* detector,
                                                     matrix_element_type img_type,
                                                     void* img,
                                                     double adjust_threshold,
                                                     std::vector<rect_detection*> *dets)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case matrix_element_type::UInt8:
            frontal_face_detector_matrix_operator2_template(uint8_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::UInt16:
            frontal_face_detector_matrix_operator2_template(uint16_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::UInt32:
            frontal_face_detector_matrix_operator2_template(uint32_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int8:
            frontal_face_detector_matrix_operator2_template(int8_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int16:
            frontal_face_detector_matrix_operator2_template(int16_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Int32:
            frontal_face_detector_matrix_operator2_template(int32_t, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Float:
            frontal_face_detector_matrix_operator2_template(float, img, adjust_threshold, dets);
            break;
        case matrix_element_type::Double:
            frontal_face_detector_matrix_operator2_template(double, img, adjust_threshold, dets);
            break;
        case matrix_element_type::RgbPixel:
            frontal_face_detector_matrix_operator2_template(rgb_pixel, img, adjust_threshold, dets);
            break;
        case matrix_element_type::HsiPixel:
            frontal_face_detector_matrix_operator2_template(hsi_pixel, img, adjust_threshold, dets);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void frontal_face_detector_delete(frontal_face_detector* obj)
{
	delete obj;
}

#endif