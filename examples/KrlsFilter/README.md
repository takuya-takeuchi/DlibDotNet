# Kernel Recursive Least Squares Filter
 
This program is ported by C# from examples\krls_filter_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;KrlsFilter_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;KrlsFilter_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <KrlsFilter_dir>
dotnet run --configuration Release

prediction error:                   0.00735201508462676
noise:                              0.0821628333482474
ratio of noise to prediction error: 11.1755528793802
````