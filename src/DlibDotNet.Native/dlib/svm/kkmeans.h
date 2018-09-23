#ifndef _CPP_SVM_KKMEANS_H_
#define _CPP_SVM_KKMEANS_H_

#include "../export.h"
#include <dlib/svm/kkmeans.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define ELEMENT element
#undef ELEMENT

#define find_clusters_using_angular_kmeans_template_sub(template_row, template_column) \
do {\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>& tmp_centers = *static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(centers);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>& tmp_samples = *static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(samples);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>& tmp_result = *static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(result);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>> out_centers;\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>> in_samples;\
    for (int index = 0; index < tmp_centers.size(); index++)\
    {\
        dlib::matrix<ELEMENT, template_row, template_column>* p = tmp_centers[index];\
        dlib::matrix<ELEMENT, template_row, template_column>& mat = *p;\
        out_centers.push_back(mat);\
    }\
    for (int index = 0; index < tmp_samples.size(); index++)\
    {\
        dlib::matrix<ELEMENT, template_row, template_column>* p = tmp_samples[index];\
        dlib::matrix<ELEMENT, template_row, template_column>& mat = *p;\
        in_samples.push_back(mat);\
    }\
    dlib::find_clusters_using_angular_kmeans(in_samples, out_centers, max_iter);\
    std::cout << out_centers.size() << std::endl;\
    for (int index = 0; index < out_centers.size(); index++)\
    {\
        tmp_result.push_back(new dlib::matrix<ELEMENT, template_row, template_column>(out_centers[index]));\
    }\
} while (0)

#define find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter, result) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        find_clusters_using_angular_kmeans_template_sub(0, 0);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        find_clusters_using_angular_kmeans_template_sub(0, 1);\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        find_clusters_using_angular_kmeans_template_sub(5, 1);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        find_clusters_using_angular_kmeans_template_sub(31, 1);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define nearest_center_template_sub(template_row, template_column) \
do {\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>* tmp_centers = static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(centers);\
    dlib::matrix<ELEMENT, template_row, template_column>& in_sample = *static_cast<dlib::matrix<ELEMENT, template_row, template_column>*>(sample);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>> in_centers;\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>& tmp = *tmp_centers;\
    for (int index = 0; index < tmp.size(); index++)\
    {\
        dlib::matrix<ELEMENT, template_row, template_column>* p = tmp[index];\
        dlib::matrix<ELEMENT, template_row, template_column>& mat = *p;\
        in_centers.push_back(mat);\
    }\
    *ret = dlib::nearest_center(in_centers, in_sample);\
} while (0)

#define nearest_center_template(templateRows, templateColumns, samples, centers, max_iter) \
do {\
    if (templateRows == 0 && templateColumns == 0)\
    {\
        nearest_center_template_sub(0, 0);\
    }\
    else if (templateRows == 0 && templateColumns == 1)\
    {\
        nearest_center_template_sub(0, 1);\
    }\
    else if (templateRows == 5 && templateColumns == 1)\
    {\
        nearest_center_template_sub(5, 1);\
    }\
    else if (templateRows == 31 && templateColumns == 1)\
    {\
        nearest_center_template_sub(31, 1);\
    }\
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#define pick_initial_centers_template_sub(template_row, template_column) \
do {\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>* tmp_centers = static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(centers);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>* tmp_samples = static_cast<std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>*>(samples);\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>> out_centers;\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>> in_samples;\
    std::vector<dlib::matrix<ELEMENT, template_row, template_column>*>& tmp = *tmp_samples;\
    for (int index = 0; index < tmp.size(); index++)\
    {\
        dlib::matrix<ELEMENT, template_row, template_column>* p = tmp[index];\
        dlib::matrix<ELEMENT, template_row, template_column>& mat = *p;\
        in_samples.push_back(mat);\
    }\
    dlib::linear_kernel<dlib::matrix<ELEMENT, template_row, template_column>>& in_k = *static_cast<dlib::linear_kernel<dlib::matrix<ELEMENT, template_row, template_column>>*>(k);\
    dlib::pick_initial_centers(num_centers, out_centers, in_samples, in_k, percentile);\
    for (int index = 0; index < out_centers.size(); index++)\
        tmp_centers->push_back(new dlib::matrix<ELEMENT, template_row, template_column>(out_centers[index]));\
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
    else\
    {\
        err = ERR_MATRIX_ELEMENT_TEMPLATE_SIZE_NOT_SUPPORT;\
    }\
} while (0)

#pragma endregion

#pragma region find_clusters_using_angular_kmeans

DLLEXPORT int find_clusters_using_angular_kmeans(const matrix_element_type type,
                                                 const int templateRows,
                                                 const int templateColumns,
                                                 void* centers,
                                                 void* samples,
                                                 const unsigned long max_iter,
                                                 void* result)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            find_clusters_using_angular_kmeans_template(templateRows, templateColumns, samples, centers, max_iter);
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

#pragma endregion find_clusters_using_angular_kmeans

#pragma region nearest_center

DLLEXPORT int nearest_center(const matrix_element_type type,
                             const int templateRows,
                             const int templateColumns,
                             void* centers,
                             void* sample,
                             unsigned long* ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case matrix_element_type::UInt8:
            #define ELEMENT uint8_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt16:
            #define ELEMENT uint16_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::UInt32:
            #define ELEMENT uint32_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int8:
            #define ELEMENT int8_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int16:
            #define ELEMENT int16_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Int32:
            #define ELEMENT int32_t
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Float:
            #define ELEMENT float
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
            #undef ELEMENT
            break;
        case matrix_element_type::Double:
            #define ELEMENT double
            nearest_center_template(templateRows, templateColumns, centers, sample, ret);
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

#pragma endregion nearest_center

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