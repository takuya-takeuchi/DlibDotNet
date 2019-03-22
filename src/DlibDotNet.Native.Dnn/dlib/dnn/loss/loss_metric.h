#ifndef _CPP_LOSS_METRIC_H_
#define _CPP_LOSS_METRIC_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

#include "../trainer.h"

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
using layer_details = add_layer<fc_<128, FC_NO_BIAS>,
                      add_layer<avg_pool_<0, 0, 1, 1, 0, 0>,
                      add_layer<relu_,
                      add_layer<add_prev_<tag2>,
                      add_layer<avg_pool_<2, 2, 2, 2, 0, 0>,
                      add_skip_layer<tag1,
                      add_tag_layer<2,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 2, 2, 0, 0>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag2>,
                      add_layer<avg_pool_<2, 2, 2, 2, 0, 0>,
                      add_skip_layer<tag1,
                      add_tag_layer<2,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<256, 3, 3, 2, 2, 0, 0>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag2>,
                      add_layer<avg_pool_<2, 2, 2, 2, 0, 0>,
                      add_skip_layer<tag1,
                      add_tag_layer<2,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<128, 3, 3, 2, 2, 0, 0>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag2>,
                      add_layer<avg_pool_<2, 2, 2, 2, 0, 0>,
                      add_skip_layer<tag1,
                      add_tag_layer<2,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<64, 3, 3, 2, 2, 0, 0>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<relu_,
                      add_layer<add_prev_<tag1>,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_<32, 3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1,
                      add_layer<max_pool_<3, 3, 2, 2, 0, 0>,
                      add_layer<relu_,
                      add_layer<affine_,
                      add_layer<con_
                      <32, 7, 7, 2, 2, 0, 0>, input_rgb_image_sized
                      <150>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>;
#pragma endregion type definitions

typedef matrix<float,0,1> out_type;
typedef unsigned long train_label_type;

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

DLLEXPORT int loss_metric_new(const int type, void** net)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net =  new anet_type();
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_metric_operator_matrixs(void* obj,
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

DLLEXPORT void loss_metric_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            delete (anet_type*)obj;
            break;
    }
}

DLLEXPORT int loss_metric_deserialize(const char* file_name, const int type, void** ret)
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

DLLEXPORT int loss_metric_deserialize_proxy(proxy_deserialize* proxy, const int type, void** ret)
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

DLLEXPORT void loss_metric_serialize(void* obj, const int type, const char* file_name)
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
    }
}

DLLEXPORT int loss_metric_num_layers(const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            return anet_type::num_layers;
    }

    return 0;
}

DLLEXPORT int loss_metric_subnet(void* obj, const int type, void** subnet)
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
    }

    return 0;
}

DLLEXPORT void loss_metric_clean(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            ((anet_type*)obj)->clean();
            break;
    }
}

DLLEXPORT void loss_metric_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
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
    }
}

#pragma region subnet

DLLEXPORT void loss_metric_subnet_delete(const int type, void* subnet)
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
    }
}

DLLEXPORT const dlib::tensor* loss_metric_subnet_get_output(void* subnet, const int type, int* ret)
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
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

DLLEXPORT const void* loss_metric_subnet_get_layer_details(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<anet_type::subnet_type*>(subnet);
                const layer_details& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

#pragma region layer_details

DLLEXPORT void loss_metric_layer_details_set_num_filters(void* layer, const int type, long num)
{
    // // Check type argument and cast to the proper type
    // switch(type)
    // {
    //     case 0:
    //         {
    //             auto ld = static_cast<layer_details::layer_details_type*>(layer);
    //             ld->set_num_filters(num);
    //         }
    //         break;
    // }
}

#pragma endregion layer_details

#pragma region operator

DLLEXPORT int loss_metric_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
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
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma region dnn_trainer

DLLEXPORT void* dnn_trainer_loss_metric_new(void* net, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template(anet_type, net);
            break;
    }

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_metric_delete(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_delete_template(anet_type, trainer);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_learning_rate_template(anet_type, trainer, lr);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_metric_get_learning_rate(void* trainer, const int type, double* lr)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_get_learning_rate_template(anet_type, trainer, lr);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT void dnn_trainer_loss_metric_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_min_learning_rate_template(anet_type, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_mini_batch_size_template(anet_type, trainer, size);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_be_verbose_template(anet_type, trainer);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_metric_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_synchronization_file_template(anet_type, trainer, filename, std::chrono::seconds(second));
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_metric_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_iterations_without_progress_threshold(anet_type, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_metric_train(void* trainer,
                                            const int type,
                                            matrix_element_type data_element_type,
                                            void* data,
                                            matrix_element_type label_element_type,
                                            void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;
    
    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
        
    switch(data_element_type)
    {
        case matrix_element_type::RgbPixel:
            switch(type)
            {
                case 0:
                    train_template(anet_type, trainer, rgb_pixel, data, labels);
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

DLLEXPORT int dnn_trainer_loss_metric_train_one_step(void* trainer,
                                                     const int type,
                                                     matrix_element_type data_element_type,
                                                     void* data,
                                                     matrix_element_type label_element_type,
                                                     void* labels)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;
    
    if (label_element_type != matrix_element_type::UInt32)
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
        
    switch(data_element_type)
    {
        case matrix_element_type::RgbPixel:
            switch(type)
            {
                case 0:
                    train_one_step_template(anet_type, trainer, rgb_pixel, data, labels);
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