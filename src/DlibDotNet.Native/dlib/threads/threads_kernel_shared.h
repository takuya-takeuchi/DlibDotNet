#ifndef _CPP_THREADS_THREADS_KERNEL_SHARED_H_
#define _CPP_THREADS_THREADS_KERNEL_SHARED_H_

#include "../export.h"
#include <dlib/threads/threads_kernel_shared.h>
#include "../shared.h"

DLLEXPORT bool create_new_thread(void (*funct)(void*), void* param)
{
    return dlib::create_new_thread(funct, param);
}

#endif