#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_BASE_WIDGETS_MENU_ITEM_SEPARATOR_H_
#define _CPP_BASE_WIDGETS_MENU_ITEM_SEPARATOR_H_

#include "../../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_core.h>
#include "../../shared.h"

DLLEXPORT dlib::menu_item_separator* menu_item_separator_new()
{
    return new dlib::menu_item_separator();
}

DLLEXPORT void menu_item_separator_delete(dlib::menu_item_separator* separator)
{
    delete separator;
}

#endif

#endif