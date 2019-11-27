#ifndef _CPP_SERIALIZE_H_
#define _CPP_SERIALIZE_H_

#include "export.h"
#include <dlib/serialize.h>

using namespace dlib;

DLLEXPORT proxy_deserialize* proxy_deserialize_new(const char* file_name)
{
    return new proxy_deserialize(file_name);
}

DLLEXPORT void proxy_deserialize_delete(proxy_deserialize* deserialize)
{
    delete deserialize;
}

DLLEXPORT proxy_serialize* proxy_serialize_new(const char* file_name)
{
    return new proxy_serialize(file_name);
}

DLLEXPORT void proxy_serialize_delete(proxy_serialize* serialize)
{
    delete serialize;
}

#endif