# Random Cropper
 
This program is ported by C# from examples\random_cropper_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;RandomCropper_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;RandomCropper_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- &lt;dlib&gt;\examples\faces\training.xml

And extract them and copy to extracted files to &lt;DnnMmodFindCars_dir&gt;.

## 3. Run

````
cd <RandomCropper_dir>
dotnet run --configuration Release <examples\faces\training.xml>

Hit enter to view the next random crop.
````
![RandomCropper](images/1.png "RandomCropper")
![RandomCropper](images/2.png "RandomCropper")
![RandomCropper](images/3.png "RandomCropper")
![RandomCropper](images/4.png "RandomCropper")