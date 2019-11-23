#ifndef _CPP_LOSS_MMOD_DEFINES_H_
#define _CPP_LOSS_MMOD_DEFINES_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
#pragma region type definitions

#pragma region 0
namespace net0
{
template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;
using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
}
#pragma endregion 0

#pragma region 1
namespace net1
{
template <long num_filters, typename SUBNET> using con5d_1 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_1  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_1  = relu<affine<con5d_1<32, relu<affine<con5d_1<32, relu<affine<con5d_1<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_1  = relu<affine<con5_1<55,SUBNET>>>;
using net_type_1 = loss_mmod<con<1,9,9,1,1,rcon5_1<rcon5_1<rcon5_1<downsampler_1<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
}
#pragma endregion 1

#pragma region 2
namespace net2
{
template <long num_filters, typename SUBNET> using con5d_2 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con3_2  = con<num_filters,3,3,1,1,SUBNET>;
template <typename SUBNET> using downsampler_2  = relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon3_2  = relu<bn_con<con3_2<32,SUBNET>>>;
using net_type_2  = loss_mmod<con<1,6,6,1,1,rcon3_2<rcon3_2<rcon3_2<downsampler_2<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
}
#pragma endregion 2

#pragma region 3
namespace net3
{
template <long num_filters, typename SUBNET> using con5d_3 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_3  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_3  = relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_3  = relu<bn_con<con5_3<55,SUBNET>>>;
using net_type_3 = loss_mmod<con<1,9,9,1,1,rcon5_3<rcon5_3<rcon5_3<downsampler_3<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
}
#pragma endregion 3

#pragma region 4-5
// https://github.com/davisking/dlib/blob/master/examples/dnn_instance_segmentation_ex.h
namespace instance_segmentaion
{
template <long num_filters, typename SUBNET> using con5d = dlib::con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = dlib::con<num_filters,5,5,1,1,SUBNET>;

template <typename SUBNET> using bdownsampler = dlib::relu<dlib::bn_con<con5d<128,dlib::relu<dlib::bn_con<con5d<128,dlib::relu<dlib::bn_con<con5d<32,SUBNET>>>>>>>>>;
template <typename SUBNET> using adownsampler = dlib::relu<dlib::affine<con5d<128,dlib::relu<dlib::affine<con5d<128,dlib::relu<dlib::affine<con5d<32,SUBNET>>>>>>>>>;

template <typename SUBNET> using brcon5 = dlib::relu<dlib::bn_con<con5<256,SUBNET>>>;
template <typename SUBNET> using arcon5 = dlib::relu<dlib::affine<con5<256,SUBNET>>>;

using det_bnet_type = dlib::loss_mmod<dlib::con<1,9,9,1,1,brcon5<brcon5<brcon5<bdownsampler<dlib::input_rgb_image_pyramid<dlib::pyramid_down<6>>>>>>>>;
using det_anet_type = dlib::loss_mmod<dlib::con<1,9,9,1,1,arcon5<arcon5<arcon5<adownsampler<dlib::input_rgb_image_pyramid<dlib::pyramid_down<6>>>>>>>>;
}
#pragma endregion 4-5

#pragma endregion type definitions

#endif