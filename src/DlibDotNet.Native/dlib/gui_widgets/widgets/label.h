#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_LABEL_H_
#define _CPP_WIDGETS_LABEL_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT label* label_new(drawable_window* window)
{
    auto& w = *window;
    return new label(w);
}

DLLEXPORT void label_delete(label* label)
{
    delete label;
}

DLLEXPORT void label_set_text(label* label, const char* text)
{
    label->set_text(std::string(text));
}

#endif

#endif