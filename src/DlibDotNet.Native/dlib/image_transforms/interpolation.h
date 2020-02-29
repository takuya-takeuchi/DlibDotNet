#ifndef _CPP_INTERPOLATION_H_
#define _CPP_INTERPOLATION_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_TYPE PYRAMID_TYPE
#define FUNCTION function
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef FUNCTION
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef PYRAMID_TYPE

#define add_image_left_right_flips_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& in_images = *(static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(images));\
std::vector<matrix<__TYPE__, __ROWS__, __COLUMNS__>> tmp_images;\
for (int i = 0; i < in_images.size(); i++)\
{\
    auto& m = *in_images[i];\
    tmp_images.push_back(m);\
}\
auto& in_objects = *(static_cast<std::vector<std::vector<ELEMENT_OUT*>*>*>(objects));\
std::vector<std::vector<ELEMENT_OUT>> tmp_objects;\
for (int i = 0; i < in_images.size(); i++)\
{\
    auto& vec = *in_objects[i];\
    std::vector<ELEMENT_OUT> tmp_vec;\
    for (int j = 0; j < vec.size(); j++)\
    {\
        auto& m = *vec[j];\
        tmp_vec.push_back(m);\
    }\
    tmp_objects.push_back(tmp_vec);\
}\
\
add_image_left_right_flips(tmp_images, tmp_objects);\
\
for (int i = 0; i < in_images.size(); i++)\
    delete in_images[i];\
in_images.clear();\
\
for (int i = 0; i < in_objects.size(); i++)\
{\
    auto& tmp = *in_objects[i];\
    for (int j = 0; j < tmp.size(); j++)\
        delete tmp[j];\
    tmp.clear();\
    delete in_objects[i];\
}\
in_objects.clear();\
\
for (int i = 0; i < tmp_images.size(); i++)\
    in_images.push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(tmp_images[i]));\
\
for (int i = 0; i < tmp_objects.size(); i++)\
{\
    auto& tmp = tmp_objects[i];\
    auto vec = new std::vector<ELEMENT_OUT*>();\
    for (int j = 0; j < tmp.size(); j++)\
        vec->push_back(new ELEMENT_OUT(tmp[j]));\
    in_objects.push_back(vec);\
}\

#define interpolation_template(__TYPE__, error, type, ...) \
auto& array = *((array2d<__TYPE__>*)img);\
dlib::FUNCTION(array);\

#define interpolation_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& mat = *((dlib::matrix<__TYPE__>*)img);\
dlib::FUNCTION(mat);\

#define interpolation_inout_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in = *((array2d<__TYPE__>*)in_img);\
auto& out = *((array2d<__SUBTYPE__>*)out_img);\
dlib::FUNCTION(in, out);\

#define interpolation_inout2_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::FUNCTION(in_, out_, angle, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::FUNCTION(in_, out_, angle, interpolate_bilinear());\
        break;\
    case interpolation_type::Quadratic:\
        dlib::FUNCTION(in_, out_, angle, interpolate_quadratic());\
        break;\
}\

#define interpolation_inout3_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::FUNCTION(in_, out_, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::FUNCTION(in_, out_, interpolate_bilinear());\
        break;\
    case interpolation_type::Quadratic:\
        dlib::FUNCTION(in_, out_, interpolate_quadratic());\
        break;\
}\

#define extract_image_4points_template(__TYPE__, error, type, ...) \
auto& m = *(static_cast<dlib::array2d<__TYPE__>*>(image));\
std::array<dlib::dpoint, 4> ps;\
for (auto index = 0; index < 4; index++)\
    ps[index] = *points[index];\
auto tmpout = new dlib::array2d<__TYPE__>(height, width);\
auto& o = *(static_cast<dlib::array2d<__TYPE__>*>(tmpout));\
dlib::extract_image_4points(m, o, ps);\
*output = tmpout;\

#define extract_image_4points_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix));\
std::array<dlib::dpoint, 4> ps;\
for (auto index = 0; index < 4; index++)\
    ps[index] = *points[index];\
auto tmpout = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(height, width);\
auto& o = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(tmpout));\
dlib::extract_image_4points(m, o, ps);\
*output = tmpout;\

