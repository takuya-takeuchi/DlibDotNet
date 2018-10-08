# Dnn Mmod Dog Hipsterizer
 
This program is ported by C# from examples\dnn_mmod_dog_hipsterizer.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnMmodDogHipsterizer_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnMmodDogHipsterizer_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- http://dlib.net/files/mmod_dog_hipsterizer.dat.bz2
- &lt;dlib&gt;\examples\faces\dogs.jpg

And extract them and copy to extracted fiels to &lt;DnnMmodDogHipsterizer_dir&gt;.

## 3. Run

````
cd <DnnMmodDogHipsterizer_dir>
dotnet run -c Release mmod_dog_hipsterizer.dat dogs.jpg

Hit enter to process the next image.
````

![Dog](images/1.png "Dog")
![Dog](images/2.png "Dog")
![Dog](images/3.jpg "Dog")
![Dog](images/4.jpg "Dog")