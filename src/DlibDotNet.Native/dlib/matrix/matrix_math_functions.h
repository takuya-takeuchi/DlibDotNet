#ifndef _CPP_MATRIX_MATH_FUNCTIONS_H_
#define _CPP_MATRIX_MATH_FUNCTIONS_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_math_functions.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_round_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__> m = dlib::round(mat);\
*ret = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(m);\

#pragma endregion template

DLLEXPORT int matrix_round(matrix_element_type type, void* matrix, int templateRows, int templateColumns, void** ret)
{
    int error = ERR_OK;
    *ret = nullptr;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            matrix_round_template,
                            templateRows,
                            templateColumns,
                            matrix,
                            ret);

    return error;
}

#endif