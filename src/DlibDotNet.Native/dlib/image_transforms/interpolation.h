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

#define PYRAMID_TYPE PYRAMID_TYPE
#define FUNCTION function
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef FUNCTION
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef PYRAMID_TYPE

#define add_image_left_right_flips_template(images, objects) \
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
    add_image_left_right_flips(tmp_images, tmp_objects);\
\
    for (int i = 0; i < in_images.size(); i++)\
        delete in_images[i];\
    in_images.clear();\
\
    for (int i = 0; i < in_objects.size(); i++)\
    {\
        std::vector<ELEMENT_OUT*>& tmp = *in_objects[i];\
        for (int j = 0; j < tmp.size(); j++)\
            delete tmp[j];\
        tmp.clear();\
        delete in_objects[i];\
    }\
    in_objects.clear();\
\
    for (int i = 0; i < tmp_images.size(); i++)\
        in_images.push_back(new dlib::matrix<ELEMENT_IN>(tmp_images[i]));\
\
    for (int i = 0; i < tmp_objects.size(); i++)\
    {\
        std::vector<ELEMENT_OUT>& tmp = tmp_objects[i];\
        auto vec = new std::vector<ELEMENT_OUT*>();\
        for (int j = 0; j < tmp.size(); j++)\
            vec->push_back(new ELEMENT_OUT(tmp[j]));\
        in_objects.push_back(vec);\
    }\
} while (0)

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
        case array2d_type::UInt32:\
            dlib::FUNCTION(*((array2d<uint32_t>*)img));\
            break;\
        case array2d_type::Int8:\
            dlib::FUNCTION(*((array2d<int8_t>*)img));\
            break;\
        case array2d_type::Int16:\
            dlib::FUNCTION(*((array2d<int16_t>*)img));\
            break;\
        case array2d_type::Int32:\
            dlib::FUNCTION(*((array2d<int32_t>*)img));\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int8:\
            dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int16:\
            dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int32:\
            dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img));\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1, interpolate_quadratic());\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
                    break;\
            }\
            break;\
        case array2d_type::Int32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor());\
                    break;\
                case interpolation_type::Bilinear:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear());\
                    break;\
                case interpolation_type::Quadratic:\
                    dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic());\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix) \
