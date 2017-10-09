#ifndef _CPP_MATRIX_OP_H_
#define _CPP_MATRIX_OP_H_

#include "../export.h"
#include <dlib/hash.h>
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/image_processing/generic_image.h>
#include <dlib/image_transforms/colormaps.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region matrix_op<template>

#define ELEMENT element

#define matrix_op_operator_template(ret, type, obj, r, c, result) \
do { \
    ret = ERR_OK;\
    switch(type)\
    {\
        case array2d_type::UInt8:\
			*result = (*((matrix_op<ELEMENT<array2d<uint8_t>>>*)obj))(r, c);\
			break;\
        case array2d_type::UInt16:\
			*result = (*((matrix_op<ELEMENT<array2d<uint16_t>>>*)obj))(r, c);\
			break;\
        case array2d_type::Float:\
			*result = (*((matrix_op<ELEMENT<array2d<float>>>*)obj))(r, c);\
			break;\
        case array2d_type::Double:\
			*result = (*((matrix_op<ELEMENT<array2d<double>>>*)obj))(r, c);\
			break;\
        case array2d_type::RgbPixel:\
			*result = (*((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)obj))(r, c);\
			break;\
        case array2d_type::HsiPixel:\
			*result = (*((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)obj))(r, c);\
			break;\
        case array2d_type::RgbAlphaPixel:\
			*result = (*((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)obj))(r, c);\
			break;\
        default:\
            ret = ERR_ARRAY_TYPE_NOT_SUPPORT;\
			break;\
    }\
} while (0)

#define matrix_op_nc_template(err, type, obj, result) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			*result = ((matrix_op<ELEMENT<array2d<uint8_t>>>*)obj)->nc();\
			err = true;\
            break;\
        case array2d_type::UInt16:\
			*result = ((matrix_op<ELEMENT<array2d<uint16_t>>>*)obj)->nc();\
			err = true;\
            break;\
        case array2d_type::Float:\
			*result = ((matrix_op<ELEMENT<array2d<float>>>*)obj)->nc();\
			err = true;\
            break;\
        case array2d_type::Double:\
			*result = ((matrix_op<ELEMENT<array2d<double>>>*)obj)->nc();\
			err = true;\
            break;\
        case array2d_type::RgbPixel:\
            *result = ((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)obj)->nc();\
            err = true;\
            break;\
        case array2d_type::HsiPixel:\
            *result = ((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)obj)->nc();\
            err = true;\
            break;\
        case array2d_type::RgbAlphaPixel:\
            *result = ((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)obj)->nc();\
            err = true;\
            break;\
        default:\
            err = false;\
            break;\
    }\
} while (0)

#define matrix_op_nr_template(err, type, obj, result) \
do { \
    switch(type)\
    {\
        case array2d_type::UInt8:\
			*result = ((matrix_op<ELEMENT<array2d<uint8_t>>>*)obj)->nr();\
			err = true;\
            break;\
        case array2d_type::UInt16:\
			*result = ((matrix_op<ELEMENT<array2d<uint16_t>>>*)obj)->nr();\
			err = true;\
            break;\
        case array2d_type::Float:\
			*result = ((matrix_op<ELEMENT<array2d<float>>>*)obj)->nr();\
			err = true;\
            break;\
        case array2d_type::Double:\
			*result = ((matrix_op<ELEMENT<array2d<double>>>*)obj)->nr();\
			err = true;\
            break;\
        case array2d_type::RgbPixel:\
            *result = ((matrix_op<ELEMENT<array2d<rgb_pixel>>>*)obj)->nr();\
            err = true;\
            break;\
        case array2d_type::HsiPixel:\
            *result = ((matrix_op<ELEMENT<array2d<hsi_pixel>>>*)obj)->nr();\
            err = true;\
            break;\
        case array2d_type::RgbAlphaPixel:\
            *result = ((matrix_op<ELEMENT<array2d<rgb_alpha_pixel>>>*)obj)->nr();\
            err = true;\
            break;\
        default:\
            err = false;\
            break;\
    }\
} while (0)

#pragma endregion matrix_op<template>

DLLEXPORT void matrix_op_delete(void* obj)
{
	delete obj;
}

DLLEXPORT int matrix_op_operator(element_type etype, array2d_type type, void* obj, int r, int c, rgb_pixel* ret)
{
    int err = ERR_OK;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            matrix_op_operator_template(err, type, obj, r, c, ret);
            #undef ELEMENT
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            matrix_op_operator_template(err, type, obj, r, c, ret);
            #undef ELEMENT
            break;
        default:
            return ERR_ELEMENT_TYPE_NOT_SUPPORT;
    }
    
    return err;
}

DLLEXPORT int matrix_op_nc(element_type etype, array2d_type type, void* obj, int* ret)
{
    bool bRet = false;
    int err = ERR_OK;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            matrix_op_nc_template(bRet, type, obj, ret);
            #undef ELEMENT
            if (!bRet)
                err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            matrix_op_nc_template(bRet, type, obj, ret);
            #undef ELEMENT
            if (!bRet)
                err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
        default:
            err =  ERR_ELEMENT_TYPE_NOT_SUPPORT;
    }

    return err;
}

DLLEXPORT int matrix_op_nr(element_type etype, array2d_type type, void* obj, int* ret)
{
    bool bRet = false;
    int err = ERR_OK;
    switch(etype)
    {
        case element_type::OpHeatmap:
            #define ELEMENT dlib::op_heatmap
            matrix_op_nr_template(bRet, type, obj, ret);
            #undef ELEMENT
            if (!bRet)
                err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
        case element_type::OpJet:
            #define ELEMENT dlib::op_jet
            matrix_op_nr_template(bRet, type, obj, ret);
            #undef ELEMENT
            if (!bRet)
                err = ERR_ELEMENT_TYPE_NOT_SUPPORT;
            break;
        default:
            err =  ERR_ELEMENT_TYPE_NOT_SUPPORT;
    }

    return err;
}

#endif