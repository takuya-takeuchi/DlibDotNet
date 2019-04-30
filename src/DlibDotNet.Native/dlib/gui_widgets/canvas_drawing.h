#ifndef DLIB_NO_GUI_SUPPORT

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
#include "../template.h"

#pragma region template

#define draw_line_canvas_template(__TYPE__, error, type, ...) \
dlib::draw_line(*c, *p1, *p2, *((__TYPE__*)p), *area);

#define draw_line_canvas_infinity_template(__TYPE__, error, type, ...) \
dlib::draw_line(*c, *p1, *p2, *((__TYPE__*)p));

#define draw_rectangle_canvas_template(__TYPE__, error, type, ...) \
dlib::draw_rectangle(*c, *rect, *((__TYPE__*)p), *area);

#define draw_rectangle_canvas_infinity_template(__TYPE__, error, type, ...) \
dlib::draw_rectangle(*c, *rect, *((__TYPE__*)p));

#define fill_rect_canvas_template(__TYPE__, error, type, ...) \
dlib::fill_rect(*c, *rect, *((__TYPE__*)p));

#pragma endregion template

DLLEXPORT int draw_line_canvas(void* canvas,
                               dlib::point* p1,
                               dlib::point* p2,
                               array2d_type type,
                               void* p,
                               dlib::rectangle* area)
{
    int error = ERR_OK;
    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    array2d_template(type,
                     error,
                     draw_line_canvas_template,
                     c,
                     p1,
                     p2,
                     p,
                     area);

    return error;
}

DLLEXPORT int draw_line_canvas_infinity(void* canvas,
                                        dlib::point* p1,
                                        dlib::point* p2,
                                        array2d_type type,
                                        void* p)
{
    int error = ERR_OK;
    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    array2d_template(type,
                     error,
                     draw_line_canvas_infinity_template,
                     c,
                     p1,
                     p2,
                     p);

    return error;
}

DLLEXPORT int draw_rectangle_canvas(void* canvas,
                                    dlib::rectangle* rect,
                                    dlib::rectangle* area,
                                    array2d_type type,
                                    void* p)
{
    int error = ERR_OK;
    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    array2d_template(type,
                     error,
                     draw_rectangle_canvas_template,
                     c,
                     rect,
                     p,
                     area);

    return error;
}

DLLEXPORT int draw_rectangle_canvas_infinity(void* canvas,
                                             dlib::rectangle* rect,
                                             array2d_type type,
                                             void* p)
{
    int error = ERR_OK;
    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    array2d_template(type,
                     error,
                     draw_rectangle_canvas_infinity_template,
                     c,
                     rect,
                     p);

    return error;
}

DLLEXPORT int fill_rect_canvas(void* canvas,
                               dlib::rectangle* rect,
                               array2d_type type,
                               void* p)
{
    int error = ERR_OK;
    dlib::canvas* c = static_cast<dlib::canvas*>(canvas);

    array2d_template(type,
                     error,
                     fill_rect_canvas_template,
                     c,
                     rect,
                     p);

    return error;
}

#endif

#endif