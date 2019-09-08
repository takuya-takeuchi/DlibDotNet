#ifndef _CPP_IMAGE_TRANSFORMS_INTERPOLATION_RESIZE_IMAGE_H_
#define _CPP_IMAGE_TRANSFORMS_INTERPOLATION_RESIZE_IMAGE_H_

#include "../../export.h"
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#define resize_image_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& src = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
auto& dst = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(out_img);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::resize_image(src, dst, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::resize_image(src, dst, interpolate_bilinear());\
        break;\
    case interpolation_type::Quadratic:\
        dlib::resize_image(src, dst, interpolate_quadratic());\
        break;\
}\

DLLEXPORT int resize_image_matrix(const matrix_element_type matrix_type,
                                  void* src_matrix,
                                  const uint32_t templateRows,
                                  const uint32_t templateColumns,
                                  void* dst_matrix,
                                  const interpolation_type int_type)
{
    int error = ERR_OK;

    auto type = matrix_type;
    auto subtype = matrix_type;
    
    matrix_numericrgbbgr_inout_in_template(type,
                                           error,
                                           matrix_inout_out_template,
                                           matrix_inout_template_size_template,
                                           resize_image_matrix_template,
                                           subtype,
                                           templateRows,
                                           templateColumns,
                                           src_matrix,
                                           dst_matrix,
                                           int_type);
    
    return error;
}

#endif