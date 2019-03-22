#ifndef _CPP_LOSS_MULTICLASS_LOG_H_
#define _CPP_LOSS_MULTICLASS_LOG_H_

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
// Inception layer has some different convolutions inside.  Here we define
// blocks as convolutions with different kernel size that we will use in
// inception layer block.
template <typename SUBNET> using block_a1 = relu<con<10,1,1,1,1,SUBNET>>;
template <typename SUBNET> using block_a2 = relu<con<10,3,3,1,1,relu<con<16,1,1,1,1,SUBNET>>>>;
template <typename SUBNET> using block_a3 = relu<con<10,5,5,1,1,relu<con<16,1,1,1,1,SUBNET>>>>;
template <typename SUBNET> using block_a4 = relu<con<10,1,1,1,1,max_pool<3,3,1,1,SUBNET>>>;

// Here is inception layer definition. It uses different blocks to process input
// and returns combined output.  Dlib includes a number of these inceptionN
// layer types which are themselves created using concat layers.  
template <typename SUBNET> using incept_a = inception4<block_a1,block_a2,block_a3,block_a4, SUBNET>;

// Network can have inception layers of different structure.  It will work
// properly so long as all the sub-blocks inside a particular inception block
// output tensors with the same number of rows and columns.
template <typename SUBNET> using block_b1 = relu<con<4,1,1,1,1,SUBNET>>;
template <typename SUBNET> using block_b2 = relu<con<4,3,3,1,1,SUBNET>>;
template <typename SUBNET> using block_b3 = relu<con<4,1,1,1,1,max_pool<3,3,1,1,SUBNET>>>;
template <typename SUBNET> using incept_b = inception3<block_b1,block_b2,block_b3,SUBNET>;

// Now we can define a simple network for classifying MNIST digits.  We will
// train and test this network in the code below.
using net_type = loss_multiclass_log<fc<10,
                                     relu<fc<32,
                                     max_pool<2,2,2,2,incept_b<
                                     max_pool<2,2,2,2,incept_a<
                                     input<matrix<unsigned char>>
                                     >>>>>>>>;

using layer_details = add_layer<fc_<10, FC_HAS_BIAS>,
                      add_layer<relu_,
                      add_layer<fc_<32, FC_HAS_BIAS>,
                      add_layer<max_pool_<2, 2, 2, 2, 0, 0>,
                      add_layer<concat_<itag1, itag2, itag3>,
                      add_tag_layer<1001,
                      add_layer<relu_,
                      add_layer<con_<4, 1, 1, 1, 1, 0, 0>,
                      add_skip_layer<itag0,
                      add_tag_layer<1002,
                      add_layer<relu_,
                      add_layer<con_<4, 3, 3, 1, 1, 1, 1>,
                      add_skip_layer<itag0,
                      add_tag_layer<1003,
                      add_layer<relu_,
                      add_layer<con_<4, 1, 1, 1, 1, 0, 0>,
                      add_layer<max_pool_<3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1000,
                      add_layer<max_pool_<2, 2, 2, 2, 0, 0>,
                      add_layer<concat_<itag1, itag2, itag3, itag4>,
                      add_tag_layer<1001,
                      add_layer<relu_,
                      add_layer<con_<10, 1, 1, 1, 1, 0, 0>,
                      add_skip_layer<itag0,
                      add_tag_layer<1002,
                      add_layer<relu_,
                      add_layer<con_<10, 3, 3, 1, 1, 1, 1>,
                      add_layer<relu_,
                      add_layer<con_<16, 1, 1, 1, 1, 0, 0>,
                      add_skip_layer<itag0,
                      add_tag_layer<1003,
                      add_layer<relu_,
                      add_layer<con_<10, 5, 5, 1, 1, 2, 2>,
                      add_layer<relu_,
                      add_layer<con_<16, 1, 1, 1, 1, 0, 0>,
                      add_skip_layer<itag0,
                      add_tag_layer<1004,
                      add_layer<relu_,
                      add_layer<con_<10, 1, 1, 1, 1, 0, 0>,
                      add_layer<max_pool_<3, 3, 1, 1, 1, 1>,
                      add_tag_layer<1000, input<matrix<unsigned char, 0, 0>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>;
#pragma endregion type definitions

