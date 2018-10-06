#ifndef _CPP_VECTOR_H_
#define _CPP_VECTOR_H_

#include "export.h"
#include <dlib/geometry/rectangle.h>

#ifndef DLIB_NO_GUI_SUPPORT
#include <dlib/gui_widgets/widgets.h>
#include <dlib/image_keypoint/draw_surf_points.h>
#endif

#include <dlib/data_io/image_dataset_metadata.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/object_detector.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/image_keypoint/surf.h>
#include "shared.h"

using namespace dlib;

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<__TYPE__>* stdvector_##__TYPENAME__##_new1()\
{\
    return new std::vector<__TYPE__>;\
}\
\
DLLEXPORT std::vector<__TYPE__>* stdvector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<__TYPE__>(size);\
}\
\
DLLEXPORT std::vector<__TYPE__>* stdvector_##__TYPENAME__##_new3(__TYPE__* data, size_t dataLength)\
{\
    return new std::vector<__TYPE__>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_##__TYPENAME__##_getSize(std::vector<__TYPE__>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT __TYPE__* stdvector_##__TYPENAME__##_getPointer(std::vector<__TYPE__> *vector)\
{\
    return &(vector->at(0));\
}\
\
DLLEXPORT void stdvector_##__TYPENAME__##_delete(std::vector<__TYPE__> *vector)\
{\
    delete vector;\
}\

#define MAKE_FUNC_POINTER(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new1()\
{\
    return new std::vector<__TYPE__*>;\
}\
\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<__TYPE__*>(size);\
}\
\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new3(__TYPE__** data, size_t dataLength)\
{\
    return new std::vector<__TYPE__*>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_##__TYPENAME__##_getSize(std::vector<__TYPE__*>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT __TYPE__* stdvector_##__TYPENAME__##_getPointer(std::vector<__TYPE__*> *vector)\
{\
    return (vector->at(0));\
}\
\
DLLEXPORT void stdvector_##__TYPENAME__##_delete(std::vector<__TYPE__*> *vector)\
{\
    delete vector;\
}\
\
DLLEXPORT void stdvector_##__TYPENAME__##_copy(std::vector<__TYPE__*> *vector, __TYPE__** dst)\
{\
    size_t length = sizeof(__TYPE__*)* vector->size();\
    memcpy(dst, &(vector->at(0)), length);\
}\

#define MAKE_FUNC_POINTER_WITH_DELETE(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new1()\
{\
    return new std::vector<__TYPE__*>();\
}\
\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<__TYPE__*>(size);\
}\
\
DLLEXPORT std::vector<__TYPE__*>* stdvector_##__TYPENAME__##_new3(__TYPE__** data, size_t dataLength)\
{\
    return new std::vector<__TYPE__*>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_##__TYPENAME__##_getSize(std::vector<__TYPE__*>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT __TYPE__* stdvector_##__TYPENAME__##_getPointer(std::vector<__TYPE__*> *vector)\
{\
    return (vector->at(0));\
}\
\
DLLEXPORT void stdvector_##__TYPENAME__##_delete(std::vector<__TYPE__*> *vector)\
{\
    std::vector<__TYPE__*>& tmp = *(static_cast<std::vector<__TYPE__*>*>(vector));\
    for (int index = 0 ; index < tmp.size(); index++)\
        delete tmp[index];\
\
    delete vector;\
}\
\
DLLEXPORT void stdvector_##__TYPENAME__##_copy(std::vector<__TYPE__*> *vector, __TYPE__** dst)\
{\
    size_t length = sizeof(__TYPE__*)* vector->size();\
    memcpy(dst, &(vector->at(0)), length);\
}\

#define MAKE_FUNC_VECTOR(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<dlib::vector<__TYPE__>*>* stdvector_vector_##__TYPENAME__##_new1()\
{\
    return new std::vector<dlib::vector<__TYPE__>*>();\
}\
\
DLLEXPORT std::vector<dlib::vector<__TYPE__>*>* stdvector_vector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<dlib::vector<__TYPE__>*>(size);\
}\
\
DLLEXPORT std::vector<dlib::vector<__TYPE__>*>* stdvector_vector_##__TYPENAME__##_new3(dlib::vector<__TYPE__>** data, size_t dataLength)\
{\
    return new std::vector<dlib::vector<__TYPE__>*>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_vector_##__TYPENAME__##_getSize(std::vector<dlib::vector<__TYPE__>*>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT dlib::vector<__TYPE__>* stdvector_vector_##__TYPENAME__##_getPointer(std::vector<dlib::vector<__TYPE__>*> *vector)\
{\
    return (vector->at(0));\
}\
\
DLLEXPORT void stdvector_vector_##__TYPENAME__##_delete(std::vector<dlib::vector<__TYPE__>*> *vector)\
{\
    delete vector;\
}\
\
DLLEXPORT void stdvector_vector_##__TYPENAME__##_copy(std::vector<dlib::vector<__TYPE__>*> *vector, dlib::vector<__TYPE__>** dst)\
{\
    size_t length = sizeof(dlib::vector<__TYPE__>*)* vector->size();\
    memcpy(dst, &(vector->at(0)), length);\
}\

#define MAKE_FUNC_VECTOR_POINTER(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<std::vector<__TYPE__*>*>* stdvector_stdvector_##__TYPENAME__##_new1()\
{\
    return new std::vector<std::vector<__TYPE__*>*>;\
}\
\
DLLEXPORT std::vector<std::vector<__TYPE__*>*>* stdvector_stdvector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<std::vector<__TYPE__*>*>(size);\
}\
\
DLLEXPORT std::vector<std::vector<__TYPE__*>*>* stdvector_stdvector_##__TYPENAME__##_new3(std::vector<__TYPE__*>** data, size_t dataLength)\
{\
    return new std::vector<std::vector<__TYPE__*>*>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_stdvector_##__TYPENAME__##_getSize(std::vector<std::vector<__TYPE__*>*>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT std::vector<__TYPE__*>* stdvector_stdvector_##__TYPENAME__##_getPointer(std::vector<std::vector<__TYPE__*>*> *vector)\
{\
    return (vector->at(0));\
}\
\
DLLEXPORT void stdvector_stdvector_##__TYPENAME__##_delete(std::vector<std::vector<__TYPE__*>*> *vector)\
{\
    delete vector;\
}\
\
DLLEXPORT void stdvector_stdvector_##__TYPENAME__##_copy(std::vector<std::vector<__TYPE__*>*> *vector, std::vector<__TYPE__*>** dst)\
{\
    /* This method is unsafe!! */\
    size_t length = sizeof(__TYPE__*)* vector->size();\
    memcpy(dst, &(vector->at(0)), length);\
}\

#pragma endregion template

// primitives
MAKE_FUNC(int32_t, int32)
MAKE_FUNC(uint32_t, uint32)
MAKE_FUNC(int64_t, long)

MAKE_FUNC_POINTER_WITH_DELETE(dlib::rectangle, rectangle)
MAKE_FUNC_POINTER_WITH_DELETE(dlib::point, point)
MAKE_FUNC_POINTER_WITH_DELETE(dlib::dpoint, dpoint)

// class (pointer)
MAKE_FUNC_POINTER(std::string, string)
MAKE_FUNC_POINTER(dlib::chip_details, chip_details)
MAKE_FUNC_POINTER(dlib::full_object_detection, full_object_detection)
MAKE_FUNC_POINTER(dlib::rect_detection, rect_detection)
MAKE_FUNC_POINTER(dlib::sample_pair, sample_pair)
MAKE_FUNC_POINTER(dlib::mmod_rect, mmod_rect)
MAKE_FUNC_POINTER(dlib::image_dataset_metadata::image, image_dataset_metadata_image)
MAKE_FUNC_POINTER(dlib::image_dataset_metadata::box, image_dataset_metadata_box)

MAKE_FUNC_VECTOR(double, double)

MAKE_FUNC_VECTOR_POINTER(dlib::rectangle, rectangle)
MAKE_FUNC_VECTOR_POINTER(dlib::mmod_rect, mmod_rect)

#pragma region matrix

#pragma region template

#define stdvector_matrix_new1_template(__TYPE__, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new std::vector<matrix<__TYPE__>*>();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new std::vector<matrix<__TYPE__, 0, 1>*>();\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_new2_template(__TYPE__, size, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new std::vector<matrix<__TYPE__>*>(size);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new std::vector<matrix<__TYPE__, 0, 1>*>(size);\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_new3_template(__TYPE__, data, dataLength, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        auto tmp = (matrix<__TYPE__>**)(data);\
        return new std::vector<matrix<__TYPE__>*>(tmp, tmp + dataLength);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        auto tmp = (matrix<__TYPE__, 0, 1>**)(data);\
        return new std::vector<matrix<__TYPE__, 0, 1>*>(tmp, tmp + dataLength);\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_delete_template(__TYPE__, in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        delete ((std::vector<dlib::matrix<__TYPE__>*>*)in_vector);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        delete ((std::vector<dlib::matrix<__TYPE__, 0, 1>*>*)in_vector);\
    }\
} while (0)

