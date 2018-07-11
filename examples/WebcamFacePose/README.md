# Webcam Face Pose
 
This program is ported by C# from examples\webcam_face_pose_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;WebcamFacePose_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;WebcamFacePose_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Deploy OpenCVSharp3 binaries

You copy OpenCVSharp3 binaries, OpenCvSharp.dll, OpenCvSharpExtern.dll and opencv_ffmpegXXX_64.dll from &lt;%USERPROFILE%\\.nuget\packages\opencvsharp3-anycpu&gt;

## 3. Download demo data

Download test data from the following urls.

- http://dlib.net/files/shape_predictor_68_face_landmarks.dat.bz2

And extract them and copy to extracted fiels to &lt;WebcamFacePose_dir&gt;.

## 4. Run

````
cd <WebcamFacePose_dir>
dotnet run -c Release
````

![All](images/video.gif "All")

**NOTE**  
The above image is from https://upload.wikimedia.org/wikipedia/commons/a/a5/20090124_WeeklyAddress.ogv  
Actually, sample source uses WebCam attached on computer.  
But this README uses video file because DlibDotNet owner hesitates to use own face to demonstrate.
