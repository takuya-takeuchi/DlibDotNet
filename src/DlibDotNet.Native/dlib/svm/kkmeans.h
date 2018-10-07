#ifndef _CPP_SVM_KKMEANS_H_
#define _CPP_SVM_KKMEANS_H_

#include "../export.h"
#include <dlib/svm/kkmeans.h>
#include "../shared.h"

using namespace dlib;

#pragma region template

#define find_clusters_using_angular_kmeans_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, samples, centers, max_iter, result) \
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp_centers = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp_samples = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp_result = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(result);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> out_centers;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
for (int index = 0; index < tmp_centers.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp_centers[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    out_centers.push_back(mat);\
}\
for (int index = 0; index < tmp_samples.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp_samples[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_samples.push_back(mat);\
}\
dlib::find_clusters_using_angular_kmeans(in_samples, out_centers, max_iter);\
std::cout << out_centers.size() << std::endl;\
for (int index = 0; index < out_centers.size(); index++)\
{\
    tmp_result.push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(out_centers[index]));\
}\

#define find_clusters_using_angular_kmeans_template(__TYPE__, __ROWS__, __COLUMNS__, error, samples, centers, max_iter, result) \
do {\
    matrix_template_size_arg4_template(__TYPE__, __ROWS__, __COLUMNS__, find_clusters_using_angular_kmeans_template_sub, error, samples, centers, max_iter, result);\
} while (0)

#define nearest_center_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, centers, sample, ret) \
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>* tmp_centers = static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& in_sample = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_centers;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp = *tmp_centers;\
for (int index = 0; index < tmp.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_centers.push_back(mat);\
}\
*ret = dlib::nearest_center(in_centers, in_sample);\

#define nearest_center_template(__TYPE__, __ROWS__, __COLUMNS__, error, centers, sample, ret) \
do {\
    matrix_template_size_arg3_template(__TYPE__, __ROWS__, __COLUMNS__, nearest_center_template_sub, error, centers, sample, ret);\
} while (0)

#define pick_initial_centers_template_sub(__TYPE__, __ROWS__, __COLUMNS__, error, num_centers, centers, samples, k, percentile) \
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>* tmp_centers = static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>* tmp_samples = static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> out_centers;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>& tmp = *tmp_samples;\
for (int index = 0; index < tmp.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_samples.push_back(mat);\
}\
dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>& in_k = *static_cast<dlib::linear_kernel<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(k);\
dlib::pick_initial_centers(num_centers, out_centers, in_samples, in_k, percentile);\
for (int index = 0; index < out_centers.size(); index++)\
    tmp_centers->push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(out_centers[index]));\

#define pick_initial_centers_template(__TYPE__, __ROWS__, __COLUMNS__, error, num_centers, centers, samples, k, percentile) \
do {\
    matrix_template_size_one_column_vector_arg5_template(__TYPE__, __ROWS__, __COLUMNS__, pick_initial_centers_template_sub, error, num_centers, centers, samples, k, percentile);\
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
            find_clusters_using_angular_kmeans_template(uint8_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::UInt16:
            find_clusters_using_angular_kmeans_template(uint16_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::UInt32:
            find_clusters_using_angular_kmeans_template(uint32_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::Int8:
            find_clusters_using_angular_kmeans_template(int8_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::Int16:
            find_clusters_using_angular_kmeans_template(int16_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::Int32:
            find_clusters_using_angular_kmeans_template(int32_t, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::Float:
            find_clusters_using_angular_kmeans_template(float, templateRows, templateColumns, err, samples, centers, max_iter, result);
            break;
        case matrix_element_type::Double:
            find_clusters_using_angular_kmeans_template(double, templateRows, templateColumns, err, samples, centers, max_iter, result);
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
            nearest_center_template(uint8_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::UInt16:
            nearest_center_template(uint16_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::UInt32:
            nearest_center_template(uint32_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::Int8:
            nearest_center_template(int8_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::Int16:
            nearest_center_template(int16_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::Int32:
            nearest_center_template(int32_t, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::Float:
            nearest_center_template(float, templateRows, templateColumns, err, centers, sample, ret);
            break;
        case matrix_element_type::Double:
            nearest_center_template(double, templateRows, templateColumns, err, centers, sample, ret);
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
            pick_initial_centers_template(uint8_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::UInt16:
            pick_initial_centers_template(uint16_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::UInt32:
            pick_initial_centers_template(uint32_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::Int8:
            pick_initial_centers_template(int8_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::Int16:
            pick_initial_centers_template(int16_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::Int32:
            pick_initial_centers_template(int32_t, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::Float:
            pick_initial_centers_template(float, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
            break;
        case matrix_element_type::Double:
            pick_initial_centers_template(double, templateRows, templateColumns, err, num_centers, centers, samples, k, percentile);
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

#pragma endregion pick_initial_centers

#endif