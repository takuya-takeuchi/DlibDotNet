# DNN Semantic Segmentation Train
 
This program is ported by C# from examples\dnn_semantic_segmentation_train_ex.cpp.

#### :bulb: NOTE

This program is based on dlib 19.16 version.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnSemanticSegmentationTrainOld_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnSemanticSegmentationTrainOld_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

And extract them and copy to extracted files to &lt;DnnSemanticSegmentationTrainOld_dir&gt;.

## 2. Download demo data

Download test data from the following urls.

- http://host.robots.ox.ac.uk/pascal/VOC/voc2012/VOCtrainval_11-May-2012.tar

And extract them and copy to extracted files to &lt;DnnSemanticSegmentationTrainOld_dir&gt;.

## 3. Run

The following result is example.
I do not have GPU which equip large memory. So I changed mini batch size and learing rate.
But you can understand what this program works.

````
cd <DnnSemanticSegmentationTrainOld_dir>
dotnet run --configuration Release <VOC2012_dir>

SCANNING PASCAL VOC2012 DATASET

images in dataset: 1464

dnn_trainer details:
  net_type::num_layers:  220
  net size: 147.726MB
  net architecture hash: d5a22ead89458736abb548a027288609
  loss: loss_multiclass_log_per_pixel
  synchronization file:                       pascal_voc2012_trainer_state_file.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      5000
  test iterations without progress threshold: 500

step#: 7647  learning rate: 0.1   average loss: 1.28987      steps without apparent progress: 975
..

Saved state to pascal_voc2012_trainer_state_file.dat
saving network
Testing the network...
train accuracy  :  0.737714387790948
val accuracy    :  0.730114111432285
````