#ifndef _CPP_FHOG_H_
#define _CPP_FHOG_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_processing/full_object_detection_abstract.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ARRAY2D_ELEMENT element
#undef ARRAY2D_ELEMENT

#define extract_fhog_features_template(ret, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding)\
do {\
    ret = ERR_OK;\
    switch(hog_type)\
    {\
        case matrix_element_type::Float:\
            dlib::extract_fhog_features(*((array2d<ARRAY2D_ELEMENT>*)img), *((array2d<matrix<float, 31, 1>>*)hog), cell_size, filter_rows_padding, filter_cols_padding);\
            break;\
        case matrix_element_type::Double:\
            dlib::extract_fhog_features(*((array2d<ARRAY2D_ELEMENT>*)img), *((array2d<matrix<double, 31, 1>>*)hog), cell_size, filter_rows_padding, filter_cols_padding);\
            break;\
        default:\
            ret = ERR_OUTPUT_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

DLLEXPORT int extract_fhog_features(
    array2d_type img_type,
    void* img,
    matrix_element_type hog_type,
    void* hog,
    int cell_size,
    int filter_rows_padding,
    int filter_cols_padding)
{
    int err = ERR_OK;
    switch(img_type)
    {
        case array2d_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Float:
            #define ARRAY2D_ELEMENT float
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Double:
            #define ARRAY2D_ELEMENT double
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ARRAY2D_ELEMENT rgb_pixel
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ARRAY2D_ELEMENT hsi_pixel
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ARRAY2D_ELEMENT rgb_alpha_pixel
            extract_fhog_features_template(err, img, hog_type, hog, cell_size, filter_rows_padding, filter_cols_padding);
            #undef ARRAY2D_ELEMENT
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int draw_fhog(
    const matrix_element_type hog_type,
    const void* hog,
    const int cell_draw_size,
    const float min_response_threshold,
    void** out_matrix)
{
    int ret = ERR_OK;
    switch(hog_type)
    {
        case matrix_element_type::Float:
            {
                auto mat = dlib::draw_fhog(*((array2d<matrix<float, 31, 1>>*)hog), cell_draw_size, min_response_threshold);
                *out_matrix = new dlib::matrix<uint8_t>(mat);
            }
            break;
        case matrix_element_type::Double:
            {
                auto mat = dlib::draw_fhog(*((array2d<matrix<double, 31, 1>>*)hog), cell_draw_size, min_response_threshold);
                *out_matrix = new dlib::matrix<uint8_t>(mat);
            }
            break;
        default:\
            ret = ERR_OUTPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return ret;
}

#pragma region fhog_matrix

DLLEXPORT void* array2d_fhog_matrix_new(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::Float:
            return new dlib::array2d<matrix<float, 31, 1>>();
        case matrix_element_type::Double:
            return new dlib::array2d<matrix<double, 31, 1>>();
        default:
            return nullptr;
    }
}

DLLEXPORT void* array2d_fhog_matrix_new1(matrix_element_type type, int rows, int cols)
{
    switch(type)
    {
        case matrix_element_type::Float:
            return new dlib::array2d<matrix<float, 31, 1>>(rows, cols);
        case matrix_element_type::Double:
            return new dlib::array2d<matrix<double, 31, 1>>(rows, cols);
        default:
            return nullptr;
    }
}

DLLEXPORT bool array2d_fhog_matrix_nc(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float, 31, 1>>*)array)->nc();
            return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double, 31, 1>>*)array)->nc();
            return true;
        default:
            return false;
    }
}

DLLEXPORT bool array2d_fhog_matrix_nr(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float, 31, 1>>*)array)->nr();
            return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double, 31, 1>>*)array)->nr();
            return true;
        default:
            return false;
    }
}

DLLEXPORT bool array2d_fhog_matrix_size(matrix_element_type type, void* array, int* ret)
{
    switch(type)
    {
        case matrix_element_type::Float:
            *ret = ((dlib::array2d<matrix<float, 31, 1>>*)array)->size();
            return true;
        case matrix_element_type::Double:
            *ret = ((dlib::array2d<matrix<double, 31, 1>>*)array)->size();
            return true;
        default:
            return false;
    }
}

DLLEXPORT void array2d_fhog_matrix_delete(matrix_element_type type, void* array)
{
    switch(type)
    {
        case matrix_element_type::Float:
            delete ((dlib::array2d<matrix<float, 31, 1>>*)array);
            break;
        case matrix_element_type::Double:
            delete ((dlib::array2d<matrix<double, 31, 1>>*)array);
            break;
        default:
            break;
    }
}

DLLEXPORT int array2d_fhog_matrix_get_rect2(matrix_element_type type, void* img, rectangle** rect)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::Float:
            *rect = new dlib::rectangle(get_rect(*((array2d<matrix<float, 31, 1>>*)img)));
			break;
        case matrix_element_type::Double:
            *rect = new dlib::rectangle(get_rect(*((array2d<matrix<double, 31, 1>>*)img)));
			break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma endregion fhog_matrix

#endif