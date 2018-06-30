#ifndef _CPP_GRAPH_UTILS_SAMPLEPAIR_H_
#define _CPP_GRAPH_UTILS_SAMPLEPAIR_H_

#include "../export.h"
#include <dlib/graph_utils/sample_pair.h>
#include "../shared.h"

using namespace dlib;

DLLEXPORT sample_pair* sample_pair_new(const unsigned long idx1, const unsigned long idx2, const double distance)
{
	return new sample_pair(idx1, idx2, distance);
}

DLLEXPORT const unsigned long sample_pair_get_index1(sample_pair* obj)
{
	return obj->index1();
}

DLLEXPORT const unsigned long sample_pair_get_index2(sample_pair* obj)
{
	return obj->index2();
}

DLLEXPORT const double sample_pair_get_distance(sample_pair* obj)
{
	return obj->distance();
}

DLLEXPORT void sample_pair_delete(sample_pair* obj)
{
	delete obj;
}

#endif