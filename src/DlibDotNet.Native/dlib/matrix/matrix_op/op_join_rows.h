#ifndef _CPP_MATRIX_OP_OPJOINROWS_H_
#define _CPP_MATRIX_OP_OPJOINROWS_H_

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

#define matrix_op_op_join_rows_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto mat= ((matrix_op<op_join_rows<matrix<__TYPE__, __ROWS__, __COLUMNS__>, matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)obj);\
delete mat;\

#define matrix_op_op_join_rows_nr_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto mat= ((matrix_op<op_join_rows<matrix<__TYPE__, __ROWS__, __COLUMNS__>, matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)obj);\
*ret = mat->nr();\

#define matrix_op_op_join_rows_nc_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto mat= ((matrix_op<op_join_rows<matrix<__TYPE__, __ROWS__, __COLUMNS__>, matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)obj);\
*ret = mat->nc();\

#pragma endregion template

DLLEXPORT void matrix_op_op_join_rows_delete(matrix_element_type type, void* obj, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_op_op_join_rows_delete_template,
                    templateRows,
                    templateColumns,
                    obj);
}

DLLEXPORT int matrix_op_op_join_rows_nc(matrix_element_type type, void* obj, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_op_op_join_rows_nc_template,
                    templateRows,
                    templateColumns,
                    obj,
                    ret);

    return error;
}

DLLEXPORT int matrix_op_op_join_rows_nr(matrix_element_type type, void* obj, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    matrix_op_op_join_rows_nr_template,
                    templateRows,
                    templateColumns,
                    obj,
                    ret);

    return error;
}

#endif