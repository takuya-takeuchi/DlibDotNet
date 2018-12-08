#ifndef _CPP_INPUT_H_
#define _CPP_INPUT_H_

#include <dlib/dnn.h>
#include <vector>

#include "../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_TYPE PYRAMID_TYPE
#define EXTRACTOR_TYPE EXTRACTOR_TYPE
#define ELEMENT_IN element
#define ELEMENT_OUT element
#undef ELEMENT_IN
#undef ELEMENT_OUT
#undef EXTRACTOR_TYPE
#undef PYRAMID_TYPE

#define input_rgb_image_pyramid_to_tensor_template_sub(input, pyramid_rate, matrix, templateRows, templateColumns, iterator_count, tensor) \
do { \
    dlib::input_rgb_image_pyramid<PYRAMID_TYPE<pyramid_rate>>& in = *static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<pyramid_rate>>*>(input);\
    dlib::matrix<ELEMENT_IN>& m = *static_cast<dlib::matrix<ELEMENT_IN>*>(matrix);\
    dlib::matrix<ELEMENT_IN>* begin = &m;\
    dlib::matrix<ELEMENT_IN>* end = &m + iterator_count;\
    dlib::resizable_tensor& t = *static_cast<dlib::resizable_tensor*>(tensor);\
    in.to_tensor(begin, end, t);\
} while (0)

#define input_rgb_image_pyramid_to_tensor_template(input, pyramid_rate, matrix, templateRows, templateColumns, iterator_count, tensor) \
do { \
    switch(pyramid_rate)\
    {\
        case 1:\
            input_rgb_image_pyramid_to_tensor_template_sub(input, 1, matrix, templateRows, templateColumns, iterator_count, tensor);\
            break;\
        case 2:\
            input_rgb_image_pyramid_to_tensor_template_sub(input, 2, matrix, templateRows, templateColumns, iterator_count, tensor);\
            break;\
        case 3:\
            input_rgb_image_pyramid_to_tensor_template_sub(input, 3, matrix, templateRows, templateColumns, iterator_count, tensor);\
            break;\
        case 4:\
            input_rgb_image_pyramid_to_tensor_template_sub(input, 4, matrix, templateRows, templateColumns, iterator_count, tensor);\
            break;\
        case 6:\
            input_rgb_image_pyramid_to_tensor_template_sub(input, 6, matrix, templateRows, templateColumns, iterator_count, tensor);\
            break;\
        default:\
            err = ERR_PYRAMID_NOT_SUPPORT_RATE;\
            break;\
    }\
} while (0)

#pragma endregion template

#pragma region input_rgb_image_pyramid

DLLEXPORT int input_rgb_image_pyramid_new(const pyramid_type pyramid_type,
                                          const unsigned int pyramid_rate,
                                          void** ret)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(pyramid_rate)
                {
                    case 1:
                        *ret = new dlib::input_rgb_image_pyramid<pyramid_down<1>>();
                        break;
                    case 2:
                        *ret = new dlib::input_rgb_image_pyramid<pyramid_down<2>>();
                        break;
                    case 3:
                        *ret = new dlib::input_rgb_image_pyramid<pyramid_down<3>>();
                        break;
                    case 4:
                        *ret = new dlib::input_rgb_image_pyramid<pyramid_down<4>>();
                        break;
                    case 6:
                        *ret = new dlib::input_rgb_image_pyramid<pyramid_down<6>>();
                        break;
                    default:
                        err = ERR_PYRAMID_NOT_SUPPORT_RATE;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

DLLEXPORT void input_rgb_image_pyramid_delete(void* input,
                                              const pyramid_type pyramid_type,
                                              const unsigned int pyramid_rate)
{
    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(pyramid_rate)
                {
                    case 1:
                        delete (dlib::input_rgb_image_pyramid<pyramid_down<1>>*)input;
                        break;
                    case 2:
                        delete (dlib::input_rgb_image_pyramid<pyramid_down<2>>*)input;
                        break;
                    case 3:
                        delete (dlib::input_rgb_image_pyramid<pyramid_down<3>>*)input;
                        break;
                    case 4:
                        delete (dlib::input_rgb_image_pyramid<pyramid_down<4>>*)input;
                        break;
                    case 6:
                        delete (dlib::input_rgb_image_pyramid<pyramid_down<6>>*)input;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
    }
}

DLLEXPORT int input_rgb_image_pyramid_to_tensor(void* input,
                                                const pyramid_type pyramid_type,
                                                const unsigned int pyramid_rate,
                                                matrix_element_type element_type,
                                                void* matrix,
                                                int templateRows,
                                                int templateColumns,
                                                unsigned int iterator_count,
                                                dlib::resizable_tensor* tensor)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(element_type)
                {
                    case matrix_element_type::RgbPixel:
                        #define ELEMENT_IN rgb_pixel
                        input_rgb_image_pyramid_to_tensor_template(input, pyramid_rate, matrix, templateRows, templateColumns, iterator_count, tensor);
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::RgbAlphaPixel:
                        #define ELEMENT_IN rgb_alpha_pixel
                        input_rgb_image_pyramid_to_tensor_template(input, pyramid_rate, matrix, templateRows, templateColumns, iterator_count, tensor);
                        #undef ELEMENT_IN
                        break;
                    case matrix_element_type::UInt8:
                    case matrix_element_type::UInt16:
                    case matrix_element_type::UInt32:
                    case matrix_element_type::Int8:
                    case matrix_element_type::Int16:
                    case matrix_element_type::Int32:
                    case matrix_element_type::Float:
                    case matrix_element_type::Double:
                    case matrix_element_type::HsiPixel:
                    default:
                        err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

DLLEXPORT int input_rgb_image_pyramid_get_pyramid_padding(void* input,
                                                          const pyramid_type pyramid_type,
                                                          const unsigned int pyramid_rate,
                                                          unsigned long* pyramid_padding)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(pyramid_rate)
                {
                    case 1:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<1>>*>(input);
                            *pyramid_padding = in->get_pyramid_padding();
                        }
                        break;
                    case 2:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<2>>*>(input);
                            *pyramid_padding = in->get_pyramid_padding();
                        }
                        break;
                    case 3:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<3>>*>(input);
                            *pyramid_padding = in->get_pyramid_padding();
                        }
                        break;
                    case 4:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<4>>*>(input);
                            *pyramid_padding = in->get_pyramid_padding();
                        }
                        break;
                    case 6:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<6>>*>(input);
                            *pyramid_padding = in->get_pyramid_padding();
                        }
                        break;
                    default:
                        err = ERR_PYRAMID_NOT_SUPPORT_RATE;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

DLLEXPORT int input_rgb_image_pyramid_get_pyramid_outer_padding(void* input,
                                                                const pyramid_type pyramid_type,
                                                                const unsigned int pyramid_rate,
                                                                unsigned long* pyramid_outer_padding)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(pyramid_rate)
                {
                    case 1:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<1>>*>(input);
                            *pyramid_outer_padding = in->get_pyramid_outer_padding();
                        }
                        break;
                    case 2:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<2>>*>(input);
                            *pyramid_outer_padding = in->get_pyramid_outer_padding();
                        }
                        break;
                    case 3:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<3>>*>(input);
                            *pyramid_outer_padding = in->get_pyramid_outer_padding();
                        }
                        break;
                    case 4:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<4>>*>(input);
                            *pyramid_outer_padding = in->get_pyramid_outer_padding();
                        }
                        break;
                    case 6:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<6>>*>(input);
                            *pyramid_outer_padding = in->get_pyramid_outer_padding();
                        }
                        break;
                    default:
                        err = ERR_PYRAMID_NOT_SUPPORT_RATE;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

DLLEXPORT int input_rgb_image_pyramid_image_space_to_tensor_space(void* input,
                                                                  const pyramid_type pyramid_type,
                                                                  const unsigned int pyramid_rate,
                                                                  tensor* data,
                                                                  const double scale,
                                                                  drectangle* r,
                                                                  drectangle** rect)
{
    int err = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                drectangle& inr = *static_cast<drectangle*>(r);
                tensor& t = *static_cast<tensor*>(data);
                switch(pyramid_rate)
                {
                    case 1:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<1>>*>(input);
                            auto ret= in->image_space_to_tensor_space(t, scale, inr);
                            *rect = new drectangle(ret);
                        }
                        break;
                    case 2:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<2>>*>(input);
                            auto ret= in->image_space_to_tensor_space(t, scale, inr);
                            *rect = new drectangle(ret);
                        }
                        break;
                    case 3:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<3>>*>(input);
                            auto ret= in->image_space_to_tensor_space(t, scale, inr);
                            *rect = new drectangle(ret);
                        }
                        break;
                    case 4:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<4>>*>(input);
                            auto ret= in->image_space_to_tensor_space(t, scale, inr);
                            *rect = new drectangle(ret);
                        }
                        break;
                    case 6:
                        {
                            auto in = static_cast<dlib::input_rgb_image_pyramid<PYRAMID_TYPE<6>>*>(input);
                            auto ret= in->image_space_to_tensor_space(t, scale, inr);
                            *rect = new drectangle(ret);
                        }
                        break;
                    default:
                        err = ERR_PYRAMID_NOT_SUPPORT_RATE;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            err = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return err;
}

#pragma endregion input_rgb_image_pyramid

#endif