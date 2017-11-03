#ifndef _CPP_FULL_OBJECT_DETECTION_H_
#define _CPP_FULL_OBJECT_DETECTION_H_

#include "../export.h"
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/geometry/rectangle.h>
 
using namespace dlib;
using namespace std;

#pragma region full_object_detection

DLLEXPORT full_object_detection* full_object_detection_new()
{
    return new full_object_detection();
}

DLLEXPORT rectangle* full_object_detection_get_rect(full_object_detection* detection)
{
    const rectangle rect = detection->get_rect();
    return new rectangle(rect);
}

DLLEXPORT unsigned int full_object_detection_num_parts(full_object_detection* detection)
{
    return detection->num_parts();
}

DLLEXPORT point* full_object_detection_part(full_object_detection* detection, unsigned int idx)
{
    point p = detection->part(idx);
    return new point(p);
}

DLLEXPORT void full_object_detection_delete(full_object_detection* obj)
{
	delete obj;
}

#pragma endregion full_object_detection

#pragma region mmod_rect

DLLEXPORT mmod_rect* mmod_rect_new()
{
    return new mmod_rect();
}

DLLEXPORT double mmod_rect_get_detection_confidence(mmod_rect* mmod, double* confidence)
{
    *confidence = mmod->detection_confidence;
    return true;
}

DLLEXPORT bool mmod_rect_get_ignore(mmod_rect* mmod, bool* ignore)
{
    *ignore = mmod->ignore;
    return true;
}

DLLEXPORT bool mmod_rect_get_label(mmod_rect* mmod, std::string** str)
{
    *str = new std::string(mmod->label);
    return true;
}

DLLEXPORT bool mmod_rect_get_rect(mmod_rect* mmod, rectangle** rect)
{
    *rect = new rectangle(mmod->rect);
    return true;
}

DLLEXPORT void mmod_rect_set_detection_confidence(mmod_rect* mmod, double confidence)
{
    mmod->detection_confidence = confidence;
}

DLLEXPORT void mmod_rect_set_ignore(mmod_rect* mmod, bool ignore)
{
    mmod->ignore = ignore;
}

DLLEXPORT void mmod_rect_set_label(mmod_rect* mmod, std::string* label)
{
    mmod->label = std::string(*label);
}

DLLEXPORT void mmod_rect_set_rect(mmod_rect* mmod, rectangle* rect)
{
    mmod->rect = rectangle(*rect);
}

DLLEXPORT void mmod_rect_delete(mmod_rect* obj)
{
	delete obj;
}

#pragma endregion mmod_rect

#endif