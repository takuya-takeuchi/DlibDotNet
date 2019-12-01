#ifndef _CPP_IMAGE_DATASET_METADATA_H_
#define _CPP_IMAGE_DATASET_METADATA_H_

#include "../export.h"
#include <dlib/pixel.h>
#include <dlib/matrix.h>
#include <dlib/data_io/image_dataset_metadata.h>
#include "../shared.h"

using namespace dlib;
using namespace std;

#pragma region box

DLLEXPORT image_dataset_metadata::box* image_dataset_metadata_box_new()
{
    return new image_dataset_metadata::box();
}

DLLEXPORT double image_dataset_metadata_box_get_age(image_dataset_metadata::box* box)
{
    return box->age;
}

DLLEXPORT void image_dataset_metadata_box_set_age(image_dataset_metadata::box* box, double value)
{
    box->age = value;
}

DLLEXPORT double image_dataset_metadata_box_get_angle(image_dataset_metadata::box* box)
{
    return box->angle;
}

DLLEXPORT void image_dataset_metadata_box_set_angle(image_dataset_metadata::box* box, double value)
{
    box->angle = value;
}

DLLEXPORT image_dataset_metadata::gender_t image_dataset_metadata_box_get_gender(image_dataset_metadata::box* box)
{
    return box->gender;
}

DLLEXPORT void image_dataset_metadata_box_set_gender(image_dataset_metadata::box* box, image_dataset_metadata::gender_t value)
{
    box->gender = value;
}

DLLEXPORT bool image_dataset_metadata_box_has_label(image_dataset_metadata::box* box)
{
    return box->has_label();
}

DLLEXPORT double image_dataset_metadata_box_get_detection_score(image_dataset_metadata::box* box)
{
    return box->detection_score;
}

#pragma region parts

DLLEXPORT bool image_dataset_metadata_box_get_parts_get_value(image_dataset_metadata::box* box, const char* key, const int key_length, dlib::point** result)
{
    std::map<std::string,point>& m = box->parts;
    std::string k(key, key_length);
    if (m.find(k) == m.end())
    {
        return false;
    }
    else
    {
        *result = new dlib::point(m[k]);
        return true;
    }
}

DLLEXPORT void image_dataset_metadata_box_get_parts_set_value(image_dataset_metadata::box* box, const char* key, const int key_length, dlib::point* value)
{
    std::map<std::string,dlib::point>& m = box->parts;
    dlib::point p(*value);
    std::string k(key, key_length);
    m[k] = p;
}

DLLEXPORT void image_dataset_metadata_box_parts_clear(image_dataset_metadata::box* box)
{
    std::map<std::string,dlib::point>& m = box->parts;
    m.clear();
}

DLLEXPORT int image_dataset_metadata_box_get_parts_get_size(image_dataset_metadata::box* box)
{
    return box->parts.size();
}

DLLEXPORT void image_dataset_metadata_box_get_parts_get_all(image_dataset_metadata::box* box,
                                                            std::vector<std::string*>* strings,
                                                            std::vector<dlib::point*>* points)
{
    std::map<std::string,dlib::point>& m = box->parts;
    auto end = m.end();
    for (auto it = m.begin(); it != end; it++)
    {
        auto f = it->first;
        auto s = it->second;
        strings->push_back(new std::string(f));
        points->push_back(new dlib::point(s));
    }
}

#pragma endregion parts

DLLEXPORT void image_dataset_metadata_box_set_detection_score(image_dataset_metadata::box* box, double value)
{
    box->detection_score = value;
}

DLLEXPORT bool image_dataset_metadata_box_get_difficult(image_dataset_metadata::box* box)
{
    return box->difficult;
}

DLLEXPORT void image_dataset_metadata_box_set_difficult(image_dataset_metadata::box* box, bool value)
{
    box->difficult = value;
}

DLLEXPORT bool image_dataset_metadata_box_get_ignore(image_dataset_metadata::box* box)
{
    return box->ignore;
}

DLLEXPORT void image_dataset_metadata_box_set_ignore(image_dataset_metadata::box* box, bool value)
{
    box->ignore = value;
}

DLLEXPORT std::string* image_dataset_metadata_box_get_label(image_dataset_metadata::box* box)
{
    return new std::string(box->label);
}

DLLEXPORT void image_dataset_metadata_box_set_label(image_dataset_metadata::box* box, const char* value, const int value_length)
{
    box->label = std::string(value, value_length);
}

DLLEXPORT bool image_dataset_metadata_box_get_occluded(image_dataset_metadata::box* box)
{
    return box->occluded;
}

DLLEXPORT void image_dataset_metadata_box_set_occluded(image_dataset_metadata::box* box, bool value)
{
    box->occluded = value;
}

DLLEXPORT double image_dataset_metadata_box_get_pose(image_dataset_metadata::box* box)
{
    return box->pose;
}

DLLEXPORT void image_dataset_metadata_box_set_pose(image_dataset_metadata::box* box, double value)
{
    box->pose = value;
}

DLLEXPORT dlib::rectangle* image_dataset_metadata_box_get_rect(image_dataset_metadata::box* box)
{
    return new dlib::rectangle(box->rect);
}

DLLEXPORT void image_dataset_metadata_box_set_rect(image_dataset_metadata::box* box, dlib::rectangle* value)
{
    box->rect = *value;
}

DLLEXPORT bool image_dataset_metadata_box_get_truncated(image_dataset_metadata::box* box)
{
    return box->truncated;
}

DLLEXPORT void image_dataset_metadata_box_set_truncated(image_dataset_metadata::box* box, bool value)
{
    box->truncated = value;
}

DLLEXPORT void image_dataset_metadata_box_delete(image_dataset_metadata::box* box)
{
    delete box;
}

