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
using net_type = loss_multiclass_log<
        fc<10,
        relu<fc<32,
        max_pool<2,2,2,2,incept_b<
        max_pool<2,2,2,2,incept_a<
        input<matrix<unsigned char>>
        >>>>>>>>;
#pragma endregion type definitions

typedef unsigned long out_type;
// typedef dlib::matrix<dlib::rgb_pixel> in_type;
typedef unsigned long  train_label_type;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define operator_template(net, matrix, batch_size, ret) \
do {\
    std::vector<dlib::matrix<ELEMENT>*>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(matrix));\
    std::vector<dlib::matrix<ELEMENT>> in_tmp;\
    for (int i = 0; i< tmp.size(); i++)\
    {\
        dlib::matrix<ELEMENT>& mat = *tmp[i];\
        in_tmp.push_back(mat);\
    }\
\
    std::vector<out_type> dets = net(in_tmp, batch_size);\
    *ret = new std::vector<out_type>(dets);\
} while (0)

#define train_template(trainer, data, labels) \
do {\
    std::vector<matrix<ELEMENT>*>& tmp_data = *(static_cast<std::vector<matrix<ELEMENT>*>*>(data));\
    std::vector<matrix<ELEMENT>> in_tmp_data;\
    for (int i = 0; i< tmp_data.size(); i++)\
    {\
        matrix<ELEMENT>& mat = *tmp_data[i];\
        in_tmp_data.push_back(mat);\
    }\
\
    std::vector<train_label_type>& tmp_label = *(static_cast<std::vector<train_label_type>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
        in_tmp_label.push_back(tmp_label[i]);\
\
    dnn_trainer_train_template(trainer, in_tmp_data, in_tmp_label);\
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
                                                   void* matrix, 
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
                    case matrix_element_type::UInt8:
                        #define ELEMENT uint8_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::UInt16:
                        #define ELEMENT uint16_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::UInt32:
                        #define ELEMENT uint32_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::Int8:
                        #define ELEMENT int8_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::Int16:
                        #define ELEMENT int16_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::Int32:
                        #define ELEMENT int32_t
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::Float:
                        #define ELEMENT float
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::Double:
                        #define ELEMENT double
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::RgbPixel:
                        #define ELEMENT rgb_pixel
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::HsiPixel:
                        #define ELEMENT hsi_pixel
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    case matrix_element_type::RgbAlphaPixel:
                        #define ELEMENT rgb_alpha_pixel
                        operator_template(net, matrix, batch_size, ret);
                        #undef ELEMENT
                        break;
                    default:
                        err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                        break;
                }
            }
            break;
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

DLLEXPORT void* loss_multiclass_log_deserialize(const char* file_name, const int type)
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
    }

    return nullptr;
}

DLLEXPORT void* loss_multiclass_log_deserialize_proxy(proxy_deserialize* proxy, const int type)
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
        default:
            return nullptr;
    }
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

#pragma endregion subnet

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
            #define NET_TYPE net_type
            dnn_trainer_new_template(net);
            #undef NET_TYPE
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
            #define NET_TYPE net_type
            dnn_trainer_delete_template(trainer);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE net_type
            dnn_trainer_set_learning_rate_template(trainer, lr);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE net_type
            dnn_trainer_set_min_learning_rate_template(trainer, lr);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE net_type
            dnn_trainer_set_mini_batch_size_template(trainer, size);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_multiclass_log_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE net_type
            dnn_trainer_be_verbose_template(trainer);
            #undef NET_TYPE
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
            #define NET_TYPE net_type
            dnn_trainer_set_synchronization_file_template(trainer, filename, std::chrono::seconds(second));
            #undef NET_TYPE
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
            #define ELEMENT uint8_t
            switch(type)
            {
                case 0:
                    #define NET_TYPE net_type
                    train_template(trainer, data, labels);
                    #undef NET_TYPE
                    break;
            }
            #undef ELEMENT
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