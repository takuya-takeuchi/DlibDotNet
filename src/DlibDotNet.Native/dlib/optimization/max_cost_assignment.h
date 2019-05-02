#ifndef _CPP_MAX_COST_ASSIGNMENT_H_
#define _CPP_MAX_COST_ASSIGNMENT_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/optimization/max_cost_assignment.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define assignment_cost_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(cost));\
std::vector<long> tmp;\
for (int i = 0, cnt = assignment->size() ; i < cnt; i++ )\
    tmp.push_back((*assignment)[i]);\
*((__TYPE__*)ret) = dlib::assignment_cost(m, tmp);\

#define max_cost_assignment_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& m = *(static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(cost));\
std::vector<long> ret = dlib::max_cost_assignment(m);\
for (int i = 0, cnt = ret.size() ; i < cnt; i++ )\
    assignments->push_back(ret[i]);\

#pragma endregion template

DLLEXPORT int assignment_cost(matrix_element_type type, void* cost, std::vector<int64_t>* assignment, void* ret)
{
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size_template,
                            assignment_cost_template,
                            0,
                            0,
                            cost,
                            assignments,
                            ret);

    return error;
}

DLLEXPORT int max_cost_assignment(matrix_element_type type, void* cost, std::vector<int64_t> *assignments)
{
    int error = ERR_OK;

    matrix_integer_template(type,
                            error,
                            matrix_template_size_template,
                            max_cost_assignment_template,
                            0,
                            0,
                            cost,
                            assignments);

    return error;
}

#endif