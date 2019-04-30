#ifndef _CPP_LOAD_IMAGE_H_
#define _CPP_LOAD_IMAGE_H_

#include "../export.h"
#include <dlib/image_io.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;
using namespace std;

#pragma region template

#define load_image_template(__TYPE__, error, type, ...) \
dlib::load_image(*((array2d<__TYPE__>*)image), file_name);

#define load_image_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp = new dlib::matrix<__TYPE__>();\
dlib::load_image(*tmp, file_name);\
*matrix = tmp;

#pragma endregion template

DLLEXPORT int load_image(array2d_type type, void* image, const char* file_name, std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        array2d_template(type,
                         error,
                         load_image_template,
                         image,
                         file_name);
    }
    catch(dlib::image_load_error& e)
    {
        error = ERR_GENERAL_IMAGE_LOAD;
        *error_message = new std::string(e.what());
    }

    return error;
}

DLLEXPORT int load_image_matrix(matrix_element_type type, const char* file_name, void** matrix, std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        // not support paratmeter rows and columns
        matrix_template(type,
                        error,
                        matrix_template_size_template,
                        load_image_matrix_template,
                        0,
                        0,
                        matrix,
                        file_name);
    }
    catch(dlib::image_load_error& e)
    {
        error = ERR_GENERAL_IMAGE_LOAD;
        *error_message = new std::string(e.what());
    }

    return error;
}

#endif