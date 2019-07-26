#ifndef _CPP_SEGMENT_IMAGE_H_
#define _CPP_SEGMENT_IMAGE_H_

#include "../export.h"
#include <dlib/image_transforms/segment_image.h>
#include <dlib/image_processing/generic_image.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define find_candidate_object_locations_template_sub(__ARRAY_TYPE__, in_img, rects, __MATRIX_TYPE__, kvals, min_size, max_merging_iterations) \
do {\
    dlib::array2d<__ARRAY_TYPE__>& array = *static_cast<dlib::array2d<__ARRAY_TYPE__>*>(in_img);\
    std::vector<dlib::rectangle> tmpRects;\
    dlib::matrix<__MATRIX_TYPE__>& in_matrix = *static_cast<dlib::matrix<__MATRIX_TYPE__>*>(kvals);\
    dlib::find_candidate_object_locations(array, tmpRects, in_matrix, min_size, max_merging_iterations);\
\
    if(tmpRects.size() > 0)\
    {\
        for (int i = 0; i < tmpRects.size(); i++)\
            rects->push_back(new rectangle(tmpRects[i]));\
    }\
} while (0)

#define find_candidate_object_locations_template(__TYPE__, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err) \
do {\
    switch(matrix_element_type)\
    {\
        case ::matrix_element_type::UInt8:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, uint8_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::UInt16:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, uint16_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::UInt32:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, uint32_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::Int8:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, int8_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::Int16:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, int16_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::Int32:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, int32_t, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::Float:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, float, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::Double:\
            find_candidate_object_locations_template_sub(__TYPE__, in_img, rects, double, kvals, min_size, max_merging_iterations);\
            break;\
        case ::matrix_element_type::RgbPixel:\
        case ::matrix_element_type::BgrPixel:\
        case ::matrix_element_type::RgbAlphaPixel:\
        case ::matrix_element_type::HsiPixel:\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion template

// NOTE
// segment_image function requires the following condition.
// - out_image_type must contain an unsigned integer pixel type.
// - is_same_object(in_img, out_img) == false
// Perhaps, find_candidate_object_locations requires same condition?
DLLEXPORT int find_candidate_object_locations(array2d_type type,
                                              void* in_img,
                                              std::vector<rectangle*> *rects,
                                              ::matrix_element_type matrix_element_type,
                                              void* kvals,
                                              const unsigned int min_size,
                                              const unsigned int max_merging_iterations)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            find_candidate_object_locations_template(uint8_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::UInt16:
            find_candidate_object_locations_template(uint16_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::UInt32:
            find_candidate_object_locations_template(uint32_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::Int8:
            find_candidate_object_locations_template(int8_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::Int16:
            find_candidate_object_locations_template(int16_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::Int32:
            find_candidate_object_locations_template(int32_t, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::RgbPixel:
            find_candidate_object_locations_template(rgb_pixel, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::BgrPixel:
            find_candidate_object_locations_template(bgr_pixel, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::RgbAlphaPixel:
            find_candidate_object_locations_template(rgb_alpha_pixel, in_img, rects, matrix_element_type, kvals, min_size, max_merging_iterations, err);
            break;
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::HsiPixel:
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif