#ifndef _CPP_STRING_STRING_H_
#define _CPP_STRING_STRING_H_

#include "../export.h"
#include <dlib/string/string.h>
#include "../shared.h"

DLLEXPORT std::string* wrap_string_char(const char* str,
                                        const int str_length,
                                        const unsigned long first_pad = 0,
                                        const unsigned long rest_pad = 0,
                                        const unsigned long max_per_line = 79)
{
    std::basic_string<char> ret = dlib::wrap_string(std::string(str, str_length), first_pad, rest_pad, max_per_line);
    return new std::string(ret);
}

#endif