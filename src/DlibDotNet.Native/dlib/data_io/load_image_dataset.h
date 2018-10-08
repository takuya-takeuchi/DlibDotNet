#ifndef _CPP_LOAD_IMAGE_DATASET_H_
#define _CPP_LOAD_IMAGE_DATASET_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/data_io/load_image_dataset.h>
#include <dlib/image_processing/full_object_detection.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

#pragma region template

#define load_image_dataset_template(__TYPE_IN__, __TYPE_OUT__, images, object_locations, filename) \
do {\
    std::vector<matrix<__TYPE_IN__>> tmp_images;\
    std::vector<std::vector<__TYPE_OUT__>> tmp_locs;\
    dlib::load_image_dataset(tmp_images, tmp_locs, filename);\
    std::vector<matrix<__TYPE_IN__>*>* ret_images = static_cast<std::vector<matrix<__TYPE_IN__>*>*>(images);\
    for (int i = 0; i < tmp_images.size(); i++)\
    {\
        matrix<__TYPE_IN__>& m = tmp_images[i];\
        ret_images->push_back(new matrix<__TYPE_IN__>(m));\
    }\
    std::vector<std::vector<__TYPE_OUT__*>*>* ret_locs = static_cast<std::vector<std::vector<__TYPE_OUT__*>*>*>(object_locations);\
    for (int i = 0; i < tmp_locs.size(); i++)\
    {\
        std::vector<__TYPE_OUT__>& tmp_vec = tmp_locs[i];\
        std::vector<__TYPE_OUT__*>* vec = new std::vector<__TYPE_OUT__*>();\
        for (int j = 0; j < tmp_vec.size(); j++)\
        {\
            __TYPE_OUT__& m = tmp_vec[j];\
            vec->push_back(new __TYPE_OUT__(m));\
        }\
        ret_locs->push_back(vec);\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int load_image_dataset_mmod_rect(matrix_element_type type,
                                           void* images,
                                           void* object_locations,
                                           const char* filename)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            load_image_dataset_template(uint8_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::UInt16:
            load_image_dataset_template(uint16_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::UInt32:
            load_image_dataset_template(uint32_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::Int8:
            load_image_dataset_template(int8_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::Int16:
            load_image_dataset_template(int16_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::Int32:
            load_image_dataset_template(int32_t, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::Float:
            load_image_dataset_template(float, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::Double:
            load_image_dataset_template(double, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::RgbPixel:
            load_image_dataset_template(rgb_pixel, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::HsiPixel:
            load_image_dataset_template(hsi_pixel, dlib::mmod_rect, images, object_locations, filename);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int load_image_dataset_rectangle(matrix_element_type type,
                                           void* images,
                                           void* object_locations,
                                           const char* filename)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            load_image_dataset_template(uint8_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::UInt16:
            load_image_dataset_template(uint16_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::UInt32:
            load_image_dataset_template(uint32_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::Int8:
            load_image_dataset_template(int8_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::Int16:
            load_image_dataset_template(int16_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::Int32:
            load_image_dataset_template(int32_t, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::Float:
            load_image_dataset_template(float, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::Double:
            load_image_dataset_template(double, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::RgbPixel:
            load_image_dataset_template(rgb_pixel, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::HsiPixel:
            load_image_dataset_template(hsi_pixel, dlib::rectangle, images, object_locations, filename);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#endif