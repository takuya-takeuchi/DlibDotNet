#ifndef _CPP_EXTENSIONS_H_
#define _CPP_EXTENSIONS_H_

#include "../export.h"
#include <dlib/array2d.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define extensions_matrix_to_array_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, src, dst) \
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& s = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(src));\
__TYPE__* d = static_cast<__TYPE__*>(dst);\
const uint32_t rows = s.nr();\
const uint32_t columns = s.nc();\
for (uint32_t r = 0; r < rows; r++)\
for (uint32_t c = 0, step = r * columns; c < columns; c++)\
    d[step + c] = s(r, c);\

#define extensions_matrix_to_array_template(__TYPE__, __ROWS__, __COLUMNS__, error, src, dst) \
do {\
    matrix_template_size_arg2_template(__TYPE__, __ROWS__, __COLUMNS__, extensions_matrix_to_array_template_sub, error, src, dst);\
} while (0)

#define extensions_convert_array_to_bytes_template(__TYPE__, src, dst, rows, columns) \
do {\
    dlib::array2d<__TYPE__>& s = *(static_cast<dlib::array2d<__TYPE__>*>(src));\
    auto d = static_cast<uint8_t*>(dst);\
    for (uint32_t r = 0; r < rows; r++)\
    for (uint32_t c = 0, dst_row = (columns * sizeof(__TYPE__)) * r, dst_column = 0; c < columns; c++, dst_column += sizeof(__TYPE__))\
        memcpy(&d[dst_row + dst_column], &(s[r][c]), sizeof(__TYPE__));\
} while (0)

#pragma endregion template

#pragma region extensions_load_image_data

#define extensions_load_image_data_from_to_sametype(__TYPE__, data, rows, columns, steps) \
do { \
    dlib::array2d<__TYPE__>* ret = new dlib::array2d<__TYPE__>(rows, columns);\
    dlib::array2d<__TYPE__>& dst = *(ret);\
    __TYPE__* src = static_cast<__TYPE__*>(data);\
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
                        extensions_load_image_data_from_to_sametype(uint8_t, data, rows, columns, steps);
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(uint16_t, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(int16_t, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(int32_t, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(float, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(double, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(rgb_pixel, data, rows, columns, steps);
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
                                    drow[c].red   = src[src_row + dst_column + 0];
                                    drow[c].green = src[src_row + dst_column + 1];
                                    drow[c].blue  = src[src_row + dst_column + 2];
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
                    case array2d_type::BgrPixel:
                    case array2d_type::HsiPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        case array2d_type::BgrPixel:
            {
                switch(src_type)
                {
                    // from bgr_pixel to bgr_pixel
                    case array2d_type::BgrPixel:
                        extensions_load_image_data_from_to_sametype(bgr_pixel, data, rows, columns, steps);
                    case array2d_type::UInt8:
                        {
                            dlib::array2d<bgr_pixel>* ret = new dlib::array2d<bgr_pixel>(rows, columns);
                            dlib::array2d<bgr_pixel>& dst = *(ret);
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
                    case array2d_type::RgbPixel:
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
                        extensions_load_image_data_from_to_sametype(rgb_alpha_pixel, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
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
                        extensions_load_image_data_from_to_sametype(hsi_pixel, data, rows, columns, steps);
                    case array2d_type::UInt8:
                    case array2d_type::UInt16:
                    case array2d_type::Int16:
                    case array2d_type::Int32:
                    case array2d_type::Float:
                    case array2d_type::Double:
                    case array2d_type::RgbPixel:
                    case array2d_type::BgrPixel:
                    case array2d_type::RgbAlphaPixel:
                    default:
                        return nullptr;
                }
            }
        default:
			return nullptr;
    }
}

DLLEXPORT void* extensions_load_image_data2(array2d_type dst_type, array2d_type src_type, image_pixel_format_type pixel_type, void* data, uint32_t rows, uint32_t columns, uint32_t steps)
{
    switch(src_type)
    {
        case array2d_type::UInt8:
            {
                switch(dst_type)
                {
                    case array2d_type::RgbPixel:
                        {
                            switch(pixel_type)
                            {
                                case image_pixel_format_type::Bgr:
                                    {
                                        const int channels = 3;
                                        auto ret = new dlib::array2d<rgb_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 0];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 2];
                                            dst[r][c] = rgb_pixel(red, green, blue);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Bgra:
                                    {
                                        const int channels = 4;
                                        auto ret = new dlib::array2d<rgb_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 0];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 2];
                                            dst[r][c] = rgb_pixel(red, green, blue);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Rgb:
                                    {
                                        const int channels = 3;
                                        auto ret = new dlib::array2d<rgb_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 2];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 0];
                                            dst[r][c] = rgb_pixel(red, green, blue);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Rgba:
                                    {
                                        const int channels = 4;
                                        auto ret = new dlib::array2d<rgb_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 2];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 0];
                                            dst[r][c] = rgb_pixel(red, green, blue);
                                        }
                                        return ret;
                                    }
                                    break;
                                default:
                                    return nullptr;
                            }
                        }
                        break;
                    case array2d_type::RgbAlphaPixel:
                        {
                            switch(pixel_type)
                            {
                                case image_pixel_format_type::Bgr:
                                    {
                                        const int channels = 3;
                                        auto ret = new dlib::array2d<rgb_alpha_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 0];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 2];
                                            dst[r][c] = rgb_alpha_pixel(red, green, blue, 255);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Bgra:
                                    {
                                        const int channels = 4;
                                        auto ret = new dlib::array2d<rgb_alpha_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 0];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 2];
                                            auto alpha = src[steps * r + c * channels + 3];
                                            dst[r][c] = rgb_alpha_pixel(red, green, blue, alpha);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Rgb:
                                    {
                                        const int channels = 3;
                                        auto ret = new dlib::array2d<rgb_alpha_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 2];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 0];
                                            dst[r][c] = rgb_alpha_pixel(red, green, blue, 255);
                                        }
                                        return ret;
                                    }
                                    break;
                                case image_pixel_format_type::Rgba:
                                    {
                                        const int channels = 4;
                                        auto ret = new dlib::array2d<rgb_alpha_pixel>(rows, columns);
                                        auto& dst = *(ret);
                                        auto src = static_cast<uint8_t*>(data);
                                        for (uint32_t r = 0; r < rows; r++)
                                        for (uint32_t c = 0; c < columns; c++)
                                        {
                                            auto blue  = src[steps * r + c * channels + 2];
                                            auto green = src[steps * r + c * channels + 1];
                                            auto red   = src[steps * r + c * channels + 0];
                                            auto alpha = src[steps * r + c * channels + 3];
                                            dst[r][c] = rgb_alpha_pixel(red, green, blue, alpha);
                                        }
                                        return ret;
                                    }
                                    break;
                                default:
                                    return nullptr;
                            }
                        }
                        break;
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

