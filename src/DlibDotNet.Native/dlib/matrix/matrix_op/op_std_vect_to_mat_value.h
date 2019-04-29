#ifndef _CPP_MATRIX_OP_OPSTDVECTTOMAT_VALUE_H_
#define _CPP_MATRIX_OP_OPSTDVECTTOMAT_VALUE_H_

#include "../../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define matrix_op_op_std_vect_to_mat_value_delete_template(__TYPE__, error, type, ...) \
delete ((dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>>*)obj);

#define matrix_op_op_std_vect_to_mat_value_nc_template(__TYPE__, error, type, ...) \
*ret = ((dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>>*)obj)->nc();

#define matrix_op_op_std_vect_to_mat_value_nr_template(__TYPE__, error, type, ...) \
*ret = ((dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>>*)obj)->nr();

#define matrix_op_op_std_vect_to_mat_value_operator_left_shift_template(__TYPE__, error, type, ...) \
auto& mat = *static_cast<dlib::matrix_op<dlib::op_std_vect_to_mat<std::vector<__TYPE__>>>*>(obj);\
*stream << mat;\

#pragma endregion template

DLLEXPORT void matrix_op_op_std_vect_to_mat_value_delete(array2d_type type, void* obj)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_std_vect_to_mat_value_delete_template);
}

DLLEXPORT int matrix_op_op_std_vect_to_mat_value_nc(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_std_vect_to_mat_value_nc_template,
                     ret);

    return error;
}

DLLEXPORT int matrix_op_op_std_vect_to_mat_value_nr(array2d_type type, void* obj, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     matrix_op_op_std_vect_to_mat_value_nr_template,
                     ret);

    return error;
}

#pragma region operator

DLLEXPORT int matrix_op_op_std_vect_to_mat_value_operator_left_shift(array2d_type type, void* obj, std::ostringstream* stream)
{
    int error = ERR_OK;

    array2d_numeric_template(type,
                             error,
                             matrix_op_op_std_vect_to_mat_value_operator_left_shift_template,
                             stream);

    return error;
}

#pragma endregion oprator

#endif