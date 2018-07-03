# DNN Semantic Segmentation
 
This program is ported by C# from examples\dnn_semantic_segmentation_ex.cpp.

## How to use?

## 1. Build

1. Open command prompt and change to &lt;DnnSemanticSegmentation_dir&gt;
1. Type the following command
````
dotnet build -c Release
````
2. Copy ***DlibDotNet.dll***, ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** to output directory; &lt;DnnSemanticSegmentation_dir&gt;\bin\Release\netcoreapp2.0.

**NOTE**  
- You should build ***DlibDotNet.Native.dll*** and ***DlibDotNet.Native.Dnn.dll*** with CUDA.
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.  
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS).

## 2. Download demo data

Download test data from the following urls.

- http://dlib.net/files/semantic_segmentation_voc2012net.dnn
- &lt;dlib&gt;\examples\faces\*.jpg

And extract them and copy to extracted fiels to &lt;DnnSemanticSegmentation_dir&gt;.

## 3. Run

````
cd <DnnSemanticSegmentation_dir>
dotnet run --configuration Release dlib\examples\faces
````

![All](images/sample.png "All")