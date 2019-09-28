#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_TEMPLATE_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_TEMPLATE_H_

#include "LossMulticlassLogPerPixel.h"
#include "defines.h"
#include "defines.h"

extern std::map<int, LossMulticlassLogPerPixelBase*> LossMulticlassLogPerPixelRegistry;

#pragma region template

#define MAKE_LOSSMULTICLASSLOGPERPIXEL_FUNC(__CLASS__, __NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__)\
DLLEXPORT LossMulticlassLogPerPixelBase* LossMulticlassLogPerPixel_##__NET__##_create()\
{\
    return new __CLASS__<__NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__>();\
}\
\
DLLEXPORT void LossMulticlassLogPerPixel_##__NET__##_delete(void* base)\
{\
    auto loss = static_cast<__CLASS__<__NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__>*>(base);\
    delete loss;\
}\

#pragma endregion template

#pragma region template

#define operator_template(net, __TYPE__, matrix_array, matrix_array_len, batch_size, ret) \
do {\
    auto tmp = static_cast<dlib::matrix<__TYPE__>**>(matrix_array);\
    std::vector<dlib::matrix<__TYPE__>> in_tmp;\
    for (int i = 0; i< matrix_array_len; i++)\
    {\
        dlib::matrix<__TYPE__>& mat = *tmp[i];\
        in_tmp.push_back(mat);\
    }\
\
    std::vector<loss_multiclass_log_per_pixel_out_type> dets = net->operator()(in_tmp, batch_size);\
    *ret = new std::vector<loss_multiclass_log_per_pixel_out_type>(dets);\
} while (0)

