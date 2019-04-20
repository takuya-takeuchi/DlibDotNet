#ifndef _CPP_LOSS_TEMPLATE_H_
#define _CPP_LOSS_TEMPLATE_H_

#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

#include "../trainer.h"
#include "../layers/layers.h"
#include "../../common.h"

using namespace dlib;
using namespace std;

#pragma region template

#define net_to_xml_template(__NET_TYPE__, __ELEMENT_TYPENAME__, __ELEMENT_TYPE__, error, ...) \
std::string str(filename);\
auto& net = *static_cast<__NET_TYPE__*>(obj);\
dlib::net_to_xml(net, str);

#pragma endregion template

#endif