# DNN Introduction
 
This program is ported by C# from examples\dnn_introduction_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnIntroduction_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;DnnIntroduction_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- http://yann.lecun.com/exdb/mnist/
  - train-images-idx3-ubyte.gz
  - train-labels-idx1-ubyte.gz
  - t10k-images-idx3-ubyte.gz 
  - t10k-labels-idx1-ubyte.gz

And extract them and copy to extracted files to &lt;DnnIntroduction_dir&gt;.

## 3. Run

````
cd <DnnIntroduction_dir>
dotnet run -c Release .

Epoch: 1     learning rate: 0.01  average loss: 0.294934     steps without apparent progress: 8
Epoch: 2     learning rate: 0.01  average loss: 0.082819     steps without apparent progress: 24
Epoch: 3     learning rate: 0.01  average loss: 0.0585108    steps without apparent progress: 5
Epoch: 4     learning rate: 0.01  average loss: 0.0469297    steps without apparent progress: 9
Epoch: 5     learning rate: 0.01  average loss: 0.0394108    steps without apparent progress: 374
Epoch: 6     learning rate: 0.01  average loss: 0.033831     steps without apparent progress: 350
Epoch: 7     learning rate: 0.01  average loss: 0.0297569    steps without apparent progress: 351
Epoch: 8     learning rate: 0.01  average loss: 0.0270409    steps without apparent progress: 356
Epoch: 9     learning rate: 0.01  average loss: 0.0239106    steps without apparent progress: 359
Epoch: 10    learning rate: 0.01  average loss: 0.0210753    steps without apparent progress: 372
Epoch: 11    learning rate: 0.01  average loss: 0.0189042    steps without apparent progress: 520
Epoch: 12    learning rate: 0.01  average loss: 0.0170421    steps without apparent progress: 374
Epoch: 13    learning rate: 0.01  average loss: 0.0155647    steps without apparent progress: 12
Epoch: 14    learning rate: 0.01  average loss: 0.0142178    steps without apparent progress: 365
Epoch: 15    learning rate: 0.01  average loss: 0.0130426    steps without apparent progress: 0
Epoch: 16    learning rate: 0.01  average loss: 0.0120666    steps without apparent progress: 373
Saved state to mnist_sync
Epoch: 17    learning rate: 0.01  average loss: 0.0109168    steps without apparent progress: 520
Epoch: 18    learning rate: 0.01  average loss: 0.0100672    steps without apparent progress: 378
Epoch: 19    learning rate: 0.01  average loss: 0.00924609   steps without apparent progress: 382
Epoch: 20    learning rate: 0.01  average loss: 0.00861545   steps without apparent progress: 558
Epoch: 21    learning rate: 0.01  average loss: 0.00798486   steps without apparent progress: 549
Epoch: 22    learning rate: 0.01  average loss: 0.00751108   steps without apparent progress: 989
Epoch: 23    learning rate: 0.01  average loss: 0.0070122    steps without apparent progress: 845
Epoch: 24    learning rate: 0.01  average loss: 0.0066557    steps without apparent progress: 545
Epoch: 25    learning rate: 0.01  average loss: 0.00634419   steps without apparent progress: 988
Epoch: 26    learning rate: 0.01  average loss: 0.00609003   steps without apparent progress: 983
Epoch: 27    learning rate: 0.01  average loss: 0.00588641   steps without apparent progress: 999
Epoch: 28    learning rate: 0.01  average loss: 0.00567884   steps without apparent progress: 1336
Epoch: 29    learning rate: 0.01  average loss: 0.00549556   steps without apparent progress: 1337
Epoch: 30    learning rate: 0.01  average loss: 0.00541955   steps without apparent progress: 1469
Epoch: 31    learning rate: 0.01  average loss: 0.00520531   steps without apparent progress: 1450
Epoch: 32    learning rate: 0.01  average loss: 0.00516312   steps without apparent progress: 1466
Epoch: 33    learning rate: 0.01  average loss: 0.00491669   steps without apparent progress: 1470
Epoch: 34    learning rate: 0.01  average loss: 0.00490416   steps without apparent progress: 1451
Saved state to mnist_sync_
Epoch: 35    learning rate: 0.01  average loss: 0.00471576   steps without apparent progress: 1467
Epoch: 36    learning rate: 0.01  average loss: 0.00460263   steps without apparent progress: 1386
Epoch: 37    learning rate: 0.01  average loss: 0.00453145   steps without apparent progress: 1479
Epoch: 38    learning rate: 0.01  average loss: 0.00445539   steps without apparent progress: 1498
Epoch: 39    learning rate: 0.01  average loss: 0.00428864   steps without apparent progress: 1461
Epoch: 40    learning rate: 0.01  average loss: 0.00427643   steps without apparent progress: 1486
Epoch: 41    learning rate: 0.01  average loss: 0.00419368   steps without apparent progress: 1505
Epoch: 42    learning rate: 0.01  average loss: 0.00410962   steps without apparent progress: 1931
Epoch: 43    learning rate: 0.01  average loss: 0.00408031   steps without apparent progress: 1487
Epoch: 44    learning rate: 0.01  average loss: 0.00400675   steps without apparent progress: 1939
Epoch: 45    learning rate: 0.001  average loss: 0.00468174   steps without apparent progress: 1774
Epoch: 46    learning rate: 0.0001  average loss: 0.00376167   steps without apparent progress: 547
Epoch: 47    learning rate: 0.0001  average loss: 0.00342293   steps without apparent progress: 954
Epoch: 48    learning rate: 0.0001  average loss: 0.00336842   steps without apparent progress: 1076
Epoch: 49    learning rate: 0.0001  average loss: 0.00334213   steps without apparent progress: 1534
Epoch: 50    learning rate: 0.0001  average loss: 0.00332123   steps without apparent progress: 1996
Epoch: 51    learning rate: 1e-05  average loss: 0.0032704    steps without apparent progress: 1815
Epoch: 52    learning rate: 1e-05  average loss: 0.0032564    steps without apparent progress: 1981
Epoch: 53    learning rate: 1e-06  average loss: 0.00304015   steps without apparent progress: 0
Saved state to mnist_sync
training num_right: 59976
training num_wrong: 24
training accuracy:  0.9996
testing num_right: 9913
testing num_wrong: 87
testing accuracy:  0.9913
````