#ifndef _CPP_STATISTICS_VECTOR_NORMALIZER_H_
#define _CPP_STATISTICS_VECTOR_NORMALIZER_H_

#include "../../export.h"
#include <dlib/statistics/statistics.h>
#include "../../template.h"
#include "../../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define vector_normalizer_new_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
ret = new dlib::vector_normalizer<dlib::matrix<__TYPE__>>();

#define vector_normalizer_delete_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
delete ((dlib::vector_normalizer<dlib::matrix<__TYPE__>>*)normalizer);

#define vector_normalizer_train_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
std::vector<dlib::matrix<__TYPE__>> dst;\
auto& tmp_src = *static_cast<std::vector<dlib::matrix<__TYPE__>*>*>(samples);\
dst.reserve(tmp_src.size());\
for (int index = 0; index < tmp_src.size(); index++)\
{\
    auto& tmp = *tmp_src.at(index);\
    dst.push_back(tmp);\
}\
tmp_normalizer->train(dst);\

#define vector_normalizer_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
auto& x = *static_cast<dlib::matrix<__TYPE__>*>(matrix);\
auto r = tmp_normalizer->operator()(x);\
*ret = new dlib::matrix<__TYPE__>(r);

#define vector_normalizer_in_vector_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
*ret = tmp_normalizer->in_vector_size();

#define vector_normalizer_out_vector_size_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
*ret = tmp_normalizer->out_vector_size();

#define vector_normalizer_means_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
const auto r = tmp_normalizer->means();\
*ret = new dlib::matrix<__TYPE__>(r);

#define vector_normalizer_std_devs_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto tmp_normalizer = static_cast<dlib::vector_normalizer<dlib::matrix<__TYPE__>>*>(normalizer);\
const auto r = tmp_normalizer->std_devs();\
*ret = new dlib::matrix<__TYPE__>(r);

#pragma endregion template

DLLEXPORT void* vector_normalizer_new(matrix_element_type type)
{
    int error = ERR_OK;
    void* ret = nullptr;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_new_template,
                            0,
                            0);

    return ret;
}

DLLEXPORT void vector_normalizer_delete(matrix_element_type type, void* normalizer)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_delete_template,
                            0,
                            0);
}

DLLEXPORT void vector_normalizer_train(matrix_element_type type,
                                       void* normalizer,
                                       void* samples)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_train_template,
                            0,
                            0,
                            normalizer,
                            samples);
}

DLLEXPORT void* vector_normalizer_operator(matrix_element_type type,
                                           void* normalizer,
                                           void* matrix,
                                           void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_operator_template,
                            0,
                            0,
                            normalizer,
                            matrix,
                            ret);

    return ret;
}

DLLEXPORT void* vector_normalizer_in_vector_size(matrix_element_type type,
                                                 void* normalizer,
                                                 int64_t* ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_in_vector_size_template,
                            0,
                            0,
                            normalizer,
                            ret);

    return ret;
}

DLLEXPORT void* vector_normalizer_out_vector_size(matrix_element_type type,
                                                  void* normalizer,
                                                  int64_t* ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_out_vector_size_template,
                            0,
                            0,
                            normalizer,
                            ret);

    return ret;
}

DLLEXPORT void* vector_normalizer_means(matrix_element_type type,
                                        void* normalizer,
                                        void* matrix,
                                        void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_means_template,
                            0,
                            0,
                            normalizer,
                            matrix,
                            ret);

    return ret;
}

DLLEXPORT void* vector_normalizer_std_devs(matrix_element_type type,
                                           void* normalizer,
                                           void* matrix,
                                           void** ret)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size00_template,
                            vector_normalizer_std_devs_template,
                            0,
                            0,
                            normalizer,
                            matrix,
                            ret);

    return ret;
}

#endif