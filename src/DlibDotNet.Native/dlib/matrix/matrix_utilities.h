#ifndef _CPP_MATRIX_UTILITIES_H_
#define _CPP_MATRIX_UTILITIES_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/matrix/matrix_utilities.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT void* linspace(double start, double end, int num)
{
    matrix_range_exp<double> ret = dlib::linspace(start, end, num);
    return new matrix_range_exp<double>(ret);
}

#endif