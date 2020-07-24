#ifndef _CPP_PIXEL_H_
#define _CPP_PIXEL_H_

#include "export.h"
#include <dlib/pixel.h>

using namespace dlib;

DLLEXPORT void assign_pixel_rgb_rgbalpha(rgb_pixel* dest, rgb_alpha_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}

DLLEXPORT void assign_pixel_rgbalpha_rgb(rgb_alpha_pixel* dest, rgb_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}

DLLEXPORT void assign_pixel_rgb_hsi(rgb_pixel* dest, hsi_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}

DLLEXPORT void assign_pixel_rgb_lab(rgb_pixel* dest, lab_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}
DLLEXPORT void assign_pixel_rgbalpha_hsi(rgb_alpha_pixel* dest, hsi_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}

DLLEXPORT void assign_pixel_rgbalpha_lab(rgb_alpha_pixel* dest, lab_pixel* src)
{
    dlib::assign_pixel(*dest, *src);
}


#endif