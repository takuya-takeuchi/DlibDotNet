#ifndef _CPP_COLORMAPS_COLORMAPS_H_
#define _CPP_COLORMAPS_COLORMAPS_H_

#include "../../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define OP op
#define FUNCTION function
#undef FUNCTION
#undef OP

#define colormaps_apply_array2d_template(__TYPE__, error, type, ...) \
auto op = static_cast<OP<array2d<__TYPE__>>*>(img);\
*ret = op->apply(r, c);\

#define colormaps_max_val_array2d_template(__TYPE__, error, type, ...) \
auto op = static_cast<OP<array2d<__TYPE__>>*>(img);\
*ret = op->max_val;\

#define colormaps_min_val_array2d_template(__TYPE__, error, type, ...) \
auto op = static_cast<OP<array2d<__TYPE__>>*>(img);\
*ret = op->min_val;\

#define colormaps_nc_array2d_template(__TYPE__, error, type, ...) \
auto op = static_cast<OP<array2d<__TYPE__>>*>(img);\
*ret = op->nc();\

#define colormaps_nc_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto op = static_cast<OP<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(img);\
*ret = op->nc();\

#define colormaps_nr_array2d_template(__TYPE__, error, type, ...) \
auto op = static_cast<OP<array2d<__TYPE__>>*>(img);\
*ret = op->nc();\

#define colormaps_nr_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto op = static_cast<OP<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(img);\
*ret = op->nr();\

#define colormaps_function_array2d_template(__TYPE__, error, type, ...) \
auto& array = *static_cast<dlib::array2d<__TYPE__>*>(img);\
auto ret = dlib::FUNCTION(array);\
*matrix = new dlib::matrix_op<OP<dlib::array2d<__TYPE__>>>(ret);\

#define colormaps_function2_array2d_template(__TYPE__, error, type, ...) \
array2d<__TYPE__>& array = *static_cast<dlib::array2d<__TYPE__>*>(img);\
auto ret = dlib::FUNCTION(array, max_val, min_val);\
*matrix = new dlib::matrix_op<OP<dlib::array2d<__TYPE__>>>(ret);\

#define colormaps_function_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(img);\
auto ret = dlib::FUNCTION(m);\
*matrix = new dlib::matrix_op<OP<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(ret);\

#define colormaps_function2_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(img);\
auto ret = dlib::FUNCTION(m, max_val, min_val);\
*matrix = new dlib::matrix_op<OP<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(ret);\

#pragma endregion template

#endif