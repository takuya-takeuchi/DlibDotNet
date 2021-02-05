#ifndef _CPP_SHAPE_PREDICTOR_H_
#define _CPP_SHAPE_PREDICTOR_H_

#include "../export.h"
#include <dlib/image_processing/shape_predictor.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define shape_predictor_operator_template(__TYPE__, error, type, ...) \
auto& d = *predictor;\
auto result = d(*((array2d<__TYPE__>*)img), *rect);\
*full_obj_detect = new full_object_detection(result);\

#define shape_predictor_matrix_operator_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& d = *predictor;\
auto result = d(*((matrix<__TYPE__, __ROWS__, __COLUMNS__>*)img), *rect);\
*full_obj_detect = new full_object_detection(result);\

#define shape_predictor_test_shape_predictor_template(__TYPE__, error, type, ...) \
auto& d = *predictor;\
std::vector<std::vector<full_object_detection>> in_Objects;\
std::vector<std::vector<double>> in_Scales;\
vector_vector_pointer_to_value(full_object_detection, objects, in_Objects);\
auto& in_images = *static_cast<dlib::array<array2d<__TYPE__>>*>(images);\
vector_vector_valueType_to_value(double, scales, in_Scales);\
double rd = dlib::test_shape_predictor(d, in_images, in_Objects, in_Scales);\
*ret = rd;\

#pragma endregion template

DLLEXPORT shape_predictor* shape_predictor_new()
{
    return new shape_predictor();
}

DLLEXPORT void shape_predictor_delete(shape_predictor* obj)
{
	delete obj;
}

DLLEXPORT int serialize_shape_predictor(shape_predictor* predictor,
                                        const char* file_name,
                                        const int file_name_length,
                                        std::string** error_message)
{
    int err = ERR_OK;

    try
    {
        std::string str(file_name, file_name_length);
        dlib::serialize(str) << (*predictor);
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return err;
}

DLLEXPORT int deserialize_shape_predictor(const char* file_name,
                                          const int file_name_length,
                                          shape_predictor** ret,
                                          std::string** error_message)
{
    int err = ERR_OK;
    *ret = nullptr;

    try
    {
        std::string str(file_name, file_name_length);
        shape_predictor* predictor = new shape_predictor();
        dlib::deserialize(str) >> (*predictor);
        *ret = predictor;
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return err;
}

// DLLEXPORT int deserialize_shape_predictor2(const char* item,
//                                            const int item_length,
//                                            shape_predictor** ret,
//                                            std::string** error_message)
// {
//     int err = ERR_OK;
//     *ret = nullptr;

//     try
//     {
//         std::vector<char> vec(item, item + item_length);
//         vector_streambuf buf(vec);
//         std::istream stream(&buf);
//         shape_predictor* predictor = new shape_predictor();
//         dlib::deserialize((*predictor), stream);
//         *ret = predictor;
//     }
//     catch (serialization_error& e)
//     {
//         err = ERR_GENERAL_SERIALIZATION;
//         *error_message = new std::string(e.what());
//     }

//     return err;
// }

DLLEXPORT int deserialize_shape_predictor2(const char* item,
                                           const int item_length,
                                           shape_predictor** ret,
                                           std::string** error_message)
{
    int err = ERR_OK;
    *ret = nullptr;

    try
    {
        std::string s(item, item_length);
        std::istringstream iss(s);
        std::istream& stream = iss;
        shape_predictor* predictor = new shape_predictor();
        dlib::deserialize((*predictor), stream);
        *ret = predictor;
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return err;
}

DLLEXPORT int deserialize_shape_predictor_proxy(proxy_deserialize* proxy,
                                                shape_predictor** ret,
                                                std::string** error_message)
{
    int err = ERR_OK;
    *ret = nullptr;

    try
    {
        proxy_deserialize& p = *static_cast<proxy_deserialize*>(proxy);
        shape_predictor* predictor = new shape_predictor();
        p >> (*predictor);
        *ret = predictor;
    }
    catch (serialization_error& e)
    {
        err = ERR_GENERAL_SERIALIZATION;
        *error_message = new std::string(e.what());
    }

    return err;
}

DLLEXPORT dlib::point_transform_affine* normalizing_tform(dlib::rectangle* rect)
{
    dlib::rectangle& r = *static_cast<dlib::rectangle*>(rect);

    auto ret = impl::normalizing_tform(r);
    return new dlib::point_transform_affine(ret);
}

#pragma region shape_predictor_operator

DLLEXPORT int shape_predictor_operator(shape_predictor* predictor,
                                       array2d_type type,
                                       void* img,
                                       rectangle* rect,
                                       full_object_detection** full_obj_detect)
{
    int error = ERR_OK;
    *full_obj_detect = nullptr;

    array2d_template(type,
                     error,
                     shape_predictor_operator_template,
                     predictor,
                     img,
                     rect,
                     full_obj_detect);

    return error;
}

DLLEXPORT int shape_predictor_matrix_operator(shape_predictor* predictor,
                                              matrix_element_type type,
                                              void* img,
                                              rectangle* rect,
                                              full_object_detection** full_obj_detect)
{
    int error = ERR_OK;
    *full_obj_detect = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    shape_predictor_matrix_operator_template,
                    0,
                    0,
                    predictor,
                    img,
                    rect,
                    full_obj_detect);

    return error;
}

DLLEXPORT int shape_predictor_operator_mmod_rect(shape_predictor* predictor,
                                                 array2d_type type,
                                                 void* img,
                                                 mmod_rect* rect,
                                                 full_object_detection** full_obj_detect)
{
    int error = ERR_OK;
    *full_obj_detect = nullptr;

    array2d_template(type,
                     error,
                     shape_predictor_operator_template,
                     predictor,
                     img,
                     rect,
                     full_obj_detect);

    return error;
}

DLLEXPORT int shape_predictor_matrix_operator_mmod_rect(shape_predictor* predictor,
                                                        matrix_element_type type,
                                                        void* img,
                                                        mmod_rect* rect,
                                                        full_object_detection** full_obj_detect)
{
    int error = ERR_OK;
    *full_obj_detect = nullptr;

    matrix_template(type,
                    error,
                    matrix_template_size_template,
                    shape_predictor_matrix_operator_template,
                    0,
                    0,
                    predictor,
                    img,
                    rect,
                    full_obj_detect);

    return error;
}

#pragma endregion shape_predictor_operator

DLLEXPORT unsigned int shape_predictor_num_parts(shape_predictor* predictor)
{
    return predictor->num_parts();
}

DLLEXPORT unsigned int shape_predictor_num_features(shape_predictor* predictor)
{
    return predictor->num_features();
}

DLLEXPORT int shape_predictor_test_shape_predictor(shape_predictor* predictor,
                                                   array2d_type type,
                                                   void* images,
                                                   std::vector<std::vector<full_object_detection*>*>* objects,
                                                   std::vector<std::vector<double>*>* scales,
                                                   double* ret)
{
    int error = ERR_OK;

    array2d_template(type,
                     error,
                     shape_predictor_test_shape_predictor_template,
                     predictor,
                     images,
                     objects,
                     scales,
                     ret);

    return error;
}

#endif