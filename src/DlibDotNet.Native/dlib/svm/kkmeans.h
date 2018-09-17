#ifndef _CPP_SVM_KKMEANS_H_
#define _CPP_SVM_KKMEANS_H_

#include "../export.h"
#include <dlib/svm/kkmeans.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define pick_initial_centers_template_sub(teplate_row, template_column) \
do {\
    std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>*>* tmp_centers = static_cast<std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>*>*>(centers);\
    std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>*>* tmp_samples = static_cast<std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>*>*>(samples);\
    std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>> out_centers;\
    std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>> in_samples;\
    std::vector<dlib::matrix<ELEMENT, teplate_row, template_column>*>& tmp = *tmp_samples;\
    for (int index = 0; index < tmp.size(); index++)\
    {\
        dlib::matrix<ELEMENT, teplate_row, template_column>* p = tmp[index];\
        dlib::matrix<ELEMENT, teplate_row, template_column>& mat = *p;\
        in_samples.push_back(mat);\
    }\
    dlib::linear_kernel<dlib::matrix<ELEMENT, teplate_row, template_column>>& in_k = *static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT, teplate_row, template_column>>*>(k);\
    dlib::pick_initial_centers(num_centers, out_centers, in_samples, in_k, percentile);\
    for (int index = 0; index < out_centers.size(); index++)\
        tmp_centers->push_back(new dlib::matrix<ELEMENT, teplate_row, template_column>(out_centers[index]));\
} while (0)

#define pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        pick_initial_centers_template_sub(0, 0);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        pick_initial_centers_template_sub(0, 1);\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        pick_initial_centers_template_sub(5, 1);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        pick_initial_centers_template_sub(31, 1);\
    }\
    err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
} while (0)

#pragma endregion

#pragma region pick_initial_centers

DLLEXPORT int pick_initial_centers(const matrix_element_type type,
                                   const int templateRows,
                                   const int templateColumns,
                                   const long num_centers, 
                                   void* centers, 
                                   void* samples, 
                                   void* k, 
                                   const double percentile)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            pick_initial_centers_template(templateRows, templateColumns, num_centers, centers, samples, k, percentile);
            #undef ELEMENT
            break;
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

#pragma endregion pick_initial_centers

#endif