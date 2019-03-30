#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_PERSPECTIVE_WINDOW_H_
#define _CPP_WIDGETS_PERSPECTIVE_WINDOW_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT perspective_window* perspective_window_new()
{
	return new perspective_window();
}

DLLEXPORT perspective_window* perspective_window_new2(std::vector<dlib::vector<double>*>* point_cloud)
{
    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(point_cloud));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = point_cloud->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

	return new perspective_window(tmp);
}

DLLEXPORT perspective_window* perspective_window_new3(std::vector<dlib::vector<double>*>* point_cloud, const char* title)
{
    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(point_cloud));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = point_cloud->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

	return new perspective_window(tmp, title);
}

DLLEXPORT void perspective_window_delete(perspective_window* window)
{
	delete window;
}

#pragma region add_overlay

DLLEXPORT int perspective_window_add_overlay(perspective_window* window, dlib::vector<double>* p1, dlib::vector<double>* p2, array2d_type type, void* p)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(*p1, *p2, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(*p1, *p2, *((uint16_t*)p));
            break;
        case array2d_type::UInt32:
            window->add_overlay(*p1, *p2, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(*p1, *p2, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(*p1, *p2, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(*p1, *p2, *((int32_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(*p1, *p2, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(*p1, *p2, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(*p1, *p2, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(*p1, *p2, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(*p1, *p2, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int perspective_window_add_overlay2(perspective_window* window, std::vector<dlib::vector<double>*>* points)
{
    int err = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);

    return err;
}

DLLEXPORT int perspective_window_add_overlay3(perspective_window* window, std::vector<dlib::vector<double>*>* points, array2d_type type, void* p)
{
    int err = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(tmp, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(tmp, *((uint16_t*)p));
            break;
        case array2d_type::UInt32:
            window->add_overlay(tmp, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(tmp, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(tmp, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(tmp, *((int32_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(tmp, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(tmp, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(tmp, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(tmp, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(tmp, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int perspective_window_add_overlay4(perspective_window* window, std::vector<perspective_window::overlay_dot*>* overlay)
{
    int err = ERR_OK;

    std::vector<perspective_window::overlay_dot*>& vector = *(static_cast<std::vector<perspective_window::overlay_dot*>*>(overlay));
    std::vector<perspective_window::overlay_dot> tmp;
    for(int i = 0, end = overlay->size(); i < end; i++)
    {
        perspective_window::overlay_dot& p = *(static_cast<perspective_window::overlay_dot*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);

    return err;
}

#pragma endregion add_overlay

#pragma region overlay_dot

DLLEXPORT perspective_window::overlay_dot* perspective_window_overlay_dot_new(dlib::vector<double>* v)
{
    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(v));
	return new perspective_window::overlay_dot(tmp);
}

DLLEXPORT perspective_window::overlay_dot* perspective_window_overlay_dot_new2(dlib::vector<double>* v, array2d_type type, void* p)
{
    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(v));

    switch(type)
    {
        case array2d_type::UInt8:
            return new perspective_window::overlay_dot(tmp, *((uint8_t*)p));
        case array2d_type::UInt16:
            return new perspective_window::overlay_dot(tmp, *((uint16_t*)p));
        case array2d_type::UInt32:
            return new perspective_window::overlay_dot(tmp, *((uint32_t*)p));
        case array2d_type::Int8:
            return new perspective_window::overlay_dot(tmp, *((int8_t*)p));
        case array2d_type::Int16:
            return new perspective_window::overlay_dot(tmp, *((int16_t*)p));
        case array2d_type::Int32:
            return new perspective_window::overlay_dot(tmp, *((int32_t*)p));
        case array2d_type::Float:
            return new perspective_window::overlay_dot(tmp, *((float*)p));
        case array2d_type::Double:
            return new perspective_window::overlay_dot(tmp, *((double*)p));
        case array2d_type::RgbPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::rgb_pixel*)p));
        case array2d_type::HsiPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::hsi_pixel*)p));
        case array2d_type::RgbAlphaPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::rgb_alpha_pixel*)p));
        default:
            return nullptr;
    }
}

DLLEXPORT bool perspective_window_overlay_dot_color(perspective_window::overlay_dot* dot, dlib::rgb_alpha_pixel* color)
{
    memcpy(color, &(dot->color), sizeof(dlib::rgb_alpha_pixel));
    return true;
}

DLLEXPORT bool perspective_window_overlay_dot_p(perspective_window::overlay_dot* dot, dlib::vector<double>** ret)
{
    *ret = new dlib::vector<double>(dot->p);
    return true;
}

DLLEXPORT void perspective_window_overlay_dot_delete(perspective_window::overlay_dot* dot)
{
	delete dot;
}

#pragma endregion overlay_dot

#endif

#endif