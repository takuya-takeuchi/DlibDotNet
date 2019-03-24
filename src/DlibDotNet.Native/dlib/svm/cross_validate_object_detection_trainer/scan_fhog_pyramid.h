#ifndef _CPP_CROSS_VALIDATE_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_
#define _CPP_CROSS_VALIDATE_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include <dlib/svm/cross_validate_object_detection_trainer.h>
#include <dlib/svm/structural_object_detection_trainer.h>
#include <dlib/svm/svm.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_TYPE PYRAMID_TYPE
#define EXTRACTOR_TYPE EXTRACTOR_TYPE
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef EXTRACTOR_TYPE
#undef PYRAMID_TYPE

#define cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects) \
do { \
    std::vector<dlib::matrix<ELEMENT_IN>*>& in_images = *(static_cast<std::vector<dlib::matrix<ELEMENT_IN>*>*>(images));\
    std::vector<matrix<ELEMENT_IN>> tmp_images;\
    for (int i = 0; i < in_images.size(); i++)\
    {\
        matrix<ELEMENT_IN>& m = *in_images[i];\
        tmp_images.push_back(m);\
    }\
    std::vector<std::vector<ELEMENT_OUT*>*>& in_objects = *(static_cast<std::vector<std::vector<ELEMENT_OUT*>*>*>(objects));\
    std::vector<std::vector<ELEMENT_OUT>> tmp_objects;\
    for (int i = 0; i < in_images.size(); i++)\
    {\
        std::vector<ELEMENT_OUT*>& vec = *in_objects[i];\
        std::vector<ELEMENT_OUT> tmp_vec;\
        for (int j = 0; j < vec.size(); j++)\
        {\
            ELEMENT_OUT& m = *vec[j];\
            tmp_vec.push_back(m);\
        }\
        tmp_objects.push_back(tmp_vec);\
    }\
\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 2:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 3:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 4:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 6:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_rectangle(const pyramid_type pyramid_type,
                                                                                                                 const unsigned int pyramid_rate,
                                                                                                                 const fhog_feature_extractor_type extractor_type,
                                                                                                                 void* obj,
                                                                                                                 const matrix_element_type element_type,
                                                                                                                 void* images,
                                                                                                                 void* objects,
                                                                                                                 void** ret)
{
    int err = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        switch(element_type)
                        {
                            case matrix_element_type::UInt8:
                                #define ELEMENT_IN uint8_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt16:
                                #define ELEMENT_IN uint16_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt32:
                                #define ELEMENT_IN uint32_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int8:
                                #define ELEMENT_IN int8_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int16:
                                #define ELEMENT_IN int16_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int32:
                                #define ELEMENT_IN int32_t
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Float:
                                #define ELEMENT_IN float
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Double:
                                #define ELEMENT_IN double
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::RgbPixel:
                                #define ELEMENT_IN rgb_pixel
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::HsiPixel:
                                #define ELEMENT_IN hsi_pixel
                                cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::RgbAlphaPixel:
                            default:
                                err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                                break;
                        }
                        #undef EXTRACTOR_TYPE
                        break;
                    default:
                        err = ERR_FHOG_NOT_SUPPORT_EXTRACTOR;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    #undef ELEMENT_OUT

    return err;
}

#endif