#define pyramid_up_matrix_template_sub(__RATE__, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
case __RATE__:\
    {\
        auto& pyramid = *(static_cast<dlib::pyramid_down<__RATE__>*>(pyramid_down));\
        auto m = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>();\
        auto& t = *m;\
        auto& img_tmp = *((dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*)img);\
        dlib::pyramid_up(img_tmp, t, pyramid);\
        *matrix = m;\
    }\
    break;\

#define pyramid_up_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
switch(pyramid_rate)\
{\
    pyramid_up_matrix_template_sub(1, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...)\
    pyramid_up_matrix_template_sub(2, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...)\
    pyramid_up_matrix_template_sub(3, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...)\
    pyramid_up_matrix_template_sub(4, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...)\
    pyramid_up_matrix_template_sub(6, __TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...)\
    default:\
        error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
        break;\
}\

#define rotate_image_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
dlib::FUNCTION(in_, out_, angle);\

#define transform_image_sub_template(in_img, out_img, type, mapping_type, mapping_obj) \
do { \
    switch(mapping_type)\
    {\
        case point_mapping_type::Rotator:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_rotator*)mapping_obj));\
            break;\
        case point_mapping_type::Transform:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform*)mapping_obj));\
            break;\
        case point_mapping_type::TransformAffine:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform_affine*)mapping_obj));\
            break;\
        case point_mapping_type::TransformProjective:\
            dlib::transform_image(in_img, out_img, type, *((dlib::point_transform_projective*)mapping_obj));\
            break;\
    }\
} while (0)

#define transform_image_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_img);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        transform_image_sub_template(in_, out_, interpolate_nearest_neighbor(), mapping_type, mapping_obj);\
        break;\
    case interpolation_type::Bilinear:\
        transform_image_sub_template(in_, out_, interpolate_bilinear(), mapping_type, mapping_obj);\
        break;\
    case interpolation_type::Quadratic:\
        transform_image_sub_template(in_, out_, interpolate_quadratic(), mapping_type, mapping_obj);\
        break;\
}\

#define extract_image_chips_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in = *((array2d<__TYPE__>*)in_img);\
auto& out = *((dlib::array<array2d<__SUBTYPE__>>*)array);\
dlib::extract_image_chips(in, chips, out);\

#define extract_image_chip_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_chip);\
dlib::extract_image_chip(in_, *chip_location, out_);\

#define extract_image_chip2_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_chip);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_bilinear());\
        break;\
    case interpolation_type::Quadratic:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_quadratic());\
        break;\
}\

#define extract_image_chip2_hsi_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)in_img);\
auto& out_ = *((array2d<__SUBTYPE__>*)out_chip);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_bilinear());\
        break;\
    default:\
        error = ERR_GENERAL_INVALID_PARAMETER;\
        break;\
}\

#define extract_image_chips_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __SUBTYPE__, subtype, ...) \
auto& in_ =*((matrix<__TYPE__>*)in_img);\
auto& out_ = *((dlib::array<dlib::matrix<__SUBTYPE__>>*)array);\
dlib::extract_image_chips(in_, chips, out_);\

#define extract_image_chip_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __SUBTYPE__, subtype, ...) \
auto& in_ = *((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)in_img);\
auto& c = *chip_location;\
auto& out_ = *((matrix<__SUBTYPE__, __ROWS__, __COLUMNS__>*)out_chip);\
dlib::extract_image_chip(in_, c, out_);\

#define extract_image_chip_matrix2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __SUBTYPE__, subtype, ...) \
auto& in_ = *((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)in_img);\
auto& out_ = *((matrix<__SUBTYPE__, __ROWS__, __COLUMNS__>*)out_chip);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_bilinear());\
        break;\
    case interpolation_type::Quadratic:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_quadratic());\
        break;\
}\

