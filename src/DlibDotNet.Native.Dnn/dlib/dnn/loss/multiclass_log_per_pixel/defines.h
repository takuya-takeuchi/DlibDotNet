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

#pragma region 0-1
namespace net
{
// Introduce the building blocks used to define the segmentation network.
// The network first does residual downsampling (similar to the dnn_imagenet_(train_)ex 
// example program), and then residual upsampling. The network could be improved e.g.
// by introducing skip connections from the input image, and/or the first layers, to the
// last layer(s).  (See Long et al., Fully Convolutional Networks for Semantic Segmentation,
// https://people.eecs.berkeley.edu/~jonlong/long_shelhamer_fcn.pdf)

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

template <typename SUBNET> using res512 = res<512, SUBNET>;
template <typename SUBNET> using res256 = res<256, SUBNET>;
template <typename SUBNET> using res128 = res<128, SUBNET>;
template <typename SUBNET> using res64  = res<64, SUBNET>;
template <typename SUBNET> using ares512 = ares<512, SUBNET>;
template <typename SUBNET> using ares256 = ares<256, SUBNET>;
template <typename SUBNET> using ares128 = ares<128, SUBNET>;
template <typename SUBNET> using ares64  = ares<64, SUBNET>;


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
}
#pragma endregion 0-1

#pragma region 2-3
// https://github.com/davisking/dlib/blob/master/examples/dnn_semantic_segmentation_ex.h
namespace unet
{
// ----------------------------------------------------------------------------------------

// Introduce the building blocks used to define the segmentation network.
// The network first does residual downsampling (similar to the dnn_imagenet_(train_)ex
// example program), and then residual upsampling. In addition, U-Net style skip
// connections are used, so that not every simple detail needs to reprented on the low
// levels. (See Ronneberger et al. (2015), U-Net: Convolutional Networks for Biomedical
// Image Segmentation, https://arxiv.org/pdf/1505.04597.pdf)

template <int N, template <typename> class BN, int stride, typename SUBNET>
using block = BN<dlib::con<N,3,3,1,1,dlib::relu<BN<dlib::con<N,3,3,stride,stride,SUBNET>>>>>;

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

template <typename SUBNET> using res64 = res<64,SUBNET>;
template <typename SUBNET> using res128 = res<128,SUBNET>;
template <typename SUBNET> using res256 = res<256,SUBNET>;
template <typename SUBNET> using res512 = res<512,SUBNET>;
template <typename SUBNET> using ares64 = ares<64,SUBNET>;
template <typename SUBNET> using ares128 = ares<128,SUBNET>;
template <typename SUBNET> using ares256 = ares<256,SUBNET>;
template <typename SUBNET> using ares512 = ares<512,SUBNET>;

template <typename SUBNET> using level1 = dlib::repeat<2,res64,res<64,SUBNET>>;
template <typename SUBNET> using level2 = dlib::repeat<2,res128,res_down<128,SUBNET>>;
template <typename SUBNET> using level3 = dlib::repeat<2,res256,res_down<256,SUBNET>>;
template <typename SUBNET> using level4 = dlib::repeat<2,res512,res_down<512,SUBNET>>;

template <typename SUBNET> using alevel1 = dlib::repeat<2,ares64,ares<64,SUBNET>>;
template <typename SUBNET> using alevel2 = dlib::repeat<2,ares128,ares_down<128,SUBNET>>;
template <typename SUBNET> using alevel3 = dlib::repeat<2,ares256,ares_down<256,SUBNET>>;
template <typename SUBNET> using alevel4 = dlib::repeat<2,ares512,ares_down<512,SUBNET>>;

template <typename SUBNET> using level1t = dlib::repeat<2,res64,res_up<64,SUBNET>>;
template <typename SUBNET> using level2t = dlib::repeat<2,res128,res_up<128,SUBNET>>;
template <typename SUBNET> using level3t = dlib::repeat<2,res256,res_up<256,SUBNET>>;
template <typename SUBNET> using level4t = dlib::repeat<2,res512,res_up<512,SUBNET>>;

template <typename SUBNET> using alevel1t = dlib::repeat<2,ares64,ares_up<64,SUBNET>>;
template <typename SUBNET> using alevel2t = dlib::repeat<2,ares128,ares_up<128,SUBNET>>;
template <typename SUBNET> using alevel3t = dlib::repeat<2,ares256,ares_up<256,SUBNET>>;
template <typename SUBNET> using alevel4t = dlib::repeat<2,ares512,ares_up<512,SUBNET>>;

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
using ubnet_type = dlib::loss_multiclass_log_per_pixel<
                               dlib::cont<class_count,1,1,1,1,
                               dlib::relu<dlib::bn_con<dlib::cont<64,7,7,2,2,
                               concat_utag1<level1t<
                               concat_utag2<level2t<
                               concat_utag3<level3t<
                               concat_utag4<level4t<
                               level4<utag4<
                               level3<utag3<
                               level2<utag2<
                               level1<dlib::max_pool<3,3,2,2,utag1<
                               dlib::relu<dlib::bn_con<dlib::con<64,7,7,2,2,
                               dlib::input<dlib::matrix<dlib::rgb_pixel>>
                               >>>>>>>>>>>>>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using uanet_type = dlib::loss_multiclass_log_per_pixel<
                               dlib::cont<class_count,1,1,1,1,
                               dlib::relu<dlib::affine<dlib::cont<64,7,7,2,2,
                               concat_utag1<alevel1t<
                               concat_utag2<alevel2t<
                               concat_utag3<alevel3t<
                               concat_utag4<alevel4t<
                               alevel4<utag4<
                               alevel3<utag3<
                               alevel2<utag2<
                               alevel1<dlib::max_pool<3,3,2,2,utag1<
                               dlib::relu<dlib::affine<dlib::con<64,7,7,2,2,
                               dlib::input<dlib::matrix<dlib::rgb_pixel>>
                               >>>>>>>>>>>>>>>>>>>>>>>>>;

// ----------------------------------------------------------------------------------------
}
#pragma endregion 2-3

