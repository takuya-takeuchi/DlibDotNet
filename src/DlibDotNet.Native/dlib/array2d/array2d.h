#ifndef _CPP_ARRAY2D_H_
#define _CPP_ARRAY2D_H_

#include "../export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define array2d_matrix_new_template(templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        return new dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>();\
    }\
    return nullptr;\
} while (0)

#define array2d_matrix_new1_template(row, cols, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>(row, cols);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        return new dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>(row, cols);\
    }\
    return nullptr;\
} while (0)

#define array2d_matrix_delete_template(array, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        delete ((dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>*)array);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        delete ((dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>*)array);\
    }\
} while (0)

#define array2d_matrix_nc_template(array, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>*)array)->nc();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>*)array)->nc();\
    }\
} while (0)

#define array2d_matrix_nr_template(array, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>*)array)->nr();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>*)array)->nr();\
    }\
} while (0)

#define array2d_matrix_size_template(array, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>*)array)->size();\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *ret = ((dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>*)array)->size();\
    }\
} while (0)

#define array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        *rect = new dlib::rectangle(get_rect(*((array2d<matrix<ELEMENT, 0, 0>>*)array)));\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        *rect = new dlib::rectangle(get_rect(*((array2d<matrix<ELEMENT, 31, 1>>*)array)));\
    }\
} while (0)

#define array2d_matrix_row_template(array, templateRows, templateColumns, row, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>*>(array));\
        auto r = tmp[row];\
        *ret = new dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>::row(r);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>*>(array));\
        auto r = tmp[row];\
        *ret = new dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>::row(r);\
    }\
} while (0)

#define array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>::row& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>::row*>(row));\
        *ret = tmp[column];\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>::row& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>::row*>(row));\
        *ret = tmp[column];\
    }\
} while (0)

#define array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>::row& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 0, 0>*>::row*>(row));\
        tmp[column] = (dlib::matrix<ELEMENT, 0, 0>*)ret;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>::row& tmp = *(static_cast<dlib::array2d<dlib::matrix<ELEMENT, 31, 1>*>::row*>(row));\
        tmp[column] = (dlib::matrix<ELEMENT, 31, 1>*)ret;\
    }\
} while (0)

#define array2d_matrix_row_delete_template(row, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        auto tmp = static_cast<dlib::array2d<dlib::matrix<ELEMENT, 0, 0>>::row*>(row);\
        delete tmp;\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        auto tmp = static_cast<dlib::array2d<dlib::matrix<ELEMENT, 31, 1>>::row*>(row);\
        delete tmp;\
    }\
} while (0)

#pragma endregion template

#pragma region array2d

DLLEXPORT void* array2d_new(array2d_type type)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array2d<uint8_t>();
        case array2d_type::UInt16:
			return new dlib::array2d<uint16_t>();
        case array2d_type::Int16:
			return new dlib::array2d<int16_t>();
        case array2d_type::Int32:
			return new dlib::array2d<int32_t>();
        case array2d_type::Float:
			return new dlib::array2d<float>();
        case array2d_type::Double:
			return new dlib::array2d<double>();
        case array2d_type::RgbPixel:
			return new dlib::array2d<rgb_pixel>();
        case array2d_type::HsiPixel:
			return new dlib::array2d<hsi_pixel>();
        case array2d_type::RgbAlphaPixel:
			return new dlib::array2d<rgb_alpha_pixel>();
        default:
			return nullptr;
    }
}

DLLEXPORT void* array2d_new1(array2d_type type, int rows, int cols)
{
    switch(type)
    {
        case array2d_type::UInt8:
			return new dlib::array2d<uint8_t>(rows, cols);
        case array2d_type::UInt16:
			return new dlib::array2d<uint16_t>(rows, cols);
        case array2d_type::Int16:
			return new dlib::array2d<int16_t>(rows, cols);
        case array2d_type::Int32:
			return new dlib::array2d<int32_t>(rows, cols);
        case array2d_type::Float:
			return new dlib::array2d<float>(rows, cols);
        case array2d_type::Double:
			return new dlib::array2d<double>(rows, cols);
        case array2d_type::RgbPixel:
			return new dlib::array2d<rgb_pixel>(rows, cols);
        case array2d_type::HsiPixel:
			return new dlib::array2d<hsi_pixel>(rows, cols);
        case array2d_type::RgbAlphaPixel:
			return new dlib::array2d<rgb_alpha_pixel>(rows, cols);
        default:
			return nullptr;
    }
}

DLLEXPORT bool array2d_nc(array2d_type type, void* array, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->nc();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->nc();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->nc();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->nc();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->nc();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->nc();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->nc();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->nc();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->nc();
			return true;
        default:
			return false;
    }
}

DLLEXPORT bool array2d_nr(array2d_type type, void* array, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->nr();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->nr();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->nr();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->nr();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->nr();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->nr();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->nr();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->nr();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->nr();
			return true;
        default:
			return false;
    }
}

DLLEXPORT bool array2d_size(array2d_type type, void* array, uint64_t* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((array2d<uint8_t>*)array)->size();
			return true;
        case array2d_type::UInt16:
			*ret = ((array2d<uint16_t>*)array)->size();
			return true;
        case array2d_type::Int16:
			*ret = ((array2d<int16_t>*)array)->size();
			return true;
        case array2d_type::Int32:
			*ret = ((array2d<int32_t>*)array)->size();
			return true;
        case array2d_type::Float:
			*ret = ((array2d<float>*)array)->size();
			return true;
        case array2d_type::Double:
			*ret = ((array2d<double>*)array)->size();
			return true;
        case array2d_type::RgbPixel:
			*ret = ((array2d<rgb_pixel>*)array)->size();
			return true;
        case array2d_type::HsiPixel:
			*ret = ((array2d<hsi_pixel>*)array)->size();
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((array2d<rgb_alpha_pixel>*)array)->size();
			return true;
        default:
			return false;
    }
}

DLLEXPORT void array2d_delete(array2d_type type, void* array)
{
    switch(type)
    {
        case array2d_type::UInt8:
			delete ((array2d<uint8_t>*)array);
			break;
        case array2d_type::UInt16:
			delete ((array2d<uint16_t>*)array);
			break;
        case array2d_type::Int16:
			delete ((array2d<int16_t>*)array);
			break;
        case array2d_type::Int32:
			delete ((array2d<int32_t>*)array);
			break;
        case array2d_type::Float:
			delete ((array2d<float>*)array);
			break;
        case array2d_type::Double:
			delete ((array2d<double>*)array);
			break;
        case array2d_type::RgbPixel:
			delete ((array2d<rgb_pixel>*)array);
			break;
        case array2d_type::HsiPixel:
			delete ((array2d<hsi_pixel>*)array);
			break;
        case array2d_type::RgbAlphaPixel:
			delete ((array2d<rgb_alpha_pixel>*)array);
			break;
    }
}

#pragma endregion array2d

#pragma region row

DLLEXPORT int array2d_row(array2d_type type, void* array, int32_t row, void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto tmp = static_cast<dlib::array2d<uint8_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<uint8_t>::row(r);
            }
            break;
        case array2d_type::UInt16:
            {
                auto tmp = static_cast<dlib::array2d<uint16_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<uint16_t>::row(r);
            }
            break;
        case array2d_type::Int16:
            {
                auto tmp = static_cast<dlib::array2d<int16_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<int16_t>::row(r);
            }
            break;
        case array2d_type::Int32:
            {
                auto tmp = static_cast<dlib::array2d<int32_t>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<int32_t>::row(r);
            }
            break;
        case array2d_type::Float:
            {
                auto tmp = static_cast<dlib::array2d<float>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<float>::row(r);
            }
            break;
        case array2d_type::Double:
            {
                auto tmp = static_cast<dlib::array2d<double>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<double>::row(r);
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<rgb_pixel>::row(r);
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto tmp = static_cast<dlib::array2d<hsi_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<hsi_pixel>::row(r);
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_alpha_pixel>*>(array);
                auto r = (*tmp)[row];
                *ret = new dlib::array2d<rgb_alpha_pixel>::row(r);
            }
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region array2d_get_row_column

