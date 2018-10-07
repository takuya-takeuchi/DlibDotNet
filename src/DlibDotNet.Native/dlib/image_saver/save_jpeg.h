#ifndef _CPP_SAVE_JPEG_H_
#define _CPP_SAVE_JPEG_H_

#include "../export.h"
#include <dlib/array2d/array2d_kernel.h>
#include <dlib/image_io.h>
#include <dlib/image_saver/save_jpeg.h>
#include <dlib/pixel.h>
#include <dlib/image_processing/generic_image.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define save_jpeg_matrix_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, file_name, quality) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
dlib::save_jpeg(mat, file_name, quality);\

#define save_jpeg_matrix_template(__TYPE__, __ROWS__, __COLUMNS__, error, matrix, file_name, quality) \
do {\
    matrix_template_size_arg3_template(__TYPE__, __ROWS__, __COLUMNS__, save_jpeg_matrix_template_sub, error, matrix, file_name, quality);\
} while (0)

#pragma endregion template

DLLEXPORT int save_jpeg(array2d_type type, void* image, const char* file_name, int quality)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::save_jpeg(*((array2d<uint8_t>*)image), file_name, quality);
            break;
        case array2d_type::UInt16:
            dlib::save_jpeg(*((array2d<uint16_t>*)image), file_name, quality);
            break;
        case array2d_type::UInt32:
            dlib::save_jpeg(*((array2d<uint32_t>*)image), file_name, quality);
            break;
        case array2d_type::Int8:
            dlib::save_jpeg(*((array2d<int8_t>*)image), file_name, quality);
            break;
        case array2d_type::Int16:
            dlib::save_jpeg(*((array2d<int16_t>*)image), file_name, quality);
            break;
        case array2d_type::Int32:
            dlib::save_jpeg(*((array2d<int32_t>*)image), file_name, quality);
            break;
        case array2d_type::Float:
            dlib::save_jpeg(*((array2d<float>*)image), file_name, quality);
            break;
        case array2d_type::Double:
            dlib::save_jpeg(*((array2d<double>*)image), file_name, quality);
            break;
        case array2d_type::RgbPixel:
            dlib::save_jpeg(*((array2d<rgb_pixel>*)image), file_name, quality);
            break;
        case array2d_type::HsiPixel:
            dlib::save_jpeg(*((array2d<hsi_pixel>*)image), file_name, quality);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::save_jpeg(*((array2d<rgb_alpha_pixel>*)image), file_name, quality);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int save_jpeg_matrix(matrix_element_type type, void* matrix, const int templateRows, const int templateColumns, const char* file_name, int quality)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            save_jpeg_matrix_template(uint8_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::UInt16:
            save_jpeg_matrix_template(uint16_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::UInt32:
            save_jpeg_matrix_template(uint32_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::Int8:
            save_jpeg_matrix_template(int8_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::Int16:
            save_jpeg_matrix_template(int16_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::Int32:
            save_jpeg_matrix_template(int32_t, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::Float:
            save_jpeg_matrix_template(float, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::Double:
            save_jpeg_matrix_template(double, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::RgbPixel:
            save_jpeg_matrix_template(rgb_pixel, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::HsiPixel:
            save_jpeg_matrix_template(hsi_pixel, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        case matrix_element_type::RgbAlphaPixel:
            save_jpeg_matrix_template(rgb_alpha_pixel, templateRows, templateColumns, err, matrix, file_name, quality);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif