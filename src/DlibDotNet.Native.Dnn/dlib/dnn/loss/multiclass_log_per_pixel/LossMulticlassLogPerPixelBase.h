#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_BASE_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_BASE_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../LossBase.h"

typedef dlib::matrix<uint16_t> loss_multiclass_log_per_pixel_out_type;
typedef dlib::matrix<uint16_t> loss_multiclass_log_per_pixel_train_label_type;

using namespace dlib;
using namespace std;

class LossMulticlassLogPerPixelBase : public LossBase
{
public:
    virtual int operator_matrixs(void* obj,
                                 matrix_element_type element_type,
                                 void* matrix_vector,
                                 int templateRows,
                                 int templateColumns,
                                 size_t batch_size,
                                 std::vector<loss_multiclass_log_per_pixel_out_type>** ret) = 0;
    virtual void input_tensor_to_output_tensor(void* obj,
                                               dlib::dpoint* p,
                                               dlib::dpoint** ret) = 0;
    virtual int cloneAs(void* obj, const int id, void** ret) = 0;
};

#endif