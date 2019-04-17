#ifndef _CPP_LOSS_MMOD_H_
#define _CPP_LOSS_MMOD_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../trainer.h"
#include "loss_mmod_defines.h"
#include "../layers/layers.h"
#include "../../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, sub_template) \
do {\
    std::vector<matrix<__TYPE__>*>& tmp_data = *(static_cast<std::vector<matrix<__TYPE__>*>*>(data));\
    std::vector<matrix<__TYPE__>> in_tmp_data;\
    for (int i = 0; i< tmp_data.size(); i++)\
    {\
        matrix<__TYPE__>& mat = *tmp_data[i];\
        in_tmp_data.push_back(mat);\
    }\
\
    std::vector<train_label_type_pointer*>& tmp_label = *(static_cast<std::vector<train_label_type_pointer*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type_pointer& v = *(tmp_label[i]);\
        train_label_type tmp_v;\
        for (int j = 0; j < v.size(); j++)\
        {\
            mmod_rect& r = *(v[j]);\
            tmp_v.push_back(r);\
        }\
        in_tmp_label.push_back(tmp_v);\
    }\
\
    sub_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label);\
} while (0)

#define test_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_test_one_step_template);\

#define train_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_template);\

#define train_one_step_template(__NET_TYPE__, trainer, __TYPE__, data, labels) \
train_test_template_sub(__NET_TYPE__, trainer, __TYPE__, data, labels, dnn_trainer_train_one_step_template);\

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
        // case 1:
        //     *net = new net_type_1(o);
        //     break;
        // case 2:
        //     *net = new net_type_2(o);
        //     break;
        // case 3:
        //     *net = new net_type_3(o);
        //     break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
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
        case 2:
            delete (net_type_2*)obj;
            break;
        case 3:
            delete (net_type_3*)obj;
            break;
    }
}