typedef unsigned long out_type;
// typedef dlib::matrix<dlib::rgb_pixel> in_type;
typedef unsigned long  train_label_type;

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
    std::vector<train_label_type>& tmp_label = *(static_cast<std::vector<train_label_type>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
        in_tmp_label.push_back(tmp_label[i]);\
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
    std::vector<train_label_type>& tmp_label = *(static_cast<std::vector<train_label_type>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
        in_tmp_label.push_back(tmp_label[i]);\
\
    dnn_trainer_train_one_step_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#pragma endregion template

DLLEXPORT int loss_multiclass_log_new(const int type, void** net)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            *net =  new net_type();
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

// NOTE
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!
DLLEXPORT int loss_multiclass_log_operator_matrixs(void* obj, 
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

DLLEXPORT void loss_multiclass_log_delete(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            delete (net_type*)obj;
            break;
    }
}

DLLEXPORT int loss_multiclass_log_deserialize(const char* file_name, const int type, void** ret)
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

DLLEXPORT int loss_multiclass_log_deserialize_proxy(proxy_deserialize* proxy, const int type, void** ret)
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

DLLEXPORT void loss_multiclass_log_serialize(void* obj, const int type, const char* file_name)
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
    }
}

DLLEXPORT int loss_multiclass_log_num_layers(const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            return net_type::num_layers;
    }

    return 0;
}

DLLEXPORT int loss_multiclass_log_subnet(void* obj, const int type, void** subnet)
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
    }

    return 0;
}

DLLEXPORT void loss_multiclass_log_clean(void* obj, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            ((net_type*)obj)->clean();
            break;
    }
}

DLLEXPORT void loss_multiclass_log_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type*>(obj);
                auto rp = dlib::input_tensor_to_output_tensor(net, *p);
                *ret = new dlib::dpoint(rp);
            }
            break;
    }
}

#pragma region subnet

DLLEXPORT void loss_multiclass_log_subnet_delete(const int type, void* subnet)
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
    }
}

DLLEXPORT const dlib::tensor* loss_multiclass_log_subnet_get_output(void* subnet, const int type, int* ret)
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
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

DLLEXPORT const void* loss_multiclass_log_subnet_get_layer_details(void* subnet, const int type, int* ret)
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
    }

    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    return nullptr;
}

#pragma endregion subnet

#pragma region layer_details

DLLEXPORT void loss_multiclass_log_layer_details_set_num_filters(void* layer, const int type, long num)
{
    // Check type argument and cast to the proper type
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

DLLEXPORT int loss_multiclass_log_operator_left_shift(void* obj, const int type, std::ostringstream* stream)
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
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion operator

#pragma region dnn_trainer

DLLEXPORT void* dnn_trainer_loss_multiclass_log_new(void* net, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template(net_type, net);
            break;
    }

    return nullptr;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_delete(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_delete_template(net_type, trainer);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_get_learning_rate(void* trainer, const int type, double* lr)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_get_learning_rate_template(net_type, trainer, lr);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_min_learning_rate_template(net_type, trainer, lr);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_mini_batch_size_template(net_type, trainer, size);
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_be_verbose_template(net_type, trainer);
            break;
    }
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_synchronization_file_template(net_type, trainer, filename, std::chrono::seconds(second));
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0: 
            dnn_trainer_set_iterations_without_progress_threshold(net_type, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_train(void* trainer,
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
        case matrix_element_type::UInt8:
            switch(type)
            {
                case 0:
                    train_template(net_type, trainer, uint8_t, data, labels);
                    break;
            }
            break;
        case matrix_element_type::UInt16:
        case matrix_element_type::UInt32:
        case matrix_element_type::Int8:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Float:
        case matrix_element_type::Double:
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_multiclass_log_train_one_step(void* trainer,
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
        case matrix_element_type::UInt8:
            switch(type)
            {
                case 0:
                    train_one_step_template(net_type, trainer, uint8_t, data, labels);
                    break;
            }
            break;
        case matrix_element_type::UInt16:
        case matrix_element_type::UInt32:
        case matrix_element_type::Int8:
        case matrix_element_type::Int16:
        case matrix_element_type::Int32:
        case matrix_element_type::Float:
        case matrix_element_type::Double:
        case matrix_element_type::RgbPixel:
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