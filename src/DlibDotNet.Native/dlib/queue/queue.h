#ifndef _CPP_QUEUE_H_
#define _CPP_QUEUE_H_

#include "../export.h"
#include <dlib/queue.h>
#include <dlib/queue/queue_kernel_1.h>
#include "../shared.h"

#include "template.h"

/*
// kernel_1a        
typedef     queue_kernel_1<T,mem_manager>     kernel_1a;
typedef     queue_kernel_c<kernel_1a>         kernel_1a_c;

// kernel_2a        
typedef     queue_kernel_2<T,20,mem_manager>  kernel_2a;
typedef     queue_kernel_c<kernel_2a>         kernel_2a_c;

// kernel_2b        
typedef     queue_kernel_2<T,100,mem_manager> kernel_2b;
typedef     queue_kernel_c<kernel_2b>         kernel_2b_c;
*/

MAKE_FUNC(kernel_1a, uint32_t, uint32_t)

// DLLEXPORT uint32_t queue_kernel_1a_uint32_t_element(dlib::queue<uint32_t>::kernel_1a* q)
// {
//     const auto size = q->size();
//     auto ret = q->element();
//     return ret;
// }

#endif