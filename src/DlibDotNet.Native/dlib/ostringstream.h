#ifndef _CPP_OSTRINGSTREAM_H_
#define _CPP_OSTRINGSTREAM_H_

#include "export.h"
#include <stdlib.h>
#include <string.h>
#include <sstream>

#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>

DLLEXPORT std::ostringstream* ostringstream_new()
{
    return new std::ostringstream();
}

DLLEXPORT std::string* ostringstream_str(std::ostringstream *ptr)
{
    return new std::string(ptr->str().c_str());
}

DLLEXPORT void ostringstream_delete(std::ostringstream* ptr)
{
    delete ptr;
}

#endif