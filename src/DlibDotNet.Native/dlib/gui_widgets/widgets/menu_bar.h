#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_MENU_BAR_H_
#define _CPP_WIDGETS_MENU_BAR_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT menu_bar* menu_bar_new(drawable_window* window)
{
    auto& w = *window;
    return new menu_bar(w);
}

DLLEXPORT void menu_bar_delete(menu_bar* menubar)
{
    delete menubar;
}

DLLEXPORT void menu_bar_set_number_of_menus(menu_bar* menubar, unsigned long num)
{
    menubar->set_number_of_menus(num);
}

DLLEXPORT void menu_bar_set_menu_name(menu_bar* menubar, unsigned long idx, const char* name, const int name_length, char underline_ch)
{
    menubar->set_menu_name(idx, std::string(name, name_length), underline_ch);
}

DLLEXPORT popup_menu* menu_bar_menu(menu_bar* menubar, unsigned long idx)
{
    popup_menu& menu = menubar->menu(idx);
    return &menu;
}

#endif

#endif