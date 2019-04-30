# Surf
 
This program is ported by C# from examples\surf_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;Surf_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Surf_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <Surf_dir>
dotnet run -c Release Lenna.bmp

number of SURF points found: 425
center of first SURF point: (135.683, 248.309, 2.57774)
pyramid scale:     2.57774330126254
SURF descriptor:
   -0.006838
  -0.0309747
    0.163334
    0.124876
    0.112213
  -0.0021986
    0.254478
    0.127507
   0.0483261
  -0.0243529
    0.143561
   0.0916604
   -0.073069
  -0.0213011
    0.112329
   0.0584928
  -0.0269711
   0.0214358
    0.179874
    0.099137
   0.0761574
-0.000575747
    0.319951
    0.209541
   0.0194923
  -0.0976551
    0.201207
    0.158726
  -0.0527905
  0.00156063
   0.0559578
   0.0270629
   0.0360337
   0.0114302
    0.197503
    0.106416
   0.0225235
    0.102672
    0.265013
    0.197439
   0.0423539
   0.0611076
    0.210113
    0.210479
  -0.0226513
-0.000341211
   0.0414504
   0.0427468
   0.0989939
  -0.0215624
    0.203642
   0.0708569
   0.0642458
 -0.00712146
    0.131311
   0.0581108
   0.0646117
 -0.00132344
    0.207242
    0.185212
  -0.0979869
    0.112837
    0.198532
    0.191969
````

![All](images/image.png "All")