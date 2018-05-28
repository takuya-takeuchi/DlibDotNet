#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_SURF_H_
#define _CPP_SURF_H_

#include "../export.h"
#include <dlib/gui_widgets/widgets.h>
#include <dlib/image_keypoint/draw_surf_points.h>
#include "../shared.h"

using namespace dlib;

DLLEXPORT void draw_surf_points (
    image_window* win,
    std::vector<surf_point*>* sp)
{
    image_window& window = *(static_cast<image_window*>(win));
    std::vector<surf_point*>& points = *(static_cast<std::vector<surf_point*>*>(sp));
    std::vector<surf_point> tmp;
    for (int index = 0; index < points.size(); index++)
    {
        surf_point& surf = *(static_cast<surf_point*>(points[index]));
        tmp.push_back(surf);
    }

    dlib::draw_surf_points(window, tmp);
}

#endif

#endif