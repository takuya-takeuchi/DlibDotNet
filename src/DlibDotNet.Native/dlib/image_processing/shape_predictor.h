#ifndef _CPP_SHAPE_PREDICTOR_H_
#define _CPP_SHAPE_PREDICTOR_H_

#include "../export.h"
#include <dlib/image_processing/shape_predictor.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define shape_predictor_operator_template(img, full_obj_detect) \
do {\
    auto result = (*predictor)(*((array2d<ELEMENT>*)img), *rect);\
    *full_obj_detect = new full_object_detection(result);\
} while (0)

#define shape_predictor_matrix_operator_template(img, full_obj_detect) \
do {\
    auto result = (*predictor)(*((matrix<ELEMENT>*)img), *rect);\
    *full_obj_detect = new full_object_detection(result);\
} while (0)

#pragma endregion template

DLLEXPORT shape_predictor* shape_predictor_new()
{
    return new shape_predictor();
}

DLLEXPORT shape_predictor* deserialize_shape_predictor(const char* file_name)
{
    shape_predictor* predictor = new shape_predictor();
    dlib::deserialize(file_name) >> (*predictor);
    return predictor;
}

DLLEXPORT shape_predictor* deserialize_shape_predictor_proxy(proxy_deserialize* proxy)
{
    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
    shape_predictor* predictor = new shape_predictor();
    p >> (*predictor);
    return predictor;
}

DLLEXPORT dlib::point_transform_affine* normalizing_tform(dlib::rectangle* rect)
{
    dlib::rectangle& r = *static_cast<dlib::rectangle*>(rect);

    auto ret = impl::normalizing_tform(r);
    return new dlib::point_transform_affine(ret);
}

#pragma region shape_predictor_operator

DLLEXPORT int shape_predictor_operator(shape_predictor* predictor,
                                       array2d_type img_type,
                                       void* img,
                                       rectangle* rect,
                                       full_object_detection** full_obj_detect)
{
    int err = ERR_OK;
    *full_obj_detect = nullptr;

    switch(img_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT uint8_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::UInt16:
            #define ELEMENT uint16_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::UInt32:
            #define ELEMENT uint32_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int8:
            #define ELEMENT int8_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int16:
            #define ELEMENT int16_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int32:
            #define ELEMENT int32_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Float:
            #define ELEMENT float
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Double:
            #define ELEMENT double
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT rgb_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT hsi_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int shape_predictor_matrix_operator(shape_predictor* predictor,
                                              matrix_element_type img_type,
                                              void* img,
                                              rectangle* rect,
                                              full_object_detection** full_obj_detect)
{
    int err = ERR_OK;
    *full_obj_detect = nullptr;

    switch(img_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int shape_predictor_operator_mmod_rect(shape_predictor* predictor,
                                                 array2d_type img_type,
                                                 void* img,
                                                 mmod_rect* rect,
                                                 full_object_detection** full_obj_detect)
{
    int err = ERR_OK;
    *full_obj_detect = nullptr;

    switch(img_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT uint8_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::UInt16:
            #define ELEMENT uint16_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::UInt32:
            #define ELEMENT uint32_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int8:
            #define ELEMENT int8_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int16:
            #define ELEMENT int16_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Int32:
            #define ELEMENT int32_t
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Float:
            #define ELEMENT float
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::Double:
            #define ELEMENT double
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT rgb_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT hsi_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            shape_predictor_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int shape_predictor_matrix_operator_mmod_rect(shape_predictor* predictor,
                                                        matrix_element_type img_type,
                                                        void* img,
                                                        mmod_rect* rect,
                                                        full_object_detection** full_obj_detect)
{
    int err = ERR_OK;
    *full_obj_detect = nullptr;

    switch(img_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            shape_predictor_matrix_operator_template(img, full_obj_detect);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion shape_predictor_operator

DLLEXPORT unsigned int shape_predictor_num_parts(shape_predictor* predictor)
{
    return predictor->num_parts();
}

DLLEXPORT unsigned int shape_predictor_num_features(shape_predictor* predictor)
{
    return predictor->num_features();
}

DLLEXPORT void shape_predictor_delete(shape_predictor* obj)
{
	delete obj;
}

#endif