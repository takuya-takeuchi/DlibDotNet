#ifndef _CPP_LOAD_BMP_H_
#define _CPP_LOAD_BMP_H_

#include "../export.h"
#include <dlib/image_io.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;
using namespace std;

#pragma region template

#define load_bmp_template(__TYPE__, error, type, ...) \
dlib::load_bmp(*((array2d<__TYPE__>*)image), std::string(file_name, file_name_length));

#pragma endregion template

DLLEXPORT int load_bmp(array2d_type type,
                       void* image,
                       const char* file_name,
                       const int file_name_length,
                       std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        array2d_template(type,
                         error,
                         load_bmp_template,
                         image,
                         file_name,
                         file_name_length);
    }
    catch(dlib::image_load_error& e)
    {
        error = ERR_GENERAL_IMAGE_LOAD;
        *error_message = new std::string(e.what());
    }

    return error;
}

#endif