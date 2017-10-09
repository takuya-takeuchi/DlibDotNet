#ifndef _CPP_WIDGETS_H_
#define _CPP_WIDGETS_H_

#include "../export.h"
#include <dlib/gui_widgets/widgets.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region ImageWindow<matrix_op<template>>

#define ELEMENT element
#undef ELEMENT

#pragma region new

#define image_window_new_matrix_op_template(ret, type, image) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint8_t>>>*)image));\
            break;\
        case array2d_type::UInt16:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint16_t>>>*)image));\
            break;\
        case array2d_type::Float:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<float>>>*)image));\
            break;\
        case array2d_type::Double:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<double>>>*)image));\
            break;\
        case array2d_type::RgbPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)image));\
            break;\
        case array2d_type::HsiPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)image));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)image));\
            break;\
        default:\
            break;\
    }\
    ret = nullptr;\
} while (0)

#define image_window_new_matrix_op_template2(ret, type, image, title) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint8_t>>>*)image), title);\
            break;\
        case array2d_type::UInt16:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint16_t>>>*)image), title);\
            break;\
        case array2d_type::Float:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<float>>>*)image), title);\
            break;\
        case array2d_type::Double:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<double>>>*)image), title);\
            break;\
        case array2d_type::RgbPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)image), title);\
            break;\
        case array2d_type::HsiPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)image), title);\
            break;\
        case array2d_type::RgbAlphaPixel:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)image), title);\
            break;\
        default:\
            break;\
    }\
    ret = nullptr;\
} while (0)

#pragma endregion new

#define image_window_set_image_matrix_op_template(ret, window, type, image) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
            window->set_image(*((matrix_op<ELEMENT<array2d<uint8_t>>>*)image));\
            break;\
        case array2d_type::UInt16:\
            window->set_image(*((matrix_op<ELEMENT<array2d<uint16_t>>>*)image));\
            break;\
        case array2d_type::Float:\
            window->set_image(*((matrix_op<ELEMENT<array2d<float>>>*)image));\
            break;\
        case array2d_type::Double:\
            window->set_image(*((matrix_op<ELEMENT<array2d<double>>>*)image));\
            break;\
        case array2d_type::RgbPixel:\
            window->set_image(*((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)image));\
            break;\
        case array2d_type::HsiPixel:\
            window->set_image(*((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)image));\
            break;\
        case array2d_type::RgbAlphaPixel:\
            window->set_image(*((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)image));\
            break;\
        default:\
            ret = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion ImageWindow<matrix_op<template>>

#pragma region ImageWindow

#pragma region new

DLLEXPORT image_window* image_window_new()
{
    return new image_window();
}

DLLEXPORT image_window* image_window_new_array2d1(array2d_type type, void* image)
{
    switch(type)
    {
        case array2d_type::UInt8:
            return new image_window(*((array2d<uint8_t>*)image));
        case array2d_type::UInt16:
            return new image_window(*((array2d<uint16_t>*)image));
        case array2d_type::Float:
            return new image_window(*((array2d<float>*)image));
        case array2d_type::Double:
            return new image_window(*((array2d<double>*)image));
        case array2d_type::RgbPixel:
            return new image_window(*((array2d<rgb_pixel>*)image));
        case array2d_type::HsiPixel:
            return new image_window(*((array2d<hsi_pixel>*)image));
        case array2d_type::RgbAlphaPixel:
            return new image_window(*((array2d<rgb_alpha_pixel>*)image));
        default:
            return nullptr;
    }
}

DLLEXPORT image_window* image_window_new_array2d2(array2d_type type, void* image, const char* title)
{
    switch(type)
    {
        case array2d_type::UInt8:
            return new image_window(*((array2d<uint8_t>*)image), title);
        case array2d_type::UInt16:
            return new image_window(*((array2d<uint16_t>*)image), title);
        case array2d_type::Float:
            return new image_window(*((array2d<float>*)image), title);
        case array2d_type::Double:
            return new image_window(*((array2d<double>*)image), title);
        case array2d_type::RgbPixel:
            return new image_window(*((array2d<rgb_pixel>*)image), title);
        case array2d_type::HsiPixel:
            return new image_window(*((array2d<hsi_pixel>*)image), title);
        case array2d_type::RgbAlphaPixel:
            return new image_window(*((array2d<rgb_alpha_pixel>*)image), title);
        default:
            return nullptr;
    }
}

