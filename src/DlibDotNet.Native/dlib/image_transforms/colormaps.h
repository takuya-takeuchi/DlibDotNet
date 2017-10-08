#ifndef _CPP_COLORMAPS_H_
#define _CPP_COLORMAPS_H_

#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region op_heatmap

extern "C" __declspec(dllexport) int op_heatmap_apply(array2d_type type, void* obj, int r, int c, rgb_pixel* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_heatmap<array2d<uint8_t>>*)obj)->apply(r, c);
			break;
        case array2d_type::UInt16:
			*ret = ((op_heatmap<array2d<uint16_t>>*)obj)->apply(r, c);
			break;
        case array2d_type::Float:
			*ret = ((op_heatmap<array2d<float>>*)obj)->apply(r, c);
			break;
        case array2d_type::Double:
			*ret = ((op_heatmap<array2d<double>>*)obj)->apply(r, c);
			break;
        case array2d_type::RgbPixel:
			*ret = ((op_heatmap<array2d<rgb_pixel>>*)obj)->apply(r, c);
			break;
        case array2d_type::HsiPixel:
			*ret = ((op_heatmap<array2d<hsi_pixel>>*)obj)->apply(r, c);
			break;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_heatmap<array2d<rgb_alpha_pixel>>*)obj)->apply(r, c);
			break;
        default:
			return ERR_ARRAY_TYPE_NOT_SUPPORT;
    }
    
    return err;
}

extern "C" __declspec(dllexport) bool op_heatmap_max_val(array2d_type type, void* obj, double* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_heatmap<array2d<uint8_t>>*)obj)->max_val;
			return true;
        case array2d_type::UInt16:
			*ret = ((op_heatmap<array2d<uint16_t>>*)obj)->max_val;
			return true;
        case array2d_type::Float:
			*ret = ((op_heatmap<array2d<float>>*)obj)->max_val;
			return true;
        case array2d_type::Double:
			*ret = ((op_heatmap<array2d<double>>*)obj)->max_val;
			return true;
        case array2d_type::RgbPixel:
			*ret = ((op_heatmap<array2d<rgb_pixel>>*)obj)->max_val;
			return true;
        case array2d_type::HsiPixel:
			*ret = ((op_heatmap<array2d<hsi_pixel>>*)obj)->max_val;
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_heatmap<array2d<rgb_alpha_pixel>>*)obj)->max_val;
			return true;
        default:
			return false;
    }
}

extern "C" __declspec(dllexport) bool op_heatmap_min_val(array2d_type type, void* obj, double* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_heatmap<array2d<uint8_t>>*)obj)->min_val;
			return true;
        case array2d_type::UInt16:
			*ret = ((op_heatmap<array2d<uint16_t>>*)obj)->min_val;
			return true;
        case array2d_type::Float:
			*ret = ((op_heatmap<array2d<float>>*)obj)->min_val;
			return true;
        case array2d_type::Double:
			*ret = ((op_heatmap<array2d<double>>*)obj)->min_val;
			return true;
        case array2d_type::RgbPixel:
			*ret = ((op_heatmap<array2d<rgb_pixel>>*)obj)->min_val;
			return true;
        case array2d_type::HsiPixel:
			*ret = ((op_heatmap<array2d<hsi_pixel>>*)obj)->min_val;
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_heatmap<array2d<rgb_alpha_pixel>>*)obj)->min_val;
			return true;
        default:
			return false;
    }
}

extern "C" __declspec(dllexport) bool op_heatmap_nc(array2d_type type, void* obj, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_heatmap<array2d<uint8_t>>*)obj)->nc();
			return true;
        case array2d_type::UInt16:
			*ret = ((op_heatmap<array2d<uint16_t>>*)obj)->nc();
			return true;
        case array2d_type::Float:
			*ret = ((op_heatmap<array2d<float>>*)obj)->nc();
			return true;
        case array2d_type::Double:
			*ret = ((op_heatmap<array2d<double>>*)obj)->nc();
			return true;
        case array2d_type::RgbPixel:
            *ret = ((op_heatmap<array2d<rgb_pixel>>*)obj)->nc();
            return true;
        case array2d_type::HsiPixel:
            *ret = ((op_heatmap<array2d<hsi_pixel>>*)obj)->nc();
            return true;
        case array2d_type::RgbAlphaPixel:
            *ret = ((op_heatmap<array2d<rgb_alpha_pixel>>*)obj)->nc();
            return true;
        default:
            return false;
    }
}

extern "C" __declspec(dllexport) bool op_heatmap_nr(array2d_type type, void* obj, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_heatmap<array2d<uint8_t>>*)obj)->nr();
			return true;
        case array2d_type::UInt16:
			*ret = ((op_heatmap<array2d<uint16_t>>*)obj)->nr();
			return true;
        case array2d_type::Float:
			*ret = ((op_heatmap<array2d<float>>*)obj)->nr();
			return true;
        case array2d_type::Double:
			*ret = ((op_heatmap<array2d<double>>*)obj)->nr();
			return true;
        case array2d_type::RgbPixel:
            *ret = ((op_heatmap<array2d<rgb_pixel>>*)obj)->nr();
            return true;
        case array2d_type::HsiPixel:
            *ret = ((op_heatmap<array2d<hsi_pixel>>*)obj)->nr();
            return true;
        case array2d_type::RgbAlphaPixel:
            *ret = ((op_heatmap<array2d<rgb_alpha_pixel>>*)obj)->nr();
            return true;
        default:
            return false;
    }
}

#pragma endregion op_heatmap

#pragma region op_jet

extern "C" __declspec(dllexport) int op_jet_apply(array2d_type type, void* obj, int r, int c, rgb_pixel* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_jet<array2d<uint8_t>>*)obj)->apply(r, c);
			break;
        case array2d_type::UInt16:
			*ret = ((op_jet<array2d<uint16_t>>*)obj)->apply(r, c);
			break;
        case array2d_type::Float:
			*ret = ((op_jet<array2d<float>>*)obj)->apply(r, c);
			break;
        case array2d_type::Double:
			*ret = ((op_jet<array2d<double>>*)obj)->apply(r, c);
			break;
        case array2d_type::RgbPixel:
			*ret = ((op_jet<array2d<rgb_pixel>>*)obj)->apply(r, c);
			break;
        case array2d_type::HsiPixel:
			*ret = ((op_jet<array2d<hsi_pixel>>*)obj)->apply(r, c);
			break;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_jet<array2d<rgb_alpha_pixel>>*)obj)->apply(r, c);
			break;
        default:
			return ERR_ARRAY_TYPE_NOT_SUPPORT;
    }
    
    return err;
}

extern "C" __declspec(dllexport) bool op_jet_max_val(array2d_type type, void* obj, double* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_jet<array2d<uint8_t>>*)obj)->max_val;
			return true;
        case array2d_type::UInt16:
			*ret = ((op_jet<array2d<uint16_t>>*)obj)->max_val;
			return true;
        case array2d_type::Float:
			*ret = ((op_jet<array2d<float>>*)obj)->max_val;
			return true;
        case array2d_type::Double:
			*ret = ((op_jet<array2d<double>>*)obj)->max_val;
			return true;
        case array2d_type::RgbPixel:
			*ret = ((op_jet<array2d<rgb_pixel>>*)obj)->max_val;
			return true;
        case array2d_type::HsiPixel:
			*ret = ((op_jet<array2d<hsi_pixel>>*)obj)->max_val;
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_jet<array2d<rgb_alpha_pixel>>*)obj)->max_val;
			return true;
        default:
			return false;
    }
}

extern "C" __declspec(dllexport) bool op_jet_min_val(array2d_type type, void* obj, double* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_jet<array2d<uint8_t>>*)obj)->min_val;
			return true;
        case array2d_type::UInt16:
			*ret = ((op_jet<array2d<uint16_t>>*)obj)->min_val;
			return true;
        case array2d_type::Float:
			*ret = ((op_jet<array2d<float>>*)obj)->min_val;
			return true;
        case array2d_type::Double:
			*ret = ((op_jet<array2d<double>>*)obj)->min_val;
			return true;
        case array2d_type::RgbPixel:
			*ret = ((op_jet<array2d<rgb_pixel>>*)obj)->min_val;
			return true;
        case array2d_type::HsiPixel:
			*ret = ((op_jet<array2d<hsi_pixel>>*)obj)->min_val;
			return true;
        case array2d_type::RgbAlphaPixel:
			*ret = ((op_jet<array2d<rgb_alpha_pixel>>*)obj)->min_val;
			return true;
        default:
			return false;
    }
}

extern "C" __declspec(dllexport) bool op_jet_nc(array2d_type type, void* obj, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_jet<array2d<uint8_t>>*)obj)->nc();
			return true;
        case array2d_type::UInt16:
			*ret = ((op_jet<array2d<uint16_t>>*)obj)->nc();
			return true;
        case array2d_type::Float:
			*ret = ((op_jet<array2d<float>>*)obj)->nc();
			return true;
        case array2d_type::Double:
			*ret = ((op_jet<array2d<double>>*)obj)->nc();
			return true;
        case array2d_type::RgbPixel:
            *ret = ((op_jet<array2d<rgb_pixel>>*)obj)->nc();
            return true;
        case array2d_type::HsiPixel:
            *ret = ((op_jet<array2d<hsi_pixel>>*)obj)->nc();
            return true;
        case array2d_type::RgbAlphaPixel:
            *ret = ((op_jet<array2d<rgb_alpha_pixel>>*)obj)->nc();
            return true;
        default:
            return false;
    }
}

extern "C" __declspec(dllexport) bool op_jet_nr(array2d_type type, void* obj, int* ret)
{
    switch(type)
    {
        case array2d_type::UInt8:
			*ret = ((op_jet<array2d<uint8_t>>*)obj)->nr();
			return true;
        case array2d_type::UInt16:
			*ret = ((op_jet<array2d<uint16_t>>*)obj)->nr();
			return true;
        case array2d_type::Float:
			*ret = ((op_jet<array2d<float>>*)obj)->nr();
			return true;
        case array2d_type::Double:
			*ret = ((op_jet<array2d<double>>*)obj)->nr();
			return true;
        case array2d_type::RgbPixel:
            *ret = ((op_jet<array2d<rgb_pixel>>*)obj)->nr();
            return true;
        case array2d_type::HsiPixel:
            *ret = ((op_jet<array2d<hsi_pixel>>*)obj)->nr();
            return true;
        case array2d_type::RgbAlphaPixel:
            *ret = ((op_jet<array2d<rgb_alpha_pixel>>*)obj)->nr();
            return true;
        default:
            return false;
    }
}

#pragma endregion op_jet

#pragma region heatmap

extern "C" __declspec(dllexport) int heatmap(array2d_type type, void* img, void** matrix)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto ret = dlib::heatmap(*((array2d<uint8_t>*)img));
                *matrix = new matrix_op<op_heatmap<array2d<uint8_t>>>(ret);
            }
            break;
        case array2d_type::UInt16:
            {
                auto ret = dlib::heatmap(*((array2d<uint16_t>*)img));
                *matrix = new matrix_op<op_heatmap<array2d<uint16_t>>>(ret);
            }
            break;
        case array2d_type::Float:
            {
                auto ret = dlib::heatmap(*((array2d<float>*)img));
                *matrix = new matrix_op<op_heatmap<array2d<float>>>(ret);
            }
            break;
        case array2d_type::Double:
            {
                auto ret = dlib::heatmap(*((array2d<double>*)img));
                *matrix = new matrix_op<op_heatmap<array2d<double>>>(ret);
            }
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

extern "C" __declspec(dllexport) int heatmap2(array2d_type type, void* img, double max_val, double min_val, void** matrix)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto ret = dlib::heatmap(*((array2d<uint8_t>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<uint8_t>>>(ret);
            }
            break;
        case array2d_type::UInt16:
            {
                auto ret = dlib::heatmap(*((array2d<uint16_t>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<uint16_t>>>(ret);
            }
            break;
        case array2d_type::Float:
            {
                auto ret = dlib::heatmap(*((array2d<float>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<float>>>(ret);
            }
            break;
        case array2d_type::Double:
            {
                auto ret = dlib::heatmap(*((array2d<double>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<double>>>(ret);
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto ret = dlib::heatmap(*((array2d<rgb_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<rgb_pixel>>>(ret);
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto ret = dlib::heatmap(*((array2d<hsi_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<hsi_pixel>>>(ret);
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto ret = dlib::heatmap(*((array2d<rgb_alpha_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_heatmap<array2d<rgb_alpha_pixel>>>(ret);
            }
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion heatmap

#pragma region jet

extern "C" __declspec(dllexport) int jet(array2d_type type, void* img, void** matrix)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto ret = dlib::jet(*((array2d<uint8_t>*)img));
                *matrix = new matrix_op<op_jet<array2d<uint8_t>>>(ret);
            }
            break;
        case array2d_type::UInt16:
            {
                auto ret = dlib::jet(*((array2d<uint16_t>*)img));
                *matrix = new matrix_op<op_jet<array2d<uint16_t>>>(ret);
            }
            break;
        case array2d_type::Float:
            {
                auto ret = dlib::jet(*((array2d<float>*)img));
                *matrix = new matrix_op<op_jet<array2d<float>>>(ret);
            }
            break;
        case array2d_type::Double:
            {
                auto ret = dlib::jet(*((array2d<double>*)img));
                *matrix = new matrix_op<op_jet<array2d<double>>>(ret);
            }
            break;
        case array2d_type::RgbPixel:
        case array2d_type::HsiPixel:
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

extern "C" __declspec(dllexport) int jet2(array2d_type type, void* img, double max_val, double min_val, void** matrix)
{
    int err = ERR_OK;
    switch(type)
    {
        case array2d_type::UInt8:
            {
                auto ret = dlib::jet(*((array2d<uint8_t>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<uint8_t>>>(ret);
            }
            break;
        case array2d_type::UInt16:
            {
                auto ret = dlib::jet(*((array2d<uint16_t>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<uint16_t>>>(ret);
            }
            break;
        case array2d_type::Float:
            {
                auto ret = dlib::jet(*((array2d<float>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<float>>>(ret);
            }
            break;
        case array2d_type::Double:
            {
                auto ret = dlib::jet(*((array2d<double>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<double>>>(ret);
            }
            break;
        case array2d_type::RgbPixel:
            {
                auto ret = dlib::jet(*((array2d<rgb_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<rgb_pixel>>>(ret);
            }
            break;
        case array2d_type::HsiPixel:
            {
                auto ret = dlib::jet(*((array2d<hsi_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<hsi_pixel>>>(ret);
            }
            break;
        case array2d_type::RgbAlphaPixel:
            {
                auto ret = dlib::jet(*((array2d<rgb_alpha_pixel>*)img), max_val, min_val);
                *matrix = new matrix_op<op_jet<array2d<rgb_alpha_pixel>>>(ret);
            }
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#pragma endregion jet

#endif