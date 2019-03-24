#ifndef _CPP_CORRELATION_TRACKER_H_
#define _CPP_CORRELATION_TRACKER_H_

#include "../export.h"
#include <dlib/image_processing.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT correlation_tracker* correlation_tracker_new(
    unsigned int filter_size,
    unsigned int num_scale_levels,
    unsigned int scale_window_size,
    double regularizer_space,
    double nu_space,
    double regularizer_scale,
    double nu_scale,
    double scale_pyramid_alpha)
{
    return new dlib::correlation_tracker(
        filter_size,
        num_scale_levels,
        scale_window_size,
        regularizer_space,
        nu_space,
        regularizer_scale,
        nu_scale,
        scale_pyramid_alpha);
}

DLLEXPORT int correlation_tracker_start_track(correlation_tracker* tracker, array2d_type img_type, void* img, dlib::drectangle* p)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            tracker->start_track(*((array2d<uint8_t>*)img), *p);
            break;
        case array2d_type::UInt16:
            tracker->start_track(*((array2d<uint16_t>*)img), *p);
            break;
        case array2d_type::UInt32:
            tracker->start_track(*((array2d<uint32_t>*)img), *p);
            break;
        case array2d_type::Int8:
            tracker->start_track(*((array2d<int8_t>*)img), *p);
            break;
        case array2d_type::Int16:
            tracker->start_track(*((array2d<int16_t>*)img), *p);
            break;
        case array2d_type::Int32:
            tracker->start_track(*((array2d<int32_t>*)img), *p);
            break;
        case array2d_type::Float:
            tracker->start_track(*((array2d<float>*)img), *p);
            break;
        case array2d_type::Double:
            tracker->start_track(*((array2d<double>*)img), *p);
            break;
        case array2d_type::RgbPixel:
            tracker->start_track(*((array2d<rgb_pixel>*)img), *p);
            break;
        case array2d_type::HsiPixel:
            tracker->start_track(*((array2d<hsi_pixel>*)img), *p);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT drectangle* correlation_tracker_get_position(correlation_tracker* tracker)
{
    dlib::drectangle rect = tracker->get_position();
    return new dlib::drectangle(rect);
}

DLLEXPORT int correlation_tracker_update_noscale(
    correlation_tracker* tracker,
    const array2d_type img_type,
    void* img,
    drectangle* guess,
    double* confident)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            *confident = tracker->update_noscale(*((array2d<uint8_t>*)img), *guess);
            break;
        case array2d_type::UInt16:
            *confident = tracker->update_noscale(*((array2d<uint16_t>*)img), *guess);
            break;
        case array2d_type::UInt32:
            *confident = tracker->update_noscale(*((array2d<uint32_t>*)img), *guess);
            break;
        case array2d_type::Int8:
            *confident = tracker->update_noscale(*((array2d<int8_t>*)img), *guess);
            break;
        case array2d_type::Int16:
            *confident = tracker->update_noscale(*((array2d<int16_t>*)img), *guess);
            break;
        case array2d_type::Int32:
            *confident = tracker->update_noscale(*((array2d<int32_t>*)img), *guess);
            break;
        case array2d_type::Float:
            *confident = tracker->update_noscale(*((array2d<float>*)img), *guess);
            break;
        case array2d_type::Double:
            *confident = tracker->update_noscale(*((array2d<double>*)img), *guess);
            break;
        case array2d_type::RgbPixel:
            *confident = tracker->update_noscale(*((array2d<rgb_pixel>*)img), *guess);
            break;
        case array2d_type::HsiPixel:
            *confident = tracker->update_noscale(*((array2d<hsi_pixel>*)img), *guess);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int correlation_tracker_update_noscale2(
    correlation_tracker* tracker,
    const array2d_type img_type,
    void* img,
    double* confident)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            *confident = tracker->update_noscale(*((array2d<uint8_t>*)img));
            break;
        case array2d_type::UInt16:
            *confident = tracker->update_noscale(*((array2d<uint16_t>*)img));
            break;
        case array2d_type::UInt32:
            *confident = tracker->update_noscale(*((array2d<uint32_t>*)img));
            break;
        case array2d_type::Int8:
            *confident = tracker->update_noscale(*((array2d<int8_t>*)img));
            break;
        case array2d_type::Int16:
            *confident = tracker->update_noscale(*((array2d<int16_t>*)img));
            break;
        case array2d_type::Int32:
            *confident = tracker->update_noscale(*((array2d<int32_t>*)img));
            break;
        case array2d_type::Float:
            *confident = tracker->update_noscale(*((array2d<float>*)img));
            break;
        case array2d_type::Double:
            *confident = tracker->update_noscale(*((array2d<double>*)img));
            break;
        case array2d_type::RgbPixel:
            *confident = tracker->update_noscale(*((array2d<rgb_pixel>*)img));
            break;
        case array2d_type::HsiPixel:
            *confident = tracker->update_noscale(*((array2d<hsi_pixel>*)img));
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int correlation_tracker_update(
    correlation_tracker* tracker,
    const array2d_type img_type,
    void* img,
    drectangle* guess,
    double* confident)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            *confident = tracker->update(*((array2d<uint8_t>*)img), *guess);
            break;
        case array2d_type::UInt16:
            *confident = tracker->update(*((array2d<uint16_t>*)img), *guess);
            break;
        case array2d_type::UInt32:
            *confident = tracker->update(*((array2d<uint32_t>*)img), *guess);
            break;
        case array2d_type::Int8:
            *confident = tracker->update(*((array2d<int8_t>*)img), *guess);
            break;
        case array2d_type::Int16:
            *confident = tracker->update(*((array2d<int16_t>*)img), *guess);
            break;
        case array2d_type::Int32:
            *confident = tracker->update(*((array2d<int32_t>*)img), *guess);
            break;
        case array2d_type::Float:
            *confident = tracker->update(*((array2d<float>*)img), *guess);
            break;
        case array2d_type::Double:
            *confident = tracker->update(*((array2d<double>*)img), *guess);
            break;
        case array2d_type::RgbPixel:
            *confident = tracker->update(*((array2d<rgb_pixel>*)img), *guess);
            break;
        case array2d_type::HsiPixel:
            *confident = tracker->update(*((array2d<hsi_pixel>*)img), *guess);
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int correlation_tracker_update2(
    correlation_tracker* tracker,
    const array2d_type img_type,
    void* img,
    double* confident)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            *confident = tracker->update(*((array2d<uint8_t>*)img));
            break;
        case array2d_type::UInt16:
            *confident = tracker->update(*((array2d<uint16_t>*)img));
            break;
        case array2d_type::UInt32:
            *confident = tracker->update(*((array2d<uint32_t>*)img));
            break;
        case array2d_type::Int8:
            *confident = tracker->update(*((array2d<int8_t>*)img));
            break;
        case array2d_type::Int16:
            *confident = tracker->update(*((array2d<int16_t>*)img));
            break;
        case array2d_type::Int32:
            *confident = tracker->update(*((array2d<int32_t>*)img));
            break;
        case array2d_type::Float:
            *confident = tracker->update(*((array2d<float>*)img));
            break;
        case array2d_type::Double:
            *confident = tracker->update(*((array2d<double>*)img));
            break;
        case array2d_type::RgbPixel:
            *confident = tracker->update(*((array2d<rgb_pixel>*)img));
            break;
        case array2d_type::HsiPixel:
            *confident = tracker->update(*((array2d<hsi_pixel>*)img));
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void correlation_tracker_delete(correlation_tracker* obj)
{
	delete obj;
}

#endif