#ifndef _CPP_DRAW_H_
#define _CPP_DRAW_H_

#include <dlib/array.h>
#include <dlib/image_transforms/draw.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;


extern "C" __declspec(dllexport) int tile_images(array2d_type in_type, void* images, void** ret_image)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case array2d_type::UInt8:
            *ret_image = new dlib::matrix<uint8_t>(dlib::tile_images(*((dlib::array<dlib::array2d<uint8_t>>*)images)));
            break;
        case array2d_type::UInt16:
            *ret_image = new dlib::matrix<uint16_t>(dlib::tile_images(*((dlib::array<dlib::array2d<uint16_t>>*)images)));
            break;
        case array2d_type::Float:
            *ret_image = new dlib::matrix<float>(dlib::tile_images(*((dlib::array<dlib::array2d<float>>*)images)));
            break;
        case array2d_type::Double:
            *ret_image = new dlib::matrix<double>(dlib::tile_images(*((dlib::array<dlib::array2d<double>>*)images)));
            break;
        case array2d_type::RgbPixel:
            *ret_image = new dlib::matrix<rgb_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<rgb_pixel>>*)images)));
            break;
        case array2d_type::HsiPixel:
            *ret_image = new dlib::matrix<hsi_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<hsi_pixel>>*)images)));
            break;
        case array2d_type::RgbAlphaPixel:
            *ret_image = new dlib::matrix<rgb_alpha_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<rgb_alpha_pixel>>*)images)));
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif