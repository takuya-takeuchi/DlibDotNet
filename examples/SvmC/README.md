# C parametrization of the SVM
  
This program is ported by C# from examples\svm_c_ex.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;SvmC_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;SvmC_dir&gt;\bin\Release\netcoreapp2.0. 
 
**NOTE**   
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA. 
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.   
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS). 
 
And extract them and copy to extracted files to &lt;SvmC_dir&gt;. 
 
## 2. Run 
 
The following result is example. 
 
```` 
cd <SvmC_dir> 
dotnet run --configuration Release

doing cross validation
gamma: 1E-05    C: 1     cross validation accuracy: 0 1
gamma: 1E-05    C: 5     cross validation accuracy: 0 1
gamma: 1E-05    C: 25     cross validation accuracy: 0 1
gamma: 1E-05    C: 125     cross validation accuracy: 0 1
gamma: 1E-05    C: 625     cross validation accuracy: 0 1
gamma: 1E-05    C: 3125     cross validation accuracy: 0 1
gamma: 1E-05    C: 15625     cross validation accuracy: 0 1
gamma: 1E-05    C: 78125     cross validation accuracy: 0 1
gamma: 5E-05    C: 1     cross validation accuracy: 0 1
gamma: 5E-05    C: 5     cross validation accuracy: 0 1
gamma: 5E-05    C: 25     cross validation accuracy: 0 1
gamma: 5E-05    C: 125     cross validation accuracy: 0 1
gamma: 5E-05    C: 625     cross validation accuracy: 0 1
gamma: 5E-05    C: 3125     cross validation accuracy: 0 1
gamma: 5E-05    C: 15625     cross validation accuracy: 0 1
gamma: 5E-05    C: 78125     cross validation accuracy: 0 1
gamma: 0.00025    C: 1     cross validation accuracy: 0 1
gamma: 0.00025    C: 5     cross validation accuracy: 0 1
gamma: 0.00025    C: 25     cross validation accuracy: 0 1
gamma: 0.00025    C: 125     cross validation accuracy: 0 1
gamma: 0.00025    C: 625     cross validation accuracy: 0 1
gamma: 0.00025    C: 3125     cross validation accuracy: 0 1
gamma: 0.00025    C: 15625     cross validation accuracy: 0 1
gamma: 0.00025    C: 78125     cross validation accuracy: 0.980952 0.978708
gamma: 0.00125    C: 1     cross validation accuracy: 0 1
gamma: 0.00125    C: 5     cross validation accuracy: 0 1
gamma: 0.00125    C: 25     cross validation accuracy: 0 1
gamma: 0.00125    C: 125     cross validation accuracy: 0 1
gamma: 0.00125    C: 625     cross validation accuracy: 0 1
gamma: 0.00125    C: 3125     cross validation accuracy: 0.971429 0.983113
gamma: 0.00125    C: 15625     cross validation accuracy: 0.968254 0.991924
gamma: 0.00125    C: 78125     cross validation accuracy: 0.971429 0.989721
gamma: 0.00625    C: 1     cross validation accuracy: 0 1
gamma: 0.00625    C: 5     cross validation accuracy: 0 1
gamma: 0.00625    C: 25     cross validation accuracy: 0 1
gamma: 0.00625    C: 125     cross validation accuracy: 0.968254 0.983113
gamma: 0.00625    C: 625     cross validation accuracy: 0.971429 0.992658
gamma: 0.00625    C: 3125     cross validation accuracy: 0.974603 0.990455
gamma: 0.00625    C: 15625     cross validation accuracy: 0.977778 0.992658
gamma: 0.00625    C: 78125     cross validation accuracy: 0.965079 0.992658
gamma: 0.03125    C: 1     cross validation accuracy: 0 1
gamma: 0.03125    C: 5     cross validation accuracy: 0.955556 0.988987
gamma: 0.03125    C: 25     cross validation accuracy: 0.968254 0.991924
gamma: 0.03125    C: 125     cross validation accuracy: 0.971429 0.991924
gamma: 0.03125    C: 625     cross validation accuracy: 0.977778 0.993392
gamma: 0.03125    C: 3125     cross validation accuracy: 0.968254 0.993392
gamma: 0.03125    C: 15625     cross validation accuracy: 0.965079 0.991924
gamma: 0.03125    C: 78125     cross validation accuracy: 0.971429 0.991924
gamma: 0.15625    C: 1     cross validation accuracy:  0.95873 0.994126
gamma: 0.15625    C: 5     cross validation accuracy: 0.974603 0.993392
gamma: 0.15625    C: 125     cross validation accuracy: 0.961905 0.993392
gamma: 0.15625    C: 625     cross validation accuracy:  0.95873 0.992658
gamma: 0.15625    C: 3125     cross validation accuracy: 0.974603 0.995595
gamma: 0.15625    C: 15625     cross validation accuracy: 0.987302 0.998532
gamma: 0.15625    C: 78125     cross validation accuracy: 0.984127  0.99486
gamma: 0.78125    C: 1     cross validation accuracy: 0.942857 0.996329
gamma: 0.78125    C: 5     cross validation accuracy: 0.971429 0.995595
gamma: 0.78125    C: 25     cross validation accuracy: 0.968254 0.997063
gamma: 0.78125    C: 125     cross validation accuracy: 0.987302 0.997797
gamma: 0.78125    C: 625     cross validation accuracy: 0.987302 0.997797
gamma: 0.78125    C: 3125     cross validation accuracy: 0.987302 0.997063
gamma: 0.78125    C: 15625     cross validation accuracy: 0.974603 0.992658
gamma: 0.78125    C: 78125     cross validation accuracy: 0.974603 0.993392

This is a +1 class example, the classifier output is 2.8112827860005
This is a +1 class example, the classifier output is 0.0862774820050145
This is a -1 class example, the classifier output is -4.26559989399015
This is a -1 class example, the classifier output is -2.06901001043894

This +1 class example should have high probability.  Its probability is: 0.999999377375693
This +1 class example should have high probability.  Its probability is: 0.585090390614627
This -1 class example should have low probability.  Its probability is: 2.99871163018877E-10
This -1 class example should have low probability.  Its probability is: 2.28555042556275E-05

cross validation accuracy with only 10 support vectors: 0.974603 0.993392
cross validation accuracy with all the original support vectors: 0.974603 0.993392
````