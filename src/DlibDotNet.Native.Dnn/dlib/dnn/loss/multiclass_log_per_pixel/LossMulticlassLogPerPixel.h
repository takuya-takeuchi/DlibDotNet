#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMulticlassLogPerPixelBase.h"

using namespace dlib;
using namespace std;

template<typename NET,
         matrix_element_type MATRIX_ELEMENT,
         typename ELEMENT,
         matrix_element_type LABEL_MATRIX_ELEMENT,
         typename LABEL_ELEMENT,
         int ID>
class LossMulticlassLogPerPixel : public LossMulticlassLogPerPixelBase
{
public:
    const int get_id() override
    {
        return ID;
    }

    const matrix_element_type get_data_type() override
    {
        return MATRIX_ELEMENT;
    }

    const matrix_element_type get_label_type() override
    {
        return LABEL_MATRIX_ELEMENT;
    }

public:
    virtual int create(void** ret) override;
    virtual void destroy(void* net) override;
    virtual int operator_matrixs(void* obj,
                                 const matrix_element_type element_type,
                                 void* matrix_array,
                                 const int matrix_array_len,
                                 const int templateRows,
                                 const int templateColumns,
                                 const uint32_t batch_size,
                                 std::vector<loss_multiclass_log_per_pixel_out_type>** ret) override;
    virtual int deserialize(const char* file_name,
                            const int file_name_length,
                            void** ret,
                            std::string** error_message) override;
    virtual int deserialize2(const char* item,
                             const int item_length,
                             void** ret,
                             std::string** error_message) override;
    virtual int deserialize_proxy(proxy_deserialize* proxy,
                                  void** ret,
                                  std::string** error_message) override;
    virtual int deserialize_proxy_map(proxy_deserialize* proxy,
                                      std::string*** keys,
                                      void** values,
                                      int* size,
                                      std::string** error_message) override;
    virtual int serialize(void* obj,
                          const char* file_name,
                          const int file_name_length,
                          std::string** error_message) override;
    virtual int serialize_proxy(proxy_serialize* proxy,
                                void* obj,
                                std::string** error_message) override;
    virtual int serialize_proxy_map(proxy_serialize* proxy,
                                    std::string** keys,
                                    void* values,
                                    int size,
                                    std::string** error_message) override;
    virtual int get_num_layers() override;
    virtual void layer_details_set_num_filters(void* layer, long num) override;
    virtual void get_subnet(void* obj, void** subnet) override;
    virtual void* subnet_get_layer_details(void* subnet) override;
    virtual const dlib::tensor* subnet_get_output(void* subnet, int* ret) override;
    virtual void subnet_delete(void* subnet) override;
    virtual void clean(void* obj) override;
    virtual void input_tensor_to_output_tensor(void* obj,
                                               dlib::dpoint* p,
                                               dlib::dpoint** ret) override;
    virtual void net_to_xml(void* obj, const char* filename, const int file_name_length) override;
    virtual void operator_left_shift(void* obj, std::ostringstream* stream) override;
    virtual void set_all_bn_running_stats_window_sizes(void* obj, unsigned long new_window_size) override;
    virtual void get_loss_details(void* obj, void** loss_details) override;
    virtual int cloneAs(void* obj, const int id, void** ret) override
    {
        return ERR_DNN_NOT_SUPPORT_NETWORKTYPE;
    }

public:
    virtual void* trainer_new(void* net) override;
    virtual void* trainer_new_optimizer(void* net, const int32_t optimizer_id, void* optimizer) override;
    virtual void trainer_delete(void* trainer, const int32_t optimizer_id) override;
    virtual void trainer_set_learning_rate(void* trainer, const int32_t optimizer_id, const double lr) override;
    virtual void trainer_get_learning_rate(void* trainer, const int32_t optimizer_id, double* lr) override;
    virtual void trainer_get_average_loss(void* trainer, const int32_t optimizer_id, double* loss) override;
    virtual void trainer_get_average_test_loss(void* trainer, const int32_t optimizer_id, double* loss) override;
    virtual void trainer_set_min_learning_rate(void* trainer, const int32_t optimizer_id, const double lr) override;
    virtual void trainer_set_mini_batch_size(void* trainer, const int32_t optimizer_id, const unsigned long size) override;
    virtual void trainer_be_verbose(void* trainer, const int32_t optimizer_id) override;
    virtual void trainer_set_synchronization_file(void* trainer,
                                                  const int32_t optimizer_id,
                                                  const char* filename,
                                                  const int filename_length,
                                                  const unsigned long second) override;
    virtual void trainer_set_iterations_without_progress_threshold(void* trainer,
                                                                   const int32_t optimizer_id,
                                                                   const unsigned long thresh) override;
    virtual void trainer_set_test_iterations_without_progress_threshold(void* trainer,
                                                                        const int32_t optimizer_id,
                                                                        const unsigned long thresh) override;
    virtual void trainer_get_net(void* trainer,
                                 const int32_t optimizer_id,
                                 void** ret) override;
    virtual void trainer_operator_left_shift(void* trainer, const int32_t optimizer_id, std::ostringstream* stream) override;
    virtual void trainer_test_one_step(void* trainer,
                                       matrix_element_type data_element_type,
                                       void* data,
                                       matrix_element_type label_element_type,
                                       void* labels,
                                       const int32_t optimizer_id) override;
    virtual void trainer_train(void* trainer,
                               matrix_element_type data_element_type,
                               void* data,
                               matrix_element_type label_element_type,
                               void* labels,
                               const int32_t optimizer_id) override;
    virtual void trainer_train_one_step(void* trainer,
                                        matrix_element_type data_element_type,
                                        void* data,
                                        matrix_element_type label_element_type,
                                        void* labels,
                                        const int32_t optimizer_id) override;
protected:
    void convert(void* data,
                 void* labels,
                 std::vector<dlib::matrix<ELEMENT>>& out_data,
                 std::vector<LABEL_ELEMENT>& out_labels)
    {
        std::vector<dlib::matrix<ELEMENT>*>& tmp_data = *(static_cast<std::vector<dlib::matrix<ELEMENT>*>*>(data));
        for (size_t i = 0; i< tmp_data.size(); i++)
        {
            dlib::matrix<ELEMENT>& mat = *tmp_data[i];
            out_data.push_back(mat);
        }

        std::vector<LABEL_ELEMENT*>& tmp_label = *(static_cast<std::vector<LABEL_ELEMENT*>*>(labels));
        for (size_t i = 0; i< tmp_label.size(); i++)
        {
            auto& label = *tmp_label[i];
            out_labels.push_back(label);
        }
    }
};

template<typename NET,
         matrix_element_type MATRIX_ELEMENT,
         typename ELEMENT,
         matrix_element_type LABEL_MATRIX_ELEMENT,
         typename LABEL_ELEMENT,
         int ID>
class LossMulticlassLogPerPixelNet : public LossMulticlassLogPerPixel <NET,
                                                                       MATRIX_ELEMENT,
                                                                       ELEMENT,
                                                                       LABEL_MATRIX_ELEMENT,
                                                                       LABEL_ELEMENT,
                                                                       ID>
{
public:
    virtual int cloneAs(void* obj, const int id, void** ret) override;
};

template<typename NET,
         matrix_element_type MATRIX_ELEMENT,
         typename ELEMENT,
         matrix_element_type LABEL_MATRIX_ELEMENT,
         typename LABEL_ELEMENT,
         int ID>
class LossMulticlassLogPerPixelUBNet : public LossMulticlassLogPerPixel <NET,
                                                                         MATRIX_ELEMENT,
                                                                         ELEMENT,
                                                                         LABEL_MATRIX_ELEMENT,
                                                                         LABEL_ELEMENT,
                                                                         ID>
{
public:
    virtual int cloneAs(void* obj, const int id, void** ret) override;
};

#endif