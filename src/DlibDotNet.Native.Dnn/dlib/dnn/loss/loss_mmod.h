#ifndef _CPP_LOSS_MMOD_H_
#define _CPP_LOSS_MMOD_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

#include "../trainer.h"

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
#pragma region type definitions
template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;
using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
using layer_details = add_layer<con_<1, 9, 9, 1, 1, 4, 4>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<45, 5, 5, 1, 1, 2, 2>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<45, 5, 5, 1, 1, 2, 2>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<45, 5, 5, 1, 1, 2, 2>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<16, 5, 5, 2, 2, 0, 0>,
                      input_rgb_image_pyramid
                      <pyramid_down<6>>>>>>>>>>>>>>>>>>>>>;

#pragma region 1
template <long num_filters, typename SUBNET> using con5d_1 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_1  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_1  = relu<affine<con5d_1<32, relu<affine<con5d_1<32, relu<affine<con5d_1<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_1  = relu<affine<con5_1<55,SUBNET>>>;
using net_type_1 = loss_mmod<con<1,9,9,1,1,rcon5_1<rcon5_1<rcon5_1<downsampler_1<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
using layer_details1 = add_layer<con_<1, 9, 9, 1, 1, 4, 4>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<affine_,
                       add_layer<con_<16, 5, 5, 2, 2, 0, 0>,
                       input_rgb_image_pyramid
                       <pyramid_down<6>>>>>>>>>>>>>>>>>>>>>;
#pragma endregion 1

#pragma region 2
template <long num_filters, typename SUBNET> using con5d_2 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con3_2  = con<num_filters,3,3,1,1,SUBNET>;
template <typename SUBNET> using downsampler_2  = relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon3_2  = relu<bn_con<con3_2<32,SUBNET>>>;
using net_type_2  = loss_mmod<con<1,6,6,1,1,rcon3_2<rcon3_2<rcon3_2<downsampler_2<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
using layer_details2 = add_layer<con_<1, 6, 6, 1, 1, 3, 3>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       input_rgb_image_pyramid
                       <pyramid_down<6>>>>>>>>>>>>>>>>>>>>>;
#pragma endregion 2

#pragma region 3
template <long num_filters, typename SUBNET> using con5d_3 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_3  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_3  = relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_3  = relu<bn_con<con5_3<55,SUBNET>>>;
using net_type_3 = loss_mmod<con<1,9,9,1,1,rcon5_3<rcon5_3<rcon5_3<downsampler_3<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
using layer_details3 = add_layer<con_<1, 9, 9, 1, 1, 4, 4>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<55, 5, 5, 1, 1, 2, 2>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<32, 5, 5, 2, 2, 0, 0>,
                       add_layer<relu_,
                       add_layer<bn_<CONV_MODE>,
                       add_layer<con_<16, 5, 5, 2, 2, 0, 0>,
                       input_rgb_image_pyramid
                       <pyramid_down<6>>>>>>>>>>>>>>>>>>>>>;
#pragma endregion 3

#pragma endregion type definitions

typedef std::vector<mmod_rect> out_type;
typedef std::vector<mmod_rect> train_label_type;

#pragma region template

