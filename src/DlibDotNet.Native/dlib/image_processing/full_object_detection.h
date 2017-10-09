#ifndef _CPP_FULL_OBJECT_DETECTION_H_
#define _CPP_FULL_OBJECT_DETECTION_H_

#include "../export.h"
#include <dlib/image_processing/full_object_detection.h>
#include <dlib/geometry/rectangle.h>
 
using namespace dlib;
using namespace std;

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

#endif