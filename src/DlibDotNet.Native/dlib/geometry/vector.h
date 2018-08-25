#ifndef _CPP_GEOMETRY_VECTOR_H_
#define _CPP_GEOMETRY_VECTOR_H_

#include "../export.h"
#include <dlib/geometry/vector.h>
#include "../shared.h"

using namespace dlib;

#pragma region vector

DLLEXPORT void* vector_new(vector_element_type type)
{
    switch(type)
    {
        case vector_element_type::UInt8:
            return new dlib::vector<uint8_t>();
        case vector_element_type::UInt16:
            return new dlib::vector<uint16_t>();
        case vector_element_type::UInt32:
            return new dlib::vector<uint32_t>();
        case vector_element_type::Int8:
            return new dlib::vector<int8_t>();
        case vector_element_type::Int16:
            return new dlib::vector<int16_t>();
        case vector_element_type::Int32:
            return new dlib::vector<int32_t>();
        case vector_element_type::Float:
            return new dlib::vector<float>();
        case vector_element_type::Double:
            return new dlib::vector<double>();
        default:
            return nullptr;
    }
}

DLLEXPORT dlib::vector<uint8_t>* vector_new1_uint8_t(uint8_t x, uint8_t y, uint8_t z)
{
    return new dlib::vector<uint8_t>(x, y, z);
}

DLLEXPORT dlib::vector<uint16_t>* vector_new1_uint16_t(uint16_t x, uint16_t y, uint16_t z)
{
    return new dlib::vector<uint16_t>(x, y, z);
}

DLLEXPORT dlib::vector<uint32_t>* vector_new1_uint32_t(uint32_t x, uint32_t y, uint32_t z)
{
    return new dlib::vector<uint32_t>(x, y, z);
}

DLLEXPORT dlib::vector<int8_t>* vector_new1_int8_t(int8_t x, int8_t y, int8_t z)
{
    return new dlib::vector<int8_t>(x, y, z);
}

DLLEXPORT dlib::vector<int16_t>* vector_new1_int16_t(int16_t x, int16_t y, int16_t z)
{
    return new dlib::vector<int16_t>(x, y, z);
}

DLLEXPORT dlib::vector<int32_t>* vector_new1_int32_t(int32_t x, int32_t y, int32_t z)
{
    return new dlib::vector<int32_t>(x, y, z);
}

#pragma endregion vector

#pragma region vector_get_xyz

