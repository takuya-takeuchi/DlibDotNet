#ifndef _CPP_DRAW_H_
#define _CPP_DRAW_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/image_transforms/draw.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix

#define tile_images_matrix_template(__TYPE__, images, ret_image) \
do { \
    std::vector<dlib::matrix<__TYPE__>*>& tmp = *(static_cast<std::vector<dlib::matrix<__TYPE__>*>*>(images));\
    std::vector<dlib::matrix<__TYPE__>> tmp_images;\
    for (int index = 0; index < tmp.size(); index++)\
    {\
        dlib::matrix<__TYPE__>& matrix = *(static_cast<dlib::matrix<__TYPE__>*>(tmp[index]));\
        tmp_images.push_back(matrix);\
    }\
    *ret_image = new dlib::matrix<__TYPE__>(dlib::tile_images(tmp_images));\
} while (0)

#pragma endregion template

DLLEXPORT int draw_line(array2d_type type,
                        void* image, 
                        dlib::point* p1,
                        dlib::point* p2,
                        void* p)
 {
     int err = ERR_OK;
 
     switch(type)
     {
         case array2d_type::UInt8:
             dlib::draw_line(*((array2d<uint8_t>*)image), *p1, *p2, *((uint8_t*)p));
             break;
         case array2d_type::UInt16:
             dlib::draw_line(*((array2d<uint16_t>*)image), *p1, *p2, *((uint16_t*)p));
             break;
         case array2d_type::UInt32:
             dlib::draw_line(*((array2d<uint32_t>*)image), *p1, *p2, *((uint32_t*)p));
             break;
         case array2d_type::Int8:
             dlib::draw_line(*((array2d<int8_t>*)image), *p1, *p2, *((int8_t*)p));
             break;
         case array2d_type::Int16:
             dlib::draw_line(*((array2d<int16_t>*)image), *p1, *p2, *((int16_t*)p));
             break;
         case array2d_type::Int32:
             dlib::draw_line(*((array2d<int32_t>*)image), *p1, *p2, *((int16_t*)p));
             break;
         case array2d_type::Float:
             dlib::draw_line(*((array2d<float>*)image), *p1, *p2, *((float*)p));
             break;
         case array2d_type::Double:
             dlib::draw_line(*((array2d<double>*)image), *p1, *p2, *((double*)p));
             break;
         case array2d_type::RgbPixel:
             dlib::draw_line(*((array2d<dlib::rgb_pixel>*)image), *p1, *p2, *((dlib::rgb_pixel*)p));
             break;
         case array2d_type::HsiPixel:
             dlib::draw_line(*((array2d<dlib::hsi_pixel>*)image), *p1, *p2, *((dlib::hsi_pixel*)p));
             break;
         case array2d_type::RgbAlphaPixel:
             dlib::draw_line(*((array2d<dlib::rgb_alpha_pixel>*)image), *p1, *p2, *((dlib::rgb_alpha_pixel*)p));
             break;
         default:
             err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
             break;
     }
 
     return err;
 }

 DLLEXPORT int draw_rectangle(array2d_type type,
                              void* image, 
                              dlib::rectangle* rect,
                              void* p,
                              unsigned int thickness)
 {
     int err = ERR_OK;
 
     switch(type)
     {
         case array2d_type::UInt8:
             dlib::draw_rectangle(*((array2d<uint8_t>*)image), *rect, *((uint8_t*)p), thickness);
             break;
         case array2d_type::UInt16:
             dlib::draw_rectangle(*((array2d<uint16_t>*)image), *rect, *((uint16_t*)p), thickness);
             break;
         case array2d_type::UInt32:
             dlib::draw_rectangle(*((array2d<uint32_t>*)image), *rect, *((uint32_t*)p), thickness);
             break;
         case array2d_type::Int8:
             dlib::draw_rectangle(*((array2d<int8_t>*)image), *rect, *((int8_t*)p), thickness);
             break;
         case array2d_type::Int16:
             dlib::draw_rectangle(*((array2d<int16_t>*)image), *rect, *((int16_t*)p), thickness);
             break;
         case array2d_type::Int32:
             dlib::draw_rectangle(*((array2d<int32_t>*)image), *rect, *((uint32_t*)p), thickness);
             break;
         case array2d_type::Float:
             dlib::draw_rectangle(*((array2d<float>*)image), *rect, *((float*)p), thickness);
             break;
         case array2d_type::Double:
             dlib::draw_rectangle(*((array2d<double>*)image), *rect, *((double*)p), thickness);
             break;
         case array2d_type::RgbPixel:
             dlib::draw_rectangle(*((array2d<dlib::rgb_pixel>*)image), *rect, *((dlib::rgb_pixel*)p), thickness);
             break;
         case array2d_type::HsiPixel:
             dlib::draw_rectangle(*((array2d<dlib::hsi_pixel>*)image), *rect, *((dlib::hsi_pixel*)p), thickness);
             break;
         case array2d_type::RgbAlphaPixel:
             dlib::draw_rectangle(*((array2d<dlib::rgb_alpha_pixel>*)image), *rect, *((dlib::rgb_alpha_pixel*)p), thickness);
             break;
         default:
             err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
             break;
     }
 
     return err;
 }

