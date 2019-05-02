#ifndef _CPP_MATRIX_OP_OPJET_H_
#define _CPP_MATRIX_OP_OPJET_H_

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

#define matrix_op_op_jet_delete_template(__TYPE__, error, type, ...) \
auto mat = static_cast<matrix_op<op_jet<array2d<__TYPE__>>>*>(obj);\
delete mat;\

#define matrix_op_op_jet_nc_template(__TYPE__, error, type, ...) \
*ret = ((matrix_op<op_jet<array2d<__TYPE__>>>*)obj)->nc();\

#define matrix_op_op_jet_nr_template(__TYPE__, error, type, ...) \
*ret = ((matrix_op<op_jet<array2d<__TYPE__>>>*)obj)->nr();\

#define matrix_op_op_jet_nc_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto op = static_cast<matrix_op<op_jet<matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(img);\
*ret = op->nc();\

#define matrix_op_op_jet_nr_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto op = static_cast<matrix_op<op_jet<matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(img);\
*ret = op->nr();\

#define matrix_op_op_jet_operator_template(__TYPE__, error, type, ...) \
*ret = (*((matrix_op<op_jet<array2d<__TYPE__>>>*)obj))(r, c);\

#pragma endregion template

DLLEXPORT void matrix_op_op_jet_delete(array2d_type type, void* obj)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_jet_delete_template,
                     obj);
}

DLLEXPORT int matrix_op_op_jet_nc(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_jet_nc_template,
                     obj,
                     ret);

    return error;
}

DLLEXPORT int matrix_op_op_jet_nc_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_op_op_jet_nc_matrix_template,
                    templateRows,
                    templateColumns,
                    img,
                    ret);

    return error;
}

DLLEXPORT int matrix_op_op_jet_nr(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_jet_nr_template,
                     obj,
                     ret);

    return error;
}

DLLEXPORT int matrix_op_op_jet_nr_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_op_op_jet_nr_matrix_template,
                    templateRows,
                    templateColumns,
                    img,
                    ret);

    return error;
}

#pragma region operator

DLLEXPORT int matrix_op_op_jet_operator(array2d_type type, void* obj, int r, int c, rgb_pixel* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_jet_operator_template,
                     obj,
                     r,
                     c,
                     ret);

    return error;
}

#pragma endregion oprator

#endif