#ifndef _CPP_FHOG_H_
#define _CPP_FHOG_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/image_processing/full_object_detection_abstract.h>
#include <dlib/image_processing/scan_fhog_pyramid.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms.h>
#include <dlib/svm/cross_validate_object_detection_trainer.h>
#include <dlib/svm/structural_object_detection_trainer.h>
#include <dlib/svm/svm.h>
#include "../template.h"
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define PYRAMID_TYPE PYRAMID_TYPE
#define EXTRACTOR_TYPE EXTRACTOR_TYPE
#undef EXTRACTOR_TYPE
#undef PYRAMID_TYPE

#define extract_fhog_features_template(__TYPE__, error, type, ...) \
auto& in_ = *((array2d<__TYPE__>*)img);\
switch(hog_type)\
{\
    case matrix_element_type::Float:\
        dlib::extract_fhog_features(in_, *((array2d<matrix<float, 31, 1>>*)hog), cell_size, filter_rows_padding, filter_cols_padding);\
        break;\
    case matrix_element_type::Double:\
        dlib::extract_fhog_features(in_, *((array2d<matrix<double, 31, 1>>*)hog), cell_size, filter_rows_padding, filter_cols_padding);\
        break;\
    default:\
        error = ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT;\
        break;\
}\

#define extract_fhog_features2_template(__TYPE__, error, type, ...) \
auto& in_ = *((array2d<__TYPE__>*)img);\
auto result = dlib::extract_fhog_features(in_, cell_size, filter_rows_padding, filter_cols_padding);\
*hog = new dlib::matrix<double, 0, 1>(result);\

#define draw_fhog_matrix_template(__TYPE__, error, __ELEMENT_TYPE__, __ROWS__, __COLUMNS__, ...) \
auto& in_ = *((array2d<matrix<__TYPE__, 31, 1>>*)hog);\
auto mat = dlib::draw_fhog(in_, cell_draw_size, min_response_threshold);\
*out_matrix = new dlib::matrix<uint8_t>(mat);

#define extract_fhog_features_array_template(__TYPE__, error, type, __SUBTYPE__, subtype, ...) \
auto& in_ = *((array2d<__TYPE__>*)img);\
dlib::array<array2d<__SUBTYPE__>> in_tmp;\
dlib::extract_fhog_features(in_, in_tmp, cell_size, filter_rows_padding, filter_cols_padding);\
array_copy(__SUBTYPE__, in_tmp, hog);\

#pragma endregion template

DLLEXPORT int extract_fhog_features(array2d_type img_type,
                                    void* img,
                                    matrix_element_type hog_type,
                                    void* hog,
                                    int cell_size,
                                    int filter_rows_padding,
                                    int filter_cols_padding)
{
    int error = ERR_OK;

    auto type = img_type;

    array2d_template(type,
                     error,
                     extract_fhog_features_template,
                     img,
                     hog_type,
                     hog,
                     cell_size,
                     filter_rows_padding,
                     filter_cols_padding);

    return error;
}

DLLEXPORT int extract_fhog_features2(array2d_type img_type,
                                     void* img,
                                     matrix_element_type hog_type,
                                     int cell_size,
                                     int filter_rows_padding,
                                     int filter_cols_padding,
                                     void** hog)
{
    int error = ERR_OK;

    auto type = img_type;

    array2d_template(type,
                     error,
                     extract_fhog_features2_template,
                     img,
                     hog_type,
                     cell_size,
                     filter_rows_padding,
                     filter_cols_padding,
                     hog);

    return error;
}

DLLEXPORT int extract_fhog_features_array(array2d_type img_type,
                                          void* img,
                                          array2d_type hog_type,
                                          void* hog,
                                          int cell_size,
                                          int filter_rows_padding,
                                          int filter_cols_padding)
{
    int error = ERR_OK;

    auto type = img_type;
    auto subtype = hog_type;

    array2d_inout_in_template(type,
                              error,
                              array2d_decimal_inout_out_template,
                              extract_fhog_features_array_template,
                              subtype,
                              img,
                              hog,
                              cell_size,
                              filter_rows_padding,
                              filter_cols_padding);

    return error;
}