#define extract_image_chip_matrix2_hsi_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, __SUBTYPE__, subtype, ...) \
auto& in_ = *((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)in_img);\
auto& out_ = *((matrix<__SUBTYPE__, __ROWS__, __COLUMNS__>*)out_chip);\
switch(int_type)\
{\
    case interpolation_type::NearestNeighbor:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_nearest_neighbor());\
        break;\
    case interpolation_type::Bilinear:\
        dlib::extract_image_chip(in_, *chip_location, out_, interpolate_bilinear());\
        break;\
    default:\
        error = ERR_GENERAL_INVALID_PARAMETER;\
        break;\
}\

#define jitter_image_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& in = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(in_img));\
auto& in_r = *(static_cast<dlib::rand*>(r));\
auto ret = dlib::jitter_image(in, in_r);\
*out_img = new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(ret);\

#define resize_image3_template(__TYPE__, error, type, ...) \
auto& in_ = *((array2d<__TYPE__>*)img);\
dlib::resize_image(size_scale, in_);

#define resize_image_matrix_scale_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(matrix);\
dlib::resize_image(scale, m);\

#define upsample_image_dataset_pyramid_down_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& in_images = *(static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(images));\
std::vector<matrix<__TYPE__, __ROWS__, __COLUMNS__>> tmp_images;\
for (int i = 0; i < in_images.size(); i++)\
{\
    auto& m = *in_images[i];\
    tmp_images.push_back(m);\
}\
std::vector<std::vector<ELEMENT_OUT*>*>& in_objects = *(static_cast<std::vector<std::vector<ELEMENT_OUT*>*>*>(objects));\
std::vector<std::vector<ELEMENT_OUT>> tmp_objects;\
for (int i = 0; i < in_images.size(); i++)\
{\
    auto& vec = *in_objects[i];\
    std::vector<ELEMENT_OUT> tmp_vec;\
    for (int j = 0; j < vec.size(); j++)\
    {\
        auto& m = *vec[j];\
        tmp_vec.push_back(m);\
    }\
    tmp_objects.push_back(tmp_vec);\
}\
\
switch(pyramid_rate)\
{\
    case 1:\
        upsample_image_dataset<pyramid_down<1>>(tmp_images, tmp_objects, max_image_size);\
        break;\
    case 2:\
        upsample_image_dataset<pyramid_down<2>>(tmp_images, tmp_objects, max_image_size);\
        break;\
    case 3:\
        upsample_image_dataset<pyramid_down<3>>(tmp_images, tmp_objects, max_image_size);\
        break;\
    case 4:\
        upsample_image_dataset<pyramid_down<4>>(tmp_images, tmp_objects, max_image_size);\
        break;\
    case 6:\
        upsample_image_dataset<pyramid_down<6>>(tmp_images, tmp_objects, max_image_size);\
        break;\
    default:\
        error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
        break;\
}\
\
for (int i = 0; i < in_images.size(); i++)\
    delete in_images[i];\
in_images.clear();\
\
for (int i = 0; i < in_objects.size(); i++)\
{\
    auto& tmp = *in_objects[i];\
    for (int j = 0; j < tmp.size(); j++)\
        delete tmp[j];\
    tmp.clear();\
    delete in_objects[i];\
}\
in_objects.clear();\
\
for (int i = 0; i < tmp_images.size(); i++)\
    in_images.push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(tmp_images[i]));\
\
for (int i = 0; i < tmp_objects.size(); i++)\
{\
    auto& tmp = tmp_objects[i];\
    auto vec = new std::vector<ELEMENT_OUT*>();\
    for (int j = 0; j < tmp.size(); j++)\
        vec->push_back(new ELEMENT_OUT(tmp[j]));\
    in_objects.push_back(vec);\
}\

