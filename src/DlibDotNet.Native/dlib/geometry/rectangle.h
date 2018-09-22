#ifndef _CPP_GEOMETRY_RECTANGLE_H_
#define _CPP_GEOMETRY_RECTANGLE_H_

#include "../export.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>
#include <dlib/image_io.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ELEMENT_IN element
#undef ELEMENT_IN

#define rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 0>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 0>*>(img);\
        *ret = new rectangle(dlib::get_rect(m));\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 0, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 0, 1>*>(img);\
        *ret = new rectangle(dlib::get_rect(m));\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        dlib::matrix<ELEMENT_IN, 31, 1>& m = *static_cast<dlib::matrix<ELEMENT_IN, 31, 1>*>(img);\
        *ret = new rectangle(dlib::get_rect(m));\
    }\
} while (0)

#pragma endregion template

DLLEXPORT rectangle* rectangle_new()
{
    return new rectangle();
}
 
DLLEXPORT rectangle* rectangle_new1(const int left, const int top, const int right, const int bottom)
{
     return new rectangle(left, top, right, bottom);
}

DLLEXPORT rectangle* rectangle_new2(const unsigned int width, const unsigned int height)
{
    return new rectangle(width, height);
}

DLLEXPORT rectangle* rectangle_new3(const point* p)
{
    return new dlib::rectangle(*p);
}
    
DLLEXPORT rectangle* rectangle_new4(const point* p1, const point* p2)
{
    return new rectangle(*p1, *p2);
}

DLLEXPORT void rectangle_delete(const rectangle* rect)
{
    delete rect;
}

DLLEXPORT int rectangle_left(rectangle* rect)
{
    return rect->left();
}

DLLEXPORT int rectangle_top(rectangle* rect)
{
    return rect->top();
}

DLLEXPORT int rectangle_right(rectangle* rect)
{
    return rect->right();
}

DLLEXPORT int rectangle_bottom(rectangle* rect)
{
    return rect->bottom();
}

DLLEXPORT void rectangle_set_left(rectangle* rect, int value)
{
    rect->set_left(value);
}

DLLEXPORT void rectangle_set_top(rectangle* rect, int value)
{
    rect->set_top(value);
}

DLLEXPORT void rectangle_set_right(rectangle* rect, int value)
{
    rect->set_right(value);
}

DLLEXPORT void rectangle_set_bottom(rectangle* rect, int value)
{
    rect->set_bottom(value);
}

DLLEXPORT point* rectangle_tl_corner(rectangle* rect)
{
    return new point(rect->tl_corner());
}

DLLEXPORT point* rectangle_bl_corner(rectangle* rect)
{
    return new point(rect->bl_corner());
}

DLLEXPORT point* rectangle_tr_corner(rectangle* rect)
{
    return new point(rect->tr_corner());
}

DLLEXPORT point* rectangle_br_corner(rectangle* rect)
{
    return new point(rect->br_corner());
}

DLLEXPORT unsigned int rectangle_width(rectangle* rect)
{
    return rect->width();
}

DLLEXPORT unsigned int rectangle_height(rectangle* rect)
{
    return rect->height();
}

DLLEXPORT unsigned int rectangle_area(rectangle* rect)
{
    return rect->area();
}

DLLEXPORT bool rectangle_is_empty(rectangle* rect)
{
    return rect->is_empty();
}

DLLEXPORT bool rectangle_contains(rectangle* rect, point* point)
{
    return rect->contains(*point);
}

DLLEXPORT bool rectangle_contains1(rectangle* rect, int x, int y)
{
    return rect->contains(x, y);
}

DLLEXPORT rectangle* rectangle_centered_rect(int x, int y, unsigned int width, unsigned int height)
{
    const rectangle result = centered_rect(x, y, width, height);
    return new rectangle(result);
}

DLLEXPORT rectangle* rectangle_centered_rect1(point* p, unsigned int width, unsigned int height)
{
    const rectangle result = centered_rect(*p, width, height);
    return new rectangle(result);
}

DLLEXPORT rectangle* rectangle_centered_rect2(rectangle* rect, unsigned int width, unsigned int height)
{
    const rectangle result = centered_rect(*rect, width, height);
    return new rectangle(result);
}

DLLEXPORT rectangle* rectangle_intersect(rectangle* rect, rectangle* target)
{
    const rectangle result = rect->intersect(*target);
    return new rectangle(result);
}

