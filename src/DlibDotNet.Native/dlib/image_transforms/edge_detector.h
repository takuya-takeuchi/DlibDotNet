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

#define ARRAY2D_ELEMENT element
#undef ARRAY2D_ELEMENT

#define sobel_edge_detector_template(__TYPE__, error, type, ...) \
switch(out_type)\
{\
    case array2d_type::Int16:\
        dlib::sobel_edge_detector(*((array2d<__TYPE__>*)in_img), *((array2d<int16_t>*)horz), *((array2d<int16_t>*)vert));\
        break;\
    case array2d_type::Float:\
        dlib::sobel_edge_detector(*((array2d<__TYPE__>*)in_img), *((array2d<float>*)horz), *((array2d<float>*)vert));\
        break;\
    case array2d_type::Double:\
        dlib::sobel_edge_detector(*((array2d<__TYPE__>*)in_img), *((array2d<double>*)horz), *((array2d<double>*)vert));\
        break;\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}\

#define suppress_non_maximum_edges_template(__TYPE__, error, type, ...) \
switch(in_type)\
{\
    case array2d_type::Int16:\
            dlib::suppress_non_maximum_edges(*((array2d<int16_t>*)horz), *((array2d<int16_t>*)vert), *((array2d<__TYPE__>*)out_img));\
        break;\
    case array2d_type::Float:\
            dlib::suppress_non_maximum_edges(*((array2d<float>*)horz), *((array2d<float>*)vert), *((array2d<__TYPE__>*)out_img));\
        break;\
    case array2d_type::Double:\
            dlib::suppress_non_maximum_edges(*((array2d<double>*)horz), *((array2d<double>*)vert), *((array2d<__TYPE__>*)out_img));\
        break;\
    default:\
        error = ERR_ARRAY2D_TYPE_NOT_SUPPORT;\
        break;\
}\

DLLEXPORT int sobel_edge_detector(array2d_type type, void* in_img, array2d_type out_type, void* horz, void* vert)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     sobel_edge_detector_template,
                     in_img,
                     out_type,
                     horz,
                     vert);

    return error;
}

DLLEXPORT int suppress_non_maximum_edges(array2d_type in_type, void* horz, void* vert, array2d_type out_type, void* out_img)
{
    int error = ERR_OK;

    auto type = out_type;

    array2d_template(type,
                     error,
                     suppress_non_maximum_edges_template,
                     horz,
                     vert,
                     in_type,
                     out_img);

    return error;
}

#endif