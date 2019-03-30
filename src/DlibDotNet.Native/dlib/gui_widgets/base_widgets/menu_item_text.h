#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_BASE_WIDGETS_MENU_ITEM_TEXT_H_
#define _CPP_BASE_WIDGETS_MENU_ITEM_TEXT_H_

#include "../../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_core.h>
#include "../../shared.h"

#include "../action_mediator.h"

DLLEXPORT dlib::menu_item_text* menu_item_text_new(const char* str,
                                                   void_action_mediator* mediator,
                                                   char hk)
{
    const std::string s(str);
    return new dlib::menu_item_text(s, *mediator, &void_action_mediator::on_action_handler, hk);
}

DLLEXPORT void menu_item_text_delete(dlib::menu_item_text* text)
{
    delete text;
}

#endif

#endif