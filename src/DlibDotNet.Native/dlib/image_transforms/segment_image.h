#ifndef _CPP_SEGMENT_IMAGE_H_
#define _CPP_SEGMENT_IMAGE_H_

#include "../export.h"
#include <dlib/image_transforms/segment_image.h>
#include <dlib/image_processing/generic_image.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

// NOTE
// segment_image function requires the following condition.
// - out_image_type must contain an unsigned integer pixel type.
// - is_same_object(in_img, out_img) == false
// Perhaps, find_candidate_object_locations requires same condition?
DLLEXPORT int find_candidate_object_locations(
    array2d_type type,
    void* in_img,
    std::vector<rectangle*> *rects,
    int kvals,
    const unsigned int min_size,
    const unsigned int max_merging_iterations)
{
    int err = ERR_OK;

    std::vector<rectangle> tmpRects;
    switch(type)
    {
        case array2d_type::UInt8:
            dlib::find_candidate_object_locations(*((array2d<uint8_t>*)in_img), tmpRects);
            break;
        case array2d_type::UInt16:
            dlib::find_candidate_object_locations(*((array2d<uint16_t>*)in_img), tmpRects);
            break;
        case array2d_type::Int32:
            dlib::find_candidate_object_locations(*((array2d<int32_t>*)in_img), tmpRects);
            break;
        case array2d_type::RgbPixel:
            dlib::find_candidate_object_locations(*((array2d<rgb_pixel>*)in_img), tmpRects);
            break;
        case array2d_type::RgbAlphaPixel:
            dlib::find_candidate_object_locations(*((array2d<rgb_alpha_pixel>*)in_img), tmpRects);
            break;
        case array2d_type::Float:
        case array2d_type::Double:
        case array2d_type::HsiPixel:
            break;
        default:
            err = ERR_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    if(tmpRects.size() > 0)
    {
        for (int i = 0; i < tmpRects.size(); i++)
            rects->push_back(new rectangle(tmpRects[i]));
    }
    
    return err;
}

#endif