#ifndef _CPP_LOSS_MMOD_COMMON_H_
#define _CPP_LOSS_MMOD_COMMON_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

using namespace dlib;
using namespace std;

#pragma region template

#define loss_mmod_template(type, error, __FUNC__, ...) \
switch(type)\
{\
    case 0:\
        {\
            __FUNC__(net_type, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 1:\
        {\
            __FUNC__(net_type_1, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 2:\
        {\
            __FUNC__(net_type_2, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
        }\
        break;\
    case 3:\
        {\
            __FUNC__(net_type_3, matrix_element_type::RgbPixel, rgb_pixel, error, __VA_ARGS__);\
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

#define loss_mmod_new2_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto& o = *option;\
*net = new __NET_TYPE__(o);

#pragma endregion template

#endif