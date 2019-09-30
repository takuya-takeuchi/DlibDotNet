#ifndef _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_DEFINES_H_
#define _CPP_LOSS_MULTICLASS_LOG_PER_PIXEL_DEFINES_H_

#include <iostream>
#include <dlib/dnn.h>
#include <dlib/matrix.h>
#include <vector>

using namespace dlib;
using namespace std;

// Developer can customize these as you want to do!!!
constexpr int class_count = 21; // background + 20 classes

#pragma region type definitions
template <int N, template <typename> class BN, int stride, typename SUBNET>
using block = BN<dlib::con<N,3,3,1,1, dlib::relu<BN<dlib::con<N,3,3,stride,stride,SUBNET>>>>>;

template <int N, template <typename> class BN, int stride, typename SUBNET>
using blockt = BN<dlib::cont<N,3,3,1,1,dlib::relu<BN<dlib::cont<N,3,3,stride,stride,SUBNET>>>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual = dlib::add_prev1<block<N,BN,1,dlib::tag1<SUBNET>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_down = dlib::add_prev2<dlib::avg_pool<2,2,2,2,dlib::skip1<dlib::tag2<block<N,BN,2,dlib::tag1<SUBNET>>>>>>;

template <template <int,template<typename>class,int,typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_up = dlib::add_prev2<dlib::cont<N,2,2,2,2,dlib::skip1<dlib::tag2<blockt<N,BN,2,dlib::tag1<SUBNET>>>>>>;

template <int N, typename SUBNET> using res       = dlib::relu<residual<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares      = dlib::relu<residual<block,N,dlib::affine,SUBNET>>;
template <int N, typename SUBNET> using res_down  = dlib::relu<residual_down<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares_down = dlib::relu<residual_down<block,N,dlib::affine,SUBNET>>;
template <int N, typename SUBNET> using res_up    = dlib::relu<residual_up<block,N,dlib::bn_con,SUBNET>>;
template <int N, typename SUBNET> using ares_up   = dlib::relu<residual_up<block,N,dlib::affine,SUBNET>>;

// ----------------------------------------------------------------------------------------

template <typename SUBNET> using res64  = res<64, SUBNET>;
template <typename SUBNET> using res128 = res<128, SUBNET>;
template <typename SUBNET> using res256 = res<256, SUBNET>;
template <typename SUBNET> using res512 = res<512, SUBNET>;
template <typename SUBNET> using ares64  = ares<64, SUBNET>;
template <typename SUBNET> using ares128 = ares<128, SUBNET>;
template <typename SUBNET> using ares256 = ares<256, SUBNET>;
template <typename SUBNET> using ares512 = ares<512, SUBNET>;


template <typename SUBNET> using level1 = dlib::repeat<2,res512,res_down<512,SUBNET>>;
template <typename SUBNET> using level2 = dlib::repeat<2,res256,res_down<256,SUBNET>>;
template <typename SUBNET> using level3 = dlib::repeat<2,res128,res_down<128,SUBNET>>;
template <typename SUBNET> using level4 = dlib::repeat<2,res64,res<64,SUBNET>>;

template <typename SUBNET> using alevel1 = dlib::repeat<2,ares512,ares_down<512,SUBNET>>;
template <typename SUBNET> using alevel2 = dlib::repeat<2,ares256,ares_down<256,SUBNET>>;
template <typename SUBNET> using alevel3 = dlib::repeat<2,ares128,ares_down<128,SUBNET>>;
template <typename SUBNET> using alevel4 = dlib::repeat<2,ares64,ares<64,SUBNET>>;

template <typename SUBNET> using level1t = dlib::repeat<2,res512,res_up<512,SUBNET>>;
template <typename SUBNET> using level2t = dlib::repeat<2,res256,res_up<256,SUBNET>>;
template <typename SUBNET> using level3t = dlib::repeat<2,res128,res_up<128,SUBNET>>;
template <typename SUBNET> using level4t = dlib::repeat<2,res64,res_up<64,SUBNET>>;

template <typename SUBNET> using alevel1t = dlib::repeat<2,ares512,ares_up<512,SUBNET>>;
template <typename SUBNET> using alevel2t = dlib::repeat<2,ares256,ares_up<256,SUBNET>>;
template <typename SUBNET> using alevel3t = dlib::repeat<2,ares128,ares_up<128,SUBNET>>;
template <typename SUBNET> using alevel4t = dlib::repeat<2,ares64,ares_up<64,SUBNET>>;

// ----------------------------------------------------------------------------------------

template <typename SUBNET> using level1_u = dlib::repeat<2,res64,res<64,SUBNET>>;
template <typename SUBNET> using level2_u = dlib::repeat<2,res128,res_down<128,SUBNET>>;
template <typename SUBNET> using level3_u = dlib::repeat<2,res256,res_down<256,SUBNET>>;
template <typename SUBNET> using level4_u = dlib::repeat<2,res512,res_down<512,SUBNET>>;

template <typename SUBNET> using alevel1_u = dlib::repeat<2,ares64,ares<64,SUBNET>>;
template <typename SUBNET> using alevel2_u = dlib::repeat<2,ares128,ares_down<128,SUBNET>>;
template <typename SUBNET> using alevel3_u = dlib::repeat<2,ares256,ares_down<256,SUBNET>>;
template <typename SUBNET> using alevel4_u = dlib::repeat<2,ares512,ares_down<512,SUBNET>>;

