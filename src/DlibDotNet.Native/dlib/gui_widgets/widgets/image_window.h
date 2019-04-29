#ifndef DLIB_NO_GUI_SUPPORT

#ifndef _CPP_WIDGETS_IMAGE_WINDOW_H_
#define _CPP_WIDGETS_IMAGE_WINDOW_H_

#include "../../export.h"
#include <dlib/gui_widgets/widgets.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define image_window_new_matrix_op_template(__TYPE__, error, type, ...) \
ret = new image_window(*((dlib::matrix_op<ELEMENT<array2d<__TYPE__>>>*)image));\

#define image_window_new_matrix_op_template2(__TYPE__, error, type, ...) \
ret = new image_window(*((matrix_op<ELEMENT<array2d<__TYPE__>>>*)image), title);\

#define image_window_new_matrix_op_template3(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(img);\
ret = new image_window(op);\

#define image_window_new_matrix_op_template4(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& op = *static_cast<matrix_op<ELEMENT<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(img);\
ret = new image_window(op, title);\

#define image_window_set_image_array2d_template(__TYPE__, error, type, ...) \
window->set_image(*((dlib::array2d<__TYPE__>*)image));

#define image_window_set_image_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
window->set_image(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image));

#define image_window_set_image_matrix_op_template(__TYPE__, error, type, ...) \
window->set_image(*((matrix_op<ELEMENT<dlib::array2d<__TYPE__>>>*)image));\

#define image_window_set_image_matrix_op_op_join_rows_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
window->set_image(*((matrix_op<op_join_rows<matrix<__TYPE__, __ROWS__, __COLUMNS__>, matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)image));\

#define image_window_add_overlay_pixel_rect_template(__TYPE__, error, type, ...) \
window->add_overlay(*r, *((__TYPE__*)p));\

#define image_window_add_overlay_pixel_rects_template(__TYPE__, error, type, ...) \
window->add_overlay(rects, *((__TYPE__*)p));\

#define image_window_add_overlay_pixel_rect_string_template(__TYPE__, error, type, ...) \
window->add_overlay(*r, *((__TYPE__*)p), *l);\

#pragma endregion template

#pragma region image_window

#pragma region new

DLLEXPORT image_window* image_window_new()
{
    return new image_window();
}


#define image_window_new_array2d1_template(__TYPE__, error, type, ...) \
ret = new image_window(*((array2d<__TYPE__>*)image));

#define image_window_new_array2d2_template(__TYPE__, error, type, ...) \
ret = new image_window(*((array2d<__TYPE__>*)image), title);

#define image_window_new_matrix1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new image_window(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image));

#define image_window_new_matrix2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new image_window(*((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)image), title);

DLLEXPORT image_window* image_window_new_array2d1(array2d_type type, void* image)
{
    int error = ERR_OK;
    image_window* ret = nullptr;

    array2d_template(type,
                     error,
                     image_window_new_array2d1_template,
                     image,
                     ret);

    return ret;
}

DLLEXPORT image_window* image_window_new_array2d2(array2d_type type, void* image, const char* title)
{
    int error = ERR_OK;
    image_window* ret = nullptr;

    array2d_template(type,
                     error,
                     image_window_new_array2d2_template,
                     image,
                     ret);

    return ret;
}

DLLEXPORT image_window* image_window_new_matrix1(matrix_element_type type, void* image)
{
    int error = ERR_OK;
    image_window* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    image_window_new_matrix1_template,
                    0,
                    0,
                    image,
                    ret);

    return ret;
}

DLLEXPORT image_window* image_window_new_matrix2(matrix_element_type type, void* image, const char* title)
{
    int error = ERR_OK;
    image_window* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    image_window_new_matrix2_template,
                    0,
                    0,
                    image,
                    ret);

    return ret;
}

DLLEXPORT int image_window_new_matrix_op1(element_type etype, array2d_type type, void* image, void** result)
{
    int error = ERR_OK;
    void* ret = nullptr;

    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            array2d_template(type,
                             error,
                             image_window_new_matrix_op_template,
                             image,
                             ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            array2d_template(type,
                             error,
                             image_window_new_matrix_op_template,
                             image,
                             ret);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }

    *result = ret;

    return error;
}

DLLEXPORT int image_window_new_matrix_op2(element_type etype, array2d_type type, void* image, const char* title, void** result)
{
    int error = ERR_OK;
    void* ret = nullptr;
    
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            array2d_template(type,
                             error,
                             image_window_new_matrix_op_template2,
                             image,
                             title,
                             ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            array2d_template(type,
                             error,
                             image_window_new_matrix_op_template2,
                             image,
                             title,
                             ret);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }
    
    *result = ret;

    return error;
}

