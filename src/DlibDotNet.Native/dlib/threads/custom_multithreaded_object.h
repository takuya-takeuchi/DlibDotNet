#ifndef _CPP_THREADS_CUSTOM_MULTITHREADED_OBJECT_EXTENSION_H_
#define _CPP_THREADS_CUSTOM_MULTITHREADED_OBJECT_EXTENSION_H_

#include "../export.h"
#include <dlib/threads/multithreaded_object_extension.h>
#include "../shared.h"

#include "../gui_widgets/action_mediator.h"

class custom_multithreaded_object : dlib::multithreaded_object
{
public:
    custom_multithreaded_object();
    ~custom_multithreaded_object();

    void register_thread_(void_action_mediator* mediator);
};

DLLEXPORT custom_multithreaded_object* custom_multithreaded_object_new()
{
    return new custom_multithreaded_object();
}

DLLEXPORT void custom_multithreaded_object_delete(custom_multithreaded_object* thread)
{
    delete thread;
}

DLLEXPORT void custom_multithreaded_object_register_thread(custom_multithreaded_object* thread, void_action_mediator* mediator)
{
    thread->register_thread_(mediator);
}

#endif