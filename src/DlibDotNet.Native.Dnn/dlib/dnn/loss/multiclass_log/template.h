#ifndef _CPP_LOSS_MULTICLASS_LOG_TEMPLATE_H_
#define _CPP_LOSS_MULTICLASS_LOG_TEMPLATE_H_

#include "LossMulticlassLog.h"
#include "defines.h"
#include "template.h"

extern std::map<int, LossMulticlassLogBase*> LossMulticlassLogRegistry;

#pragma region template

#define MAKE_LOSSMULTICLASSLOG_FUNC(__NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__)\
DLLEXPORT LossMulticlassLogBase* LossMulticlassLog_##__NET__##_create()\
{\
    return new LossMulticlassLog<__NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__>(__NET__##_labels);\
}\
\
DLLEXPORT void LossMulticlassLog_##__NET__##_delete(void* base)\
{\
    auto loss = static_cast<LossMulticlassLog<__NET__, __MATRIX_ELEMENT__, __ELEMENT__, __LABEL_MATRIX_ELEMENT__, __LABEL_ELEMENT__, __ID__>*>(base);\
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
    std::vector<loss_multiclass_log_out_type> dets = net->operator()(in_tmp, batch_size);\
    *ret = new std::vector<loss_multiclass_log_out_type>(dets);\
} while (0)

#pragma endregion template

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::create(void** ret)
{
    *ret = new NET();
    return ERR_OK;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::destroy(void* net)
{
    auto n = static_cast<NET*>(net);
    delete n;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_label(void* obj,
                                                                                                        std::vector<std::string*>** ret)
{
    int error = ERR_OK;
    auto vec = new std::vector<std::string*>(this->_labels->size());
    for (size_t i = 0; i < this->_labels->size(); i++)
        vec->at(i) = new std::string(this->_labels->at(i));
    *ret = vec;
    return error;
}

/*// NOTE\
// ret is not std::vector<loss_multiclass_log_out_type*>** but std::vector<loss_multiclass_log_out_type>**!! It is important!!*/
template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::operator_matrixs(void* obj,
                                                                                                               const matrix_element_type element_type,
                                                                                                               void* matrix_array,
                                                                                                               const int matrix_array_len,
                                                                                                               const int templateRows,
                                                                                                               const int templateColumns,
                                                                                                               const uint32_t batch_size,
                                                                                                               std::vector<loss_multiclass_log_out_type>** ret)
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::probability(void* obj,
                                                                                                          const matrix_element_type element_type,
                                                                                                          void* matrix_array,
                                                                                                          const int matrix_array_len,
                                                                                                          const int templateRows,
                                                                                                          const int templateColumns,
                                                                                                          const uint32_t batch_size,
                                                                                                          std::vector<float>** ret)
{
    int error = ERR_OK;

    try
    {
        switch(element_type)
        {
            case MATRIX_ELEMENT:
                {
                    auto net = static_cast<NET*>(obj);
                    auto tmp = static_cast<dlib::matrix<ELEMENT>**>(matrix_array);
                    std::vector<dlib::matrix<ELEMENT>> in_tmp;
                    for (int i = 0; i< matrix_array_len; i++)
                    {
                        dlib::matrix<ELEMENT>& mat = *tmp[i];
                        in_tmp.push_back(mat);
                    }

	                softmax<typename NET::subnet_type> snet;
	                snet.subnet() = net->subnet();
	                auto p = mat(snet(in_tmp.begin(), in_tmp.end()));

                    auto batch = p.nr();
                    auto classes = p.nc();
                    auto out_vec = new std::vector<float>(batch * classes);
                    int index = 0;
                    for (long i = 0; i < batch; ++i)
                    for (long c = 0; c < classes; ++c)
                        out_vec->at(index++) = p(i, c);

                    *ret = out_vec;
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::deserialize(const char* file_name,
                                                                                                          const int file_name_length,
                                                                                                          void** ret,
                                                                                                          std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto net = new NET();
        dlib::deserialize(std::string(file_name, file_name_length)) >> (*net);
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::deserialize2(const char* item,
                                                                                                           const int item_length,
                                                                                                           void** ret,
                                                                                                           std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        std::string s(item, item_length);
        std::istringstream iss(s);
        std::istream& stream = iss;
        auto net = new NET();
        dlib::deserialize((*net), stream);
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::deserialize_proxy(proxy_deserialize* proxy,
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::serialize(void* obj,
                                                                                                        const char* file_name,
                                                                                                        const int file_name_length,
                                                                                                        std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto net = static_cast<NET*>(obj);
        dlib::serialize(std::string(file_name, file_name_length)) << (*net);
    }
    catch (serialization_error& e)
    {
        error = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return error;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::serialize_proxy(proxy_serialize* proxy,
                                                                                                              void* obj,
                                                                                                              std::string** error_message)
{
    int error = ERR_OK;

    try
    {
        auto& p = *static_cast<proxy_serialize*>(proxy);
        auto net = static_cast<NET*>(obj);
        p << net;
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
int LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_num_layers()
{
    return NET::num_layers;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::layer_details_set_num_filters(void* layer, long num)
{
    auto ld = static_cast<typename NET::subnet_type::layer_details_type*>(layer);
    // ToDo: some network does not support
    // ld->set_num_filters(num);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_subnet(void* obj, void** subnet)
{
    auto net = static_cast<NET*>(obj);
    typename NET::subnet_type& sn = net->subnet();
    *subnet = &sn;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_get_layer_details(void* subnet)
{
    auto net = static_cast<typename NET::subnet_type*>(subnet);
    typename NET::subnet_type::layer_details_type& layer_details = net->layer_details();
    return &layer_details;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::clean(void* obj)
{
    auto net = static_cast<NET*>(obj);
    net->clean();
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::input_tensor_to_output_tensor(void* obj,
                                                                                                                             dlib::dpoint* p,
                                                                                                                             dlib::dpoint** ret)
{
    // auto& net = *static_cast<NET*>(obj);
    // auto rp = dlib::input_tensor_to_output_tensor(net, *p);
    // *ret = new dlib::dpoint(rp);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::net_to_xml(void* obj, const char* filename, const int file_name_length)
{
    std::string str(filename, file_name_length);
    auto& net = *static_cast<NET*>(obj);
    dlib::net_to_xml(net, str);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::operator_left_shift(void* obj, std::ostringstream* stream)
{
    auto& net = *(static_cast<NET*>(obj));
    *stream << net;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::set_all_bn_running_stats_window_sizes(void* net,
                                                                                                                                     unsigned long new_window_size)
{
    auto& n = *static_cast<NET*>(net);
    dlib::set_all_bn_running_stats_window_sizes(n, new_window_size);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::get_loss_details(void* net,
                                                                                                                void** loss_details)
{
    auto n = static_cast<NET*>(net);
    typename NET::loss_details_type& ret = n->loss_details();
    *loss_details = &ret;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_new(void* net)
{
    auto& n = *static_cast<NET*>(net);
    return new dnn_trainer<NET>(n);
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void* LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_new_optimizer(void* net, const ::optimizer_type optimizer_id, void* optimizer)
{
    auto& n = *static_cast<NET*>(net);
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto& sgd = *static_cast<dlib::sgd*>(optimizer);
                return new dnn_trainer<NET, dlib::sgd>(n, sgd);
            }
        case ::optimizer_type::Adam:
            {
                auto& adam = *static_cast<dlib::adam*>(optimizer);
                return new dnn_trainer<NET, dlib::adam>(n, adam);
            }
        default:
            return nullptr;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_delete(void* trainer, const ::optimizer_type optimizer_id)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                delete t;
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                delete t;
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_learning_rate(void* trainer,
                                                                                                                         const ::optimizer_type optimizer_id,
                                                                                                                         const double lr)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_learning_rate(lr);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_learning_rate(lr);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_learning_rate(void* trainer,
                                                                                                                         const ::optimizer_type optimizer_id,
                                                                                                                         double* lr)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                *lr = t->get_learning_rate();
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                *lr = t->get_learning_rate();
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_average_loss(void* trainer,
                                                                                                                        const ::optimizer_type optimizer_id,
                                                                                                                        double* loss)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                *loss = t->get_average_loss();
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                *loss = t->get_average_loss();
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_average_test_loss(void* trainer,
                                                                                                                             const ::optimizer_type optimizer_id,
                                                                                                                             double* loss)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                *loss = t->get_average_test_loss();
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                *loss = t->get_average_test_loss();
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_min_learning_rate(void* trainer,
                                                                                                                             const ::optimizer_type optimizer_id,
                                                                                                                             const double lr)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_min_learning_rate(lr);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_min_learning_rate(lr);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_mini_batch_size(void* trainer,
                                                                                                                           const ::optimizer_type optimizer_id,
                                                                                                                           const unsigned long size)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_mini_batch_size(size);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_mini_batch_size(size);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_be_verbose(void* trainer, const ::optimizer_type optimizer_id)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->be_verbose();
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->be_verbose();
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_synchronization_file(void* trainer,
                                                                                                                                const ::optimizer_type optimizer_id,
                                                                                                                                const char* filename,
                                                                                                                                const int filename_length,
                                                                                                                                const unsigned long second)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_synchronization_file(std::string(filename, filename_length), std::chrono::seconds(second));
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_synchronization_file(std::string(filename, filename_length), std::chrono::seconds(second));
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_iterations_without_progress_threshold(void* trainer,
                                                                                                                                                 const ::optimizer_type optimizer_id,
                                                                                                                                                 const unsigned long thresh)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_iterations_without_progress_threshold(thresh);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_iterations_without_progress_threshold(thresh);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_set_test_iterations_without_progress_threshold(void* trainer,
                                                                                                                                                      const ::optimizer_type optimizer_id,
                                                                                                                                                      const unsigned long thresh)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->set_test_iterations_without_progress_threshold(thresh);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->set_test_iterations_without_progress_threshold(thresh);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_get_net(void* trainer,
                                                                                                               const ::optimizer_type optimizer_id,
                                                                                                               void** ret)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                *ret = &(t->get_net());
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                *ret = &(t->get_net());
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_operator_left_shift(void* trainer,
                                                                                                                           const ::optimizer_type optimizer_id,
                                                                                                                           std::ostringstream* stream)
{
    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                *stream << t;
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                *stream << t;
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_delete(void* subnet)
{
    auto sb = static_cast<typename NET::subnet_type*>(subnet);
    delete sb;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
const dlib::tensor* LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::subnet_get_output(void* subnet,
                                                                                                                                int* ret)
{
    auto net = static_cast<typename NET::subnet_type*>(subnet);
    const dlib::tensor& tensor = net->get_output();
    *ret = ERR_OK;
    return &tensor;
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_test_one_step(void* trainer,
                                                                                                                     const ::optimizer_type optimizer_id,
                                                                                                                     matrix_element_type data_element_type,
                                                                                                                     void* data,
                                                                                                                     matrix_element_type label_element_type,
                                                                                                                     void* labels)
{
    std::vector<dlib::matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->test_one_step(out_data, out_label);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->test_one_step(out_data, out_label);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_train(void* trainer,
                                                                                                             const ::optimizer_type optimizer_id,
                                                                                                             matrix_element_type data_element_type,
                                                                                                             void* data,
                                                                                                             matrix_element_type label_element_type,
                                                                                                             void* labels)
{
    std::vector<dlib::matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->train(out_data, out_label);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->train(out_data, out_label);
            }
            break;
    }
}

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
void LossMulticlassLog<NET, MATRIX_ELEMENT, ELEMENT, LABEL_MATRIX_ELEMENT, LABEL_ELEMENT, ID>::trainer_train_one_step(void* trainer,
                                                                                                                      const ::optimizer_type optimizer_id,
                                                                                                                      matrix_element_type data_element_type,
                                                                                                                      void* data,
                                                                                                                      matrix_element_type label_element_type,
                                                                                                                      void* labels)
{
    std::vector<matrix<ELEMENT>> out_data;
    std::vector<LABEL_ELEMENT> out_label;
    convert(data, labels, out_data, out_label);

    switch(optimizer_id)
    {
        case ::optimizer_type::Sgd:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::sgd>*>(trainer);
                t->train_one_step(out_data, out_label);
            }
            break;
        case ::optimizer_type::Adam:
            {
                auto t = static_cast<dnn_trainer<NET, dlib::adam>*>(trainer);
                t->train_one_step(out_data, out_label);
            }
            break;
    }
}

#endif