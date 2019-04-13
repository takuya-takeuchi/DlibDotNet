# DNN Semantic Segmentation Train
 
This program is ported by C# from examples\dnn_semantic_segmentation_train_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnSemanticSegmentationTrain_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;DnnSemanticSegmentationTrain_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

And extract them and copy to extracted files to &lt;DnnSemanticSegmentationTrain_dir&gt;.

## 2. Download demo data

Download test data from the following urls.

- http://host.robots.ox.ac.uk/pascal/VOC/voc2012/VOCtrainval_11-May-2012.tar

And extract them and copy to extracted files to &lt;DnnSemanticSegmentationTrain_dir&gt;.

## 3. Run

The following result is example.
I do not have GPU which equip large memory. So I changed learing rate.
But you can understand what this program works.

````
cd <DnnSemanticSegmentationTrain_dir>
dotnet run --configuration Release <VOC2012_dir> <batchSize>

mini-batch size: 10

SCANNING PASCAL VOC2012 DATASET

images in dataset: 1464

dnn_trainer details:
  net_type::num_layers:  239
  net size: 153.279MB
  net architecture hash: ca957805a9e35b40ff4481384dc5d66e
  loss: loss_multiclass_log_per_pixel
  synchronization file:                       pascal_voc2012_trainer_state_file.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      5000
  test iterations without progress threshold: 500

step#: 3535  learning rate: 0.1   average loss: 0.796554     steps without apparent progress: 341
Saved state to pascal_voc2012_trainer_state_file.dat
saving network
Testing the network...
train accuracy  :  0.7273497658274
val accuracy    :  0.723289723139368
````