#ifndef _CPP_STATISTICS_H_
#define _CPP_STATISTICS_H_

#include "../export.h"
#include <dlib/statistics/statistics.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region running_stats

DLLEXPORT void* running_stats_new(running_stats_type type)
{
    // long double equals to System.Double
    // Please refer to https://msdn.microsoft.com/en-us/library/0wf2yk2k.aspx
    switch(type)
    {
        case running_stats_type::Float:
            return new running_stats<float>();
        case running_stats_type::Double:
            return new running_stats<double>();
        default:
            return nullptr;
    }
}

DLLEXPORT int running_stats_add(running_stats_type type, void* stats, void* val)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                s->add(*((float*)val));
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                s->add(*((double*)val));
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_clear(running_stats_type type, void* stats)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                s->clear();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                s->clear();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_current_n(running_stats_type type, void* stats, void* n)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)n) = s->current_n();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)n) = s->current_n();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_max(running_stats_type type, void* stats, void* max)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)max) = s->max();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)max) = s->max();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_mean(running_stats_type type, void* stats, void* mean)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)mean) = s->mean();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)mean) = s->mean();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_min(running_stats_type type, void* stats, void* min)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)min) = s->min();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)min) = s->min();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_variance(running_stats_type type, void* stats, void* variance)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)variance) = s->variance();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)variance) = s->variance();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_stddev(running_stats_type type, void* stats, void* stddev)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)stddev) = s->stddev();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)stddev) = s->stddev();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_skewness(running_stats_type type, void* stats, void* skewness)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)skewness) = s->skewness();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)skewness) = s->skewness();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_ex_kurtosis(running_stats_type type, void* stats, void* ex_kurtosis)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)ex_kurtosis) = s->ex_kurtosis();
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)ex_kurtosis) = s->ex_kurtosis();
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT int running_stats_scale(running_stats_type type, void* stats, void* scale, void* ret)
{
    int err = ERR_OK;

    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                *((float*)ret) = s->scale(*((float*)scale));
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                *((double*)ret) = s->scale(*((double*)scale));
            }
            break;
        default:
            err = ERR_RUNNING_STATS_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

DLLEXPORT void running_stats_delete(running_stats_type type, void* stats)
{
    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>* s = static_cast<running_stats<float>*>(stats);
                delete s;
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>* s = static_cast<running_stats<double>*>(stats);
                delete s;
            }
            break;
        default:
            break;
    }
}

#pragma region operator

DLLEXPORT void* running_stats_operator_add(running_stats_type type, void* left, void* right)
{
    switch(type)
    {
        case running_stats_type::Float:
            {
                running_stats<float>& l = *(static_cast<running_stats<float>*>(left));
                running_stats<float>& r = *(static_cast<running_stats<float>*>(right));
                const running_stats<float>& ret = l + r;
                return new running_stats<float>(ret);
            }
            break;
        case running_stats_type::Double:
            {
                running_stats<double>& l = *(static_cast<running_stats<double>*>(left));
                running_stats<double>& r = *(static_cast<running_stats<double>*>(right));
                const running_stats<double>& ret = l + r;
                return new running_stats<double>(ret);
            }
            break;
        default:
            return nullptr;
    }
}

#pragma endregion operator

#pragma endregion running_stats

#endif