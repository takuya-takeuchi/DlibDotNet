#ifndef _CPP_CONFIG_H_
#define _CPP_CONFIG_H_

#include "../../DlibDotNet.Native/dlib/export.h"
#include <iostream>
#include <string>
#include <sstream>

#define PROJECT_NAME  "DlibDotNetNativeDnn"
#define VERSION_MAJOR "19"
#define VERSION_MINOR "17"
#define VERSION_PATCH "0"
#define VERSION_DATE  "20190513"

DLLEXPORT std::string* get_version()
{
    std::stringstream ss;
    ss << VERSION_MAJOR << "." << VERSION_MINOR << "." << VERSION_PATCH << "." << VERSION_DATE;
    return new std::string(ss.str());
}

#endif
