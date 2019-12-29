#ifndef _CPP_CLUSTERING_SPECTRAL_CLUSTER_H_
#define _CPP_CLUSTERING_SPECTRAL_CLUSTER_H_

#include "../export.h"
#include <dlib/clustering/spectral_cluster.h>
#include "../svm/template.h"
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define spectral_cluster_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
auto& s = *(static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples));\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
for (int index = 0; index < s.size(); index++)\
{\
    auto& m = *(s.at(index));\
    in_samples.push_back(m);\
}\
auto r = dlib::spectral_cluster(k, in_samples, num_clusters);\
auto out_ret = new std::vector<unsigned long>();\
for (int index = 0; index < r.size(); index++)\
    out_ret->push_back(r[index]);\
*ret = out_ret;

#define spectral_cluster_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, spectral_cluster_template_sub, __VA_ARGS__)

#pragma endregion template

DLLEXPORT int spectral_cluster(svm_kernel_type kernel_type,
                               matrix_element_type type,
                               const int templateRows,
                               const int templateColumns,
                               void* kernel,
                               void* samples,
                               const unsigned long num_clusters,
                               std::vector<unsigned long>** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            spectral_cluster_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kernel,
                            samples,
                            num_clusters,
                            ret);

    return error;
}

#endif