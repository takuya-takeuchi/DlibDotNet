#ifndef _CPP_ARRAY2D_H_
#define _CPP_ARRAY2D_H_

#include "../export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;

#pragma region template

#define array2d_new_template(__TYPE__, error, type, ...) \
ret = new dlib::array2d<__TYPE__>();

#define array2d_new1_template(__TYPE__, error, type, ...) \
ret = new dlib::array2d<__TYPE__>(rows, cols);

#define array2d_data_raw_template(__TYPE__, error, type, ...) \
ret = &(((array2d<__TYPE__>*)array)->[0][0]);

#define array2d_delete_template(__TYPE__, error, type, ...) \
delete ((dlib::array2d<__TYPE__>*)array);

#define array2d_nc_template(__TYPE__, error, type, ...) \
*ret = ((array2d<__TYPE__>*)array)->nc();\
return true;

#define array2d_nr_template(__TYPE__, error, type, ...) \
*ret = ((array2d<__TYPE__>*)array)->nr();\
return true;

#define array2d_size_template(__TYPE__, error, type, ...) \
*ret = ((array2d<__TYPE__>*)array)->size();\
return true;

#define array2d_set_size_template(__TYPE__, error, type, ...) \
((array2d<__TYPE__>*)array)->set_size(rows, cols);\
return true;

#define array2d_row_template(__TYPE__, error, type, ...) \
auto tmp = static_cast<dlib::array2d<__TYPE__>*>(array);\
auto r = (*tmp)[row];\
*ret = new dlib::array2d<__TYPE__>::row(r);

#define array2d_row_delete_template(__TYPE__, error, type, ...) \
auto tmp = static_cast<dlib::array2d<__TYPE__>::row*>(row);\
delete tmp;

#define array2d_matrix_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>();

#define array2d_matrix_new1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(rows, cols);

#define array2d_matrix_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
delete ((dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array);

#define array2d_matrix_nc_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array)->nc();

#define array2d_matrix_nr_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array)->nr();

#define array2d_matrix_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*ret = ((dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array)->size();

#define array2d_matrix_set_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
((dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array)->set_size(rows, cols);

#define array2d_matrix_get_rect_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
*rect = new dlib::rectangle(get_rect(*((array2d<matrix<__TYPE__, __ROWS__, __COLUMNS__>>*)array)));

#define array2d_matrix_row_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp = *(static_cast<dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(array));\
auto r = tmp[row];\
*ret = new dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>::row(r);

#define array2d_matrix_row_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp = static_cast<dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>::row*>(row);\
delete tmp;\

#define MAKE_ROWCOLUMN_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT void array2d_get_row_column_##__TYPENAME__(void* row, int32_t column, __TYPE__* ret)\
{\
    dlib::array2d<__TYPE__>::row& tmp = *(static_cast<dlib::array2d<__TYPE__>::row*>(row));\
    *((__TYPE__*)ret) = tmp[column];\
}\
\
DLLEXPORT void array2d_set_row_column_##__TYPENAME__(void* row, int32_t column, __TYPE__ ret)\
{\
    dlib::array2d<__TYPE__>::row& tmp = *(static_cast<dlib::array2d<__TYPE__>::row*>(row));\
    tmp[column] = ret;\
}

#define array2d_matrix_get_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& tmp = *(static_cast<dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>::row*>(row));\
*ret = tmp[column];\

#define array2d_matrix_set_row_column_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& tmp = *(static_cast<dlib::array2d<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>::row*>(row));\
tmp[column] = (dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)ret;\

#define MAKE_ROWCOLUMN_MATRIX_FUNC(__TYPE__, __TYPENAME__, __ELEMENT_TYPE__)\
DLLEXPORT void array2d_matrix_get_row_column_##__TYPENAME__(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)\
{\
    int error = ERR_OK;\
    matrix_template_size_template(__TYPE__,\
                                  error,\
                                  __ELEMENT_TYPE__,\
                                  array2d_matrix_get_row_column_template,\
                                  templateRows,\
                                  templateColumns,\
                                  row,\
                                  column,\
                                  ret);\
}\
\
DLLEXPORT void array2d_matrix_set_row_column_##__TYPENAME__(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)\
{\
    int error = ERR_OK;\
    matrix_template_size_template(__TYPE__,\
                                  error,\
                                  __ELEMENT_TYPE__,\
                                  array2d_matrix_set_row_column_template,\
                                  templateRows,\
                                  templateColumns,\
                                  row,\
                                  column,\
                                  ret);\
}

#pragma endregion template

#pragma region array2d

DLLEXPORT void* array2d_new(array2d_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array2d_new_template,
                     ret);

    return ret;
}

DLLEXPORT void* array2d_new1(array2d_type type, int rows, int cols)
{
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array2d_new1_template,
                     rows,
                     cols,
                     ret);

    return ret;
}

DLLEXPORT void* array2d_data_raw(array2d_type type, void* array) {
    int error = ERR_OK;
    void* ret = nullptr;

    array2d_template(type,
                     error,
                     array2d_data_raw_template,
                     array,
                     ret);

    return ret;
}

DLLEXPORT bool array2d_nc(array2d_type type, void* array, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_nc_template,
                     array,
                     ret);

    return false;
}

DLLEXPORT bool array2d_nr(array2d_type type, void* array, int* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_nr_template,
                     array,
                     ret);

    return false;
}

DLLEXPORT bool array2d_size(array2d_type type, void* array, uint64_t* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_size_template,
                     array,
                     ret);

    return false;
}