DLLEXPORT void array2d_get_row_column_uint8_t(void* row, int32_t column, uint8_t* ret)
{
    dlib::array2d<uint8_t>::row& tmp = *(static_cast<dlib::array2d<uint8_t>::row*>(row));
    *((uint8_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_uint16_t(void* row, int32_t column, uint16_t* ret)
{
    dlib::array2d<uint16_t>::row& tmp = *(static_cast<dlib::array2d<uint16_t>::row*>(row));
    *((uint16_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_int16_t(void* row, int32_t column, int16_t* ret)
{
    dlib::array2d<int16_t>::row& tmp = *(static_cast<dlib::array2d<int16_t>::row*>(row));
    *((int16_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_int32_t(void* row, int32_t column, int32_t* ret)
{
    dlib::array2d<int32_t>::row& tmp = *(static_cast<dlib::array2d<int32_t>::row*>(row));
    *((int32_t*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_double(void* row, int32_t column, double* ret)
{
    dlib::array2d<double>::row& tmp = *(static_cast<dlib::array2d<double>::row*>(row));
    *((double*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_float(void* row, int32_t column, float* ret)
{
    dlib::array2d<float>::row& tmp = *(static_cast<dlib::array2d<float>::row*>(row));
    *((float*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_rgb_pixel(void* row, int32_t column, rgb_pixel* ret)
{
    dlib::array2d<rgb_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_pixel>::row*>(row));
    *((rgb_pixel*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_rgb_alpha_pixel(void* row, int32_t column, rgb_alpha_pixel* ret)
{
    dlib::array2d<rgb_alpha_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row));
    *((rgb_alpha_pixel*)ret) = tmp[column];
}

DLLEXPORT void array2d_get_row_column_hsi_pixel(void* row, int32_t column, hsi_pixel* ret)
{
    dlib::array2d<hsi_pixel>::row& tmp = *(static_cast<dlib::array2d<hsi_pixel>::row*>(row));
    *((hsi_pixel*)ret) = tmp[column];
}

#pragma endregion array2d_get_row_column

#pragma region array2d_set_row_column

DLLEXPORT void array2d_set_row_column_uint8_t(void* row, int32_t column, uint8_t ret)
{
    dlib::array2d<uint8_t>::row& tmp = *(static_cast<dlib::array2d<uint8_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_uint16_t(void* row, int32_t column, uint16_t ret)
{
    dlib::array2d<uint16_t>::row& tmp = *(static_cast<dlib::array2d<uint16_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_int16_t(void* row, int32_t column, int16_t ret)
{
    dlib::array2d<int16_t>::row& tmp = *(static_cast<dlib::array2d<int16_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_int32_t(void* row, int32_t column, int32_t ret)
{
    dlib::array2d<int32_t>::row& tmp = *(static_cast<dlib::array2d<int32_t>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_double(void* row, int32_t column, double ret)
{
    dlib::array2d<double>::row& tmp = *(static_cast<dlib::array2d<double>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_float(void* row, int32_t column, float ret)
{
    dlib::array2d<float>::row& tmp = *(static_cast<dlib::array2d<float>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_rgb_pixel(void* row, int32_t column, rgb_pixel ret)
{
    dlib::array2d<rgb_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_pixel>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_rgb_alpha_pixel(void* row, int32_t column, rgb_alpha_pixel ret)
{
    dlib::array2d<rgb_alpha_pixel>::row& tmp = *(static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row));
    tmp[column] = ret;
}

DLLEXPORT void array2d_set_row_column_hsi_pixel(void* row, int32_t column, hsi_pixel ret)
{
    dlib::array2d<hsi_pixel>::row& tmp = *(static_cast<dlib::array2d<hsi_pixel>::row*>(row));
    tmp[column] = ret;
}

#pragma endregion array2d_set_row_column

DLLEXPORT int array2d_row_delete(array2d_type type, void* row)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto tmp = static_cast<dlib::array2d<uint8_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::UInt16:
            {
                auto tmp = static_cast<dlib::array2d<uint16_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Int16:
            {
                auto tmp = static_cast<dlib::array2d<int16_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Int32:
            {
                auto tmp = static_cast<dlib::array2d<int32_t>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Float:
            {
                auto tmp = static_cast<dlib::array2d<float>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::Double:
            {
                auto tmp = static_cast<dlib::array2d<double>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_pixel>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto tmp = static_cast<dlib::array2d<hsi_pixel>::row*>(row);
                delete tmp;
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto tmp = static_cast<dlib::array2d<rgb_alpha_pixel>::row*>(row);
                delete tmp;
            }
            break;
    }

    return err;
}

#pragma endregion row

#pragma region matrix

DLLEXPORT void* array2d_matrix_new(matrix_element_type type, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_new_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* array2d_matrix_new1(matrix_element_type type, const int rows, const int cols, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_new1_template(rows, cols, templateRows, templateColumns);
            #undef ELEMENT
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT bool array2d_matrix_nc(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    bool b = true;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_nc_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            b = false;
            break;
    }

    return b;
}

DLLEXPORT bool array2d_matrix_nr(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    bool b = true;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_nr_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            b = false;
            break;
    }

    return b;
}

DLLEXPORT bool array2d_matrix_size(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int* ret)
{
    bool b = true;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_size_template(array, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            b = false;
            break;
    }

    return b;
}

DLLEXPORT void array2d_matrix_delete(matrix_element_type type, void* array, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_delete_template(array, templateRows, templateColumns);
            #undef ELEMENT
            break;
    }
}

DLLEXPORT int array2d_matrix_get_rect(matrix_element_type type, void* array, const int templateRows, const int templateColumns, rectangle** rect)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_get_rect_template(array, templateRows, templateColumns, rect);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma endregion matrix

#pragma region matrix row

DLLEXPORT int array2d_matrix_row(matrix_element_type type, void* array, const int templateRows, const int templateColumns, int32_t row, void** ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_row_template(array, templateRows, templateColumns, row, ret);
            #undef ELEMENT
            break;
        default:
            err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma region array2d_get_row_column

DLLEXPORT void array2d_matrix_get_row_column_uint8_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT uint8_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_uint16_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT uint16_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_uint32_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT uint32_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_int8_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT int8_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_int16_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT int16_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_int32_t(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT int32_t
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_double(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT double
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_float(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT float
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_rgb_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT rgb_pixel
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_rgb_alpha_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT rgb_alpha_pixel
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_get_row_column_hsi_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void** ret)
{
    #define ELEMENT hsi_pixel
    array2d_matrix_get_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

#pragma endregion array2d_get_row_column

#pragma region array2d_get_row_column

DLLEXPORT void array2d_matrix_set_row_column_uint8_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT uint8_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_uint16_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT uint16_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_uint32_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT uint32_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_int8_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT int8_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_int16_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT int16_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_int32_t(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT int32_t
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_double(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT double
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_float(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT float
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_rgb_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT rgb_pixel
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_rgb_alpha_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT rgb_alpha_pixel
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

DLLEXPORT void array2d_matrix_set_row_column_hsi_pixel(void* row, const int templateRows, const int templateColumns, int32_t column, void* ret)
{
    #define ELEMENT hsi_pixel
    array2d_matrix_set_row_column_template(row, templateRows, templateColumns, column, ret);
    #undef ELEMENT
}

#pragma endregion array2d_get_row_column

DLLEXPORT void array2d_matrix_row_delete(matrix_element_type type, void* row, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT double
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            array2d_matrix_row_delete_template(row, templateRows, templateColumns);
            #undef ELEMENT
            break;
    }
}

#pragma endregion matrix row

#endif