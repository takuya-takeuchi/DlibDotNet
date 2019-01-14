#ifndef _CPP_CSTD_STRING_H_
#define _CPP_CSTD_STRING_H_

#include "../export.h"
#include <string.h>

DLLEXPORT void* cstd_memcpy(void* buf1, const void* buf2, size_t n)
{
    return memcpy(buf1, buf2, n);
}

#endif