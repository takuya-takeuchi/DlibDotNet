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

DLLEXPORT std::vector<dlib::rectangle>* create_grid_detection_template(dlib::rectangle* object_box,
                                                                       const unsigned int cells_x,
                                                                       const unsigned int cells_y)
{
    auto& ob = *object_box;
    const auto ret = dlib::create_grid_detection_template(ob, cells_x, cells_y);
    auto vector = new std::vector<dlib::rectangle>();
    for (auto i = 0; i < ret.size(); i++) vector->push_back(ret.at(i));

    return vector;
}

#endif