#define pyramid_up_pyramid_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
do { \
    switch(pyramid_rate)\
    {\
        case 1:\
            {\
                const PYRAMID_TYPE<1> p;\
                auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 2:\
            {\
                const PYRAMID_TYPE<2> p;\
                auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 3:\
            {\
                const PYRAMID_TYPE<3> p;\
                auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 4:\
            {\
                const PYRAMID_TYPE<4> p;\
                auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        case 6:\
            {\
                const PYRAMID_TYPE<6> p;\
                auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(image));\
                dlib::pyramid_up(m, p);\
            }\
            break;\
        default:\
            error = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

#pragma region add_image_left_right_flips

DLLEXPORT int add_image_left_right_flips_rectangle(matrix_element_type type, void* images, void* objects)
{
    int error = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    add_image_left_right_flips_template,
                    0,
                    0,
                    images,
                    objects);
    #undef ELEMENT_OUT

    return error;
}

#pragma endregion add_image_left_right_flips

#pragma region flip_image_left_right

DLLEXPORT int flip_image_left_right(array2d_type type, void* img)
{
    int error = ERR_OK;

    #define FUNCTION flip_image_left_right
    array2d_template(type,
                     error,
                     interpolation_template,
                     img);
    #undef FUNCTION

    return error;
}

DLLEXPORT int flip_image_left_right2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img )
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION flip_image_left_right
    array2d_inout_in_template(type,
                              error,
                              array2d_inout_out_template,
                              interpolation_inout_template,
                              subtype,
                              in_img,
                              out_img);
    #undef FUNCTION

    return error;
}

#pragma endregion flip_image_left_right

DLLEXPORT int flip_image_up_down(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION flip_image_up_down
    array2d_inout_in_template(type,
                              error,
                              array2d_inout_out_template,
                              interpolation_inout_template,
                              subtype,
                              in_img,
                              out_img);
    #undef FUNCTION

    return error;
}

DLLEXPORT int pyramid_up(array2d_type type, void* img)
{
    int error = ERR_OK;

    #define FUNCTION pyramid_up
    array2d_template(type,
                     error,
                     interpolation_template,
                     img);
    #undef FUNCTION

    return error;
}

DLLEXPORT int pyramid_up_matrix(matrix_element_type type, void* img)
{
    int error = ERR_OK;

    #define FUNCTION pyramid_up
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    interpolation_matrix_template,
                    0,
                    0,
                    img);
    #undef FUNCTION

    return error;
}

DLLEXPORT int pyramid_up_matrix2(matrix_element_type type, void* img, void* pyramid_down, unsigned int pyramid_rate, void** matrix)
{
    int error = ERR_OK;
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    pyramid_up_matrix_template,
                    0,
                    0,
                    img,
                    pyramid_down,
                    pyramid_rate,
                    matrix);

    return error;
}

DLLEXPORT int pyramid_up_pyramid_matrix(const pyramid_type pyramid_type,
                                        const unsigned int pyramid_rate,
                                        matrix_element_type type,
                                        void* image)
{
    int error = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                {
                    matrix_template(type,
                                    error,
                                    matrix_template_size_template,
                                    pyramid_up_pyramid_matrix_template,
                                    0,
                                    0,
                                    pyramid_rate,
                                    image);
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            error = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return error;
}

#pragma region resize_image

DLLEXPORT int resize_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION resize_image
    array2d_inout_in_template(type,
                              error,
                              array2d_numericrgb_inout_out_template,
                              interpolation_inout_template,
                              subtype,
                              in_img,
                              out_img);
    #undef FUNCTION

    return error;
}

DLLEXPORT int resize_image2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, interpolation_type int_type)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION resize_image
    array2d_numericrgb_inout_in_template(type,
                                         error,
                                         array2d_numericrgb_inout_out_template,
                                         interpolation_inout3_template,
                                         subtype,
                                         in_img,
                                         out_img,
                                         int_type);
    #undef FUNCTION

    return error;
}

DLLEXPORT int resize_image3(array2d_type type, void* img, double size_scale)
{
    int error = ERR_OK;

    array2d_numericrgb_template(type,
                                error,
                                resize_image3_template,
                                img,
                                size_scale);

    return error;
}

DLLEXPORT int resize_image_matrix_scale(matrix_element_type type, void* matrix, int templateRows, int templateColumns, double scale)
{
    int error = ERR_OK;

    matrix_numericrgbbgr_template(type,
                                  error,
                                  matrix_template_size_template,
                                  resize_image_matrix_scale_template,
                                  templateRows,
                                  templateColumns,
                                  matrix,
                                  scale);

    return error;
}

#pragma endregion resize_image

#pragma region rotate_image

