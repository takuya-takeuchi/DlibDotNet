#ifndef _CPP_IMAGE_PYRAMID_H_
#define _CPP_IMAGE_PYRAMID_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_transforms/spatial_filtering.h>
#include <dlib/image_transforms/draw.h>
#include <dlib/image_transforms/image_pyramid.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/matrix/matrix.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_RATE 0
#define PYRAMID_TYPE PYRAMID_TYPE
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef PYRAMID_TYPE
#undef PYRAMID_RATE

#define create_tiled_pyramid_template_sub(img, rate, tiled_img, rects, padding, outer_padding) \
do {\
    dlib::matrix<ELEMENT_IN>& tmp_img = *static_cast<dlib::matrix<ELEMENT_IN>*>(img);\
    dlib::matrix<ELEMENT_OUT> tmp_tiled_img;\
    std::vector<dlib::rectangle> tmp_rects;\
    create_tiled_pyramid<PYRAMID_TYPE<rate>>(tmp_img, tmp_tiled_img, tmp_rects, padding, outer_padding);\
    auto out_rects = new std::vector<dlib::rectangle*>();\
    for (int i = 0; i < tmp_rects.size(); i++)\
        out_rects->push_back(new dlib::rectangle(tmp_rects[i]));\
    *tiled_img = new dlib::matrix<ELEMENT_OUT>(tmp_tiled_img);\
    *rects = out_rects;\
} while (0)

#define create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding) \
do {\
    switch(pyramid_rate)\
    {\
        case 1:\
            create_tiled_pyramid_template_sub(img, 1, tiled_img, rects, padding, outer_padding);\
            break;\
        case 2:\
            create_tiled_pyramid_template_sub(img, 2, tiled_img, rects, padding, outer_padding);\
            break;\
        case 3:\
            create_tiled_pyramid_template_sub(img, 3, tiled_img, rects, padding, outer_padding);\
            break;\
        case 4:\
            create_tiled_pyramid_template_sub(img, 4, tiled_img, rects, padding, outer_padding);\
            break;\
        case 6:\
            create_tiled_pyramid_template_sub(img, 6, tiled_img, rects, padding, outer_padding);\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int create_tiled_pyramid(const matrix_element_type element_type,
                                   void* img,
                                   const pyramid_type pyramid_type,
                                   const unsigned int pyramid_rate,
                                   const unsigned int padding,
                                   const unsigned int outer_padding,
                                   void** tiled_img,
                                   void** rects)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(element_type)
                {
                    case matrix_element_type::UInt8:
                        #define ELEMENT_IN uint8_t
                        #define ELEMENT_OUT uint8_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::UInt16:
                        #define ELEMENT_IN uint16_t
                        #define ELEMENT_OUT uint16_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::UInt32:
                        #define ELEMENT_IN uint32_t
                        #define ELEMENT_OUT uint32_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int8:
                        #define ELEMENT_IN int8_t
                        #define ELEMENT_OUT int8_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int16:
                        #define ELEMENT_IN int16_t
                        #define ELEMENT_OUT int16_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Int32:
                        #define ELEMENT_IN int32_t
                        #define ELEMENT_OUT int32_t
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Float:
                        #define ELEMENT_IN float
                        #define ELEMENT_OUT float
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::Double:
                        #define ELEMENT_IN double
                        #define ELEMENT_OUT double
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::RgbPixel:
                        #define ELEMENT_IN rgb_pixel
                        #define ELEMENT_OUT rgb_pixel
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::HsiPixel:
                        #define ELEMENT_IN hsi_pixel
                        #define ELEMENT_OUT hsi_pixel
                        create_tiled_pyramid_template(img, pyramid_rate, tiled_img, rects, padding, outer_padding);
                        #undef ELEMENT_OUT
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::RgbAlphaPixel:
                    default:
                        err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                        break;
                }
                break;
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

#endif