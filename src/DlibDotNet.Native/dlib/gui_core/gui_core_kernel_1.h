
#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_GUI_CORE_KERNEL1_H_
#define _CPP_GUI_CORE_KERNEL1_H_

#include "../export.h"
#include <dlib/gui_core/gui_core_kernel_1.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT void base_window_set_title(base_window* window, const char* title)
{
	window->set_title(title);
}

DLLEXPORT void base_window_wait_until_closed(base_window* window)
{
	window->wait_until_closed();
}

#endif

#endif