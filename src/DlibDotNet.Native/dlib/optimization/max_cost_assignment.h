#ifndef _CPP_MAX_COST_ASSIGNMENT_H_
#define _CPP_MAX_COST_ASSIGNMENT_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/optimization/max_cost_assignment.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#define ARRAY2D_ELEMENT element
#undef ARRAY2D_ELEMENT

#define assignment_cost_template(ret, cost, assignment)\
do {\
    dlib::matrix<ARRAY2D_ELEMENT>& m = *(static_cast<dlib::matrix<ARRAY2D_ELEMENT>*>(cost));\
    std::vector<long> tmp;\
    for (int i = 0, cnt = assignment->size() ; i < cnt; i++ )\
        tmp.push_back((*assignment)[i]);\
    *((ARRAY2D_ELEMENT*)ret) = dlib::assignment_cost(m, tmp);\
} while (0)

#define max_cost_assignment_template(cost, assignments)\
do {\
    dlib::matrix<ARRAY2D_ELEMENT>& m = *(static_cast<dlib::matrix<ARRAY2D_ELEMENT>*>(cost));\
    std::vector<long> ret = dlib::max_cost_assignment(m);\
    for (int i = 0, cnt = ret.size() ; i < cnt; i++ )\
    {\
        assignments->push_back(ret[i]);\
    }\
} while (0)

DLLEXPORT int assignment_cost(matrix_element_type type, void* cost, std::vector<int64_t>* assignment, void* ret)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ARRAY2D_ELEMENT uint32_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ARRAY2D_ELEMENT int8_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ARRAY2D_ELEMENT int16_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ARRAY2D_ELEMENT int32_t
            assignment_cost_template(ret, cost, assignment);
            #undef ARRAY2D_ELEMENT
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

DLLEXPORT int max_cost_assignment(matrix_element_type type, void* cost, std::vector<int64_t> *assignments)
{
    int err = ERR_OK;
    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ARRAY2D_ELEMENT uint8_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ARRAY2D_ELEMENT uint16_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ARRAY2D_ELEMENT uint32_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ARRAY2D_ELEMENT int8_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ARRAY2D_ELEMENT int16_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ARRAY2D_ELEMENT int32_t
            max_cost_assignment_template(cost, assignments);
            #undef ARRAY2D_ELEMENT
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