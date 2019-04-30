#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_BASE_WIDGETS_POPUP_MENU_H_
#define _CPP_BASE_WIDGETS_POPUP_MENU_H_

#include "../../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_core.h>
#include "../../shared.h"

DLLEXPORT unsigned int popup_menu_add_menu_item_menu_item_text(dlib::popup_menu* popup_menu, dlib::menu_item_text* new_item)
{
    dlib::menu_item_text& ni = *new_item;
    return popup_menu->add_menu_item(ni);
}

DLLEXPORT unsigned int popup_menu_add_menu_item_menu_item_separator(dlib::popup_menu* popup_menu, dlib::menu_item_separator* new_item)
{
    dlib::menu_item_separator& ni = *new_item;
    return popup_menu->add_menu_item(ni);
}

#endif

#endif