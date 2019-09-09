#ifndef _CPP_LOSS_MMOD_BASE_H_
#define _CPP_LOSS_MMOD_BASE_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../LossBase.h"

typedef std::vector<mmod_rect>  loss_mmod_out_type;
typedef std::vector<mmod_rect>  loss_mmod_train_label_type;
typedef std::vector<mmod_rect*> loss_mmod_train_label_type_pointer;

using namespace dlib;
using namespace std;

class LossMmodBase : public LossBase
{
public:
    virtual int create2(mmod_options* option, void** ret) = 0;
    virtual int get_input_layer(void* obj, void** ret) = 0;
    virtual int operator_matrixs(void* obj,
                                 matrix_element_type element_type,
                                 void* matrix_vector,
                                 int templateRows,
                                 int templateColumns,
                                 size_t batch_size,
                                 std::vector<loss_mmod_out_type>** ret) = 0;
    virtual void input_tensor_to_output_tensor(void* obj,
                                               dlib::dpoint* p,
                                               dlib::dpoint** ret) = 0;
};

#endif