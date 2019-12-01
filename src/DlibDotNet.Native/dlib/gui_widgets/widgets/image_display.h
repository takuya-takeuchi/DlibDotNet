#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_IMAGE_DISPLAY_H_
#define _CPP_WIDGETS_IMAGE_DISPLAY_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"
#include "../../template.h"

#include "../action_mediator.h"

using namespace dlib;
using namespace std;

#pragma region template

#define image_display_set_image_template(__TYPE__, error, type, ...) \
array2d<__TYPE__>& tmp = *((array2d<__TYPE__>*)img);\
display->set_image(tmp);

#pragma endregion template

DLLEXPORT image_display* image_display_new(drawable_window* window)
{
    auto& w = *window;
    return new image_display(w);
}

DLLEXPORT void image_display_delete(image_display* display)
{
    delete display;
}

DLLEXPORT void image_display_set_image_clicked_handler(image_display* display,
                                                       click_action_mediator* mediator)
{
    display->set_image_clicked_handler(*mediator, &click_action_mediator::on_action_handler);
}

DLLEXPORT void image_display_set_overlay_rects_changed_handler(image_display* display,
                                                               void_action_mediator* mediator)
{
    display->set_overlay_rects_changed_handler(*mediator, &void_action_mediator::on_action_handler);
}

DLLEXPORT void image_display_set_overlay_rect_selected_handler(image_display* display,
                                                               image_display_overlay_rect_action_mediator* mediator)
{
    display->set_overlay_rect_selected_handler(*mediator, &image_display_overlay_rect_action_mediator::on_action_handler);
}

DLLEXPORT void image_displayclear_overlay(image_display* display)
{
    display->clear_overlay();
}

DLLEXPORT int image_display_set_image(image_display* display,
                                      array2d_type type,
                                      void* img)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     image_display_set_image_template,
                     display,
                     img);

    return error;
}

DLLEXPORT void image_display_set_default_overlay_rect_color(image_display* display, rgb_alpha_pixel color)
{
    display->set_default_overlay_rect_color(color);
}

DLLEXPORT void image_display_set_default_overlay_rect_label(image_display* display, const char* label, const int label_length)
{
    display->set_default_overlay_rect_label(std::string(label, label_length));
}

DLLEXPORT void image_display_add_overlay(image_display* display, std::vector<image_display::overlay_rect*>* rects)
{
    std::vector<image_display::overlay_rect*>& tmp = *rects;
    std::vector<image_display::overlay_rect> in;
    for (auto index = 0; index < tmp.size(); index++)
        in.push_back(*tmp[index]);
    display->add_overlay(in);
}

DLLEXPORT void image_display_clear_overlay(image_display* display)
{
    display->clear_overlay();
}

DLLEXPORT std::vector<image_display::overlay_rect*>* image_display_get_overlay_rects(image_display* display)
{
    auto rects = display->get_overlay_rects();
    auto ret = new std::vector<image_display::overlay_rect*>();
    for (auto index = 0; index < rects.size(); index++)
        ret->push_back(new image_display::overlay_rect(rects[index]));

    return ret;
}

DLLEXPORT std::string* image_display_get_default_overlay_rect_label(image_display* display)
{
    auto str = display->get_default_overlay_rect_label();
    return new std::string(str);
}

DLLEXPORT void image_display_add_labelable_part_name(image_display* display, const char* name, const int name_length)
{
    display->add_labelable_part_name(std::string(name, name_length));
}

#pragma region overlay_rect

DLLEXPORT image_display::overlay_rect* image_display_overlay_rect_new()
{
    return new image_display::overlay_rect();
}

DLLEXPORT void image_display_overlay_rect_delete(image_display::overlay_rect* overlayRect)
{
    delete overlayRect;
}

DLLEXPORT void image_display_overlay_rect_get_ignore(image_display::overlay_rect* overlayRect, rgb_alpha_pixel* value)
{
    *value = overlayRect->color;
}

DLLEXPORT void image_display_overlay_rect_set_ignore(image_display::overlay_rect* overlayRect, rgb_alpha_pixel value)
{
    overlayRect->color = value;
}

DLLEXPORT bool image_display_overlay_rect_get_crossed_out(image_display::overlay_rect* overlayRect)
{
    return overlayRect->crossed_out;
}

DLLEXPORT void image_display_overlay_rect_set_crossed_out(image_display::overlay_rect* overlayRect, bool value)
{
    overlayRect->crossed_out = value;
}

DLLEXPORT std::string* image_display_overlay_rect_get_label(image_display::overlay_rect* overlayRect)
{
    return new std::string(overlayRect->label);
}

DLLEXPORT void image_display_overlay_rect_set_label(image_display::overlay_rect* overlayRect, const char* value, const int value_length)
{
    overlayRect->label = std::string(value, value_length);
}

DLLEXPORT dlib::rectangle* image_display_overlay_rect_get_rect(image_display::overlay_rect* overlayRect)
{
    return new dlib::rectangle(overlayRect->rect);
}

DLLEXPORT void image_display_overlay_rect_set_rect(image_display::overlay_rect* overlayRect, dlib::rectangle* value)
{
    overlayRect->rect = *value;
}

DLLEXPORT void image_display_overlay_rect_get_color(image_display::overlay_rect* overlayRect, rgb_alpha_pixel* value)
{
    *value = overlayRect->color;
}

DLLEXPORT void image_display_overlay_rect_set_color(image_display::overlay_rect* overlayRect, rgb_alpha_pixel value)
{
    overlayRect->color = value;
}

#pragma region parts

DLLEXPORT void image_display_overlay_rect_get_parts_get_all(image_display::overlay_rect* overlayRect,
                                                            std::vector<std::string*>* strings,
                                                            std::vector<dlib::point*>* points)
{
    std::map<std::string,dlib::point>& m = overlayRect->parts;
    auto end = m.end();
    for (auto it = m.begin(); it != end; it++)
    {
        auto f = it->first;
        auto s = it->second;
        strings->push_back(new std::string(f));
        points->push_back(new dlib::point(s));
    }
}

DLLEXPORT bool image_display_overlay_rect_get_parts_get_value(image_display::overlay_rect* overlayRect,
                                                              const char* key,
                                                              const int key_length,
                                                              dlib::point** result)
{
    std::map<std::string,point>& m = overlayRect->parts;
    std::string k(key, key_length);
    if (m.find(k) == m.end())
    {
        return false;
    }
    else
    {
        *result = new dlib::point(m[k]);
        return true;
    }
}

DLLEXPORT void image_display_overlay_rect_get_parts_set_value(image_display::overlay_rect* overlayRect,
                                                              const char* key,
                                                              const int key_length,
                                                              dlib::point* value)
{
    std::map<std::string,dlib::point>& m = overlayRect->parts;
    dlib::point p(*value);
    std::string k(key, key_length);
    m[k] = p;
}

DLLEXPORT int image_display_overlay_rect_get_parts_get_size(image_display::overlay_rect* overlayRect)
{
    return overlayRect->parts.size();
}

DLLEXPORT void image_display_overlay_rect_get_parts_clear(image_display::overlay_rect* overlayRect)
{
    overlayRect->parts.clear();
}

#pragma endregion parts

#pragma endregion overlay_rect

#endif

#endif