#include <stdio.h>
#include <iostream>

#include <dlib/dnn.h>
#include <dlib/data_io.h>
#include <dlib/image_processing.h>

#include <boost/thread.hpp>
#include "boost/filesystem/operations.hpp"
#include "boost/filesystem/path.hpp"
#include "boost/progress.hpp"

namespace fs = boost::filesystem;

using namespace dlib;

// ----------------------------------------------------------------------------------------

template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;

template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;

using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;

// ----------------------------------------------------------------------------------------

void face_location(size_t thread_id, net_type* net, matrix<rgb_pixel>* mat)
{
    std::cout << "start thread_id: " << thread_id << std::endl;
    try
    {
        auto& n = *net;
        auto& m = *mat;
        auto dets = n(m);
        for (auto&& d : dets)
            std::cout << d.rect << std::endl;
    }
    catch (...)
    {
        std::cout << "catch exception!!" << std::endl;
    }
    std::cout << "finish thread_id: " << thread_id << std::endl;
}

int main(int argc, char* argv[])
{
    net_type net;
    deserialize(argv[1]) >> net;
    std::cout << "deserialize: " << argv[1] << std::endl;
    const size_t thread_num = atoi(argv[2]);
    std::cout << "thread_num: " << thread_num << std::endl;

    matrix<rgb_pixel> img;
    load_image(img, argv[3]);
    std::cout << "load_image: " << argv[3] << std::endl;

    while(img.size() < 1800*1800)
        pyramid_up(img);

    // prepare thread
    std::vector<boost::thread*> threads(thread_num);
    for (size_t i = 0; i < thread_num; i++)
        threads[i] = new boost::thread(boost::bind(face_location, i, &net, &img));
    std::cout << "prepare thread" << std::endl;

    // wait thread
    std::cout << "wait all thread" << std::endl;
    for (size_t i = 0; i < thread_num; i++)
        threads[i]->join();
    std::cout << "finish all thread" << std::endl;

    return 0;
}