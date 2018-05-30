#ifndef _CPP_FRONTAL_FACE_DETECTOR_H_
#define _CPP_FRONTAL_FACE_DETECTOR_H_

#include "../export.h"
#include <dlib/image_processing/frontal_face_detector.h>
#include <dlib/image_io.h>
#include <dlib/pixel.h>
#include <iostream>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

DLLEXPORT frontal_face_detector* get_frontal_face_detector()
{
    frontal_face_detector ret = dlib::get_frontal_face_detector();
    return new dlib::frontal_face_detector(ret);
}

DLLEXPORT int frontal_face_detector_operator(
    frontal_face_detector* detector,
    array2d_type img_type,
    void* img,
    double adjust_threshold,
    std::vector<rectangle*> *dets)
{
    int err = ERR_OK;

    std::vector<rectangle>* ret = nullptr;
    switch(img_type)
    {
        case array2d_type::UInt8:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<uint8_t>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::UInt16:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<uint16_t>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::Int32:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<int32_t>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::Float:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<float>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::Double:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<double>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::RgbPixel:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<rgb_pixel>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::HsiPixel:
            {
                std::vector<rectangle> result = ((*detector)(*((array2d<hsi_pixel>*)img), adjust_threshold));
                for(int index = 0; index < result.size(); index++)
                    dets->push_back(new rectangle(result[index]));
            }
            break;
        case array2d_type::RgbAlphaPixel:
        default:
            err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void frontal_face_detector_delete(frontal_face_detector* obj)
{
	delete obj;
}

#endif