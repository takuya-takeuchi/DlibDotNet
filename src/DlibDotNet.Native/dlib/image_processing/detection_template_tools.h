#ifndef _CPP_DETECTION_TEMPLATE_TOOLS_H_
#define _CPP_DETECTION_TEMPLATE_TOOLS_H_

#include "../export.h"
#include <dlib/image_processing/detection_template_tools.h>
#include <dlib/geometry/rectangle.h>

using namespace dlib;
using namespace std;

DLLEXPORT dlib::rectangle* compute_box_dimensions(const double width_to_height_ratio,
                                                  const double area)
{
    const auto ret = dlib::compute_box_dimensions(width_to_height_ratio, area);
    return new dlib::rectangle(ret);
}

#endif