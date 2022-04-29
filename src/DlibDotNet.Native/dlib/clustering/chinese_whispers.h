#ifndef _CPP_CLUSTERING_CHINESE_WHISPERS_H_
#define _CPP_CLUSTERING_CHINESE_WHISPERS_H_

#include "../export.h"
#include <dlib/clustering/chinese_whispers.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

DLLEXPORT uint64_t clustering_chinese_whispers(std::vector<sample_pair*>* edges,
                                               std::vector<uint32_t>* labels,
                                               const uint32_t num_iterations = 100)
{
    std::vector<sample_pair*>& e = *(static_cast<std::vector<sample_pair*>*>(edges));
    std::vector<sample_pair> edges_tmp;
    for (auto index = 0; index < e.size(); index++)
    {
        sample_pair& edge = *(static_cast<sample_pair*>(e[index]));
        edges_tmp.push_back(edge);
    }

    // unsigned long is 4byte in windows but 8 byte in SILP64, ILP64, LP64 (UNIX/Linux, macOS/iOS)
    std::vector<unsigned long> tmp;
    const auto ret = dlib::chinese_whispers(edges_tmp, tmp, num_iterations);

    for (auto index = 0; index < tmp.size(); index++)
        labels->push_back(tmp[index]);

    return ret;
}

#endif