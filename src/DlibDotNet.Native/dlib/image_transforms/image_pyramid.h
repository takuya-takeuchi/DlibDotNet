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
#define RECTANGLE_TYPE drectangle
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef RECTANGLE_TYPE
#undef PYRAMID_TYPE
#undef PYRAMID_RATE

#define create_tiled_pyramid_template_sub(img, rate, tiled_img, rects, padding, outer_padding) \
do {\
    dlib::matrix<ELEMENT_IN>& tmp_img = *static_cast<dlib::matrix<ELEMENT_IN>*>(img);\
    dlib::matrix<ELEMENT_OUT> tmp_tiled_img;\
    std::vector<dlib::rectangle> tmp_rects;\
    dlib::create_tiled_pyramid<PYRAMID_TYPE<rate>>(tmp_img, tmp_tiled_img, tmp_rects, padding, outer_padding);\
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

#define pyramid_down_rect_down_template(pyramid_rate, pyramid, rect, ret) \
do {\
    pyramid_down<pyramid_rate>& pyr = *static_cast<pyramid_down<pyramid_rate>*>(pyramid);\
    auto r = pyr.rect_down(*rect);\
    *ret = new RECTANGLE_TYPE(r);\
} while (0)

#define pyramid_down_rect_down2_template(pyramid_rate, pyramid, rect, levels, ret) \
do {\
    pyramid_down<pyramid_rate>& pyr = *static_cast<pyramid_down<pyramid_rate>*>(pyramid);\
    auto r = pyr.rect_down(*rect, levels);\
    *ret = new RECTANGLE_TYPE(r);\
} while (0)

#define pyramid_down_rect_up_template(pyramid_rate, pyramid, rect, ret) \
do {\
    pyramid_down<pyramid_rate>& pyr = *static_cast<pyramid_down<pyramid_rate>*>(pyramid);\
    auto r = pyr.rect_up(*rect);\
    *ret = new RECTANGLE_TYPE(r);\
} while (0)

#define pyramid_down_rect_up2_template(pyramid_rate, pyramid, rect, levels, ret) \
do {\
    pyramid_down<pyramid_rate>& pyr = *static_cast<pyramid_down<pyramid_rate>*>(pyramid);\
    auto r = pyr.rect_up(*rect, levels);\
    *ret = new RECTANGLE_TYPE(r);\
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
        case ::pyramid_type::Down:
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
                    case matrix_element_type::BgrPixel:
                        #define ELEMENT_IN bgr_pixel
                        #define ELEMENT_OUT bgr_pixel
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

#pragma region pyramid_down

#pragma endregion pyramid_down

DLLEXPORT int pyramid_down_new(const unsigned int pyramid_rate,
                               void** ret)
{
    int err = ERR_OK;

    switch(pyramid_rate)
    {
        case 1:
            *ret = new pyramid_down<1>();
            break;
        case 2:
            *ret = new pyramid_down<2>();
            break;
        case 3:
            *ret = new pyramid_down<3>();
            break;
        case 4:
            *ret = new pyramid_down<4>();
            break;
        case 6:
            *ret = new pyramid_down<6>();
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    return err;
}

DLLEXPORT void pyramid_down_delete(const unsigned int pyramid_rate,
                                   void* pyramid)
{
    switch(pyramid_rate)
    {
        case 1:
            delete ((pyramid_down<1>*)pyramid);
            break;
        case 2:
            delete ((pyramid_down<2>*)pyramid);
            break;
        case 3:
            delete ((pyramid_down<3>*)pyramid);
            break;
        case 4:
            delete ((pyramid_down<4>*)pyramid);
            break;
        case 6:
            delete ((pyramid_down<6>*)pyramid);
            break;
    }
}

DLLEXPORT int pyramid_down_rect_down(void* pyramid,
                                     const unsigned int pyramid_rate,
                                     drectangle* rect,
                                     drectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE drectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_down_template(1, pyramid, rect, ret);
            break;
        case 2:
            pyramid_down_rect_down_template(2, pyramid, rect, ret);
            break;
        case 3:
            pyramid_down_rect_down_template(3, pyramid, rect, ret);
            break;
        case 4:
            pyramid_down_rect_down_template(4, pyramid, rect, ret);
            break;
        case 6:
            pyramid_down_rect_down_template(6, pyramid, rect, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_down_rectangle(void* pyramid,
                                               const unsigned int pyramid_rate,
                                               rectangle* rect,
                                               rectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE rectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_down_template(1, pyramid, rect, ret);
            break;
        case 2:
            pyramid_down_rect_down_template(2, pyramid, rect, ret);
            break;
        case 3:
            pyramid_down_rect_down_template(3, pyramid, rect, ret);
            break;
        case 4:
            pyramid_down_rect_down_template(4, pyramid, rect, ret);
            break;
        case 6:
            pyramid_down_rect_down_template(6, pyramid, rect, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_down2(void* pyramid,
                                      const unsigned int pyramid_rate,
                                      drectangle* rect,
                                      unsigned int levels,
                                      drectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE drectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_down2_template(1, pyramid, rect, levels, ret);
            break;
        case 2:
            pyramid_down_rect_down2_template(2, pyramid, rect, levels, ret);
            break;
        case 3:
            pyramid_down_rect_down2_template(3, pyramid, rect, levels, ret);
            break;
        case 4:
            pyramid_down_rect_down2_template(4, pyramid, rect, levels, ret);
            break;
        case 6:
            pyramid_down_rect_down2_template(6, pyramid, rect, levels, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_down2_rectangle(void* pyramid,
                                                const unsigned int pyramid_rate,
                                                rectangle* rect,
                                                unsigned int levels,
                                                rectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE rectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_down2_template(1, pyramid, rect, levels, ret);
            break;
        case 2:
            pyramid_down_rect_down2_template(2, pyramid, rect, levels, ret);
            break;
        case 3:
            pyramid_down_rect_down2_template(3, pyramid, rect, levels, ret);
            break;
        case 4:
            pyramid_down_rect_down2_template(4, pyramid, rect, levels, ret);
            break;
        case 6:
            pyramid_down_rect_down2_template(6, pyramid, rect, levels, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}


DLLEXPORT int pyramid_down_rect_up(void* pyramid,
                                   const unsigned int pyramid_rate,
                                   drectangle* rect,
                                   drectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE drectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_up_template(1, pyramid, rect, ret);
            break;
        case 2:
            pyramid_down_rect_up_template(2, pyramid, rect, ret);
            break;
        case 3:
            pyramid_down_rect_up_template(3, pyramid, rect, ret);
            break;
        case 4:
            pyramid_down_rect_up_template(4, pyramid, rect, ret);
            break;
        case 6:
            pyramid_down_rect_up_template(6, pyramid, rect, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_up_rectangle(void* pyramid,
                                             const unsigned int pyramid_rate,
                                             rectangle* rect,
                                             rectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE rectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_up_template(1, pyramid, rect, ret);
            break;
        case 2:
            pyramid_down_rect_up_template(2, pyramid, rect, ret);
            break;
        case 3:
            pyramid_down_rect_up_template(3, pyramid, rect, ret);
            break;
        case 4:
            pyramid_down_rect_up_template(4, pyramid, rect, ret);
            break;
        case 6:
            pyramid_down_rect_up_template(6, pyramid, rect, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_up2(void* pyramid,
                                    const unsigned int pyramid_rate,
                                    drectangle* rect,
                                    unsigned int levels,
                                    drectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE drectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_up2_template(1, pyramid, rect, levels, ret);
            break;
        case 2:
            pyramid_down_rect_up2_template(2, pyramid, rect, levels, ret);
            break;
        case 3:
            pyramid_down_rect_up2_template(3, pyramid, rect, levels, ret);
            break;
        case 4:
            pyramid_down_rect_up2_template(4, pyramid, rect, levels, ret);
            break;
        case 6:
            pyramid_down_rect_up2_template(6, pyramid, rect, levels, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

DLLEXPORT int pyramid_down_rect_up2_rectangle(void* pyramid,
                                              const unsigned int pyramid_rate,
                                              rectangle* rect,
                                              unsigned int levels,
                                              rectangle** ret)
{
    int err = ERR_OK;

    #define RECTANGLE_TYPE rectangle

    switch(pyramid_rate)
    {
        case 1:
            pyramid_down_rect_up2_template(1, pyramid, rect, levels, ret);
            break;
        case 2:
            pyramid_down_rect_up2_template(2, pyramid, rect, levels, ret);
            break;
        case 3:
            pyramid_down_rect_up2_template(3, pyramid, rect, levels, ret);
            break;
        case 4:
            pyramid_down_rect_up2_template(4, pyramid, rect, levels, ret);
            break;
        case 6:
            pyramid_down_rect_up2_template(6, pyramid, rect, levels, ret);
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;
            break;
    }

    #undef RECTANGLE_TYPE

    return err;
}

#endif