DLLEXPORT int rotate_image(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double angle)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION rotate_image
    array2d_numericrgb_inout_in_template(type,
                                         error,
                                         array2d_numericrgb_inout_out_template,
                                         rotate_image_template,
                                         subtype,
                                         in_img,
                                         out_img,
                                         angle);
    #undef FUNCTION

    return error;
}

DLLEXPORT int rotate_image2(array2d_type in_type, void* in_img, array2d_type out_type, void* out_img, double angle, interpolation_type int_type)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    #define FUNCTION rotate_image
    array2d_numericrgb_inout_in_template(type,
                                         error,
                                         array2d_numericrgb_inout_out_template,
                                         interpolation_inout2_template,
                                         subtype,
                                         in_img,
                                         out_img,
                                         angle,
                                         int_type);
    #undef FUNCTION

    return error;
}

#pragma endregion rotate_image

DLLEXPORT int transform_image(array2d_type in_type,
                              void* in_img,
                              array2d_type out_type,
                              void* out_img,
                              point_mapping_type mapping_type,
                              void* mapping_obj,
                              interpolation_type int_type)
{
    int error = ERR_OK;

    auto type = in_type;
    auto subtype = out_type;

    array2d_numericrgb_inout_in_template(type,
                                         error,
                                         array2d_numericrgb_inout_out_template,
                                         transform_image_template,
                                         subtype,
                                         in_img,
                                         mapping_type,
                                         mapping_obj,
                                         int_type,
                                         out_img);

    return error;
}

#pragma region chip_details

DLLEXPORT chip_details* chip_details_new()
{
    return new dlib::chip_details();
}

DLLEXPORT chip_details* chip_details_new2(drectangle* rect, chip_dims* dims)
{
    drectangle& r = *rect;
    chip_dims& d = *dims;
    return new dlib::chip_details(r, d);
}

DLLEXPORT chip_details* chip_details_new3(rectangle* rect, chip_dims* dims)
{
    rectangle& r = *rect;
    chip_dims& d = *dims;
    return new dlib::chip_details(r, d);
}

DLLEXPORT chip_details* chip_details_new4(drectangle* rect, const unsigned long size)
{
    drectangle& r = *rect;
    return new dlib::chip_details(r, size);
}

DLLEXPORT chip_details* chip_details_new5(drectangle* rect, const unsigned long size, const double angle)
{
    drectangle& r = *rect;
    return new dlib::chip_details(r, size, angle);
}

DLLEXPORT bool chip_details_angle(chip_details* chip, double* angle)
{
    *angle = chip->angle;
    return true;
}

DLLEXPORT bool chip_details_cols(chip_details* chip, uint32_t* cols)
{
    *cols = chip->cols;
    return true;
}

DLLEXPORT bool chip_details_rect(chip_details* chip, dlib::drectangle** rect)
{
    *rect = new dlib::drectangle(chip->rect);
    return true;
}

DLLEXPORT bool chip_details_rows(chip_details* chip, uint32_t* rows)
{
    *rows = chip->rows;
    return true;
}

DLLEXPORT void chip_details_delete(dlib::chip_details* obj)
{
    delete obj;
}

#pragma endregion chip_details

#pragma region chip_dims

DLLEXPORT chip_dims* chip_dims_new(unsigned int rows, unsigned int cols)
{
    return new dlib::chip_dims(rows, cols);
}

DLLEXPORT bool chip_dims_get_cols(chip_dims* chip, uint32_t* cols)
{
    *cols = chip->cols;
    return true;
}

DLLEXPORT void chip_dims_set_cols(chip_dims* chip, uint32_t cols)
{
    chip->cols = cols;
}

DLLEXPORT bool chip_dims_get_rows(chip_dims* chip, uint32_t* rows)
{
    *rows = chip->rows;
    return true;
}

DLLEXPORT void chip_dims_set_rows(chip_dims* chip, uint32_t rows)
{
    chip->rows = rows;
}

DLLEXPORT void chip_dims_delete(dlib::chip_dims* obj)
{
    delete obj;
}

#pragma endregion chip_dims

#pragma region flip_rect_left_right

