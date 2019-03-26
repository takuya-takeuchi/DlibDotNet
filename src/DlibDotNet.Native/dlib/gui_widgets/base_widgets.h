#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_BASE_WIDGETS_H_
#define _CPP_BASE_WIDGETS_H_

#include "../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_core.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/matrix.h>
#include <dlib/pixel.h>
#include "../shared.h"

#pragma region scrollable_region

DLLEXPORT void scrollable_region_set_pos(dlib::scrollable_region* region, int x, int y)
{
    region->set_pos(x, y);
}

DLLEXPORT void scrollable_region_set_size(dlib::scrollable_region* region, unsigned int width, unsigned int height)
{
    region->set_size(width, height);
}

#pragma endregion scrollable_region

#pragma region popup_menu

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

#pragma endregion popup_menu

#pragma region menu_item_text

DLLEXPORT dlib::menu_item_text* menu_item_text_new(const char* str,
                                                   dlib::drawable_window* window,
                                                   void (dlib::drawable_window::*event_handler)(),
                                                   char hk)
{
    const std::string s(str);
    dlib::drawable_window& w = *window;
    return new dlib::menu_item_text(s, w, event_handler, hk);
}

DLLEXPORT void menu_item_text_delete(dlib::menu_item_text* text)
{
    delete text;
}

#pragma endregion menu_item_text

#pragma region menu_item_separator

DLLEXPORT dlib::menu_item_separator* menu_item_separator_new()
{
    return new dlib::menu_item_separator();
}

DLLEXPORT void menu_item_separator_delete(dlib::menu_item_separator* separator)
{
    delete separator;
}

#pragma endregion menu_item_separator

#endif

#endif