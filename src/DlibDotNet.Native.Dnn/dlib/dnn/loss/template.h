#ifndef _CPP_LOSS_TEMPLATE_H_
#define _CPP_LOSS_TEMPLATE_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../trainer.h"
#include "../layers/layers.h"
#include "../../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define loss_new_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
*net = new __NET_TYPE__();

#define loss_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
delete (__NET_TYPE__*)obj;

#define loss_operator_matrixs_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        {\
            __NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
            operator_template(net, __ELEMENT_TYPE__, matrix_vector, batch_size, ret);\
        }\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define loss_deserialize_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
__NET_TYPE__* net = new __NET_TYPE__();\
dlib::deserialize(file_name) >> (*net);\
*ret = net;

#define loss_deserialize_proxy_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);\
__NET_TYPE__* net = new __NET_TYPE__();\
p >> (*net);\
*ret = net;

#define loss_serialize_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
dlib::serialize(file_name) << (*net);

#define loss_num_layers_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
return __NET_TYPE__::num_layers;

#define loss_layer_details_set_num_filters(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto ld = static_cast<__NET_TYPE__::subnet_type::layer_details_type*>(layer);\
ld->set_num_filters(num);

#define loss_subnet_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
__NET_TYPE__::subnet_type& sn = net->subnet();\
*subnet = &sn;

#define loss_clean_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
((__NET_TYPE__*)obj)->clean();

#define loss_input_tensor_to_output_tensor_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__*>(obj);\
auto rp = dlib::input_tensor_to_output_tensor(net, *p);\
*ret = new dlib::dpoint(rp);

#define loss_net_to_xml_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
std::string str(filename);\
auto& net = *static_cast<__NET_TYPE__*>(obj);\
dlib::net_to_xml(net, str);

#define loss_operator_left_shift_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
__NET_TYPE__& net = *(static_cast<__NET_TYPE__*>(obj));\
*stream << net;

#define dnn_trainer_loss_new_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_new_template(__NET_TYPE__, net);

#define dnn_trainer_loss_new_sgd_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_new_template2(__NET_TYPE__, net, *sgd);

#define dnn_trainer_loss_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_delete_template(__NET_TYPE__, trainer);

#define dnn_trainer_loss_set_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_get_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_get_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_set_min_learning_rate_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_min_learning_rate_template(__NET_TYPE__, trainer, lr);

#define dnn_trainer_loss_set_mini_batch_size_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_mini_batch_size_template(__NET_TYPE__, trainer, size);

#define dnn_trainer_loss_be_verbose_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_be_verbose_template(__NET_TYPE__, trainer);

#define dnn_trainer_loss_set_synchronization_file_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_synchronization_file_template(__NET_TYPE__, trainer, filename, std::chrono::seconds(second));

#define dnn_trainer_loss_set_iterations_without_progress_threshold_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_iterations_without_progress_threshold(__NET_TYPE__, trainer, thresh);

#define dnn_trainer_loss_set_test_iterations_without_progress_threshold_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_set_test_iterations_without_progress_threshold(__NET_TYPE__, trainer, thresh);

#define dnn_trainer_loss_test_one_step_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        test_one_step_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_train_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        train_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_train_one_step_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
switch(data_element_type)\
{\
    case __ELEMENT_TYPENAME__:\
        train_one_step_template(__NET_TYPE__, trainer, __ELEMENT_TYPE__, data, labels);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}

#define dnn_trainer_loss_get_net_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_get_net_template(__NET_TYPE__, trainer, ret);

#define dnn_trainer_loss_operator_left_shift_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
dnn_trainer_operator_left_shift_template(__NET_TYPE__, trainer, stream);

#define set_all_bn_running_stats_window_sizes_loss_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
set_all_bn_running_stats_window_sizes_template(__NET_TYPE__, obj, new_window_size);

#define subnet_delete_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto sb = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
delete sb;

#define subnet_get_output_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
const dlib::tensor& tensor = net->get_output();\
return &tensor;

#define subnet_get_layer_details_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
auto net = static_cast<__NET_TYPE__::subnet_type*>(subnet);\
__NET_TYPE__::subnet_type::layer_details_type& layer_details = net->layer_details();\
return &layer_details;

