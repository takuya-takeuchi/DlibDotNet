#ifndef _CPP_SVM_KKMEANS_H_
#define _CPP_SVM_KKMEANS_H_

#include "../export.h"
#include <dlib/svm/kkmeans.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define kkmeans_new_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& kc = *static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kcentroid);\
*ret = new dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(kc);\

#define kkmeans_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_new_template_sub, __VA_ARGS__)

#define kkmeans_delete_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
delete ((dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*)kkmeans);\

#define kkmeans_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_delete_template_sub, __VA_ARGS__)

#define kkmeans_get_kernel_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
*ret = new KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>(k->get_kernel());

#define kkmeans_get_kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_get_kernel_template_sub, __VA_ARGS__)

#define kkmeans_set_number_of_centers_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
k->set_number_of_centers(num);

#define kkmeans_set_number_of_centers_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_set_number_of_centers_template_sub, __VA_ARGS__)

#define kkmeans_get_number_of_centers_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
*num = k->number_of_centers();

#define kkmeans_get_number_of_centers_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_get_number_of_centers_template_sub, __VA_ARGS__)

#define kkmeans_set_kcentroid_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
auto& kc = *static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kcentroid);\
k->set_kcentroid(kc);

#define kkmeans_set_kcentroid_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_set_kcentroid_template_sub, __VA_ARGS__)

#define kkmeans_get_kcentroid_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
*kcentroid = new dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>(k->get_kcentroid(i));

#define kkmeans_get_kcentroid_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_get_kcentroid_template_sub, __VA_ARGS__)

#define kkmeans_operator_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& k = *static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
auto& s = *static_cast<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>(sample);\
*ret = k(s);

#define kkmeans_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_operator_template_sub, __VA_ARGS__)

#define kkmeans_train_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto k = static_cast<dlib::kkmeans<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kkmeans);\
auto& tmp_centers = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
auto& tmp_samples = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_centers;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
for (int index = 0; index < tmp_centers.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp_centers[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_centers.push_back(mat);\
}\
for (int index = 0; index < tmp_samples.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp_samples[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_samples.push_back(mat);\
}\
k->train(in_samples, in_centers, max_iter);

#define kkmeans_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, kkmeans_train_template_sub, __VA_ARGS__)

#define find_clusters_using_angular_kmeans_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& tmp_centers = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
auto& tmp_samples = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
auto& tmp_result = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(result);\
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

#define pick_initial_centers_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto tmp_centers = static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(centers);\
auto tmp_samples = static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> out_centers;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
auto& tmp = *tmp_samples;\
for (int index = 0; index < tmp.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_samples.push_back(mat);\
}\
auto& in_k = *static_cast<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>*>(kernel);\
dlib::pick_initial_centers(num_centers, out_centers, in_samples, in_k, percentile);\
for (int index = 0; index < out_centers.size(); index++)\
    tmp_centers->push_back(new dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>(out_centers[index]));

#define pick_initial_centers_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, pick_initial_centers_template_sub, __VA_ARGS__)

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

DLLEXPORT int pick_initial_centers(svm_kernel_type kernel_type,
                                   const matrix_element_type type,
                                   const int templateRows,
                                   const int templateColumns,
                                   const long num_centers,
                                   void* centers,
                                   void* samples,
                                   void* kernel,
                                   const double percentile)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            pick_initial_centers_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            num_centers,
                            centers,
                            samples,
                            kernel,
                            percentile);

    return error;
}

#pragma endregion pick_initial_centers

DLLEXPORT int kkmeans_new(svm_kernel_type kernel_type,
                          matrix_element_type type,
                          const int templateRows,
                          const int templateColumns,
                          void* kcentroid,
                          void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_new_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kcentroid,
                            ret);

    return error;
}

DLLEXPORT void kkmeans_delete(svm_kernel_type kernel_type,
                              matrix_element_type type,
                              const int templateRows,
                              const int templateColumns,
                              void* kkmeans)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_delete_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans);
}

DLLEXPORT int kkmeans_get_kernel(svm_kernel_type kernel_type,
                                 matrix_element_type type,
                                 const int templateRows,
                                 const int templateColumns,
                                 void* kkmeans,
                                 void** ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_get_kernel_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            ret);

    return error;
}

DLLEXPORT int kkmeans_set_number_of_centers(svm_kernel_type kernel_type,
                                            matrix_element_type type,
                                            const int templateRows,
                                            const int templateColumns,
                                            void* kkmeans,
                                            unsigned long num)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_set_number_of_centers_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            num);

    return error;
}

DLLEXPORT int kkmeans_get_number_of_centers(svm_kernel_type kernel_type,
                                            matrix_element_type type,
                                            const int templateRows,
                                            const int templateColumns,
                                            void* kkmeans,
                                            unsigned long* num)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_get_number_of_centers_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            num);

    return error;
}

DLLEXPORT int kkmeans_set_kcentroid(svm_kernel_type kernel_type,
                                    matrix_element_type type,
                                    const int templateRows,
                                    const int templateColumns,
                                    void* kkmeans,
                                    void* kcentroid)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_set_kcentroid_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            kcentroid);

    return error;
}

DLLEXPORT int kkmeans_get_kcentroid(svm_kernel_type kernel_type,
                                    matrix_element_type type,
                                    const int templateRows,
                                    const int templateColumns,
                                    void* kkmeans,
                                    unsigned long i,
                                    void** kcentroid)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_get_kcentroid_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            i,
                            kcentroid);

    return error;
}

DLLEXPORT int kkmeans_operator(svm_kernel_type kernel_type,
                               matrix_element_type type,
                               const int templateRows,
                               const int templateColumns,
                               void* kkmeans,
                               void* sample,
                               unsigned long* ret)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_operator_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            sample,
                            ret);

    return error;
}

DLLEXPORT int kkmeans_train(svm_kernel_type kernel_type,
                            matrix_element_type type,
                            const int templateRows,
                            const int templateColumns,
                            void* kkmeans,
                            void* samples,
                            void* centers,
                            const unsigned long max_iter)
{
    int error = ERR_OK;
    
    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            kkmeans_train_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            kkmeans,
                            samples,
                            centers,
                            max_iter);

    return error;
}

#endif