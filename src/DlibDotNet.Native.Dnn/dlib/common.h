#ifndef _CPP_COMMON_H_
#define _CPP_COMMON_H_

#include "../../DlibDotNet.Native/dlib/export.h"
#include "../../DlibDotNet.Native/dlib/shared.h"

#pragma region template

#define operator_template(net, __TYPE__, matrix_vector, batch_size, ret) \
do {\
    std::vector<dlib::matrix<__TYPE__>*>& tmp = *(static_cast<std::vector<dlib::matrix<__TYPE__>*>*>(matrix_vector));\
    std::vector<dlib::matrix<__TYPE__>> in_tmp;\
    for (int i = 0; i< tmp.size(); i++)\
    {\
        dlib::matrix<__TYPE__>& mat = *tmp[i];\
        in_tmp.push_back(mat);\
    }\
\
    std::vector<out_type> dets = net(in_tmp, batch_size);\
    *ret = new std::vector<out_type>(dets);\
} while (0)

#pragma endregion template

#endif