#ifndef _CPP_NET_H_
#define _CPP_NET_H_

#include "../export.h"
#include <dlib/dnn.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;

template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;

using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;

// Developer can customize to control construction and destruction as you want to do!!
typedef struct
{
} net_param;


DLLEXPORT void* net_new(const net_param* param)
{
    return new net_type();
}

void convert(std::vector<mmod_rect>& src, std::vector<mmod_rect*>* dst)
{
    for (int i = 0; i < src.size(); i++)
    {
        mmod_rect& m = src[i];
        dst->push_back(new mmod_rect(m));
    }
}

DLLEXPORT int net_operator(void* obj, const net_param* param, matrix_element_type type, void* matrix, std::vector<mmod_rect*>* ret)
{
    net_type& net = *(static_cast<net_type*>(obj));
    int err = ERR_OK;
    
    switch(type)
    {
        case matrix_element_type::RgbPixel:
            {                
                dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
                auto dets = net(tmp);
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

DLLEXPORT void net_delete(void* obj, const net_param* param)
{
    auto net = static_cast<net_type*>(obj);
    delete net;
}

#endif