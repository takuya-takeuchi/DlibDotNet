# Face Detection
 
This program is ported by C# from examples\face_detection_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;FaceDetection_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;FaceDetection_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <FaceDetection_dir>
dotnet run -c Release <dlib\examples\faces\2008_001322.jpg>

Number of faces detected: 3
hit enter to process next frame
````

![All](images/sample.png "All")