# Video Tracking
 
This program is ported by C# from examples\video_tracking_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;VideoTracking_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;VideoTracking_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Copy these test data from the following path to &lt;DnnFaceRecognition_dir&gt;.

- &lt;dlib&gt;\examples\video_frames

## 3. Run

````
cd <VideoTracking_dir>
dotnet run -c Release <examples\video_frames>

hit enter to process next frame
````

![All](images/anime.gif "All")