#ifndef _CPP_SPATIAL_FILTERING_H_
#define _CPP_SPATIAL_FILTERING_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_processing/full_object_detection_abstract.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/spatial_filtering.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#define gaussian_blur_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
dlib::gaussian_blur(in_, out_, sigma, max_size);\

#define sum_filter_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
dlib::sum_filter(in_, out_, *rect);\

#pragma region gaussian_blur

DLLEXPORT int gaussian_blur(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double sigma, int max_size)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_nonalpha_inout_in_template(type,
                                       error,
                                       array2d_nonalpha_inout_out_template,
                                       gaussian_blur_template,
                                       subtype,
                                       in_img,
                                       out_img,
                                       sigma,
                                       max_size);

    return error;
}

#pragma region gaussian_blur

#pragma endregion sum_filter

DLLEXPORT int sum_filter(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, rectangle* rect)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_numeric_inout_in_template(type,
                                      error,
                                      array2d_numeric_inout_out_template,
                                      sum_filter_template,
                                      subtype,
                                      in_img,
                                      out_img,
                                      rect);

    return error;
}

#pragma endregion sum_filter

#endif