DLLEXPORT point* rectangle_center(rectangle* rect)
{
    const point result = center(*rect);
    return new point(result);
}

DLLEXPORT dpoint* rectangle_dcenter(rectangle* rect)
{
    const dpoint result = dcenter(*rect);
    return new dpoint(result);
}

DLLEXPORT int rectangle_get_rect(array2d_type img_type, void* img, rectangle** rect)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            *rect = new dlib::rectangle(get_rect(*((array2d<uint8_t>*)img)));
            break;
        case array2d_type::UInt16:
            *rect = new dlib::rectangle(get_rect(*((array2d<uint16_t>*)img)));
            break;
        case array2d_type::UInt32:
            *rect = new dlib::rectangle(get_rect(*((array2d<uint32_t>*)img)));
            break;
        case array2d_type::Int8:
            *rect = new dlib::rectangle(get_rect(*((array2d<int8_t>*)img)));
            break;
        case array2d_type::Int16:
            *rect = new dlib::rectangle(get_rect(*((array2d<int16_t>*)img)));
            break;
        case array2d_type::Int32:
            *rect = new dlib::rectangle(get_rect(*((array2d<int32_t>*)img)));
            break;
        case array2d_type::Float:
            *rect = new dlib::rectangle(get_rect(*((array2d<float>*)img)));
            break;
        case array2d_type::Double:        
            *rect = new dlib::rectangle(get_rect(*((array2d<double>*)img)));
            break;
        case array2d_type::RgbPixel:
            *rect = new dlib::rectangle(get_rect(*((array2d<rgb_pixel>*)img)));
            break;
        case array2d_type::HsiPixel:
            *rect = new dlib::rectangle(get_rect(*((array2d<hsi_pixel>*)img)));
            break;
        case array2d_type::RgbAlphaPixel:
            *rect = new dlib::rectangle(get_rect(*((array2d<rgb_alpha_pixel>*)img)));
            break;
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int rectangle_get_rect_matrix(matrix_element_type type, void* img, const int templateRows, const int templateColumns, rectangle** ret)
{
    int err = ERR_OK;

    switch(type)
    {
		case matrix_element_type::UInt8:
            #define ELEMENT_IN uint8_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt16:
            #define ELEMENT_IN uint16_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::UInt32:
            #define ELEMENT_IN uint32_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int8:
            #define ELEMENT_IN int8_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int16:
            #define ELEMENT_IN int16_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Int32:
            #define ELEMENT_IN int32_t
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Float:
            #define ELEMENT_IN float
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::Double:
            #define ELEMENT_IN double
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbPixel:
            #define ELEMENT_IN rgb_pixel
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::HsiPixel:
            #define ELEMENT_IN hsi_pixel
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
		case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT_IN rgb_alpha_pixel
            rectangle_get_rect_matrix_template(img, templateRows, templateColumns, ret);
            #undef ELEMENT_IN
			break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT rectangle* rectangle_set_aspect_ratio(rectangle* rect, const double ratio)
{
    return new rectangle(dlib::set_aspect_ratio(*rect, ratio));
}

DLLEXPORT rectangle* rectangle_translate_rect(rectangle* rect, point* p)
{
    rectangle result = dlib::translate_rect(*rect, *p);
    return new dlib::rectangle(result);
}

DLLEXPORT rectangle* rectangle_translate_rect_xy(rectangle* rect, int x, int y)
{
    rectangle result = dlib::translate_rect(*rect, x, y);
    return new dlib::rectangle(result);
}

#pragma region operator

DLLEXPORT void* rectangle_operator_add(rectangle* rect, rectangle* rhs)
{
    const rectangle result = *rect + *rhs;
    return new rectangle(result);
}

DLLEXPORT void* rectangle_operator_add_point(rectangle* rect, point* rhs)
{
    // rectangle r(*rect);
    // point p(*rhs);
    // r += p;    
    *rect = *rect + rectangle(*rhs);
    return new rectangle(*rect);
}

// DLLEXPORT void* rectangle_operator_sub(rectangle* rect, rectangle* rhs)
// {
//     const rectangle result = *rect - *rhs;
//     return new rectangle(result);
// }

DLLEXPORT bool rectangle_operator_equal(rectangle* rect, rectangle* rhs)
{
    return *rect == *rhs;
}

#pragma endregion operator

#endif