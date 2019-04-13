#ifndef _CPP_MISC_API_MISC_API_KERNEL_1_H_
#define _CPP_MISC_API_MISC_API_KERNEL_1_H_

#include "../export.h"
#include <dlib/misc_api.h>
#include "../shared.h"

DLLEXPORT void sleep(unsigned long milliseconds)
{
    dlib::sleep(milliseconds);
}

#endif