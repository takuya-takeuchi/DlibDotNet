#ifndef _CPP_VECTOR_H_
#define _CPP_VECTOR_H_

#include "export.h"
#include <dlib/geometry/rectangle.h>

#ifndef DLIB_NO_GUI_SUPPORT
#include <dlib/gui_widgets/widgets.h>
#include <dlib/image_keypoint/draw_surf_points.h>
#endif

#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_processing/object_detector.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/image_keypoint/surf.h>
#include "shared.h"

using namespace dlib;

#pragma region int32_t

DLLEXPORT std::vector<int32_t>* stdvector_int32_new1()
{
    return new std::vector<int32_t>;
}

DLLEXPORT std::vector<int32_t>* stdvector_int32_new2(size_t size)
{
    return new std::vector<int32_t>(size);
}

DLLEXPORT std::vector<int32_t>* stdvector_int32_new3(int32_t* data, size_t dataLength)
{
    return new std::vector<int32_t>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_int32_getSize(std::vector<int32_t>* vector)
{
    return vector->size();
}

DLLEXPORT int32_t* stdvector_int32_getPointer(std::vector<int32_t> *vector)
{
    return &(vector->at(0));
}

DLLEXPORT void stdvector_int32_delete(std::vector<int32_t> *vector)
{    
    delete vector;
}

#pragma endregion int32_t

#pragma region int64_t

DLLEXPORT std::vector<int64_t>* stdvector_long_new1()
{
    return new std::vector<int64_t>;
}

DLLEXPORT std::vector<int64_t>* stdvector_long_new2(size_t size)
{
    return new std::vector<int64_t>(size);
}

DLLEXPORT std::vector<int64_t>* stdvector_long_new3(int64_t* data, size_t dataLength)
{
    return new std::vector<int64_t>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_long_getSize(std::vector<int64_t>* vector)
{
    return vector->size();
}

DLLEXPORT int64_t* stdvector_long_getPointer(std::vector<int64_t> *vector)
{
    return &(vector->at(0));
}

DLLEXPORT void stdvector_long_delete(std::vector<int64_t> *vector)
{    
    delete vector;
}

#pragma endregion int64_t

#pragma region uint32_t

DLLEXPORT std::vector<uint32_t>* stdvector_uint32_new1()
{
    return new std::vector<uint32_t>;
}

DLLEXPORT std::vector<uint32_t>* stdvector_uint32_new2(size_t size)
{
    return new std::vector<uint32_t>(size);
}

DLLEXPORT std::vector<uint32_t>* stdvector_uint32_new3(uint32_t* data, size_t dataLength)
{
    return new std::vector<uint32_t>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_uint32_getSize(std::vector<uint32_t>* vector)
{
    return vector->size();
}

DLLEXPORT uint32_t* stdvector_uint32_getPointer(std::vector<uint32_t> *vector)
{
    return &(vector->at(0));
}

DLLEXPORT void stdvector_uint32_delete(std::vector<uint32_t> *vector)
{    
    delete vector;
}

#pragma endregion uint32_t

#pragma region rectangle

DLLEXPORT std::vector<rectangle*>* stdvector_rectangle_new1()
{
    return new std::vector<rectangle*>();
}

DLLEXPORT std::vector<rectangle*>* stdvector_rectangle_new2(size_t size)
{
    return new std::vector<rectangle*>(size);
}

DLLEXPORT std::vector<rectangle*>* stdvector_rectangle_new3(rectangle** data, size_t dataLength)
{
    return new std::vector<rectangle*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_rectangle_getSize(std::vector<rectangle*>* vector)
{
    return vector->size();
}

DLLEXPORT rectangle* stdvector_rectangle_getPointer(std::vector<rectangle*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_rectangle_delete(std::vector<rectangle*> *vector)
{
    std::vector<rectangle*>& tmp = *(static_cast<std::vector<rectangle*>*>(vector));
    for (int index = 0 ; index < tmp.size(); index++)
        delete tmp[index];

    delete vector;
}

DLLEXPORT void stdvector_rectangle_copy(std::vector<rectangle*> *vector, rectangle** dst)
{
    size_t length = sizeof(rectangle*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion rectangle

#pragma region dlib::vector<double>

DLLEXPORT std::vector<dlib::vector<double>*>* stdvector_vector_double_new1()
{
    return new std::vector<dlib::vector<double>*>();
}

DLLEXPORT std::vector<dlib::vector<double>*>* stdvector_vector_double_new2(size_t size)
{
    return new std::vector<dlib::vector<double>*>(size);
}

DLLEXPORT std::vector<dlib::vector<double>*>* stdvector_vector_double_new3(dlib::vector<double>** data, size_t dataLength)
{
    return new std::vector<dlib::vector<double>*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_vector_double_getSize(std::vector<dlib::vector<double>*>* vector)
{
    return vector->size();
}

DLLEXPORT dlib::vector<double>* stdvector_vector_double_getPointer(std::vector<dlib::vector<double>*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_vector_double_delete(std::vector<dlib::vector<double>*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_vector_double_copy(std::vector<dlib::vector<double>*> *vector, dlib::vector<double>** dst)
{
    size_t length = sizeof(dlib::vector<double>*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion dlib::vector<double>

#pragma region matrix

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define stdvector_matrix_new1_template(templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new std::vector<matrix<ELEMENT>*>();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new std::vector<matrix<ELEMENT, 0, 1>*>();\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_new2_template(size, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return new std::vector<matrix<ELEMENT>*>(size);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return new std::vector<matrix<ELEMENT, 0, 1>*>(size);\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        auto tmp = (matrix<ELEMENT>**)(data);\
        return new std::vector<matrix<ELEMENT>*>(tmp, tmp + dataLength);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        auto tmp = (matrix<ELEMENT, 0, 1>**)(data);\
        return new std::vector<matrix<ELEMENT, 0, 1>*>(tmp, tmp + dataLength);\
    }\
    return nullptr;\
} while (0)

#define stdvector_matrix_delete_template(in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        delete ((std::vector<dlib::matrix<ELEMENT>*>*)in_vector);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        delete ((std::vector<dlib::matrix<ELEMENT, 0, 1>*>*)in_vector);\
    }\
} while (0)

#define stdvector_matrix_getSize_template(in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return ((std::vector<dlib::matrix<ELEMENT>*>*)in_vector)->size();\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return ((std::vector<dlib::matrix<ELEMENT, 0, 1>*>*)in_vector)->size();\
    }\
    return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
} while (0)

#define stdvector_matrix_getPointer_template(in_vector, templateRows, templateColumns) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        return ((std::vector<dlib::matrix<ELEMENT>*>*)in_vector)->at(0);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        return ((std::vector<dlib::matrix<ELEMENT, 0, 1>*>*)in_vector)->at(0);\
    }\
    return nullptr;\
} while (0)

#pragma endregion template

DLLEXPORT void* stdvector_matrix_new1(matrix_element_type type, const int templateRows, const int templateColumns)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_new1_template(templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_new2_template(size, templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_new3_template(data, dataLength, templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_getSize_template(vector, templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT 
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_getPointer_template(vector, templateRows, templateColumns);
            #undef ELEMENT
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
            #define ELEMENT uint8_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
            #define ELEMENT rgb_pixel
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::HsiPixel:
            #define ELEMENT hsi_pixel
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbAlphaPixel:
            #define ELEMENT rgb_alpha_pixel
            stdvector_matrix_delete_template(in_vector, templateRows, templateColumns);
            #undef ELEMENT
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

#pragma region std::vector<rectangle>

DLLEXPORT std::vector<std::vector<rectangle*>*>* stdvector_stdvector_rectangle_new1()
{
    return new std::vector<std::vector<rectangle*>*>;
}

DLLEXPORT std::vector<std::vector<rectangle*>*>* stdvector_stdvector_rectangle_new2(size_t size)
{
    return new std::vector<std::vector<rectangle*>*>(size);
}

DLLEXPORT std::vector<std::vector<rectangle*>*>* stdvector_stdvector_rectangle_new3(std::vector<rectangle*>** data, size_t dataLength)
{
    return new std::vector<std::vector<rectangle*>*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_stdvector_rectangle_getSize(std::vector<std::vector<rectangle*>*>* vector)
{
    return vector->size();
}

DLLEXPORT std::vector<rectangle*>* stdvector_stdvector_rectangle_getPointer(std::vector<std::vector<rectangle*>*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_stdvector_rectangle_delete(std::vector<std::vector<rectangle*>*> *vector)
{    
    delete vector;
}

// This method is unsafe!!
DLLEXPORT void stdvector_stdvector_rectangle_copy(std::vector<std::vector<rectangle*>*> *vector, std::vector<rectangle*>** dst)
{
    size_t length = sizeof(rectangle*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion std::vector<rectangle>

#pragma region full_object_detection

DLLEXPORT std::vector<full_object_detection*>* stdvector_full_object_detection_new1()
{
    return new std::vector<full_object_detection*>;
}

DLLEXPORT std::vector<full_object_detection*>* stdvector_full_object_detection_new2(size_t size)
{
    return new std::vector<full_object_detection*>(size);
}

DLLEXPORT std::vector<full_object_detection*>* stdvector_full_object_detection_new3(full_object_detection** data, size_t dataLength)
{
    return new std::vector<full_object_detection*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_full_object_detection_getSize(std::vector<full_object_detection*>* vector)
{
    return vector->size();
}

DLLEXPORT full_object_detection* stdvector_full_object_detection_getPointer(std::vector<full_object_detection*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_full_object_detection_delete(std::vector<full_object_detection*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_full_object_detection_copy(std::vector<full_object_detection*> *vector, full_object_detection** dst)
{
    size_t length = sizeof(full_object_detection*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion full_object_detection

#pragma region rect_detection

DLLEXPORT std::vector<rect_detection*>* stdvector_rect_detection_new1()
{
    return new std::vector<rect_detection*>;
}

DLLEXPORT std::vector<rect_detection*>* stdvector_rect_detection_new2(size_t size)
{
    return new std::vector<rect_detection*>(size);
}

DLLEXPORT std::vector<rect_detection*>* stdvector_rect_detection_new3(rect_detection** data, size_t dataLength)
{
    return new std::vector<rect_detection*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_rect_detection_getSize(std::vector<rect_detection*>* vector)
{
    return vector->size();
}

DLLEXPORT rect_detection* stdvector_rect_detection_getPointer(std::vector<rect_detection*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_rect_detection_delete(std::vector<rect_detection*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_rect_detection_copy(std::vector<rect_detection*> *vector, rect_detection** dst)
{
    size_t length = sizeof(rect_detection*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion rect_detection

#pragma region chip_details

DLLEXPORT std::vector<chip_details*>* stdvector_chip_details_new1()
{
    return new std::vector<chip_details*>;
}

DLLEXPORT std::vector<chip_details*>* stdvector_chip_details_new2(size_t size)
{
    return new std::vector<chip_details*>(size);
}

DLLEXPORT std::vector<chip_details*>* stdvector_chip_details_new3(chip_details** data, size_t dataLength)
{
    return new std::vector<chip_details*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_chip_details_getSize(std::vector<chip_details*>* vector)
{
    return vector->size();
}

DLLEXPORT chip_details* stdvector_chip_details_getPointer(std::vector<chip_details*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_chip_details_delete(std::vector<chip_details*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_chip_details_copy(std::vector<chip_details*> *vector, chip_details** dst)
{
    size_t length = sizeof(chip_details*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion chip_details

#pragma region sample_pair

DLLEXPORT std::vector<sample_pair*>* stdvector_sample_pair_new1()
{
    return new std::vector<sample_pair*>;
}

DLLEXPORT std::vector<sample_pair*>* stdvector_sample_pair_new2(size_t size)
{
    return new std::vector<sample_pair*>(size);
}

DLLEXPORT std::vector<sample_pair*>* stdvector_sample_pair_new3(sample_pair** data, size_t dataLength)
{
    return new std::vector<sample_pair*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_sample_pair_getSize(std::vector<sample_pair*>* vector)
{
    return vector->size();
}

DLLEXPORT sample_pair* stdvector_sample_pair_getPointer(std::vector<sample_pair*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_sample_pair_delete(std::vector<sample_pair*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_sample_pair_copy(std::vector<sample_pair*> *vector, sample_pair** dst)
{
    size_t length = sizeof(sample_pair*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion sample_pair

#pragma region mmod_rect

DLLEXPORT std::vector<mmod_rect*>* stdvector_mmod_rect_new1()
{
    return new std::vector<mmod_rect*>;
}

DLLEXPORT std::vector<mmod_rect*>* stdvector_mmod_rect_new2(size_t size)
{
    return new std::vector<mmod_rect*>(size);
}

DLLEXPORT std::vector<mmod_rect*>* stdvector_mmod_rect_new3(mmod_rect** data, size_t dataLength)
{
    return new std::vector<mmod_rect*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_mmod_rect_getSize(std::vector<mmod_rect*>* vector)
{
    return vector->size();
}

DLLEXPORT mmod_rect* stdvector_mmod_rect_getPointer(std::vector<mmod_rect*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_mmod_rect_delete(std::vector<mmod_rect*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_mmod_rect_copy(std::vector<mmod_rect*> *vector, mmod_rect** dst)
{
    size_t length = sizeof(mmod_rect*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion mmod_rect

#pragma region std::vector<mmod_rect>

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* stdvector_stdvector_mmod_rect_new1()
{
    return new std::vector<std::vector<mmod_rect*>*>;
}

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* stdvector_stdvector_mmod_rect_new2(size_t size)
{
    return new std::vector<std::vector<mmod_rect*>*>(size);
}

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* stdvector_stdvector_mmod_rect_new3(std::vector<mmod_rect*>** data, size_t dataLength)
{
    return new std::vector<std::vector<mmod_rect*>*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_stdvector_mmod_rect_getSize(std::vector<std::vector<mmod_rect*>*>* vector)
{
    return vector->size();
}

DLLEXPORT std::vector<mmod_rect*>* stdvector_stdvector_mmod_rect_getPointer(std::vector<std::vector<mmod_rect*>*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_stdvector_mmod_rect_delete(std::vector<std::vector<mmod_rect*>*> *vector)
{    
    delete vector;
}

// This method is unsafe!!
DLLEXPORT void stdvector_stdvector_mmod_rect_copy(std::vector<std::vector<mmod_rect*>*> *vector, std::vector<mmod_rect*>** dst)
{
    size_t length = sizeof(mmod_rect*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion std::vector<mmod_rect>

#ifndef DLIB_NO_GUI_SUPPORT
#pragma region image_window::overlay_line

DLLEXPORT std::vector<image_window::overlay_line*>* stdvector_image_window_overlay_line_new1()
{
    return new std::vector<image_window::overlay_line*>;
}

DLLEXPORT std::vector<image_window::overlay_line*>* stdvector_image_window_overlay_line_new2(size_t size)
{
    return new std::vector<image_window::overlay_line*>(size);
}

DLLEXPORT std::vector<image_window::overlay_line*>* stdvector_image_window_overlay_line_new3(image_window::overlay_line** data, size_t dataLength)
{
    return new std::vector<image_window::overlay_line*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_image_window_overlay_line_getSize(std::vector<image_window::overlay_line*>* vector)
{
    return vector->size();
}

DLLEXPORT image_window::overlay_line* stdvector_image_window_overlay_line_getPointer(std::vector<image_window::overlay_line*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_image_window_overlay_line_delete(std::vector<image_window::overlay_line*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_image_window_overlay_line_copy(std::vector<image_window::overlay_line*> *vector, image_window::overlay_line** dst)
{
    size_t length = sizeof(image_window::overlay_line*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion image_window::overlay_line

#pragma region perspective_window::overlay_dot

DLLEXPORT std::vector<perspective_window::overlay_dot*>* stdvector_perspective_window_overlay_dot_new1()
{
    return new std::vector<perspective_window::overlay_dot*>;
}

DLLEXPORT std::vector<perspective_window::overlay_dot*>* stdvector_perspective_window_overlay_dot_new2(size_t size)
{
    return new std::vector<perspective_window::overlay_dot*>(size);
}

DLLEXPORT std::vector<perspective_window::overlay_dot*>* stdvector_perspective_window_overlay_dot_new3(perspective_window::overlay_dot** data, size_t dataLength)
{
    return new std::vector<perspective_window::overlay_dot*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_perspective_window_overlay_dot_getSize(std::vector<perspective_window::overlay_dot*>* vector)
{
    return vector->size();
}

DLLEXPORT perspective_window::overlay_dot* stdvector_perspective_window_overlay_dot_getPointer(std::vector<perspective_window::overlay_dot*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_perspective_window_overlay_dot_delete(std::vector<perspective_window::overlay_dot*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_perspective_window_overlay_dot_copy(std::vector<perspective_window::overlay_dot*> *vector, perspective_window::overlay_dot** dst)
{
    size_t length = sizeof(perspective_window::overlay_dot*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion perspective_window::overlay_dot

#pragma region dlib::surf_point

DLLEXPORT std::vector<dlib::surf_point*>* stdvector_surf_point_new1()
{
    return new std::vector<dlib::surf_point*>;
}

DLLEXPORT std::vector<dlib::surf_point*>* stdvector_surf_point_new2(size_t size)
{
    return new std::vector<dlib::surf_point*>(size);
}

DLLEXPORT std::vector<dlib::surf_point*>* stdvector_surf_point_new3(dlib::surf_point** data, size_t dataLength)
{
    return new std::vector<dlib::surf_point*>(data, data + dataLength);
}

DLLEXPORT size_t stdvector_surf_point_getSize(std::vector<dlib::surf_point*>* vector)
{
    return vector->size();
}

DLLEXPORT dlib::surf_point* stdvector_surf_point_getPointer(std::vector<dlib::surf_point*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void stdvector_surf_point_delete(std::vector<dlib::surf_point*> *vector)
{    
    delete vector;
}

DLLEXPORT void stdvector_surf_point_copy(std::vector<dlib::surf_point*> *vector, dlib::surf_point** dst)
{
    size_t length = sizeof(dlib::surf_point*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion dlib::surf_point

#endif

#endif