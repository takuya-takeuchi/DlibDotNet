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

#define ELEMENT element
#undef ELEMENT

#define load_image_dataset_template(images, object_locations, filename) \
do {\
    std::vector<matrix<ELEMENT>> tmp_images;\
    std::vector<std::vector<mmod_rect>> tmp_locs;\
    dlib::load_image_dataset(tmp_images, tmp_locs, filename);\
    std::vector<matrix<ELEMENT>*>* ret_images = static_cast<std::vector<matrix<ELEMENT>*>*>(images);\
    for (int i = 0; i < tmp_images.size(); i++)\
    {\
        matrix<ELEMENT>& m = tmp_images[i];\
        ret_images->push_back(new matrix<ELEMENT>(m));\
    }\
    std::vector<std::vector<mmod_rect*>*>* ret_locs = static_cast<std::vector<std::vector<mmod_rect*>*>*>(object_locations);\
    for (int i = 0; i < tmp_locs.size(); i++)\
    {\
        std::vector<mmod_rect>& tmp_vec = tmp_locs[i];\
        std::vector<mmod_rect*>* vec = new std::vector<mmod_rect*>();\
        for (int j = 0; j < tmp_vec.size(); j++)\
        {\
            mmod_rect& m = tmp_vec[j];\
            vec->push_back(new mmod_rect(m));\
        }\
        ret_locs->push_back(vec);\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int load_image_dataset(
    matrix_element_type type,
    void* images,
    void* object_locations,
    const char* filename)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#endif