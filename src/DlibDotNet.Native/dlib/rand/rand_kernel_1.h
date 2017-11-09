#ifndef _CPP_RAND_KERNEL_1_H_
#define _CPP_RAND_KERNEL_1_H_

#include "../export.h"
#include <dlib/rand/rand_kernel_1.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT dlib::rand* rand_new()
{
    return new dlib::rand();
}

DLLEXPORT double rand_get_random_gaussian(dlib::rand* obj)
{
    return obj->get_random_gaussian();
}

DLLEXPORT void rand_delete(dlib::rand* obj)
{
    delete obj;
}

#endif