DLLEXPORT dlib::rectangle* flip_rect_left_right(dlib::rectangle* rect, dlib::rectangle* window)
{
    dlib::rectangle& r = *static_cast<dlib::rectangle*>(rect);
    dlib::rectangle& w = *static_cast<dlib::rectangle*>(window);

    auto ret = impl::flip_rect_left_right(r, w);
    return new dlib::rectangle(ret);
}

#pragma endregion flip_rect_left_right

#pragma region get_face_chip_details

DLLEXPORT int get_face_chip_details(std::vector<full_object_detection*>* dets, const unsigned int size, const double padding, std::vector<chip_details*>* rets)
{
    int error = ERR_OK;

    std::vector<full_object_detection> tmpDets;
    for (int index = 0 ; index < dets->size(); index++)
        tmpDets.push_back(*(*dets)[index]);

    std::vector<chip_details> ret = dlib::get_face_chip_details(tmpDets, size, padding);

    for (int index = 0 ; index < ret.size(); index++)
    {
        chip_details* chip = new chip_details(ret[index]);
        rets->push_back(chip);
    }

    return error;
}

DLLEXPORT int get_face_chip_details2(full_object_detection* det, const unsigned int size, const double padding, chip_details** ret)
{
    int error = ERR_OK;

    chip_details r = dlib::get_face_chip_details(*det, size, padding);
    *ret = new chip_details(r);

    return error;
}

#pragma endregion get_face_chip_details

#pragma region extract_image_chips

DLLEXPORT int extract_image_chips(array2d_type img_type, void* in_img, std::vector<chip_details*>* chip_locations, array2d_type array_type, void* array)
{
    int error = ERR_OK;

    std::vector<chip_details> chips;
    for (int index = 0 ; index < chip_locations->size(); index++)
        chips.push_back(*(*chip_locations)[index]);

    auto type = img_type;
    auto subtype = array_type;

    array2d_numericrgb_inout_in_template(type,
                                         error,
                                         array2d_inout_out_template,
                                         extract_image_chips_template,
                                         subtype,
                                         in_img,
                                         chips,
                                         array);

    return error;
}

DLLEXPORT int extract_image_chips_matrix(matrix_element_type img_type, void* in_img, std::vector<chip_details*>* chip_locations, matrix_element_type array_type, void* array)
{
    int error = ERR_OK;

    std::vector<chip_details> chips;
    for (int index = 0 ; index < chip_locations->size(); index++)
        chips.push_back(*(*chip_locations)[index]);

    auto type = img_type;
    auto subtype = array_type;

    matrix_nonalpha_inout_in_template(type,
                                      error,
                                      matrix_inout_out_template,
                                      matrix_inout_template_size_template,
                                      extract_image_chips_matrix_template,
                                      subtype,
                                      0,
                                      0,
                                      in_img,
                                      chips,
                                      array);

    return error;
}

DLLEXPORT int extract_image_chip(array2d_type img_type,
                                 void* in_img,
                                 chip_details* chip_location,
                                 array2d_type array_type,
                                 void* out_chip)
{
    int error = ERR_OK;

    auto type = img_type;
    auto subtype = array_type;

    array2d_nonalpha_inout_in_template(type,
                                       error,
                                       array2d_nonalpha_inout_out_template,
                                       extract_image_chip_template,
                                       subtype,
                                       in_img,
                                       chip_location,
                                       out_chip);

    return error;
}

DLLEXPORT int extract_image_chip2(array2d_type img_type,
                                  void* in_img,
                                  chip_details* chip_location,
                                  array2d_type array_type,
                                  interpolation_type int_type,
                                  void* out_chip)
{
    int error = ERR_OK;

    auto type = img_type;
    auto subtype = array_type;

    if (type == array2d_type::HsiPixel)
    {
        array2d_hsi_inout_in_template(type,
                                      error,
                                      array2d_hsi_inout_out_template,
                                      extract_image_chip2_hsi_template,
                                      subtype,
                                      in_img,
                                      chip_location,
                                      int_type,
                                      out_chip);
    }
    else
    {
        array2d_numericrgb_inout_in_template(type,
                                             error,
                                             array2d_inout_out_template,
                                             extract_image_chip2_template,
                                             subtype,
                                             in_img,
                                             chip_location,
                                             int_type,
                                             out_chip);
    }

    return error;
}

