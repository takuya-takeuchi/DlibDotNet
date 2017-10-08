#ifndef _CPP_FULL_OBJECT_DETECTION_H_
#define _CPP_FULL_OBJECT_DETECTION_H_

#include <dlib/image_processing/full_object_detection.h>
#include <dlib/geometry/rectangle.h>
 
using namespace dlib;
using namespace std;

extern "C" __declspec(dllexport) full_object_detection* full_object_detection_new()
{
    return new full_object_detection();
}

extern "C" __declspec(dllexport) rectangle* full_object_detection_get_rect(full_object_detection* detection)
{
    const rectangle rect = detection->get_rect();
    return new rectangle(rect);
}

extern "C" __declspec(dllexport) unsigned int full_object_detection_num_parts(full_object_detection* detection)
{
    return detection->num_parts();
}

extern "C" __declspec(dllexport) point* full_object_detection_part(full_object_detection* detection, unsigned int idx)
{
    point p = detection->part(idx);
    return new point(p);
}

extern "C" _declspec(dllexport) void full_object_detection_delete(void* obj)
{
	delete obj;
}

#endif