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

#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef ELEMENT_OUT
#undef ELEMENT_IN

#define load_image_dataset_template(images, object_locations, filename) \
do {\
    std::vector<matrix<ELEMENT_IN>> tmp_images;\
    std::vector<std::vector<ELEMENT_OUT>> tmp_locs;\
    dlib::load_image_dataset(tmp_images, tmp_locs, filename);\
    std::vector<matrix<ELEMENT_IN>*>* ret_images = static_cast<std::vector<matrix<ELEMENT_IN>*>*>(images);\
    for (int i = 0; i < tmp_images.size(); i++)\
    {\
        matrix<ELEMENT_IN>& m = tmp_images[i];\
        ret_images->push_back(new matrix<ELEMENT_IN>(m));\
    }\
    std::vector<std::vector<ELEMENT_OUT*>*>* ret_locs = static_cast<std::vector<std::vector<ELEMENT_OUT*>*>*>(object_locations);\
    for (int i = 0; i < tmp_locs.size(); i++)\
    {\
        std::vector<ELEMENT_OUT>& tmp_vec = tmp_locs[i];\
        std::vector<ELEMENT_OUT*>* vec = new std::vector<ELEMENT_OUT*>();\
        for (int j = 0; j < tmp_vec.size(); j++)\
        {\
            ELEMENT_OUT& m = tmp_vec[j];\
            vec->push_back(new ELEMENT_OUT(m));\
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

    #define ELEMENT_OUT dlib::mmod_rect

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:
            #define ELEMENT_IN double
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    #undef ELEMENT_OUT
    
    return err;
}

DLLEXPORT int load_image_dataset_rectangle(matrix_element_type type,
                                           void* images,
                                           void* object_locations,
                                           const char* filename)
{
    int err = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Float:
            #define ELEMENT_IN float
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::Double:
            #define ELEMENT_IN double
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            load_image_dataset_template(images, object_locations, filename);
            #undef ELEMENT_IN
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    #undef ELEMENT_OUT
    
    return err;
}

#endif