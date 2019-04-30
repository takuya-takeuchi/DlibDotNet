#ifndef _CPP_SHAPE_PREDICTOR_TRAINER_H_
#define _CPP_SHAPE_PREDICTOR_TRAINER_H_

#include "../export.h"
#include <dlib/image_transforms/assign_image.h>
#include <dlib/image_processing/shape_predictor_trainer.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region template

#define shape_predictor_trainer_train_template(__TYPE__, trainer, images, objects, predictor) \
do {\
    dlib::array<array2d<__TYPE__>>& in_images = *static_cast<dlib::array<array2d<__TYPE__>>*>(images);\
    std::vector<std::vector<full_object_detection>> in_Objects;\
    vector_vector_pointer_to_value(full_object_detection, objects, in_Objects);\
\
    auto p = trainer->train(in_images, in_Objects);\
    *predictor = new shape_predictor(p);\
} while (0)

#pragma endregion template

DLLEXPORT shape_predictor_trainer* shape_predictor_trainer_new()
{
    return new shape_predictor_trainer();
}

DLLEXPORT void shape_predictor_trainer_delete(shape_predictor_trainer* trainer)
{
    delete trainer;
}

DLLEXPORT unsigned long shape_predictor_trainer_get_cascade_depth(shape_predictor_trainer* trainer)
{
	return trainer->get_cascade_depth();
}

