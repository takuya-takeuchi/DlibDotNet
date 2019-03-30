#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_BASE_WIDGETS_SCROLLABLE_REGION_H_
#define _CPP_BASE_WIDGETS_SCROLLABLE_REGION_H_

#include "../../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_core.h>
#include "../../shared.h"

DLLEXPORT void scrollable_region_set_pos(dlib::scrollable_region* region, int x, int y)
{
    region->set_pos(x, y);
}

DLLEXPORT void scrollable_region_set_size(dlib::scrollable_region* region, unsigned int width, unsigned int height)
{
    region->set_size(width, height);
}

#endif

#endif