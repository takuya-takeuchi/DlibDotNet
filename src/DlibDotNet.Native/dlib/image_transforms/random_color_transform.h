#ifndef _CPP_RANDOM_COLOR_TRANSFORM_H_
#define _CPP_RANDOM_COLOR_TRANSFORM_H_

#include "../export.h"
#include <dlib/image_transforms/random_color_transform.h>
#include <dlib/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define apply_random_color_offset_matrix_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, rnd)\
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto& r = *(static_cast<dlib::rand*>(rnd));\
dlib::apply_random_color_offset(m, r);\

#define apply_random_color_offset_matrix_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, rnd)\
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, apply_random_color_offset_matrix_template_sub, error, matrix, rnd);\
} while (0)

#define disturb_colors_matrix_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, rnd, gamma_magnitude, color_magnitude)\
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto& r = *(static_cast<dlib::rand*>(rnd));\
dlib::disturb_colors(m, r, gamma_magnitude, color_magnitude);\

#define disturb_colors_matrix_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, rnd, gamma_magnitude, color_magnitude)\
do {\
    matrix_template_size_arg4_template(__TYPE__, __ROWS__, __COLUMNS__, disturb_colors_matrix_template_sub, error, matrix, rnd, gamma_magnitude, color_magnitude);\
} while (0)

#pragma endregion template

DLLEXPORT int apply_random_color_offset_matrix(matrix_element_type element_type,
                                               void* matrix,
                                               int templateRows,
                                               int templateColumns,
                                               dlib::rand* rnd)
{
    int err = ERR_OK;

    switch(element_type)
    {
        case matrix_element_type::UInt8:
            apply_random_color_offset_matrix_template(uint8_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::UInt16:
            apply_random_color_offset_matrix_template(uint16_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::UInt32:
            apply_random_color_offset_matrix_template(uint32_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::UInt64:
            apply_random_color_offset_matrix_template(uint64_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Int8:
            apply_random_color_offset_matrix_template(int8_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Int16:
            apply_random_color_offset_matrix_template(int16_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Int32:
            apply_random_color_offset_matrix_template(int32_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Int64:
            apply_random_color_offset_matrix_template(int64_t, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Float:
            apply_random_color_offset_matrix_template(float, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::Double:
            apply_random_color_offset_matrix_template(double, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::RgbPixel:
            apply_random_color_offset_matrix_template(rgb_pixel, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::HsiPixel:
            apply_random_color_offset_matrix_template(hsi_pixel, templateRows, templateColumns, err, matrix, rnd);
            break;
        case matrix_element_type::RgbAlphaPixel:
            apply_random_color_offset_matrix_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, rnd);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int disturb_colors_matrix(matrix_element_type element_type,
                                    void* matrix,
                                    int templateRows,
                                    int templateColumns,
                                    dlib::rand* rnd,
                                    const double gamma_magnitude,
                                    const double color_magnitude)
{
    int err = ERR_OK;

    switch(element_type)
    {
        case matrix_element_type::UInt8:
            disturb_colors_matrix_template(uint8_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::UInt16:
            disturb_colors_matrix_template(uint16_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::UInt32:
            disturb_colors_matrix_template(uint32_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::UInt64:
            disturb_colors_matrix_template(uint64_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Int8:
            disturb_colors_matrix_template(int8_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Int16:
            disturb_colors_matrix_template(int16_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Int32:
            disturb_colors_matrix_template(int32_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Int64:
            disturb_colors_matrix_template(int64_t, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Float:
            disturb_colors_matrix_template(float, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::Double:
            disturb_colors_matrix_template(double, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::RgbPixel:
            disturb_colors_matrix_template(rgb_pixel, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::HsiPixel:
            disturb_colors_matrix_template(hsi_pixel, templateRows, templateColumns, err, matrix, rnd, gamma_magnitude, color_magnitude);
            break;
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif