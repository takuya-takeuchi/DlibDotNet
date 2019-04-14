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

#define shape_predictor_test_shape_predictor_template(__TYPE__, predictor, images, objects, scales, ret) \
do {\
    std::vector<std::vector<full_object_detection>> in_Objects;\
    std::vector<std::vector<double>> in_Scales;\
    vector_vector_pointer_to_value(full_object_detection, objects, in_Objects);\
    dlib::array<array2d<__TYPE__>>& in_images = *static_cast<dlib::array<array2d<__TYPE__>>*>(images);\
    vector_vector_valueType_to_value(double, scales, in_Scales);\
\
    double d = dlib::test_shape_predictor(*predictor, in_images, in_Objects, in_Scales);\
    *ret = d;\
} while (0)

#pragma endregion template

DLLEXPORT shape_predictor* shape_predictor_new()
{
    return new shape_predictor();
}

DLLEXPORT void shape_predictor_delete(shape_predictor* obj)
{
	delete obj;
}

DLLEXPORT void serialize_shape_predictor(shape_predictor* predictor, const char* file_name)
{
    dlib::serialize(file_name) << (*predictor);
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

DLLEXPORT int shape_predictor_test_shape_predictor(shape_predictor* predictor,
                                                   array2d_type img_type,
                                                   void* images,
                                                   std::vector<std::vector<full_object_detection*>*>* objects,
                                                   std::vector<std::vector<double>*>* scales,
                                                   double* ret)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            shape_predictor_test_shape_predictor_template(uint8_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::UInt16:
            shape_predictor_test_shape_predictor_template(uint16_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::UInt32:
            shape_predictor_test_shape_predictor_template(uint32_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::Int8:
            shape_predictor_test_shape_predictor_template(int8_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::Int16:
            shape_predictor_test_shape_predictor_template(int16_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::Int32:
            shape_predictor_test_shape_predictor_template(int32_t, predictor, images, objects, scales, ret);
            break;
        case array2d_type::Float:
            shape_predictor_test_shape_predictor_template(float, predictor, images, objects, scales, ret);
            break;
        case array2d_type::Double:
            shape_predictor_test_shape_predictor_template(double, predictor, images, objects, scales, ret);
            break;
        case array2d_type::RgbPixel:
            shape_predictor_test_shape_predictor_template(rgb_pixel, predictor, images, objects, scales, ret);
            break;
        case array2d_type::HsiPixel:
            shape_predictor_test_shape_predictor_template(hsi_pixel, predictor, images, objects, scales, ret);
            break;
        case array2d_type::RgbAlphaPixel:
            shape_predictor_test_shape_predictor_template(rgb_alpha_pixel, predictor, images, objects, scales, ret);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif