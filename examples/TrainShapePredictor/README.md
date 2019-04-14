# Train Shape Predictor
 
This program is ported by C# from examples\train_shape_predictor_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;TrainShapePredictor_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;TrainShapePredictor_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Run

````
cd <TrainShapePredictor_dir>
dotnet run -c Release <examples\faces>

Fitting trees...
Training complete
mean training error: 4.80209517493103E-05
mean testing error:  0.0886283338531519
````

![DnnMmodFindCars2](images/image.png "DnnMmodFindCars2")