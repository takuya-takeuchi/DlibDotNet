# Pipe
 
This program is ported by C# from examples\pipe_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Pipe_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;Pipe_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Pipe_dir>
dotnet run --configuration Release

    4 INFO  [0] pipe_example: Add job 0 to pipe
    5 INFO  [0] pipe_example: Add job 1 to pipe
    5 INFO  [0] pipe_example: Add job 2 to pipe
    6 INFO  [1] pipe_example: got job 1
    6 INFO  [2] pipe_example: got job 0
    8 INFO  [0] pipe_example: Add job 3 to pipe
    8 INFO  [3] pipe_example: got job 2
    8 INFO  [0] pipe_example: Add job 4 to pipe
    9 INFO  [0] pipe_example: Add job 5 to pipe
    9 INFO  [0] pipe_example: Add job 6 to pipe
   10 INFO  [0] pipe_example: Add job 7 to pipe
  108 INFO  [1] pipe_example: got job 3
  108 INFO  [0] pipe_example: Add job 8 to pipe
  109 INFO  [2] pipe_example: got job 4
  109 INFO  [3] pipe_example: got job 5
  110 INFO  [0] pipe_example: Add job 9 to pipe
  111 INFO  [0] pipe_example: Add job 10 to pipe
  209 INFO  [1] pipe_example: got job 6
  211 INFO  [0] pipe_example: Add job 11 to pipe
  212 INFO  [2] pipe_example: got job 7
  213 INFO  [3] pipe_example: got job 8
  214 INFO  [0] pipe_example: Add job 12 to pipe
  215 INFO  [0] pipe_example: Add job 13 to pipe
  311 INFO  [1] pipe_example: got job 9
  312 INFO  [0] pipe_example: Add job 14 to pipe
  314 INFO  [2] pipe_example: got job 10
  314 INFO  [0] pipe_example: main ending
  315 INFO  [3] pipe_example: got job 11
  315 INFO  [0] pipe_example: destructing pipe object: wait for job_pipe to be empty
  414 INFO  [1] pipe_example: got job 12
  415 INFO  [2] pipe_example: got job 13
  417 INFO  [3] pipe_example: got job 14
  417 INFO  [0] pipe_example: destructing pipe object: job_pipe is empty
  516 INFO  [1] pipe_example: thread ending
  517 INFO  [2] pipe_example: thread ending
  517 INFO  [3] pipe_example: thread ending
  518 INFO  [0] pipe_example: destructing pipe object: all threads have ended
````