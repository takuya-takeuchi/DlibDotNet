#ifndef _CPP_SERIALIZE_H_
#define _CPP_SERIALIZE_H_

#include "export.h"
#include <dlib/serialize.h>

using namespace dlib;

DLLEXPORT void deserialize(const char* file_name, std::istream* in)
{
    //dlib::deserialize(*file_name, *in);
}

DLLEXPORT void serialize(const char* file_name, std::ostream* out)
{
    //dlib::serialize(*file_name, *out);
}

#endif