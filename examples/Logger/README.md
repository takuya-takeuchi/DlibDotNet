# Logger2
 
This program is ported by C# from examples\logger_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Logger_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Logger_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Logger_dir>
dotnet run --configuration Release

    1 INFO  [0] example: This is an informational message.
    1 DEBUG [0] example: The integer variable is set to 8
    2 WARN  [0] example: The variable is bigger than 4!  Its value is 8
    3 INFO  [0] example: we are going to sleep for half a second.
  505 INFO  [0] example: we just woke up
  506 INFO  [0] example: program ending
````