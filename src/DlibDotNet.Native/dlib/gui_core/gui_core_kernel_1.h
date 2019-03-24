
#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_GUI_CORE_KERNEL1_H_
#define _CPP_GUI_CORE_KERNEL1_H_

#include "../export.h"
#include <dlib/gui_widgets.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT void base_window_close_window(base_window* window)
{
	window->close_window();
}

DLLEXPORT void base_window_get_size(base_window* window, unsigned int* width, unsigned int* height)
{
	unsigned long w;
	unsigned long h;
	window->get_size(w, h);
	*width = w;
	*height = h;
}

DLLEXPORT void base_window_set_title(base_window* window, const char* title)
{
	window->set_title(title);
}

DLLEXPORT void base_window_wait_until_closed(base_window* window)
{
	window->wait_until_closed();
}

DLLEXPORT void base_window_show(base_window* window)
{
	window->show();
}

#endif

#endif