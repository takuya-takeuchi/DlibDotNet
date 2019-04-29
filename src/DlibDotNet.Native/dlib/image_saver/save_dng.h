#ifndef _CPP_SAVE_DNG_H_
#define _CPP_SAVE_DNG_H_

#include "../export.h"
#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/image_saver/image_saver.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define save_dng_array2d_template(__TYPE__, error, type, ...) \
dlib::save_dng(*((array2d<__TYPE__>*)image), file_name);

#define save_dng_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
dlib::save_dng(mat, file_name);\

#pragma endregion template

DLLEXPORT int save_dng(array2d_type type, void* image, const char* file_name)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     save_dng_array2d_template,
                     image,
                     file_name);

    return error;
}

DLLEXPORT int save_dng_matrix(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, const char* file_name)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    save_dng_matrix_template,
                    templateRows,
                    templateColumns,
                    matrix,
                    file_name);

    return error;
}

#endif