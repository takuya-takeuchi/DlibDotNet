#ifndef _CPP_HOUGH_TRANSFORM_H_
#define _CPP_HOUGH_TRANSFORM_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_transforms.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ELEMENT_OUT element
#undef ELEMENT_OUT

#define assign_image_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& o = *obj;\
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
o(in_, *rectangle, out_);\

#define hough_transform_get_best_hough_point_template(__TYPE__, error, type, ...) \
auto& in_ = *((dlib::array2d<__TYPE__>*)img);\
*point = new dlib::point(obj->get_best_hough_point(*p, in_));

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

DLLEXPORT int hough_transform_get_best_hough_point(dlib::hough_transform* obj,
                                                   dlib::point* p,
                                                   array2d_type type,
                                                   void* img,
                                                   dlib::point** point)
{
    int error = ERR_OK;

    array2d_numeric_template(type,
                             error,
                             hough_transform_get_best_hough_point_template,
                             obj,
                             p,
                             img,
                             point);

    return error;
}

DLLEXPORT int hough_transform_operator(dlib::hough_transform* obj,
                                       array2d_type in_type,
                                       void* in_img,
                                       array2d_type out_type,
                                       void* out_img,
                                       dlib::rectangle* rectangle)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_numeric_inout_in_template(type,
                                      error,
                                      array2d_numeric_inout_out_template,
                                      assign_image_template,
                                      subtype,
                                      in_img,
                                      out_img,
                                      rectangle);
    return error;
}

DLLEXPORT void hough_transform_delete(dlib::hough_transform* obj)
{
    delete obj;
}

#endif