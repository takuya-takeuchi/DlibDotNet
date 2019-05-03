#ifndef _CPP_ASSIGN_IMAGE_H_
#define _CPP_ASSIGN_IMAGE_H_

#include "../export.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/image_transforms/assign_image.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define assign_all_pixels_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
dlib::assign_all_pixels(*((dlib::array2d<__SUBTYPE__>*)out_img), *((__TYPE__*)in_pixel));\

#define assign_image_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
dlib::assign_image(*((array2d<__SUBTYPE__>*)out_img), *((array2d<__TYPE__>*)in_img));\

#define assign_image_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __SUBTYPE__, subtype, ...) \
auto& in = *((matrix<__TYPE__>*)in_img);\
auto& out = *((matrix<__TYPE__>*)out_img);\
dlib::assign_image(out, in);\

#pragma endregion template

DLLEXPORT int assign_all_pixels(array2d_type out_type, void* out_img, array2d_type in_type, void* in_pixel)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_inout_in_template(type,
                              error,
                              array2d_inout_out_template,
                              assign_all_pixels_template,
                              subtype,
                              in_pixel,
                              out_img);
    return error;
}

DLLEXPORT int assign_image(array2d_type out_type, void* out_img, array2d_type in_type, void* in_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_inout_in_template(type,
                              error,
                              array2d_inout_out_template,
                              assign_image_template,
                              subtype,
                              in_img,
                              out_img);
    return error;
}

DLLEXPORT int assign_image_matrix(matrix_element_type out_type, void* out_img, matrix_element_type in_type, void* in_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    matrix_inout_in_template(type,
                             error,
                             matrix_inout_out_template,
                             matrix_inout_template_size_template,
                             assign_image_matrix_template,
                             subtype,
                             0,
                             0,
                             in_img,
                             out_img);

    return error;
}

#endif