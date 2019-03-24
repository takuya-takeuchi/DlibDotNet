#include "custom_drawable_window.h"

custom_drawable_window::custom_drawable_window(const bool resizable,
                                               const bool undecorated,
                                               void (*constructor_function)(custom_drawable_window*),
                                               void (*destructor_function)(),
                                               void (*on_window_resized_function)(),
                                               void (*on_keydown_function)(unsigned long, bool, unsigned long)):
    drawable_window(resizable, undecorated),
    m_constructor_function(constructor_function),
    m_destructor_function(destructor_function),
    m_on_window_resized_function(on_window_resized_function),
    m_on_keydown_function(on_keydown_function)
{
    if (this->m_constructor_function)
        this->m_constructor_function(this);
}

custom_drawable_window::~custom_drawable_window()
{
    if (this->m_destructor_function)
        this->m_destructor_function();
}

void custom_drawable_window::on_window_resized()
{
    if (this->m_on_window_resized_function)
        this->m_on_window_resized_function();
}

void custom_drawable_window::on_keydown(unsigned long key,
                                        bool is_printable,
                                        unsigned long state)
{
    if (this->m_on_keydown_function)
        this->m_on_keydown_function(key, is_printable, state);
}