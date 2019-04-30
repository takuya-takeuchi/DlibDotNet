#ifndef _CPP_TEST_BOX_OVERLAP_H_
#define _CPP_TEST_BOX_OVERLAP_H_

#include "../export.h"
#include <dlib/image_processing/box_overlap_testing.h>
#include <dlib/geometry/rectangle.h>

using namespace dlib;
using namespace std;

#pragma region test_box_overlap

DLLEXPORT test_box_overlap* test_box_overlap_new(const double iou_thresh,
                                                 const double percent_covered_thresh)
{
    return new test_box_overlap(iou_thresh, percent_covered_thresh);
}

DLLEXPORT void test_box_overlap_delete(test_box_overlap* obj)
{
	delete obj;
}

DLLEXPORT double test_box_overlap_get_iou_thresh(test_box_overlap* overlap)
{
	return overlap->get_iou_thresh();
}

DLLEXPORT double test_box_overlap_get_percent_covered_thresh(test_box_overlap* overlap)
{
	return overlap->get_percent_covered_thresh();
}

#pragma region operator

DLLEXPORT bool test_box_overlap_operator(test_box_overlap* overlap, dlib::rectangle* a, dlib::rectangle* b)
{
	const dlib::rectangle& ta = *a;
	const dlib::rectangle& tb = *b;
	test_box_overlap& tbo = *overlap;
	return tbo(ta, tb);
}

#pragma endregion operator

#pragma endregion test_box_overlap

#endif