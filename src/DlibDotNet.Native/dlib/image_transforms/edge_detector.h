#ifndef _CPP_EDGE_DETECTOR_H_
#define _CPP_EDGE_DETECTOR_H_

#include "../export.h"
#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/edge_detector.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ARRAY2D_ELEMENT element

#define sobel_edge_detector_template(in_img, out_type, horz, vert)\
do {\
    switch(out_type)\
    {\
        case array2d_type::Float:\
            dlib::sobel_edge_detector(*((array2d<ARRAY2D_ELEMENT>*)in_img), *((array2d<float>*)horz), *((array2d<float>*)vert));\
            break;\
        case array2d_type::Double:\
            dlib::sobel_edge_detector(*((array2d<ARRAY2D_ELEMENT>*)in_img), *((array2d<double>*)horz), *((array2d<double>*)vert));\
            break;\
    }\
} while (0)

#define suppress_non_maximum_edges_template(in_type, horz, vert, out_img)\
do {\
    switch(in_type)\
    {\
        case array2d_type::Float:\
            dlib::suppress_non_maximum_edges(*((array2d<float>*)horz), *((array2d<float>*)vert), *((array2d<ARRAY2D_ELEMENT>*)out_img));\
            break;\
        case array2d_type::Double:\
            dlib::suppress_non_maximum_edges(*((array2d<double>*)horz), *((array2d<double>*)vert), *((array2d<ARRAY2D_ELEMENT>*)out_img));\
            break;\
    }\
} while (0)

DLLEXPORT int sobel_edge_detector(array2d_type in_type, void* in_img, array2d_type out_type, void* horz, void* vert)
{
    // Check output type
    switch(out_type)
    {
		case array2d_type::Float:
		case array2d_type::Double:
			break;
        default:
            return ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
    }

    int err = ERR_OK;
    switch(in_type)
    {
        case array2d_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Float:
            #define ARRAY2D_ELEMENT float
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Double:
            #define ARRAY2D_ELEMENT double
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ARRAY2D_ELEMENT rgb_pixel
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ARRAY2D_ELEMENT hsi_pixel
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ARRAY2D_ELEMENT rgb_alpha_pixel
            sobel_edge_detector_template(in_img, out_type, horz, vert);
            #undef ARRAY2D_ELEMENT
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int suppress_non_maximum_edges(array2d_type in_type, void* horz, void* vert, array2d_type out_type, void* out_img)
{
    // Check output type
    switch(in_type)
    {
		case array2d_type::Float:
		case array2d_type::Double:
			break;
        default:
            return ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
    }

    int err = ERR_OK;
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Float:
            #define ARRAY2D_ELEMENT float
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::Double:
            #define ARRAY2D_ELEMENT double
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbPixel:
            #define ARRAY2D_ELEMENT rgb_pixel
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::HsiPixel:
            #define ARRAY2D_ELEMENT hsi_pixel
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        case array2d_type::RgbAlphaPixel:
            #define ARRAY2D_ELEMENT rgb_alpha_pixel
            suppress_non_maximum_edges_template(in_type, horz, vert, out_img);
            #undef ARRAY2D_ELEMENT
            break;
        default:
            err = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif