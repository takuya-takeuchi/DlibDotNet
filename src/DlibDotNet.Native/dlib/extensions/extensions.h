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
                        {
                            dlib::array2d<rgb_pixel>* ret = new dlib::array2d<rgb_pixel>(rows, columns);
                            dlib::array2d<rgb_pixel>& dst = *(ret);
                            uint8_t* src = static_cast<uint8_t*>(data);
                            for (uint32_t r = 0; r < rows; r++)
                            {
                                uint32_t src_row = steps * r;
                                auto drow = dst[r];
                                for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += 3)
                                {
                                    drow[c].red   = src[src_row + dst_column + 2];
                                    drow[c].green = src[src_row + dst_column + 1];
                                    drow[c].blue  = src[src_row + dst_column + 0];
                                }   
                            }
                            return ret;
                        }
                        break;
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
        case array2d_type::RgbAlphaPixel:
            {
                switch(src_type)
                {
                    // from rgb_alpha_pixel to rgb_alpha_pixel
                    case array2d_type::RgbAlphaPixel:
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
                    case array2d_type::HsiPixel:
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
        default:
			return nullptr;
    }
}

#pragma endregion extensions_load_image_data

#pragma region extensions_convert_array_to_managed_image

DLLEXPORT void extensions_convert_array_to_managed_image(array2d_type src_type, void* src, void* dst, bool rgb_reverse, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(src_type)
    {
        case array2d_type::RgbPixel:
            {
                dlib::array2d<dlib::rgb_pixel>& s = *(static_cast<dlib::array2d<dlib::rgb_pixel>*>(src));
                uint8_t* d = static_cast<uint8_t*>(dst);
                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            d[dst_row + dst_column + 2] = s[r][c].red;
                            d[dst_row + dst_column + 1] = s[r][c].green;
                            d[dst_row + dst_column + 0] = s[r][c].blue;
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            d[dst_row + dst_column + 0] = s[r][c].red;
                            d[dst_row + dst_column + 1] = s[r][c].green;
                            d[dst_row + dst_column + 2] = s[r][c].blue;
                        }
                    }
                }
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                dlib::array2d<dlib::rgb_alpha_pixel>& s = *(static_cast<dlib::array2d<dlib::rgb_alpha_pixel>*>(src));
                uint8_t* d = static_cast<uint8_t*>(dst);
                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            d[dst_row + dst_column + 3] = s[r][c].alpha;
                            d[dst_row + dst_column + 2] = s[r][c].red;
                            d[dst_row + dst_column + 1] = s[r][c].green;
                            d[dst_row + dst_column + 0] = s[r][c].blue;
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            d[dst_row + dst_column + 0] = s[r][c].red;
                            d[dst_row + dst_column + 1] = s[r][c].green;
                            d[dst_row + dst_column + 2] = s[r][c].blue;
                            d[dst_row + dst_column + 3] = s[r][c].alpha;
                        }
                    }
                }
            }
            break;
        case array2d_type::UInt8:
        case array2d_type::UInt16:
        case array2d_type::Int16:
        case array2d_type::Int32:
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::HsiPixel:
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_array_to_managed_image

#pragma region extensions_convert_managed_image_to_array

DLLEXPORT void extensions_convert_managed_image_to_array(void* src, array2d_type dst_type, void* dst, bool rgb_reverse, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(dst_type)
    {
        case array2d_type::RgbPixel:
            {
                dlib::array2d<dlib::rgb_pixel>& d = *(static_cast<dlib::array2d<dlib::rgb_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);

                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        auto row = d[r];
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            row[c].red   = s[src_row + dst_column + 2];
                            row[c].green = s[src_row + dst_column + 1];
                            row[c].blue  = s[src_row + dst_column + 0];
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        auto row = d[r];
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            row[c].red   = s[src_row + dst_column + 0];
                            row[c].green = s[src_row + dst_column + 1];
                            row[c].blue  = s[src_row + dst_column + 2];
                        }
                    }
                }
            }
            break;
        case array2d_type::RgbAlphaPixel:        
            {
                dlib::array2d<dlib::rgb_alpha_pixel>& d = *(static_cast<dlib::array2d<dlib::rgb_alpha_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);

                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        auto row = d[r];
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            row[c].alpha = s[src_row + dst_column + 3];
                            row[c].red   = s[src_row + dst_column + 2];
                            row[c].green = s[src_row + dst_column + 1];
                            row[c].blue  = s[src_row + dst_column + 0];
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        auto row = d[r];
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            row[c].red   = s[src_row + dst_column + 0];
                            row[c].green = s[src_row + dst_column + 1];
                            row[c].blue  = s[src_row + dst_column + 2];
                            row[c].alpha = s[src_row + dst_column + 3];
                        }
                    }
                }
            }
            break;
        case array2d_type::UInt8:
            {
                dlib::array2d<uint8_t>& d = *(static_cast<dlib::array2d<uint8_t>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);

                for (uint32_t r = 0; r < rows; r++)
                {
                    uint32_t src_row = steps * r;
                    for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        d[r][c] = s[src_row + dst_column];
                }
            }
            break;
        case array2d_type::UInt16:
        case array2d_type::Int16:
        case array2d_type::Int32:
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::HsiPixel:
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_array

#pragma region extensions_convert_managed_image_to_array_by_pallete

DLLEXPORT void extensions_convert_managed_image_to_array_by_pallete(void* src, array2d_type dst_type, void* dst, const dlib::rgb_pixel* pallete, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(dst_type)
    {
        case array2d_type::RgbPixel:
            {
                dlib::array2d<dlib::rgb_pixel>& d = *(static_cast<dlib::array2d<dlib::rgb_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);
                for (uint32_t r = 0; r < rows; r++)
                {
                    uint32_t src_row = steps * r;
                    auto row = d[r];
                    for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        row[c] = pallete[s[src_row + dst_column]];
                }
            }
            break;
        case array2d_type::UInt8:
            {
                dlib::array2d<uint8_t>& d = *(static_cast<dlib::array2d<uint8_t>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);
                for (uint32_t r = 0; r < rows; r++)
                {
                    uint32_t src_row = steps * r;
                    auto row = d[r];
                    for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        row[c] = pallete[s[src_row + dst_column]].red;
                }
            }
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_array_by_pallete

#pragma region extensions_convert_matrix_to_managed_image

DLLEXPORT void extensions_convert_matrix_to_managed_image(matrix_element_type src_type, void* src, void* dst, bool rgb_reverse, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(src_type)
    {
        case matrix_element_type::RgbPixel:
            {
                dlib::matrix<dlib::rgb_pixel>& s = *(static_cast<dlib::matrix<dlib::rgb_pixel>*>(src));
                uint8_t* d = static_cast<uint8_t*>(dst);
                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        uint32_t src_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_pixel& rgb = s(r, c);
                            d[dst_row + dst_column + 2] = rgb.red;
                            d[dst_row + dst_column + 1] = rgb.green;
                            d[dst_row + dst_column + 0] = rgb.blue;
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        uint32_t src_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_pixel& rgb = s(r, c);
                            d[dst_row + dst_column + 0] = rgb.red;
                            d[dst_row + dst_column + 1] = rgb.green;
                            d[dst_row + dst_column + 2] = rgb.blue;
                        }
                    }
                }
            }
            break;
        case matrix_element_type::RgbAlphaPixel:
            {
                dlib::matrix<dlib::rgb_alpha_pixel>& s = *(static_cast<dlib::matrix<dlib::rgb_alpha_pixel>*>(src));
                uint8_t* d = static_cast<uint8_t*>(dst);
                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        uint32_t src_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_alpha_pixel& rgb = s(r, c);
                            d[dst_row + dst_column + 3] = rgb.alpha;
                            d[dst_row + dst_column + 2] = rgb.red;
                            d[dst_row + dst_column + 1] = rgb.green;
                            d[dst_row + dst_column + 0] = rgb.blue;
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t dst_row = steps * r;
                        uint32_t src_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_alpha_pixel& rgb = s(r, c);
                            d[dst_row + dst_column + 0] = rgb.red;
                            d[dst_row + dst_column + 1] = rgb.green;
                            d[dst_row + dst_column + 2] = rgb.blue;
                            d[dst_row + dst_column + 3] = rgb.alpha;
                        }
                    }
                }
            }
            break;
        case matrix_element_type::UInt8:
        case matrix_element_type::UInt16:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Float:
        case matrix_element_type::Double:
        case matrix_element_type::HsiPixel:
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_matrix_to_managed_image

