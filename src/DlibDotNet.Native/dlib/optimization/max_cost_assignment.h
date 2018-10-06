#ifndef _CPP_MAX_COST_ASSIGNMENT_H_
#define _CPP_MAX_COST_ASSIGNMENT_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/optimization/max_cost_assignment.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define assignment_cost_template(__TYPE__, ret, cost, assignment)\
do {\
    dlib::matrix<__TYPE__>& m = *(static_cast<dlib::matrix<__TYPE__>*>(cost));\
    std::vector<long> tmp;\
    for (int i = 0, cnt = assignment->size() ; i < cnt; i++ )\
        tmp.push_back((*assignment)[i]);\
    *((__TYPE__*)ret) = dlib::assignment_cost(m, tmp);\
} while (0)

#define max_cost_assignment_template(__TYPE__, cost, assignments)\
do {\
    dlib::matrix<__TYPE__>& m = *(static_cast<dlib::matrix<__TYPE__>*>(cost));\
    std::vector<long> ret = dlib::max_cost_assignment(m);\
    for (int i = 0, cnt = ret.size() ; i < cnt; i++ )\
    {\
        assignments->push_back(ret[i]);\
    }\
} while (0)

#pragma endregion template

DLLEXPORT int assignment_cost(matrix_element_type type, void* cost, std::vector<int64_t>* assignment, void* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            assignment_cost_template(uint8_t, ret, cost, assignment);
            break;
        case matrix_element_type::UInt16:
            assignment_cost_template(uint16_t, ret, cost, assignment);
            break;
        case matrix_element_type::UInt32:
            assignment_cost_template(uint32_t, ret, cost, assignment);
            break;
        case matrix_element_type::Int8:
            assignment_cost_template(int8_t, ret, cost, assignment);
            break;
        case matrix_element_type::Int16:
            assignment_cost_template(int16_t, ret, cost, assignment);
            break;
        case matrix_element_type::Int32:
            assignment_cost_template(int32_t, ret, cost, assignment);
            break;
        case matrix_element_type::Float:
            assignment_cost_template(float, ret, cost, assignment);
            break;
        case matrix_element_type::Double:
            assignment_cost_template(double, ret, cost, assignment);
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int max_cost_assignment(matrix_element_type type, void* cost, std::vector<int64_t> *assignments)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            max_cost_assignment_template(uint8_t, cost, assignments);
            break;
        case matrix_element_type::UInt16:
            max_cost_assignment_template(uint16_t, cost, assignments);
            break;
        case matrix_element_type::UInt32:
            max_cost_assignment_template(uint32_t, cost, assignments);
            break;
        case matrix_element_type::UInt64:
            max_cost_assignment_template(uint64_t, cost, assignments);
            break;
        case matrix_element_type::Int8:
            max_cost_assignment_template(int8_t, cost, assignments);
            break;
        case matrix_element_type::Int16:
            max_cost_assignment_template(int16_t, cost, assignments);
            break;
        case matrix_element_type::Int32:
            max_cost_assignment_template(int32_t, cost, assignments);
            break;
        case matrix_element_type::Int64:
            max_cost_assignment_template(int64_t, cost, assignments);
            break;
        case matrix_element_type::Float:
        case matrix_element_type::Double:
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#endif