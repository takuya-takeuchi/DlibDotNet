#ifndef _CPP_LOSS_METRIC_H_
#define _CPP_LOSS_METRIC_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
#pragma region type definitions
template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual = add_prev1<block<N,BN,1,tag1<SUBNET>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_down = add_prev2<avg_pool<2,2,2,2,skip1<tag2<block<N,BN,2,tag1<SUBNET>>>>>>;

template <int N, template <typename> class BN, int stride, typename SUBNET> 
using block  = BN<con<N,3,3,1,1,relu<BN<con<N,3,3,stride,stride,SUBNET>>>>>;

template <int N, typename SUBNET> using ares      = relu<residual<block,N,affine,SUBNET>>;
template <int N, typename SUBNET> using ares_down = relu<residual_down<block,N,affine,SUBNET>>;

template <typename SUBNET> using alevel0 = ares_down<256,SUBNET>;
template <typename SUBNET> using alevel1 = ares<256,ares<256,ares_down<256,SUBNET>>>;
template <typename SUBNET> using alevel2 = ares<128,ares<128,ares_down<128,SUBNET>>>;
template <typename SUBNET> using alevel3 = ares<64,ares<64,ares<64,ares_down<64,SUBNET>>>>;
template <typename SUBNET> using alevel4 = ares<32,ares<32,ares<32,SUBNET>>>;

using anet_type = loss_metric<fc_no_bias<128,avg_pool_everything<
                            alevel0<
                            alevel1<
                            alevel2<
                            alevel3<
                            alevel4<
                            max_pool<3,3,2,2,relu<affine<con<32,7,7,2,2,
                            input_rgb_image_sized<150>
                            >>>>>>>>>>>>;
#pragma endregion type definitions

typedef matrix<float,0,1> out_type;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define loss_metric_operator_matrixs_template(net, matrix, templateRows, templateColumns, dst) \
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

DLLEXPORT void* loss_metric_new(const int type)
{
    // Check type argument and cast to the proper type
    return new anet_type();
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

DLLEXPORT int loss_metric_operator_matrix(void* obj, const int type, matrix_element_type element_type, void* matrix, out_type** ret)
{
    // Check type argument and cast to the proper type
    anet_type& net = *(static_cast<anet_type*>(obj));
    int err = ERR_OK;
    
    switch(element_type)
    {
        case matrix_element_type::RgbPixel:
            {                
                dlib::matrix<rgb_pixel>& tmp = *(static_cast<dlib::matrix<rgb_pixel>*>(matrix));
                dlib::matrix<float, 0L, 1L> dets = net(tmp);
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
DLLEXPORT int loss_metric_operator_matrixs(void* obj, const int type, matrix_element_type element_type, void* matrix, int templateRows, int templateColumns, std::vector<out_type>** ret)
{
    // Check type argument and cast to the proper type
    anet_type& net = *(static_cast<anet_type*>(obj));
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

DLLEXPORT void loss_metric_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    delete obj;
}

DLLEXPORT void* loss_metric_deserialize(const char* file_name, const int type)
{
    // Check type argument and cast to the proper type
    anet_type* net = new anet_type();
    dlib::deserialize(file_name) >> (*net);
    return net;
}

#endif