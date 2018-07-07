#ifndef _CPP_MNIST_H_
#define _CPP_MNIST_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/data_io/mnist.h>
#include "../shared.h"
 
using namespace dlib;
using namespace std;

DLLEXPORT void load_mnist_dataset (
        const char* folder_name,
        std::vector<matrix<unsigned char>*>** training_images,
        std::vector<unsigned long>** training_labels,
        std::vector<matrix<unsigned char>*>** testing_images,
        std::vector<unsigned long>** testing_labels
    )
{
    std::string tmp(folder_name);
    std::vector<matrix<unsigned char>> tmp_training_images;
    std::vector<unsigned long>         tmp_training_labels;
    std::vector<matrix<unsigned char>> tmp_testing_images;
    std::vector<unsigned long>         tmp_testing_labels;
    load_mnist_dataset(tmp, tmp_training_images, tmp_training_labels, tmp_testing_images, tmp_testing_labels);

    auto ret_training_images = new std::vector<matrix<unsigned char>*>();
    auto ret_training_labels = new std::vector<unsigned long>();
    auto ret_testing_images = new std::vector<matrix<unsigned char>*>();
    auto ret_testing_labels = new std::vector<unsigned long>();

    for (int i = 0; i < tmp_training_images.size(); i++)
        ret_training_images->push_back(new matrix<unsigned char>(tmp_training_images[i]));
    for (int i = 0; i < tmp_training_labels.size(); i++)
        ret_training_labels->push_back(tmp_training_labels[i]);
    for (int i = 0; i < tmp_testing_images.size(); i++)
        ret_testing_images->push_back(new matrix<unsigned char>(tmp_testing_images[i]));
    for (int i = 0; i < tmp_testing_labels.size(); i++)
        ret_testing_labels->push_back(tmp_testing_labels[i]);

    *training_images = ret_training_images;
    *training_labels = ret_training_labels;
    *testing_images = ret_testing_images;
    *testing_labels = ret_testing_labels;
}

#endif