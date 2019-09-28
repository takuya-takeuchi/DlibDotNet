#ifndef _CPP_VALIDATION_H_
#define _CPP_VALIDATION_H_

#include <dlib/dnn.h>
#include <vector>

#include "loss/mmod/defines.h"
#include "../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define test_object_detection_function_template(net, __TYPE__, matrix_vector, truth_dets, overlap_tester, adjust_threshold, overlaps_ignore_tester, ret) \
do {\
    std::vector<dlib::matrix<__TYPE__>*>& matvec_tmp = *(static_cast<std::vector<dlib::matrix<__TYPE__>*>*>(matrix_vector));\
    std::vector<dlib::matrix<__TYPE__>> in_matvec;\
    for (size_t i = 0; i< matvec_tmp.size(); i++)\
    {\
        dlib::matrix<__TYPE__>& mat = *matvec_tmp[i];\
        in_matvec.push_back(mat);\
    }\
\
    std::vector<std::vector<dlib::mmod_rect*>*>& tmp_dets = *(static_cast<std::vector<std::vector<dlib::mmod_rect*>*>*>(truth_dets));\
    std::vector<std::vector<dlib::mmod_rect>> in_dets;\
    for (size_t i = 0; i< tmp_dets.size(); i++)\
    {\
        std::vector<dlib::mmod_rect*>& v = *(tmp_dets[i]);\
        std::vector<dlib::mmod_rect> tmp_v;\
        for (size_t j = 0; j < v.size(); j++)\
        {\
            mmod_rect& r = *(v[j]);\
            tmp_v.push_back(r);\
        }\
        in_dets.push_back(tmp_v);\
    }\
\
    const auto& ot = *overlap_tester;\
    const auto& oit = *overlaps_ignore_tester;\
    const auto r = dlib::test_object_detection_function(net,\
                                                        in_matvec,\
                                                        in_dets,\
                                                        ot,\
                                                        adjust_threshold,\
                                                        oit);\
    *ret = new dlib::matrix<double,1,3>(r);\
} while (0)

#pragma endregion template

DLLEXPORT int test_object_detection_function_net(const int type,
                                                 void* detector,
                                                 matrix_element_type element_type,
                                                 void* matrix_vector,
                                                 int templateRows,
                                                 int templateColumns,
                                                 std::vector<std::vector<dlib::mmod_rect*>*>* truth_dets,
                                                 test_box_overlap* overlap_tester,
                                                 const double adjust_threshold,
                                                 test_box_overlap* overlaps_ignore_tester,
                                                 dlib::matrix<double,1,3>** ret)
{
    int err = ERR_OK;

    // Check type argument and cast to the proper type
    try
    {
        switch(type)
        {
            case 0:
                {
                    net_type& net = *(static_cast<net_type*>(detector));
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            {
                                test_object_detection_function_template(net,
                                                                        rgb_pixel,
                                                                        matrix_vector,
                                                                        truth_dets,
                                                                        overlap_tester,
                                                                        adjust_threshold,
                                                                        overlaps_ignore_tester,
                                                                        ret);
                            }
                            break;
                        case matrix_element_type::UInt8:
                        case matrix_element_type::UInt16:
                        case matrix_element_type::UInt32:
                        case matrix_element_type::Int8:
                        case matrix_element_type::Int16:
                        case matrix_element_type::Int32:
                        case matrix_element_type::Float:
                        case matrix_element_type::Double:
                        case matrix_element_type::BgrPixel:
                        case matrix_element_type::HsiPixel:
                        case matrix_element_type::RgbAlphaPixel:
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
            case 1:
                {
                    net_type_1& net = *(static_cast<net_type_1*>(detector));
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            {
                                test_object_detection_function_template(net,
                                                                        rgb_pixel,
                                                                        matrix_vector,
                                                                        truth_dets,
                                                                        overlap_tester,
                                                                        adjust_threshold,
                                                                        overlaps_ignore_tester,
                                                                        ret);
                            }
                            break;
                        case matrix_element_type::UInt8:
                        case matrix_element_type::UInt16:
                        case matrix_element_type::UInt32:
                        case matrix_element_type::Int8:
                        case matrix_element_type::Int16:
                        case matrix_element_type::Int32:
                        case matrix_element_type::Float:
                        case matrix_element_type::Double:
                        case matrix_element_type::BgrPixel:
                        case matrix_element_type::HsiPixel:
                        case matrix_element_type::RgbAlphaPixel:
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
            case 2:
                {
                    net_type_2& net = *(static_cast<net_type_2*>(detector));
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            {
                                test_object_detection_function_template(net,
                                                                        rgb_pixel,
                                                                        matrix_vector,
                                                                        truth_dets,
                                                                        overlap_tester,
                                                                        adjust_threshold,
                                                                        overlaps_ignore_tester,
                                                                        ret);
                            }
                            break;
                        case matrix_element_type::UInt8:
                        case matrix_element_type::UInt16:
                        case matrix_element_type::UInt32:
                        case matrix_element_type::Int8:
                        case matrix_element_type::Int16:
                        case matrix_element_type::Int32:
                        case matrix_element_type::Float:
                        case matrix_element_type::Double:
                        case matrix_element_type::BgrPixel:
                        case matrix_element_type::HsiPixel:
                        case matrix_element_type::RgbAlphaPixel:
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
            case 3:
                {
                    net_type_3& net = *(static_cast<net_type_3*>(detector));
                    switch(element_type)
                    {
                        case matrix_element_type::RgbPixel:
                            {
                                test_object_detection_function_template(net,
                                                                        rgb_pixel,
                                                                        matrix_vector,
                                                                        truth_dets,
                                                                        overlap_tester,
                                                                        adjust_threshold,
                                                                        overlaps_ignore_tester,
                                                                        ret);
                            }
                            break;
                        case matrix_element_type::UInt8:
                        case matrix_element_type::UInt16:
                        case matrix_element_type::UInt32:
                        case matrix_element_type::Int8:
                        case matrix_element_type::Int16:
                        case matrix_element_type::Int32:
                        case matrix_element_type::Float:
                        case matrix_element_type::Double:
                        case matrix_element_type::BgrPixel:
                        case matrix_element_type::HsiPixel:
                        case matrix_element_type::RgbAlphaPixel:
                        default:
                            err = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;
                            break;
                    }
                }
                break;
        }
    }
    catch(dlib::cuda_error ce)
    {
        cuda_error_to_error_code(ce, err);
    }

    return err;
}

#endif