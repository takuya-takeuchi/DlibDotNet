#ifndef _CPP_VECTOR_H_
#define _CPP_VECTOR_H_

#include "export.h"
#include <dlib/geometry/rectangle.h>

#ifndef DLIB_NO_GUI_SUPPORT
#include <dlib/gui_widgets/widgets.h>
#endif

#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_transforms/interpolation.h>
#include "shared.h"

using namespace dlib;

#pragma region int32_t

DLLEXPORT std::vector<int32_t>* vector_int32_new1()
{
    return new std::vector<int32_t>;
}

DLLEXPORT std::vector<int32_t>* vector_int32_new2(size_t size)
{
    return new std::vector<int32_t>(size);
}

DLLEXPORT std::vector<int32_t>* vector_int32_new3(int32_t* data, size_t dataLength)
{
    return new std::vector<int32_t>(data, data + dataLength);
}

DLLEXPORT size_t vector_int32_getSize(std::vector<int32_t>* vector)
{
    return vector->size();
}

DLLEXPORT int32_t* vector_int32_getPointer(std::vector<int32_t> *vector)
{
    return &(vector->at(0));
}

DLLEXPORT void vector_int32_delete(std::vector<int32_t> *vector)
{    
    delete vector;
}

#pragma endregion int32_t

#pragma region int64_t

DLLEXPORT std::vector<int64_t>* vector_long_new1()
{
    return new std::vector<int64_t>;
}

DLLEXPORT std::vector<int64_t>* vector_long_new2(size_t size)
{
    return new std::vector<int64_t>(size);
}

DLLEXPORT std::vector<int64_t>* vector_long_new3(int64_t* data, size_t dataLength)
{
    return new std::vector<int64_t>(data, data + dataLength);
}

DLLEXPORT size_t vector_long_getSize(std::vector<int64_t>* vector)
{
    return vector->size();
}

DLLEXPORT int64_t* vector_long_getPointer(std::vector<int64_t> *vector)
{
    return &(vector->at(0));
}

DLLEXPORT void vector_long_delete(std::vector<int64_t> *vector)
{    
    delete vector;
}

#pragma endregion int64_t

#pragma region rectangle

DLLEXPORT std::vector<rectangle*>* vector_rectangle_new1()
{
    return new std::vector<rectangle*>();
}

DLLEXPORT std::vector<rectangle*>* vector_rectangle_new2(size_t size)
{
    return new std::vector<rectangle*>(size);
}

DLLEXPORT std::vector<rectangle*>* vector_rectangle_new3(rectangle** data, size_t dataLength)
{
    return new std::vector<rectangle*>(data, data + dataLength);
}

DLLEXPORT size_t vector_rectangle_getSize(std::vector<rectangle*>* vector)
{
    return vector->size();
}

