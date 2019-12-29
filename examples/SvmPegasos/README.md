# Implementation of the pegasos algorithm of the SVM
  
This program is ported by C# from examples\svm_pegasos_ex.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;SvmPegasos_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;SvmPegasos_dir&gt;\bin\Release\netcoreapp2.0. 
 
**NOTE**   
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA. 
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.   
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS). 
 
And extract them and copy to extracted files to &lt;SvmPegasos_dir&gt;. 
 
## 2. Run 
 
The following result is example. 
 
```` 
cd <SvmPegasos_dir> 
dotnet run --configuration Release

This is a +1 example, its SVM output is: 31.0435355681312
This is a -1 example, its SVM output is: -30.6126878916667
This is a -1 example, its SVM output is: -16.3176675461284
cross validation: 0.980847 0.997629
batch_trainer(): Percent complete: 100
    Num sv: 10
    bias:   0.538718
This is a +1 example, its SVM output is: 10.7098714016529
This is a -1 example, its SVM output is: -10.6643645338158
This is a -1 example, its SVM output is: -4.9650171510977
````