#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_TEXT_FIELDS_H_
#define _CPP_WIDGETS_TEXT_FIELDS_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

#include "../action_mediator.h"

using namespace dlib;
using namespace std;

DLLEXPORT text_field* text_field_new(drawable_window* window)
{
    auto& w = *window;
    return new text_field(w);
}

DLLEXPORT void text_field_delete(text_field* text_field)
{
    delete text_field;
}

DLLEXPORT void text_field_get_text(text_field* text_field, std::string** text)
{
    *text = new std::string(text_field->text());
}

DLLEXPORT void text_field_set_text(text_field* text_field, const char* text, const int text_length)
{
    text_field->set_text(std::string(text, text_length));
}

DLLEXPORT void text_field_set_width(text_field* text_field, const unsigned long width)
{
    text_field->set_width(width);
}

DLLEXPORT void text_field_set_text_modified_handler(text_field* text_field,
                                                    void_action_mediator* mediator)
{
    text_field->set_text_modified_handler(*mediator, &void_action_mediator::on_action_handler);
}

DLLEXPORT bool text_field_has_input_focus(text_field* text_field)
{
    return text_field->has_input_focus();
}

DLLEXPORT void text_field_select_all_text(text_field* text_field)
{
    text_field->select_all_text();
}

DLLEXPORT void text_field_give_input_focus(text_field* text_field)
{
    text_field->give_input_focus();
}

#endif

#endif