#ifndef _CPP_RANDOM_COLOR_TRANSFORM_H_
#define _CPP_RANDOM_COLOR_TRANSFORM_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/image_transforms/random_color_transform.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define apply_random_color_offset_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto& r = *(static_cast<dlib::rand*>(rnd));\
dlib::apply_random_color_offset(m, r);\

#define disturb_colors_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
auto& r = *(static_cast<dlib::rand*>(rnd));\
dlib::disturb_colors(m, r, gamma_magnitude, color_magnitude);\

#pragma endregion template

DLLEXPORT int apply_random_color_offset_matrix(matrix_element_type type,
                                               void* matrix,
                                               int templateRows,
                                               int templateColumns,
                                               dlib::rand* rnd)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    apply_random_color_offset_matrix_template,
                    templateRows,
                    templateRows,
                    rnd);

    return error;
}

DLLEXPORT int disturb_colors_matrix(matrix_element_type type,
                                    void* matrix,
                                    int templateRows,
                                    int templateColumns,
                                    dlib::rand* rnd,
                                    const double gamma_magnitude,
                                    const double color_magnitude)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             disturb_colors_matrix_template,
                             templateRows,
                             templateRows,
                             rnd,
                             gamma_magnitude,
                             color_magnitude);

    return error;
}

#endif
