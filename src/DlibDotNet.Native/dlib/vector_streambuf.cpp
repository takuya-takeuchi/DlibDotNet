#include "vector_streambuf.h"

vector_streambuf::vector_streambuf(std::vector<char> &vec)
{
    setg(vec.data(), vec.data(), vec.data() + vec.size());
}

vector_streambuf::~vector_streambuf()
{        
}