#include "action_mediator.h"

#ifndef DLIB_NO_GUI_SUPPORT
MAKE_1ARGS_R_MEDIATOR_IMPLEMENTATION(dlib::image_display::overlay_rect, image_display_overlay_rect)
MAKE_1ARGS_R_MEDIATOR_IMPLEMENTATION(std::string, string)
MAKE_1ARGS_V_MEDIATOR_IMPLEMENTATION(unsigned long, uint32t)
MAKE_3ARGS_R_MEDIATOR_IMPLEMENTATION(dlib::point, bool, unsigned long, click)
#endif

MAKE_VOID_MEDIATOR_IMPLEMENTATION(void)