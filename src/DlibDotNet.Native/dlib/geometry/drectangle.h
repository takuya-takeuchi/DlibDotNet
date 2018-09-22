#ifndef _CPP_GEOMETRY_RECTANGLE_H_
#define _CPP_GEOMETRY_RECTANGLE_H_

#include "../export.h"
#include <dlib/geometry/drectangle.h>
#include <dlib/geometry/rectangle.h>
#include <dlib/geometry/vector.h>

using namespace dlib;

DLLEXPORT void* drectangle_new()
{
    return new drectangle();
}
 
DLLEXPORT void* drectangle_new1(const double left, const double top, const double right, const double bottom)
{
     return new drectangle(left, top, right, bottom);
}

DLLEXPORT void* drectangle_new2(const dlib::vector<double, 2>* p)
{
    return new drectangle(*p);
}

DLLEXPORT void* drectangle_new3(const dlib::vector<double, 2>* p1, const dlib::vector<double, 2>* p2)
{
    return new drectangle(*p1, *p2);
}

DLLEXPORT void* drectangle_new4(const dlib::drectangle* rect)
{
    return new drectangle(*rect);
}

DLLEXPORT void* drectangle_operator(dlib::drectangle* obj)
{
    dlib::rectangle ret = (*obj);
    return new dlib::rectangle(ret);
}

DLLEXPORT double drectangle_left(drectangle* rect)
{
    return rect->left();
}

DLLEXPORT double drectangle_top(drectangle* rect)
{
    return rect->top();
}

DLLEXPORT double drectangle_right(drectangle* rect)
{
    return rect->right();
}

DLLEXPORT double drectangle_bottom(drectangle* rect)
{
    return rect->bottom();
}

DLLEXPORT void* drectangle_tl_corner(drectangle* rect)
{
    return new dpoint(rect->tl_corner());
}

DLLEXPORT void* drectangle_bl_corner(drectangle* rect)
{
    return new dpoint(rect->bl_corner());
}

DLLEXPORT void* drectangle_tr_corner(drectangle* rect)
{
    return new dpoint(rect->tr_corner());
}

DLLEXPORT void* drectangle_br_corner(drectangle* rect)
{
    return new dpoint(rect->br_corner());
}

DLLEXPORT double drectangle_width(drectangle* rect)
{
    return rect->width();
}

DLLEXPORT double drectangle_height(drectangle* rect)
{
    return rect->height();
}

DLLEXPORT double drectangle_area(drectangle* rect)
{
    return rect->area();
}

DLLEXPORT bool drectangle_is_empty(drectangle* rect)
{
    return rect->is_empty();
}

DLLEXPORT void* drectangle_intersect(drectangle* rect, drectangle* rhs)
{
    const drectangle result = rect->intersect(*rhs);
    return new drectangle(result);
}

DLLEXPORT bool drectangle_contains(drectangle* rect, dpoint* point)
{
    return rect->contains(*point);
}

DLLEXPORT bool drectangle_contains2(drectangle* rect, drectangle* rhs)
{
    return rect->contains(*rhs);
}

DLLEXPORT void* drectangle_center(drectangle* rect, drectangle* rhs)
{
    const dpoint result = center(*rect);
    return new dpoint(result);
}

DLLEXPORT void* drectangle_dcenter(drectangle* rect, drectangle* rhs)
{
    const dpoint result = dcenter(*rect);
    return new dpoint(result);
}

DLLEXPORT void* drectangle_centered_rect(drectangle* rect, double width, double height)
{
    const drectangle result = dlib::centered_drect(*rect, width, height);
    return new drectangle(result);
}

DLLEXPORT void* drectangle_centered_rect1(dpoint* p, double width, double height)
{
    const drectangle result = dlib::centered_drect(*p, width, height);
    return new drectangle(result);
}

DLLEXPORT void drectangle_delete(const drectangle* rect)
{
    delete rect;
}

DLLEXPORT drectangle* drectangle_set_aspect_ratio(drectangle* rect, const double ratio)
{
    return new drectangle(dlib::set_aspect_ratio(*rect, ratio));
}

DLLEXPORT drectangle* drectangle_translate_rect(drectangle* rect, point* p)
{
    drectangle result = dlib::translate_rect(*rect, *p);
    return new dlib::drectangle(result);
}

DLLEXPORT drectangle* drectangle_translate_rect_d(drectangle* rect, dpoint* p)
{
    drectangle result = dlib::translate_rect(*rect, *p);
    return new dlib::drectangle(result);
}

#pragma region operator

DLLEXPORT void* drectangle_operator_add(drectangle* rect, drectangle* rhs)
{
    const drectangle result = *rect + *rhs;
    return new drectangle(result);
}

// DLLEXPORT void* drectangle_operator_sub(drectangle* rect, drectangle* rhs)
// {
//     const drectangle result = *rect - *rhs;
//     return new drectangle(result);
// }

DLLEXPORT void* drectangle_operator_mul(drectangle* rect, double scale)
{
    const dlib::drectangle result = (*rect) * (scale);
    return new dlib::drectangle(result);
}

DLLEXPORT void* drectangle_operator_div(drectangle* rect, double scale)
{
    const dlib::drectangle result = (*rect) / (scale);
    return new dlib::drectangle(result);
}

DLLEXPORT bool drectangle_operator_equal(drectangle* rect, drectangle* rhs)
{
    return *rect == *rhs;
}

#pragma endregion operator

#endif