DLLEXPORT int loss_mmod_clone(void* obj, const int src_type, const int dst_type, void** new_net)
{
    int err = ERR_OK;

    if (src_type != dst_type)
        return ERR_DNN_NOT_CLONEABLE_AS_SPECIFIED_NETWORKTYPE;

    // Check type argument and cast to the proper type
    switch(src_type)
    {
        case 0:
            // Only 0 to 0
            {                
                net_type& net = *static_cast<net_type*>(obj);\
                *new_net = new net_type(net);\
            }
            break;
        case 1:
            // Only 1 to 1
            {                
                net_type_1& net = *static_cast<net_type_1*>(obj);\
                *new_net = new net_type_1(net);\
            }
            break;
        case 2:
            // Only 2 to 2
            {                
                net_type_2& net = *static_cast<net_type_2*>(obj);\
                *new_net = new net_type_2(net);\
            }
            break;
        case 3:
            // Only 3 to 3
            {                
                net_type_3& net = *static_cast<net_type_3*>(obj);\
                *new_net = new net_type_3(net);\
            }
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
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int loss_mmod_deserialize(const char* file_name,
                                    const int type,
                                    void** ret,
                                    std::string** error_message)
{
    int err = ERR_OK;

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
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int loss_mmod_deserialize_proxy(proxy_deserialize* proxy,
                                          const int type,
                                          void** ret,
                                          std::string** error_message)
{
    int err = ERR_OK;

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
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int loss_mmod_serialize(void* obj,
                                  const int type,
                                  const char* file_name,
                                  std::string** error_message)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
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
            default:
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return err;
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
                net_type::subnet_type& sn = net->subnet();
                *subnet = &sn;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1*>(obj);
                net_type_1::subnet_type& sn = net->subnet();
                *subnet = &sn;
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2*>(obj);
                net_type_2::subnet_type& sn = net->subnet();
                *subnet = &sn;
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3*>(obj);
                net_type_3::subnet_type& sn = net->subnet();
                *subnet = &sn;
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

DLLEXPORT void* loss_mmod_subnet_get_layer_details(void* subnet, const int type, int* ret)
{
    // Check type argument and cast to the proper type
    *ret = ERR_OK;

    switch(type)
    {
        case 0:
            {
                auto net = static_cast<net_type::subnet_type*>(subnet);
                net_type::subnet_type::layer_details_type& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 1:
            {
                auto net = static_cast<net_type_1::subnet_type*>(subnet);
                net_type_1::subnet_type::layer_details_type& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 2:
            {
                auto net = static_cast<net_type_2::subnet_type*>(subnet);
                net_type_2::subnet_type::layer_details_type& layer_details = net->layer_details();
                return &layer_details;
            }
            break;
        case 3:
            {
                auto net = static_cast<net_type_3::subnet_type*>(subnet);
                net_type_3::subnet_type::layer_details_type& layer_details = net->layer_details();
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
                auto ld = static_cast<net_type::subnet_type::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 1:
            {
                auto ld = static_cast<net_type_1::subnet_type::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 2:
            {
                auto ld = static_cast<net_type_2::subnet_type::layer_details_type*>(layer);
                ld->set_num_filters(num);
            }
            break;
        case 3:
            {
                auto ld = static_cast<net_type_3::subnet_type::layer_details_type*>(layer);
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

DLLEXPORT void* dnn_trainer_loss_mmod_new_sgd(void* net, const int type, sgd* sgd)
{
    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_new_template2(net_type, net, *sgd);
            break;
        case 1:
            dnn_trainer_new_template2(net_type_1, net, *sgd);
            break;
        case 2:
            dnn_trainer_new_template2(net_type_2, net, *sgd);
            break;
        case 3:
            dnn_trainer_new_template2(net_type_3, net, *sgd);
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

DLLEXPORT int dnn_trainer_loss_mmod_set_test_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_set_test_iterations_without_progress_threshold(net_type, trainer, thresh);
            break;
        case 1:
            dnn_trainer_set_test_iterations_without_progress_threshold(net_type_1, trainer, thresh);
            break;
        case 2:
            dnn_trainer_set_test_iterations_without_progress_threshold(net_type_2, trainer, thresh);
            break;
        case 3:
            dnn_trainer_set_test_iterations_without_progress_threshold(net_type_3, trainer, thresh);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_mmod_test_one_step(void* trainer,
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

    try
    {
        switch(data_element_type)
        {
            case matrix_element_type::RgbPixel:
                switch(type)
                {
                    case 0:
                        test_one_step_template(net_type, trainer, rgb_pixel, data, labels);
                        break;
                    case 1:
                        test_one_step_template(net_type_1, trainer, rgb_pixel, data, labels);
                        break;
                    case 2:
                        test_one_step_template(net_type_2, trainer, rgb_pixel, data, labels);
                        break;
                    case 3:
                        test_one_step_template(net_type_3, trainer, rgb_pixel, data, labels);
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
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
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
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
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
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
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
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;

    try
    {
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
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_mmod_get_net(void* trainer,
                                            const int type,
                                            void** ret)
{
    // Check type argument and cast to the proper type
    int err = ERR_OK;

    try
    {
        switch(type)
        {
            case 0:
                dnn_trainer_get_net_template(net_type, trainer, ret);
                break;
            case 1:
                dnn_trainer_get_net_template(net_type_1, trainer, ret);
                break;
            case 2:
                dnn_trainer_get_net_template(net_type_2, trainer, ret);
                break;
            case 3:
                dnn_trainer_get_net_template(net_type_3, trainer, ret);
                break;
            default:
                err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
                break;
        }
    }
    catch(std::exception)
    {
        err = ERR_DNN_PROPAGATE_EXCEPTION;
    }

    return err;
}

DLLEXPORT int dnn_trainer_loss_mmod_operator_left_shift(void* trainer, const int type, std::ostringstream* stream)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            dnn_trainer_operator_left_shift_template(net_type, trainer, stream);
            break;
        case 1:
            dnn_trainer_operator_left_shift_template(net_type_1, trainer, stream);
            break;
        case 2:
            dnn_trainer_operator_left_shift_template(net_type_2, trainer, stream);
            break;
        case 3:
            dnn_trainer_operator_left_shift_template(net_type_3, trainer, stream);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion dnn_trainer

#pragma region layers

DLLEXPORT int set_all_bn_running_stats_window_sizes_loss_mmod(void* obj, const int type, unsigned long new_window_size)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    switch(type)
    {
        case 0:
            set_all_bn_running_stats_window_sizes_template(net_type, obj, new_window_size);
            break;
        case 1:
            set_all_bn_running_stats_window_sizes_template(net_type_1, obj, new_window_size);
            break;
        case 2:
            set_all_bn_running_stats_window_sizes_template(net_type_2, obj, new_window_size);
            break;
        case 3:
            set_all_bn_running_stats_window_sizes_template(net_type_3, obj, new_window_size);
            break;
        default:
            err = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
            break;
    }

    return err;
}

#pragma endregion layers

#endif