#ifndef _CPP_SERIALIZE_H_
#define _CPP_SERIALIZE_H_

#include <dlib/serialize.h>

using namespace dlib;

extern "C" __declspec(dllexport) void deserialize(const char* file_name, std::istream* in)
{
    //dlib::deserialize(*file_name, *in);
}

extern "C" __declspec(dllexport) void serialize(const char* file_name, std::ostream* out)
{
    //dlib::serialize(*file_name, *out);
}

#endif