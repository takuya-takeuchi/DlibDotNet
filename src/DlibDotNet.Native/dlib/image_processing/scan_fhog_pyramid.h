#ifndef _CPP_SCAN_FHOG_PYRAMID_H_
#define _CPP_SCAN_FHOG_PYRAMID_H_

#include "../export.h"
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_TYPE PYRAMID_TYPE
#define EXTRACTOR_TYPE EXTRACTOR_TYPE
#define PYRAMID_RATE 1
#undef PYRAMID_RATE
#undef EXTRACTOR_TYPE
#undef PYRAMID_TYPE

#define scan_fhog_pyramid_new_template(pyramid_rate, ret, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            *ret = new scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>();\
            break;\
        case 2:\
            *ret = new scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>();\
            break;\
        case 3:\
            *ret = new scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>();\
            break;\
        case 4:\
            *ret = new scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>();\
            break;\
        case 6:\
            *ret = new scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>();\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_delete_template(pyramid_rate, obj) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            delete (scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>*)obj;\
            break;\
        case 2:\
            delete (scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>*)obj;\
            break;\
        case 3:\
            delete (scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>*)obj;\
            break;\
        case 4:\
            delete (scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>*)obj;\
            break;\
        case 6:\
            delete (scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>*)obj;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_set_detection_window_size_template(pyramid_rate, obj, width, height, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>*)obj)->set_detection_window_size(width, height);\
            break;\
        case 2:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>*)obj)->set_detection_window_size(width, height);\
            break;\
        case 3:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>*)obj)->set_detection_window_size(width, height);\
            break;\
        case 4:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>*)obj)->set_detection_window_size(width, height);\
            break;\
        case 6:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>*)obj)->set_detection_window_size(width, height);\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_set_nuclear_norm_regularization_strength_template(pyramid_rate, obj, strength, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>*)obj)->set_nuclear_norm_regularization_strength(strength);\
            break;\
        case 2:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>*)obj)->set_nuclear_norm_regularization_strength(strength);\
            break;\
        case 3:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>*)obj)->set_nuclear_norm_regularization_strength(strength);\
            break;\
        case 4:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>*)obj)->set_nuclear_norm_regularization_strength(strength);\
            break;\
        case 6:\
            ((scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>*)obj)->set_nuclear_norm_regularization_strength(strength);\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_evaluate_detectors_sub_template(pyramid_rate, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error) \
do {\
    object_detector<scan_fhog_pyramid<PYRAMID_TYPE<pyramid_rate>, EXTRACTOR_TYPE>>** in_objects = static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<pyramid_rate>, EXTRACTOR_TYPE>>**>(objects);\
    std::vector<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<pyramid_rate>, EXTRACTOR_TYPE>>> detectors;\
    for (int i = 0; i < objects_num; i++)\
    {\
        object_detector<scan_fhog_pyramid<PYRAMID_TYPE<pyramid_rate>, EXTRACTOR_TYPE>>& tmp = *(in_objects[i]);\
        detectors.push_back(tmp);\
    }\
    matrix<__TYPE__>& mat = *static_cast<matrix<__TYPE__>*>(image);\
    auto dect = evaluate_detectors(detectors, mat, adjust_threshold);\
    auto ret_dect = new std::vector<rectangle*>();\
    for (int i = 0; i < dect.size(); i++)\
        ret_dect->push_back(new rectangle(dect[i]));\
    *ret = ret_dect;\
} while (0)