DLLEXPORT rectangle* vector_rectangle_getPointer(std::vector<rectangle*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_rectangle_delete(std::vector<rectangle*> *vector)
{    
    delete vector;
}

DLLEXPORT void vector_rectangle_copy(std::vector<rectangle*> *vector, rectangle** dst)
{
    size_t length = sizeof(rectangle*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion rectangle

#pragma region matrix

DLLEXPORT void* vector_matrix_new1(matrix_element_type type)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new std::vector<matrix<uint8_t>*>();
        case matrix_element_type::UInt16:
            return new std::vector<matrix<uint16_t>*>();
        case matrix_element_type::UInt32:
            return new std::vector<matrix<uint32_t>*>();
        case matrix_element_type::Int8:
            return new std::vector<matrix<int8_t>*>();
        case matrix_element_type::Int16:
            return new std::vector<matrix<int16_t>*>();
        case matrix_element_type::Int32:
            return new std::vector<matrix<int32_t>*>();
        case matrix_element_type::Float:
            return new std::vector<matrix<float>*>();
        case matrix_element_type::Double:
            return new std::vector<matrix<double>*>();    
        case matrix_element_type::RgbPixel:
            return new std::vector<matrix<rgb_pixel>*>();
        case matrix_element_type::HsiPixel:
            return new std::vector<matrix<hsi_pixel>*>();
        case matrix_element_type::RgbAlphaPixel:
            return new std::vector<matrix<rgb_alpha_pixel>*>();
        default:
            return nullptr;
    }
}

DLLEXPORT void* vector_matrix_new2(matrix_element_type type, size_t size)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            return new std::vector<matrix<uint8_t>*>(size);
        case matrix_element_type::UInt16:
            return new std::vector<matrix<uint16_t>*>(size);
        case matrix_element_type::UInt32:
            return new std::vector<matrix<uint32_t>*>(size);
        case matrix_element_type::Int8:
            return new std::vector<matrix<int8_t>*>(size);
        case matrix_element_type::Int16:
            return new std::vector<matrix<int16_t>*>(size);
        case matrix_element_type::Int32:
            return new std::vector<matrix<int32_t>*>(size);
        case matrix_element_type::Float:
            return new std::vector<matrix<float>*>(size);
        case matrix_element_type::Double:
            return new std::vector<matrix<double>*>(size);    
        case matrix_element_type::RgbPixel:
            return new std::vector<matrix<rgb_pixel>*>(size);
        case matrix_element_type::HsiPixel:
            return new std::vector<matrix<hsi_pixel>*>(size);
        case matrix_element_type::RgbAlphaPixel:
            return new std::vector<matrix<rgb_alpha_pixel>*>(size);
        default:
            return nullptr;
    }
}

DLLEXPORT void* vector_matrix_new3(matrix_element_type type, void** data, size_t dataLength)
{
    switch(type)
    {
        case matrix_element_type::UInt8:
            {
                auto tmp = (matrix<uint8_t>**)(data);
                return new std::vector<matrix<uint8_t>*>(tmp, tmp + dataLength);
            }
            case matrix_element_type::UInt16:
            {
                auto tmp = (matrix<uint16_t>**)(data);
                return new std::vector<matrix<uint16_t>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::UInt32:
            {
                auto tmp = (matrix<uint32_t>**)(data);
                return new std::vector<matrix<uint32_t>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::Int8:
            {
                auto tmp = (matrix<int8_t>**)(data);
                return new std::vector<matrix<int8_t>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::Int16:
            {
                auto tmp = (matrix<int16_t>**)(data);
                return new std::vector<matrix<int16_t>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::Int32:
            {
                auto tmp = (matrix<int32_t>**)(data);
                return new std::vector<matrix<int32_t>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::Float:
            {
                auto tmp = (matrix<float>**)(data);
                return new std::vector<matrix<float>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::Double:
            {
                auto tmp = (matrix<double>**)(data);
                return new std::vector<matrix<double>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::RgbPixel:
            {
                auto tmp = (matrix<rgb_pixel>**)(data);
                return new std::vector<matrix<rgb_pixel>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::HsiPixel:
            {
                auto tmp = (matrix<hsi_pixel>**)(data);
                return new std::vector<matrix<hsi_pixel>*>(tmp, tmp + dataLength);
            }
        case matrix_element_type::RgbAlphaPixel:
            {
                auto tmp = (matrix<rgb_alpha_pixel>**)(data);
                return new std::vector<matrix<rgb_alpha_pixel>*>(tmp, tmp + dataLength);
            }
        default:
            return nullptr;
    }
}

DLLEXPORT size_t vector_matrix_getSize(std::vector<void*>* vector)
{
    return vector->size();
}

DLLEXPORT void* vector_matrix_getPointer(std::vector<void*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_matrix_delete(matrix_element_type type, std::vector<void*> *vector)
{    
    switch(type)
    {
        case matrix_element_type::UInt8:
            {
                auto tmp = (std::vector<matrix<uint8_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::UInt16:
            {
                auto tmp = (std::vector<matrix<uint16_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::UInt32:
            {
                auto tmp = (std::vector<matrix<uint32_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::Int8:
            {
                auto tmp = (std::vector<matrix<int8_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::Int16:
            {
                auto tmp = (std::vector<matrix<int16_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::Int32:
            {
                auto tmp = (std::vector<matrix<int32_t>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::Float:
            {
                auto tmp = (std::vector<matrix<float>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::Double:
            {
                auto tmp = (std::vector<matrix<double>*>*)(vector);
                delete tmp;
            } 
            break; 
        case matrix_element_type::RgbPixel:
            {
                auto tmp = (std::vector<matrix<rgb_pixel>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::HsiPixel:
            {
                auto tmp = (std::vector<matrix<hsi_pixel>*>*)(vector);
                delete tmp;
            }
            break;
        case matrix_element_type::RgbAlphaPixel:
            {
                auto tmp = (std::vector<matrix<rgb_alpha_pixel>*>*)(vector);
                delete tmp;
            }
            break;
        default:
            break;
    }
}

DLLEXPORT void vector_matrix_copy(std::vector<void*> *vector, void** dst)
{
    size_t length = sizeof(void*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion matrix

#pragma region std::vector<rectangle>

DLLEXPORT std::vector<std::vector<rectangle*>*>* vector_vector_rectangle_new1()
{
    return new std::vector<std::vector<rectangle*>*>;
}

DLLEXPORT std::vector<std::vector<rectangle*>*>* vector_vector_rectangle_new2(size_t size)
{
    return new std::vector<std::vector<rectangle*>*>(size);
}

DLLEXPORT std::vector<std::vector<rectangle*>*>* vector_vector_rectangle_new3(std::vector<rectangle*>** data, size_t dataLength)
{
    return new std::vector<std::vector<rectangle*>*>(data, data + dataLength);
}

DLLEXPORT size_t vector_vector_rectangle_getSize(std::vector<std::vector<rectangle*>*>* vector)
{
    return vector->size();
}

DLLEXPORT std::vector<rectangle*>* vector_vector_rectangle_getPointer(std::vector<std::vector<rectangle*>*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_vector_rectangle_delete(std::vector<std::vector<rectangle*>*> *vector)
{    
    delete vector;
}

// This method is unsafe!!
DLLEXPORT void vector_vector_rectangle_copy(std::vector<std::vector<rectangle*>*> *vector, std::vector<rectangle*>** dst)
{
    size_t length = sizeof(rectangle*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion std::vector<rectangle>

#pragma region full_object_detection

DLLEXPORT std::vector<full_object_detection*>* vector_full_object_detection_new1()
{
    return new std::vector<full_object_detection*>;
}

DLLEXPORT std::vector<full_object_detection*>* vector_full_object_detection_new2(size_t size)
{
    return new std::vector<full_object_detection*>(size);
}

DLLEXPORT std::vector<full_object_detection*>* vector_full_object_detection_new3(full_object_detection** data, size_t dataLength)
{
    return new std::vector<full_object_detection*>(data, data + dataLength);
}

DLLEXPORT size_t vector_full_object_detection_getSize(std::vector<full_object_detection*>* vector)
{
    return vector->size();
}

DLLEXPORT full_object_detection* vector_full_object_detection_getPointer(std::vector<full_object_detection*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_full_object_detection_delete(std::vector<full_object_detection*> *vector)
{    
    delete vector;
}

DLLEXPORT void vector_full_object_detection_copy(std::vector<full_object_detection*> *vector, full_object_detection** dst)
{
    size_t length = sizeof(full_object_detection*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion full_object_detection

#pragma region chip_details

DLLEXPORT std::vector<chip_details*>* vector_chip_details_new1()
{
    return new std::vector<chip_details*>;
}

DLLEXPORT std::vector<chip_details*>* vector_chip_details_new2(size_t size)
{
    return new std::vector<chip_details*>(size);
}

DLLEXPORT std::vector<chip_details*>* vector_chip_details_new3(chip_details** data, size_t dataLength)
{
    return new std::vector<chip_details*>(data, data + dataLength);
}

DLLEXPORT size_t vector_chip_details_getSize(std::vector<chip_details*>* vector)
{
    return vector->size();
}

DLLEXPORT chip_details* vector_chip_details_getPointer(std::vector<chip_details*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_chip_details_delete(std::vector<chip_details*> *vector)
{    
    delete vector;
}

DLLEXPORT void vector_chip_details_copy(std::vector<chip_details*> *vector, chip_details** dst)
{
    size_t length = sizeof(chip_details*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion chip_details

#pragma region mmod_rect

DLLEXPORT std::vector<mmod_rect*>* vector_mmod_rect_new1()
{
    return new std::vector<mmod_rect*>;
}

DLLEXPORT std::vector<mmod_rect*>* vector_mmod_rect_new2(size_t size)
{
    return new std::vector<mmod_rect*>(size);
}

DLLEXPORT std::vector<mmod_rect*>* vector_mmod_rect_new3(mmod_rect** data, size_t dataLength)
{
    return new std::vector<mmod_rect*>(data, data + dataLength);
}

DLLEXPORT size_t vector_mmod_rect_getSize(std::vector<mmod_rect*>* vector)
{
    return vector->size();
}

DLLEXPORT mmod_rect* vector_mmod_rect_getPointer(std::vector<mmod_rect*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_mmod_rect_delete(std::vector<mmod_rect*> *vector)
{    
    delete vector;
}

DLLEXPORT void vector_mmod_rect_copy(std::vector<mmod_rect*> *vector, mmod_rect** dst)
{
    size_t length = sizeof(mmod_rect*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion mmod_rect

#pragma region std::vector<mmod_rect>

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* vector_vector_mmod_rect_new1()
{
    return new std::vector<std::vector<mmod_rect*>*>;
}

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* vector_vector_mmod_rect_new2(size_t size)
{
    return new std::vector<std::vector<mmod_rect*>*>(size);
}

DLLEXPORT std::vector<std::vector<mmod_rect*>*>* vector_vector_mmod_rect_new3(std::vector<mmod_rect*>** data, size_t dataLength)
{
    return new std::vector<std::vector<mmod_rect*>*>(data, data + dataLength);
}

DLLEXPORT size_t vector_vector_mmod_rect_getSize(std::vector<std::vector<mmod_rect*>*>* vector)
{
    return vector->size();
}

DLLEXPORT std::vector<mmod_rect*>* vector_vector_mmod_rect_getPointer(std::vector<std::vector<mmod_rect*>*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_vector_mmod_rect_delete(std::vector<std::vector<mmod_rect*>*> *vector)
{    
    delete vector;
}

// This method is unsafe!!
DLLEXPORT void vector_vector_mmod_rect_copy(std::vector<std::vector<mmod_rect*>*> *vector, std::vector<mmod_rect*>** dst)
{
    size_t length = sizeof(mmod_rect*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion std::vector<mmod_rect>

#ifndef DLIB_NO_GUI_SUPPORT
#pragma region image_window::overlay_line

DLLEXPORT std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new1()
{
    return new std::vector<image_window::overlay_line*>;
}

DLLEXPORT std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new2(size_t size)
{
    return new std::vector<image_window::overlay_line*>(size);
}

DLLEXPORT std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new3(image_window::overlay_line** data, size_t dataLength)
{
    return new std::vector<image_window::overlay_line*>(data, data + dataLength);
}

DLLEXPORT size_t vector_image_window_overlay_line_getSize(std::vector<image_window::overlay_line*>* vector)
{
    return vector->size();
}

DLLEXPORT image_window::overlay_line* vector_image_window_overlay_line_getPointer(std::vector<image_window::overlay_line*> *vector)
{
    return (vector->at(0));
}

DLLEXPORT void vector_image_window_overlay_line_delete(std::vector<image_window::overlay_line*> *vector)
{    
    delete vector;
}

DLLEXPORT void vector_image_window_overlay_line_copy(std::vector<image_window::overlay_line*> *vector, image_window::overlay_line** dst)
{
    size_t length = sizeof(image_window::overlay_line*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion image_window::overlay_line
#endif

#endif