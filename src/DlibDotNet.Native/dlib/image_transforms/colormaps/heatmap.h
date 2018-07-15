#ifndef _CPP_COLORMAPS_HEATMAP_H_
#define _CPP_COLORMAPS_HEATMAP_H_

#include "colormap.h"

using namespace dlib;
using namespace std;

DLLEXPORT int op_heatmap_apply(array2d_type type, void* img, int r, int c, rgb_pixel* ret)
{
    int err = ERR_OK;
    #define OP op_heatmap
    colormaps_apply_array2d_template(type, img, r, c, ret, err);
    #undef OP
    return err;
}

DLLEXPORT bool op_heatmap_max_val(array2d_type type, void* img, double* ret)
{
    int err = ERR_OK;
    #define OP op_heatmap
    colormaps_max_val_array2d_template(type, img, ret, err);
    #undef OP
    return err == ERR_OK;
}

DLLEXPORT bool op_heatmap_min_val(array2d_type type, void* img, double* ret)
{
    int err = ERR_OK;
    #define OP op_heatmap
    colormaps_min_val_array2d_template(type, img, ret, err);
    #undef OP
    return err == ERR_OK;
}

DLLEXPORT bool op_heatmap_nc(array2d_type type, void* img, int* ret)
{
    int err = ERR_OK;
    #define OP op_heatmap
    colormaps_nc_array2d_template(type, img, ret, err);
    #undef OP
    return err == ERR_OK;
}

DLLEXPORT bool op_heatmap_nc_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define OP op_heatmap
            #define ELEMENT_IN uint8_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::UInt16:
            #define OP op_heatmap
            #define ELEMENT_IN uint16_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::UInt32:
            #define OP op_heatmap
            #define ELEMENT_IN uint32_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int8:
            #define OP op_heatmap
            #define ELEMENT_IN int8_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int16:
            #define OP op_heatmap
            #define ELEMENT_IN int16_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int32:
            #define OP op_heatmap
            #define ELEMENT_IN int32_t
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Float:
            #define OP op_heatmap
            #define ELEMENT_IN float
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Double:
            #define OP op_heatmap
            #define ELEMENT_IN double
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::RgbPixel:
            #define OP op_heatmap
            #define ELEMENT_IN rgb_pixel
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::HsiPixel:
            #define OP op_heatmap
            #define ELEMENT_IN hsi_pixel
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define OP op_heatmap
            #define ELEMENT_IN rgb_alpha_pixel
            colormaps_nc_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err == ERR_OK;
}

DLLEXPORT bool op_heatmap_nr(array2d_type type, void* img, int* ret)
{
    int err = ERR_OK;
    #define OP op_heatmap
    colormaps_nr_array2d_template(type, img, ret, err);
    #undef OP
    return err == ERR_OK;
}

DLLEXPORT bool op_heatmap_nr_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define OP op_heatmap
            #define ELEMENT_IN uint8_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::UInt16:
            #define OP op_heatmap
            #define ELEMENT_IN uint16_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::UInt32:
            #define OP op_heatmap
            #define ELEMENT_IN uint32_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int8:
            #define OP op_heatmap
            #define ELEMENT_IN int8_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int16:
            #define OP op_heatmap
            #define ELEMENT_IN int16_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Int32:
            #define OP op_heatmap
            #define ELEMENT_IN int32_t
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Float:
            #define OP op_heatmap
            #define ELEMENT_IN float
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::Double:
            #define OP op_heatmap
            #define ELEMENT_IN double
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::RgbPixel:
            #define OP op_heatmap
            #define ELEMENT_IN rgb_pixel
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::HsiPixel:
            #define OP op_heatmap
            #define ELEMENT_IN hsi_pixel
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define OP op_heatmap
            #define ELEMENT_IN rgb_alpha_pixel
            colormaps_nr_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
            #undef OP
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err == ERR_OK;
}

DLLEXPORT int heatmap(array2d_type type, void* img, void** matrix)
{
    int err = ERR_OK;
    #define OP op_heatmap
    #define FUNCTION heatmap
    colormaps_function_array2d_template(type, img, matrix, err);
    #undef FUNCTION
    #undef OP
    return err;
}

DLLEXPORT int heatmap2(array2d_type type, void* img, double max_val, double min_val, void** matrix)
{
    int err = ERR_OK;
    #define OP op_heatmap
    #define FUNCTION heatmap
    colormaps_function2_array2d_template(type, img, matrix, max_val, min_val, err);
    #undef FUNCTION
    #undef OP
    return err;
}

DLLEXPORT int heatmap_matrix(const matrix_element_type type, void* img, const int templateRows, const int templateColumns, void** matrix)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint8_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::UInt16:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint16_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::UInt32:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint32_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int8:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int8_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int16:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int16_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int32:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int32_t
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Float:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN float
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Double:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN double
            colormaps_function_matrix_template(img, templateRows, templateColumns, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int heatmap2_matrix(const matrix_element_type type, void* img, const int templateRows, const int templateColumns, double max_val, double min_val, void** matrix)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint8_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::UInt16:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint16_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::UInt32:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN uint32_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int8:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int8_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int16:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int16_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Int32:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN int32_t
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Float:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN float
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::Double:
            #define OP op_heatmap
            #define FUNCTION heatmap
            #define ELEMENT_IN double
            colormaps_function2_matrix_template(img, templateRows, templateColumns, max_val, min_val, matrix);
            #undef ELEMENT_IN
            #undef FUNCTION
            #undef OP
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void colormap_heat(const double value, const double min_val, const double max_val, dlib::rgb_pixel* pixel)
{
    *pixel = dlib::colormap_heat(value, min_val, max_val);
}

#endif