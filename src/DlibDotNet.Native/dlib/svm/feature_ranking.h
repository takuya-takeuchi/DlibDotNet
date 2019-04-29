#ifndef _CPP_SVM_FEATURE_RANKING_H_
#define _CPP_SVM_FEATURE_RANKING_H_

#include "../export.h"
#include <dlib/svm/feature_ranking.h>
#include "template.h"
#include "../shared.h"

using namespace dlib;

#pragma region template

#define rank_features_template_sub(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, KERNEL, ...) \
auto& kc = *static_cast<dlib::kcentroid<KERNEL<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>>>*>(kcentroid);\
auto& tmp_labels = *static_cast<std::vector<__TYPE__>*>(labels);\
auto& tmp_samples = *static_cast<std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>*>*>(samples);\
std::vector<__TYPE__> in_labels;\
std::vector<dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>> in_samples;\
for (int index = 0; index < tmp_labels.size(); index++)\
{\
    in_labels.push_back(tmp_labels[index]);\
}\
for (int index = 0; index < tmp_samples.size(); index++)\
{\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>* p = tmp_samples[index];\
    dlib::matrix<__TYPE__, __ROWS__, __COLUMNS__>& mat = *p;\
    in_samples.push_back(mat);\
}\
auto r = dlib::rank_features(kc, in_samples, in_labels);\
*ret = new dlib::matrix<__TYPE__, 0, 2>(r);\

#define rank_features_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
kernel_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, kernel_type, rank_features_template_sub, __VA_ARGS__)

#pragma endregion template

DLLEXPORT int rank_features(const kernel_type kernel_type,
                            const matrix_element_type type,
                            const int templateRows,
                            const int templateColumns,
                            void* kcentroid,
                            void* samples,
                            void* labels,
                            void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size_column1or0_template,
                            rank_features_template,
                            templateRows,
                            templateColumns,
                            kernel_type,
                            samples,
                            labels,
                            ret);

    return error;
}

#endif