#ifndef _CPP_CANVAS_DRAWING_H_
#define _CPP_CANVAS_DRAWING_H_

#include "../export.h"
#include <dlib/gui_widgets/canvas_drawing.h>
#include <dlib/gui_core.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../shared.h"
 
DLLEXPORT int draw_line_canvas(
    void* canvas, 
    dlib::point* p1,
    dlib::point* p2,
    array2d_type type,
    void* p,
    dlib::rectangle* area)
{
    int err = ERR_OK;

    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    switch(type)
    {
        case array2d_type::UInt8:
            dlib::draw_line(*c, *p1, *p2, *((uint8_t*)p), *area);
            break;
        case array2d_type::UInt16:
            dlib::draw_line(*c, *p1, *p2, *((uint16_t*)p), *area);
            break;
        case array2d_type::Float:
            dlib::draw_line(*c, *p1, *p2, *((float*)p), *area);
            break;
        case array2d_type::Double:
            dlib::draw_line(*c, *p1, *p2, *((double*)p), *area);
            break;
        case array2d_type::RgbPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::rgb_pixel*)p), *area);
            break;
        case array2d_type::HsiPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::hsi_pixel*)p), *area);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::rgb_alpha_pixel*)p), *area);
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int draw_line_canvas_infinity(
   void* canvas, 
   dlib::point* p1,
   dlib::point* p2,
   array2d_type type,
   void* p)
{
    int err = ERR_OK;

    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    switch(type)
    {
        case array2d_type::UInt8:
            dlib::draw_line(*c, *p1, *p2, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            dlib::draw_line(*c, *p1, *p2, *((uint16_t*)p));
            break;
        case array2d_type::Float:
            dlib::draw_line(*c, *p1, *p2, *((float*)p));
            break;
        case array2d_type::Double:
            dlib::draw_line(*c, *p1, *p2, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::draw_line(*c, *p1, *p2, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int draw_rectangle_canvas(
    void* canvas, 
    dlib::rectangle* rect,
    dlib::rectangle* area,
    array2d_type type,
    void* p)
{
    int err = ERR_OK;

    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    switch(type)
    {
        case array2d_type::UInt8:
            dlib::draw_rectangle(*c, *rect, *((uint8_t*)p), *area);
            break;
        case array2d_type::UInt16:
            dlib::draw_rectangle(*c, *rect, *((uint16_t*)p), *area);
            break;
        case array2d_type::Int32:
            dlib::draw_rectangle(*c, *rect, *((int32_t*)p), *area);
            break;
        case array2d_type::Float:
            dlib::draw_rectangle(*c, *rect, *((float*)p), *area);
            break;
        case array2d_type::Double:
            dlib::draw_rectangle(*c, *rect, *((double*)p), *area);
            break;
        case array2d_type::RgbPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::rgb_pixel*)p), *area);
            break;
        case array2d_type::HsiPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::hsi_pixel*)p), *area);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::rgb_alpha_pixel*)p), *area);
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int draw_rectangle_canvas_infinity(
   void* canvas, 
   dlib::rectangle* rect,
   array2d_type type,
   void* p)
{
    int err = ERR_OK;

    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    switch(type)
    {
        case array2d_type::UInt8:
            dlib::draw_rectangle(*c, *rect, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            dlib::draw_rectangle(*c, *rect, *((uint16_t*)p));
            break;
        case array2d_type::Int32:
            dlib::draw_rectangle(*c, *rect, *((int32_t*)p));
            break;
        case array2d_type::Float:
            dlib::draw_rectangle(*c, *rect, *((float*)p));
            break;
        case array2d_type::Double:
            dlib::draw_rectangle(*c, *rect, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::draw_rectangle(*c, *rect, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif