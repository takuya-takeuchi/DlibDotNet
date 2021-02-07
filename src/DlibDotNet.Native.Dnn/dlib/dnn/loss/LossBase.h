#ifndef _CPP_LOSS_BASE_H_
#define _CPP_LOSS_BASE_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../../common.h"

using namespace dlib;
using namespace std;

class LossBase
{
public:
    virtual const int get_id() = 0;

    virtual const matrix_element_type get_data_type() = 0;

    virtual const matrix_element_type get_label_type() = 0;

public:
    virtual int create(void** ret) = 0;
    virtual void destroy(void* net) = 0;
    virtual int deserialize(const char* file_name,
                            const int file_name_length,
                            void** ret,
                            std::string** error_message) = 0;
    virtual int deserialize2(const char* item,
                             const int item_length,
                             void** ret,
                             std::string** error_message) = 0;
    virtual int deserialize_proxy(proxy_deserialize* proxy,
                                  void** ret,
                                  std::string** error_message) = 0;
    virtual int serialize(void* obj,
                          const char* file_name,
                          const int file_name_length,
                          std::string** error_message) = 0;
    virtual int serialize_proxy(proxy_serialize* proxy,
                                void* obj,
                                std::string** error_message) = 0;
    virtual int get_num_layers() = 0;
    virtual void layer_details_set_num_filters(void* layer, long num) = 0;
    virtual void get_subnet(void* obj, void** subnet) = 0;
    virtual void* subnet_get_layer_details(void* subnet) = 0;
    virtual const dlib::tensor* subnet_get_output(void* subnet, int* ret) = 0;
    virtual void subnet_delete(void* subnet) = 0;
    virtual void clean(void* obj) = 0;
    virtual void net_to_xml(void* obj, const char* filename, const int file_name_length) = 0;
    virtual void operator_left_shift(void* obj, std::ostringstream* stream) = 0;
    virtual void set_all_bn_running_stats_window_sizes(void* obj, unsigned long new_window_size) = 0;
    virtual void get_loss_details(void* obj, void** loss_details) = 0;

public:
    virtual void* trainer_new(void* net) = 0;
    virtual void* trainer_new_sgd(void* net, sgd* sgd) = 0;
    virtual void trainer_delete(void* trainer) = 0;
    virtual void trainer_set_learning_rate(void* trainer, const double lr) = 0;
    virtual void trainer_get_learning_rate(void* trainer, double* lr) = 0;
    virtual void trainer_get_average_loss(void* trainer, double* loss) = 0;
    virtual void trainer_get_average_test_loss(void* trainer, double* loss) = 0;
    virtual void trainer_set_min_learning_rate(void* trainer, const double lr) = 0;
    virtual void trainer_set_mini_batch_size(void* trainer, const unsigned long size) = 0;
    virtual void trainer_be_verbose(void* trainer) = 0;
    virtual void trainer_set_synchronization_file(void* trainer,
                                                  const char* filename,
                                                  const int filename_length,
                                                  const unsigned long second) = 0;
    virtual void trainer_set_iterations_without_progress_threshold(void* trainer,
                                                                   const unsigned long thresh) = 0;
    virtual void trainer_set_test_iterations_without_progress_threshold(void* trainer,
                                                                        const unsigned long thresh) = 0;
    virtual void trainer_get_net(void* trainer,
                                 void** ret) = 0;
    virtual void trainer_operator_left_shift(void* trainer, std::ostringstream* stream) = 0;
    virtual void trainer_test_one_step(void* trainer,
                                       matrix_element_type data_element_type,
                                       void* data,
                                       matrix_element_type label_element_type,
                                       void* labels) = 0;
    virtual void trainer_train(void* trainer,
                               matrix_element_type data_element_type,
                               void* data,
                               matrix_element_type label_element_type,
                               void* labels) = 0;
    virtual void trainer_train_one_step(void* trainer,
                                        matrix_element_type data_element_type,
                                        void* data,
                                        matrix_element_type label_element_type,
                                        void* labels) = 0;

};

#endif