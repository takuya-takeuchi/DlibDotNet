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

#define ELEMENT element
#undef ELEMENT

#define frontal_face_detector_operator_template(img, adjust_threshold, dets) \
do {\
    std::vector<rectangle> result = ((*detector)(*((array2d<ELEMENT>*)img), adjust_threshold));\
    for(int index = 0; index < result.size(); index++)\
        dets->push_back(new rectangle(result[index]));\
} while (0)

#define frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets) \
do {\
    std::vector<rectangle> result = ((*detector)(*((matrix<ELEMENT>*)img), adjust_threshold));\
    for(int index = 0; index < result.size(); index++)\
        dets->push_back(new rectangle(result[index]));\
} while (0)

#define frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets) \
do {\
    std::vector<rect_detection> final_dets;\
    ((*detector)(*((matrix<ELEMENT>*)img), final_dets, adjust_threshold));\
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
            #define ELEMENT uint8_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::UInt16:
            #define ELEMENT uint16_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::UInt32:
            #define ELEMENT uint32_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::Int8:
            #define ELEMENT int8_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::Int16:
            #define ELEMENT int16_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::Int32:
            #define ELEMENT int32_t
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::Float:
            #define ELEMENT float
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::Double:
            #define ELEMENT double
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT rgb_pixel
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT hsi_pixel
            frontal_face_detector_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            frontal_face_detector_matrix_operator_template(img, adjust_threshold, dets);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            frontal_face_detector_matrix_operator2_template(img, adjust_threshold, dets);
            #undef ELEMENT
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