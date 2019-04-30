#ifndef _CPP_LOAD_IMAGE_DATASET_H_
#define _CPP_LOAD_IMAGE_DATASET_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/data_io/load_image_dataset.h>
#include <dlib/image_processing/full_object_detection.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define load_image_dataset_array_template_sub(__TYPE__, error, type, __TYPE_OUT__, ...) \
do {\
    dlib::array<array2d<__TYPE__>> tmp_images;\
    std::vector<std::vector<__TYPE_OUT__>> tmp_locs;\
\
    dlib::load_image_dataset(tmp_images, tmp_locs, filename);\
\
    array_copy2(__TYPE__, tmp_images, images);\
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

#define load_image_dataset_array_full_object_detection_template(__TYPE__, error, type, ...) \
load_image_dataset_array_template_sub(__TYPE__, error, type, dlib::full_object_detection, __VA_ARGS__)

#define load_image_dataset_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __TYPE_OUT__, ...) \
do {\
    std::vector<matrix<__TYPE__>> tmp_images;\
    std::vector<std::vector<__TYPE_OUT__>> tmp_locs;\
    dlib::load_image_dataset(tmp_images, tmp_locs, filename);\
    std::vector<matrix<__TYPE__>*>* ret_images = static_cast<std::vector<matrix<__TYPE__>*>*>(images);\
    for (int i = 0; i < tmp_images.size(); i++)\
    {\
        matrix<__TYPE__>& m = tmp_images[i];\
        ret_images->push_back(new matrix<__TYPE__>(m));\
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

#define load_image_dataset_mmod_rect_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
load_image_dataset_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::mmod_rect, __VA_ARGS__)

#define load_image_dataset_rectangle_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
load_image_dataset_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, dlib::rectangle, __VA_ARGS__)

#pragma endregion template

DLLEXPORT int load_image_dataset_array_full_object_detection(array2d_type type,
                                                             void* images,
                                                             void* object_locations,
                                                             const char* filename)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              load_image_dataset_array_full_object_detection_template,
                              images,
                              object_locations,
                              filename);

    return error;
}

DLLEXPORT int load_image_dataset_mmod_rect(matrix_element_type type,
                                           void* images,
                                           void* object_locations,
                                           const char* filename)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             load_image_dataset_mmod_rect_template,
                             0,
                             0,
                             images,
                             object_locations,
                             filename);

    return error;
}

DLLEXPORT int load_image_dataset_rectangle(matrix_element_type type,
                                           void* images,
                                           void* object_locations,
                                           const char* filename)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             load_image_dataset_rectangle_template,
                             0,
                             0,
                             images,
                             object_locations,
                             filename);

    return error;
}

#endif