#pragma endregion template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int __TYPENAME__##_new(const int type, void** net)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_new_template,\
                            net);\
    return error;\
}\
\
DLLEXPORT void __TYPENAME__##_delete(void* obj, const int type)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_delete_template,\
                            obj);\
}\
\
/*// NOTE\
// ret is not std::vector<out_type*>** but std::vector<out_type>**!! It is important!!*/\
DLLEXPORT int __TYPENAME__##_operator_matrixs(void* obj,\
                                              const int type,\
                                              matrix_element_type element_type,\
                                              void* matrix_vector,\
                                              int templateRows,\
                                              int templateColumns,\
                                              size_t batch_size,\
                                              std::vector<out_type>** ret)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                loss_operator_matrixs_template,\
                                obj,\
                                element_type,\
                                matrix_vector,\
                                templateRows,\
                                templateColumns,\
                                batch_size,\
                                ret);\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int __TYPENAME__##_deserialize(const char* file_name,\
                                         const int type,\
                                         void** ret,\
                                         std::string** error_message)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                loss_deserialize_template,\
                                file_name,\
                                ret);\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int __TYPENAME__##_deserialize_proxy(proxy_deserialize* proxy,\
                                               const int type,\
                                               void** ret,\
                                               std::string** error_message)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                loss_deserialize_proxy_template,\
                                proxy,\
                                ret);\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int __TYPENAME__##_serialize(void* obj,\
                                       const int type,\
                                       const char* file_name,\
                                       std::string** error_message)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                loss_serialize_template,\
                                obj,\
                                file_name);\
    }\
    catch (serialization_error& e)\
    {\
        error = ERR_GENERAL_SERIALIZATION;\
        *error_message = new std::string(e.what());\
    }\
\
    return error;\
}\
\
DLLEXPORT int __TYPENAME__##_num_layers(const int type)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_num_layers_template);\
    return 0;\
}\
\
DLLEXPORT int __TYPENAME__##_subnet(void* obj, const int type, void** subnet)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                         error,\
                         loss_subnet_template,\
                         obj,\
                         subnet);\
    return 0;\
}\
\
DLLEXPORT void __TYPENAME__##_clean(void* obj, const int type)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_clean_template);\
}\
\
DLLEXPORT void __TYPENAME__##_input_tensor_to_output_tensor(void* obj, const int type, dlib::dpoint* p, dlib::dpoint** ret)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_input_tensor_to_output_tensor_template,\
                            obj,\
                            p,\
                            ret);\
}\
\
DLLEXPORT void __TYPENAME__##_net_to_xml(void* obj, const int type, const char* filename)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_net_to_xml_template,\
                            filename);\
}\
\
DLLEXPORT void __TYPENAME__##_subnet_delete(const int type, void* subnet)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            subnet_delete_template,\
                            subnet);\
}\
\
DLLEXPORT const dlib::tensor* __TYPENAME__##_subnet_get_output(void* subnet, const int type, int* ret)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            subnet_get_output_template,\
                            subnet,\
                            ret);\
\
    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
    return nullptr;\
}\
\
DLLEXPORT void* __TYPENAME__##_subnet_get_layer_details(void* subnet, const int type, int* ret)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            subnet_get_layer_details_template,\
                            subnet,\
                            ret);\
\
    *ret = ERR_DNN_NOT_SUPPORT_NETWORKTYPE;\
    return nullptr;\
}\
\
DLLEXPORT void* dnn_trainer_##__TYPENAME__##_new(void* net, const int type)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_new_template,\
                            net);\
\
    return nullptr;\
}\
\
DLLEXPORT void* dnn_trainer_##__TYPENAME__##_new_sgd(void* net, const int type, sgd* sgd)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_new_sgd_template,\
                            net,\
                            sgd);\
\
    return nullptr;\
}\
\
DLLEXPORT void dnn_trainer_##__TYPENAME__##_delete(void* trainer, const int type)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_delete_template,\
                            trainer);\
}\
\
DLLEXPORT void dnn_trainer_##__TYPENAME__##_set_learning_rate(void* trainer, const int type, const double lr)\
{\
    int error = ERR_OK;\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_learning_rate_template,\
                            trainer,\
                            lr);\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_get_learning_rate(void* trainer, const int type, double* lr)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_get_learning_rate_template,\
                            trainer,\
                            lr);\
