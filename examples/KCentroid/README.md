# K-Centroid
 
This program is ported by C# from examples\kcentroid_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;KCentroid_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;KCentroid_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <KCentroid_dir>
dotnet run --configuration Release

Points that are on the sinc function:
   0.86991349932841
   0.86991349932841
   0.873408103973538
   0.872806915227224
   0.870431744300751
   0.86991349932841
   0.872806915227224

Points that are NOT on the sinc function:
   1.06365748560979 is 119.649876918704 standard deviations from sinc.
   1.02211934386878 is 93.8105852058224 standard deviations from sinc.
   0.9213821589881 is 31.1458294568676 standard deviations from sinc.
   0.918438535931847 is 29.3147139650501 standard deviations from sinc.
   0.931427955579286 is 37.3949358859979 standard deviations from sinc.
   0.898018426328152 is 16.6121435865133 standard deviations from sinc.
   0.914425356366181 is 26.8182682130961 standard deviations from sinc.

mean: 0.871313453816281
standard deviation: 0.00160755728920808
````