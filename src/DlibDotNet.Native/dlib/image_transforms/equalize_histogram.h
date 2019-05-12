#ifndef _CPP_EQUALIZE_HISTOGRAM_H_
#define _CPP_EQUALIZE_HISTOGRAM_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/equalize_histogram.h>
#include <dlib/matrix.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define equalize_histogram_array2d_template(__TYPE__, error, type, ...) \
auto& in = *(array2d<__TYPE__>*)in_img;\
dlib::equalize_histogram(in);\

#define equalize_histogram_array2d_2_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in = *(array2d<__TYPE__>*)in_img;\
auto& out = *(array2d<__SUBTYPE__>*)out_img;\
dlib::equalize_histogram(in, out);\

#pragma endregion template

DLLEXPORT int equalize_histogram_array2d(array2d_type type, void* in_img)
{
    int error = ERR_OK;

    array2d_U2nonalpha_template(type,
                                error,
                                equalize_histogram_array2d_template,
                                in_img);

    return error;
}

DLLEXPORT int equalize_histogram_array2d_2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_U2nonalpha_inout_in_template(type,
                                         error,
                                         array2d_unsigned_nonalpha_inout_out_template,
                                         equalize_histogram_array2d_2_template,
                                         subtype,
                                         in_img,
                                         out_img);
    
    return error;
}

#endif