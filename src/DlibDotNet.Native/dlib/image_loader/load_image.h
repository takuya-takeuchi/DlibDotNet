#ifndef _CPP_LOAD_IMAGE_H_
#define _CPP_LOAD_IMAGE_H_

#include "../export.h"
#include <dlib/image_io.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

#pragma region template

#define load_image_matrix_template(__TYPE__, error, file_name, ret)\
do {\
    auto tmp = new dlib::matrix<__TYPE__>();\
    dlib::load_image(*tmp, file_name);\
    *ret = tmp;\
}while(0)

#pragma endregion template

DLLEXPORT int load_image(array2d_type type, void* image, const char* file_name)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::load_image(*((array2d<uint8_t>*)image), file_name);
            break;
        case array2d_type::UInt16:
            dlib::load_image(*((array2d<uint16_t>*)image), file_name);
            break;
        case array2d_type::UInt32:
            dlib::load_image(*((array2d<uint32_t>*)image), file_name);
            break;
        case array2d_type::Int8:
            dlib::load_image(*((array2d<int8_t>*)image), file_name);
            break;
        case array2d_type::Int16:
            dlib::load_image(*((array2d<int16_t>*)image), file_name);
            break;
        case array2d_type::Int32:
            dlib::load_image(*((array2d<int32_t>*)image), file_name);
            break;
        case array2d_type::Float:
            dlib::load_image(*((array2d<float>*)image), file_name);
            break;
        case array2d_type::Double:
            dlib::load_image(*((array2d<double>*)image), file_name);
            break;
        case array2d_type::RgbPixel:
            dlib::load_image(*((array2d<rgb_pixel>*)image), file_name);
            break;
        case array2d_type::HsiPixel:
            dlib::load_image(*((array2d<hsi_pixel>*)image), file_name);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::load_image(*((array2d<rgb_alpha_pixel>*)image), file_name);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int load_image_matrix(matrix_element_type type, const char* file_name, void** matrix)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            load_image_matrix_template(uint8_t, err, file_name, matrix);
            break;
        case matrix_element_type::UInt16:
            load_image_matrix_template(uint16_t, err, file_name, matrix);
            break;
        case matrix_element_type::UInt32:
            load_image_matrix_template(uint32_t, err, file_name, matrix);
            break;
        case matrix_element_type::UInt64:
            load_image_matrix_template(uint64_t, err, file_name, matrix);
            break;
        case matrix_element_type::Int8:
            load_image_matrix_template(int8_t, err, file_name, matrix);
            break;
        case matrix_element_type::Int16:
            load_image_matrix_template(int16_t, err, file_name, matrix);
            break;
        case matrix_element_type::Int32:
            load_image_matrix_template(int32_t, err, file_name, matrix);
            break;
        case matrix_element_type::Int64:
            load_image_matrix_template(int64_t, err, file_name, matrix);
            break;
        case matrix_element_type::Float:
            load_image_matrix_template(float, err, file_name, matrix);
            break;
        case matrix_element_type::Double:
            load_image_matrix_template(double, err, file_name, matrix);
            break;
        case matrix_element_type::RgbPixel:
            load_image_matrix_template(rgb_pixel, err, file_name, matrix);
            break;
        case matrix_element_type::HsiPixel:
            load_image_matrix_template(hsi_pixel, err, file_name, matrix);
            break;
        case matrix_element_type::RgbAlphaPixel:
            load_image_matrix_template(rgb_alpha_pixel, err, file_name, matrix);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif