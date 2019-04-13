#ifndef _CPP_PIPE_TEMPLATE_H_
#define _CPP_PIPE_TEMPLATE_H_

#include "../export.h"
#include "../shared.h"

struct generic_element
{
    void* ptr;
};

#define MAKE_GENERIC_FUNC()\
DLLEXPORT dlib::pipe<generic_element>* pipe_generic_new(unsigned long long maxsize)\
{\
    return new dlib::pipe<generic_element>(maxsize);\
}\
DLLEXPORT void pipe_generic_delete(dlib::pipe<generic_element>* p)\
{\
    delete p;\
}\
DLLEXPORT void pipe_generic_enqueue(dlib::pipe<generic_element>* p, void* e)\
{\
    generic_element g;\
    g.ptr = e;\
    p->enqueue(g);\
}\
DLLEXPORT bool pipe_generic_dequeue(dlib::pipe<generic_element>* p, void** e)\
{\
    generic_element tmp;\
    auto b = p->dequeue(tmp);\
    *e = tmp.ptr;\
    return b;\
}\
DLLEXPORT void pipe_generic_disable(dlib::pipe<generic_element>* p)\
{\
    p->disable();\
}\
DLLEXPORT bool pipe_generic_is_enabled(dlib::pipe<generic_element>* p)\
{\
    return p->is_enabled();\
}\
DLLEXPORT void pipe_generic_wait_until_empty(dlib::pipe<generic_element>* p)\
{\
    p->wait_until_empty();\
}\

#endif