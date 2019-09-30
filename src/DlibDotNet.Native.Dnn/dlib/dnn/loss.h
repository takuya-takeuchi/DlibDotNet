#ifndef _CPP_LOSS_H_
#define _CPP_LOSS_H_

#include <dlib/dnn.h>
#include <vector>

#include "../common.h"

using namespace dlib;
using namespace std;

#pragma region mmod_options

DLLEXPORT mmod_options* mmod_options_new(const std::vector<std::vector<mmod_rect*>*>* boxes,
                                         const unsigned long target_size,
                                         const unsigned long min_target_size,
                                         const double min_detector_window_overlap_iou = 0.75)
{
    const std::vector<std::vector<mmod_rect*>*>& tmp_boxes = *boxes;
    std::vector<std::vector<mmod_rect>> input;
    for (size_t i = 0; i < tmp_boxes.size(); i++)
    {
        std::vector<mmod_rect> v;
        for (size_t j = 0; j < tmp_boxes[i]->size(); j++)
            v.push_back(*tmp_boxes[i]->at(j));
        input.push_back(v);
    }

    return new mmod_options(input, target_size, min_target_size, min_detector_window_overlap_iou);
}

DLLEXPORT void mmod_options_delete(mmod_options* options)
{
    delete options;
}

DLLEXPORT std::vector<mmod_options::detector_window_details*>* mmod_options_get_detector_windows(mmod_options* options)
{
    auto result = new std::vector<mmod_options::detector_window_details*>();
    const auto tmp = options->detector_windows;
    for (size_t i = 0; i < tmp.size(); i++)
        result->push_back(new mmod_options::detector_window_details(tmp.at(i)));

    return result;
}

DLLEXPORT void mmod_options_set_detector_windows(mmod_options* options, std::vector<mmod_options::detector_window_details*>* value)
{
    std::vector<mmod_options::detector_window_details> tmp;
    for (size_t i = 0; i < value->size(); i++)
        tmp.push_back(*value->at(i));

    options->detector_windows = tmp;
}

DLLEXPORT double mmod_options_get_loss_per_false_alarm(mmod_options* options)
{
    return options->loss_per_false_alarm;
}

DLLEXPORT void mmod_options_set_loss_per_false_alarm(mmod_options* options, double value)
{
    options->loss_per_false_alarm = value;
}

DLLEXPORT double mmod_options_get_loss_per_missed_target(mmod_options* options)
{
    return options->loss_per_missed_target;
}

DLLEXPORT void mmod_options_set_loss_per_missed_target(mmod_options* options, double value)
{
    options->loss_per_missed_target = value;
}

DLLEXPORT double mmod_options_get_truth_match_iou_threshold(mmod_options* options)
{
    return options->truth_match_iou_threshold;
}

DLLEXPORT void mmod_options_set_truth_match_iou_threshold(mmod_options* options, double value)
{
    options->truth_match_iou_threshold = value;
}

DLLEXPORT test_box_overlap* mmod_options_get_overlaps_nms(mmod_options* options)
{
    return &(options->overlaps_nms);
}

DLLEXPORT void mmod_options_set_overlaps_nms(mmod_options* options, test_box_overlap* value)
{
    options->overlaps_nms = *value;
}

DLLEXPORT test_box_overlap* mmod_options_get_overlaps_ignore(mmod_options* options)
{
    return &(options->overlaps_ignore);
}

DLLEXPORT void mmod_options_set_overlaps_ignore(mmod_options* options, test_box_overlap* value)
{
    options->overlaps_ignore = *value;
}

DLLEXPORT bool mmod_options_get_use_bounding_box_regression(mmod_options* options)
{
    return options->use_bounding_box_regression;
}

DLLEXPORT void mmod_options_set_use_bounding_box_regression(mmod_options* options, bool value)
{
    options->use_bounding_box_regression = value;
}

DLLEXPORT double mmod_options_get_bbr_lambda(mmod_options* options)
{
    return options->bbr_lambda;
}

DLLEXPORT void mmod_options_set_vbbr_lambda(mmod_options* options, double value)
{
    options->bbr_lambda = value;
}

#pragma endregion mmod_options

#pragma region detector_window_details

DLLEXPORT mmod_options::detector_window_details* detector_window_details_new(const unsigned long w,
                                                                             const unsigned long h,
                                                                             const char* label)
{
    const std::string l(label);
    return new mmod_options::detector_window_details(w, h, l);
}

DLLEXPORT void detector_window_details_delete(mmod_options::detector_window_details* details)
{
    delete details;
}

DLLEXPORT unsigned long detector_window_details_width(mmod_options::detector_window_details* details)
{
    return details->width;
}

DLLEXPORT unsigned long detector_window_details_height(mmod_options::detector_window_details* details)
{
    return details->height;
}

DLLEXPORT std::string* detector_window_details_label(mmod_options::detector_window_details* details)
{
    return new std::string(details->label);
}

#pragma endregion detector_window_details

#endif