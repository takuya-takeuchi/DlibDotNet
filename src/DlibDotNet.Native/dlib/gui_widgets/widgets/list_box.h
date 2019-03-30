#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_LIST_BOX_H_
#define _CPP_WIDGETS_LIST_BOX_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

#include "../action_mediator.h"

using namespace dlib;
using namespace std;

DLLEXPORT list_box* list_box_new(drawable_window* window)
{
    auto& w = *window;
    return new list_box(w);
}

DLLEXPORT void list_box_delete(list_box* list_box)
{
    delete list_box;
}

DLLEXPORT void list_box_enable_multiple_select(list_box* list_box)
{
    list_box->enable_multiple_select();
}

DLLEXPORT void list_box_disable_multiple_select(list_box* list_box)
{
    list_box->disable_multiple_select();
}

DLLEXPORT bool list_box_multiple_select_enabled(list_box* list_box)
{
    return list_box->multiple_select_enabled();
}

DLLEXPORT void list_box_set_click_handler(list_box* list_box,
                                          uint32t_action_mediator* mediator)
{
    list_box->set_click_handler(*mediator, &uint32t_action_mediator::on_action_handler);
}

DLLEXPORT void list_box_load_stdstring(list_box* list_box, std::vector<std::string*>* vector)
{
    std::vector<std::string*>& tmp = *vector;
    dlib::array<std::string>::expand_1a in;
    in.resize(tmp.size());
    for (auto index = 0; index < tmp.size(); index++)
        in[index] = std::string(tmp[index]->c_str());
    list_box->load(in);
}

DLLEXPORT unsigned long long list_box_size(list_box* list_box)
{
    return list_box->size();
}

DLLEXPORT void list_box_select(list_box* list_box, unsigned long index)
{
    list_box->select(index);
}

DLLEXPORT void list_box_unselect(list_box* list_box, unsigned long index)
{
    list_box->unselect(index);
}

DLLEXPORT dlib::queue<unsigned long>::kernel_1a* list_box_get_selected(list_box* list_box)
{
    dlib::queue<unsigned long>::kernel_1a* ret = new dlib::queue<unsigned long>::kernel_1a();
    dlib::queue<unsigned long>::kernel_1a& list = *ret;
    list_box->get_selected(list);
    return ret;
}

#endif

#endif