#ifndef _CPP_MATRIX_EXPRESSIONS_H_
#define _CPP_MATRIX_EXPRESSIONS_H_

#include <dlib/matrix.h>
#include <dlib/matrix/matrix_expressions.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

extern "C" _declspec(dllexport) void matrix_range_exp_delete(void* obj)
{
	delete obj;
}

extern "C" __declspec(dllexport) void* matrix_range_exp_create(matrix_element_type type, matrix_range_exp_create_param* param)
{ 
    void* result = nullptr;
    switch(type)
    {
        case matrix_element_type::UInt8:
            if (param->use_uint8_t_inc)
                result = new matrix_range_exp<uint8_t>(param->uint8_t_start, param->uint8_t_inc, param->uint8_t_end);
            else if (param->use_num)
                result = new matrix_range_exp<uint8_t>(param->uint8_t_start, param->uint8_t_end, param->num);
            else
                result = new matrix_range_exp<uint8_t>(param->uint8_t_start, param->uint8_t_end);
            break;
        case matrix_element_type::UInt16:
            if (param->use_uint16_t_inc)
                result = new matrix_range_exp<uint16_t>(param->uint16_t_start, param->uint16_t_inc, param->uint16_t_end);
            else if (param->use_num)
                result = new matrix_range_exp<uint16_t>(param->uint16_t_start, param->uint16_t_end, param->num);
            else
                result = new matrix_range_exp<uint16_t>(param->uint16_t_start, param->uint16_t_end);
            break;
        case matrix_element_type::Int8:
            if (param->use_int8_t_inc)
                result = new matrix_range_exp<int8_t>(param->int8_t_start, param->int8_t_inc, param->int8_t_end);
            else if (param->use_num)
                result = new matrix_range_exp<int8_t>(param->int8_t_start, param->int8_t_end, param->num);
            else
                result = new matrix_range_exp<int8_t>(param->int8_t_start, param->int8_t_end);
            break;
        case matrix_element_type::Int16:
            if (param->use_int16_t_inc)
                result = new matrix_range_exp<int16_t>(param->int16_t_start, param->int16_t_inc, param->int16_t_end);
            else if (param->use_num)
                result = new matrix_range_exp<int16_t>(param->int16_t_start, param->int16_t_end, param->num);
            else
                result = new matrix_range_exp<int16_t>(param->int16_t_start, param->int16_t_end);
            break;
        case matrix_element_type::Int32:
            if (param->use_int32_t_inc)
                result = new matrix_range_exp<int32_t>(param->int32_t_start, param->int32_t_inc, param->int32_t_end);
            else if (param->use_num)
                result = new matrix_range_exp<int32_t>(param->int32_t_start, param->int32_t_end, param->num);
            else
                result = new matrix_range_exp<int32_t>(param->int32_t_start, param->int32_t_end);
            break;
        case matrix_element_type::Float:
            if (param->use_float_inc)
                result = new matrix_range_exp<float>(param->float_start, param->float_inc, param->float_end);
            else if (param->use_num)
                result = new matrix_range_exp<float>(param->float_start, param->float_end, param->num);
            else
                result = new matrix_range_exp<float>(param->float_start, param->float_end);
            break;
        case matrix_element_type::Double:
            if (param->use_double_inc)
                result = new matrix_range_exp<double>(param->double_start, param->double_inc, param->double_end);
            else if (param->use_num)
                result = new matrix_range_exp<double>(param->double_start, param->double_end, param->num);
            else
                result = new matrix_range_exp<double>(param->double_start, param->double_end);
            break;
        case matrix_element_type::UInt32:
        case matrix_element_type::RgbPixel:
        case matrix_element_type::HsiPixel:
        case matrix_element_type::RgbAlphaPixel:
        default:
            result = nullptr;
            break;
    }

    return result;
}

extern "C" __declspec(dllexport) bool matrix_range_exp_nc(matrix_element_type type, void* matrix, int* result)
{
    bool err = false;   
    switch(type)
    {
        case matrix_element_type::UInt8:
			*result = ((matrix_range_exp<uint8_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::UInt16:
			*result = ((matrix_range_exp<uint16_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::UInt32:
			*result = ((matrix_range_exp<uint32_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::Int8:
			*result = ((matrix_range_exp<int8_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::Int16:
			*result = ((matrix_range_exp<int16_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::Int32:
			*result = ((matrix_range_exp<int32_t>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::Float:
			*result = ((matrix_range_exp<float>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::Double:
			*result = ((matrix_range_exp<double>*)matrix)->nc();
			err = true;
            break;
        case matrix_element_type::RgbPixel:
            *result = ((matrix_range_exp<rgb_pixel>*)matrix)->nc();
            err = true;
            break;
        case matrix_element_type::HsiPixel:
            *result = ((matrix_range_exp<hsi_pixel>*)matrix)->nc();
            err = true;
            break;
        case matrix_element_type::RgbAlphaPixel:
            *result = ((matrix_range_exp<rgb_alpha_pixel>*)matrix)->nc();
            err = true;
            break;
        default:
            err = false;
            break;
    }

    return err;
}

extern "C" __declspec(dllexport) bool matrix_range_exp_nr(matrix_element_type type, void* matrix, int* result)
{
    bool err = false;    
    switch(type)
    {
        case matrix_element_type::UInt8:
			*result = ((matrix_range_exp<uint8_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::UInt16:
			*result = ((matrix_range_exp<uint16_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::UInt32:
			*result = ((matrix_range_exp<uint32_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::Int8:
			*result = ((matrix_range_exp<int8_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::Int16:
			*result = ((matrix_range_exp<int16_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::Int32:
			*result = ((matrix_range_exp<int32_t>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::Float:
			*result = ((matrix_range_exp<float>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::Double:
			*result = ((matrix_range_exp<double>*)matrix)->nr();
			err = true;
            break;
        case matrix_element_type::RgbPixel:
            *result = ((matrix_range_exp<rgb_pixel>*)matrix)->nr();
            err = true;
            break;
        case matrix_element_type::HsiPixel:
            *result = ((matrix_range_exp<hsi_pixel>*)matrix)->nr();
            err = true;
            break;
        case matrix_element_type::RgbAlphaPixel:
            *result = ((matrix_range_exp<rgb_alpha_pixel>*)matrix)->nr();
            err = true;
            break;
        default:
            err = false;
            break;
    }

    return err;
}

#endif