#define train_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
do {\
    std::vector<matrix<__TYPE__>*>& tmp_data = *(static_cast<std::vector<matrix<__TYPE__>*>*>(data));\
    std::vector<matrix<__TYPE__>> in_tmp_data;\
    for (int i = 0; i< tmp_data.size(); i++)\
    {\
        matrix<__TYPE__>& mat = *tmp_data[i];\
        in_tmp_data.push_back(mat);\
    }\
\
    std::vector<train_label_type*>& tmp_label = *(static_cast<std::vector<train_label_type*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type& mat = *static_cast<train_label_type*>(tmp_label[i]);\
        in_tmp_label.push_back(mat);\
    }\
\
    dnn_trainer_train_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#define train_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
do {\
    std::vector<matrix<__TYPE__>*>& tmp_data = *(static_cast<std::vector<matrix<__TYPE__>*>*>(data));\
    std::vector<matrix<__TYPE__>> in_tmp_data;\
    for (int i = 0; i< tmp_data.size(); i++)\
    {\
        matrix<__TYPE__>& mat = *tmp_data[i];\
        in_tmp_data.push_back(mat);\
    }\
\
    std::vector<train_label_type*>& tmp_label = *(static_cast<std::vector<train_label_type*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type& mat = *static_cast<train_label_type*>(tmp_label[i]);\
        in_tmp_label.push_back(mat);\
    }\
\
    dnn_trainer_train_one_step_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#pragma endregion template

DLLEXPORT int loss_mmod_new(const int type, void** net)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net = new net_type();
            break;
        case 1:
            *net = new net_type_1();
            break;
        case 2:
            *net = new net_type_2();
            break;
        case 3:
            *net = new net_type_3();
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int loss_mmod_new2(const int type, mmod_options* option, void** net)
{
    int err = ERR_OK;
    
    mmod_options& o = *option;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net = new net_type(o);
            break;
        case 1:
            *net = new net_type_1(o);
            break;
        case 2:
            *net = new net_type_2(o);
            break;
        case 3:
            *net = new net_type_3(o);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_mmod_operator_matrixs(void* obj, 
                                         const int type,
                                         matrix_element_type element_type,
                                         void* matrix_vector,
                                         int templateRows,
                                         int templateColumns,
                                         size_t batch_size,
                                         std::vector<out_type>** ret)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {       
                    net_type& net = *(static_cast<net_type*>(obj));         
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
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
                }
                break;
            case 1:
                {       
                    net_type_1& net = *(static_cast<net_type_1*>(obj));         
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
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
                }
                break;
            case 2:
                {       
                    net_type_2& net = *(static_cast<net_type_2*>(obj));         
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
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
                }
                break;
            case 3:
                {       
                    net_type_3& net = *(static_cast<net_type_3*>(obj));         
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
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
                }
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_errot_to_error_code(ce, err);
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
        case 1:
            delete (net_type_1*)obj;
            break;
        case 2:
            delete (net_type_2*)obj;
            break;
        case 3:
            delete (net_type_3*)obj;
            break;
    }
}

DLLEXPORT int loss_mmod_deserialize(const char* file_name, const int type, void** ret)
{
    int error = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    net_type* net = new net_type();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    net_type_1* net = new net_type_1();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            case 2:
                {
                    net_type_2* net = new net_type_2();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            case 3:
                {
                    net_type_3* net = new net_type_3();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            default:
                error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_errot_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT int loss_mmod_deserialize_proxy(proxy_deserialize* proxy, const int type, void** ret)
{
    int error = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type* net = new net_type();
                    p >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type_1* net = new net_type_1();
                    p >> (*net);
                    *ret = net;
                }
                break;
            case 2:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type_2* net = new net_type_2();
                    p >> (*net);
                    *ret = net;
                }
                break;
            case 3:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type_3* net = new net_type_3();
                    p >> (*net);
                    *ret = net;
                }
                break;
            default:
                error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_errot_to_error_code(ce, error);
    }

    return error;
}

DLLEXPORT void loss_mmod_serialize(void* obj, const int type, const char* file_name)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
    }
}

DLLEXPORT int loss_mmod_num_layers(const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            return net_type::num_layers;
        case 1:
            return net_type_1::num_layers;
        case 2:
            return net_type_2::num_layers;
        case 3:
            return net_type_3::num_layers;
    }

    return 0;
}

DLLEXPORT int loss_mmod_subnet(void* obj, const int type, void** subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type*>(obj);
                auto sn = net->subnet();
                *subnet = new net_type::subnet_type(sn);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1*>(obj);
                auto sn = net->subnet();
                *subnet = new net_type_1::subnet_type(sn);
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2*>(obj);
                auto sn = net->subnet();
                *subnet = new net_type_2::subnet_type(sn);
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3*>(obj);
                auto sn = net->subnet();
                *subnet = new net_type_3::subnet_type(sn);
            }
            break;
    }

    return 0;
}