#pragma endregion box

#pragma region dataset

DLLEXPORT image_dataset_metadata::dataset* image_dataset_metadata_dataset_new()
{
    return new image_dataset_metadata::dataset();
}

DLLEXPORT std::string* image_dataset_metadata_dataset_get_comment(image_dataset_metadata::dataset* dataset)
{
    return new std::string(dataset->comment);
}

DLLEXPORT void image_dataset_metadata_dataset_set_comment(image_dataset_metadata::dataset* dataset, const char* value, const int value_length)
{
    dataset->comment = std::string(value, value_length);
}

#pragma region images

DLLEXPORT std::vector<image_dataset_metadata::image*>* image_dataset_metadata_dataset_get_images(image_dataset_metadata::dataset* dataset)
{
    std::vector<image_dataset_metadata::image>& src = dataset->images;
    auto dst = new std::vector<image_dataset_metadata::image*>();
    vector_to_new_instance(dlib::image_dataset_metadata::image, src, dst);
    return dst;
}

DLLEXPORT image_dataset_metadata::image* image_dataset_metadata_dataset_get_images_at(image_dataset_metadata::dataset* dataset, int index)
{
    image_dataset_metadata::image& image = dataset->images.at(index);
    return &image;
}

DLLEXPORT int image_dataset_metadata_dataset_get_images_get_size(image_dataset_metadata::dataset* dataset)
{
    return dataset->images.size();
}

DLLEXPORT void image_dataset_metadata_dataset_get_images_clear(image_dataset_metadata::dataset* dataset)
{
    dataset->images.clear();
}

DLLEXPORT void image_dataset_metadata_dataset_get_images_push_back(image_dataset_metadata::dataset* dataset, image_dataset_metadata::image* image)
{
    image_dataset_metadata::image& i = *image;
    dataset->images.push_back(i);
}

DLLEXPORT void image_dataset_metadata_dataset_get_images_remove_at(image_dataset_metadata::dataset* dataset, int index)
{
    dataset->images.erase(dataset->images.begin() + index);
}

#pragma endregion images

DLLEXPORT std::string* image_dataset_metadata_dataset_get_name(image_dataset_metadata::dataset* dataset)
{
    return new std::string(dataset->name);
}

DLLEXPORT void image_dataset_metadata_dataset_set_name(image_dataset_metadata::dataset* dataset, const char* value, const int value_length)
{
    dataset->name = std::string(value, value_length);
}

DLLEXPORT void image_dataset_metadata_dataset_delete(image_dataset_metadata::dataset* dataset)
{
    delete dataset;
}

#pragma endregion dataset

#pragma region image

DLLEXPORT image_dataset_metadata::image* image_dataset_metadata_image_new(const char* filename, const int filename_length)
{
    std::string str(filename, filename_length);
    return new image_dataset_metadata::image(str);
}

DLLEXPORT image_dataset_metadata::image* image_dataset_metadata_image_new2()
{
    return new image_dataset_metadata::image();
}

#pragma region boxes

DLLEXPORT std::vector<image_dataset_metadata::box*>* image_dataset_metadata_dataset_get_boxes(image_dataset_metadata::image* image)
{
    std::vector<image_dataset_metadata::box>& src = image->boxes;
    auto dst = new std::vector<image_dataset_metadata::box*>();
    vector_to_new_instance(dlib::image_dataset_metadata::box, src, dst);
    return dst;
}

DLLEXPORT image_dataset_metadata::box* image_dataset_metadata_dataset_get_boxes_at(image_dataset_metadata::image* image, int index)
{
    image_dataset_metadata::box& box = image->boxes.at(index);
    return &box;
}

DLLEXPORT int image_dataset_metadata_dataset_get_boxes_get_size(image_dataset_metadata::image* image)
{
    return image->boxes.size();
}

DLLEXPORT void image_dataset_metadata_dataset_get_boxes_clear(image_dataset_metadata::image* image)
{
    image->boxes.clear();
}

DLLEXPORT void image_dataset_metadata_dataset_get_boxes_push_back(image_dataset_metadata::image* image, image_dataset_metadata::box* box)
{
    image_dataset_metadata::box& b = *box;
    image->boxes.push_back(b);
}

#pragma endregion boxes

DLLEXPORT std::string* image_dataset_metadata_image_get_filename(image_dataset_metadata::image* image)
{
    return new std::string(image->filename);
}

DLLEXPORT void image_dataset_metadata_image_set_filename(image_dataset_metadata::image* image, const char* value, const int value_length)
{
    image->filename = std::string(value, value_length);
}

DLLEXPORT void image_dataset_metadata_image_delete(image_dataset_metadata::image* image)
{
    delete image;
}

#pragma endregion image

DLLEXPORT int load_image_dataset_metadata(image_dataset_metadata::dataset* meta, const char* filename, const int filename_length)
{
    int err = ERR_OK;

    image_dataset_metadata::dataset& in_meta = *meta;
    std::string in_filename(filename, filename_length);
    try
    {    
        dlib::image_dataset_metadata::load_image_dataset_metadata(in_meta, filename);
    }
    catch(dlib::error e)
    {
        err = ERR_GENERAL_FILE_IO;
    }

    return err;
}

DLLEXPORT int save_image_dataset_metadata(image_dataset_metadata::dataset* meta, const char* filename, const int filename_length)
{
    int err = ERR_OK;

    const image_dataset_metadata::dataset& in_meta = *meta;
    std::string in_filename(filename, filename_length);
    try
    {
        dlib::image_dataset_metadata::save_image_dataset_metadata(in_meta, filename);
    }
    catch(dlib::error e)
    {
        err = ERR_GENERAL_FILE_IO;
    }

    return err;
}

#endif