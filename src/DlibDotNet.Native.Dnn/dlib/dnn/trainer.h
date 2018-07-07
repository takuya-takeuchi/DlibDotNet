#ifndef _CPP_TRAINER_H_
#define _CPP_TRAINER_H_

#include <dlib/dnn.h>
#include <dlib/dnn/trainer.h>

#include "../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define NET_TYPE nettype
#undef NET_TYPE

#define dnn_trainer_new_template(net) \
do {\
    NET_TYPE& n = *static_cast<NET_TYPE*>(net);\
    return new dnn_trainer<NET_TYPE>(n);\
} while (0)

#define dnn_trainer_delete_template(trainer) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    delete t;\
} while (0)

#define dnn_trainer_set_learning_rate_template(trainer, lr) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->set_learning_rate(lr);\
} while (0)

#define dnn_trainer_set_min_learning_rate_template(trainer, lr) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->set_min_learning_rate(lr);\
} while (0)

#define dnn_trainer_set_mini_batch_size_template(trainer, size) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->set_mini_batch_size(size);\
} while (0)

#define dnn_trainer_be_verbose_template(trainer) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->be_verbose();\
} while (0)

#define dnn_trainer_set_synchronization_file_template(trainer, filename, sec) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->set_synchronization_file(filename, sec);\
} while (0)

#define dnn_trainer_train_template(trainer, in_tmp_data, in_tmp_label) \
do {\
    auto t = static_cast<dnn_trainer<NET_TYPE>*>(trainer);\
    t->train(in_tmp_data, in_tmp_label);\
} while (0)

#pragma endregion template

#endif