DLLEXPORT void loss_mmod_clean(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            ((net_type*)obj)->clean();
            break;
        case 1:
            ((net_type_1*)obj)->clean();
            break;
        case 2:
            ((net_type_2*)obj)->clean();
            break;
        case 3:
            ((net_type_3*)obj)->clean();
            break;
    }
}

DLLEXPORT void loss_mmod_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                net_type& net = *static_cast<net_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
        case 1:
            {
                net_type_1& net = *static_cast<net_type_1*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
        case 2:
            {
                net_type_2& net = *static_cast<net_type_2*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
        case 3:
            {
                net_type_3& net = *static_cast<net_type_3*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
    }
}

DLLEXPORT void loss_mmod_input_layer(void* obj, const int type, void** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                net_type& net = *static_cast<net_type*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 1:
            {
                net_type_1& net = *static_cast<net_type_1*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 2:
            {
                net_type_2& net = *static_cast<net_type_2*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
        case 3:
            {
                net_type_3& net = *static_cast<net_type_3*>(obj);
                auto inputLayer = dlib::input_layer(net);
                *ret = new input_rgb_image_pyramid<pyramid_down<6>>(inputLayer);
            }
            break;
    }
}

#pragma region subnet

DLLEXPORT void loss_mmod_subnet_delete(const int type, void* subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto sb = static_cast<net_type::subnet_type*>(subnet);
                delete sb;
            }
            break;
        case 1:
            {
                auto sb = static_cast<net_type_1::subnet_type*>(subnet);
                delete sb;
            }
            break;
        case 2:
            {
                auto sb = static_cast<net_type_2::subnet_type*>(subnet);
                delete sb;
            }
            break;
        case 3:
            {
                auto sb = static_cast<net_type_3::subnet_type*>(subnet);
                delete sb;
            }
            break;
    }
}

