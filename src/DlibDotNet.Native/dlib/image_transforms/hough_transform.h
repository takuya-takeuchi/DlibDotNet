#ifndef _CPP_HOUGH_TRANSFORM_H_
#define _CPP_HOUGH_TRANSFORM_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_transforms.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ELEMENT_OUT element
#undef ELEMENT_OUT

#define hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle) \
do { \
    ret = ERR_OK;\
    switch(in_type)\
    {\
        case array2d_type::UInt8:\
            (*obj)(*((array2d<uint8_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::UInt16:\
            (*obj)(*((array2d<uint16_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::UInt32:\
            (*obj)(*((array2d<uint32_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int8:\
            (*obj)(*((array2d<int8_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int16:\
            (*obj)(*((array2d<int16_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Int32:\
            (*obj)(*((array2d<int32_t>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Float:\
            (*obj)(*((array2d<float>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::Double:\
            (*obj)(*((array2d<double>*)in_img), *rectangle, *((array2d<ELEMENT_OUT>*)out_img));\
            break;\
        case array2d_type::RgbPixel:\
        case array2d_type::HsiPixel:\
        case array2d_type::RgbAlphaPixel:\
        default:\
            ret = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

DLLEXPORT dlib::hough_transform* hough_transform_new(unsigned int size)
{
    return new dlib::hough_transform(size);
}

DLLEXPORT bool hough_transform_nc(dlib::hough_transform* obj, int* ret)
{
    *ret = obj->nc();
    return true;
}

DLLEXPORT bool hough_transform_nr(dlib::hough_transform* obj, int* ret)
{
    *ret = obj->nr();
    return true;
}

DLLEXPORT bool hough_transform_size(dlib::hough_transform* obj, unsigned int* ret)
{
    *ret = obj->size();
    return true;
}

DLLEXPORT bool hough_transform_get_rect(dlib::hough_transform* obj, dlib::rectangle** ret)
{
    dlib::rectangle rect = dlib::get_rect(*obj);
    *ret = new dlib::rectangle(rect);
    return true;
}

DLLEXPORT std::pair<dlib::point*, dlib::point*>* hough_transform_get_line(
    dlib::hough_transform* obj,
    dlib::point* p)
{
    std::pair<dlib::point, dlib::point> line = obj->get_line(*p);
    return new std::pair<dlib::point*, dlib::point*>(new dlib::point(line.first), new dlib::point(line.second));
}

DLLEXPORT int hough_transform_get_best_hough_point(dlib::hough_transform* obj, dlib::point* p, array2d_type type, void* img, dlib::point** point)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<uint8_t>*)img)));
            break;
        case array2d_type::UInt16:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<uint16_t>*)img)));
            break;
        case array2d_type::UInt32:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<uint32_t>*)img)));
            break;
        case array2d_type::Int8:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<int8_t>*)img)));
            break;
        case array2d_type::Int16:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<int16_t>*)img)));
            break;
        case array2d_type::Int32:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<int32_t>*)img)));
            break;
        case array2d_type::Float:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<float>*)img)));
            break;
        case array2d_type::Double:
            *point = new dlib::point(obj->get_best_hough_point(*p, *((dlib::array2d<double>*)img)));
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
			break;;
    }

    return err;
}

DLLEXPORT int hough_transform_operator(
    dlib::hough_transform* obj,
    array2d_type in_type,
    void* in_img,
    array2d_type out_type,
    void* out_img,
    dlib::rectangle* rectangle)
{
    int ret = ERR_OK;
    switch(out_type)
    {
        case array2d_type::UInt8:
            #define ELEMENT_OUT uint8_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt16:
            #define ELEMENT_OUT uint16_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::UInt32:
            #define ELEMENT_OUT uint32_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int8:
            #define ELEMENT_OUT int8_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int16:
            #define ELEMENT_OUT int16_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Int32:
            #define ELEMENT_OUT int32_t
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Float:
            #define ELEMENT_OUT float
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::Double:
            #define ELEMENT_OUT double
            hough_transform_operator_template(ret, obj, in_type, in_img, out_img, rectangle);
            #undef ELEMENT_OUT
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            ret = ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;;
    }

    return ret;
}

DLLEXPORT void hough_transform_delete(dlib::hough_transform* obj)
{
    delete obj;
}

#endif