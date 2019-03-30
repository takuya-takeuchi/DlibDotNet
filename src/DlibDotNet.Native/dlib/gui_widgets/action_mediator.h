#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_GUI_WIDGETS_ACTION_MEDIATOR_H_
#define _CPP_GUI_WIDGETS_ACTION_MEDIATOR_H_

#include "../export.h"
#include <dlib/gui_widgets/base_widgets.h>
#include <dlib/gui_widgets/widgets.h>
#include <dlib/gui_core.h>
#include "../shared.h"

#pragma region template

#define MAKE_VOID_MEDIATOR(__TYPENAME__)\
class __TYPENAME__##_action_mediator\
{\
public:\
    __TYPENAME__##_action_mediator(void (*action_handler)());\
    ~__TYPENAME__##_action_mediator();\
    void on_action_handler();\
private:\
    void (*m_action_handler)();\
};\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)());\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator);\

#define MAKE_VOID_MEDIATOR_IMPLEMENTATION(__TYPENAME__)\
__TYPENAME__##_action_mediator::##__TYPENAME__##_action_mediator(void (*action_handler)()): \
    m_action_handler(action_handler)\
{\
}\
\
__TYPENAME__##_action_mediator::~##__TYPENAME__##_action_mediator()\
{\
    this->m_action_handler = nullptr;\
}\
\
void __TYPENAME__##_action_mediator::on_action_handler()\
{\
    if (this->m_action_handler)\
    {\
        this->m_action_handler();\
    }\
}\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)())\
{\
    return new __TYPENAME__##_action_mediator(action_handler);\
}\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator)\
{\
    delete mediator;\
}\

#define MAKE_1ARGS_R_MEDIATOR(__TYPE__, __TYPENAME__)\
class __TYPENAME__##_action_mediator\
{\
public:\
    __TYPENAME__##_action_mediator(void (*action_handler)(__TYPE__*));\
    ~__TYPENAME__##_action_mediator();\
    void on_action_handler(const __TYPE__& value);\
private:\
    void (*m_action_handler)(__TYPE__* value);\
};\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE__* value));\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator);\

#define MAKE_1ARGS_R_MEDIATOR_IMPLEMENTATION(__TYPE__, __TYPENAME__)\
__TYPENAME__##_action_mediator::##__TYPENAME__##_action_mediator(void (*action_handler)(__TYPE__*)): \
    m_action_handler(action_handler)\
{\
}\
\
__TYPENAME__##_action_mediator::~##__TYPENAME__##_action_mediator()\
{\
    this->m_action_handler = nullptr;\
}\
\
void __TYPENAME__##_action_mediator::on_action_handler(const __TYPE__& value)\
{\
    if (this->m_action_handler)\
    {\
        auto tmp = new __TYPE__(value);\
        this->m_action_handler(tmp);\
        delete tmp;\
    }\
}\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE__* value))\
{\
    return new __TYPENAME__##_action_mediator(action_handler);\
}\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator)\
{\
    delete mediator;\
}\

#define MAKE_1ARGS_V_MEDIATOR(__TYPE__, __TYPENAME__)\
class __TYPENAME__##_action_mediator\
{\
public:\
    __TYPENAME__##_action_mediator(void (*action_handler)(__TYPE__));\
    ~__TYPENAME__##_action_mediator();\
    void on_action_handler(__TYPE__ value);\
private:\
    void (*m_action_handler)(__TYPE__ value);\
};\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE__ value));\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator);\

#define MAKE_1ARGS_V_MEDIATOR_IMPLEMENTATION(__TYPE__, __TYPENAME__)\
__TYPENAME__##_action_mediator::##__TYPENAME__##_action_mediator(void (*action_handler)(__TYPE__)): \
    m_action_handler(action_handler)\
{\
}\
\
__TYPENAME__##_action_mediator::~##__TYPENAME__##_action_mediator()\
{\
    this->m_action_handler = nullptr;\
}\
\
void __TYPENAME__##_action_mediator::on_action_handler(__TYPE__ value)\
{\
    if (this->m_action_handler)\
        this->m_action_handler(value);\
}\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE__ value))\
{\
    return new __TYPENAME__##_action_mediator(action_handler);\
}\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator)\
{\
    delete mediator;\
}\

#define MAKE_3ARGS_R_MEDIATOR(__TYPE1__, __TYPE2__, __TYPE3__, __TYPENAME__)\
class __TYPENAME__##_action_mediator\
{\
public:\
    __TYPENAME__##_action_mediator(void (*action_handler)(__TYPE1__*, __TYPE2__, __TYPE3__));\
    ~__TYPENAME__##_action_mediator();\
    void on_action_handler(const __TYPE1__& value1, __TYPE2__ value2, __TYPE3__ value3);\
private:\
    void (*m_action_handler)(__TYPE1__* value1, __TYPE2__ value2, __TYPE3__ value3);\
};\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE1__* value1, __TYPE2__ value2, __TYPE3__ value3));\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator);\

#define MAKE_3ARGS_R_MEDIATOR_IMPLEMENTATION(__TYPE1__, __TYPE2__, __TYPE3__, __TYPENAME__)\
__TYPENAME__##_action_mediator::##__TYPENAME__##_action_mediator(void (*action_handler)(__TYPE1__*, __TYPE2__, __TYPE3__)): \
    m_action_handler(action_handler)\
{\
}\
\
__TYPENAME__##_action_mediator::~##__TYPENAME__##_action_mediator()\
{\
    this->m_action_handler = nullptr;\
}\
\
void __TYPENAME__##_action_mediator::on_action_handler(const __TYPE1__& value1, __TYPE2__ value2, __TYPE3__ value3)\
{\
    if (this->m_action_handler)\
    {\
        auto tmp = new __TYPE1__(value1);\
        this->m_action_handler(tmp, value2, value3);\
        delete tmp;\
    }\
}\
\
DLLEXPORT __TYPENAME__##_action_mediator* __TYPENAME__##_action_mediator_new(void (*action_handler)(__TYPE1__* value1, __TYPE2__ value2, __TYPE3__ value3))\
{\
    return new __TYPENAME__##_action_mediator(action_handler);\
}\
\
DLLEXPORT void __TYPENAME__##_action_mediator_delete(__TYPENAME__##_action_mediator* mediator)\
{\
    delete mediator;\
}\

#pragma endregion template

MAKE_1ARGS_R_MEDIATOR(std::string, string)
MAKE_1ARGS_R_MEDIATOR(dlib::image_display::overlay_rect, image_display_overlay_rect)
MAKE_1ARGS_V_MEDIATOR(unsigned long, uint32t)
MAKE_3ARGS_R_MEDIATOR(dlib::point, bool, unsigned long, click)
MAKE_VOID_MEDIATOR(void)

#endif

#endif