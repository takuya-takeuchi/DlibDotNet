# Dnn Mmod Find Cars2
 
This program is ported by C# from examples\dnn_mmod_find_cars2_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnMmodFindCars2_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnMmodFindCars2_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- http://dlib.net/files/mmod_front_and_rear_end_vehicle_detector.dat.bz2
- &lt;dlib&gt;\examples\mmod_cars_test_image2.jpg

And extract them and copy to extracted files to &lt;DnnMmodFindCars2_dir&gt;.

## 3. Run

````
cd <DnnMmodFindCars2_dir>
dotnet run -c Release

Hit enter to end program
````

![DnnMmodFindCars2](images/image.png "DnnMmodFindCars2")