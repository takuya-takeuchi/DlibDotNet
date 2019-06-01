# OpenCVSharp.Mat to Dlib.Matrix

## How to use?

## 1. Build

1. Open command prompt and change to &lt;MatToMatrix_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;MatToMatrix_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Deploy OpenCVSharp3 binaries

You copy OpenCVSharp3 binaries, OpenCvSharp.dll, OpenCvSharpExtern.dll and opencv_ffmpegXXX_64.dll from &lt;%USERPROFILE%\\.nuget\packages\opencvsharp3-anycpu&gt;

## 3. Run

````
cd <MatToMatrix_dir>
dotnet run -c Release
````