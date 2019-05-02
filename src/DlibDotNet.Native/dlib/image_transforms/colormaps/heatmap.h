#ifndef _CPP_COLORMAPS_HEATMAP_H_
#define _CPP_COLORMAPS_HEATMAP_H_

#include "colormap.h"

using namespace dlib;
using namespace std;

DLLEXPORT int op_heatmap_apply(array2d_type type, void* img, int r, int c, rgb_pixel* ret)
{
    int error = ERR_OK;
    #define OP op_heatmap
    array2d_template(type,
                     error,
                     colormaps_apply_array2d_template,
                     img,
                     r,
                     c,
                     ret);
    #undef OP
    return error;
}

DLLEXPORT bool op_heatmap_max_val(array2d_type type, void* img, double* ret)
{
    int error = ERR_OK;
    #define OP op_heatmap
    array2d_template(type,
                     error,
                     colormaps_max_val_array2d_template,
                     img,
                     ret);
    #undef OP
    return error == ERR_OK;
}

DLLEXPORT bool op_heatmap_min_val(array2d_type type, void* img, double* ret)
{
    int error = ERR_OK;
    #define OP op_heatmap
    array2d_template(type,
                     error,
                     colormaps_min_val_array2d_template,
                     img,
                     ret);
    #undef OP
    return error == ERR_OK;
}

DLLEXPORT bool op_heatmap_nc(array2d_type type, void* img, int* ret)
{
    int error = ERR_OK;

    #define OP op_heatmap
    array2d_template(type,
                     error,
                     colormaps_nc_array2d_template,
                     img,
                     ret);
    #undef OP

    return error == ERR_OK;
}

DLLEXPORT bool op_heatmap_nc_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{    
    int error = ERR_OK;

    #define OP op_heatmap
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    colormaps_nc_matrix_template,
                    templateRows,
                    templateColumns,
                    img,
                    ret);
    #undef OP

    return error != ERR_OK;
}

DLLEXPORT bool op_heatmap_nr(array2d_type type, void* img, int* ret)
{
    int error = ERR_OK;

    #define OP op_heatmap
    array2d_template(type,
                     error,
                     colormaps_nr_array2d_template,
                     img,
                     ret);
    #undef OP

    return error == ERR_OK;
}

DLLEXPORT bool op_heatmap_nr_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    #define OP op_heatmap
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    colormaps_nr_matrix_template,
                    templateRows,
                    templateColumns,
                    img,
                    ret);
    #undef OP

    return error != ERR_OK;
}

DLLEXPORT int heatmap(array2d_type type, void* img, void** matrix)
{
    int error = ERR_OK;
    #define OP op_heatmap
    #define FUNCTION heatmap
    array2d_numeric_template(type,
                             error,
                             colormaps_function_array2d_template,
                             img,
                             matrix);
    #undef FUNCTION
    #undef OP
    return error;
}

DLLEXPORT int heatmap2(array2d_type type, void* img, double max_val, double min_val, void** matrix)
{
    int error = ERR_OK;
    #define OP op_heatmap
    #define FUNCTION heatmap
    array2d_numeric_template(type,
                             error,
                             colormaps_function2_array2d_template,
                             img,
                             matrix,
                             max_val,
                             min_val);
    #undef FUNCTION
    #undef OP
    return error;
}

DLLEXPORT int heatmap_matrix(const matrix_element_type type, void* img, const int templateRows, const int templateColumns, void** matrix)
{
    int error = ERR_OK;

    #define OP op_heatmap
    #define FUNCTION heatmap
    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            colormaps_function_matrix_template,
                            templateRows,
                            templateColumns,
                            img,
                            matrix);
    #undef FUNCTION
    #undef OP

    return error != ERR_OK;
}

DLLEXPORT int heatmap2_matrix(const matrix_element_type type, void* img, const int templateRows, const int templateColumns, double max_val, double min_val, void** matrix)
{
    int error = ERR_OK;

    #define OP op_heatmap
    #define FUNCTION heatmap
    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            colormaps_function2_matrix_template,
                            templateRows,
                            templateColumns,
                            img,
                            max_val,
                            min_val,
                            matrix);
    #undef FUNCTION
    #undef OP

    return error != ERR_OK;
}

DLLEXPORT void colormap_heat(const double value, const double min_val, const double max_val, dlib::rgb_pixel* pixel)
{
    *pixel = dlib::colormap_heat(value, min_val, max_val);
}

#endif