DLLEXPORT void shape_predictor_trainer_set_cascade_depth(shape_predictor_trainer* trainer, const unsigned long depth)
{
	return trainer->set_cascade_depth(depth);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_tree_depth(shape_predictor_trainer* trainer)
{
	return trainer->get_tree_depth();
}

DLLEXPORT void shape_predictor_trainer_set_tree_depth(shape_predictor_trainer* trainer, const unsigned long depth)
{
	return trainer->set_tree_depth(depth);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_num_trees_per_cascade_level(shape_predictor_trainer* trainer)
{
	return trainer->get_num_trees_per_cascade_level();
}

DLLEXPORT void shape_predictor_trainer_set_num_trees_per_cascade_level(shape_predictor_trainer* trainer, const unsigned long num)
{
	return trainer->set_num_trees_per_cascade_level(num);
}

DLLEXPORT double shape_predictor_trainer_get_nu(shape_predictor_trainer* trainer)
{
	return trainer->get_nu();
}

DLLEXPORT void shape_predictor_trainer_set_nu(shape_predictor_trainer* trainer, const double nu)
{
	return trainer->set_nu(nu);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_oversampling_amount(shape_predictor_trainer* trainer)
{
	return trainer->get_oversampling_amount();
}

DLLEXPORT void shape_predictor_trainer_set_oversampling_amount(shape_predictor_trainer* trainer, const unsigned long amount)
{
	return trainer->set_oversampling_amount(amount);
}

DLLEXPORT double shape_predictor_trainer_get_oversampling_translation_jitter(shape_predictor_trainer* trainer)
{
	return trainer->get_oversampling_translation_jitter();
}

DLLEXPORT void shape_predictor_trainer_set_oversampling_translation_jitter(shape_predictor_trainer* trainer, const double amount)
{
	return trainer->set_oversampling_translation_jitter(amount);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_feature_pool_size(shape_predictor_trainer* trainer)
{
	return trainer->get_feature_pool_size();
}

DLLEXPORT void shape_predictor_trainer_set_feature_pool_size(shape_predictor_trainer* trainer, const unsigned long size)
{
	return trainer->set_feature_pool_size(size);
}

DLLEXPORT double shape_predictor_trainer_get_lambda(shape_predictor_trainer* trainer)
{
	return trainer->get_lambda();
}

DLLEXPORT void shape_predictor_trainer_set_lambda(shape_predictor_trainer* trainer, const double lambda)
{
	return trainer->set_lambda(lambda);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_num_test_splits(shape_predictor_trainer* trainer)
{
	return trainer->get_num_test_splits();
}

DLLEXPORT void shape_predictor_trainer_set_num_test_splits(shape_predictor_trainer* trainer, const unsigned long num)
{
	return trainer->set_num_test_splits(num);
}

DLLEXPORT unsigned long shape_predictor_trainer_get_num_threads(shape_predictor_trainer* trainer)
{
	return trainer->get_num_threads();
}

DLLEXPORT void shape_predictor_trainer_set_num_threads(shape_predictor_trainer* trainer, const unsigned long num)
{
	return trainer->set_num_threads(num);
}

DLLEXPORT double shape_predictor_trainer_get_feature_pool_region_padding(shape_predictor_trainer* trainer)
{
	return trainer->get_feature_pool_region_padding();
}

DLLEXPORT void shape_predictor_trainer_set_feature_pool_region_padding(shape_predictor_trainer* trainer, const double padding)
{
	return trainer->set_feature_pool_region_padding(padding);
}

DLLEXPORT shape_predictor_trainer::padding_mode_t shape_predictor_trainer_get_padding_mode(shape_predictor_trainer* trainer)
{
	return trainer->get_padding_mode();
}

DLLEXPORT void shape_predictor_trainer_set_padding_mode(shape_predictor_trainer* trainer, const shape_predictor_trainer::padding_mode_t mode)
{
	return trainer->set_padding_mode(mode);
}

DLLEXPORT std::string* shape_predictor_trainer_get_random_seed(shape_predictor_trainer* trainer)
{
	auto ret = trainer->get_random_seed();
	return new std::string(ret);
}

DLLEXPORT void shape_predictor_trainer_set_random_seed(shape_predictor_trainer* trainer, std::string* seed)
{
	return trainer->set_random_seed(*seed);
}

DLLEXPORT void shape_predictor_trainer_be_verbose(shape_predictor_trainer* trainer)
{
	trainer->be_verbose();
}

DLLEXPORT void shape_predictor_trainer_be_quiet(shape_predictor_trainer* trainer)
{
	trainer->be_quiet();
}

DLLEXPORT int shape_predictor_trainer_train(shape_predictor_trainer* trainer,
                                            array2d_type img_type,
                                            void* images,
                                            std::vector<std::vector<full_object_detection*>*>* objects,
                                            shape_predictor** predictor)
{
    int err = ERR_OK;

    switch(img_type)
    {
        case array2d_type::UInt8:
            shape_predictor_trainer_train_template(uint8_t, trainer, images, objects, predictor);
            break;
        case array2d_type::UInt16:
            shape_predictor_trainer_train_template(uint16_t, trainer, images, objects, predictor);
            break;
        case array2d_type::UInt32:
            shape_predictor_trainer_train_template(uint32_t, trainer, images, objects, predictor);
            break;
        case array2d_type::Int8:
            shape_predictor_trainer_train_template(int8_t, trainer, images, objects, predictor);
            break;
        case array2d_type::Int16:
            shape_predictor_trainer_train_template(int16_t, trainer, images, objects, predictor);
            break;
        case array2d_type::Int32:
            shape_predictor_trainer_train_template(int32_t, trainer, images, objects, predictor);
            break;
        case array2d_type::Float:
            shape_predictor_trainer_train_template(float, trainer, images, objects, predictor);
            break;
        case array2d_type::Double:
            shape_predictor_trainer_train_template(double, trainer, images, objects, predictor);
            break;
        case array2d_type::RgbPixel:
            shape_predictor_trainer_train_template(rgb_pixel, trainer, images, objects, predictor);
            break;
        case array2d_type::HsiPixel:
            shape_predictor_trainer_train_template(hsi_pixel, trainer, images, objects, predictor);
            break;
        case array2d_type::RgbAlphaPixel:
            shape_predictor_trainer_train_template(rgb_alpha_pixel, trainer, images, objects, predictor);
            break;
        default:
            err = ERR_ARRAY2D_TYPE_NOT_SUPPORT;
            break;
    }

    return err;
}

#endif