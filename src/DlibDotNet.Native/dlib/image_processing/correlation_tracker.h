#ifndef _CPP_CORRELATION_TRACKER_H_
#define _CPP_CORRELATION_TRACKER_H_

#include "../export.h"
#include <dlib/image_processing.h>
#include "../shared.h"
#include "../template.h"

using namespace dlib;
using namespace std;

#pragma region template

#define correlation_tracker_start_track_template(__TYPE__, error, type, ...) \
tracker->start_track(*((array2d<__TYPE__>*)img), *p);

#define correlation_tracker_update_noscale_template(__TYPE__, error, type, ...) \
*confident = tracker->update_noscale(*((array2d<__TYPE__>*)img), *guess);

#define correlation_tracker_update_noscale2_template(__TYPE__, error, type, ...) \
*confident = tracker->update_noscale(*((array2d<__TYPE__>*)img));

#define correlation_tracker_update_template(__TYPE__, error, type, ...) \
*confident = tracker->update(*((array2d<__TYPE__>*)img), *guess);

#define correlation_tracker_update2_template(__TYPE__, error, type, ...) \
*confident = tracker->update(*((array2d<__TYPE__>*)img));

#pragma endregion template

DLLEXPORT correlation_tracker* correlation_tracker_new(
    unsigned int filter_size,
    unsigned int num_scale_levels,
    unsigned int scale_window_size,
    double regularizer_space,
    double nu_space,
    double regularizer_scale,
    double nu_scale,
    double scale_pyramid_alpha)
{
    return new dlib::correlation_tracker(
        filter_size,
        num_scale_levels,
        scale_window_size,
        regularizer_space,
        nu_space,
        regularizer_scale,
        nu_scale,
        scale_pyramid_alpha);
}

DLLEXPORT int correlation_tracker_start_track(correlation_tracker* tracker, array2d_type type, void* img, dlib::drectangle* p)
{
    int error = ERR_OK;
    
    array2d_nonalpha_template(type,
                              error,
                              correlation_tracker_start_track_template,
                              img,
                              p);

    return error;
}

DLLEXPORT drectangle* correlation_tracker_get_position(correlation_tracker* tracker)
{
    dlib::drectangle rect = tracker->get_position();
    return new dlib::drectangle(rect);
}

DLLEXPORT int correlation_tracker_update_noscale(correlation_tracker* tracker,
                                                 const array2d_type type,
                                                 void* img,
                                                 drectangle* guess,
                                                 double* confident)
{
    int error = ERR_OK;
    
    array2d_nonalpha_template(type,
                              error,
                              correlation_tracker_update_noscale_template,
                              img,
                              guess,
                              confident);

    return error;
}

DLLEXPORT int correlation_tracker_update_noscale2(correlation_tracker* tracker,
                                                  const array2d_type type,
                                                  void* img,
                                                  double* confident)
{
    int error = ERR_OK;
    
    array2d_nonalpha_template(type,
                              error,
                              correlation_tracker_update_noscale2_template,
                              img,
                              confident);

    return error;
}

DLLEXPORT int correlation_tracker_update(correlation_tracker* tracker,
                                         const array2d_type type,
                                         void* img,
                                         drectangle* guess,
                                         double* confident)
{
    int error = ERR_OK;
    
    array2d_nonalpha_template(type,
                              error,
                              correlation_tracker_update_template,
                              img,
                              guess,
                              confident);

    return error;
}

DLLEXPORT int correlation_tracker_update2(correlation_tracker* tracker,
                                          const array2d_type type,
                                          void* img,
                                          double* confident)
{
    int error = ERR_OK;
    
    array2d_nonalpha_template(type,
                              error,
                              correlation_tracker_update2_template,
                              img,
                              confident);

    return error;
}

DLLEXPORT void correlation_tracker_delete(correlation_tracker* obj)
{
	delete obj;
}

#endif