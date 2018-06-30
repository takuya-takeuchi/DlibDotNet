#ifndef _CPP_LOSS_MMOD_H_
#define _CPP_LOSS_MMOD_H_

#include <iostream>
#include <dlib/data_io.h>
#include <dlib/dnn.h>
#include <dlib/image_processing.h>
#include <dlib/gui_widgets.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
#pragma region type definitions
template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;

template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;

using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
#pragma endregion type definitions

typedef std::vector<mmod_rect> out_type;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define loss_mmod_operator_matrixs_template(net, matrix, templateRows, templateColumns, dst) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        std::vector<dlib::matrix<ELEMENT>*>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(matrix));\
        std::vector<dlib::matrix<ELEMENT>> in_tmp;\
        for (int i = 0; i< tmp.size(); i++)\
        {\
            dlib::matrix<ELEMENT>& mat = *tmp[i];\
            in_tmp.push_back(mat);\
        }\
        std::vector<out_type> dets = net(in_tmp);\
        for (int i = 0; i< dets.size(); i++)\
        {\
            dst->push_back(new out_type(dets[i]));\
        }\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        std::vector<dlib::matrix<ELEMENT>*>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(matrix));\
        std::vector<dlib::matrix<ELEMENT>> in_tmp;\
        for (int i = 0; i< tmp.size(); i++)\
        {\
            dlib::matrix<ELEMENT>& mat = *tmp[i];\
            in_tmp.push_back(mat);\
        }\
        std::vector<out_type> dets = net(in_tmp);\
        for (int i = 0; i< dets.size(); i++)\
        {\
            dst->push_back(new out_type(dets[i]));\
        }\
    }\
} while (0)

#pragma endregion template

DLLEXPORT void* loss_mmod_new(const int type)
{
    // Check type argument and cast to the proper type
    return new net_type();
}

void convert(out_type& src, out_type** dst)
{
    *dst = new out_type(src);
}

void convert(std::vector<out_type>& src, std::vector<out_type*>** dst)
{
    auto tmp = new std::vector<out_type*>();
    for (int i = 0; i < src.size(); i++)
        tmp->push_back(new out_type(src[i]));
    *dst = tmp;
}

DLLEXPORT int loss_mmod_operator_matrix(void* obj, const int type, matrix_element_type element_type, void* matrix, out_type** ret)
{
    // Check type argument and cast to the proper type
    net_type& net = *(static_cast<net_type*>(obj));
    int err = ERR_OK;
    
    switch(element_type)
    {
        case matrix_element_type::RgbPixel:
            {                
                dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
                out_type dets = net(tmp);
                convert(dets, ret);
            }
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
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_mmod_operator_matrixs(void* obj, const int type, matrix_element_type element_type, void* matrix, int templateRows, int templateColumns, std::vector<out_type>** ret)
{
    // Check type argument and cast to the proper type
    net_type& net = *(static_cast<net_type*>(obj));
    int err = ERR_OK;
    
    switch(element_type)
    {
        case matrix_element_type::RgbPixel:
            {
                std::vector<dlib::matrix<rgb_pixel>*>& tmp = *(static_cast<std::vector<dlib::matrix<rgb_pixel>*>*>(matrix));
                std::vector<dlib::matrix<rgb_pixel>> in_tmp;
                for (int i = 0; i< tmp.size(); i++)
                {
                    dlib::matrix<rgb_pixel>& mat = *tmp[i];
                    in_tmp.push_back(mat);
                }

                std::vector<out_type> dets = net(in_tmp);
                *ret = new std::vector<out_type>(dets);
            }
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
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT void loss_mmod_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            delete (net_type*)obj;
            break;
    }
}

DLLEXPORT void* loss_mmod_deserialize(const char* file_name, const int type)
{
    // Check type argument and cast to the proper type
    net_type* net = new net_type();
    dlib::deserialize(file_name) >> (*net);
    return net;
}

#endif