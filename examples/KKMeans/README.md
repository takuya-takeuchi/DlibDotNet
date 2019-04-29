# Kernel K-Means
 
This program is ported by C# from examples\kkmeans_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;KKMeans_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;KKMeans_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <KKMeans_dir>
dotnet run --configuration Release

0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
0 2 1
num dictionary vectors for center 0: 3
num dictionary vectors for center 1: 8
num dictionary vectors for center 2: 8
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
0 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
1 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
2 
````