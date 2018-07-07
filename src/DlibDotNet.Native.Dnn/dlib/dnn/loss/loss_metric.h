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
#pragma endregion type definitions

typedef matrix<float,0,1> out_type;
typedef unsigned long train_label_type;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define operator_template(net, images, ret) \
do {\
    std::vector<dlib::matrix<ELEMENT>*>& tmp = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(images));\
    std::vector<dlib::matrix<ELEMENT>> in_tmp;\
    for (int i = 0; i< tmp.size(); i++)\
    {\
        dlib::matrix<ELEMENT>& mat = *tmp[i];\
        in_tmp.push_back(mat);\
    }\
\
    std::vector<out_type> dets = net(in_tmp);\
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
DLLEXPORT int loss_metric_operator_matrixs(void* obj, const int type, matrix_element_type element_type, void* matrix, int templateRows, int templateColumns, std::vector<out_type>** ret)
{
    int err = ERR_OK;
    
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {       
                anet_type& net = *(static_cast<anet_type*>(obj));         
                switch(element_type)
                {
                    case matrix_element_type::RgbPixel:
                        #define ELEMENT rgb_pixel
                        operator_template(net, matrix, ret);
                        #undef ELEMENT
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

DLLEXPORT void* loss_metric_deserialize(const char* file_name, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            {
                anet_type* net = new anet_type();
                dlib::deserialize(file_name) >> (*net);
                return net;
            }
            break;
    }

    return nullptr;
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
            #define NET_TYPE anet_type
            dnn_trainer_new_template(net);
            #undef NET_TYPE
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
            #define NET_TYPE anet_type
            dnn_trainer_delete_template(trainer);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_set_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE anet_type
            dnn_trainer_set_learning_rate_template(trainer, lr);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_set_min_learning_rate(void* trainer, const int type, const double lr)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE anet_type
            dnn_trainer_set_min_learning_rate_template(trainer, lr);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_set_mini_batch_size(void* trainer, const int type, const unsigned long size)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE anet_type
            dnn_trainer_set_mini_batch_size_template(trainer, size);
            #undef NET_TYPE
            break;
    }
}

DLLEXPORT void dnn_trainer_loss_metric_be_verbose(void* trainer, const int type)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            #define NET_TYPE anet_type
            dnn_trainer_be_verbose_template(trainer);
            #undef NET_TYPE
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
            #define NET_TYPE anet_type
            dnn_trainer_set_synchronization_file_template(trainer, filename, std::chrono::seconds(second));
            #undef NET_TYPE
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion dnn_trainer

#endif