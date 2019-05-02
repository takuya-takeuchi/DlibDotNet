#ifndef _CPP_MATRIX_EXPRESSIONS_H_
#define _CPP_MATRIX_EXPRESSIONS_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_expressions.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT matrix_range_exp<__TYPE__>* matrix_range_exp_create_##__TYPENAME__##_new1(__TYPE__ start, __TYPE__ inc, __TYPE__ end)\
{\
    return new matrix_range_exp<__TYPE__>(start, inc, end);\
}\
\
DLLEXPORT matrix_range_exp<__TYPE__>* matrix_range_exp_create_##__TYPENAME__##_new2(__TYPE__ start, __TYPE__ end, long num)\
{\
    /* true is dummy */ \
    return new matrix_range_exp<__TYPE__>(start, end, num, true);\
}\
\
DLLEXPORT matrix_range_exp<__TYPE__>* matrix_range_exp_create_##__TYPENAME__##_new3(__TYPE__ start, __TYPE__ end)\
{\
    return new matrix_range_exp<__TYPE__>(start, end);\
}\

#define matrix_range_exp_delete_template(__TYPE__, error, __ELEMENT_TYPE__, ...) \
delete ((dlib::matrix_range_exp<__TYPE__>*)matrix);

#define matrix_range_exp_nc_template(__TYPE__, error, __ELEMENT_TYPE__, ...) \
*result = ((dlib::matrix_range_exp<__TYPE__>*)matrix)->nc();

#define matrix_range_exp_nr_template(__TYPE__, error, __ELEMENT_TYPE__, ...) \
*result = ((dlib::matrix_range_exp<__TYPE__>*)matrix)->nr();

#pragma endregion template

DLLEXPORT void matrix_range_exp_delete(matrix_element_type type, void* matrix)
{
    int error = ERR_OK;

    matrix_nosize_template(type,
                           error,
                           matrix_range_exp_delete_template,
                           matrix);
}

MAKE_FUNC(int8_t, int8_t)
MAKE_FUNC(int16_t, int16_t)
MAKE_FUNC(int32_t, int32_t)
MAKE_FUNC(int64_t, int64_t)
MAKE_FUNC(uint8_t, uint8_t)
MAKE_FUNC(uint16_t, uint16_t)
MAKE_FUNC(float, float)
MAKE_FUNC(double, double)
// not support due to std::abs has no proper overloads
// MAKE_FUNC(uint32_t, uint32_t)
// MAKE_FUNC(uint64_t, uint64_t)

DLLEXPORT bool matrix_range_exp_nc(matrix_element_type type, void* matrix, int* result)
{
    int error = ERR_OK;

    matrix_nosize_template(type,
                           error,
                           matrix_range_exp_nc_template,
                           matrix,
                           result);

    return error == ERR_OK;
}

DLLEXPORT bool matrix_range_exp_nr(matrix_element_type type, void* matrix, int* result)
{
    int error = ERR_OK;

    matrix_nosize_template(type,
                           error,
                           matrix_range_exp_nr_template,
                           matrix,
                           result);

    return error == ERR_OK;
}

#endif