#ifndef _CPP_EXTENSIONS_H_
#define _CPP_EXTENSIONS_H_

#include "../export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include "../shared.h"

#define ELEMENT element
#undef ELEMENT

using namespace dlib;

#pragma region extensions_load_image_data

#define extensions_load_image_data_from_to_sametype(data, rows, columns, steps) \
do { \
    dlib::array2d<ELEMENT>* ret = new dlib::array2d<ELEMENT>(rows, columns);\
    dlib::array2d<ELEMENT>& dst = *(ret);\
    ELEMENT* src = static_cast<ELEMENT*>(data);\
    for (uint32_t r = 0; r < rows; r++)\
    for (uint32_t c = 0; c < columns; c++)\
        dst[r][c] = src[steps * r + c];\
    return ret;\
} while (0)

DLLEXPORT void* extensions_load_image_data(array2d_type dst_type, array2d_type src_type, void* data, uint32_t rows, uint32_t columns, uint32_t steps)
{
    switch(dst_type)
    {
        case array2d_type::UInt8:
            {
                switch(src_type)
                {
                    // from uint8_t to uint8_t
                    case array2d_type::UInt8:
                        #define ELEMENT uint8_t
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::UInt16:
            {
                switch(src_type)
                {
                    // from uint16_t to uint16_t
                    case array2d_type::UInt16:
                        #define ELEMENT uint16_t
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::Int16:
            {
                switch(src_type)
                {
                    // from int16_t to int16_t
                    case array2d_type::Int16:
                        #define ELEMENT int16_t
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::Int32:
            {
                switch(src_type)
                {
                    // from int32_t to int32_t
                    case array2d_type::Int32:
                        #define ELEMENT int32_t
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::Float:
            {
                switch(src_type)
                {
                    // from float to float
                    case array2d_type::Float:
                        #define ELEMENT float
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::Double:
            {
                switch(src_type)
                {
                    // from double to double
                    case array2d_type::Double:
                        #define ELEMENT double
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::RgbPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::RgbPixel:
            {
                switch(src_type)
                {
                    // from rgb_pixel to rgb_pixel
                    case array2d_type::RgbPixel:
                        #define ELEMENT rgb_pixel
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::HsiPixel:
            {
                switch(src_type)
                {
                    // from hsi_pixel to hsi_pixel
                    case array2d_type::HsiPixel:
                        #define ELEMENT hsi_pixel
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::RgbAlphaPixel:
            {
                switch(src_type)
                {
                    // from rgb_alpha_pixel to rgb_alpha_pixel
                    case array2d_type::HsiPixel:
                        #define ELEMENT rgb_alpha_pixel
                        extensions_load_image_data_from_to_sametype(data, rows, columns, steps);
                        #undef ELEMENT
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        default:
			return nullptr;
    }
}

#pragma endregion extensions_load_image_data

#endif