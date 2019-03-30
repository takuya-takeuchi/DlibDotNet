#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_CUSTOM_DRAWABLE_WINDOW_H_
#define _CPP_CUSTOM_DRAWABLE_WINDOW_H_

#include "../export.h"
#include <dlib/gui_widgets/drawable.h>
#include <dlib/gui_core.h>
#include "../shared.h"

class custom_drawable_window : dlib::drawable_window
{
public:
    custom_drawable_window(const bool resizable,
                           const bool undecorated,
                           void (*constructor_function)(custom_drawable_window*) = nullptr,
                           void (*destructor_function)() = nullptr,
                           void (*on_window_resized_function)() = nullptr,
                           void (*on_keydown_function)(unsigned long, bool, unsigned long) = nullptr);
    ~custom_drawable_window();

protected:
    virtual void on_window_resized();
    virtual void on_keydown(unsigned long key,
                            bool is_printable,
                            unsigned long state);

private:
    void (*m_constructor_function)(custom_drawable_window*);
    void (*m_destructor_function)();
    void (*m_on_window_resized_function)();
    void (*m_on_keydown_function)(unsigned long, bool, unsigned long);
};

DLLEXPORT custom_drawable_window* custom_drawable_window_new(const bool resizable,
                                                             const bool undecorated,
                                                             void (*constructor_function)(custom_drawable_window*),
                                                             void (*destructor_function)(),
                                                             void (*on_window_resized_function)(),
                                                             void (*on_keydown_function)(unsigned long, bool, unsigned long))
{
    return new custom_drawable_window(resizable,
                                      undecorated,
                                      constructor_function,
                                      destructor_function,
                                      on_window_resized_function,
                                      on_keydown_function);
}

DLLEXPORT void custom_drawable_window_delete(custom_drawable_window* window)
{
    delete window;;
}

#endif

#endif