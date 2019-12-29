#ifndef _CPP_DRAW_H_
#define _CPP_DRAW_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/image_transforms/draw.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix/matrix.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix

#define draw_line_template(__TYPE__, error, type, ...) \
dlib::draw_line(*((array2d<__TYPE__>*)image), *p1, *p2, *((__TYPE__*)p));

#define draw_line_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::draw_line(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image), *p1, *p2, *((__TYPE__*)p));

#define draw_rectangle_template(__TYPE__, error, type, ...) \
dlib::draw_rectangle(*((array2d<__TYPE__>*)image), *rect, *((__TYPE__*)p), thickness);

#define draw_rectangle_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::draw_rectangle(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image), *rect, *((__TYPE__*)p), thickness);

#define fill_rect_template(__TYPE__, error, type, ...) \
dlib::fill_rect(*((array2d<__TYPE__>*)image), *rect, *((__TYPE__*)p));

#define fill_rect_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
dlib::fill_rect(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image), *rect, *((__TYPE__*)p));

#define tile_images_template(__TYPE__, error, type, ...) \
auto& img = *((dlib::array<dlib::array2d<__TYPE__>>*)images);\
*ret_image = new dlib::matrix<__TYPE__>(dlib::tile_images(img));\

#define tile_images_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& tmp = *(static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(images));\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> tmp_images;\
for (int index = 0; index < tmp.size(); index++)\
{\
    auto& matrix = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(tmp[index]));\
    tmp_images.push_back(matrix);\
}\
*ret_image = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(dlib::tile_images(tmp_images));\

#pragma endregion template

DLLEXPORT int draw_line(array2d_type type,
                        void* image,
                        dlib::point* p1,
                        dlib::point* p2,
                        void* p)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     draw_line_template,
                     image,
                     p1,
                     p2,
                     p);

    return error;
}

DLLEXPORT int draw_line_matrix(matrix_element_type type,
                               void* image,
                               dlib::point* p1,
                               dlib::point* p2,
                               void* p)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    draw_line_matrix_template,
                    0,
                    0,
                    image,
                    p1,
                    p2,
                    p);

    return error;
}

DLLEXPORT int draw_rectangle(array2d_type type,
                             void* image,
                             dlib::rectangle* rect,
                             void* p,
                             unsigned int thickness)
{
    int error = ERR_OK;
    array2d_template(type,
                     error,
                     draw_rectangle_template,
                     image,
                     rect,
                     p,
                     thickness);
    return error;
}

DLLEXPORT int draw_rectangle_matrix(matrix_element_type type,
                                    void* image,
                                    dlib::rectangle* rect,
                                    void* p,
                                    unsigned int thickness)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    draw_rectangle_matrix_template,
                    0,
                    0,
                    image,
                    rect,
                    p,
                    thickness);

    return error;
}

DLLEXPORT int fill_rect(array2d_type type,
                        void* image,
                        dlib::rectangle* rect,
                        void* p)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     fill_rect_template,
                     image,
                     rect,
                     p);

    return error;
}

DLLEXPORT int fill_rect_matrix(matrix_element_type type,
                               void* image,
                               dlib::rectangle* rect,
                               void* p)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    fill_rect_matrix_template,
                    0,
                    0,
                    image,
                    rect,
                    p);

    return error;
}

DLLEXPORT int tile_images(array2d_type type, void* images, void** ret_image)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     tile_images_template,
                     images,
                     ret_image);

    return error;
}

DLLEXPORT int tile_images_matrix(matrix_element_type type, void* images, void** ret_image)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    tile_images_matrix_template,
                    0,
                    0,
                    images,
                    ret_image);

    return error;
}

#endif