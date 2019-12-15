#ifndef _CPP_OPTIMIZATION_OPTIMIZATION_SOLVE_QP2_USING_SMO_H_
#define _CPP_OPTIMIZATION_OPTIMIZATION_SOLVE_QP2_USING_SMO_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/optimization/optimization_solve_qp2_using_smo.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int maximum_nu_##__TYPENAME__##_vector(void* y, __TYPE__* ret)\
{\
    int error = ERR_OK;\
\
    auto& v = *(static_cast<std::vector<__TYPE__>*>(y));\
    std::vector<__TYPE__> tmp;\
    tmp.reserve(v.size());\
    for (int i = 0, cnt = v.size() ; i < cnt; i++ )\
        tmp.push_back(v[i]);\
    *ret = dlib::maximum_nu(tmp);\
\
    return error;\
}\

#pragma endregion template

MAKE_FUNC(float, float)
MAKE_FUNC(double, double)

#endif