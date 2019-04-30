# Logger2
 
This program is ported by C# from examples\logger_ex_2.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Logger2_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Logger2_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Logger2_dir>
dotnet run --configuration Release

    1 INFO  [0] example: This is an informational message.
    2 WARN  [0] example: The variable is bigger than 4!  Its value is 8
    3 INFO  [0] example: make two threads
    5 WARN  [0] example.test_class: warning!  someone called warning()!
    6 INFO  [0] example: we are going to sleep for half a second.
    7 INFO  [1] example.thread: entering our thread
    8 WARN  [1] example.test_class: warning!  someone called warning()!
    8 INFO  [2] example.thread: entering our thread
    9 WARN  [2] example.test_class: warning!  someone called warning()!
  209 INFO  [1] example.thread: exiting our thread
  211 INFO  [2] example.thread: exiting our thread
  507 INFO  [0] example: we just woke up
  509 INFO  [0] example: program ending
````