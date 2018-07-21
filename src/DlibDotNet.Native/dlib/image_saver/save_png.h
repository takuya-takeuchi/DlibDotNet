#ifndef _CPP_SAVE_PNG_H_
#define _CPP_SAVE_PNG_H_

#include "../export.h"
#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/image_saver/save_png.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT int save_png(array2d_type type, void* image, const char* file_name)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::save_png(*((array2d<uint8_t>*)image), file_name);
            break;
        case array2d_type::UInt16:
            dlib::save_png(*((array2d<uint16_t>*)image), file_name);
            break;
        case array2d_type::UInt32:
            dlib::save_png(*((array2d<uint32_t>*)image), file_name);
            break;
        case array2d_type::Int8:
            dlib::save_png(*((array2d<int8_t>*)image), file_name);
            break;
        case array2d_type::Int16:
            dlib::save_png(*((array2d<int16_t>*)image), file_name);
            break;
        case array2d_type::Int32:
            dlib::save_png(*((array2d<int32_t>*)image), file_name);
            break;
        case array2d_type::Float:
            dlib::save_png(*((array2d<float>*)image), file_name);
            break;
        case array2d_type::Double:
            dlib::save_png(*((array2d<double>*)image), file_name);
            break;
        case array2d_type::RgbPixel:
            dlib::save_png(*((array2d<rgb_pixel>*)image), file_name);
            break;
        case array2d_type::HsiPixel:
            dlib::save_png(*((array2d<hsi_pixel>*)image), file_name);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::save_png(*((array2d<rgb_alpha_pixel>*)image), file_name);
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif