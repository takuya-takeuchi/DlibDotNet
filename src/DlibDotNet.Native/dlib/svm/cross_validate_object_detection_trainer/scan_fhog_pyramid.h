#ifndef _CPP_CROSS_VALIDATE_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_
#define _CPP_CROSS_VALIDATE_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include <dlib/svm/cross_validate_object_detection_trainer.h>
#include <dlib/svm/structural_object_detection_trainer.h>
#include <dlib/svm/svm.h>
#include "../../template.h"
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

#define cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
do { \
    auto& in_images = *(static_cast<std::vector<dlib::matrix<__TYPE__>*>*>(images));\
    std::vector<matrix<__TYPE__>> tmp_images;\
    for (int i = 0; i < in_images.size(); i++)\
    {\
        matrix<__TYPE__>& m = *in_images[i];\
        tmp_images.push_back(m);\
    }\
    auto& in_objects = *(static_cast<std::vector<std::vector<ELEMENT_OUT*>*>*>(objects));\
    std::vector<std::vector<ELEMENT_OUT>> tmp_objects;\
    for (int i = 0; i < in_images.size(); i++)\
    {\
        auto& vec = *in_objects[i];\
        std::vector<ELEMENT_OUT> tmp_vec;\
        for (int j = 0; j < vec.size(); j++)\
        {\
            auto& m = *vec[j];\
            tmp_vec.push_back(m);\
        }\
        tmp_objects.push_back(tmp_vec);\
    }\
\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 2:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 3:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 4:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        case 6:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                auto mat = test_object_detection_function(detector, tmp_images, tmp_objects);\
                *ret = new matrix<double, 1, 3>(mat);\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_rectangle(const pyramid_type pyramid_type,
                                                                                                                 const unsigned int pyramid_rate,
                                                                                                                 const fhog_feature_extractor_type extractor_type,
                                                                                                                 void* obj,
                                                                                                                 const matrix_element_type type,
                                                                                                                 void* images,
                                                                                                                 void* objects,
                                                                                                                 void** ret)
{
    int error = ERR_OK;

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
                        {
                            matrix_nonalpha_template(type,
                                                     error,
                                                     matrix_template_size_template,
                                                     cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_template,
                                                     0,
                                                     0,
                                                     pyramid_rate,
                                                     objects,
                                                     image,
                                                     ret);
                        }
                        #undef EXTRACTOR_TYPE
                        break;
                    default:
                        error = ERR_FHOG_NOT_SUPPORT_EXTRACTOR;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            error = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    #undef ELEMENT_OUT

    return error;
}

#endif