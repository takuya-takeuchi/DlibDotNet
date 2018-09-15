#ifndef _CPP_OBJECT_DETECTOR_H_
#define _CPP_OBJECT_DETECTOR_H_

#include "../export.h"
#include <dlib/image_processing/object_detector.h>
#include <dlib/geometry/rectangle.h>
 
using namespace dlib;
using namespace std;

#pragma region rect_detection

DLLEXPORT rectangle* rect_detection_get_rect(rect_detection* detection)
{
    const rectangle rect = detection->rect;
    return new rectangle(rect);
}

DLLEXPORT double rect_detection_get_detection_confidence(rect_detection* detection)
{
    return detection->detection_confidence;
}

DLLEXPORT unsigned long rect_detection_get_weight_index(rect_detection* detection)
{
    return detection->weight_index;
}

DLLEXPORT void rect_detection_delete(rect_detection* obj)
{
	delete obj;
}

#pragma endregion rect_detection

#pragma region full_detection

DLLEXPORT full_object_detection* full_detection_get_rect(full_detection* detection)
{
    const full_object_detection rect = detection->rect;
    return new full_object_detection(rect);
}

DLLEXPORT double full_detection_get_detection_confidence(full_detection* detection)
{
    return detection->detection_confidence;
}

DLLEXPORT unsigned long full_detection_get_weight_index(full_detection* detection)
{
    return detection->weight_index;
}

DLLEXPORT void full_detection_delete(full_detection* obj)
{
	delete obj;
}

#pragma endregion full_detection

#endif