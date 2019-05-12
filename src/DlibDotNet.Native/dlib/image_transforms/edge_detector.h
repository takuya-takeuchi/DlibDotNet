#ifndef _CPP_EDGE_DETECTOR_H_
#define _CPP_EDGE_DETECTOR_H_

#include "../export.h"
#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/edge_detector.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#define sobel_edge_detector_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in = *((array2d<__TYPE__>*)in_img);\
auto& h = *((array2d<__SUBTYPE__>*)horz);\
auto& v = *((array2d<__SUBTYPE__>*)vert);\
dlib::sobel_edge_detector(in, h, v);\

#define suppress_non_maximum_edges_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& h = *((array2d<__TYPE__>*)horz);\
auto& v = *((array2d<__TYPE__>*)vert);\
auto& out = *((array2d<__SUBTYPE__>*)out_img);\
dlib::suppress_non_maximum_edges(h, v, out);\

DLLEXPORT int sobel_edge_detector(array2d_type type, void* in_img, array2d_type out_type, void* horz, void* vert)
{
    int error = ERR_OK;

    auto subtype = out_type;

    array2d_inout_in_template(type,
                              error,
                              array2d_shortdecimal_inout_out_template,
                              sobel_edge_detector_template,
                              subtype,
                              in_img,
                              out_type,
                              horz,
                              vert);

    return error;
}

DLLEXPORT int suppress_non_maximum_edges(array2d_type in_type, void* horz, void* vert, array2d_type out_type, void* out_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_shortdecimal_inout_in_template(type,
                                           error,
                                           array2d_inout_out_template,
                                           suppress_non_maximum_edges_template,
                                           subtype,
                                           horz,
                                           vert,
                                           out_img);

    return error;
}

#endif