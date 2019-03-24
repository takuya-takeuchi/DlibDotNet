# Dnn Mmod Train Find Cars
 
This program is ported by C# from examples\dnn_mmod_train_find_cars_ex.cpp.

#### :warning: Warning

This program requires high performance gpu like GeForce GTX 1080 Ti and large memory.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnMmodTrainFindCars_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnMmodTrainFindCars_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

And extract them and copy to extracted fiels to &lt;DnnMmodTrainFindCars_dir&gt;.

## 2. Download demo data

Download test data from the following urls.

- http://dlib.net/files/data/dlib_rear_end_vehicles_v1.tar

And extract them and copy to extracted folder **'dlib_rear_end_vehicles'** to &lt;DnnMmodTrainFindCars_dir&gt;.

## 3. Run

````
cd <DnnMmodTrainFindCars_dir>
dotnet run -c Release dlib_rear_end_vehicles
````