DLLEXPORT point* image_to_fhog(point* p, int cell_size, int filter_rows_padding, int filter_cols_padding)
{
    auto ret = dlib::image_to_fhog(*p, cell_size, filter_rows_padding, filter_cols_padding);
    return new point(ret);
}

#pragma region draw_fhog

DLLEXPORT int draw_fhog(const matrix_element_type type,
                        const void* hog,
                        const int cell_draw_size,
                        const float min_response_threshold,
                        void** out_matrix)
{
    int error = ERR_OK;

    matrix_decimal_template(type,
                            error,
                            matrix_template_size_template,
                            draw_fhog_matrix_template,
                            0,
                            0,
                            hog,
                            cell_draw_size,
                            min_response_threshold,
                            out_matrix);

    return error;
}

DLLEXPORT int draw_fhog_object_detector_scan_fhog_pyramid(const pyramid_type pyramid_type,
                                                          const unsigned int pyramid_rate,
                                                          const fhog_feature_extractor_type extractor_type,
                                                          void* detector,
                                                          const unsigned int weight_index,
                                                          const int cell_draw_size,
                                                          void** out_matrix)
{
    int error = ERR_OK;

    switch(pyramid_type)
    {
        case ::pyramid_type::Down:
            {
                #define PYRAMID_TYPE pyramid_down
                switch(extractor_type)
                {
                    case fhog_feature_extractor_type::Default:
                        #define EXTRACTOR_TYPE default_fhog_feature_extractor
                        switch(pyramid_rate)
                        {
                            case 1:
                                {
                                    auto& d = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<1>, EXTRACTOR_TYPE>>*>(detector));\
                                    auto mat = dlib::draw_fhog(d, weight_index, cell_draw_size);
                                    *out_matrix = new dlib::matrix<uint8_t>(mat);
                                }
                                break;
                            case 2:
                                {
                                    auto& d = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<2>, EXTRACTOR_TYPE>>*>(detector));\
                                    auto mat = dlib::draw_fhog(d, weight_index, cell_draw_size);
                                    *out_matrix = new dlib::matrix<uint8_t>(mat);
                                }
                                break;
                            case 3:
                                {
                                    auto& d = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<3>, EXTRACTOR_TYPE>>*>(detector));\
                                    auto mat = dlib::draw_fhog(d, weight_index, cell_draw_size);
                                    *out_matrix = new dlib::matrix<uint8_t>(mat);
                                }
                                break;
                            case 4:
                                {
                                    auto& d = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<4>, EXTRACTOR_TYPE>>*>(detector));\
                                    auto mat = dlib::draw_fhog(d, weight_index, cell_draw_size);
                                    *out_matrix = new dlib::matrix<uint8_t>(mat);
                                }
                                break;
                            case 6:
                                {
                                    auto& d = *(static_cast<object_detector<scan_fhog_pyramid<PYRAMID_TYPE<6>, EXTRACTOR_TYPE>>*>(detector));\
                                    auto mat = dlib::draw_fhog(d, weight_index, cell_draw_size);
                                    *out_matrix = new dlib::matrix<uint8_t>(mat);
                                }
                                break;
                            default:
                                error = ERR_PYRAMID_NOT_SUPPORT_RATE;
                                break;
                        }
                        #undef EXTRACTOR_TYPE
                        break;
                    default:
                        error = ERR_FHOG_NOT_SUPPORT_EXTRACTOR;
                        break;
                }
                #undef PYRAMID_TYPE
            }
            break;
        default:
            error = ERR_PYRAMID_NOT_SUPPORT_TYPE;
            break;
    }

    return error;
}

#pragma endregion draw_fhog

#endif