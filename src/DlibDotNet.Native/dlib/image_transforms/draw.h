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

#define ELEMENT element
#undef ELEMENT

#define tile_images_matrix_template(images, ret_image) \
do { \
    std::vector<dlib::matrix<ELEMENT>*>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(images));\
    std::vector<dlib::matrix<ELEMENT>> tmp_images;\
    for (int index = 0; index < tmp.size(); index++)\
    {\
        dlib::matrix<ELEMENT>& matrix = *(static_cast<dlib::matrix<ELEMENT>*>(tmp[index]));\
        tmp_images.push_back(matrix);\
    }\
    *ret_image = new dlib::matrix<ELEMENT>(dlib::tile_images(tmp_images));\
} while (0)

#pragma endregion template

DLLEXPORT int draw_line(
    array2d_type type,
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
             err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
             break;
     }
 
     return err;
 }

 DLLEXPORT int draw_rectangle(
    array2d_type type,
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
         case array2d_type::Int32:
             dlib::draw_rectangle(*((array2d<int32_t>*)image), *rect, *((uint16_t*)p), thickness);
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
             err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
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
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
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
            #define ELEMENT uint8_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            tile_images_matrix_template(images, ret_image);
            #undef ELEMENT
            break;
        default:
            err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif