#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_COMMON_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_COMMON_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

using namespace dlib;
using namespace std;

#pragma region template

#define loss_multiclass_log_per_pixel_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(anet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 1:\
        {\
            __FUNC__(net_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 2:\
        {\
            __FUNC__(ubnet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 3:\
        {\
            __FUNC__(uanet_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    default:\
        error = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
        break;\
}

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
    std::vector<train_label_type*>& tmp_label = *(static_cast<std::vector<train_label_type*>*>(labels));\
    std::vector<train_label_type> in_tmp_label;\
    for (int i = 0; i< tmp_label.size(); i++)\
    {\
        train_label_type& mat = *static_cast<train_label_type*>(tmp_label[i]);\
        in_tmp_label.push_back(mat);\
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

#define operator_matrixs_template(element_type, __NET_TYPE__, obj, matrix_vector, batch_size, ret) \
do {\
    __NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
    switch(element_type)\
    {\
        case matrix_element_type::UInt8:\
            operator_template(net, uint8_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::UInt16:\
            operator_template(net, uint16_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::UInt32:\
            operator_template(net, uint32_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int8:\
            operator_template(net, int8_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int16:\
            operator_template(net, int16_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Int32:\
            operator_template(net, int32_t, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Float:\
            operator_template(net, float, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::Double:\
            operator_template(net, double, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::RgbPixel:\
            operator_template(net, rgb_pixel, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::HsiPixel:\
            operator_template(net, hsi_pixel, matrix_vector, batch_size, ret);\
            break;\
        case matrix_element_type::RgbAlphaPixel:\
            operator_template(net, rgb_alpha_pixel, matrix_vector, batch_size, ret);\
            break;\
        default:\
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
            break;\
    }\
} while (0)

#pragma endregion template

#endif