#ifndef _CPP_INTERPOLATION_H_
#define _CPP_INTERPOLATION_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define FUNCTION function
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef FUNCTION
#undef ELEMENT_IN
#undef ELEMENT_OUT

#define interpolation_template(ret, type, img) \
do { \
    ret = ERR_OK;\
    switch(type)\
    {\
        case array2d_type::UInt8:\
            dlib::FUNCTION(*((array2d<uint8_t>*)img));\
            break;\
        case array2d_type::UInt16:\
            dlib::FUNCTION(*((array2d<uint16_t>*)img));\
            break;\
        case array2d_type::Float:\
            dlib::FUNCTION(*((array2d<float>*)img));\
            break;\
        case array2d_type::Double:\
            dlib::FUNCTION(*((array2d<double>*)img));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::FUNCTION(*((array2d<rgb_pixel>*)img));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::FUNCTION(*((array2d<hsi_pixel>*)img));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            dlib::FUNCTION(*((array2d<rgb_alpha_pixel>*)img));\
            break;\
        default:\
            ret = ERR_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define interpolation_matrix_template(ret, type, img) \
do { \
    ret = ERR_OK;\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            dlib::FUNCTION(*((dlib::matrix<uint8_t>*)img));\
            break;\
        case matrix_element_type::UInt16:\
            dlib::FUNCTION(*((dlib::matrix<uint16_t>*)img));\
            break;\
        case matrix_element_type::UInt32:\
            dlib::FUNCTION(*((dlib::matrix<uint32_t>*)img));\
            break;\
        case matrix_element_type::Int8:\
            dlib::FUNCTION(*((dlib::matrix<int8_t>*)img));\
            break;\
        case matrix_element_type::Int16:\
            dlib::FUNCTION(*((dlib::matrix<int16_t>*)img));\
            break;\
        case matrix_element_type::Int32:\
            dlib::FUNCTION(*((dlib::matrix<int32_t>*)img));\
            break;\
        case matrix_element_type::Float:\
            dlib::FUNCTION(*((dlib::matrix<float>*)img));\
            break;\
        case matrix_element_type::Double:\
            dlib::FUNCTION(*((dlib::matrix<double>*)img));\
            break;\
        case matrix_element_type::RgbPixel:\
            dlib::FUNCTION(*((dlib::matrix<rgb_pixel>*)img));\
            break;\
        case matrix_element_type::HsiPixel:\
            dlib::FUNCTION(*((dlib::matrix<hsi_pixel>*)img));\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            dlib::FUNCTION(*((dlib::matrix<rgb_alpha_pixel>*)img));\
            break;\
        default:\
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define interpolation_inout_template(ret, in_type, in_img, out_img) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::UInt16:\
            dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Float:\
            dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Double:\
            dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::FUNCTION(*((array2d<hsi_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            dlib::FUNCTION(*((array2d<rgb_alpha_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define interpolation_inout2_template(ret, in_type, in_img, out_img, arg1, type) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::UInt16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Float:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Double:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::RgbPixel:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define interpolation_inout3_template(ret, in_type, in_img, out_img, type) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::UInt16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Float:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Double:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::RgbPixel:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define rotate_image_template(ret, in_type, in_img, out_img, arg1) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::FUNCTION(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::UInt16:\
            dlib::FUNCTION(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::Float:\
            dlib::FUNCTION(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::Double:\
            dlib::FUNCTION(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::RgbPixel:\
            dlib::FUNCTION(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define transform_image_sub_template(in_img, out_img, type, mapping_type, mapping_obj) \
do { \
    switch(mapping_type)\
    {\
        case point_mapping_type::Rotator:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_rotator*)mapping_obj));\
            break;\
        case point_mapping_type::Transform:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform*)mapping_obj));\
            break;\
        case point_mapping_type::TransformAffine:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform_affine*)mapping_obj));\
            break;\
        case point_mapping_type::TransformProjective:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform_projective*)mapping_obj));\
            break;\
    }\
} while (0)

#define transform_image_template(ret, in_type, in_img, out_img, point_mapping_type, mapping_obj, type) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<uint8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::UInt16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<uint16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::Float:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<float>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::Double:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<double>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::RgbPixel:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<rgb_pixel>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define extract_image_chips_template(ret, in_type, in_img, chips, array) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::extract_image_chips(*((array2d<uint8_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::UInt16:\
            dlib::extract_image_chips(*((array2d<uint16_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::Float:\
            dlib::extract_image_chips(*((array2d<float>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::Double:\
            dlib::extract_image_chips(*((array2d<double>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::extract_image_chips(*((array2d<rgb_pixel>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::extract_image_chips(*((array2d<hsi_pixel>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define extract_image_chip_template(ret, in_type, in_img, chip, out_chip) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            dlib::extract_image_chip(*((array2d<uint8_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::UInt16:\
            dlib::extract_image_chip(*((array2d<uint16_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::Float:\
            dlib::extract_image_chip(*((array2d<float>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::Double:\
            dlib::extract_image_chip(*((array2d<double>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::RgbPixel:\
            dlib::extract_image_chip(*((array2d<rgb_pixel>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::HsiPixel:\
            dlib::extract_image_chip(*((array2d<hsi_pixel>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define extract_image_chip2_template(ret, in_img, chip, type, out_chip)\
do { \
    switch(type)\
    {\
        case interpolation_type::NearestNeighbor:\
            dlib::extract_image_chip(*((array2d<ELEMENT_OUT>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip), interpolate_nearest_neighbor());\
            break;\
        case interpolation_type::Bilinear:\
            dlib::extract_image_chip(*((array2d<ELEMENT_OUT>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip), interpolate_bilinear());\
            break;\
        case interpolation_type::Quadratic:\
            dlib::extract_image_chip(*((array2d<ELEMENT_OUT>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip), interpolate_quadratic());\
            break;\
    }\
} while (0)

#define extract_image_chips_matrix_template(ret, in_type, in_img, chips, array) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case matrix_element_type::UInt8:\
            dlib::extract_image_chips(*((matrix<uint8_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::UInt16:\
            dlib::extract_image_chips(*((matrix<uint16_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::UInt32:\
            dlib::extract_image_chips(*((matrix<uint32_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::Int8:\
            dlib::extract_image_chips(*((matrix<int8_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::Int16:\
            dlib::extract_image_chips(*((matrix<int16_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::Int32:\
            dlib::extract_image_chips(*((matrix<int32_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::Float:\
            dlib::extract_image_chips(*((matrix<float>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::Double:\
            dlib::extract_image_chips(*((matrix<double>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::RgbPixel:\
            dlib::extract_image_chips(*((matrix<rgb_pixel>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case matrix_element_type::HsiPixel:\
            dlib::extract_image_chips(*((matrix<hsi_pixel>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define extract_image_chip_matrix_template(ret, in_type, in_img, chip, out_chip) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case matrix_element_type::UInt8:\
            dlib::extract_image_chip(*((matrix<uint8_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::UInt16:\
            dlib::extract_image_chip(*((matrix<uint16_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::UInt32:\
            dlib::extract_image_chip(*((matrix<uint32_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::Int8:\
            dlib::extract_image_chip(*((matrix<int8_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::Int16:\
            dlib::extract_image_chip(*((matrix<int16_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::Int32:\
            dlib::extract_image_chip(*((matrix<int32_t>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::Float:\
            dlib::extract_image_chip(*((matrix<float>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::Double:\
            dlib::extract_image_chip(*((matrix<double>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::RgbPixel:\
            dlib::extract_image_chip(*((matrix<rgb_pixel>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        case matrix_element_type::HsiPixel:\
            dlib::extract_image_chip(*((matrix<hsi_pixel>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip));\
            break;\
        default:\
            ret = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define extract_image_chip_matrix2_template(ret, in_type, in_img, chip, type, out_chip) \
do { \
    switch(type)\
    {\
        case interpolation_type::NearestNeighbor:\
            dlib::extract_image_chip(*((matrix<ELEMENT_OUT>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip), interpolate_nearest_neighbor());\
            break;\
        case interpolation_type::Bilinear:\
            dlib::extract_image_chip(*((matrix<ELEMENT_OUT>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip), interpolate_bilinear());\
            break;\
        case interpolation_type::Quadratic:\
            dlib::extract_image_chip(*((matrix<ELEMENT_OUT>*)in_img), *chip, *((matrix<ELEMENT_OUT>*)out_chip), interpolate_quadratic());\
            break;\
    }\
} while (0)

#define jitter_image_template(in_img, r, out_img) \
do { \
    dlib::matrix<ELEMENT_IN>& in = *(static_cast<dlib::matrix<ELEMENT_IN>*>(in_img));\
    dlib::rand& in_r = *(static_cast<dlib::rand*>(r));\
    auto ret = dlib::jitter_image(in, in_r);\
    *out_img = new dlib::matrix<ELEMENT_IN>(ret);\
} while (0)

#pragma endregion template

#pragma region flip_image_left_right

DLLEXPORT int flip_image_left_right(array2d_type type, void* img)
{
    int err = ERR_OK;

    #define FUNCTION flip_image_left_right
    interpolation_template(err, type, img);
    #undef FUNCTION

    return err;
}

DLLEXPORT int flip_image_left_right2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img )
{
    int err = ERR_OK;

    #define FUNCTION flip_image_left_right
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_OUT hsi_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT_OUT rgb_alpha_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

#pragma endregion flip_image_left_right

DLLEXPORT int flip_image_up_down(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img )
{
    int err = ERR_OK;

    #define FUNCTION flip_image_up_down
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_OUT hsi_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT_OUT rgb_alpha_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

DLLEXPORT int pyramid_up(array2d_type type, void* img)
{
    int err = ERR_OK;

    #define FUNCTION pyramid_up
    interpolation_template(err, type, img);
    #undef FUNCTION

    return err;
}

DLLEXPORT int pyramid_up_matrix(matrix_element_type type, void* img)
{
    int err = ERR_OK;

    #define FUNCTION pyramid_up
    interpolation_matrix_template(err, type, img);
    #undef FUNCTION

    return err;
}

#pragma region resize_image

DLLEXPORT int resize_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int err = ERR_OK;

    if (in_type == array2d_type::HsiPixel || in_type == array2d_type::RgbAlphaPixel)
        return ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;

    #define FUNCTION resize_image
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

DLLEXPORT int resize_image2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, interpolation_type type)
{
    int err = ERR_OK;

    #define FUNCTION resize_image
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

DLLEXPORT int resize_image3(array2d_type type, void* img, double size_scale)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::resize_image(size_scale, *((array2d<uint8_t>*)img));
            break;
        case array2d_type::UInt16:
            dlib::resize_image(size_scale, *((array2d<uint16_t>*)img));
            break;
        case array2d_type::Float:
            dlib::resize_image(size_scale, *((array2d<float>*)img));
            break;
        case array2d_type::Double:
            dlib::resize_image(size_scale, *((array2d<double>*)img));
            break;
        case array2d_type::RgbPixel:
            dlib::resize_image(size_scale, *((array2d<rgb_pixel>*)img));
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion resize_image

#pragma region rotate_image

DLLEXPORT int rotate_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double angle)
{
    int err = ERR_OK;

    #define FUNCTION rotate_image
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

DLLEXPORT int rotate_image2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double angle, interpolation_type type)
{
    int err = ERR_OK;

    #define FUNCTION rotate_image
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

#pragma endregion rotate_image

DLLEXPORT int transform_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, point_mapping_type mapping_type, void* mapping_obj, interpolation_type type)
{
    int err = ERR_OK;

    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region chip_details

DLLEXPORT chip_details* chip_details_new()
{
    return new dlib::chip_details();
}

DLLEXPORT chip_details* chip_details_new2(drectangle* rect, chip_dims* dims)
{
    drectangle& r = *rect;
    chip_dims& d = *dims;
    return new dlib::chip_details(r, d);
}

DLLEXPORT chip_details* chip_details_new3(rectangle* rect, chip_dims* dims)
{
    rectangle& r = *rect;
    chip_dims& d = *dims;
    return new dlib::chip_details(r, d);
}

DLLEXPORT bool chip_details_angle(chip_details* chip, double* angle)
{
    *angle = chip->angle;   
    return true;
}

DLLEXPORT bool chip_details_cols(chip_details* chip, uint32_t* cols)
{
    *cols = chip->cols;   
    return true;
}

DLLEXPORT bool chip_details_rect(chip_details* chip, dlib::drectangle** rect)
{
    *rect = new dlib::drectangle(chip->rect);   
    return true;
}

DLLEXPORT bool chip_details_rows(chip_details* chip, uint32_t* rows)
{
    *rows = chip->rows;   
    return true;
}

DLLEXPORT void chip_details_delete(dlib::chip_details* obj)
{
    delete obj;
}

#pragma endregion chip_details

#pragma region chip_dims

DLLEXPORT chip_dims* chip_dims_new(unsigned int rows, unsigned int cols)
{
    return new dlib::chip_dims(rows, cols);
}

DLLEXPORT bool chip_dims_get_cols(chip_dims* chip, uint32_t* cols)
{
    *cols = chip->cols;   
    return true;
}

DLLEXPORT void chip_dims_set_cols(chip_dims* chip, uint32_t cols)
{
    chip->cols = cols;
}

DLLEXPORT bool chip_dims_get_rows(chip_dims* chip, uint32_t* rows)
{
    *rows = chip->rows;   
    return true;
}

DLLEXPORT void chip_dims_set_rows(chip_dims* chip, uint32_t rows)
{
    chip->rows = rows;
}

DLLEXPORT void chip_dims_delete(dlib::chip_dims* obj)
{
    delete obj;
}

#pragma endregion chip_dims

#pragma region get_face_chip_details

DLLEXPORT int get_face_chip_details(std::vector<full_object_detection*>* dets, const unsigned int size, const double padding, std::vector<chip_details*>* rets)
{
    int err = ERR_OK;
    
    std::vector<full_object_detection> tmpDets;
    for (int index = 0 ; index < dets->size(); index++)
        tmpDets.push_back(*(*dets)[index]);

    std::vector<chip_details> ret = dlib::get_face_chip_details(tmpDets, size, padding);

    for (int index = 0 ; index < ret.size(); index++)
    {
        chip_details* chip = new chip_details(ret[index]);
        rets->push_back(chip);
    }

    return err;
}

DLLEXPORT int get_face_chip_details2(full_object_detection* det, const unsigned int size, const double padding, chip_details** ret)
{
    int err = ERR_OK;

    chip_details r = dlib::get_face_chip_details(*det, size, padding);
    *ret = new chip_details(r);

    return err;
}

#pragma endregion get_face_chip_details

#pragma region extract_image_chips

DLLEXPORT int extract_image_chips(array2d_type img_type, void* in_img, std::vector<chip_details*>* chip_locations, array2d_type array_type, void* array)
{
    int err = ERR_OK;
    
    std::vector<chip_details> chips;
    for (int index = 0 ; index < chip_locations->size(); index++)
        chips.push_back(*(*chip_locations)[index]);

    switch(array_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT array2d<uint8_t>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT array2d<uint16_t>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT array2d<float>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT array2d<double>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT array2d<rgb_pixel>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_OUT array2d<hsi_pixel>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ELEMENT_OUT array2d<rgb_alpha_pixel>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_chip(array2d_type img_type, void* in_img, chip_details* chip_location, array2d_type array_type, void* out_chip)
{
    int err = ERR_OK;

    switch(array_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            #define ELEMENT_OUT hsi_pixel
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_chip2(array2d_type img_type, void* in_img, chip_details* chip_location, array2d_type array_type, interpolation_type type, void* out_chip)
{
    int err = ERR_OK;

    switch(array_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT            
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:        
            #define ELEMENT_OUT double
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            extract_image_chip2_template(err, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::HsiPixel:
            switch(type)
            {
                case interpolation_type::NearestNeighbor:
                    dlib::extract_image_chip(*((array2d<hsi_pixel>*)in_img), *chip_location, *((array2d<hsi_pixel>*)out_chip), interpolate_nearest_neighbor());
                    break;
                case interpolation_type::Bilinear:
                    dlib::extract_image_chip(*((array2d<hsi_pixel>*)in_img), *chip_location, *((array2d<hsi_pixel>*)out_chip), interpolate_bilinear());
                    break;
            }
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_chip_matrix(matrix_element_type img_type, void* in_img, chip_details* chip_location, matrix_element_type array_type, void* out_chip)
{
    int err = ERR_OK;

    switch(array_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_OUT uint8_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_OUT uint16_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_OUT uint32_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_OUT int8_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_OUT int16_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_OUT int32_t
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Float:
            #define ELEMENT_OUT float
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Double:
            #define ELEMENT_OUT double
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_OUT hsi_pixel
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_OUT rgb_alpha_pixel
            extract_image_chip_matrix_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        default:
            err = ERR_OUTPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_chip_matrix2(matrix_element_type img_type, void* in_img, chip_details* chip_location, matrix_element_type array_type, interpolation_type type, void* out_chip)
{
    int err = ERR_OK;

    switch(array_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_OUT uint8_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_OUT uint16_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_OUT uint32_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_OUT int8_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_OUT int16_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_OUT int32_t
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Float:
            #define ELEMENT_OUT float
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Double:
            #define ELEMENT_OUT double
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_OUT rgb_pixel
            extract_image_chip_matrix2_template(err, img_type, in_img, chip_location, type, out_chip);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::HsiPixel:
            switch(type)
            {
                case interpolation_type::NearestNeighbor:
                    dlib::extract_image_chip(*((matrix<hsi_pixel>*)in_img), *chip_location, *((matrix<hsi_pixel>*)out_chip), interpolate_nearest_neighbor());
                    break;
                case interpolation_type::Bilinear:
                    dlib::extract_image_chip(*((matrix<hsi_pixel>*)in_img), *chip_location, *((matrix<hsi_pixel>*)out_chip), interpolate_bilinear());
                    break;
            }
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_OUTPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion extract_image_chips

#pragma region jitter_image

DLLEXPORT int jitter_image(matrix_element_type in_type, void* in_img, dlib::rand* r, void** out_img)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:        
            #define ELEMENT_IN double
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            jitter_image_template(in_img, r, out_img);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:     
        default:
            err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion jitter_image

#endif