DLLEXPORT int extract_image_chip_matrix(matrix_element_type img_type, void* in_img, chip_details* chip_location, matrix_element_type array_type, void* out_chip)
{
    int error = ERR_OK;

    auto type = img_type;
    auto subtype = array_type;

    matrix_nonalpha_inout_in_template(type,
                                      error,
                                      matrix_inout_out_template,
                                      matrix_inout_template_size_template,
                                      extract_image_chip_matrix_template,
                                      subtype,
                                      0,
                                      0,
                                      in_img,
                                      chip_location,
                                      out_chip);

    return error;
}

DLLEXPORT int extract_image_chip_matrix2(matrix_element_type img_type, void* in_img, chip_details* chip_location, matrix_element_type array_type, interpolation_type int_type, void* out_chip)
{
    int error = ERR_OK;

    auto type = img_type;
    auto subtype = array_type;

    if (type == matrix_element_type::HsiPixel)
    {
        matrix_hsi_inout_in_template(type,
                                     error,
                                     matrix_hsi_inout_out_template,
                                     matrix_inout_template_size_template,
                                     extract_image_chip_matrix2_hsi_template,
                                     subtype,
                                     0,
                                     0,
                                     in_img,
                                     chip_location,
                                     int_type,
                                     out_chip);
    }
    else
    {
        matrix_numericrgbbgr_inout_in_template(type,
                                               error,
                                               matrix_inout_out_template,
                                               matrix_inout_template_size_template,
                                               extract_image_chip_matrix2_template,
                                               subtype,
                                               0,
                                               0,
                                               in_img,
                                               chip_location,
                                               int_type,
                                               out_chip);
    }

    return error;
}

#pragma endregion extract_image_chips

#pragma region jitter_image

DLLEXPORT int jitter_image(matrix_element_type type, void* in_img, dlib::rand* r, void** out_img)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             jitter_image_template,
                             0,
                             0,
                             in_img,
                             r,
                             out_img);

    return error;
}

#pragma endregion jitter_image

#pragma region upsample_image_dataset

DLLEXPORT int upsample_image_dataset_pyramid_down_rect(const unsigned int pyramid_rate,
                                                       const matrix_element_type type,
                                                       void* images,
                                                       void* objects,
                                                       const unsigned long max_image_size)
{
    int error = ERR_OK;

    #define ELEMENT_OUT dlib::rectangle
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    upsample_image_dataset_pyramid_down_template,
                    0,
                    0,
                    pyramid_rate,
                    images,
                    objects,
                    max_image_size);
    #undef ELEMENT_OUT

    return error;
}

DLLEXPORT int upsample_image_dataset_pyramid_down_mmod_rect(const unsigned int pyramid_rate,
                                                            const matrix_element_type type,
                                                            void* images,
                                                            void* objects,
                                                            const unsigned long max_image_size)
{
    int error = ERR_OK;

    #define ELEMENT_OUT dlib::mmod_rect
    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    upsample_image_dataset_pyramid_down_template,
                    0,
                    0,
                    pyramid_rate,
                    images,
                    objects,
                    max_image_size);
    #undef ELEMENT_OUT

    return error;
}

#pragma endregion upsample_image_dataset

#pragma region extract_image_4points

DLLEXPORT int extract_image_4points(array2d_type type,
                                    void* image,
                                    dlib::dpoint** points,
                                    int width,
                                    int height,
                                    void** output)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              extract_image_4points_template,
                              image,
                              points,
                              width,
                              height,
                              output);

    return error;
}

DLLEXPORT int extract_image_4points_matrix(matrix_element_type type,
                                           void* matrix,
                                           int templateRows,
                                           int templateColumns,
                                           dlib::dpoint** points,
                                           int width,
                                           int height,
                                           void** output)
{
    int error = ERR_OK;

    matrix_nonalpha_template(type,
                             error,
                             matrix_template_size_template,
                             extract_image_4points_matrix_template,
                             templateRows,
                             templateColumns,
                             matrix,
                             points,
                             width,
                             height,
                             output);

    return error;
}

#pragma endregion extract_image_4points

#endif