DLLEXPORT image_window* image_window_new_matrix1(matrix_element_type type, void* image)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new image_window(*((dlib::matrix<uint8_t>*)image));
        case matrix_element_type::UInt16:
            return new image_window(*((dlib::matrix<uint16_t>*)image));
        case matrix_element_type::UInt32:
            return new image_window(*((dlib::matrix<uint32_t>*)image));
        case matrix_element_type::Int8:
            return new image_window(*((dlib::matrix<int8_t>*)image));
        case matrix_element_type::Int16:
            return new image_window(*((dlib::matrix<int16_t>*)image));
        case matrix_element_type::Int32:
            return new image_window(*((dlib::matrix<int32_t>*)image));
        case matrix_element_type::Float:
            return new image_window(*((dlib::matrix<float>*)image));
        case matrix_element_type::Double:
            return new image_window(*((dlib::matrix<double>*)image));
        case matrix_element_type::RgbPixel:
            return new image_window(*((dlib::matrix<rgb_pixel>*)image));
        case matrix_element_type::HsiPixel:
            return new image_window(*((dlib::matrix<hsi_pixel>*)image));
        case matrix_element_type::RgbAlphaPixel:
            return new image_window(*((dlib::matrix<rgb_alpha_pixel>*)image));
        default:
            return nullptr;
    }
}

DLLEXPORT image_window* image_window_new_matrix2(matrix_element_type type, void* image, const char* title)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new image_window(*((dlib::matrix<uint8_t>*)image), title);
        case matrix_element_type::UInt16:
            return new image_window(*((dlib::matrix<uint16_t>*)image), title);
        case matrix_element_type::UInt32:
            return new image_window(*((dlib::matrix<uint32_t>*)image), title);
        case matrix_element_type::Int8:
            return new image_window(*((dlib::matrix<int8_t>*)image), title);
        case matrix_element_type::Int16:
            return new image_window(*((dlib::matrix<int16_t>*)image), title);
        case matrix_element_type::Int32:
            return new image_window(*((dlib::matrix<int32_t>*)image), title);
        case matrix_element_type::Float:
            return new image_window(*((dlib::matrix<float>*)image), title);
        case matrix_element_type::Double:
            return new image_window(*((dlib::matrix<double>*)image), title);
        case matrix_element_type::RgbPixel:
            return new image_window(*((dlib::matrix<rgb_pixel>*)image), title);
        case matrix_element_type::HsiPixel:
            return new image_window(*((dlib::matrix<hsi_pixel>*)image), title);
        case matrix_element_type::RgbAlphaPixel:
            return new image_window(*((dlib::matrix<rgb_alpha_pixel>*)image), title);
        default:
            return nullptr;
    }
}

DLLEXPORT void* image_window_new_matrix_op1(element_type etype, array2d_type type, void* image)
{    
    void* ret = nullptr;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_new_matrix_op_template(ret, type, image);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_new_matrix_op_template(ret, type, image);
            #undef ELEMENT
            break;
        default:
            break;
    }

    return ret;
}

DLLEXPORT void* image_window_new_matrix_op2(element_type etype, array2d_type type, void* image, const char* title)
{   
    void* ret = nullptr;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_new_matrix_op_template2(ret, type, image, title);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_new_matrix_op_template2(ret, type, image, title);
            #undef ELEMENT
            break;
        default:
            break;
    }

    return ret;
}

#pragma endregion new

#pragma region add_overlay

