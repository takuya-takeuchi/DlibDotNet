#ifndef _CPP_TRAINER_H_
#define _CPP_TRAINER_H_

#include <dlib/dnn.h>
#include <dlib/dnn/trainer.h>

#include "../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define dnn_trainer_new_template(__NET_TYPE__, net) \
do {\
    __NET_TYPE__& n = *static_cast<__NET_TYPE__*>(net);\
    return new dnn_trainer<__NET_TYPE__>(n);\
} while (0)

#define dnn_trainer_delete_template(__NET_TYPE__, trainer) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    delete t;\
} while (0)

#define dnn_trainer_set_learning_rate_template(__NET_TYPE__, trainer, lr) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->set_learning_rate(lr);\
} while (0)

#define dnn_trainer_set_min_learning_rate_template(__NET_TYPE__, trainer, lr) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->set_min_learning_rate(lr);\
} while (0)

#define dnn_trainer_set_mini_batch_size_template(__NET_TYPE__, trainer, size) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->set_mini_batch_size(size);\
} while (0)

#define dnn_trainer_be_verbose_template(__NET_TYPE__, trainer) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->be_verbose();\
} while (0)

#define dnn_trainer_set_synchronization_file_template(__NET_TYPE__, trainer, filename, sec) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->set_synchronization_file(filename, sec);\
} while (0)

#define dnn_trainer_train_template(__NET_TYPE__, trainer, in_tmp_data, in_tmp_label) \
do {\
    auto t = static_cast<dnn_trainer<__NET_TYPE__>*>(trainer);\
    t->train(in_tmp_data, in_tmp_label);\
} while (0)

#pragma endregion template

#endif