#pragma region extensions_convert_managed_image_to_array_by_palette

DLLEXPORT void extensions_convert_managed_image_to_array_by_palette(void* src, array2d_type dst_type, void* dst, const dlib::rgb_pixel* palette, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
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
                        row[c] = palette[s[src_row + dst_column]];
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
                        row[c] = palette[s[src_row + dst_column]].red;
                }
            }
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_array_by_palette

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

#pragma region extensions_convert_managed_image_to_matrix_by_palette

DLLEXPORT void extensions_convert_managed_image_to_matrix_by_palette(void* src, matrix_element_type dst_type, void* dst, const dlib::rgb_pixel* palette, uint32_t rows, uint32_t columns, uint32_t steps, uint32_t channels)
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
                        d(dst_row + c) = palette[s[src_row + dst_column]];
                }
            }
            break;
        default:
            break;
    }
}

#pragma endregion extensions_convert_managed_image_to_matrix_by_palette

#pragma region extensions_to_array

DLLEXPORT int extensions_matrix_to_array(void* src, matrix_element_type type, const int templateRows, const int templateColumns, void* dst)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            extensions_matrix_to_array_template(uint8_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::UInt16:
            extensions_matrix_to_array_template(uint16_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::UInt32:
            extensions_matrix_to_array_template(uint32_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::Int8:
            extensions_matrix_to_array_template(int8_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::Int16:
            extensions_matrix_to_array_template(int16_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::Int32:
            extensions_matrix_to_array_template(int32_t, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::Float:
            extensions_matrix_to_array_template(float, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::Double:
            extensions_matrix_to_array_template(double, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::RgbPixel:
            extensions_matrix_to_array_template(rgb_pixel, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::BgrPixel:
            extensions_matrix_to_array_template(bgr_pixel, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::HsiPixel:
            extensions_matrix_to_array_template(hsi_pixel, templateRows, templateColumns, err, src, dst);
            break;
        case matrix_element_type::RgbAlphaPixel:
            extensions_matrix_to_array_template(rgb_alpha_pixel, templateRows, templateColumns, err, src, dst);
            break;
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion extensions_to_array

#pragma region extensions_convert_array_to_bytes

DLLEXPORT int extensions_convert_array_to_bytes(array2d_type src_type, void* src, void* dst, uint32_t rows, uint32_t columns)
{
    int err = ERR_OK;

    switch(src_type)
    {
        case array2d_type::UInt8:
            extensions_convert_array_to_bytes_template(uint8_t, src, dst, rows, columns);
            break;
        case array2d_type::Int8:
            extensions_convert_array_to_bytes_template(int8_t, src, dst, rows, columns);
            break;
        case array2d_type::UInt16:
            extensions_convert_array_to_bytes_template(uint16_t, src, dst, rows, columns);
            break;
        case array2d_type::Int16:
            extensions_convert_array_to_bytes_template(int16_t, src, dst, rows, columns);
            break;
        case array2d_type::UInt32:
            extensions_convert_array_to_bytes_template(uint32_t, src, dst, rows, columns);
            break;
        case array2d_type::Int32:
            extensions_convert_array_to_bytes_template(int32_t, src, dst, rows, columns);
            break;
        case array2d_type::Float:
            extensions_convert_array_to_bytes_template(float, src, dst, rows, columns);
            break;
        case array2d_type::Double:
            extensions_convert_array_to_bytes_template(double, src, dst, rows, columns);
            break;
        case array2d_type::RgbPixel:
            extensions_convert_array_to_bytes_template(dlib::rgb_pixel, src, dst, rows, columns);
            break;
        case array2d_type::BgrPixel:
            extensions_convert_array_to_bytes_template(dlib::bgr_pixel, src, dst, rows, columns);
            break;
        case array2d_type::RgbAlphaPixel:
            extensions_convert_array_to_bytes_template(dlib::rgb_alpha_pixel, src, dst, rows, columns);
            break;
        case array2d_type::HsiPixel:
            extensions_convert_array_to_bytes_template(dlib::hsi_pixel, src, dst, rows, columns);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion extensions_convert_array_to_bytes

#endif