DLLEXPORT int tile_images(array2d_type in_type, void* images, void** ret_image)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case array2d_type::UInt8:
            *ret_image = new dlib::matrix<uint8_t>(dlib::tile_images(*((dlib::array<dlib::array2d<uint8_t>>*)images)));
            break;
        case array2d_type::UInt16:
            *ret_image = new dlib::matrix<uint16_t>(dlib::tile_images(*((dlib::array<dlib::array2d<uint16_t>>*)images)));
            break;
        case array2d_type::UInt32:
            *ret_image = new dlib::matrix<uint32_t>(dlib::tile_images(*((dlib::array<dlib::array2d<uint32_t>>*)images)));
            break;
        case array2d_type::Int8:
            *ret_image = new dlib::matrix<int8_t>(dlib::tile_images(*((dlib::array<dlib::array2d<int8_t>>*)images)));
            break;
        case array2d_type::Int16:
            *ret_image = new dlib::matrix<int16_t>(dlib::tile_images(*((dlib::array<dlib::array2d<int16_t>>*)images)));
            break;
        case array2d_type::Int32:
            *ret_image = new dlib::matrix<int32_t>(dlib::tile_images(*((dlib::array<dlib::array2d<int32_t>>*)images)));
            break;
        case array2d_type::Float:
            *ret_image = new dlib::matrix<float>(dlib::tile_images(*((dlib::array<dlib::array2d<float>>*)images)));
            break;
        case array2d_type::Double:
            *ret_image = new dlib::matrix<double>(dlib::tile_images(*((dlib::array<dlib::array2d<double>>*)images)));
            break;
        case array2d_type::RgbPixel:
            *ret_image = new dlib::matrix<rgb_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<rgb_pixel>>*)images)));
            break;
        case array2d_type::HsiPixel:
            *ret_image = new dlib::matrix<hsi_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<hsi_pixel>>*)images)));
            break;
        case array2d_type::RgbAlphaPixel:
            *ret_image = new dlib::matrix<rgb_alpha_pixel>(dlib::tile_images(*((dlib::array<dlib::array2d<rgb_alpha_pixel>>*)images)));
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int tile_images_matrix(matrix_element_type in_type, void* images, void** ret_image)
{
    int err = ERR_OK;

    switch(in_type)
    {
        case matrix_element_type::UInt8:
            tile_images_matrix_template(uint8_t, images, ret_image);
            break;
        case matrix_element_type::UInt16:
            tile_images_matrix_template(uint16_t, images, ret_image);
            break;
        case matrix_element_type::UInt32:
            tile_images_matrix_template(uint32_t, images, ret_image);
            break;
        case matrix_element_type::Int8:
            tile_images_matrix_template(int8_t, images, ret_image);
            break;
        case matrix_element_type::Int16:
            tile_images_matrix_template(int16_t, images, ret_image);
            break;
        case matrix_element_type::Int32:
            tile_images_matrix_template(int32_t, images, ret_image);
            break;
        case matrix_element_type::Float:
            tile_images_matrix_template(float, images, ret_image);
            break;
        case matrix_element_type::Double:
            tile_images_matrix_template(double, images, ret_image);
            break;
        case matrix_element_type::RgbPixel:
            tile_images_matrix_template(rgb_pixel, images, ret_image);
            break;
        case matrix_element_type::HsiPixel:
            tile_images_matrix_template(hsi_pixel, images, ret_image);
            break;
        case matrix_element_type::RgbAlphaPixel:
            tile_images_matrix_template(rgb_alpha_pixel, images, ret_image);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif