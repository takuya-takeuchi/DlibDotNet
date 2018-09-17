#ifndef _CPP_MATRIX_H_
#define _CPP_MATRIX_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>
#include "../shared.h"

#include "matrix_common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define OPERAND +
#undef OPERAND

#define matrix_new2_template(num_rows, num_cols, src) \
do { \
    auto m = new matrix<ELEMENT>(num_rows, num_cols);\
    auto &d = *m;\
    ELEMENT* s = static_cast<ELEMENT*>(src);\
    for (int32_t r = 0; r < num_rows; r++)\
    for (int32_t c = 0, step = r * num_cols; c < num_cols; c++)\
        d(r, c) = s[step + c];\
    return m;\
} while (0)

#define matrix_operator_array_template(matrix, array) \
do { \
    dlib::matrix<ELEMENT>& mat = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
    ELEMENT* src = static_cast<ELEMENT*>(array);\
    const long row = mat.nr();\
    const long column = mat.nc();\
    for (long r = 0; r < row; ++r)\
        for (long c = 0; c < column; ++c)\
            mat(r, c) = src[r * column + c];\
} while (0)

// ToDo:
// // You are trying to multiply two incompatible matrices together.  The number of columns 
//             // in the matrix on the left must match the number of rows in the matrix on the right.
//             COMPILE_TIME_ASSERT(LHS::NC == RHS::NR || LHS::NC*RHS::NR == 0);
//             DLIB_ASSERT(lhs.nc() == rhs.nr() && lhs.size() > 0 && rhs.size() > 0, 
//                 "\tconst matrix_exp operator*(const matrix_exp& lhs, const matrix_exp& rhs)"
//                 << "\n\tYou are trying to multiply two incompatible matrices together"
//                 << "\n\tlhs.nr(): " << lhs.nr()
//                 << "\n\tlhs.nc(): " << lhs.nc()
//                 << "\n\trhs.nr(): " << rhs.nr()
//                 << "\n\trhs.nc(): " << rhs.nc()
//                 << "\n\t&lhs: " << &lhs 
//                 << "\n\t&rhs: " << &rhs 
//                 );
#define matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret) \
do {\
    dlib::matrix<ELEMENT>* left = nullptr;\
    dlib::matrix<ELEMENT>* right = nullptr;\
    matrix_cast_without_type_parameter_template(lhs,\
                                                rhs,\
                                                leftTemplateRows,\
                                                leftTemplateColumns,\
                                                rightTemplateRows,\
                                                rightTemplateColumns,\
                                                left,\
                                                right);\
\
    if (left != nullptr && right != nullptr)\
    {\
        dlib::matrix<ELEMENT>& l = *(static_cast<dlib::matrix<ELEMENT>*>(left));\
        dlib::matrix<ELEMENT>& r = *(static_cast<dlib::matrix<ELEMENT>*>(right));\
        *ret = new dlib::matrix<ELEMENT>(l OPERAND r);\
    }\
\
    if (left != nullptr)\
        delete left;\
    if (right != nullptr)\
        delete right;\
} while (0)

#define matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret) \
do {\
    dlib::matrix<ELEMENT>* left = nullptr;\
    matrix_cast_left_without_type_parameter_template(lhs,\
                                                     leftTemplateRows,\
                                                     leftTemplateColumns,\
                                                     left);\
\
    if (left != nullptr)\
    {\
        dlib::matrix<ELEMENT>& l = *(static_cast<dlib::matrix<ELEMENT>*>(left));\
        *ret = new dlib::matrix<ELEMENT>(l OPERAND rhs);\
    }\
\
    if (left != nullptr)\
        delete left;\
} while (0)

#define matrix_nc_template(matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<ELEMENT>*)matrix)->nc();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 0, 1>*)matrix)->nc();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 1, 3>*)matrix)->nc();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 31, 1>*)matrix)->nc();\
    }\
} while (0)

#define matrix_nr_template(matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<ELEMENT>*)matrix)->nr();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 0, 1>*)matrix)->nr();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 1, 3>*)matrix)->nr();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 31, 1>*)matrix)->nr();\
    }\
} while (0)

#define matrix_size_template(matrix, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::matrix<ELEMENT>*)matrix)->size();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 0, 1>*)matrix)->size();\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 1, 3>*)matrix)->size();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::matrix<ELEMENT, 31, 1>*)matrix)->size();\
    }\
} while (0)

#define matrix_delete_template(matrix, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        delete ((dlib::matrix<ELEMENT>*)matrix);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        delete ((dlib::matrix<ELEMENT, 0, 1>*)matrix);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        delete ((dlib::matrix<ELEMENT, 1, 3>*)matrix);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        delete ((dlib::matrix<ELEMENT, 31, 1>*)matrix);\
    }\
} while (0)

#define matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
        *ret = tmp(index);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(matrix));\
        *ret = tmp(index);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<ELEMENT, 1, 3>& tmp = *(static_cast<dlib::matrix<ELEMENT, 1, 3>*>(matrix));\
        *ret = tmp(index);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 31, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 31, 1>*>(matrix));\
        *ret = tmp(index);\
    }\
} while (0)

#define matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
        tmp(index) = value;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(matrix));\
        tmp(index) = value;\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<ELEMENT, 1, 3>& tmp = *(static_cast<dlib::matrix<ELEMENT, 1, 3>*>(matrix));\
        tmp(index) = value;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 31, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 31, 1>*>(matrix));\
        tmp(index) = value;\
    }\
} while (0)

#define matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
        *ret = tmp(row, column);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(matrix));\
        *ret = tmp(row, column);\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<ELEMENT, 1, 3>& tmp = *(static_cast<dlib::matrix<ELEMENT, 1, 3>*>(matrix));\
        *ret = tmp(row, column);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 31, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 31, 1>*>(matrix));\
        *ret = tmp(row, column);\
    }\
} while (0)

#define matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
        tmp(row, column) = value;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(matrix));\
        tmp(row, column) = value;\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<ELEMENT, 1, 3>& tmp = *(static_cast<dlib::matrix<ELEMENT, 1, 3>*>(matrix));\
        tmp(row, column) = value;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 31, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 31, 1>*>(matrix));\
        tmp(row, column) = value;\
    }\
} while (0)

#define matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& mat = *(static_cast<dlib::matrix<ELEMENT>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& mat = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 1 && templateColumns == 3)\
    {\
        dlib::matrix<ELEMENT, 1, 3>& mat = *(static_cast<dlib::matrix<ELEMENT, 1, 3>*>(matrix));\
        *stream << mat;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 31, 1>& mat = *(static_cast<dlib::matrix<ELEMENT, 31, 1>*>(matrix));\
        *stream << mat;\
    }\
} while (0)

#pragma endregion template

#pragma region matrix

DLLEXPORT void* matrix_new(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new matrix<uint8_t>();
        case matrix_element_type::UInt16:
            return new matrix<uint16_t>();
        case matrix_element_type::UInt32:
            return new matrix<uint32_t>();
        case matrix_element_type::Int8:
            return new matrix<int8_t>();
        case matrix_element_type::Int16:
            return new matrix<int16_t>();
        case matrix_element_type::Int32:
            return new matrix<int32_t>();
        case matrix_element_type::Float:
            return new matrix<float>();
        case matrix_element_type::Double:
            return new matrix<double>();
        case matrix_element_type::RgbPixel:
            return new matrix<rgb_pixel>();
        case matrix_element_type::HsiPixel:
            return new matrix<hsi_pixel>();
        case matrix_element_type::RgbAlphaPixel:
            return new matrix<rgb_alpha_pixel>();
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new1(matrix_element_type type, int num_rows, int num_cols)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new matrix<uint8_t>(num_rows, num_cols);
        case matrix_element_type::UInt16:
            return new matrix<uint16_t>(num_rows, num_cols);
        case matrix_element_type::UInt32:
            return new matrix<uint32_t>(num_rows, num_cols);
        case matrix_element_type::Int8:
            return new matrix<int8_t>(num_rows, num_cols);
        case matrix_element_type::Int16:
            return new matrix<int16_t>(num_rows, num_cols);
        case matrix_element_type::Int32:
            return new matrix<int32_t>(num_rows, num_cols);
        case matrix_element_type::Float:
            return new matrix<float>(num_rows, num_cols);
        case matrix_element_type::Double:
            return new matrix<double>(num_rows, num_cols);
        case matrix_element_type::RgbPixel:
            return new matrix<rgb_pixel>(num_rows, num_cols);
        case matrix_element_type::HsiPixel:
            return new matrix<hsi_pixel>(num_rows, num_cols);
        case matrix_element_type::RgbAlphaPixel:
            return new matrix<rgb_alpha_pixel>(num_rows, num_cols);
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new2(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* matrix_new3(const matrix_element_type type, const int num_rows, const int num_cols, void* src)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_new2_template(num_rows, num_cols, src);
            #undef ELEMENT
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT int matrix_nc(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_nc_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int matrix_nr(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_nr_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int matrix_size(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, int* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_size_template(matrix, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT void matrix_delete(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns)
{
    // dlib::matrix is template type.
    // Ex. dlib::matrix<float, 31, 1> does NOT equal to dlib::matrix<float>
    // Template argument is decided in compile time rather than runtime.
    // So we can not specify template argument as variable.
    // In other words, we have to call delete for void* because we do not known exact type.
    // Fortunately, dlib::matrix does not implement destructor.
    // So it is could be no problem when delete void* and destructor is not called.
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_delete_template(matrix, templateRows, templateColumns);
            #undef ELEMENT
            break;
    }
}

DLLEXPORT int matrix_operator_array(matrix_element_type type, void* matrix, void* array)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            matrix_operator_array_template(matrix, array);
            #undef ELEMENT
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma region 

#pragma region matrix_operator_get_one_row_column

DLLEXPORT void matrix_operator_get_one_row_column_uint8_t(void* matrix, int index, int templateRows, int templateColumns, uint8_t* ret)
{
    #define ELEMENT uint8_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_uint16_t(void* matrix, int index, int templateRows, int templateColumns, uint16_t* ret)
{
    #define ELEMENT uint16_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_uint32_t(void* matrix, int index, int templateRows, int templateColumns, uint32_t* ret)
{
    #define ELEMENT uint32_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_int8_t(void* matrix, int index, int templateRows, int templateColumns, int8_t* ret)
{
    #define ELEMENT int8_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_int16_t(void* matrix, int index, int templateRows, int templateColumns, int16_t* ret)
{
    #define ELEMENT int16_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_int32_t(void* matrix, int index, int templateRows, int templateColumns, int32_t* ret)
{
    #define ELEMENT int32_t
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_double(void* matrix, int index, int templateRows, int templateColumns, double* ret)
{
    #define ELEMENT double
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_float(void* matrix, int index, int templateRows, int templateColumns, float* ret)
{
    #define ELEMENT float
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_rgb_pixel(void* matrix, int index, int templateRows, int templateColumns, rgb_pixel* ret)
{
    #define ELEMENT rgb_pixel
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_rgb_alpha_pixel(void* matrix, int index, int templateRows, int templateColumns, rgb_alpha_pixel* ret)
{
    #define ELEMENT rgb_alpha_pixel
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_one_row_column_hsi_pixel(void* matrix, int index, int templateRows, int templateColumns, hsi_pixel* ret)
{
    #define ELEMENT hsi_pixel
    matrix_operator_get_one_row_column_template(matrix, index, templateRows, templateColumns, ret);
    #undef ELEMENT
}

#pragma endregion

#pragma region matrix_operator_set_one_row_column

DLLEXPORT void matrix_operator_set_one_row_column_uint8_t(void* matrix, int index, int templateRows, int templateColumns, uint8_t value)
{
    #define ELEMENT uint8_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_uint16_t(void* matrix, int index, int templateRows, int templateColumns, uint16_t value)
{
    #define ELEMENT uint16_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_uint32_t(void* matrix, int index, int templateRows, int templateColumns, uint32_t value)
{
    #define ELEMENT uint32_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_int8_t(void* matrix, int index, int templateRows, int templateColumns, int8_t value)
{
    #define ELEMENT int8_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_int16_t(void* matrix, int index, int templateRows, int templateColumns, int16_t value)
{
    #define ELEMENT int16_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_int32_t(void* matrix, int index, int templateRows, int templateColumns, int32_t value)
{
    #define ELEMENT int32_t
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_double(void* matrix, int index, int templateRows, int templateColumns, double value)
{
    #define ELEMENT double
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_float(void* matrix, int index, int templateRows, int templateColumns, float value)
{
    #define ELEMENT float
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_rgb_pixel(void* matrix, int index, int templateRows, int templateColumns, rgb_pixel value)
{
    #define ELEMENT rgb_pixel
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_rgb_alpha_pixel(void* matrix, int index, int templateRows, int templateColumns, rgb_alpha_pixel value)
{
    #define ELEMENT rgb_alpha_pixel
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_one_row_column_hsi_pixel(void* matrix, int index, int templateRows, int templateColumns, hsi_pixel value)
{
    #define ELEMENT hsi_pixel
    matrix_operator_set_one_row_column_template(matrix, index, templateRows, templateColumns, value);
    #undef ELEMENT
}

#pragma endregion

#pragma region matrix_operator_get_row_column

DLLEXPORT void matrix_operator_get_row_column_uint8_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint8_t* ret)
{
    #define ELEMENT uint8_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_uint16_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint16_t* ret)
{
    #define ELEMENT uint16_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_uint32_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint32_t* ret)
{
    #define ELEMENT uint32_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_int8_t(void* matrix, int row, int column, int templateRows, int templateColumns, int8_t* ret)
{
    #define ELEMENT int8_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_int16_t(void* matrix, int row, int column, int templateRows, int templateColumns, int16_t* ret)
{
    #define ELEMENT int16_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_int32_t(void* matrix, int row, int column, int templateRows, int templateColumns, int32_t* ret)
{
    #define ELEMENT int32_t
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_double(void* matrix, int row, int column, int templateRows, int templateColumns, double* ret)
{
    #define ELEMENT double
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_float(void* matrix, int row, int column, int templateRows, int templateColumns, float* ret)
{
    #define ELEMENT float
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_rgb_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, rgb_pixel* ret)
{
    #define ELEMENT rgb_pixel
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_rgb_alpha_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, rgb_alpha_pixel* ret)
{
    #define ELEMENT rgb_alpha_pixel
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_get_row_column_hsi_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, hsi_pixel* ret)
{
    #define ELEMENT hsi_pixel
    matrix_operator_get_row_column_template(matrix, row, column, templateRows, templateColumns, ret);
    #undef ELEMENT
}

#pragma endregion

#pragma region matrix_operator_set_row_column

DLLEXPORT void matrix_operator_set_row_column_uint8_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint8_t value)
{
    #define ELEMENT uint8_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_uint16_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint16_t value)
{
    #define ELEMENT uint16_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_uint32_t(void* matrix, int row, int column, int templateRows, int templateColumns, uint32_t value)
{
    #define ELEMENT uint32_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_int8_t(void* matrix, int row, int column, int templateRows, int templateColumns, int8_t value)
{
    #define ELEMENT int8_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_int16_t(void* matrix, int row, int column, int templateRows, int templateColumns, int16_t value)
{
    #define ELEMENT int16_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_int32_t(void* matrix, int row, int column, int templateRows, int templateColumns, int32_t value)
{
    #define ELEMENT int32_t
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_double(void* matrix, int row, int column, int templateRows, int templateColumns, double value)
{
    #define ELEMENT double
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_float(void* matrix, int row, int column, int templateRows, int templateColumns, float value)
{
    #define ELEMENT float
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_rgb_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, rgb_pixel value)
{    
    #define ELEMENT rgb_pixel
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_rgb_alpha_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, rgb_alpha_pixel value)
{
    #define ELEMENT rgb_alpha_pixel
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

DLLEXPORT void matrix_operator_set_row_column_hsi_pixel(void* matrix, int row, int column, int templateRows, int templateColumns, hsi_pixel value)
{
    #define ELEMENT hsi_pixel
    matrix_operator_set_row_column_template(matrix, row, column, templateRows, templateColumns, value);
    #undef ELEMENT
}

#pragma endregion

#pragma endregion

#pragma region operator

DLLEXPORT int matrix_operator_left_shift(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT int8_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_left_shift_template(matrix, templateRows, templateColumns, stream);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int matrix_operator_add(matrix_element_type type, 
                                  void* lhs,
                                  void* rhs,
                                  const int leftTemplateRows,
                                  const int leftTemplateColumns,
                                  const int rightTemplateRows,
                                  const int rightTemplateColumns,
                                  void** ret)
{
    int err = ERR_OK;
    #define OPERAND +
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    #undef OPERAND
    
    return err;
}

DLLEXPORT int matrix_operator_subtract(matrix_element_type type,
                                       void* lhs,
                                       void* rhs,
                                       const int leftTemplateRows,
                                       const int leftTemplateColumns,
                                       const int rightTemplateRows,
                                       const int rightTemplateColumns,
                                       void** ret)
{
    int err = ERR_OK;
    #define OPERAND -
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    #undef OPERAND
    
    return err;
}

DLLEXPORT int matrix_operator_multiply(matrix_element_type type,
                                       void* lhs,
                                       void* rhs,
                                       const int leftTemplateRows,
                                       const int leftTemplateColumns,
                                       const int rightTemplateRows,
                                       const int rightTemplateColumns,
                                       void** ret)
{
    int err = ERR_OK;
    #define OPERAND *
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    #undef OPERAND
    
    return err;
}

DLLEXPORT int matrix_operator_divide(matrix_element_type type,
                                     void* lhs,
                                     void* rhs,
                                     const int leftTemplateRows,
                                     const int leftTemplateColumns,
                                     const int rightTemplateRows,
                                     const int rightTemplateColumns,
                                     void** ret)
{
    int err = ERR_OK;
    #define OPERAND /
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    #undef OPERAND
    
    return err;
}


DLLEXPORT int matrix_operator_divide_double(matrix_element_type type,
                                            void* lhs,
                                            double rhs,
                                            const int leftTemplateRows,
                                            const int leftTemplateColumns,
                                            void** ret)
{
    int err = ERR_OK;
    #define OPERAND /
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            matrix_operator_primitive_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    #undef OPERAND
    
    return err;
}

#pragma endregion operator

#pragma endregion matrix

#endif