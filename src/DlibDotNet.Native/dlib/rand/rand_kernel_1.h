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

DLLEXPORT dlib::rand* rand_new2(time_t seed)
{
    return new dlib::rand(seed);
}

DLLEXPORT long long rand_get_integer_in_range(dlib::rand* obj, long long begin, long long end)
{
    return obj->get_integer_in_range(begin, end);
}

DLLEXPORT double rand_get_random_gaussian(dlib::rand* obj)
{
    return obj->get_random_gaussian();
}

DLLEXPORT double rand_get_random_double(dlib::rand* obj)
{
    return obj->get_random_double();
}

DLLEXPORT unsigned char rand_get_random_8bit_number(dlib::rand* obj)
{
    return obj->get_random_8bit_number();
}

DLLEXPORT uint32_t rand_get_random_32bit_number(dlib::rand* obj)
{
    return obj->get_random_32bit_number();
}

DLLEXPORT void rand_delete(dlib::rand* obj)
{
    delete obj;
}

#endif