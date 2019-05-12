#ifndef _CPP_MATRIX_OP_OPARRAY2DTOMAT_H_
#define _CPP_MATRIX_OP_OPARRAY2DTOMAT_H_

#include "../../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_op_op_array2d_to_mat_delete_template(__TYPE__, error, type, ...) \
auto mat = static_cast<matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>*>(obj);\
delete mat;\

#define matrix_op_op_array2d_to_mat_nc_template(__TYPE__, error, type, ...) \
*ret = ((matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>*)obj)->nc();\

#define matrix_op_op_array2d_to_mat_nr_template(__TYPE__, error, type, ...) \
*ret = ((matrix_op<op_array2d_to_mat<array2d<__TYPE__>>>*)obj)->nr();\

#pragma endregion template

DLLEXPORT void matrix_op_op_array2d_to_mat_delete(array2d_type type, void* obj)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_array2d_to_mat_delete_template,
                     obj);
}

DLLEXPORT int matrix_op_op_array2d_to_mat_nc(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_array2d_to_mat_nc_template,
                     obj,
                     ret);

    return error;
}

DLLEXPORT int matrix_op_op_array2d_to_mat_nr(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_array2d_to_mat_nr_template,
                     obj,
                     ret);

    return error;
}

#endif