template <typename SUBNET> using level1t_u = dlib::repeat<2,res64,res_up<64,SUBNET>>;
template <typename SUBNET> using level2t_u = dlib::repeat<2,res128,res_up<128,SUBNET>>;
template <typename SUBNET> using level3t_u = dlib::repeat<2,res256,res_up<256,SUBNET>>;
template <typename SUBNET> using level4t_u = dlib::repeat<2,res512,res_up<512,SUBNET>>;

template <typename SUBNET> using alevel1t_u = dlib::repeat<2,ares64,ares_up<64,SUBNET>>;
template <typename SUBNET> using alevel2t_u = dlib::repeat<2,ares128,ares_up<128,SUBNET>>;
template <typename SUBNET> using alevel3t_u = dlib::repeat<2,ares256,ares_up<256,SUBNET>>;
template <typename SUBNET> using alevel4t_u = dlib::repeat<2,ares512,ares_up<512,SUBNET>>;

// ----------------------------------------------------------------------------------------

// ----------------------------------------------------------------------------------------

template <
    template<typename> class TAGGED,
    template<typename> class PREV_RESIZED,
    typename SUBNET
>
using resize_and_concat = dlib::add_layer<
                          dlib::concat_<TAGGED,PREV_RESIZED>,
                          PREV_RESIZED<dlib::resize_prev_to_tagged<TAGGED,SUBNET>>>;

template <typename SUBNET> using utag1 = dlib::add_tag_layer<2100+1,SUBNET>;
template <typename SUBNET> using utag2 = dlib::add_tag_layer<2100+2,SUBNET>;
template <typename SUBNET> using utag3 = dlib::add_tag_layer<2100+3,SUBNET>;
template <typename SUBNET> using utag4 = dlib::add_tag_layer<2100+4,SUBNET>;

template <typename SUBNET> using utag1_ = dlib::add_tag_layer<2110+1,SUBNET>;
template <typename SUBNET> using utag2_ = dlib::add_tag_layer<2110+2,SUBNET>;
template <typename SUBNET> using utag3_ = dlib::add_tag_layer<2110+3,SUBNET>;
template <typename SUBNET> using utag4_ = dlib::add_tag_layer<2110+4,SUBNET>;

template <typename SUBNET> using concat_utag1 = resize_and_concat<utag1,utag1_,SUBNET>;
template <typename SUBNET> using concat_utag2 = resize_and_concat<utag2,utag2_,SUBNET>;
template <typename SUBNET> using concat_utag3 = resize_and_concat<utag3,utag3_,SUBNET>;
template <typename SUBNET> using concat_utag4 = resize_and_concat<utag4,utag4_,SUBNET>;

// ----------------------------------------------------------------------------------------

// training network type
using net_type = dlib::loss_multiclass_log_per_pixel<
                            dlib::cont<class_count,7,7,2,2,
                            level4t<level3t<level2t<level1t<
                            level1<level2<level3<level4<
                            dlib::max_pool<3,3,2,2,dlib::relu<dlib::bn_con<dlib::con<64,7,7,2,2,
                            dlib::input<dlib::matrix<dlib::rgb_pixel>>
                            >>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using anet_type = dlib::loss_multiclass_log_per_pixel<
                            dlib::cont<class_count,7,7,2,2,
                            alevel4t<alevel3t<alevel2t<alevel1t<
                            alevel1<alevel2<alevel3<alevel4<
                            dlib::max_pool<3,3,2,2,dlib::relu<dlib::affine<dlib::con<64,7,7,2,2,
                            dlib::input<dlib::matrix<dlib::rgb_pixel>>
                            >>>>>>>>>>>>>>;

// ----------------------------------------------------------------------------------------

// training network type
using ubnet_type = dlib::loss_multiclass_log_per_pixel<
                               dlib::cont<class_count,1,1,1,1,
                               dlib::relu<dlib::bn_con<dlib::cont<64,7,7,2,2,
                               concat_utag1<level1t_u<
                               concat_utag2<level2t_u<
                               concat_utag3<level3t_u<
                               concat_utag4<level4t_u<
                               level4_u<utag4<
                               level3_u<utag3<
                               level2_u<utag2<
                               level1_u<dlib::max_pool<3,3,2,2,utag1<
                               dlib::relu<dlib::bn_con<dlib::con<64,7,7,2,2,
                               dlib::input<dlib::matrix<dlib::rgb_pixel>>
                               >>>>>>>>>>>>>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using uanet_type = dlib::loss_multiclass_log_per_pixel<
                               dlib::cont<class_count,1,1,1,1,
                               dlib::relu<dlib::affine<dlib::cont<64,7,7,2,2,
                               concat_utag1<alevel1t_u<
                               concat_utag2<alevel2t_u<
                               concat_utag3<alevel3t_u<
                               concat_utag4<alevel4t_u<
                               alevel4_u<utag4<
                               alevel3_u<utag3<
                               alevel2_u<utag2<
                               alevel1_u<dlib::max_pool<3,3,2,2,utag1<
                               dlib::relu<dlib::affine<dlib::con<64,7,7,2,2,
                               dlib::input<dlib::matrix<dlib::rgb_pixel>>
                               >>>>>>>>>>>>>>>>>>>>>>>>>;
// ----------------------------------------------------------------------------------------
#pragma endregion type definitions

#endif