DLLEXPORT int image_window_new_matrix_op3(element_type etype,
                                          matrix_element_type type,
                                          void* img,
                                          const int templateRows,
                                          const int templateColumns,
                                          void** result)
{
    int error = ERR_OK;
    void* ret = nullptr;
    
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            matrix_template(type,
                            error,
                            matrix_template_size_template,
                            image_window_new_matrix_op_template3,
                            templateRows,
                            templateColumns,
                            img,
                            ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            matrix_template(type,
                            error,
                            matrix_template_size_template,
                            image_window_new_matrix_op_template3,
                            templateRows,
                            templateColumns,
                            img,
                            ret);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }
    
    *result = ret;

    return error;
}

DLLEXPORT int image_window_new_matrix_op4(element_type etype,
                                          matrix_element_type type,
                                          void* img,
                                          const int templateRows,
                                          const int templateColumns,
                                          const char* title,
                                          void** result)
{
    int error = ERR_OK;
    void* ret = nullptr;

    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            matrix_template(type,
                            error,
                            matrix_template_size_template,
                            image_window_new_matrix_op_template4,
                            templateRows,
                            templateColumns,
                            img,
                            title,
                            ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            matrix_template(type,
                            error,
                            matrix_template_size_template,
                            image_window_new_matrix_op_template4,
                            templateRows,
                            templateColumns,
                            img,
                            title,
                            ret);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }
    
    *result = ret;

    return error;
}

#pragma endregion new

DLLEXPORT void image_window_delete(image_window* window)
{
	delete window;
}

#pragma region add_overlay

DLLEXPORT int image_window_add_overlay(image_window* window, dlib::rectangle* r, array2d_type type, void* p)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     image_window_add_overlay_pixel_rect_template,
                     window,
                     type,
                     p,
                     r);

    return error;
}

DLLEXPORT int image_window_add_overlay2(image_window* window, std::vector<rectangle*>* r, array2d_type type, void* p)
{
    int error = ERR_OK;

    std::vector<rectangle*>& vector = *(static_cast<std::vector<rectangle*>*>(r));
    std::vector<rectangle> rects;
    for (int index = 0 ; index < vector.size(); index++)
    {
        dlib::rectangle& rect = *(vector[index]);
        rects.push_back(rect);
    }

    array2d_template(type,
                     error,
                     image_window_add_overlay_pixel_rects_template,
                     window,
                     type,
                     p,
                     rects);
    
    return error;
}

DLLEXPORT int image_window_add_overlay3(image_window* window, dlib::drectangle* r, array2d_type type, void* p)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     image_window_add_overlay_pixel_rect_template,
                     window,
                     type,
                     p,
                     r);

    return error;
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

DLLEXPORT int image_window_add_overlay6(image_window* window, dlib::rectangle* r, array2d_type type, void* p, std::string* l)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     image_window_add_overlay_pixel_rect_string_template,
                     window,
                     type,
                     p,
                     r,
                     l);
    
    return error;
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
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     image_window_set_image_array2d_template,
                     window,
                     image);

    return error;
}

DLLEXPORT int image_window_set_image_matrix(image_window* window, matrix_element_type type, void* image)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    image_window_set_image_matrix_template,
                    0,
                    0,
                    window,
                    image);

    return error;
}

DLLEXPORT int image_window_set_image_matrix_op_array2d(image_window* window, element_type etype, array2d_type type, void* image)
{
    int error = ERR_OK;

    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            array2d_template(type,
                             error,
                             image_window_set_image_matrix_op_template,
                             image,
                             window);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            array2d_template(type,
                             error,
                             image_window_set_image_matrix_op_template,
                             image,
                             window);
            #undef ELEMENT
            break;
        case element_type::OpArray2dToMat:
            #define ELEMENT dlib::op_array2d_to_mat
            array2d_template(type,
                             error,
                             image_window_set_image_matrix_op_template,
                             image,
                             window);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }

    return error;
}

DLLEXPORT int image_window_set_image_matrix_op_matrix(image_window* window,
                                                      element_type etype,
                                                      matrix_element_type type,
                                                      void* image)
{
    int error = ERR_OK;

    switch(etype)
    {
        case element_type::OpJoinRows:
            #define ELEMENT dlib::op_join_rows        
            matrix_template(type,
                            error,
                            matrix_template_size_template,
                            image_window_set_image_matrix_op_op_join_rows_template,
                            0,
                            0,
                            image,
                            window);
            #undef ELEMENT
            break;
        default:
            error = ERR_MATRIX_OP_TYPE_NOT_SUPPORT;
            break;
    }

    return error;
}

#pragma endregion set_image

#pragma region overlay_line

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

#pragma endregion overlay_line

#pragma region get_next_double_click

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

#pragma endregion get_next_double_click

#pragma endregion image_window

#endif

#endif