#ifndef _CPP_QUEUE_TEMPLATE_H_
#define _CPP_QUEUE_TEMPLATE_H_

#include "../export.h"
#include "../shared.h"

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

// sort_1 extend kernel_1a
typedef     queue_sort_1<kernel_1a>           sort_1a;
typedef     queue_sort_1<kernel_1a_c>         sort_1a_c;

// sort_1 extend kernel_2a
typedef     queue_sort_1<kernel_2a>           sort_1b;
typedef     queue_sort_1<kernel_2a_c>         sort_1b_c;

// sort_1 extend kernel_2b
typedef     queue_sort_1<kernel_2b>           sort_1c;
typedef     queue_sort_1<kernel_2b_c>         sort_1c_c;
*/

#define MAKE_FUNC_KERNEL(__QUEUETYPE__, __TYPE__, __TYPENAME__)\
DLLEXPORT dlib::queue<__TYPE__>::__QUEUETYPE__* queue_##__QUEUETYPE__##_##__TYPENAME__##_new()\
{\
    return new dlib::queue<__TYPE__>::__QUEUETYPE__();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_delete(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    delete q;\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_clear(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    q->clear();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_reset(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    q->reset();\
}\
DLLEXPORT bool queue_##__QUEUETYPE__##_##__TYPENAME__##_move_next(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->move_next();\
}\
DLLEXPORT size_t queue_##__QUEUETYPE__##_##__TYPENAME__##_size(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->size();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_enqueue(dlib::queue<__TYPE__>::__QUEUETYPE__* q, __TYPE__ e)\
{\
    q->enqueue(e);\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_dequeue(dlib::queue<__TYPE__>::__QUEUETYPE__* q, __TYPE__* e)\
{\
    __TYPENAME__ tmp;\
    q->dequeue(tmp);\
    *e = tmp;\
}\
DLLEXPORT __TYPE__ queue_##__QUEUETYPE__##_##__TYPENAME__##_element(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->element();\
}\

#define MAKE_FUNC_SORT(__QUEUETYPE__, __TYPE__, __TYPENAME__)\
DLLEXPORT dlib::queue<__TYPE__>::__QUEUETYPE__* queue_##__QUEUETYPE__##_##__TYPENAME__##_new()\
{\
    return new dlib::queue<__TYPE__>::__QUEUETYPE__();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_delete(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    delete q;\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_clear(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    q->clear();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_reset(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    q->reset();\
}\
DLLEXPORT bool queue_##__QUEUETYPE__##_##__TYPENAME__##_move_next(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->move_next();\
}\
DLLEXPORT size_t queue_##__QUEUETYPE__##_##__TYPENAME__##_size(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->size();\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_enqueue(dlib::queue<__TYPE__>::__QUEUETYPE__* q, __TYPE__ e)\
{\
    q->enqueue(e);\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_dequeue(dlib::queue<__TYPE__>::__QUEUETYPE__* q, __TYPE__* e)\
{\
    __TYPENAME__ tmp;\
    q->dequeue(tmp);\
    *e = tmp;\
}\
DLLEXPORT void queue_##__QUEUETYPE__##_##__TYPENAME__##_sort(dlib::queue<__TYPE__>::__QUEUETYPE__* q, __TYPE__* e)\
{\
    q->sort();\
}\
DLLEXPORT __TYPE__ queue_##__QUEUETYPE__##_##__TYPENAME__##_element(dlib::queue<__TYPE__>::__QUEUETYPE__* q)\
{\
    return q->element();\
}\

#endif