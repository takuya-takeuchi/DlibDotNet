#ifndef _CPP_SURF_H_
#define _CPP_SURF_H_

#include "../export.h"
#include <dlib/image_keypoint/hessian_pyramid.h>
#include <dlib/pixel.h>
#include "../shared.h"

using namespace dlib;

#pragma region interest_point

DLLEXPORT dlib::vector<double,2>* interest_point_get_center(dlib::interest_point* ip)
{
    return &(ip->center);
}

DLLEXPORT double interest_point_get_scale(dlib::interest_point* ip)
{
    return ip->scale;
}

DLLEXPORT double interest_point_get_score(dlib::interest_point* ip)
{
    return ip->score;
}

DLLEXPORT double interest_point_get_laplacian(dlib::interest_point* ip)
{
    return ip->laplacian;
}

DLLEXPORT void interest_point_delete(dlib::interest_point* ip)
{
    delete ip;
}

#pragma endregion interest_point

#endif