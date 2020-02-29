#ifndef _CPP_STDLIB_H_
#define _CPP_STDLIB_H_

#include "export.h"
#include <stdlib.h>
#include <cstdint>

DLLEXPORT void* stdlib_malloc(size_t size)
{
    return malloc(size);
}

DLLEXPORT void stdlib_free(void *ptr)
{
    free(ptr);
}

DLLEXPORT void stdlib_srand(uint32_t seed)
{
    srand(seed);
}

#endif