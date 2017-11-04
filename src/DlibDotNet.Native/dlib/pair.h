#ifndef _CPP_PAIR_H_
#define _CPP_PAIR_H_

#include "export.h"
#include <dlib/geometry/rectangle.h>

using namespace dlib;

#pragma region point_point

DLLEXPORT std::pair<dlib::point*, dlib::point*>* pair_point_point_new(dlib::point* first, dlib::point* second)
{
    return new std::pair<dlib::point*, dlib::point*>(first, second);
}

DLLEXPORT dlib::point* pair_point_point_get_first(std::pair<dlib::point*, dlib::point*>* pair)
{
    return pair->first;
}

DLLEXPORT void pair_point_point_set_first(std::pair<dlib::point*, dlib::point*>* pair, dlib::point* first)
{
    pair->first = first;
}

DLLEXPORT dlib::point* pair_point_point_get_second(std::pair<dlib::point*, dlib::point*>* pair)
{
    return pair->second;
}

DLLEXPORT void pair_point_point_set_second(std::pair<dlib::point*, dlib::point*>* pair, dlib::point* second)
{
    pair->second = second;
}

DLLEXPORT void pair_point_point_delete(std::pair<dlib::point*, dlib::point*>* pair)
{
    // if (pair->first) delete pair->first;
    // if (pair->second) delete pair->second;
    delete pair;
}

#pragma endregion point_point

#endif