#pragma endregion template

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::create(void** ret)
{
    *ret = new NET();
    return ERR_OK;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::destroy(void* net)
{
    auto n = static_cast<NET*>(net);
    delete n;
}

/*// NOTE\
// ret is not std::vector<loss_multiclass_log_per_pixel_out_type*>** but std::vector<loss_multiclass_log_per_pixel_out_type>**!! It is important!!*/
template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::operator_matrixs(void* obj,
                                                                                                                       const matrix_element_type element_type,
                                                                                                                       void* matrix_array,
                                                                                                                       const int matrix_array_len,
                                                                                                                       const int templateRows,
                                                                                                                       const int templateColumns,
                                                                                                                       const uint32_t batch_size,
                                                                                                                       std::vector<loss_multiclass_log_per_pixel_out_type>** ret)
{
    int error = ERR_OK;

    try
    {
        switch(element_type)
        {
            case MATRIX_ELEMENT:
                {
                    auto net = static_cast<NET*>(obj);
                    operator_template(net, ELEMENT, matrix_array, matrix_array_len, batch_size, ret);
                }
                break;
            default:
                error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::deserialize(const char* file_name,
                                                                                                                  void** ret,
                                                                                                                  std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto net = new NET();
        dlib::deserialize(file_name) >> (*net);
        *ret = net;
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::deserialize_proxy(proxy_deserialize* proxy,
                                                                                                                        void** ret,
                                                                                                                        std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto& p = *static_cast<proxy_deserialize*>(proxy);
        auto net = new NET();
        p >> (*net);
        *ret = net;
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, error);
    }

    return error;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::serialize(void* obj,
                                                                                                                const char* file_name,
                                                                                                                std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto net = static_cast<NET*>(obj);
        dlib::serialize(file_name) << (*net);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_num_layers()
{
    return NET::num_layers;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::layer_details_set_num_filters(void* layer, long num)
{
    auto ld = static_cast<typename NET::subnet_type::layer_details_type*>(layer);
    // ToDo: some network does not support
    // ld->set_num_filters(num);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_subnet(void* obj, void** subnet)
{
    auto net = static_cast<NET*>(obj);
    typename NET::subnet_type& sn = net->subnet();
    *subnet = &sn;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_get_layer_details(void* subnet)
{
    auto net = static_cast<typename NET::subnet_type*>(subnet);
    typename NET::subnet_type::layer_details_type& layer_details = net->layer_details();
    return &layer_details;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::clean(void* obj)
{
    auto net = static_cast<NET*>(obj);
    net->clean();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::input_tensor_to_output_tensor(void* obj,
                                                                                                                                     dlib::dpoint* p,
                                                                                                                                     dlib::dpoint** ret)
{
    // auto& net = *static_cast<NET*>(obj);
    // auto rp = dlib::input_tensor_to_output_tensor(net, *p);
    // *ret = new dlib::dpoint(rp);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::net_to_xml(void* obj, const char* filename)
{
    std::string str(filename);
    auto& net = *static_cast<NET*>(obj);
    dlib::net_to_xml(net, str);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::operator_left_shift(void* obj, std::ostringstream* stream)
{
    auto& net = *(static_cast<NET*>(obj));
    *stream << net;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::set_all_bn_running_stats_window_sizes(void* net,
                                                                                                                                             unsigned long new_window_size)
{
    auto& n = *static_cast<NET*>(net);
    dlib::set_all_bn_running_stats_window_sizes(n, new_window_size);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_loss_details(void* net,
                                                                                                                        void** loss_details)
{
    auto n = static_cast<NET*>(net);
    NET::loss_details_type& ret = n->loss_details();
    *loss_details = &ret;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_new(void* net)
{
    auto& n = *static_cast<NET*>(net);
    return new dnn_trainer<NET>(n);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_new_sgd(void* net, sgd* param)
{
    auto& n = *static_cast<NET*>(net);
    auto& p = *static_cast<sgd*>(param);
    return new dnn_trainer<NET>(n, p);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_delete(void* trainer)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    delete t;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_learning_rate(void* trainer, const double lr)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_learning_rate(lr);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_learning_rate(void* trainer, double* lr)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    *lr = t->get_learning_rate();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_average_loss(void* trainer, double* loss)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    *loss = t->get_average_loss();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_average_test_loss(void* trainer, double* loss)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    *loss = t->get_average_test_loss();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_min_learning_rate(void* trainer, const double lr)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_min_learning_rate(lr);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_mini_batch_size(void* trainer, const unsigned long size)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_mini_batch_size(size);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_be_verbose(void* trainer)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->be_verbose();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_synchronization_file(void* trainer,
                                                                                                                                        const char* filename,
                                                                                                                                        const unsigned long second)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_synchronization_file(filename, std::chrono::seconds(second));
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_iterations_without_progress_threshold(void* trainer,
                                                                                                                                                         const unsigned long thresh)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_iterations_without_progress_threshold(thresh);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_test_iterations_without_progress_threshold(void* trainer,
                                                                                                                                                                                                   const unsigned long thresh)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->set_test_iterations_without_progress_threshold(thresh);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_net(void* trainer,
                                                                                                                       void** ret)
{
    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    *ret = &(t->get_net());
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_operator_left_shift(void* trainer,
                                                                                                                                   std::ostringstream* stream)
{
    auto& t = *static_cast<dnn_trainer<NET>*>(trainer);
    *stream << t;\
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_delete(void* subnet)
{
    auto sb = static_cast<typename NET::subnet_type*>(subnet);
    delete sb;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
const dlib::tensor* LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_get_output(void* subnet,
                                                                                                                                        int* ret)
{
    auto net = static_cast<typename NET::subnet_type*>(subnet);
    const dlib::tensor& tensor = net->get_output();
    *ret = ERR_OK;
    return &tensor;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_test_one_step(void* trainer,
                                                                                                                             matrix_element_type data_element_type,
                                                                                                                             void* data,
                                                                                                                             matrix_element_type label_element_type,
                                                                                                                             void* labels)
{
    std::vector<dlib::matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->test_one_step(out_data, out_label);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_train(void* trainer,
                                                                                                                     matrix_element_type data_element_type,
                                                                                                                     void* data,
                                                                                                                     matrix_element_type label_element_type,
                                                                                                                     void* labels)
{
    std::vector<dlib::matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->train(out_data, out_label);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLogPerPixel<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_train_one_step(void* trainer,
                                                                                                                              matrix_element_type data_element_type,
                                                                                                                              void* data,
                                                                                                                              matrix_element_type label_element_type,
                                                                                                                              void* labels)
{
    std::vector<matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    auto t = static_cast<dnn_trainer<NET>*>(trainer);
    t->train_one_step(out_data, out_label);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixelNet<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::cloneAs(void* obj,
                                                                                                                 const int id,
                                                                                                                 void** ret)
{
    // clone to anet
    if (id != 1)
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    auto& net = *static_cast<NET*>(obj);
    *ret = new anet_type(net);
    return ERR_OK;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLogPerPixelUBNet<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::cloneAs(void* obj,
                                                                                                                   const int id,
                                                                                                                   void** ret)
{
    // clone to uanet
    if (id != 3)
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;

    auto& net = *static_cast<NET*>(obj);
    *ret = new uanet_type(net);
    return ERR_OK;
}

#endif