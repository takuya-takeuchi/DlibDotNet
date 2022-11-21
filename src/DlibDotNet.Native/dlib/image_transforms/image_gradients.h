#ifndef _CPP_IMAGE_GRADIENTS_H_
#define _CPP_IMAGE_GRADIENTS_H_

#include "../export.h"
#include <dlib/image_transforms/spatial_filtering.h>
#include <dlib/image_transforms/edge_detector.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define image_gradients_gradient_x_template(__TYPE__, error, type, ...) \
*((dlib::rectangle*)valid_area) = gradients->gradient_x(*((array2d<__TYPE__>*)in_image), *((array2d<float>*)out_image));

#define image_gradients_gradient_y_template(__TYPE__, error, type, ...) \
*((dlib::rectangle*)valid_area) = gradients->gradient_y(*((array2d<__TYPE__>*)in_image), *((array2d<float>*)out_image));

#define image_gradients_gradient_xx_template(__TYPE__, error, type, ...) \
*((dlib::rectangle*)valid_area) = gradients->gradient_xx(*((array2d<__TYPE__>*)in_image), *((array2d<float>*)out_image));

#define image_gradients_gradient_xy_template(__TYPE__, error, type, ...) \
*((dlib::rectangle*)valid_area) = gradients->gradient_xy(*((array2d<__TYPE__>*)in_image), *((array2d<float>*)out_image));

#define image_gradients_gradient_yy_template(__TYPE__, error, type, ...) \
*((dlib::rectangle*)valid_area) = gradients->gradient_yy(*((array2d<__TYPE__>*)in_image), *((array2d<float>*)out_image));

#pragma endregion template

DLLEXPORT dlib::image_gradients* get_image_gradients(long scale)
{
    return new dlib::image_gradients(scale);
}

DLLEXPORT int image_gradients_gradient_x(dlib::image_gradients* gradients,
                                         array2d_type type,
                                         void* in_image,
                                         void* out_image,
                                         void* valid_area)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              image_gradients_gradient_x_template,
                              in_image,
                              out_image,
                              valid_area);

    return error;
}

DLLEXPORT int image_gradients_gradient_y(dlib::image_gradients* gradients,
                                         array2d_type type,
                                         void* in_image,
                                         void* out_image,
                                         void* valid_area)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              image_gradients_gradient_y_template,
                              in_image,
                              out_image,
                              valid_area);

    return error;
}

DLLEXPORT int image_gradients_gradient_xx(dlib::image_gradients* gradients,
                                         array2d_type type,
                                         void* in_image,
                                         void* out_image,
                                         void* valid_area)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              image_gradients_gradient_xx_template,
                              in_image,
                              out_image,
                              valid_area);

    return error;
}

DLLEXPORT int image_gradients_gradient_xy(dlib::image_gradients* gradients,
                                         array2d_type type,
                                         void* in_image,
                                         void* out_image,
                                         void* valid_area)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              image_gradients_gradient_xy_template,
                              in_image,
                              out_image,
                              valid_area);

    return error;
}


DLLEXPORT int image_gradients_gradient_yy(dlib::image_gradients* gradients,
                                         array2d_type type,
                                         void* in_image,
                                         void* out_image,
                                         void* valid_area)
{
    int error = ERR_OK;

    array2d_nonalpha_template(type,
                              error,
                              image_gradients_gradient_yy_template,
                              in_image,
                              out_image,
                              valid_area);

    return error;
}

DLLEXPORT void image_gradients_delete(dlib::image_gradients* obj)
{
	delete obj;
}

#endif