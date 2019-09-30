#ifndef _CPP_LOSS_METRIC_BASE_H_
#define _CPP_LOSS_METRIC_BASE_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../LossBase.h"

typedef matrix<float,0,1> loss_metric_out_type;
typedef unsigned long     loss_metric_train_label_type;

using namespace dlib;
using namespace std;

class LossMetricBase : public LossBase
{
public:
    virtual int operator_matrixs(void* obj,
                                 const matrix_element_type element_type,
                                 void* matrix_array,
                                 const int matrix_array_len,
                                 const int templateRows,
                                 const int templateColumns,
                                 const uint32_t batch_size,
                                 std::vector<loss_metric_out_type>** ret) = 0;
    virtual void input_tensor_to_output_tensor(void* obj,
                                               dlib::dpoint* p,
                                               dlib::dpoint** ret) = 0;

public:
    virtual void loss_details_get_distance_threshold(void* loss_details, float* distance_threshold) = 0;
};

#endif