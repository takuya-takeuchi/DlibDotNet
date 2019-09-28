#ifndef _CPP_LOSS_MULTICLASS_LOG_BASE_H_
#define _CPP_LOSS_MULTICLASS_LOG_BASE_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../LossBase.h"

typedef unsigned long loss_multiclass_log_out_type;
typedef unsigned long loss_multiclass_log_train_label_type;

using namespace dlib;
using namespace std;

class LossMulticlassLogBase : public LossBase
{
public:
    virtual int get_label(void* obj,
                          std::vector<std::string*>** ret) = 0;
    virtual int operator_matrixs(void* obj,
                                 const matrix_element_type element_type,
                                 void* matrix_array,
                                 const int matrix_array_len,
                                 const int templateRows,
                                 const int templateColumns,
                                 const uint32_t batch_size,
                                 std::vector<loss_multiclass_log_out_type>** ret) = 0;
    virtual int probability(void* obj,
                            const matrix_element_type element_type,
                            void* matrix_array,
                            const int matrix_array_len,
                            const int templateRows,
                            const int templateColumns,
                            const uint32_t batch_size,
                            std::vector<float>** ret) = 0;
    virtual void input_tensor_to_output_tensor(void* obj,
                                               dlib::dpoint* p,
                                               dlib::dpoint** ret) = 0;
};

#endif