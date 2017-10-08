#ifndef _CPP_RENDER_FACE_DETECTIONS_H_
#define _CPP_RENDER_FACE_DETECTIONS_H_

#include <dlib/image_processing/render_face_detections.h>
#include <dlib/gui_widgets.h>
#include <dlib/image_io.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

extern "C" _declspec(dllexport) void render_face_detections(const std::vector<full_object_detection*>* dets, rgb_pixel* p, std::vector<image_window::overlay_line*>* rets)
{
    std::vector<full_object_detection> tmpDets;
    for (int index = 0 ; index < dets->size(); index++)
        tmpDets.push_back(*(*dets)[index]);

    std::vector<image_window::overlay_line> ret = dlib::render_face_detections(tmpDets, *p);

    for (int index = 0 ; index < ret.size(); index++)
    {
        image_window::overlay_line* line = new image_window::overlay_line();
        memcpy(line, &ret[index], sizeof(image_window::overlay_line));
        rets->push_back(line);
    }
}

extern "C" _declspec(dllexport) void render_face_detections2(full_object_detection* det, rgb_pixel* p, std::vector<image_window::overlay_line*>* rets)
{
    std::vector<image_window::overlay_line> ret = dlib::render_face_detections(*det, *p);
    
    for (int index = 0 ; index < ret.size(); index++)
    {
        image_window::overlay_line* line = new image_window::overlay_line();
        memcpy(line, &ret[index], sizeof(image_window::overlay_line));
        rets->push_back(line);
    }
}

#endif