#pragma region extensions_convert_managed_image_to_matrix

DLLEXPORT void extensions_convert_managed_image_to_matrix(void* src, matrix_element_type dst_type, void* dst, bool rgb_reverse, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(dst_type)
    {
        case matrix_element_type::RgbPixel:
            {
                dlib::matrix<dlib::rgb_pixel>& d = *(static_cast<dlib::matrix<dlib::rgb_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);

                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        uint32_t dst_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_pixel& rgb = d(r, c);
                            rgb.red   = s[src_row + dst_column + 2];
                            rgb.green = s[src_row + dst_column + 1];
                            rgb.blue  = s[src_row + dst_column + 0];
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        uint32_t dst_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_pixel& rgb = d(r, c);
                            rgb.red   = s[src_row + dst_column + 0];
                            rgb.green = s[src_row + dst_column + 1];
                            rgb.blue  = s[src_row + dst_column + 2];
                        }
                    }
                }
            }
            break;
        case matrix_element_type::RgbAlphaPixel:        
            {
                dlib::matrix<dlib::rgb_alpha_pixel>& d = *(static_cast<dlib::matrix<dlib::rgb_alpha_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);

                if (rgb_reverse)
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        uint32_t dst_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_alpha_pixel& rgb = d(r, c);
                            rgb.alpha = s[src_row + dst_column + 3];
                            rgb.red   = s[src_row + dst_column + 2];
                            rgb.green = s[src_row + dst_column + 1];
                            rgb.blue  = s[src_row + dst_column + 0];
                        }
                    }
                }
                else
                {
                    for (uint32_t r = 0; r < rows; r++)
                    {
                        uint32_t src_row = steps * r;
                        uint32_t dst_row = columns * r;
                        for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        {
                            dlib::rgb_alpha_pixel& rgb = d(r, c);
                            rgb.red   = s[src_row + dst_column + 0];
                            rgb.green = s[src_row + dst_column + 1];
                            rgb.blue  = s[src_row + dst_column + 2];
                            rgb.alpha = s[src_row + dst_column + 3];
                        }
                    }
                }
            }
            break;
        case matrix_element_type::UInt8:
        case matrix_element_type::UInt16:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Float:
        case matrix_element_type::Double:
        case matrix_element_type::HsiPixel:
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_matrix

#pragma region extensions_convert_managed_image_to_matrix_by_pallete

DLLEXPORT void extensions_convert_managed_image_to_matrix_by_pallete(void* src, matrix_element_type dst_type, void* dst, const dlib::rgb_pixel* pallete, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
{
    switch(dst_type)
    {
        case matrix_element_type::RgbPixel:
            {
                dlib::matrix<dlib::rgb_pixel>& d = *(static_cast<dlib::matrix<dlib::rgb_pixel>*>(dst));
                uint8_t* s = static_cast<uint8_t*>(src);
                for (uint32_t r = 0; r < rows; r++)
                {
                    uint32_t src_row = steps * r;
                    uint32_t dst_row = columns * r;
                    for (uint32_t c = 0, dst_column = 0; c < columns; c++, dst_column += channels)
                        d(dst_row + c) = pallete[s[src_row + dst_column]];
                }
            }
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_matrix_by_pallete

#endif