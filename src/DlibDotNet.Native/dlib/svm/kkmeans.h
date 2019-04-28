#ifndef _CPP_SVM_KKMEANS_H_
#define _CPP_SVM_KKMEANS_H_

#include "../export.h"
#include <dlib/svm/kkmeans.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define find_clusters_using_angular_kmeans_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
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
}

#define nearest_center_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
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
*ret = dlib::nearest_center(in_centers, in_sample);

#define pick_initial_centers_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
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
    tmp_centers->push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(out_centers[index]));

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
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size3_template,
                            find_clusters_using_angular_kmeans_template,
                            templateRows,
                            templateColumns,
                            samples,
                            centers,
                            max_iter,
                            result);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size3_template,
                            nearest_center_template,
                            templateRows,
                            templateColumns,
                            centers,
                            sample,
                            ret);

    return error;
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
    int error = ERR_OK;

    matrix_numeric_template(type,
                            error,
                            matrix_template_size3_template,
                            pick_initial_centers_template,
                            templateRows,
                            templateColumns,
                            num_centers,
                            centers,
                            samples,
                            k,
                            percentile);

    return error;
}

#pragma endregion pick_initial_centers

#endif