#define scan_fhog_pyramid_evaluate_detectors_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            scan_fhog_pyramid_evaluate_detectors_sub_template(1, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error);\
            break;\
        case 2:\
            scan_fhog_pyramid_evaluate_detectors_sub_template(2, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error);\
            break;\
        case 3:\
            scan_fhog_pyramid_evaluate_detectors_sub_template(3, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error);\
            break;\
        case 4:\
            scan_fhog_pyramid_evaluate_detectors_sub_template(4, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error);\
            break;\
        case 6:\
            scan_fhog_pyramid_evaluate_detectors_sub_template(6, __TYPE__, objects, objects_num, image, adjust_threshold, ret, error);\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_num_separable_filters_template(pyramid_rate, obj, weight_index, ret, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = num_separable_filters(detector, weight_index);\
            }\
            break;\
        case 2:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = num_separable_filters(detector, weight_index);\
            }\
            break;\
        case 3:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = num_separable_filters(detector, weight_index);\
            }\
            break;\
        case 4:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = num_separable_filters(detector, weight_index);\
            }\
            break;\
        case 6:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = num_separable_filters(detector, weight_index);\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#define scan_fhog_pyramid_threshold_filter_singular_values_template(pyramid_rate, obj, threshold, weight_index, ret, error) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>(threshold_filter_singular_values(detector, threshold, weight_index));\
            }\
            break;\
        case 2:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>(threshold_filter_singular_values(detector, threshold, weight_index));\
            }\
            break;\
        case 3:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>(threshold_filter_singular_values(detector, threshold, weight_index));\
            }\
            break;\
        case 4:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>(threshold_filter_singular_values(detector, threshold, weight_index));\
            }\
            break;\
        case 6:\
            {\
                object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>& detector = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(obj));\
                *ret = new object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>(threshold_filter_singular_values(detector, threshold, weight_index));\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int scan_fhog_pyramid_new(const pyramid_type pyramid_type,
                                    const unsigned int pyramid_rate,
                                    const fhog_feature_extractor_type extractor_type,
                                    void** ret)
{
    int error = ERR_OK;

    // Too complex....
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        scan_fhog_pyramid_new_template(pyramid_rate, ret, error);
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

DLLEXPORT void scan_fhog_pyramid_delete(const pyramid_type pyramid_type,
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
                        scan_fhog_pyramid_delete_template(pyramid_rate, obj);
                        #undef EXTRACTOR_TYPE
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
    }
}

DLLEXPORT int scan_fhog_pyramid_set_detection_window_size(const pyramid_type pyramid_type,
                                                          const unsigned int pyramid_rate,
                                                          const fhog_feature_extractor_type extractor_type,
                                                          void* obj,
                                                          const unsigned long width,
                                                          const unsigned long height)
{
    int error = ERR_OK;

    // Too complex....
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        scan_fhog_pyramid_set_detection_window_size_template(pyramid_rate, obj, width, height, error);
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

DLLEXPORT int scan_fhog_pyramid_set_nuclear_norm_regularization_strength(const pyramid_type pyramid_type,
                                                                         const unsigned int pyramid_rate,
                                                                         const fhog_feature_extractor_type extractor_type,
                                                                         void* obj,
                                                                         const double strength)
{
    int error = ERR_OK;

    // Too complex....
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        scan_fhog_pyramid_set_nuclear_norm_regularization_strength_template(pyramid_rate, obj, strength, error);
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

DLLEXPORT int scan_fhog_pyramid_evaluate_detectors(const pyramid_type pyramid_type,
                                                   const unsigned int pyramid_rate,
                                                   const fhog_feature_extractor_type extractor_type,
                                                   void* objects,
                                                   const int objects_num,
                                                   const matrix_element_type type,
                                                   void* image,
                                                   const double adjust_threshold,
                                                   void** ret)
{
    int error = ERR_OK;

    // Too complex....
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
                                                     scan_fhog_pyramid_evaluate_detectors_template,
                                                     0,
                                                     0,
                                                     pyramid_rate,
                                                     objects,
                                                     objects_num,
                                                     image,
                                                     adjust_threshold,
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

    return error;
}

DLLEXPORT int scan_fhog_pyramid_num_separable_filters(const pyramid_type pyramid_type,
                                                      const unsigned int pyramid_rate,
                                                      const fhog_feature_extractor_type extractor_type,
                                                      void* obj,
                                                      const unsigned long weight_index,
                                                      unsigned int* ret)
{
    int error = ERR_OK;

    // Too complex....
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        scan_fhog_pyramid_num_separable_filters_template(pyramid_rate, obj, weight_index, ret, error);
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

DLLEXPORT int scan_fhog_pyramid_threshold_filter_singular_values(const pyramid_type pyramid_type,
                                                                 const unsigned int pyramid_rate,
                                                                 const fhog_feature_extractor_type extractor_type,
                                                                 void* obj,
                                                                 const double threshold,
                                                                 const unsigned long weight_index,
                                                                 void** ret)
{
    int error = ERR_OK;

    // Too complex....
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        scan_fhog_pyramid_threshold_filter_singular_values_template(pyramid_rate, obj, threshold, weight_index, ret, error);
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

#endif