DLLEXPORT void vector_get_xyz_uint8_t(dlib::vector<uint8_t>* vector, uint8_t* x, uint8_t* y, uint8_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_uint16_t(dlib::vector<uint16_t>* vector, uint16_t* x, uint16_t* y, uint16_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_uint32_t(dlib::vector<uint32_t>* vector, uint32_t* x, uint32_t* y, uint32_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_int8_t(dlib::vector<int8_t>* vector, int8_t* x, int8_t* y, int8_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_int16_t(dlib::vector<int16_t>* vector, int16_t* x, int16_t* y, int16_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_int32_t(dlib::vector<int32_t>* vector, int32_t* x, int32_t* y, int32_t* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_float(dlib::vector<float>* vector, float* x, float* y, float* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

DLLEXPORT void vector_get_xyz_double(dlib::vector<double>* vector, double* x, double* y, double* z)
{
    *x = vector->x();
    *y = vector->y();
    *z = vector->z();
}

#pragma endregion vector_get_xyz

#pragma region vector_set_xyz

DLLEXPORT void vector_set_xyz_uint8_t(dlib::vector<uint8_t>* vector, uint8_t x, uint8_t y, uint8_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_uint16_t(dlib::vector<uint16_t>* vector, uint16_t x, uint16_t y, uint16_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_uint32_t(dlib::vector<uint32_t>* vector, uint32_t x, uint32_t y, uint32_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_int8_t(dlib::vector<int8_t>* vector, int8_t x, int8_t y, int8_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_int16_t(dlib::vector<int16_t>* vector, int16_t x, int16_t y, int16_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_int32_t(dlib::vector<int32_t>* vector, int32_t x, int32_t y, int32_t z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_float(dlib::vector<float>* vector, float x, float y, float z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

DLLEXPORT void vector_set_xyz_double(dlib::vector<double>* vector, double x, double y, double z)
{
    vector->x() = x;
    vector->y() = y;
    vector->z() = z;
}

#pragma endregion vector_set_xyz

#pragma region vector_operator_add

DLLEXPORT void vector_operator_add_uint8_t(dlib::vector<uint8_t>* left, dlib::vector<uint8_t>* right, dlib::vector<uint8_t>** ret)
{
    dlib::vector<uint8_t>& r = *(static_cast<dlib::vector<uint8_t>*>(left));
    dlib::vector<uint8_t>& l = *(static_cast<dlib::vector<uint8_t>*>(right));
    *ret = new dlib::vector<uint8_t>(l + r);
}

DLLEXPORT void vector_operator_add_uint16_t(dlib::vector<uint16_t>* left, dlib::vector<uint16_t>* right, dlib::vector<uint16_t>** ret)
{
    dlib::vector<uint16_t>& l = *(static_cast<dlib::vector<uint16_t>*>(left));
    dlib::vector<uint16_t>& r = *(static_cast<dlib::vector<uint16_t>*>(right));
    *ret = new dlib::vector<uint16_t>(l + r);
}

DLLEXPORT void vector_operator_add_uint32_t(dlib::vector<uint32_t>* left, dlib::vector<uint32_t>* right, dlib::vector<uint32_t>** ret)
{
    dlib::vector<uint32_t>& l = *(static_cast<dlib::vector<uint32_t>*>(left));
    dlib::vector<uint32_t>& r = *(static_cast<dlib::vector<uint32_t>*>(right));
    *ret = new dlib::vector<uint32_t>(l + r);
}

DLLEXPORT void vector_operator_add_int8_t(dlib::vector<int8_t>* left, dlib::vector<int8_t>* right, dlib::vector<int8_t>** ret)
{
    dlib::vector<int8_t>& l = *(static_cast<dlib::vector<int8_t>*>(left));
    dlib::vector<int8_t>& r = *(static_cast<dlib::vector<int8_t>*>(right));
    *ret = new dlib::vector<int8_t>(l + r);
}

DLLEXPORT void vector_operator_add_int16_t(dlib::vector<int16_t>* left, dlib::vector<int16_t>* right, dlib::vector<int16_t>** ret)
{
    dlib::vector<int16_t>& l = *(static_cast<dlib::vector<int16_t>*>(left));
    dlib::vector<int16_t>& r = *(static_cast<dlib::vector<int16_t>*>(right));
    *ret = new dlib::vector<int16_t>(l + r);
}

DLLEXPORT void vector_operator_add_int32_t(dlib::vector<int32_t>* left, dlib::vector<int32_t>* right, dlib::vector<int32_t>** ret)
{
    dlib::vector<int32_t>& l = *(static_cast<dlib::vector<int32_t>*>(left));
    dlib::vector<int32_t>& r = *(static_cast<dlib::vector<int32_t>*>(right));
    *ret = new dlib::vector<int32_t>(l + r);
}

DLLEXPORT void vector_operator_add_float(dlib::vector<float>* left, dlib::vector<float>* right, dlib::vector<float>** ret)
{
    dlib::vector<float>& l = *(static_cast<dlib::vector<float>*>(left));
    dlib::vector<float>& r = *(static_cast<dlib::vector<float>*>(right));
    *ret = new dlib::vector<float>(l + r);
}

DLLEXPORT void vector_operator_add_double(dlib::vector<double>* left, dlib::vector<double>* right, dlib::vector<double>** ret)
{
    dlib::vector<double>& l = *(static_cast<dlib::vector<double>*>(left));
    dlib::vector<double>& r = *(static_cast<dlib::vector<double>*>(right));
    *ret = new dlib::vector<double>(l + r);
}

#pragma endregion vector_operator_add

#pragma region vector_operator_div

DLLEXPORT void vector_operator_div_uint8_t(dlib::vector<uint8_t>* vector, uint8_t value, dlib::vector<uint8_t>** ret)
{
    dlib::vector<uint8_t>& tmp = *(static_cast<dlib::vector<uint8_t>*>(vector));
    *ret = new dlib::vector<uint8_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_uint16_t(dlib::vector<uint16_t>* vector, uint16_t value, dlib::vector<uint16_t>** ret)
{
    dlib::vector<uint16_t>& tmp = *(static_cast<dlib::vector<uint16_t>*>(vector));
    *ret = new dlib::vector<uint16_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_uint32_t(dlib::vector<uint32_t>* vector, uint32_t value, dlib::vector<uint32_t>** ret)
{
    dlib::vector<uint32_t>& tmp = *(static_cast<dlib::vector<uint32_t>*>(vector));
    *ret = new dlib::vector<uint32_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_int8_t(dlib::vector<int8_t>* vector, int8_t value, dlib::vector<int8_t>** ret)
{
    dlib::vector<int8_t>& tmp = *(static_cast<dlib::vector<int8_t>*>(vector));
    *ret = new dlib::vector<int8_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_int16_t(dlib::vector<int16_t>* vector, int16_t value, dlib::vector<int16_t>** ret)
{
    dlib::vector<int16_t>& tmp = *(static_cast<dlib::vector<int16_t>*>(vector));
    *ret = new dlib::vector<int16_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_int32_t(dlib::vector<int32_t>* vector, int32_t value, dlib::vector<int32_t>** ret)
{
    dlib::vector<int32_t>& tmp = *(static_cast<dlib::vector<int32_t>*>(vector));
    *ret = new dlib::vector<int32_t>(tmp / value);
}

DLLEXPORT void vector_operator_div_float(dlib::vector<float>* vector, float value, dlib::vector<float>** ret)
{
    dlib::vector<float>& tmp = *(static_cast<dlib::vector<float>*>(vector));
    *ret = new dlib::vector<float>(tmp / value);
}

DLLEXPORT void vector_operator_div_double(dlib::vector<double>* vector, double value, dlib::vector<double>** ret)
{
    dlib::vector<double>& tmp = *(static_cast<dlib::vector<double>*>(vector));
    *ret = new dlib::vector<double>(tmp / value);
}

#pragma endregion vector_operator_div

DLLEXPORT int vector_operator_left_shift(vector_element_type type, void* matrix, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case vector_element_type::UInt8:
            {
                dlib::vector<uint8_t>& mat = *(static_cast<dlib::vector<uint8_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt16:
            {
                dlib::vector<uint16_t>& mat = *(static_cast<dlib::vector<uint16_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt32:
            {
                dlib::vector<uint32_t>& mat = *(static_cast<dlib::vector<uint32_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int8:
            {
                dlib::vector<int8_t>& mat = *(static_cast<dlib::vector<int8_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int16:
            {
                dlib::vector<int16_t>& mat = *(static_cast<dlib::vector<int16_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int32:
            {
                dlib::vector<int32_t>& mat = *(static_cast<dlib::vector<int32_t>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Float:
            {
                dlib::vector<float>& mat = *(static_cast<dlib::vector<float>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Double:
            {
                dlib::vector<double>& mat = *(static_cast<dlib::vector<double>*>(matrix));
                *stream << mat;
            }
            break;
        default:
            err = ERR_VECTOR_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int vector_2_operator_left_shift(vector_element_type type, void* matrix, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case vector_element_type::UInt8:
            {
                dlib::vector<uint8_t,2>& mat = *(static_cast<dlib::vector<uint8_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt16:
            {
                dlib::vector<uint16_t,2>& mat = *(static_cast<dlib::vector<uint16_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt32:
            {
                dlib::vector<uint32_t,2>& mat = *(static_cast<dlib::vector<uint32_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int8:
            {
                dlib::vector<int8_t,2>& mat = *(static_cast<dlib::vector<int8_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int16:
            {
                dlib::vector<int16_t,2>& mat = *(static_cast<dlib::vector<int16_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int32:
            {
                dlib::vector<int32_t,2>& mat = *(static_cast<dlib::vector<int32_t,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Float:
            {
                dlib::vector<float,2>& mat = *(static_cast<dlib::vector<float,2>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Double:
            {
                dlib::vector<double,2>& mat = *(static_cast<dlib::vector<double,2>*>(matrix));
                *stream << mat;
            }
            break;
        default:
            err = ERR_VECTOR_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT int vector_3_operator_left_shift(vector_element_type type, void* matrix, std::ostringstream* stream)
{
    int err = ERR_OK;
    switch(type)
    {
        case vector_element_type::UInt8:
            {
                dlib::vector<uint8_t,3>& mat = *(static_cast<dlib::vector<uint8_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt16:
            {
                dlib::vector<uint16_t,3>& mat = *(static_cast<dlib::vector<uint16_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::UInt32:
            {
                dlib::vector<uint32_t,3>& mat = *(static_cast<dlib::vector<uint32_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int8:
            {
                dlib::vector<int8_t,3>& mat = *(static_cast<dlib::vector<int8_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int16:
            {
                dlib::vector<int16_t,3>& mat = *(static_cast<dlib::vector<int16_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Int32:
            {
                dlib::vector<int32_t,3>& mat = *(static_cast<dlib::vector<int32_t,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Float:
            {
                dlib::vector<float,3>& mat = *(static_cast<dlib::vector<float,3>*>(matrix));
                *stream << mat;
            }
            break;
        case vector_element_type::Double:
            {
                dlib::vector<double,3>& mat = *(static_cast<dlib::vector<double,3>*>(matrix));
                *stream << mat;
            }
            break;
        default:
            err = ERR_VECTOR_TYPE_NOT_SUPPORT;
            break;
    }
    
    return err;
}

DLLEXPORT void vector_delete(vector_element_type type, void* vector)
{
    switch(type)
    {
        case vector_element_type::UInt8:
            {
                dlib::vector<uint8_t>* tmp = (dlib::vector<uint8_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::UInt16:
            {
                dlib::vector<uint16_t>* tmp = (dlib::vector<uint16_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::UInt32:
            {
                dlib::vector<uint32_t>* tmp = (dlib::vector<uint32_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::Int8:
            {
                dlib::vector<int8_t>* tmp = (dlib::vector<int8_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::Int16:
            {
                dlib::vector<int16_t>* tmp = (dlib::vector<int16_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::Int32:
            {
                dlib::vector<int32_t>* tmp = (dlib::vector<int32_t>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::Float:
            {
                dlib::vector<float>* tmp = (dlib::vector<float>*)vector;
                delete tmp;
            }
            break;
        case vector_element_type::Double:
            {
                dlib::vector<double>* tmp = (dlib::vector<double>*)vector;
                delete tmp;
            }
            break;
        default:
            break;
    }
}

#endif