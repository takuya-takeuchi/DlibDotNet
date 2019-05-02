#ifndef _CPP_STRUCTURAL_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_
#define _CPP_STRUCTURAL_OBJECT_DETECTION_TRAINER_SCAN_FHOG_PYRAMID_H_

#include "../../export.h"
#include <dlib/image_processing/scan_fhog_pyramid.h>
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

#define structural_object_detection_trainer_scan_fhog_pyramid_new_template(pyramid_rate, scanner, ret, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                auto& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 2:\
            {\
                auto& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 3:\
            {\
                auto& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 4:\
            {\
                auto& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        case 6:\
            {\
                auto& tmp = *(static_cast<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>*>(scanner));\
                *ret = new structural_object_detection_trainer<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>(tmp);\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
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

#define structural_object_detection_trainer_scan_fhog_pyramid_be_verbose_template(pyramid_rate, obj, error) \
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
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_c_template(pyramid_rate, obj, c, error) \
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
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon_template(pyramid_rate, obj, epsilon, error) \
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
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads_template(pyramid_rate, obj, threads, error) \
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
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define structural_object_detection_trainer_scan_fhog_pyramid_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
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
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
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
                        structural_object_detection_trainer_scan_fhog_pyramid_new_template(pyramid_rate, scanner, ret, error);
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
                        structural_object_detection_trainer_scan_fhog_pyramid_be_verbose_template(pyramid_rate, obj, error);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_c(const pyramid_type pyramid_type,
                                                                          const unsigned int pyramid_rate,
                                                                          const fhog_feature_extractor_type extractor_type,
                                                                          void* obj,
                                                                          const double c)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_c_template(pyramid_rate, obj, c, error);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon(const pyramid_type pyramid_type,
                                                                                const unsigned int pyramid_rate,
                                                                                const fhog_feature_extractor_type extractor_type,
                                                                                void* obj,
                                                                                const double epsilon)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon_template(pyramid_rate, obj, epsilon, error);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads(const pyramid_type pyramid_type,
                                                                                    const unsigned int pyramid_rate,
                                                                                    const fhog_feature_extractor_type extractor_type,
                                                                                    void* obj,
                                                                                    const unsigned long threads)
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
                        structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads_template(pyramid_rate, obj, threads, error);
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

DLLEXPORT int structural_object_detection_trainer_scan_fhog_pyramid_train_rectangle(const pyramid_type pyramid_type,
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
                                                     structural_object_detection_trainer_scan_fhog_pyramid_train_template,
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