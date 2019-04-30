# Kernel Recursive Least Squares
 
This program is ported by C# from examples\krls_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Krls_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Krls_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Krls_dir>
dotnet run --configuration Release

0.239388857641583   0.239361859903616
0.998334166468282   0.998333122231445
-0.189200623826982   -0.189200623495875
-0.191784854932628   -0.197267150553605
````