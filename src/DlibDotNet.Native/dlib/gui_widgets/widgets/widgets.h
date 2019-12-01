#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_H_
#define _CPP_WIDGETS_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

#include "../action_mediator.h"

#include <functional>

using namespace dlib;
using namespace std;

#pragma region XXX_box

DLLEXPORT void message_box(const char* title, const int title_length, const char* message, const int message_length)
{
    dlib::message_box(std::string(title, title_length), std::string(message, message_length));
}

DLLEXPORT void save_file_box(string_action_mediator* mediator)
{
    dlib::save_file_box(*mediator, &string_action_mediator::on_action_handler);
}

#pragma endregion XXX_box

#endif

#endif