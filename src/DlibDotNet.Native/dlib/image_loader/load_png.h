#ifndef _CPP_LOAD_PNG_H_
#define _CPP_LOAD_PNG_H_

#include "../export.h"
#include <dlib/image_io.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

DLLEXPORT int load_png(array2d_type type, void* image, const char* file_name)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::load_png(*((array2d<uint8_t>*)image), file_name);
            break;
        case array2d_type::UInt16:
            dlib::load_png(*((array2d<uint16_t>*)image), file_name);
            break;
        case array2d_type::UInt32:
            dlib::load_png(*((array2d<uint32_t>*)image), file_name);
            break;
        case array2d_type::Int8:
            dlib::load_png(*((array2d<int8_t>*)image), file_name);
            break;
        case array2d_type::Int16:
            dlib::load_png(*((array2d<int16_t>*)image), file_name);
            break;
        case array2d_type::Int32:
            dlib::load_png(*((array2d<int32_t>*)image), file_name);
            break;
        case array2d_type::Float:
            dlib::load_png(*((array2d<float>*)image), file_name);
            break;
        case array2d_type::Double:
            dlib::load_png(*((array2d<double>*)image), file_name);
            break;
        case array2d_type::RgbPixel:
            dlib::load_png(*((array2d<rgb_pixel>*)image), file_name);
            break;
        case array2d_type::HsiPixel:
            dlib::load_png(*((array2d<hsi_pixel>*)image), file_name);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::load_png(*((array2d<rgb_alpha_pixel>*)image), file_name);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif