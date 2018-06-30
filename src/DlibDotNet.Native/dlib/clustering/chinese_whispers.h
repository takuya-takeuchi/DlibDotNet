#ifndef _CPP_CLUSTERING_CHINESE_WHISPERS_H_
#define _CPP_CLUSTERING_CHINESE_WHISPERS_H_

#include "../export.h"
#include <dlib/clustering/chinese_whispers.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT unsigned long clustering_chinese_whispers(std::vector<sample_pair*>* edges,
                                                    std::vector<unsigned long>* labels,
                                                    const unsigned long num_iterations = 100)
{
    std::vector<sample_pair*>& e = *(static_cast<std::vector<sample_pair*>*>(edges));
    std::vector<sample_pair> edges_tmp;
    for (int index = 0; index < e.size(); index++)
    {
        sample_pair& edge = *(static_cast<sample_pair*>(e[index]));
        edges_tmp.push_back(edge);
    }

    std::vector<unsigned long>& l = *(static_cast<std::vector<unsigned long>*>(labels));
    return dlib::chinese_whispers(edges_tmp, l, num_iterations);
}

#endif