DLLEXPORT int image_window_add_overlay(image_window* window, dlib::rectangle* r, array2d_type type, void* p)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(*r, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(*r, *((uint16_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(*r, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(*r, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(*r, *((rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(*r, *((hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(*r, *((rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_add_overlay2(image_window* window, std::vector<rectangle*>* r, array2d_type type, void* p)
{
    int err = ERR_OK;
    
    std::vector<rectangle> tmpRects;
    for (int index = 0 ; index < (*r).size(); index++)
        tmpRects.push_back(*(*r)[index]);

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(tmpRects, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(tmpRects, *((uint16_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(tmpRects, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(tmpRects, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(tmpRects, *((rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(tmpRects, *((hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(tmpRects, *((rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_add_overlay3(image_window* window, dlib::drectangle* r, array2d_type type, void* p)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(*r, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(*r, *((uint16_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(*r, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(*r, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(*r, *((rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(*r, *((hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(*r, *((rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_add_overlay4(image_window* window, image_window::overlay_line* line)
{
    int err = ERR_OK;

    window->add_overlay(*line);

    return err;
}

DLLEXPORT int image_window_add_overlay5(image_window* window, std::vector<image_window::overlay_line*>* lines)
{
    int err = ERR_OK;
    
    std::vector<image_window::overlay_line> tmpRects;
    for (int index = 0 ; index < (*lines).size(); index++)
        tmpRects.push_back(*(*lines)[index]);

    window->add_overlay(tmpRects);

    return err;
}

#pragma endregion add_overlay

DLLEXPORT void image_window_clear_overlay(image_window* window)
{
    window->clear_overlay();
}

DLLEXPORT void image_window_delete(image_window* window)
{
	delete window;
}

DLLEXPORT void image_window_wait_until_closed(image_window* window)
{
	window->wait_until_closed();
}

#pragma region set_image

DLLEXPORT int image_window_set_image_array2d(image_window* window, array2d_type type, void* image)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            window->set_image(*((array2d<uint8_t>*)image));
            break;
        case array2d_type::UInt16:
            window->set_image(*((array2d<uint16_t>*)image));
            break;
        case array2d_type::Float:
            window->set_image(*((array2d<float>*)image));
            break;
        case array2d_type::Double:
            window->set_image(*((array2d<double>*)image));
            break;
        case array2d_type::RgbPixel:
            window->set_image(*((array2d<rgb_pixel>*)image));
            break;
        case array2d_type::HsiPixel:
            window->set_image(*((array2d<hsi_pixel>*)image));
            break;
        case array2d_type::RgbAlphaPixel:
            window->set_image(*((array2d<rgb_alpha_pixel>*)image));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_set_image_matrix(image_window* window, matrix_element_type type, void* image)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            window->set_image(*((dlib::matrix<uint8_t>*)image));
            break;
        case matrix_element_type::UInt16:
            window->set_image(*((dlib::matrix<uint16_t>*)image));
            break;
        case matrix_element_type::UInt32:
            window->set_image(*((dlib::matrix<uint32_t>*)image));
            break;
        case matrix_element_type::Int8:
            window->set_image(*((dlib::matrix<int8_t>*)image));
            break;
        case matrix_element_type::Int16:
            window->set_image(*((dlib::matrix<int16_t>*)image));
            break;
        case matrix_element_type::Int32:
            window->set_image(*((dlib::matrix<int32_t>*)image));
            break;
        case matrix_element_type::Float:
            window->set_image(*((dlib::matrix<float>*)image));
            break;
        case matrix_element_type::Double:
            window->set_image(*((dlib::matrix<double>*)image));
            break;
        case matrix_element_type::RgbPixel:
            window->set_image(*((dlib::matrix<rgb_pixel>*)image));
            break;
        case matrix_element_type::HsiPixel:
            window->set_image(*((dlib::matrix<hsi_pixel>*)image));
            break;
        case matrix_element_type::RgbAlphaPixel:
            window->set_image(*((dlib::matrix<rgb_alpha_pixel>*)image));
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_set_image_matrix_op(image_window* window, element_type etype, array2d_type type, void* image)
{
    int err = ERR_OK;

    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_set_image_matrix_op_template(err, window, type, image);
            #undef ELEMENT
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_set_image_matrix_op_template(err, window, type, image);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion set_image

#pragma endregion ImageWindow

#pragma region ImageWindow::overlay_line

DLLEXPORT image_window::overlay_line* image_window_overlay_line_new()
{
	return new image_window::overlay_line();
}

DLLEXPORT bool image_window_overlay_line_p1(image_window::overlay_line* line, dlib::point** point)
{
    *point = new dlib::point(line->p1);
    return true;
}

DLLEXPORT bool image_window_overlay_line_p2(image_window::overlay_line* line, dlib::point** point)
{
    *point = new dlib::point(line->p2);
    return true;
}

DLLEXPORT bool image_window_overlay_line_color(image_window::overlay_line* line, rgb_alpha_pixel* color)
{
    memcpy(color, &(line->color), sizeof(rgb_alpha_pixel));
    return true;
}

DLLEXPORT void image_window_overlay_line_delete(image_window::overlay_line* line)
{
	delete line;
}

#pragma endregion ImageWindow::overlay_line

#endif