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

#pragma region 1
template <long num_filters, typename SUBNET> using con5d_1 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_1  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_1  = relu<affine<con5d_1<32, relu<affine<con5d_1<32, relu<affine<con5d_1<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_1  = relu<affine<con5_1<55,SUBNET>>>;
using net_type_1 = loss_mmod<con<1,9,9,1,1,rcon5_1<rcon5_1<rcon5_1<downsampler_1<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
#pragma endregion 1

#pragma endregion type definitions

typedef std::vector<mmod_rect> out_type;
typedef std::vector<mmod_rect> train_label_type;

#pragma region template

#define train_template(trainer, __TYPE__, data, labels) \
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
    dnn_trainer_train_template(trainer, in_tmp_data, in_tmp_label);\
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
    }
}

DLLEXPORT void* loss_mmod_deserialize(const char* file_name, const int type)
{
    // Check type argument and cast to the proper type    
    switch(type)
    {
        case 0:
            {
                net_type* net = new net_type();
                dlib::deserialize(file_name) >> (*net);
                return net;
            }
            break;
        case 1:
            {
                net_type_1* net = new net_type_1();
                dlib::deserialize(file_name) >> (*net);
                return net;
            }
            break;
        default:
            return nullptr;
    }
}

DLLEXPORT void* loss_mmod_deserialize_proxy(proxy_deserialize* proxy, const int type)
{
    // Check type argument and cast to the proper type    
    switch(type)
    {
        case 0:
            {
                proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                net_type* net = new net_type();
                p >> (*net);
                return net;
            }
            break;
        case 1:
            {
                proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                net_type_1* net = new net_type_1();
                p >> (*net);
                return net;
            }
            break;
        default:
            return nullptr;
    }
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
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

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
    }
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
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion dnn_trainer

#endif