DLLEXPORT bool array2d_set_size(array2d_type type, void* array, const int rows, const int cols)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_set_size_template,
                     rows,
                     cols);

    return false;
}

DLLEXPORT void array2d_delete(array2d_type type, void* array)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_delete_template,
                     array);
}

#pragma endregion array2d

#pragma region row

DLLEXPORT int array2d_row(array2d_type type, void* array, int32_t row, void** ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_row_template,
                     row,
                     ret);

    return error;
}

#pragma region array2d_row_column

MAKE_ROWCOLUMN_FUNC(int8_t, int8_t)
MAKE_ROWCOLUMN_FUNC(int16_t, int16_t)
MAKE_ROWCOLUMN_FUNC(int32_t, int32_t)
MAKE_ROWCOLUMN_FUNC(uint8_t, uint8_t)
MAKE_ROWCOLUMN_FUNC(uint16_t, uint16_t)
MAKE_ROWCOLUMN_FUNC(uint32_t, uint32_t)
MAKE_ROWCOLUMN_FUNC(float, float)
MAKE_ROWCOLUMN_FUNC(double, double)
MAKE_ROWCOLUMN_FUNC(rgb_pixel, rgb_pixel)
MAKE_ROWCOLUMN_FUNC(bgr_pixel, bgr_pixel)
MAKE_ROWCOLUMN_FUNC(hsi_pixel, hsi_pixel)
MAKE_ROWCOLUMN_FUNC(rgb_alpha_pixel, rgb_alpha_pixel)

#pragma endregion array2d_row_column

DLLEXPORT int array2d_row_delete(array2d_type type, void* row)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     array2d_row_delete_template,
                     row);

    return error;
}

#pragma endregion row

#pragma region matrix

DLLEXPORT void* array2d_matrix_new(matrix_element_type type, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_new_template,
                    templateRows,
                    templateColumns,
                    ret);

    return ret;
}

DLLEXPORT void* array2d_matrix_new1(matrix_element_type type, const int rows, const int cols, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_new1_template,
                    templateRows,
                    templateColumns,
                    rows,
                    cols,
                    ret);

    return ret;
}

DLLEXPORT bool array2d_matrix_nc(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_nc_template,
                    templateRows,
                    templateColumns,
                    array,
                    ret);

    return error == ERR_OK;
}

DLLEXPORT bool array2d_matrix_nr(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_nr_template,
                    templateRows,
                    templateColumns,
                    array,
                    ret);

    return error == ERR_OK;
}

DLLEXPORT bool array2d_matrix_size(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_size_template,
                    templateRows,
                    templateColumns,
                    array,
                    ret);

    return error == ERR_OK;
}

DLLEXPORT bool array2d_matrix_set_size(matrix_element_type type, void* array, const int templateRows, const int templateColumns, const int rows, const int cols)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_set_size_template,
                    templateRows,
                    templateColumns,
                    array,
                    rows,
                    cols);

    return error == ERR_OK;
}

DLLEXPORT void array2d_matrix_delete(matrix_element_type type, void* array, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_delete_template,
                    templateRows,
                    templateColumns,
                    array);
}

DLLEXPORT int array2d_matrix_get_rect(matrix_element_type type, void* array, const int templateRows, const int templateColumns, rectangle** rect)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_get_rect_template,
                    templateRows,
                    templateColumns,
                    array,
                    rect);

    return error;
}

#pragma endregion matrix

#pragma region matrix row

DLLEXPORT int array2d_matrix_row(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int32_t row, void** ret)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_row_template,
                    templateRows,
                    templateColumns,
                    array,
                    row,
                    ret);

    return error;
}

#pragma region array2d_get_row_column

MAKE_ROWCOLUMN_MATRIX_FUNC(int8_t, int8_t, matrix_element_type::Int8)
MAKE_ROWCOLUMN_MATRIX_FUNC(int16_t, int16_t, matrix_element_type::Int16)
MAKE_ROWCOLUMN_MATRIX_FUNC(int32_t, int32_t, matrix_element_type::Int32)
MAKE_ROWCOLUMN_MATRIX_FUNC(uint8_t, uint8_t, matrix_element_type::UInt8)
MAKE_ROWCOLUMN_MATRIX_FUNC(uint16_t, uint16_t, matrix_element_type::UInt16)
MAKE_ROWCOLUMN_MATRIX_FUNC(uint32_t, uint32_t, matrix_element_type::UInt32)
MAKE_ROWCOLUMN_MATRIX_FUNC(float, float, matrix_element_type::Float)
MAKE_ROWCOLUMN_MATRIX_FUNC(double, double, matrix_element_type::Double)
MAKE_ROWCOLUMN_MATRIX_FUNC(rgb_pixel, rgb_pixel, matrix_element_type::RgbPixel)
MAKE_ROWCOLUMN_MATRIX_FUNC(bgr_pixel, bgr_pixel, matrix_element_type::BgrPixel)
MAKE_ROWCOLUMN_MATRIX_FUNC(hsi_pixel, hsi_pixel, matrix_element_type::HsiPixel)
MAKE_ROWCOLUMN_MATRIX_FUNC(rgb_alpha_pixel, rgb_alpha_pixel, matrix_element_type::RgbAlphaPixel)

#pragma endregion array2d_get_row_column

DLLEXPORT void array2d_matrix_row_delete(matrix_element_type type, void* row, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    array2d_matrix_row_delete_template,
                    templateRows,
                    templateColumns,
                    row);
}

#pragma endregion matrix row

#endif