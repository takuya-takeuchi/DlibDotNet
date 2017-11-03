#ifndef _CPP_STRING_H_
#define _CPP_STRING_H_

#include "export.h"
#include <string>
#include <stdlib.h>

DLLEXPORT std::string* string_new()
{
    return new std::string;
}

DLLEXPORT void string_append(std::string* str, const char* c_str, int len)
{
    str->append(c_str, len);
}

DLLEXPORT const char* string_c_str(std::string* str)
{
    return str->c_str();
}

DLLEXPORT void string_delete(std::string* str)
{
    delete str;
}

#endif