#ifndef _CPP_EQUALIZE_HISTOGRAM_H_
#define _CPP_EQUALIZE_HISTOGRAM_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/equalize_histogram.h>
#include <dlib/matrix.h>
#include "../shared.h"

#include "template.h"

using namespace dlib;
using namespace std;

DLLEXPORT int equalize_histogram_array2d(array2d_type in_type, void* in_img)
{
    int err = ERR_OK;

    #define FUNCTION equalize_histogram
    switch(in_type)
    {
        case array2d_type::UInt8:
            image_transforms_in_U_2_array2d__template(err, in_type, in_img);
            break;
        case array2d_type::UInt16:
            image_transforms_in_U_2_array2d__template(err, in_type, in_img);
            break;
        case array2d_type::RgbPixel:
            image_transforms_in_U_2_array2d__template(err, in_type, in_img);
            break;
        case array2d_type::HsiPixel:
            image_transforms_in_U_2_array2d__template(err, in_type, in_img);
            break;
        case array2d_type::Int8:
        case array2d_type::Int16:
        case array2d_type::Int32:
        case array2d_type::UInt32:
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

DLLEXPORT int equalize_histogram_array2d_2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int err = ERR_OK;

    #define FUNCTION equalize_histogram
    switch(out_type)
    {
        case array2d_type::UInt8:
            image_transforms_inout_U_2_array2d__template(err, in_type, in_img, uint8_t, out_img);
            break;
        case array2d_type::UInt16:
            image_transforms_inout_U_2_array2d__template(err, in_type, in_img, uint16_t, out_img);
            break;
        case array2d_type::UInt32:
            image_transforms_inout_U_2_array2d__template(err, in_type, in_img, uint32_t, out_img);
            break;
        case array2d_type::RgbPixel:
            image_transforms_inout_U_2_array2d__template(err, in_type, in_img, rgb_pixel, out_img);
            break;
        case array2d_type::HsiPixel:
            image_transforms_inout_U_2_array2d__template(err, in_type, in_img, hsi_pixel, out_img);
            break;
        case array2d_type::Int8:
        case array2d_type::Int16:
        case array2d_type::Int32:
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }
    #undef FUNCTION

    return err;
}

#endif