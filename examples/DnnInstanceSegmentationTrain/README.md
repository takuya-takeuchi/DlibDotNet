# DNN Instance Segmentation Train 
  
This program is ported by C# from examples\dnn_instance_segmentation_train_ex.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;DnnInstanceSegmentationTrain_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;DnnInstanceSegmentationTrain_dir&gt;\bin\Release\netcoreapp2.0. 
 
**NOTE**   
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA. 
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.   
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS). 
 
And extract them and copy to extracted files to &lt;DnnInstanceSegmentationTrain_dir&gt;. 
 
## 2. Download demo data 
 
Download test data from the following urls. 
 
- http://host.robots.ox.ac.uk/pascal/VOC/voc2012/VOCtrainval_11-May-2012.tar 
 
And extract them and copy to extracted files to &lt;DnnInstanceSegmentationTrain_dir&gt;. 
 
## 3. Run 
 
The following result is example. 
I do not have GPU which equip large memory. So I changed learing rate. 
But you can understand what this program works. 
 
```` 
cd <DnnInstanceSegmentationTrain_dir> 
dotnet run --configuration Release -- <VOC2012_dir> <detBatchSize> <segBatchSize> 
 
SCANNING PASCAL VOC2012 DATASET

images in entire dataset: 1464
det mini-batch size: 8
seg mini-batch size: 60
desired classlabels: bicycle car cat

Extracting all truth instances... Done!

images in dataset filtered by class: 311
images in dataset after ignoring some truth boxes: 262

Training detector network:
dnn_trainer details:
  net_type::num_layers:  21
  net size: 0.00196838MB
  net architecture hash: 201e30c7fcafacc7a7b129480b930f4a
  loss: loss_mmod        (detector_windows:(bicycle:70x52,70x32,52x70;car:70x39,70x68,88x30;cat:70x61,73x30,51x70), loss per FA:1, loss per miss:1, truth match IOU thresh:0.5, use_bounding_box_regression:0, overlaps_nms:(0.613008,0.928147), overlaps_ignore:(0.5,0.9))
  synchronization file:                       pascal_voc2012_det_trainer_state_file.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      5000
  test iterations without progress threshold: 500

step#: 0     learning rate: 0.1   average loss: 0            steps without apparent progress: 0
Saved state to pascal_voc2012_det_trainer_state_file.dat

Training segmentation network for class bicycle:
dnn_trainer details:
  net_type::num_layers:  239
  net size: 0.0164766MB
  net architecture hash: ba3432c6a6f8c9b5a333a05b2fed119e
  loss: loss_multiclass_log_per_pixel
  synchronization file:                       pascal_voc2012_seg_trainer_state_file_bicycle.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      2000
  test iterations without progress threshold: 500

step#: 0     learning rate: 0.1   average loss: 0            steps without apparent progress: 0
Saved state to pascal_voc2012_seg_trainer_state_file_bicycle.dat

Training segmentation network for class car:
dnn_trainer details:
  net_type::num_layers:  239
  net size: 0.0164766MB
  net architecture hash: ba3432c6a6f8c9b5a333a05b2fed119e
  loss: loss_multiclass_log_per_pixel
  synchronization file:                       pascal_voc2012_seg_trainer_state_file_car.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      2000
  test iterations without progress threshold: 500

step#: 0     learning rate: 0.1   average loss: 0            steps without apparent progress: 0
Saved state to pascal_voc2012_seg_trainer_state_file_car.dat

Training segmentation network for class cat:
dnn_trainer details:
  net_type::num_layers:  239
  net size: 0.0164766MB
  net architecture hash: ba3432c6a6f8c9b5a333a05b2fed119e
  loss: loss_multiclass_log_per_pixel
  synchronization file:                       pascal_voc2012_seg_trainer_state_file_cat.dat
  trainer.get_solvers()[0]:                   sgd: weight_decay=0.0001, momentum=0.9
  learning rate:                              0.1
  learning rate shrink factor:                0.1
  min learning rate:                          1e-05
  iterations without progress threshold:      2000
  test iterations without progress threshold: 500

step#: 0     learning rate: 0.1   average loss: 0            steps without apparent progress: 0
Saved state to pascal_voc2012_seg_trainer_state_file_cat.dat
Saving networks
````