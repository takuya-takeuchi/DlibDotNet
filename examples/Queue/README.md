# Queue
 
This program is ported by C# from examples\queue_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Queue_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Queue_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Queue_dir>
dotnet run --configuration Release

The contents of the queue are:
26 12 70 111 93 117 228 216 173 103 101 213 245 25 219 200 43 124 52 59

Now we sort the queue and its contents are:
12 25 26 43 52 59 70 93 101 103 111 117 124 173 200 213 216 219 228 245

Now we remove the numbers from the queue:
12 25 26 43 52 59 70 93 101 103 111 117 124 173 200 213 216 219 228 245
````