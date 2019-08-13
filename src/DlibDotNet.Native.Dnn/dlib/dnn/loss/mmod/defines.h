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
template <long num_filters, typename SUBNET> using con5d = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler  = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5  = relu<affine<con5<45,SUBNET>>>;
using net_type = loss_mmod<con<1,9,9,1,1,rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;

#pragma region 1
template <long num_filters, typename SUBNET> using con5d_1 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_1  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_1  = relu<affine<con5d_1<32, relu<affine<con5d_1<32, relu<affine<con5d_1<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_1  = relu<affine<con5_1<55,SUBNET>>>;
using net_type_1 = loss_mmod<con<1,9,9,1,1,rcon5_1<rcon5_1<rcon5_1<downsampler_1<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
#pragma endregion 1

#pragma region 2
template <long num_filters, typename SUBNET> using con5d_2 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con3_2  = con<num_filters,3,3,1,1,SUBNET>;
template <typename SUBNET> using downsampler_2  = relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32, relu<bn_con<con5d_2<32,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon3_2  = relu<bn_con<con3_2<32,SUBNET>>>;
using net_type_2  = loss_mmod<con<1,6,6,1,1,rcon3_2<rcon3_2<rcon3_2<downsampler_2<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
#pragma endregion 2

#pragma region 3
template <long num_filters, typename SUBNET> using con5d_3 = con<num_filters,5,5,2,2,SUBNET>;
template <long num_filters, typename SUBNET> using con5_3  = con<num_filters,5,5,1,1,SUBNET>;
template <typename SUBNET> using downsampler_3  = relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<32, relu<bn_con<con5d_3<16,SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5_3  = relu<bn_con<con5_3<55,SUBNET>>>;
using net_type_3 = loss_mmod<con<1,9,9,1,1,rcon5_3<rcon5_3<rcon5_3<downsampler_3<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;
#pragma endregion 3

#pragma endregion type definitions

typedef std::vector<mmod_rect> out_type;
typedef std::vector<mmod_rect> train_label_type;
typedef std::vector<mmod_rect*> train_label_type_pointer;

#endif