#pragma region 4-5
// https://github.com/davisking/dlib/blob/master/examples/dnn_instance_segmentation_ex.h
namespace instance_segmentation
{
// The segmentation network.
// For the time being, this is very much copy-paste from dnn_semantic_segmentation.h, although the network is made narrower (smaller feature maps).

template <int N, template <typename> class BN, int stride, typename SUBNET>
using block = BN<dlib::con<N,3,3,1,1,dlib::relu<BN<dlib::con<N,3,3,stride,stride,SUBNET>>>>>;

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

template <typename SUBNET> using res16 = res<16,SUBNET>;
template <typename SUBNET> using res24 = res<24,SUBNET>;
template <typename SUBNET> using res32 = res<32,SUBNET>;
template <typename SUBNET> using res48 = res<48,SUBNET>;
template <typename SUBNET> using ares16 = ares<16,SUBNET>;
template <typename SUBNET> using ares24 = ares<24,SUBNET>;
template <typename SUBNET> using ares32 = ares<32,SUBNET>;
template <typename SUBNET> using ares48 = ares<48,SUBNET>;

template <typename SUBNET> using level1 = dlib::repeat<2,res16,res<16,SUBNET>>;
template <typename SUBNET> using level2 = dlib::repeat<2,res24,res_down<24,SUBNET>>;
template <typename SUBNET> using level3 = dlib::repeat<2,res32,res_down<32,SUBNET>>;
template <typename SUBNET> using level4 = dlib::repeat<2,res48,res_down<48,SUBNET>>;

template <typename SUBNET> using alevel1 = dlib::repeat<2,ares16,ares<16,SUBNET>>;
template <typename SUBNET> using alevel2 = dlib::repeat<2,ares24,ares_down<24,SUBNET>>;
template <typename SUBNET> using alevel3 = dlib::repeat<2,ares32,ares_down<32,SUBNET>>;
template <typename SUBNET> using alevel4 = dlib::repeat<2,ares48,ares_down<48,SUBNET>>;

template <typename SUBNET> using level1t = dlib::repeat<2,res16,res_up<16,SUBNET>>;
template <typename SUBNET> using level2t = dlib::repeat<2,res24,res_up<24,SUBNET>>;
template <typename SUBNET> using level3t = dlib::repeat<2,res32,res_up<32,SUBNET>>;
template <typename SUBNET> using level4t = dlib::repeat<2,res48,res_up<48,SUBNET>>;

template <typename SUBNET> using alevel1t = dlib::repeat<2,ares16,ares_up<16,SUBNET>>;
template <typename SUBNET> using alevel2t = dlib::repeat<2,ares24,ares_up<24,SUBNET>>;
template <typename SUBNET> using alevel3t = dlib::repeat<2,ares32,ares_up<32,SUBNET>>;
template <typename SUBNET> using alevel4t = dlib::repeat<2,ares48,ares_up<48,SUBNET>>;

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
using seg_bnet_type = dlib::loss_multiclass_log_per_pixel<
                              dlib::cont<2,1,1,1,1,
                              dlib::relu<dlib::bn_con<dlib::cont<16,7,7,2,2,
                              concat_utag1<level1t<
                              concat_utag2<level2t<
                              concat_utag3<level3t<
                              concat_utag4<level4t<
                              level4<utag4<
                              level3<utag3<
                              level2<utag2<
                              level1<dlib::max_pool<3,3,2,2,utag1<
                              dlib::relu<dlib::bn_con<dlib::con<16,7,7,2,2,
                              dlib::input<dlib::matrix<dlib::rgb_pixel>>
                              >>>>>>>>>>>>>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using seg_anet_type = dlib::loss_multiclass_log_per_pixel<
                              dlib::cont<2,1,1,1,1,
                              dlib::relu<dlib::affine<dlib::cont<16,7,7,2,2,
                              concat_utag1<alevel1t<
                              concat_utag2<alevel2t<
                              concat_utag3<alevel3t<
                              concat_utag4<alevel4t<
                              alevel4<utag4<
                              alevel3<utag3<
                              alevel2<utag2<
                              alevel1<dlib::max_pool<3,3,2,2,utag1<
                              dlib::relu<dlib::affine<dlib::con<16,7,7,2,2,
                              dlib::input<dlib::matrix<dlib::rgb_pixel>>
                              >>>>>>>>>>>>>>>>>>>>>>>>>;

// ----------------------------------------------------------------------------------------
}
#pragma endregion 4-5

#pragma endregion type definitions

#endif