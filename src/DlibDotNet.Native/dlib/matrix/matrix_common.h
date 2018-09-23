#ifndef _CPP_MATRIX_COMMON_H_
#define _CPP_MATRIX_COMMON_H_

#include "../export.h"
#include <dlib/array.h>
#include <dlib/pixel.h>
#include <dlib/image_processing.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix.h>
#include <dlib/matrix/matrix.h>
#include <dlib/matrix/matrix_op.h>

using namespace dlib;
using namespace std;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define matrix_cast_without_type_parameter_template(lhs, rhs, leftTemplateRows, leftTemplateColumns, rightTemplateRows, rightTemplateColumns, left, right) \
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(lhs));\
        left = new dlib::matrix<ELEMENT>(tmp);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(lhs));\
        left = new dlib::matrix<ELEMENT>(tmp);\
    }\
\
    if (rightTemplateRows == 0 && rightTemplateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(rhs));\
        right = new dlib::matrix<ELEMENT>(tmp);\
    }\
    else if (rightTemplateRows == 0 && rightTemplateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(rhs));\
        right = new dlib::matrix<ELEMENT>(tmp);\
    }\
} while (0)

#define matrix_cast_left_without_type_parameter_template(lhs, leftTemplateRows, leftTemplateColumns, left) \
do {\
    if (leftTemplateRows == 0 && leftTemplateColumns == 0)\
    {\
        dlib::matrix<ELEMENT>& tmp = *(static_cast<dlib::matrix<ELEMENT>*>(lhs));\
        left = new dlib::matrix<ELEMENT>(tmp);\
    }\
    else if (leftTemplateRows == 0 && leftTemplateColumns == 1)\
    {\
        dlib::matrix<ELEMENT, 0, 1>& tmp = *(static_cast<dlib::matrix<ELEMENT, 0, 1>*>(lhs));\
        left = new dlib::matrix<ELEMENT>(tmp);\
    }\
} while (0)

#pragma endregion template

#endif