do { \
    ret = ERR_OK;\
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            {\
                dlib::matrix<uint8_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<uint8_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<uint8_t>(tmp);\
            }\
            break;\
        case matrix_element_type::UInt16:\
            {\
                dlib::matrix<uint16_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<uint16_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<uint16_t>(tmp);\
            }\
            break;\
        case matrix_element_type::UInt32:\
            {\
                dlib::matrix<uint32_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<uint32_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<uint32_t>(tmp);\
            }\
            break;\
        case matrix_element_type::Int8:\
            {\
                dlib::matrix<int8_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<int8_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<int8_t>(tmp);\
            }\
            break;\
        case matrix_element_type::Int16:\
            {\
                dlib::matrix<int16_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<int16_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<int16_t>(tmp);\
            }\
            break;\
        case matrix_element_type::Int32:\
            {\
                dlib::matrix<int32_t> tmp;\
                dlib::pyramid_up(*((dlib::matrix<int32_t>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<int32_t>(tmp);\
            }\
            break;\
        case matrix_element_type::Float:\
            {\
                dlib::matrix<float> tmp;\
                dlib::pyramid_up(*((dlib::matrix<float>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<float>(tmp);\
            }\
            break;\
        case matrix_element_type::Double:\
            {\
                dlib::matrix<double> tmp;\
                dlib::pyramid_up(*((dlib::matrix<double>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<double>(tmp);\
            }\
            break;\
        case matrix_element_type::RgbPixel:\
            {\
                dlib::matrix<rgb_pixel> tmp;\
                dlib::pyramid_up(*((dlib::matrix<rgb_pixel>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<rgb_pixel>(tmp);\
            }\
            break;\
        case matrix_element_type::HsiPixel:\
            {\
                dlib::matrix<hsi_pixel> tmp;\
                dlib::pyramid_up(*((dlib::matrix<hsi_pixel>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<hsi_pixel>(tmp);\
            }\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            {\
                dlib::matrix<rgb_alpha_pixel> tmp;\
                dlib::pyramid_up(*((dlib::matrix<rgb_alpha_pixel>*)img), tmp, pyramid);\
                *matrix = new dlib::matrix<rgb_alpha_pixel>(tmp);\
            }\
            break;\
        default:\
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define pyramid_up_matrix_template(ret, type, img, pyramid_down, pyramid_rate, matrix) \
do { \
    ret = ERR_OK;\
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                dlib::pyramid_down<1>& pyramid = *(static_cast<dlib::pyramid_down<1>*>(pyramid_down));\
                pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix);\
            }\
            break;\
        case 2:\
            {\
                dlib::pyramid_down<2>& pyramid = *(static_cast<dlib::pyramid_down<2>*>(pyramid_down));\
                pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix);\
            }\
            break;\
        case 3:\
            {\
                dlib::pyramid_down<3>& pyramid = *(static_cast<dlib::pyramid_down<3>*>(pyramid_down));\
                pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix);\
            }\
            break;\
        case 4:\
            {\
                dlib::pyramid_down<4>& pyramid = *(static_cast<dlib::pyramid_down<4>*>(pyramid_down));\
                pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix);\
            }\
            break;\
        case 6:\
            {\
                dlib::pyramid_down<6>& pyramid = *(static_cast<dlib::pyramid_down<6>*>(pyramid_down));\
                pyramid_up_matrix_template_sub(ret, type, img, pyramid, matrix);\
            }\
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
        case array2d_type::UInt32:\
            dlib::FUNCTION(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::Int8:\
            dlib::FUNCTION(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::Int16:\
            dlib::FUNCTION(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
            break;\
        case array2d_type::Int32:\
            dlib::FUNCTION(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), arg1);\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<uint32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::Int8:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<int8_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::Int16:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<int16_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
                    break;\
            }\
            break;\
        case array2d_type::Int32:\
            switch(type)\
            {\
                case interpolation_type::NearestNeighbor:\
                    transform_image_sub_template(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_nearest_neighbor(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Bilinear:\
                    transform_image_sub_template(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_bilinear(), point_mapping_type, mapping_obj);\
                    break;\
                case interpolation_type::Quadratic:\
                    transform_image_sub_template(*((array2d<int32_t>*)in_img), *((array2d<ELEMENT_OUT>*)out_img), interpolate_quadratic(), point_mapping_type, mapping_obj);\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            dlib::extract_image_chips(*((array2d<uint32_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::Int8:\
            dlib::extract_image_chips(*((array2d<int8_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::Int16:\
            dlib::extract_image_chips(*((array2d<int16_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
            break;\
        case array2d_type::Int32:\
            dlib::extract_image_chips(*((array2d<int32_t>*)in_img), chips, *((dlib::array<ELEMENT_OUT>*)array));\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
        case array2d_type::UInt32:\
            dlib::extract_image_chip(*((array2d<uint32_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::Int8:\
            dlib::extract_image_chip(*((array2d<int8_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::Int16:\
            dlib::extract_image_chip(*((array2d<int16_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
            break;\
        case array2d_type::Int32:\
            dlib::extract_image_chip(*((array2d<int32_t>*)in_img), *chip, *((array2d<ELEMENT_OUT>*)out_chip));\
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
            ret = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
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
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
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
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
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

#define resize_image_matrix_scale_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, scale) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
dlib::resize_image(scale, m);\

#define resize_image_matrix_scale_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, scale) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, resize_image_matrix_scale_template_sub, error, matrix, scale);\
} while (0)

#define upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects) \
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
            upsample_image_dataset<pyramid_down<1>>(tmp_images, tmp_objects);\
            break;\
        case 2:\
            upsample_image_dataset<pyramid_down<2>>(tmp_images, tmp_objects);\
            break;\
        case 3:\
            upsample_image_dataset<pyramid_down<3>>(tmp_images, tmp_objects);\
            break;\
        case 4:\
            upsample_image_dataset<pyramid_down<4>>(tmp_images, tmp_objects);\
            break;\
        case 6:\
            upsample_image_dataset<pyramid_down<6>>(tmp_images, tmp_objects);\
            break;\
        default:\
            ret = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
\
    for (int i = 0; i < in_images.size(); i++)\
        delete in_images[i];\
    in_images.clear();\
\
    for (int i = 0; i < in_objects.size(); i++)\
    {\
        std::vector<ELEMENT_OUT*>& tmp = *in_objects[i];\
        for (int j = 0; j < tmp.size(); j++)\
            delete tmp[j];\
        tmp.clear();\
        delete in_objects[i];\
    }\
    in_objects.clear();\
\
    for (int i = 0; i < tmp_images.size(); i++)\
        in_images.push_back(new dlib::matrix<ELEMENT_IN>(tmp_images[i]));\
\
    for (int i = 0; i < tmp_objects.size(); i++)\
    {\
        std::vector<ELEMENT_OUT>& tmp = tmp_objects[i];\
        auto vec = new std::vector<ELEMENT_OUT*>();\
        for (int j = 0; j < tmp.size(); j++)\
            vec->push_back(new ELEMENT_OUT(tmp[j]));\
        in_objects.push_back(vec);\
    }\
} while (0)

#define pyramid_up_pyramid_matrix_template(pyramid_rate, image) \
do { \
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                const PYRAMID_TYPE<1> p;\
                dlib::matrix<ELEMENT_IN>& m = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 2:\
            {\
                const PYRAMID_TYPE<2> p;\
                dlib::matrix<ELEMENT_IN>& m = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 3:\
            {\
                const PYRAMID_TYPE<3> p;\
                dlib::matrix<ELEMENT_IN>& m = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 4:\
            {\
                const PYRAMID_TYPE<4> p;\
                dlib::matrix<ELEMENT_IN>& m = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 6:\
            {\
                const PYRAMID_TYPE<6> p;\
                dlib::matrix<ELEMENT_IN>& m = *(static_cast<dlib::matrix<ELEMENT_IN>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

#pragma region add_image_left_right_flips

DLLEXPORT int add_image_left_right_flips_rectangle(matrix_element_type element_type, void* images, void* objects)
{
    int err = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle

    switch(element_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:
            #define ELEMENT_IN double
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            add_image_left_right_flips_template(images, objects);
            #undef ELEMENT_IN
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    #undef ELEMENT_OUT

    return err;
}

#pragma endregion add_image_left_right_flips

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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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

DLLEXPORT int pyramid_up_matrix2(matrix_element_type type, void* img, void* pyramid_down, unsigned int pyramid_rate, void** matrix)
{
    int err = ERR_OK;
    pyramid_up_matrix_template(err, type, img, pyramid_down, pyramid_rate, matrix);
    return err;
}

DLLEXPORT int pyramid_up_pyramid_matrix(const pyramid_type pyramid_type, 
                                        const unsigned int pyramid_rate,
                                        matrix_element_type element_type,
                                        void* image)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(element_type)
                {
                    case matrix_element_type::UInt8:
                        #define ELEMENT_IN uint8_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);                                
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::UInt16:
                        #define ELEMENT_IN uint16_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::UInt32:
                        #define ELEMENT_IN uint32_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int8:
                        #define ELEMENT_IN int8_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int16:
                        #define ELEMENT_IN int16_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int32:
                        #define ELEMENT_IN int32_t
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Float:
                        #define ELEMENT_IN float
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Double:
                        #define ELEMENT_IN double
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::RgbPixel:
                        #define ELEMENT_IN rgb_pixel
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::HsiPixel:
                        #define ELEMENT_IN hsi_pixel
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::RgbAlphaPixel:
                        #define ELEMENT_IN rgb_alpha_pixel
                        pyramid_up_pyramid_matrix_template(pyramid_rate, image);   
                        #undef ELEMENT_IN
                        break;
                    default:
                        err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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

#pragma region resize_image

DLLEXPORT int resize_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int err = ERR_OK;

    if (in_type == array2d_type::HsiPixel || in_type == array2d_type::RgbAlphaPixel)
        return ERR_ARRAY2D_TYPE_NOT_SUPPORT;

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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            interpolation_inout_template(err, in_type, in_img, out_img);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            interpolation_inout3_template(err, in_type, in_img, out_img, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            dlib::resize_image(size_scale, *((array2d<uint32_t>*)img));
            break;
        case array2d_type::Int8:
            dlib::resize_image(size_scale, *((array2d<int8_t>*)img));
            break;
        case array2d_type::Int16:
            dlib::resize_image(size_scale, *((array2d<int16_t>*)img));
            break;
        case array2d_type::Int32:
            dlib::resize_image(size_scale, *((array2d<int32_t>*)img));
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int resize_image_matrix_scale(matrix_element_type type, void* matrix, int templateRows, int templateColumns, double scale)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            resize_image_matrix_scale_template(uint8_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::UInt16:
            resize_image_matrix_scale_template(uint16_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::UInt32:
            resize_image_matrix_scale_template(uint32_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::Int8:
            resize_image_matrix_scale_template(int8_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::Int16:
            resize_image_matrix_scale_template(int16_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::Int32:
            resize_image_matrix_scale_template(int32_t, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::Float:
            resize_image_matrix_scale_template(float, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::Double:
            resize_image_matrix_scale_template(double, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::RgbPixel:
            resize_image_matrix_scale_template(rgb_pixel, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::HsiPixel:
            resize_image_matrix_scale_template(hsi_pixel, templateRows, templateColumns, err, matrix, scale);
            break;
        case matrix_element_type::RgbAlphaPixel:
            resize_image_matrix_scale_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, scale);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            rotate_image_template(err, in_type, in_img, out_img, angle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            interpolation_inout2_template(err, in_type, in_img, out_img, angle, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            transform_image_template(err, in_type, in_img, out_img, mapping_type, mapping_obj, type);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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

DLLEXPORT chip_details* chip_details_new4(drectangle* rect, const unsigned long size)
{
    drectangle& r = *rect;
    return new dlib::chip_details(r, size);
}

DLLEXPORT chip_details* chip_details_new5(drectangle* rect, const unsigned long size, const double angle)
{
    drectangle& r = *rect;
    return new dlib::chip_details(r, size, angle);
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

#pragma region flip_rect_left_right

DLLEXPORT dlib::rectangle* flip_rect_left_right(dlib::rectangle* rect, dlib::rectangle* window)
{
    dlib::rectangle& r = *static_cast<dlib::rectangle*>(rect);
    dlib::rectangle& w = *static_cast<dlib::rectangle*>(window);

    auto ret = impl::flip_rect_left_right(r, w);
    return new dlib::rectangle(ret);
}

#pragma endregion flip_rect_left_right

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
        case array2d_type::UInt32:
            #define ELEMENT_OUT array2d<uint32_t>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT array2d<int8_t>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT array2d<int16_t>
            extract_image_chips_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT array2d<int32_t>
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_chips_matrix(matrix_element_type img_type, void* in_img, std::vector<chip_details*>* chip_locations, matrix_element_type array_type, void* array)
{
    int err = ERR_OK;
    
    std::vector<chip_details> chips;
    for (int index = 0 ; index < chip_locations->size(); index++)
        chips.push_back(*(*chip_locations)[index]);

    switch(array_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_OUT dlib::matrix<uint8_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_OUT dlib::matrix<uint16_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_OUT dlib::matrix<uint32_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_OUT dlib::matrix<int8_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_OUT dlib::matrix<int16_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_OUT dlib::matrix<int32_t>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Float:
            #define ELEMENT_OUT dlib::matrix<float>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::Double:        
            #define ELEMENT_OUT dlib::matrix<double>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_OUT dlib::matrix<rgb_pixel>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_OUT dlib::matrix<hsi_pixel>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_OUT dlib::matrix<rgb_alpha_pixel>
            extract_image_chips_matrix_template(err, img_type, in_img, chips, array);
            #undef ELEMENT_OUT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            extract_image_chip_template(err, img_type, in_img, chip_location, out_chip);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion jitter_image

#pragma region upsample_image_dataset

DLLEXPORT int upsample_image_dataset_pyramid_down(const unsigned int pyramid_rate, matrix_element_type element_type, void* images, void* objects)
{
    int ret = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle

    switch(element_type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:
            #define ELEMENT_IN double
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            upsample_image_dataset_pyramid_down_template(ret, pyramid_rate, images, objects);
            #undef ELEMENT_IN
            break;
        default:
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    #undef ELEMENT_OUT

    return ret;
}

#pragma endregion upsample_image_dataset

#pragma region extract_image_4points

#define extract_image_4points_template(__TYPE__, error, image, points, width, height, output)\
do {\
    auto& m = *(static_cast<dlib::array2d<__TYPE__>*>(image));\
    std::array<dlib::dpoint, 4> ps;\
    for (auto index = 0; index < 4; index++)\
        ps[index] = *points[index];\
    auto tmpout = new dlib::array2d<__TYPE__>(height, width);\
    auto& o = *(static_cast<dlib::array2d<__TYPE__>*>(tmpout));\
    dlib::extract_image_4points(m, o, ps);\
    *output = tmpout;\
} while (0)

#define extract_image_4points_matrix_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, points, width, height, output)\
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
std::array<dlib::dpoint, 4> ps;\
for (auto index = 0; index < 4; index++)\
    ps[index] = *points[index];\
auto tmpout = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(height, width);\
auto& o = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(tmpout));\
dlib::extract_image_4points(m, o, ps);\
*output = tmpout;\

#define extract_image_4points_matrix_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, points, width, height, output)\
do {\
    matrix_template_size_arg5_template(__TYPE__, __ROWS__, __COLUMNS__, extract_image_4points_matrix_template_sub, error, matrix, points, width, height, output);\
} while (0)

DLLEXPORT int extract_image_4points(array2d_type type,
                                    void* image,
                                    dlib::dpoint** points,
                                    int width,
                                    int height,
                                    void** output)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            extract_image_4points_template(uint8_t, err, image, points, width, height, output);
            break;
        case array2d_type::UInt16:
            extract_image_4points_template(uint16_t, err, image, points, width, height, output);
            break;
        case array2d_type::UInt32:
            extract_image_4points_template(uint32_t, err, image, points, width, height, output);
            break;
        case array2d_type::Int8:
            extract_image_4points_template(int8_t, err, image, points, width, height, output);
            break;
        case array2d_type::Int16:
            extract_image_4points_template(int16_t, err, image, points, width, height, output);
            break;
        case array2d_type::Int32:
            extract_image_4points_template(int32_t, err, image, points, width, height, output);
            break;
        case array2d_type::Float:
            extract_image_4points_template(float, err, image, points, width, height, output);
            break;
        case array2d_type::Double:
            extract_image_4points_template(double, err, image, points, width, height, output);
            break;
        case array2d_type::RgbPixel:
            extract_image_4points_template(rgb_pixel, err, image, points, width, height, output);
            break;
        case array2d_type::HsiPixel:
            extract_image_4points_template(hsi_pixel, err, image, points, width, height, output);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int extract_image_4points_matrix(matrix_element_type element_type,
                                           void* matrix,
                                           int templateRows,
                                           int templateColumns,
                                           dlib::dpoint** points,
                                           int width,
                                           int height,
                                           void** output)
{
    int err = ERR_OK;

    switch(element_type)
    {
        case matrix_element_type::UInt8:
            extract_image_4points_matrix_template(uint8_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::UInt16:
            extract_image_4points_matrix_template(uint16_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::UInt32:
            extract_image_4points_matrix_template(uint32_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::UInt64:
            extract_image_4points_matrix_template(uint64_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Int8:
            extract_image_4points_matrix_template(int8_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Int16:
            extract_image_4points_matrix_template(int16_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Int32:
            extract_image_4points_matrix_template(int32_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Int64:
            extract_image_4points_matrix_template(int64_t, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Float:
            extract_image_4points_matrix_template(float, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::Double:
            extract_image_4points_matrix_template(double, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::RgbPixel:
            extract_image_4points_matrix_template(rgb_pixel, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::HsiPixel:
            extract_image_4points_matrix_template(hsi_pixel, templateRows, templateColumns, err, matrix, points, width, height, output);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion extract_image_4points

#endif