#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_PERSPECTIVE_WINDOW_H_
#define _CPP_WIDGETS_PERSPECTIVE_WINDOW_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define perspective_window_add_overlay_template(__TYPE__, error, type, ...) \
window->add_overlay(*p1, *p2, *((__TYPE__*)p));

#define perspective_window_add_overlay3_template(__TYPE__, error, type, ...) \
window->add_overlay(tmp, *((__TYPE__*)p));

#define perspective_window_overlay_dot_new2_template(__TYPE__, error, type, ...) \
ret = new perspective_window::overlay_dot(tmp, *((__TYPE__*)p));

#pragma endregion template

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
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     perspective_window_add_overlay_template,
                     window,
                     p1,
                     p2,
                     p);

    return error;
}

DLLEXPORT int perspective_window_add_overlay2(perspective_window* window, std::vector<dlib::vector<double>*>* points)
{
    int error = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);

    return error;
}

DLLEXPORT int perspective_window_add_overlay3(perspective_window* window, std::vector<dlib::vector<double>*>* points, array2d_type type, void* p)
{
    int error = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    array2d_template(type,
                     error,
                     perspective_window_add_overlay3_template,
                     window,
                     tmp,
                     p);

    return error;
}

DLLEXPORT int perspective_window_add_overlay4(perspective_window* window, std::vector<perspective_window::overlay_dot*>* overlay)
{
    int error = ERR_OK;

    std::vector<perspective_window::overlay_dot*>& vector = *(static_cast<std::vector<perspective_window::overlay_dot*>*>(overlay));
    std::vector<perspective_window::overlay_dot> tmp;
    for(int i = 0, end = overlay->size(); i < end; i++)
    {
        perspective_window::overlay_dot& p = *(static_cast<perspective_window::overlay_dot*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);

    return error;
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
    int error = ERR_OK;
    perspective_window::overlay_dot* ret = nullptr;

    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(v));

    array2d_template(type,
                     error,
                     perspective_window_overlay_dot_new2_template,
                     tmp,
                     p,
                     ret);

    return ret;
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