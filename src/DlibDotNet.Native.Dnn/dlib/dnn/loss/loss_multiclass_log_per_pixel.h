#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

#include "../trainer.h"

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
constexpr int class_count = 21; // background + 20 classes

#pragma region type definitions
template <int N, template <typename> class BN, int stride, typename SUBNET> 
using block = BN<dlib::con<N,3,3,1,1, dlib::relu<BN<dlib::con<N,3,3,stride,stride,SUBNET>>>>>;

template <int N, template <typename> class BN, int stride, typename SUBNET> 
using blockt = BN<dlib::cont<N,3,3,1,1,dlib::relu<BN<dlib::cont<N,3,3,stride,stride,SUBNET>>>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual = dlib::add_prev1<block<N,BN,1,dlib::tag1<SUBNET>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_down = dlib::add_prev2<dlib::avg_pool<2,2,2,2,dlib::skip1<dlib::tag2<block<N,BN,2,dlib::tag1<SUBNET>>>>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_up = dlib::add_prev2<dlib::cont<N,2,2,2,2,dlib::skip1<dlib::tag2<blockt<N,BN,2,dlib::tag1<SUBNET>>>>>>;

template <int N, typename SUBNET> using res       = dlib::relu<residual<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares      = dlib::relu<residual<block,N,dlib::affine,SUBNET>>;
template <int N, typename SUBNET> using res_down  = dlib::relu<residual_down<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares_down = dlib::relu<residual_down<block,N,dlib::affine,SUBNET>>;
template <int N, typename SUBNET> using res_up    = dlib::relu<residual_up<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares_up   = dlib::relu<residual_up<block,N,dlib::affine,SUBNET>>;

// ----------------------------------------------------------------------------------------

template <typename SUBNET> using res512 = res<512, SUBNET>;
template <typename SUBNET> using res256 = res<256, SUBNET>;
template <typename SUBNET> using res128 = res<128, SUBNET>;
template <typename SUBNET> using res64  = res<64, SUBNET>;
template <typename SUBNET> using ares512 = ares<512, SUBNET>;
template <typename SUBNET> using ares256 = ares<256, SUBNET>;
template <typename SUBNET> using ares128 = ares<128, SUBNET>;
template <typename SUBNET> using ares64  = ares<64, SUBNET>;


template <typename SUBNET> using level1 = dlib::repeat<2,res512,res_down<512,SUBNET>>;
template <typename SUBNET> using level2 = dlib::repeat<2,res256,res_down<256,SUBNET>>;
template <typename SUBNET> using level3 = dlib::repeat<2,res128,res_down<128,SUBNET>>;
template <typename SUBNET> using level4 = dlib::repeat<2,res64,res<64,SUBNET>>;

template <typename SUBNET> using alevel1 = dlib::repeat<2,ares512,ares_down<512,SUBNET>>;
template <typename SUBNET> using alevel2 = dlib::repeat<2,ares256,ares_down<256,SUBNET>>;
template <typename SUBNET> using alevel3 = dlib::repeat<2,ares128,ares_down<128,SUBNET>>;
template <typename SUBNET> using alevel4 = dlib::repeat<2,ares64,ares<64,SUBNET>>;

template <typename SUBNET> using level1t = dlib::repeat<2,res512,res_up<512,SUBNET>>;
template <typename SUBNET> using level2t = dlib::repeat<2,res256,res_up<256,SUBNET>>;
template <typename SUBNET> using level3t = dlib::repeat<2,res128,res_up<128,SUBNET>>;
template <typename SUBNET> using level4t = dlib::repeat<2,res64,res_up<64,SUBNET>>;

template <typename SUBNET> using alevel1t = dlib::repeat<2,ares512,ares_up<512,SUBNET>>;
template <typename SUBNET> using alevel2t = dlib::repeat<2,ares256,ares_up<256,SUBNET>>;
template <typename SUBNET> using alevel3t = dlib::repeat<2,ares128,ares_up<128,SUBNET>>;
template <typename SUBNET> using alevel4t = dlib::repeat<2,ares64,ares_up<64,SUBNET>>;

// ----------------------------------------------------------------------------------------

// training network type
using net_type = dlib::loss_multiclass_log_per_pixel<
                            dlib::cont<class_count,7,7,2,2,
                            level4t<level3t<level2t<level1t<
                            level1<level2<level3<level4<
                            dlib::max_pool<3,3,2,2,dlib::relu<dlib::bn_con<dlib::con<64,7,7,2,2,
                            dlib::input<dlib::matrix<dlib::rgb_pixel>>
                            >>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using anet_type = dlib::loss_multiclass_log_per_pixel<
                            dlib::cont<class_count,7,7,2,2,
                            alevel4t<alevel3t<alevel2t<alevel1t<
                            alevel1<alevel2<alevel3<alevel4<
                            dlib::max_pool<3,3,2,2,dlib::relu<dlib::affine<dlib::con<64,7,7,2,2,
                            dlib::input<dlib::matrix<dlib::rgb_pixel>>
                            >>>>>>>>>>>>>>;
#pragma endregion type definitions

typedef dlib::matrix<uint16_t> out_type;
typedef dlib::matrix<uint16_t> train_label_type;

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

#pragma endregion template

DLLEXPORT int loss_multiclass_log_per_pixel_new(const int type, void** net)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net =  new anet_type();
            break;
        case 1:
            *net =  new net_type();
            break;
    }

    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_multiclass_log_per_pixel_operator_matrixs(void* obj, 
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
                    anet_type& net = *(static_cast<anet_type*>(obj));                
                    switch(element_type)
                    {
                        case matrix_element_type::UInt8:
                            operator_template(net, uint8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt16:
                            operator_template(net, uint16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt32:
                            operator_template(net, uint32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int8:
                            operator_template(net, int8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int16:
                            operator_template(net, int16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int32:
                            operator_template(net, int32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Float:
                            operator_template(net, float, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Double:
                            operator_template(net, double, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::HsiPixel:
                            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbAlphaPixel:
                            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);
                            break;
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
            case 1:
                {
                    net_type& net = *(static_cast<net_type*>(obj));             
                    switch(element_type)
                    {
                        case matrix_element_type::UInt8:
                            operator_template(net, uint8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt16:
                            operator_template(net, uint16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::UInt32:
                            operator_template(net, uint32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int8:
                            operator_template(net, int8_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int16:
                            operator_template(net, int16_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Int32:
                            operator_template(net, int32_t, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Float:
                            operator_template(net, float, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::Double:
                            operator_template(net, double, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbPixel:
                            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::HsiPixel:
                            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);
                            break;
                        case matrix_element_type::RgbAlphaPixel:
                            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);
                            break;
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

DLLEXPORT void loss_multiclass_log_per_pixel_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            delete (anet_type*)obj;
            break;
        case 1:
            delete (net_type*)obj;
            break;
    }
}

DLLEXPORT uint16_t loss_multiclass_log_per_pixel_get_label_to_ignore()
{
    return loss_multiclass_log_per_pixel_::label_to_ignore;
}

DLLEXPORT int loss_multiclass_log_per_pixel_deserialize(const char* file_name, const int type, void** ret)
{
    int error = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    anet_type* net = new anet_type();
                    dlib::deserialize(file_name) >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    net_type* net = new net_type();
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

DLLEXPORT int loss_multiclass_log_per_pixel_deserialize_proxy(proxy_deserialize* proxy, const int type, void** ret)
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
                    anet_type* net = new anet_type();
                    p >> (*net);
                    *ret = net;
                }
                break;
            case 1:
                {
                    proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
                    net_type* net = new net_type();
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

DLLEXPORT void loss_multiclass_log_per_pixel_serialize(void* obj, const int type, const char* file_name)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                dlib::serialize(file_name) << (*net);
            }
            break;
        default:
            break;
    }
}

DLLEXPORT int loss_multiclass_log_per_pixel_num_layers(const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            return anet_type::num_layers;
        case 1:
            return net_type::num_layers;
    }

    return 0;
}

DLLEXPORT int loss_multiclass_log_per_pixel_subnet(void* obj, const int type, void** subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                auto sn = net->subnet();
                *subnet = new anet_type::subnet_type(sn);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                auto sn = net->subnet();
                *subnet = new net_type::subnet_type(sn);
            }
            break;
    }

    return 0;
}

DLLEXPORT void loss_multiclass_log_per_pixel_clean(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            ((anet_type*)obj)->clean();
            break;
        case 1:
            ((net_type*)obj)->clean();
            break;
    }
}

DLLEXPORT void loss_multiclass_log_per_pixel_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
    }
}

#pragma region subnet

DLLEXPORT void loss_multiclass_log_per_pixel_subnet_delete(const int type, void* subnet)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto sb = static_cast<anet_type::subnet_type*>(subnet);
                delete sb;
            }
            break;
        case 1:
            {
                auto sb = static_cast<net_type::subnet_type*>(subnet);
                delete sb;
            }
            break;
    }
}

DLLEXPORT const dlib::tensor* loss_multiclass_log_per_pixel_subnet_get_output(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type::subnet_type*>(subnet);
                const dlib::tensor& tensor = net->get_output();
                return &tensor;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
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

DLLEXPORT int loss_multiclass_log_per_pixel_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                anet_type& anet = *(static_cast<anet_type*>(obj));
                *stream << anet;
            }
            break;
        case 1:
            {
                net_type& anet = *(static_cast<net_type*>(obj));
                *stream << anet;
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

DLLEXPORT void* dnn_trainer_loss_multiclass_log_per_pixel_new(void* net, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template(anet_type, net);
            break;
        case 1:
            dnn_trainer_new_template(net_type, net);
            break;
    }

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_delete(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_delete_template(anet_type, trainer);
            break;
        case 1:
            dnn_trainer_delete_template(net_type, trainer);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_learning_rate_template(anet_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_min_learning_rate_template(anet_type, trainer, lr);
            break;
        case 1:
            dnn_trainer_set_min_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_mini_batch_size_template(anet_type, trainer, size);
            break;
        case 1:
            dnn_trainer_set_mini_batch_size_template(net_type, trainer, size);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_per_pixel_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_be_verbose_template(anet_type, trainer);
            break;
        case 1:
            dnn_trainer_be_verbose_template(net_type, trainer);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0: 
            dnn_trainer_set_synchronization_file_template(anet_type, trainer, filename, std::chrono::seconds(second));
            break;
        case 1:
            dnn_trainer_set_synchronization_file_template(net_type, trainer, filename, std::chrono::seconds(second));
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_per_pixel_train(void* trainer,
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
                    train_template(anet_type, trainer, rgb_pixel, data, labels);
                    break;
                case 1:
                    train_template(net_type, trainer, rgb_pixel, data, labels);
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