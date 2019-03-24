# DNN Inception
 
This program is ported by C# from examples\dnn_inception_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnInception_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnInception_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- http://yann.lecun.com/exdb/mnist/
  - train-images-idx3-ubyte.gz
  - train-labels-idx1-ubyte.gz
  - t10k-images-idx3-ubyte.gz 
  - t10k-labels-idx1-ubyte.gz

And extract them and copy to extracted files to &lt;DnnInception_dir&gt;.

## 3. Run

````
cd <DnnInception_dir>
dotnet run -c Release .

The net has 43 layers in it.
layer<0>	loss_multiclass_log
layer<1>	fc	 (num_outputs=10) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<2>	relu
layer<3>	fc	 (num_outputs=32) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<4>	max_pool (nr=2, nc=2, stride_y=2, stride_x=2, padding_y=0, padding_x=0)
layer<5>	concat	 (1001,1002,1003)
layer<6>	tag1001
layer<7>	relu
layer<8>	con	 (num_filters=4, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<9>	skip1000
layer<10>	tag1002
layer<11>	relu
layer<12>	con	 (num_filters=4, nr=3, nc=3, stride_y=1, stride_x=1, padding_y=1, padding_x=1) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<13>	skip1000
layer<14>	tag1003
layer<15>	relu
layer<16>	con	 (num_filters=4, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<17>	max_pool (nr=3, nc=3, stride_y=1, stride_x=1, padding_y=1, padding_x=1)
layer<18>	tag1000
layer<19>	max_pool (nr=2, nc=2, stride_y=2, stride_x=2, padding_y=0, padding_x=0)
layer<20>	concat	 (1001,1002,1003,1004)
layer<21>	tag1001
layer<22>	relu
layer<23>	con	 (num_filters=10, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<24>	skip1000
layer<25>	tag1002
layer<26>	relu
layer<27>	con	 (num_filters=10, nr=3, nc=3, stride_y=1, stride_x=1, padding_y=1, padding_x=1) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<28>	relu
layer<29>	con	 (num_filters=16, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<30>	skip1000
layer<31>	tag1003
layer<32>	relu
layer<33>	con	 (num_filters=10, nr=5, nc=5, stride_y=1, stride_x=1, padding_y=2, padding_x=2) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<34>	relu
layer<35>	con	 (num_filters=16, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<36>	skip1000
layer<37>	tag1004
layer<38>	relu
layer<39>	con	 (num_filters=10, nr=1, nc=1, stride_y=1, stride_x=1, padding_y=0, padding_x=0) learning_rate_mult=1 weight_decay_mult=1 bias_learning_rate_mult=1 bias_weight_decay_mult=0
layer<40>	max_pool (nr=3, nc=3, stride_y=1, stride_x=1, padding_y=1, padding_x=1)
layer<41>	tag1000
layer<42>	input<matrix>

Traning NN...
Epoch: 1     learning rate: 0.01  average loss: 0.480985     steps without apparent progress: 8
Epoch: 2     learning rate: 0.01  average loss: 0.105669     steps without apparent progress: 15
Epoch: 3     learning rate: 0.01  average loss: 0.0740526    steps without apparent progress: 318
Epoch: 4     learning rate: 0.01  average loss: 0.0604944    steps without apparent progress: 9
Saved state to inception_sync
Epoch: 5     learning rate: 0.01  average loss: 0.0520312    steps without apparent progress: 326
Epoch: 6     learning rate: 0.01  average loss: 0.0459913    steps without apparent progress: 350
Epoch: 7     learning rate: 0.01  average loss: 0.0413119    steps without apparent progress: 10
Epoch: 8     learning rate: 0.01  average loss: 0.0379306    steps without apparent progress: 26
Saved state to inception_sync_
Epoch: 9     learning rate: 0.01  average loss: 0.0350026    steps without apparent progress: 542
Epoch: 10    learning rate: 0.01  average loss: 0.0325879    steps without apparent progress: 11
Epoch: 11    learning rate: 0.01  average loss: 0.0306371    steps without apparent progress: 550
Epoch: 12    learning rate: 0.01  average loss: 0.0287543    steps without apparent progress: 517
Epoch: 13    learning rate: 0.01  average loss: 0.0271211    steps without apparent progress: 12
Saved state to inception_sync
Epoch: 14    learning rate: 0.01  average loss: 0.0258299    steps without apparent progress: 540
Epoch: 15    learning rate: 0.01  average loss: 0.0246587    steps without apparent progress: 518
Epoch: 16    learning rate: 0.01  average loss: 0.0235673    steps without apparent progress: 522
Epoch: 17    learning rate: 0.01  average loss: 0.0224451    steps without apparent progress: 558
Epoch: 18    learning rate: 0.01  average loss: 0.0216577    steps without apparent progress: 545
Saved state to inception_sync_
Epoch: 19    learning rate: 0.01  average loss: 0.0207857    steps without apparent progress: 566
Epoch: 20    learning rate: 0.01  average loss: 0.0197219    steps without apparent progress: 979
Epoch: 21    learning rate: 0.01  average loss: 0.0189983    steps without apparent progress: 989
Epoch: 22    learning rate: 0.01  average loss: 0.0183556    steps without apparent progress: 1037
Saved state to inception_sync
Epoch: 23    learning rate: 0.01  average loss: 0.0176799    steps without apparent progress: 986
Epoch: 24    learning rate: 0.01  average loss: 0.0169129    steps without apparent progress: 970
Epoch: 25    learning rate: 0.01  average loss: 0.0161669    steps without apparent progress: 979
Epoch: 26    learning rate: 0.01  average loss: 0.0154576    steps without apparent progress: 983
Epoch: 27    learning rate: 0.01  average loss: 0.0146988    steps without apparent progress: 985
Saved state to inception_sync_
Epoch: 28    learning rate: 0.01  average loss: 0.0141198    steps without apparent progress: 984
Epoch: 29    learning rate: 0.01  average loss: 0.0135091    steps without apparent progress: 984
Epoch: 30    learning rate: 0.01  average loss: 0.0130994    steps without apparent progress: 991
Epoch: 31    learning rate: 0.01  average loss: 0.0125059    steps without apparent progress: 979
Epoch: 32    learning rate: 0.01  average loss: 0.012067     steps without apparent progress: 983
Saved state to inception_sync
Epoch: 33    learning rate: 0.01  average loss: 0.0114886    steps without apparent progress: 998
Epoch: 34    learning rate: 0.01  average loss: 0.0109174    steps without apparent progress: 560
Epoch: 35    learning rate: 0.01  average loss: 0.0106421    steps without apparent progress: 986
Epoch: 36    learning rate: 0.01  average loss: 0.0103991    steps without apparent progress: 1060
Saved state to inception_sync_
Epoch: 37    learning rate: 0.01  average loss: 0.0100768    steps without apparent progress: 1061
Epoch: 38    learning rate: 0.01  average loss: 0.00983767   steps without apparent progress: 991
Epoch: 39    learning rate: 0.01  average loss: 0.00953358   steps without apparent progress: 1027
Epoch: 40    learning rate: 0.01  average loss: 0.00912254   steps without apparent progress: 988
Epoch: 41    learning rate: 0.01  average loss: 0.00889484   steps without apparent progress: 997
Saved state to inception_sync
Epoch: 42    learning rate: 0.01  average loss: 0.0085257    steps without apparent progress: 1059
Epoch: 43    learning rate: 0.01  average loss: 0.00825252   steps without apparent progress: 1033
Epoch: 44    learning rate: 0.01  average loss: 0.00806025   steps without apparent progress: 1462
Epoch: 45    learning rate: 0.01  average loss: 0.00785482   steps without apparent progress: 1451
Epoch: 46    learning rate: 0.01  average loss: 0.00769135   steps without apparent progress: 1033
Saved state to inception_sync_
Epoch: 47    learning rate: 0.01  average loss: 0.00719076   steps without apparent progress: 982
Epoch: 48    learning rate: 0.01  average loss: 0.00693816   steps without apparent progress: 995
Epoch: 49    learning rate: 0.01  average loss: 0.00693408   steps without apparent progress: 1461
Epoch: 50    learning rate: 0.01  average loss: 0.00671753   steps without apparent progress: 1497
Saved state to inception_sync
Epoch: 51    learning rate: 0.01  average loss: 0.00666447   steps without apparent progress: 1922
Epoch: 52    learning rate: 0.01  average loss: 0.00653228   steps without apparent progress: 1926
Epoch: 53    learning rate: 0.01  average loss: 0.0064682    steps without apparent progress: 1919
Epoch: 54    learning rate: 0.01  average loss: 0.00638581   steps without apparent progress: 1935
Epoch: 55    learning rate: 0.01  average loss: 0.00621068   steps without apparent progress: 1960
Saved state to inception_sync_
Epoch: 56    learning rate: 0.001  average loss: 0.00683079   steps without apparent progress: 1455
Epoch: 57    learning rate: 0.001  average loss: 0.00606328   steps without apparent progress: 1928
Epoch: 58    learning rate: 0.001  average loss: 0.00531812   steps without apparent progress: 562
Epoch: 59    learning rate: 0.001  average loss: 0.00507048   steps without apparent progress: 958
Epoch: 60    learning rate: 0.001  average loss: 0.00489779   steps without apparent progress: 987
Saved state to inception_sync
Epoch: 61    learning rate: 0.001  average loss: 0.0047727    steps without apparent progress: 1413
Epoch: 62    learning rate: 0.001  average loss: 0.00467329   steps without apparent progress: 1428
Epoch: 63    learning rate: 0.001  average loss: 0.00458635   steps without apparent progress: 1455
Epoch: 64    learning rate: 0.001  average loss: 0.00451557   steps without apparent progress: 1883
Saved state to inception_sync_
Epoch: 65    learning rate: 0.001  average loss: 0.00445576   steps without apparent progress: 1887
Epoch: 66    learning rate: 0.001  average loss: 0.00440275   steps without apparent progress: 1924
Epoch: 67    learning rate: 0.0001  average loss: 0.00455743   steps without apparent progress: 1785
Epoch: 68    learning rate: 0.0001  average loss: 0.00404007   steps without apparent progress: 656
Epoch: 69    learning rate: 0.0001  average loss: 0.00402818   steps without apparent progress: 1119
Saved state to inception_sync
Epoch: 70    learning rate: 0.0001  average loss: 0.00401996   steps without apparent progress: 1591
Epoch: 71    learning rate: 1e-05  average loss: 0.00401777   steps without apparent progress: 1444
Epoch: 72    learning rate: 1e-05  average loss: 0.0039625    steps without apparent progress: 1906
Epoch: 73    learning rate: 1e-06  average loss: 0.00389764   steps without apparent progress: 0
Saved state to inception_sync_
training num_right: 59963
training num_wrong: 37
training accuracy:  0.999383333333333
testing num_right: 9906
testing num_wrong: 94
testing accuracy:  0.9906
````