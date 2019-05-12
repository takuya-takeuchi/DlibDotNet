#ifndef _CPP_OBJECT_DETECTOR_SCAN_FHOG_PYRAMID_H_
#define _CPP_OBJECT_DETECTOR_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/object_detector.h>
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include <dlib/svm/structural_object_detection_trainer.h>
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

#define object_detector_scan_fhog_pyramid_new_template(pyramid_rate, ret) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>();\
            break;\
        case 2:\
            *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>();\
            break;\
        case 3:\
            *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>();\
            break;\
        case 4:\
            *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>();\
            break;\
        case 6:\
            *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>();\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define object_detector_scan_fhog_pyramid_delete_template(pyramid_rate, obj) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            delete (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 2:\
            delete (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 3:\
            delete (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 4:\
            delete (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 6:\
            delete (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj;\
            break;\
    }\
} while (0)

#define object_detector_scan_fhog_pyramid_deserialize_template(file_name, pyramid_rate, ret, error_message) \
do {\
    try\
    {\
        switch(pyramid_rate)\
        {\
            case 1:\
                {\
                    auto detector = (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)ret;\
                    dlib::deserialize(file_name) >> (*detector);\
                }\
                break;\
            case 2:\
                {\
                    auto detector = (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)ret;\
                    dlib::deserialize(file_name) >> (*detector);\
                }\
                break;\
            case 3:\
                {\
                    auto detector = (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)ret;\
                    dlib::deserialize(file_name) >> (*detector);\
                }\
                break;\
            case 4:\
                {\
                    auto detector = (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)ret;\
                    dlib::deserialize(file_name) >> (*detector);\
                }\
                break;\
            case 6:\
                {\
                    auto detector = (object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)ret;\
                    dlib::deserialize(file_name) >> (*detector);\
                }\
                break;\
            default:\
                error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
                break;\
        }\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
} while (0)

#define object_detector_scan_fhog_pyramid_serialize_template(file_name, pyramid_rate, obj, error_message) \
do {\
    try\
    {\
        switch(pyramid_rate)\
        {\
            case 1:\
                {\
                    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                    dlib::serialize(file_name) << detector;\
                }\
                break;\
            case 2:\
                {\
                    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                    dlib::serialize(file_name) << detector;\
                }\
                break;\
            case 3:\
                {\
                    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                    dlib::serialize(file_name) << detector;\
                }\
                break;\
            case 4:\
                {\
                    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                    dlib::serialize(file_name) << detector;\
                }\
                break;\
            case 6:\
                {\
                    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                    dlib::serialize(file_name) << detector;\
                }\
                break;\
            default:\
                error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
                break;\
        }\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
} while (0)

#define object_detector_scan_fhog_pyramid_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
do { \
    auto& in_image = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
\
    std::vector<rectangle> dets;\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 2:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 3:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 4:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 6:\
            {\
                auto& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
    auto tmp = new std::vector<rectangle*>();\
    for (auto i = 0; i < dets.size(); i++)\
        tmp->push_back(new rectangle(dets[i]));\
    *ret = tmp;\
} while (0)

#pragma endregion template

DLLEXPORT int object_detector_scan_fhog_pyramid_new(const pyramid_type pyramid_type,
                                                    const unsigned int pyramid_rate,
                                                    const fhog_feature_extractor_type extractor_type,
                                                    void** obj)
{
    int error = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        object_detector_scan_fhog_pyramid_new_template(pyramid_rate, obj);
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

    return error;
}

DLLEXPORT void object_detector_scan_fhog_pyramid_delete(const pyramid_type pyramid_type,
                                                        const unsigned int pyramid_rate,
                                                        const fhog_feature_extractor_type extractor_type,
                                                        void* obj)
{
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        object_detector_scan_fhog_pyramid_delete_template(pyramid_rate, obj);
                        #undef EXTRACTOR_TYPE
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
    }
}

DLLEXPORT int object_detector_scan_fhog_pyramid_deserialize(const char* file_name,
                                                            const pyramid_type pyramid_type,
                                                            const unsigned int pyramid_rate,
                                                            const fhog_feature_extractor_type extractor_type,
                                                            void* ret,
                                                            std::string** error_message)
{
    int error = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        object_detector_scan_fhog_pyramid_deserialize_template(file_name, pyramid_rate, ret, error_message);
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

    return error;
}

DLLEXPORT int object_detector_scan_fhog_pyramid_serialize(const char* file_name,
                                                          const pyramid_type pyramid_type,
                                                          const unsigned int pyramid_rate,
                                                          const fhog_feature_extractor_type extractor_type,
                                                          void* obj,
                                                          std::string** error_message)
{
    int error = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        object_detector_scan_fhog_pyramid_serialize_template(file_name, pyramid_rate, obj, error_message);
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

    return error;
}

#pragma region operator

DLLEXPORT int object_detector_scan_fhog_pyramid_operator(const pyramid_type pyramid_type,
                                                         const unsigned int pyramid_rate,
                                                         const fhog_feature_extractor_type extractor_type,
                                                         void* obj,
                                                         const matrix_element_type type,
                                                         void* image,
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
                                                     object_detector_scan_fhog_pyramid_operator_template,
                                                     0,
                                                     0,
                                                     pyramid_rate,
                                                     obj,
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


#pragma endregion operator

#endif