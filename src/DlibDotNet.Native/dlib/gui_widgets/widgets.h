#ifndef DLIB_NO_GUI_SUPPORT

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

#pragma region template

#define ELEMENT element
#undef ELEMENT

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
        case array2d_type::UInt32:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint32_t>>>*)image));\
            break;\
        case array2d_type::Int8:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int8_t>>>*)image));\
            break;\
        case array2d_type::Int16:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int16_t>>>*)image));\
            break;\
        case array2d_type::Int32:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int32_t>>>*)image));\
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
        case array2d_type::UInt32:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<uint32_t>>>*)image), title);\
            break;\
        case array2d_type::Int8:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int8_t>>>*)image), title);\
            break;\
        case array2d_type::Int16:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int16_t>>>*)image), title);\
            break;\
        case array2d_type::Int32:\
            ret = new image_window(*((matrix_op<ELEMENT<array2d<int32_t>>>*)image), title);\
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

#define image_window_new_matrix_op_template3_sub(img, element, templateRows, templateColumns, ret) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 0, 0>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 0, 0>>>*>(img);\
        ret = new image_window(op);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 0, 1>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 0, 1>>>*>(img);\
        ret = new image_window(op);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 31, 1>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 31, 1>>>*>(img);\
        ret = new image_window(op);\
    }\
} while (0)