#define stdvector_matrix_getSize_template(__TYPE__, in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return ((std::vector<dlib::matrix<__TYPE__>*>*)in_vector)->size();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return ((std::vector<dlib::matrix<__TYPE__, 0, 1>*>*)in_vector)->size();\
    }\
    return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
} while (0)

#define stdvector_matrix_getPointer_template(__TYPE__, in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return ((std::vector<dlib::matrix<__TYPE__>*>*)in_vector)->at(0);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return ((std::vector<dlib::matrix<__TYPE__, 0, 1>*>*)in_vector)->at(0);\
    }\
    return nullptr;\
} while (0)

#pragma endregion template

DLLEXPORT void* stdvector_matrix_new1(matrix_element_type type, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_new1_template(uint8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_new1_template(uint16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_new1_template(uint32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_new1_template(int8_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_new1_template(int16_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_new1_template(int32_t, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_new1_template(float, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_new1_template(double, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_new1_template(rgb_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_new1_template(hsi_pixel, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_new1_template(rgb_alpha_pixel, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* stdvector_matrix_new2(matrix_element_type type, size_t size, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_new2_template(uint8_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_new2_template(uint16_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_new2_template(uint32_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_new2_template(int8_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_new2_template(int16_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_new2_template(int32_t, size, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_new2_template(float, size, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_new2_template(double, size, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_new2_template(rgb_pixel, size, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_new2_template(hsi_pixel, size, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_new2_template(rgb_alpha_pixel, size, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* stdvector_matrix_new3(matrix_element_type type, void** data, size_t dataLength, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_new3_template(uint8_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_new3_template(uint16_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_new3_template(uint32_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_new3_template(int8_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_new3_template(int16_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_new3_template(int32_t, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_new3_template(float, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_new3_template(double, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_new3_template(rgb_pixel, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_new3_template(hsi_pixel, data, dataLength, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_new3_template(rgb_alpha_pixel, data, dataLength, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT size_t stdvector_matrix_getSize(matrix_element_type type, void* vector, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_getSize_template(uint8_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_getSize_template(uint16_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_getSize_template(uint32_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_getSize_template(int8_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_getSize_template(int16_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_getSize_template(int32_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_getSize_template(float, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_getSize_template(double, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_getSize_template(rgb_pixel, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_getSize_template(hsi_pixel, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_getSize_template(rgb_alpha_pixel, vector, templateRows, templateColumns);
            break;
        default:
            return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
    }
}

DLLEXPORT void* stdvector_matrix_getPointer(matrix_element_type type, void* vector, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_getPointer_template(uint8_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_getPointer_template(uint16_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_getPointer_template(uint32_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_getPointer_template(int8_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_getPointer_template(int16_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_getPointer_template(int32_t, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_getPointer_template(float, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_getPointer_template(double, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_getPointer_template(rgb_pixel, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_getPointer_template(hsi_pixel, vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_getPointer_template(rgb_alpha_pixel, vector, templateRows, templateColumns);
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void stdvector_matrix_delete(matrix_element_type type, std::vector<void*> *in_vector, const int templateRows, const int templateColumns)
{    
    switch(type)
    {
        case matrix_element_type::UInt8:
            stdvector_matrix_delete_template(uint8_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt16:
            stdvector_matrix_delete_template(uint16_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::UInt32:
            stdvector_matrix_delete_template(uint32_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int8:
            stdvector_matrix_delete_template(int8_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int16:
            stdvector_matrix_delete_template(int16_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Int32:
            stdvector_matrix_delete_template(int32_t, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Float:
            stdvector_matrix_delete_template(float, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::Double:
            stdvector_matrix_delete_template(double, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbPixel:
            stdvector_matrix_delete_template(rgb_pixel, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::HsiPixel:
            stdvector_matrix_delete_template(hsi_pixel, in_vector, templateRows, templateColumns);
            break;
        case matrix_element_type::RgbAlphaPixel:
            stdvector_matrix_delete_template(rgb_alpha_pixel, in_vector, templateRows, templateColumns);
            break;
        default:
            break;
    }
}

DLLEXPORT void stdvector_matrix_copy(std::vector<void*> *vector, void** dst)
{
    size_t length = sizeof(void*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion matrix

#ifndef DLIB_NO_GUI_SUPPORT

MAKE_FUNC_POINTER(dlib::image_window::overlay_line, image_window_overlay_line)
MAKE_FUNC_POINTER(dlib::perspective_window::overlay_dot, perspective_window_overlay_dot)
MAKE_FUNC_POINTER(dlib::surf_point, surf_point)

#endif

#endif