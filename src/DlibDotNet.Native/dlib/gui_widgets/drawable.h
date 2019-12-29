#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_DRAWABLE_H_
#define _CPP_DRAWABLE_H_

#include "../export.h"
#include <dlib/gui_widgets/drawable.h>
#include <dlib/gui_core.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../shared.h"

DLLEXPORT int drawable_get_bottom(dlib::drawable* drawable)
{
    return drawable->bottom();
}

DLLEXPORT int drawable_get_top(dlib::drawable* drawable)
{
    return drawable->top();
}

DLLEXPORT int drawable_get_left(dlib::drawable* drawable)
{
    return drawable->left();
}

DLLEXPORT int drawable_get_right(dlib::drawable* drawable)
{
    return drawable->right();
}

DLLEXPORT int drawable_get_width(dlib::drawable* drawable)
{
    return drawable->width();
}

DLLEXPORT int drawable_get_height(dlib::drawable* drawable)
{
    return drawable->height();
}

DLLEXPORT void drawable_set_pos(dlib::drawable* drawable, int x, int y)
{
    drawable->set_pos(x, y);
}

#endif

#endif