#define image_window_new_matrix_op_template3(type, img, templateRows, templateColumns, ret) \
do { \
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            image_window_new_matrix_op_template3_sub(img, uint8_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::UInt16:\
            image_window_new_matrix_op_template3_sub(img, uint16_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::UInt32:\
            image_window_new_matrix_op_template3_sub(img, uint32_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int8:\
            image_window_new_matrix_op_template3_sub(img, int8_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int16:\
            image_window_new_matrix_op_template3_sub(img, int16_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Int32:\
            image_window_new_matrix_op_template3_sub(img, int32_t, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Float:\
            image_window_new_matrix_op_template3_sub(img, float, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::Double:\
            image_window_new_matrix_op_template3_sub(img, double, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::RgbPixel:\
            image_window_new_matrix_op_template3_sub(img, rgb_pixel, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::HsiPixel:\
            image_window_new_matrix_op_template3_sub(img, hsi_pixel, templateRows, templateColumns, ret);\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            image_window_new_matrix_op_template3_sub(img, rgb_alpha_pixel, templateRows, templateColumns, ret);\
            break;\
        default:\
            break;\
    }\
    ret = nullptr;\
} while (0)

#define image_window_new_matrix_op_template4_sub(img, element, templateRows, templateColumns, title, ret) \
do { \
    if (templateRows == 0 && templateColumns == 0)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 0, 0>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 0, 0>>>*>(img);\
        ret = new image_window(op, title);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 0, 1>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 0, 1>>>*>(img);\
        ret = new image_window(op, title);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        matrix_op<ELEMENT<dlib::matrix<element, 31, 1>>>& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<element, 31, 1>>>*>(img);\
        ret = new image_window(op, title);\
    }\
} while (0)

#define image_window_new_matrix_op_template4(type, img, templateRows, templateColumns, title, ret) \
do { \
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            image_window_new_matrix_op_template4_sub(img, uint8_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::UInt16:\
            image_window_new_matrix_op_template4_sub(img, uint16_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::UInt32:\
            image_window_new_matrix_op_template4_sub(img, uint32_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::Int8:\
            image_window_new_matrix_op_template4_sub(img, int8_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::Int16:\
            image_window_new_matrix_op_template4_sub(img, int16_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::Int32:\
            image_window_new_matrix_op_template4_sub(img, int32_t, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::Float:\
            image_window_new_matrix_op_template4_sub(img, float, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::Double:\
            image_window_new_matrix_op_template4_sub(img, double, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::RgbPixel:\
            image_window_new_matrix_op_template4_sub(img, rgb_pixel, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::HsiPixel:\
            image_window_new_matrix_op_template4_sub(img, hsi_pixel, templateRows, templateColumns, title, ret);\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            image_window_new_matrix_op_template4_sub(img, rgb_alpha_pixel, templateRows, templateColumns, title, ret);\
            break;\
        default:\
            break;\
    }\
    ret = nullptr;\
} while (0)

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
        case array2d_type::UInt32:\
            window->set_image(*((matrix_op<ELEMENT<array2d<uint32_t>>>*)image));\
            break;\
        case array2d_type::Int8:\
            window->set_image(*((matrix_op<ELEMENT<array2d<int8_t>>>*)image));\
            break;\
        case array2d_type::Int16:\
            window->set_image(*((matrix_op<ELEMENT<array2d<int16_t>>>*)image));\
            break;\
        case array2d_type::Int32:\
            window->set_image(*((matrix_op<ELEMENT<array2d<int32_t>>>*)image));\
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

#define image_window_set_image_matrix_op_op_join_rows_template(ret, window, type, image) \
do { \
    switch(type)\
    {\
        case matrix_element_type::UInt8:\
            window->set_image(*((matrix_op<op_join_rows<matrix<uint8_t, 0, 0>, matrix<uint8_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::UInt16:\
            window->set_image(*((matrix_op<op_join_rows<matrix<uint16_t, 0, 0>, matrix<uint16_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::UInt32:\
            window->set_image(*((matrix_op<op_join_rows<matrix<uint32_t, 0, 0>, matrix<uint32_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::Int8:\
            window->set_image(*((matrix_op<op_join_rows<matrix<int8_t, 0, 0>, matrix<int8_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::Int16:\
            window->set_image(*((matrix_op<op_join_rows<matrix<int16_t, 0, 0>, matrix<int16_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::Int32:\
            window->set_image(*((matrix_op<op_join_rows<matrix<int32_t, 0, 0>, matrix<int32_t, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::Float:\
            window->set_image(*((matrix_op<op_join_rows<matrix<float, 0, 0>, matrix<float, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::Double:\
            window->set_image(*((matrix_op<op_join_rows<matrix<double, 0, 0>, matrix<double, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::RgbPixel:\
            window->set_image(*((matrix_op<op_join_rows<matrix<rgb_pixel, 0, 0>, matrix<rgb_pixel, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::HsiPixel:\
            window->set_image(*((matrix_op<op_join_rows<matrix<hsi_pixel, 0, 0>, matrix<hsi_pixel, 0, 0>>>*)image));\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            window->set_image(*((matrix_op<op_join_rows<matrix<rgb_alpha_pixel, 0, 0>, matrix<rgb_alpha_pixel, 0, 0>>>*)image));\
            break;\
        default:\
            ret = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion template

#pragma region image_window

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
        case array2d_type::UInt32:
            return new image_window(*((array2d<uint32_t>*)image));
        case array2d_type::Int8:
            return new image_window(*((array2d<int8_t>*)image));
        case array2d_type::Int16:
            return new image_window(*((array2d<int16_t>*)image));
        case array2d_type::Int32:
            return new image_window(*((array2d<int32_t>*)image));
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
        case array2d_type::UInt32:
            return new image_window(*((array2d<uint32_t>*)image), title);
        case array2d_type::Int8:
            return new image_window(*((array2d<int8_t>*)image), title);
        case array2d_type::Int16:
            return new image_window(*((array2d<int16_t>*)image), title);
        case array2d_type::Int32:
            return new image_window(*((array2d<int32_t>*)image), title);
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

DLLEXPORT void* image_window_new_matrix_op3(element_type etype, 
                                            matrix_element_type type,
                                            void* img, 
                                            const int templateRows,
                                            const int templateColumns)
{   
    void* ret = nullptr;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_new_matrix_op_template3(type, img, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_new_matrix_op_template3(type, img, templateRows, templateColumns, ret);
            #undef ELEMENT
            break;
        default:
            break;
    }

    return ret;
}

DLLEXPORT void* image_window_new_matrix_op4(element_type etype, 
                                            matrix_element_type type,
                                            void* img, 
                                            const int templateRows,
                                            const int templateColumns,
                                            const char* title)
{   
    void* ret = nullptr;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_new_matrix_op_template4(type, img, templateRows, templateColumns, title, ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_new_matrix_op_template4(type, img, templateRows, templateColumns, title, ret);
            #undef ELEMENT
            break;
        default:
            break;
    }

    return ret;
}

#pragma endregion new

DLLEXPORT void image_window_delete(image_window* window)
{
	delete window;
}

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
        case array2d_type::UInt32:
            window->add_overlay(*r, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(*r, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(*r, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(*r, *((int32_t*)p));
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_add_overlay2(image_window* window, std::vector<rectangle*>* r, array2d_type type, void* p)
{
    int err = ERR_OK;
    
    std::vector<rectangle*>& vector = *(static_cast<std::vector<rectangle*>*>(r));
    std::vector<rectangle> tmpRects;
    for (int index = 0 ; index < vector.size(); index++)
    {
        dlib::rectangle& rect = *(vector[index]);
        tmpRects.push_back(rect);
    }

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(tmpRects, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(tmpRects, *((uint16_t*)p));
            break;
        case array2d_type::UInt32:
            window->add_overlay(tmpRects, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(tmpRects, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(tmpRects, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(tmpRects, *((int32_t*)p));
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
        case array2d_type::UInt32:
            window->add_overlay(*r, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(*r, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(*r, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(*r, *((int32_t*)p));
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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

DLLEXPORT bool image_window_is_closed(image_window* window)
{
    return window->is_closed();
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
        case array2d_type::UInt32:
            window->set_image(*((array2d<uint32_t>*)image));
            break;
        case array2d_type::Int8:
            window->set_image(*((array2d<int8_t>*)image));
            break;
        case array2d_type::Int16:
            window->set_image(*((array2d<int16_t>*)image));
            break;
        case array2d_type::Int32:
            window->set_image(*((array2d<int32_t>*)image));
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
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
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
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_set_image_matrix_op_array2d(image_window* window, element_type etype, array2d_type type, void* image)
{
    int err = ERR_OK;

    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            image_window_set_image_matrix_op_template(err, window, type, image);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            image_window_set_image_matrix_op_template(err, window, type, image);
            #undef ELEMENT
            break;
        case element_type::OpArray2dToMat:
            #define ELEMENT dlib::op_array2d_to_mat
            image_window_set_image_matrix_op_template(err, window, type, image);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int image_window_set_image_matrix_op_matrix(image_window* window, element_type etype, matrix_element_type type, void* image)
{
    int err = ERR_OK;

    switch(etype)
    {
        case element_type::OpJoinRows:
            #define ELEMENT dlib::op_join_rows
            image_window_set_image_matrix_op_op_join_rows_template(err, window, type, image);
            #undef ELEMENT
            break;
        default:
            err = ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion set_image

#pragma region image_window::overlay_line

DLLEXPORT image_window::overlay_line* image_window_overlay_line_new()
{
	return new image_window::overlay_line();
}

DLLEXPORT image_window::overlay_line* image_window_overlay_line_new_rgb(dlib::point* p1, dlib::point* p2, dlib::rgb_pixel pixel)
{
	return new image_window::overlay_line(*p1, *p2, pixel);
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

#pragma endregion image_window::overlay_line

#pragma region image_window::get_next_double_click 

DLLEXPORT bool image_window_get_next_double_click(image_window* window, dlib::point** point) 
{
    dlib::point p;
    bool ret = window->get_next_double_click(p);
    *point = new dlib::point(p);
    return ret;
} 

DLLEXPORT bool image_window_get_next_double_click2(image_window* window, dlib::point** point, unsigned long* mouse_button) 
{
    dlib::point p;
    unsigned long m;
    bool ret = window->get_next_double_click(p, m);
    *point = new dlib::point(p);
    *mouse_button = m;
    return ret;
} 

#pragma endregion image_window::get_next_double_click 

#pragma endregion image_window

#pragma region perspective_window

#pragma endregion perspective_window

DLLEXPORT perspective_window* perspective_window_new()
{
	return new perspective_window();
}

DLLEXPORT perspective_window* perspective_window_new2(std::vector<dlib::vector<double>*>* point_cloud)
{
    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(point_cloud));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = point_cloud->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

	return new perspective_window(tmp);
}

DLLEXPORT perspective_window* perspective_window_new3(std::vector<dlib::vector<double>*>* point_cloud, const char* title)
{
    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(point_cloud));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = point_cloud->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

	return new perspective_window(tmp, title);
}

DLLEXPORT int perspective_window_add_overlay(perspective_window* window, dlib::vector<double>* p1, dlib::vector<double>* p2, array2d_type type, void* p)
{
    int err = ERR_OK;

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(*p1, *p2, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(*p1, *p2, *((uint16_t*)p));
            break;
        case array2d_type::UInt32:
            window->add_overlay(*p1, *p2, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(*p1, *p2, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(*p1, *p2, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(*p1, *p2, *((int32_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(*p1, *p2, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(*p1, *p2, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(*p1, *p2, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(*p1, *p2, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(*p1, *p2, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int perspective_window_add_overlay2(perspective_window* window, std::vector<dlib::vector<double>*>* points)
{
    int err = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);
    
    return err;
}

DLLEXPORT int perspective_window_add_overlay3(perspective_window* window, std::vector<dlib::vector<double>*>* points, array2d_type type, void* p)
{
    int err = ERR_OK;

    std::vector<dlib::vector<double>*>& vector = *(static_cast<std::vector<dlib::vector<double>*>*>(points));
    std::vector<dlib::vector<double>> tmp;
    for(int i = 0, end = points->size(); i < end; i++)
    {
        dlib::vector<double>& p = *(static_cast<dlib::vector<double>*>(vector[i]));
        tmp.push_back(p);
    }

    switch(type)
    {
        case array2d_type::UInt8:
            window->add_overlay(tmp, *((uint8_t*)p));
            break;
        case array2d_type::UInt16:
            window->add_overlay(tmp, *((uint16_t*)p));
            break;
        case array2d_type::UInt32:
            window->add_overlay(tmp, *((uint32_t*)p));
            break;
        case array2d_type::Int8:
            window->add_overlay(tmp, *((int8_t*)p));
            break;
        case array2d_type::Int16:
            window->add_overlay(tmp, *((int16_t*)p));
            break;
        case array2d_type::Int32:
            window->add_overlay(tmp, *((int32_t*)p));
            break;
        case array2d_type::Float:
            window->add_overlay(tmp, *((float*)p));
            break;
        case array2d_type::Double:
            window->add_overlay(tmp, *((double*)p));
            break;
        case array2d_type::RgbPixel:
            window->add_overlay(tmp, *((dlib::rgb_pixel*)p));
            break;
        case array2d_type::HsiPixel:
            window->add_overlay(tmp, *((dlib::hsi_pixel*)p));
            break;
        case array2d_type::RgbAlphaPixel:
            window->add_overlay(tmp, *((dlib::rgb_alpha_pixel*)p));
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int perspective_window_add_overlay4(perspective_window* window, std::vector<perspective_window::overlay_dot*>* overlay)
{
    int err = ERR_OK;

    std::vector<perspective_window::overlay_dot*>& vector = *(static_cast<std::vector<perspective_window::overlay_dot*>*>(overlay));
    std::vector<perspective_window::overlay_dot> tmp;
    for(int i = 0, end = overlay->size(); i < end; i++)
    {
        perspective_window::overlay_dot& p = *(static_cast<perspective_window::overlay_dot*>(vector[i]));
        tmp.push_back(p);
    }

    window->add_overlay(tmp);
    
    return err;
}

DLLEXPORT void perspective_window_delete(perspective_window* window)
{
	delete window;
}

#pragma region perspective_window::overlay_dot

DLLEXPORT perspective_window::overlay_dot* perspective_window_overlay_dot_new(dlib::vector<double>* v)
{
    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(v));
	return new perspective_window::overlay_dot(tmp);
}

DLLEXPORT perspective_window::overlay_dot* perspective_window_overlay_dot_new2(dlib::vector<double>* v, array2d_type type, void* p)
{
    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(v));

    switch(type)
    {
        case array2d_type::UInt8:
            return new perspective_window::overlay_dot(tmp, *((uint8_t*)p));
        case array2d_type::UInt16:
            return new perspective_window::overlay_dot(tmp, *((uint16_t*)p));
        case array2d_type::UInt32:
            return new perspective_window::overlay_dot(tmp, *((uint32_t*)p));
        case array2d_type::Int8:
            return new perspective_window::overlay_dot(tmp, *((int8_t*)p));
        case array2d_type::Int16:
            return new perspective_window::overlay_dot(tmp, *((int16_t*)p));
        case array2d_type::Int32:
            return new perspective_window::overlay_dot(tmp, *((int32_t*)p));
        case array2d_type::Float:
            return new perspective_window::overlay_dot(tmp, *((float*)p));
        case array2d_type::Double:
            return new perspective_window::overlay_dot(tmp, *((double*)p));
        case array2d_type::RgbPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::rgb_pixel*)p));
        case array2d_type::HsiPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::hsi_pixel*)p));
        case array2d_type::RgbAlphaPixel:
            return new perspective_window::overlay_dot(tmp, *((dlib::rgb_alpha_pixel*)p));
        default:
            return nullptr;
    }
}

DLLEXPORT bool perspective_window_overlay_dot_color(perspective_window::overlay_dot* dot, dlib::rgb_alpha_pixel* color)
{
    memcpy(color, &(dot->color), sizeof(dlib::rgb_alpha_pixel));
    return true;
}

DLLEXPORT bool perspective_window_overlay_dot_p(perspective_window::overlay_dot* dot, dlib::vector<double>** ret)
{
    *ret = new dlib::vector<double>(dot->p);
    return true;
}

DLLEXPORT void perspective_window_overlay_dot_delete(perspective_window::overlay_dot* dot)
{
	delete dot;
}

#pragma endregion perspective_window::overlay_dot

#endif

#endif