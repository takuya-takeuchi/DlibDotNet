#ifndef _CPP_VECTOR_H_
#define _CPP_VECTOR_H_

#include "export.h"
#include <dlib/geometry/rectangle.h>

#ifndef DLIB_NO_GUI_SUPPORT
#include <dlib/gui_widgets/widgets.h>
#include <dlib/image_keypoint/draw_surf_points.h>
#endif

#include <dlib/data_io/image_dataset_metadata.h>
#include <dlib/dnn.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/object_detector.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/image_keypoint/surf.h>
#include "template.h"
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

#define MAKE_FUNC_VECTOR_NOPOINTER(__TYPE__, __TYPENAME__)\
DLLEXPORT std::vector<std::vector<__TYPE__>*>* stdvector_stdvector_##__TYPENAME__##_new1()\
{\
    return new std::vector<std::vector<__TYPE__>*>;\
}\
\
DLLEXPORT std::vector<std::vector<__TYPE__>*>* stdvector_stdvector_##__TYPENAME__##_new2(size_t size)\
{\
    return new std::vector<std::vector<__TYPE__>*>(size);\
}\
\
DLLEXPORT std::vector<std::vector<__TYPE__>*>* stdvector_stdvector_##__TYPENAME__##_new3(std::vector<__TYPE__>** data, size_t dataLength)\
{\
    return new std::vector<std::vector<__TYPE__>*>(data, data + dataLength);\
}\
\
DLLEXPORT size_t stdvector_stdvector_##__TYPENAME__##_getSize(std::vector<std::vector<__TYPE__>*>* vector)\
{\
    return vector->size();\
}\
\
DLLEXPORT std::vector<__TYPE__>* stdvector_stdvector_##__TYPENAME__##_getPointer(std::vector<std::vector<__TYPE__>*> *vector)\
{\
    return (vector->at(0));\
}\
\
DLLEXPORT void stdvector_stdvector_##__TYPENAME__##_delete(std::vector<std::vector<__TYPE__>*> *vector)\
{\
    delete vector;\
}\
\
DLLEXPORT void stdvector_stdvector_##__TYPENAME__##_copy(std::vector<std::vector<__TYPE__>*> *vector, std::vector<__TYPE__>** dst)\
{\
    /* This method is unsafe!! */\
    size_t length = sizeof(__TYPE__) * vector->size();\
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
MAKE_FUNC(double, double)

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
MAKE_FUNC_POINTER(dlib::mmod_options::detector_window_details, mmod_options_detector_window_details)
#ifndef DLIB_NO_GUI_SUPPORT
MAKE_FUNC_POINTER(dlib::image_display::overlay_rect, image_display_overlay_rect)
#endif

MAKE_FUNC_VECTOR(double, double)

MAKE_FUNC_VECTOR_NOPOINTER(double, double)
MAKE_FUNC_VECTOR_POINTER(dlib::rectangle, rectangle)
MAKE_FUNC_VECTOR_POINTER(dlib::mmod_rect, mmod_rect)
MAKE_FUNC_VECTOR_POINTER(dlib::full_object_detection, full_object_detection)

#pragma region matrix

#pragma region template

#define stdvector_matrix_new1_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new std::vector<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>();\

#define stdvector_matrix_new2_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new std::vector<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(size);\

#define stdvector_matrix_new3_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp = (matrix<__TYPE__, __ROWS__, __COLUMNS__>**)(data);\
ret = new std::vector<matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(tmp, tmp + dataLength);\

#define stdvector_matrix_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
delete ((std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*)vector);\

#define stdvector_matrix_getSize_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = ((std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*)vector)->size();\

#define stdvector_matrix_getPointer_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = ((std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*)vector)->at(0);\

#pragma endregion template

DLLEXPORT void* stdvector_matrix_new1(matrix_element_type type, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_new1_template,
                    templateRows,
                    templateColumns,
                    ret);

    return ret;
}

DLLEXPORT void* stdvector_matrix_new2(matrix_element_type type, size_t size, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_new2_template,
                    templateRows,
                    templateColumns,
                    size,
                    ret);

    return ret;
}

DLLEXPORT void* stdvector_matrix_new3(matrix_element_type type, void** data, size_t dataLength, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_new3_template,
                    templateRows,
                    templateColumns,
                    data,
                    dataLength,
                    ret);

    return ret;
}

DLLEXPORT size_t stdvector_matrix_getSize(matrix_element_type type, void* vector, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    size_t ret;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_getSize_template,
                    templateRows,
                    templateColumns,
                    vector,
                    ret);

    return ret;
}

DLLEXPORT void* stdvector_matrix_getPointer(matrix_element_type type, void* vector, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_getPointer_template,
                    templateRows,
                    templateColumns,
                    vector,
                    ret);

    return ret;
}

DLLEXPORT void stdvector_matrix_delete(matrix_element_type type, std::vector<void*> *vector, const int templateRows, const int templateColumns)
{
    int error = ERR_OK;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    stdvector_matrix_delete_template,
                    templateRows,
                    templateColumns,
                    vector);
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