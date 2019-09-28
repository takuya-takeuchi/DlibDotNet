# Dnn Metric Learning
 
This program is ported by C# from examples\dnn_metric_learning_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnMetricLearning_dir&gt;
2. Type the following command
````
dotnet build -c Release
````
3. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;DnnMetricLearning_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

And extract them and copy to extracted files to &lt;DnnMetricLearning_dir&gt;.

## 2. Run

````
cd <DnnMetricLearning_dir>
dotnet run -c Release

done training
label: 1         -0.151458 -0.0766358
label: 1         -0.147003 -0.0889591
label: 2        0.0614506  0.529338
label: 2        0.136568 0.497333
label: 3          0.561938 0.00232332
label: 3          0.624519 -0.0519932
label: 4        -0.538596 -0.606784
label: 4        -0.429636 -0.711575
num_right: 28
num_wrong: 0
````