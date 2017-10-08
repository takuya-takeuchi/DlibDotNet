#ifndef _CPP_STDLIB_H_
#define _CPP_STDLIB_H_

#include <stdlib.h>

extern "C" __declspec(dllexport) void* stdlib_malloc(size_t size)
{
    return malloc(size);
}

extern "C" __declspec(dllexport) void stdlib_free(void *ptr)
{
    free(ptr);
}

#endif