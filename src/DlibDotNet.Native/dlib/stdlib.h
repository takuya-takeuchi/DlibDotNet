#ifndef _CPP_STDLIB_H_
#define _CPP_STDLIB_H_

#include "export.h"
#include <stdlib.h>

DLLEXPORT void* stdlib_malloc(size_t size)
{
    return malloc(size);
}

DLLEXPORT void stdlib_free(void *ptr)
{
    free(ptr);
}

#endif