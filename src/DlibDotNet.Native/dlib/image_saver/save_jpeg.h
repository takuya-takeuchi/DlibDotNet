#ifndef _CPP_SAVE_JPEG_H_
#define _CPP_SAVE_JPEG_H_

#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/image_saver/save_jpeg.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

extern "C" __declspec(dllexport) int save_jpeg(array2d_type type, void* image, const char* file_name, int quality)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::save_jpeg(*((array2d<uint8_t>*)image), file_name, quality);
            break;
        case array2d_type::UInt16:
            dlib::save_jpeg(*((array2d<uint16_t>*)image), file_name, quality);
            break;
        case array2d_type::Float:
            dlib::save_jpeg(*((array2d<float>*)image), file_name, quality);
            break;
        case array2d_type::Double:
            dlib::save_jpeg(*((array2d<double>*)image), file_name, quality);
            break;
        case array2d_type::RgbPixel:
            dlib::save_jpeg(*((array2d<rgb_pixel>*)image), file_name, quality);
            break;
        case array2d_type::HsiPixel:
            dlib::save_jpeg(*((array2d<hsi_pixel>*)image), file_name, quality);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::save_jpeg(*((array2d<rgb_alpha_pixel>*)image), file_name, quality);
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif