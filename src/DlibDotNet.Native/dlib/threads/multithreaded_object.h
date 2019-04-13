#ifndef _CPP_THREADS_MULTITHREADED_OBJECT_H_
#define _CPP_THREADS_MULTITHREADED_OBJECT_H_

#include "../export.h"
#include <dlib/threads/multithreaded_object_extension.h>
#include "../shared.h"

DLLEXPORT void multithreaded_object_wait(dlib::multithreaded_object* thread)
{
    thread->wait();
}

DLLEXPORT void multithreaded_object_start(dlib::multithreaded_object* thread)
{
    thread->start();
}

DLLEXPORT void multithreaded_object_stop(dlib::multithreaded_object* thread)
{
    thread->stop();
}

DLLEXPORT void multithreaded_object_pause(dlib::multithreaded_object* thread)
{
    thread->pause();
}

#endif