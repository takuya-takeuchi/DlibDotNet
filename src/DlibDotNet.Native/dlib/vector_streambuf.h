#include <iostream>
#include <vector>

class vector_streambuf : public std::basic_streambuf<char>
{
public:
    vector_streambuf(std::vector<char> &vec);
    ~vector_streambuf();
};