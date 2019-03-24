#ifndef _CPP_STRUCTURAL_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_
#define _CPP_STRUCTURAL_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/scan_fhog_pyramid.h>
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

#define structural_object_detection_trainer_scan_fhog_pyramid_new_template(pyramid_rate, scanner, ret, err) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 2:\
            {\
                scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 3:\
            {\
                scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 4:\
            {\
                scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 6:\
            {\
                scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_delete_template(pyramid_rate, obj) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            delete (structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 2:\
            delete (structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 3:\
            delete (structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 4:\
            delete (structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj;\
            break;\
        case 6:\
            delete (structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_be_verbose_template(pyramid_rate, obj, err) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj)->be_verbose();\
            break;\
        case 2:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj)->be_verbose();\
            break;\
        case 3:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj)->be_verbose();\
            break;\
        case 4:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj)->be_verbose();\
            break;\
        case 6:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj)->be_verbose();\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_c_template(pyramid_rate, obj, c, err) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj)->set_c(c);\
            break;\
        case 2:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj)->set_c(c);\
            break;\
        case 3:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj)->set_c(c);\
            break;\
        case 4:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj)->set_c(c);\
            break;\
        case 6:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj)->set_c(c);\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon_template(pyramid_rate, obj, epsilon, err) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj)->set_epsilon(epsilon);\
            break;\
        case 2:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj)->set_epsilon(epsilon);\
            break;\
        case 3:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj)->set_epsilon(epsilon);\
            break;\
        case 4:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj)->set_epsilon(epsilon);\
            break;\
        case 6:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj)->set_epsilon(epsilon);\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads_template(pyramid_rate, obj, threads, err) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj)->set_num_threads(threads);\
            break;\
        case 2:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj)->set_num_threads(threads);\
            break;\
        case 3:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj)->set_num_threads(threads);\
            break;\
        case 4:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj)->set_num_threads(threads);\
            break;\
        case 6:\
            ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj)->set_num_threads(threads);\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects) \
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
                auto detector = ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*)obj)->train(tmp_images, tmp_objects);\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>(detector);\
            }\
            break;\
        case 2:\
            {\
                auto detector = ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*)obj)->train(tmp_images, tmp_objects);\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>(detector);\
            }\
            break;\
        case 3:\
            {\
                auto detector = ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*)obj)->train(tmp_images, tmp_objects);\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>(detector);\
            }\
            break;\
        case 4:\
            {\
                auto detector = ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*)obj)->train(tmp_images, tmp_objects);\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>(detector);\
            }\
            break;\
        case 6:\
            {\
                auto detector = ((structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*)obj)->train(tmp_images, tmp_objects);\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>(detector);\
            }\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_new(const pyramid_type pyramid_type,
                                                                        const unsigned int pyramid_rate,
                                                                        const fhog_feature_extractor_type extractor_type,
                                                                        void* scanner,
                                                                        void** ret)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_new_template(pyramid_rate, scanner, ret, err);
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

DLLEXPORT void structural_object_detection_trainer_scan_fhog_pyramid_delete(const pyramid_type pyramid_type,
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
                        structural_object_detection_trainer_scan_fhog_pyramid_delete_template(pyramid_rate, obj);
                        #undef EXTRACTOR_TYPE
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
    }
}

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_be_verbose(const pyramid_type pyramid_type,
                                                                               const unsigned int pyramid_rate,
                                                                               const fhog_feature_extractor_type extractor_type,
                                                                               void* obj)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_be_verbose_template(pyramid_rate, obj, err);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_c(const pyramid_type pyramid_type,
                                                                          const unsigned int pyramid_rate,
                                                                          const fhog_feature_extractor_type extractor_type,
                                                                          void* obj,
                                                                          const double c)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_c_template(pyramid_rate, obj, c, err);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon(const pyramid_type pyramid_type,
                                                                                const unsigned int pyramid_rate,
                                                                                const fhog_feature_extractor_type extractor_type,
                                                                                void* obj,
                                                                                const double epsilon)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon_template(pyramid_rate, obj, epsilon, err);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads(const pyramid_type pyramid_type,
                                                                                    const unsigned int pyramid_rate,
                                                                                    const fhog_feature_extractor_type extractor_type,
                                                                                    void* obj,
                                                                                    const unsigned long threads)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads_template(pyramid_rate, obj, threads, err);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_train_rectangle(const pyramid_type pyramid_type,
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
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt16:
                                #define ELEMENT_IN uint16_t
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::UInt32:
                                #define ELEMENT_IN uint32_t
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int8:
                                #define ELEMENT_IN int8_t
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int16:
                                #define ELEMENT_IN int16_t
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Int32:
                                #define ELEMENT_IN int32_t
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Float:
                                #define ELEMENT_IN float
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::Double:
                                #define ELEMENT_IN double
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::RgbPixel:
                                #define ELEMENT_IN rgb_pixel
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
                                #undef ELEMENT_IN
                                break;
                            case matrix_element_type::HsiPixel:
                                #define ELEMENT_IN hsi_pixel
                                structural_object_detection_trainer_scan_fhog_pyramid_train_template(ret, pyramid_rate, images, objects);
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