DLLEXPORT const dlib::tensor* loss_mmod_subnet_get_output(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

DLLEXPORT const void* loss_mmod_subnet_get_layer_details(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
                const layer_details& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1::subnet_type*>(subnet);
                const layer_details1& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2::subnet_type*>(subnet);
                const layer_details2& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3::subnet_type*>(subnet);
                const layer_details3& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

#pragma region layer_details

DLLEXPORT void loss_mmod_layer_details_set_num_filters(void* layer, const int type, long num)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto ld = static_cast<layer_details::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 1:
            {
                auto ld = static_cast<layer_details1::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 2:
            {
                auto ld = static_cast<layer_details2::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 3:
            {
                auto ld = static_cast<layer_details3::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
    }
}

#pragma endregion layer_details

#pragma region operator

DLLEXPORT int loss_mmod_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                net_type& net = *(static_cast<net_type*>(obj));
                *stream << net;
            }
            break;
        case 1:
            {
                net_type_1& net = *(static_cast<net_type_1*>(obj));
                *stream << net;
            }
            break;
        case 2:
            {
                net_type_2& net = *(static_cast<net_type_2*>(obj));
                *stream << net;
            }
            break;
        case 3:
            {
                net_type_3& net = *(static_cast<net_type_3*>(obj));
                *stream << net;
            }
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma region dnn_trainer

DLLEXPORT void* dnn_trainer_loss_mmod_new(void* net, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template(net_type, net);
            break;
        case 1:
            dnn_trainer_new_template(net_type_1, net);
            break;
        case 2:
            dnn_trainer_new_template(net_type_2, net);
            break;
        case 3:
            dnn_trainer_new_template(net_type_3, net);
            break;
    }

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_mmod_delete(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_delete_template(net_type, trainer);
            break;
        case 1:
            dnn_trainer_delete_template(net_type_1, trainer);
            break;
        case 2:
            dnn_trainer_delete_template(net_type_2, trainer);
            break;
        case 3:
            dnn_trainer_delete_template(net_type_3, trainer);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_mmod_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_learning_rate_template(net_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_learning_rate_template(net_type_1, trainer, lr);
            break;
        case 2:
            dnn_trainer_set_learning_rate_template(net_type_2, trainer, lr);
            break;
        case 3:
            dnn_trainer_set_learning_rate_template(net_type_3, trainer, lr);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_mmod_get_learning_rate(void* trainer, const int type, double* lr)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_get_learning_rate_template(net_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_get_learning_rate_template(net_type_1, trainer, lr);
            break;
        case 2:
            dnn_trainer_get_learning_rate_template(net_type_2, trainer, lr);
            break;
        case 3:
            dnn_trainer_get_learning_rate_template(net_type_3, trainer, lr);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT void dnn_trainer_loss_mmod_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_min_learning_rate_template(net_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_min_learning_rate_template(net_type_1, trainer, lr);
            break;
        case 2:
            dnn_trainer_set_min_learning_rate_template(net_type_2, trainer, lr);
            break;
        case 3:
            dnn_trainer_set_min_learning_rate_template(net_type_3, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_mmod_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_mini_batch_size_template(net_type, trainer, size);
            break;
        case 1:
            dnn_trainer_set_mini_batch_size_template(net_type_1, trainer, size);
            break;
        case 2:
            dnn_trainer_set_mini_batch_size_template(net_type_2, trainer, size);
            break;
        case 3:
            dnn_trainer_set_mini_batch_size_template(net_type_3, trainer, size);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_mmod_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_be_verbose_template(net_type, trainer);
            break;
        case 1:
            dnn_trainer_be_verbose_template(net_type_1, trainer);
            break;
        case 2:
            dnn_trainer_be_verbose_template(net_type_2, trainer);
            break;
        case 3:
            dnn_trainer_be_verbose_template(net_type_3, trainer);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_mmod_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_synchronization_file_template(net_type, trainer, filename, std::chrono::seconds(second));
            break;
        case 1:
            dnn_trainer_set_synchronization_file_template(net_type_1, trainer, filename, std::chrono::seconds(second));
            break;
        case 2:
            dnn_trainer_set_synchronization_file_template(net_type_2, trainer, filename, std::chrono::seconds(second));
            break;
        case 3:
            dnn_trainer_set_synchronization_file_template(net_type_3, trainer, filename, std::chrono::seconds(second));
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_mmod_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_iterations_without_progress_threshold(net_type, trainer, thresh);
            break;
        case 1:
            dnn_trainer_set_iterations_without_progress_threshold(net_type_1, trainer, thresh);
            break;
        case 2:
            dnn_trainer_set_iterations_without_progress_threshold(net_type_2, trainer, thresh);
            break;
        case 3:
            dnn_trainer_set_iterations_without_progress_threshold(net_type_3, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_mmod_train(void* trainer,
                                          const int type,
                                          matrix_element_type data_element_type,
                                          void* data,
                                          matrix_element_type label_element_type,
                                          void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;
    
    if (label_element_type != matrix_element_type::UInt32)
        return ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;

    switch(data_element_type)
    {
        case matrix_element_type::RgbPixel:
            switch(type)
            {
                case 0:
                    train_template(net_type, trainer, rgb_pixel, data, labels);
                    break;
                case 1:
                    train_template(net_type_1, trainer, rgb_pixel, data, labels);
                    break;
                case 2:
                    train_template(net_type_2, trainer, rgb_pixel, data, labels);
                    break;
                case 3:
                    train_template(net_type_3, trainer, rgb_pixel, data, labels);
                    break;
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

DLLEXPORT int dnn_trainer_loss_mmod_train_one_step(void* trainer,
                                                   const int type,
                                                   matrix_element_type data_element_type,
                                                   void* data,
                                                   matrix_element_type label_element_type,
                                                   void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;
    
    if (label_element_type != matrix_element_type::UInt32)
        return ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT;

    switch(data_element_type)
    {
        case matrix_element_type::RgbPixel:
            switch(type)
            {
                case 0:
                    train_one_step_template(net_type, trainer, rgb_pixel, data, labels);
                    break;
                case 1:
                    train_one_step_template(net_type_1, trainer, rgb_pixel, data, labels);
                    break;
                case 2:
                    train_one_step_template(net_type_2, trainer, rgb_pixel, data, labels);
                    break;
                case 3:
                    train_one_step_template(net_type_3, trainer, rgb_pixel, data, labels);
                    break;
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

#pragma endregion dnn_trainer

#endif