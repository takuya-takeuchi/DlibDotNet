#ifndef _CPP_LOSS_METRIC_H_
#define _CPP_LOSS_METRIC_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "LossMetricBase.h"

using namespace dlib;
using namespace std;

template<typename NET, matrix_element_type MATRIX_ELEMENT, typename ELEMENT, int ROW, int COLUMN, matrix_element_type LABEL_MATRIX_ELEMENT, typename LABEL_ELEMENT, int ID>
class LossMetric : public LossMetricBase
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
                                 std::vector<loss_metric_out_type>** ret) override;
    virtual int deserialize(const char* file_name,
                            void** ret,
                            std::string** error_message) override;
    virtual int deserialize_proxy(proxy_deserialize* proxy,
                                  void** ret,
                                  std::string** error_message) override;
    virtual int serialize(void* obj,
                          const char* file_name,
                          std::string** error_message) override;
    virtual int serialize_proxy(proxy_serialize* proxy,
                                void* obj,
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
    virtual void net_to_xml(void* obj, const char* filename) override;
    virtual void operator_left_shift(void* obj, std::ostringstream* stream) override;
    virtual void set_all_bn_running_stats_window_sizes(void* obj, unsigned long new_window_size) override;

public:
    virtual void get_loss_details(void* obj, void** loss_details) override;
    virtual void loss_details_get_distance_threshold(void* loss_details, float* distance_threshold) override;

public:
    virtual void* trainer_new(void* net) override;
    virtual void* trainer_new_sgd(void* net, sgd* sgd) override;
    virtual void trainer_delete(void* trainer) override;
    virtual void trainer_set_learning_rate(void* trainer, const double lr) override;
    virtual void trainer_get_learning_rate(void* trainer, double* lr) override;
    virtual void trainer_get_average_loss(void* trainer, double* loss) override;
    virtual void trainer_get_average_test_loss(void* trainer, double* loss) override;
    virtual void trainer_set_min_learning_rate(void* trainer, const double lr) override;
    virtual void trainer_set_mini_batch_size(void* trainer, const unsigned long size) override;
    virtual void trainer_be_verbose(void* trainer) override;
    virtual void trainer_set_synchronization_file(void* trainer,
                                           const char* filename,
                                           const unsigned long second) override;
    virtual void trainer_set_iterations_without_progress_threshold(void* trainer,
                                                            const unsigned long thresh) override;
    virtual void trainer_set_test_iterations_without_progress_threshold(void* trainer,
                                                                 const unsigned long thresh) override;
    virtual void trainer_get_net(void* trainer,
                                 void** ret) override;
    virtual void trainer_operator_left_shift(void* trainer, std::ostringstream* stream) override;
    virtual void trainer_test_one_step(void* trainer,
                                       matrix_element_type data_element_type,
                                       void* data,
                                       matrix_element_type label_element_type,
                                       void* labels) override;
    virtual void trainer_train(void* trainer,
                               matrix_element_type data_element_type,
                               void* data,
                               matrix_element_type label_element_type,
                               void* labels) override;
    virtual void trainer_train_one_step(void* trainer,
                                        matrix_element_type data_element_type,
                                        void* data,
                                        matrix_element_type label_element_type,
                                        void* labels) override;
protected:
    void convert(void* data,
                 void* labels,
                 std::vector<dlib::matrix<ELEMENT, ROW, COLUMN>>& out_data,
                 std::vector<LABEL_ELEMENT>& out_labels)
    {
        std::vector<dlib::matrix<ELEMENT, ROW, COLUMN>*>& tmp_data = *(static_cast<std::vector<dlib::matrix<ELEMENT, ROW, COLUMN>*>*>(data));
        for (size_t i = 0; i< tmp_data.size(); i++)
        {
            dlib::matrix<ELEMENT, ROW, COLUMN>& mat = *tmp_data[i];
            out_data.push_back(mat);
        }

        std::vector<LABEL_ELEMENT>& tmp_label = *(static_cast<std::vector<LABEL_ELEMENT>*>(labels));
        for (size_t i = 0; i< tmp_label.size(); i++)
            out_labels.push_back(tmp_label[i]);
    }
};

#endif