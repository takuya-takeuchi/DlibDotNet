#ifndef _CPP_DNN_MMOD_FACE_DETECTOR_H_
#define _CPP_DNN_MMOD_FACE_DETECTOR_H_

#include "../export.h"
#include <dlib/matrix.h>
#include <dlib/dnn.h>
#include <iostream>
#include "../shared.h"

using namespace dlib;
using namespace std;

template <long num_filters, typename SUBNET> using con5d = con<num_filters, 5, 5, 2, 2, SUBNET>;
template <long num_filters, typename SUBNET> using con5 = con<num_filters, 5, 5, 1, 1, SUBNET>;
template <typename SUBNET> using downsampler = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16, SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5 = relu<affine<con5<45, SUBNET>>>;
using net_type = loss_mmod<con<1, 9, 9, 1, 1, rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;

//https://github.com/davisking/dlib/blob/master/examples/dnn_mmod_face_detection_ex.cpp
//train model come form http://dlib.net/files/mmod_human_face_detector.dat.bz2

DLLEXPORT net_type* get_dnn_mmod_face_detector(const char* model_filename)
{
	net_type* net = new net_type();
	dlib::deserialize(model_filename) >> (*net);
	return net;
}

DLLEXPORT int dnn_mmod_face_detector_operator(
	net_type* net,
	array2d_type img_type,
	void* img,
	const int upsample_num_times,
	std::vector<mmod_rect*> *dets)
{
	int err = ERR_OK;

	pyramid_down<2> pyr;
	std::vector<mmod_rect>* ret = nullptr;
	matrix<rgb_pixel> image;

	switch (img_type)
	{
		case array2d_type::UInt8:
		{
			assign_image(image, *((array2d<byte>*)img));
		}
		break;
		case array2d_type::UInt16:
		case array2d_type::Int32:
		case array2d_type::Float:
		case array2d_type::Double:
		case array2d_type::RgbPixel:
		{
			assign_image(image, *((array2d<rgb_pixel>*)img));
		}
		break;
		case array2d_type::HsiPixel:
		case array2d_type::RgbAlphaPixel:
		default:
			err = ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT;
			break;
	}

	unsigned int levels = upsample_num_times;
	while (levels > 0)
	{
		levels--;
		dlib::pyramid_up(image, pyr);
	}

	std::vector<mmod_rect> result = (*net)(image);

	// Scale the detection locations back to the original image size
	// if the image was upscaled.
	for (int index = 0; index < result.size(); index++) {
		mmod_rect d = result[index];
		d.rect = pyr.rect_down(d.rect, upsample_num_times);
		dets->push_back(new mmod_rect(d));
	}
	return err;
}

DLLEXPORT void dnn_mmod_face_detector_delete(net_type* net)
{
	delete net;
}

#endif