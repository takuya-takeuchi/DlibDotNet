#ifndef _CPP_VECTOR_H_
#define _CPP_VECTOR_H_

#include <dlib/geometry/rectangle.h>
#include <dlib/gui_widgets/widgets.h>
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/image_transforms/interpolation.h>

using namespace dlib;

#pragma region int32_t

extern "C" __declspec(dllexport) std::vector<int32_t>* vector_int32_new1()
{
    return new std::vector<int32_t>;
}

extern "C" __declspec(dllexport) std::vector<int32_t>* vector_int32_new2(size_t size)
{
    return new std::vector<int32_t>(size);
}

extern "C" __declspec(dllexport) std::vector<int32_t>* vector_int32_new3(int32_t* data, size_t dataLength)
{
    return new std::vector<int32_t>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_int32_getSize(std::vector<int32_t>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) int32_t* vector_int32_getPointer(std::vector<int32_t> *vector)
{
    return &(vector->at(0));
}

extern "C" __declspec(dllexport) void vector_int32_delete(std::vector<int32_t> *vector)
{    
    delete vector;
}

#pragma endregion int32_t

#pragma region int64_t

extern "C" __declspec(dllexport) std::vector<int64_t>* vector_long_new1()
{
    return new std::vector<int64_t>;
}

extern "C" __declspec(dllexport) std::vector<int64_t>* vector_long_new2(size_t size)
{
    return new std::vector<int64_t>(size);
}

extern "C" __declspec(dllexport) std::vector<int64_t>* vector_long_new3(int64_t* data, size_t dataLength)
{
    return new std::vector<int64_t>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_long_getSize(std::vector<int64_t>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) int64_t* vector_long_getPointer(std::vector<int64_t> *vector)
{
    return &(vector->at(0));
}

extern "C" __declspec(dllexport) void vector_long_delete(std::vector<int64_t> *vector)
{    
    delete vector;
}

#pragma endregion int64_t

#pragma region rectangle

extern "C" __declspec(dllexport) std::vector<rectangle*>* vector_rectangle_new1()
{
    return new std::vector<rectangle*>;
}

extern "C" __declspec(dllexport) std::vector<rectangle*>* vector_rectangle_new2(size_t size)
{
    return new std::vector<rectangle*>(size);
}

extern "C" __declspec(dllexport) std::vector<rectangle*>* vector_rectangle_new3(rectangle** data, size_t dataLength)
{
    return new std::vector<rectangle*>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_rectangle_getSize(std::vector<rectangle*>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) rectangle* vector_rectangle_getPointer(std::vector<rectangle*> *vector)
{
    return (vector->at(0));
}

extern "C" __declspec(dllexport) void vector_rectangle_delete(std::vector<rectangle*> *vector)
{    
    delete vector;
}

extern "C" __declspec(dllexport) void vector_rectangle_copy(std::vector<rectangle*> *vector, rectangle** dst)
{
    size_t length = sizeof(rectangle*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion rectangle

#pragma region image_window::overlay_line

extern "C" __declspec(dllexport) std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new1()
{
    return new std::vector<image_window::overlay_line*>;
}

extern "C" __declspec(dllexport) std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new2(size_t size)
{
    return new std::vector<image_window::overlay_line*>(size);
}

extern "C" __declspec(dllexport) std::vector<image_window::overlay_line*>* vector_image_window_overlay_line_new3(image_window::overlay_line** data, size_t dataLength)
{
    return new std::vector<image_window::overlay_line*>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_image_window_overlay_line_getSize(std::vector<image_window::overlay_line*>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) image_window::overlay_line* vector_image_window_overlay_line_getPointer(std::vector<image_window::overlay_line*> *vector)
{
    return (vector->at(0));
}

extern "C" __declspec(dllexport) void vector_image_window_overlay_line_delete(std::vector<image_window::overlay_line*> *vector)
{    
    delete vector;
}

extern "C" __declspec(dllexport) void vector_image_window_overlay_line_copy(std::vector<image_window::overlay_line*> *vector, image_window::overlay_line** dst)
{
    size_t length = sizeof(image_window::overlay_line*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion image_window::overlay_line

#pragma region full_object_detection

extern "C" __declspec(dllexport) std::vector<full_object_detection*>* vector_full_object_detection_new1()
{
    return new std::vector<full_object_detection*>;
}

extern "C" __declspec(dllexport) std::vector<full_object_detection*>* vector_full_object_detection_new2(size_t size)
{
    return new std::vector<full_object_detection*>(size);
}

extern "C" __declspec(dllexport) std::vector<full_object_detection*>* vector_full_object_detection_new3(full_object_detection** data, size_t dataLength)
{
    return new std::vector<full_object_detection*>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_full_object_detection_getSize(std::vector<full_object_detection*>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) full_object_detection* vector_full_object_detection_getPointer(std::vector<full_object_detection*> *vector)
{
    return (vector->at(0));
}

extern "C" __declspec(dllexport) void vector_full_object_detection_delete(std::vector<full_object_detection*> *vector)
{    
    delete vector;
}

extern "C" __declspec(dllexport) void vector_full_object_detection_copy(std::vector<full_object_detection*> *vector, full_object_detection** dst)
{
    size_t length = sizeof(full_object_detection*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion full_object_detection

#pragma region chip_details

extern "C" __declspec(dllexport) std::vector<chip_details*>* vector_chip_details_new1()
{
    return new std::vector<chip_details*>;
}

extern "C" __declspec(dllexport) std::vector<chip_details*>* vector_chip_details_new2(size_t size)
{
    return new std::vector<chip_details*>(size);
}

extern "C" __declspec(dllexport) std::vector<chip_details*>* vector_chip_details_new3(chip_details** data, size_t dataLength)
{
    return new std::vector<chip_details*>(data, data + dataLength);
}

extern "C" __declspec(dllexport) size_t vector_chip_details_getSize(std::vector<chip_details*>* vector)
{
    return vector->size();
}

extern "C" __declspec(dllexport) chip_details* vector_chip_details_getPointer(std::vector<chip_details*> *vector)
{
    return (vector->at(0));
}

extern "C" __declspec(dllexport) void vector_chip_details_delete(std::vector<chip_details*> *vector)
{    
    delete vector;
}

extern "C" __declspec(dllexport) void vector_chip_details_copy(std::vector<chip_details*> *vector, chip_details** dst)
{
    size_t length = sizeof(chip_details*)* vector->size();
    memcpy(dst, &(vector->at(0)), length);
}

#pragma endregion chip_details

#endif