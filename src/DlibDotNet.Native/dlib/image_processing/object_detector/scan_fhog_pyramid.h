#ifndef _CPP_OBJECT_DETECTOR_SCAN_FHOG_PYRAMID_H_
#define _CPP_OBJECT_DETECTOR_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/object_detector.h>
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include <dlib/svm/structural_object_detection_trainer.h>
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
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
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
                err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
                break;\
        }\
    }\
    catch (serialization_error& e)\
    {\
        err = ERR_GENERAL_SERIALIZATION;\
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
                err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
                break;\
        }\
    }\
    catch (serialization_error& e)\
    {\
        err = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
} while (0)

#define object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, image) \
do { \
    dlib::matrix<ELEMENT_IN>& in_image = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
\
    std::vector<rectangle> dets;\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 2:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 3:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 4:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        case 6:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                dets = detector(in_image);\
            }\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
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
    int err = ERR_OK;

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

    return err;
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
    int err = ERR_OK;

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

    return err;
}

DLLEXPORT int object_detector_scan_fhog_pyramid_serialize(const char* file_name,
                                                          const pyramid_type pyramid_type,
                                                          const unsigned int pyramid_rate,
                                                          const fhog_feature_extractor_type extractor_type,
                                                          void* obj,
                                                          std::string** error_message)
{
    int err = ERR_OK;

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

    return err;
}

#pragma region operator

DLLEXPORT int object_detector_scan_fhog_pyramid_operator(const pyramid_type pyramid_type,
                                                         const unsigned int pyramid_rate,
                                                         const fhog_feature_extractor_type extractor_type,
                                                         void* obj,
                                                         const matrix_element_type element_type,
                                                         void* matrix,
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
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt16:
                                #define ELEMENT_IN uint16_t
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt32:
                                #define ELEMENT_IN uint32_t
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int8:
                                #define ELEMENT_IN int8_t
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int16:
                                #define ELEMENT_IN int16_t
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int32:
                                #define ELEMENT_IN int32_t
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Float:
                                #define ELEMENT_IN float
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Double:
                                #define ELEMENT_IN double
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::RgbPixel:
                                #define ELEMENT_IN rgb_pixel
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::HsiPixel:
                                #define ELEMENT_IN hsi_pixel
                                object_detector_scan_fhog_pyramid_operator_template(ret, pyramid_rate, obj, matrix);
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


#pragma endregion operator

#endif