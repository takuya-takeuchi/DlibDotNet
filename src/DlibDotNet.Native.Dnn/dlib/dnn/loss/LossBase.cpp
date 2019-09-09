#include "LossBase.h"

DLLEXPORT int LossBase_get_id(LossBase* base)
{
    return base->get_id();
}

DLLEXPORT matrix_element_type LossBase_get_data_type(LossBase* base)
{
    return base->get_data_type();
}

DLLEXPORT matrix_element_type LossBase_get_label_type(LossBase* base)
{
    return base->get_label_type();
}