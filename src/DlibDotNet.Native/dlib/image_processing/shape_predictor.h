#ifndef _CPP_SHAPE_PREDICTOR_H_
#define _CPP_SHAPE_PREDICTOR_H_

#include "../export.h"
#include <dlib/image_processing/shape_predictor.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

#pragma region template

#define shape_predictor_operator_template(__TYPE__, img, full_obj_detect) \
do {\
    auto result = (*predictor)(*((array2d<__TYPE__>*)img), *rect);\
    *full_obj_detect = new full_object_detection(result);\
} while (0)

#define shape_predictor_matrix_operator_template(__TYPE__, img, full_obj_detect) \
do {\
    auto result = (*predictor)(*((matrix<__TYPE__>*)img), *rect);\
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
            shape_predictor_operator_template(uint8_t, img, full_obj_detect);
            break;
        case array2d_type::UInt16:
            shape_predictor_operator_template(uint16_t, img, full_obj_detect);
            break;
        case array2d_type::UInt32:
            shape_predictor_operator_template(uint32_t, img, full_obj_detect);
            break;
        case array2d_type::Int8:
            shape_predictor_operator_template(int8_t, img, full_obj_detect);
            break;
        case array2d_type::Int16:
            shape_predictor_operator_template(int16_t, img, full_obj_detect);
            break;
        case array2d_type::Int32:
            shape_predictor_operator_template(int32_t, img, full_obj_detect);
            break;
        case array2d_type::Float:
            shape_predictor_operator_template(float, img, full_obj_detect);
            break;
        case array2d_type::Double:
            shape_predictor_operator_template(double, img, full_obj_detect);
            break;
        case array2d_type::RgbPixel:
            shape_predictor_operator_template(rgb_pixel, img, full_obj_detect);
            break;
        case array2d_type::HsiPixel:
            shape_predictor_operator_template(hsi_pixel, img, full_obj_detect);
            break;
        case array2d_type::RgbAlphaPixel:
            shape_predictor_operator_template(rgb_alpha_pixel, img, full_obj_detect);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
            shape_predictor_matrix_operator_template(uint8_t, img, full_obj_detect);
            break;
        case matrix_element_type::UInt16:
            shape_predictor_matrix_operator_template(uint16_t, img, full_obj_detect);
            break;
        case matrix_element_type::UInt32:
            shape_predictor_matrix_operator_template(uint32_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int8:
            shape_predictor_matrix_operator_template(int8_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int16:
            shape_predictor_matrix_operator_template(int16_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int32:
            shape_predictor_matrix_operator_template(int32_t, img, full_obj_detect);
            break;
        case matrix_element_type::Float:
            shape_predictor_matrix_operator_template(float, img, full_obj_detect);
            break;
        case matrix_element_type::Double:
            shape_predictor_matrix_operator_template(double, img, full_obj_detect);
            break;
        case matrix_element_type::RgbPixel:
            shape_predictor_matrix_operator_template(rgb_pixel, img, full_obj_detect);
            break;
        case matrix_element_type::HsiPixel:
            shape_predictor_matrix_operator_template(hsi_pixel, img, full_obj_detect);
            break;
        case matrix_element_type::RgbAlphaPixel:
            shape_predictor_matrix_operator_template(rgb_alpha_pixel, img, full_obj_detect);
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
            shape_predictor_operator_template(uint8_t, img, full_obj_detect);
            break;
        case array2d_type::UInt16:
            shape_predictor_operator_template(uint16_t, img, full_obj_detect);
            break;
        case array2d_type::UInt32:
            shape_predictor_operator_template(uint32_t, img, full_obj_detect);
            break;
        case array2d_type::Int8:
            shape_predictor_operator_template(int8_t, img, full_obj_detect);
            break;
        case array2d_type::Int16:
            shape_predictor_operator_template(int16_t, img, full_obj_detect);
            break;
        case array2d_type::Int32:
            shape_predictor_operator_template(int32_t, img, full_obj_detect);
            break;
        case array2d_type::Float:
            shape_predictor_operator_template(float, img, full_obj_detect);
            break;
        case array2d_type::Double:
            shape_predictor_operator_template(double, img, full_obj_detect);
            break;
        case array2d_type::RgbPixel:
            shape_predictor_operator_template(rgb_pixel, img, full_obj_detect);
            break;
        case array2d_type::HsiPixel:
            shape_predictor_operator_template(hsi_pixel, img, full_obj_detect);
            break;
        case array2d_type::RgbAlphaPixel:
            shape_predictor_operator_template(rgb_alpha_pixel, img, full_obj_detect);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
            shape_predictor_matrix_operator_template(uint8_t, img, full_obj_detect);
            break;
        case matrix_element_type::UInt16:
            shape_predictor_matrix_operator_template(uint16_t, img, full_obj_detect);
            break;
        case matrix_element_type::UInt32:
            shape_predictor_matrix_operator_template(uint32_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int8:
            shape_predictor_matrix_operator_template(int8_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int16:
            shape_predictor_matrix_operator_template(int16_t, img, full_obj_detect);
            break;
        case matrix_element_type::Int32:
            shape_predictor_matrix_operator_template(int32_t, img, full_obj_detect);
            break;
        case matrix_element_type::Float:
            shape_predictor_matrix_operator_template(float, img, full_obj_detect);
            break;
        case matrix_element_type::Double:
            shape_predictor_matrix_operator_template(double, img, full_obj_detect);
            break;
        case matrix_element_type::RgbPixel:
            shape_predictor_matrix_operator_template(rgb_pixel, img, full_obj_detect);
            break;
        case matrix_element_type::HsiPixel:
            shape_predictor_matrix_operator_template(hsi_pixel, img, full_obj_detect);
            break;
        case matrix_element_type::RgbAlphaPixel:
            shape_predictor_matrix_operator_template(rgb_alpha_pixel, img, full_obj_detect);
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