\
    return error;\
}\
\
DLLEXPORT void dnn_trainer_##__TYPENAME__##_set_min_learning_rate(void* trainer, const int type, const double lr)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_min_learning_rate_template,\
                            trainer,\
                            lr);\
}\
\
DLLEXPORT void dnn_trainer_##__TYPENAME__##_set_mini_batch_size(void* trainer, const int type, const unsigned long size)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_mini_batch_size_template,\
                            trainer,\
                            size);\
}\
\
DLLEXPORT void dnn_trainer_##__TYPENAME__##_be_verbose(void* trainer, const int type)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_be_verbose_template,\
                            trainer);\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_set_synchronization_file(void* trainer, const int type, const char* filename, const unsigned long second)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_synchronization_file_template,\
                            trainer,\
                            filename,\
                            second);\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_set_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_iterations_without_progress_threshold_template,\
                            trainer,\
                            thresh);\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_set_test_iterations_without_progress_threshold(void* trainer, const int type, const unsigned long thresh)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_set_test_iterations_without_progress_threshold_template,\
                            trainer,\
                            thresh);\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_test_one_step(void* trainer,\
                                                         const int type,\
                                                         matrix_element_type data_element_type,\
                                                         void* data,\
                                                         matrix_element_type label_element_type,\
                                                         void* labels)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                dnn_trainer_loss_test_one_step_template,\
                                trainer,\
                                data_element_type,\
                                data,\
                                label_element_type,\
                                labels);\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_train(void* trainer,\
                                                 const int type,\
                                                 matrix_element_type data_element_type,\
                                                 void* data,\
                                                 matrix_element_type label_element_type,\
                                                 void* labels)\
{\
    int error = ERR_OK;\
\
    /* ToDo: label type*/\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                dnn_trainer_loss_train_template,\
                                trainer,\
                                data_element_type,\
                                data,\
                                label_element_type,\
                                labels);\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_train_one_step(void* trainer,\
                                                          const int type,\
                                                          matrix_element_type data_element_type,\
                                                          void* data,\
                                                          matrix_element_type label_element_type,\
                                                          void* labels)\
{\
    int error = ERR_OK;\
\
    if (label_element_type != matrix_element_type::UInt32)\
        return ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                dnn_trainer_loss_train_one_step_template,\
                                trainer,\
                                data_element_type,\
                                data,\
                                label_element_type,\
                                labels);\
    }\
    catch(dlib::cuda_error ce)\
    {\
        cuda_error_to_error_code(ce, error);\
    }\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_get_net(void* trainer,\
                                                   const int type,\
                                                   void** ret)\
{\
    int error = ERR_OK;\
\
    try\
    {\
        __TYPENAME__##_template(type,\
                                error,\
                                dnn_trainer_loss_get_net_template,\
                                trainer,\
                                ret);\
    }\
    catch(std::exception)\
    {\
        error = ERR_DNN_PROPAGATE_EXCEPTION;\
    }\
\
    return error;\
}\
\
DLLEXPORT int dnn_trainer_##__TYPENAME__##_operator_left_shift(void* trainer, const int type, std::ostringstream* stream)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            dnn_trainer_loss_operator_left_shift_template,\
                            trainer,\
                            stream);\
\
    return error;\
}\
\
DLLEXPORT int set_all_bn_running_stats_window_sizes_##__TYPENAME__(void* obj, const int type, unsigned long new_window_size)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            set_all_bn_running_stats_window_sizes_loss_template,\
                            obj,\
                            new_window_size);\
\
    return error;\
}\
\
DLLEXPORT int __TYPENAME__##_operator_left_shift(void* obj, const int type, std::ostringstream* stream)\
{\
    int error = ERR_OK;\
\
    __TYPENAME__##_template(type,\
                            error,\
                            loss_operator_left_shift_template,\
                            obj,\
                            stream);\
\
    return error;\
}\

#endif