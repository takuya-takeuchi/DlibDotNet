# SVM
  
This program is ported by C# from examples\svm_c_ex.cpp. 
 
## How to use? 
 
## 1. Build 
 
1. Open command prompt and change to &lt;Svm_dir&gt; 
1. Type the following command 
```` 
dotnet build -c Release 
```` 
2. Copy ***DlibDotNet.dll***, ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** to output directory; &lt;Svm_dir&gt;\bin\Release\netcoreapp2.0. 
 
**NOTE**   
- You should build ***DlibDotNetNative.dll*** and ***DlibDotNetNativeDnn.dll*** with CUDA. 
- If you want to run at Linux and MacOS, you should build the **DlibDotNet** at first.   
Please refer the [Tutorial for Linux](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-Linux) or [Tutorial for MacOS](https://github.com/takuya-takeuchi/DlibDotNet/wiki/Tutorial-for-MacOS). 
 
And extract them and copy to extracted files to &lt;Svm_dir&gt;. 
 
## 2. Run 
 
The following result is example. 
 
```` 
cd <Svm_dir> 
dotnet run --configuration Release

doing cross validation
gamma: 1E-05    nu: 1E-05     cross validation accuracy: 0.857143 0.309104
gamma: 1E-05    nu: 5E-05     cross validation accuracy: 0.857143 0.309104
gamma: 1E-05    nu: 0.00025     cross validation accuracy: 0.857143 0.309104
gamma: 1E-05    nu: 0.00125     cross validation accuracy: 0.857143 0.309104
gamma: 1E-05    nu: 0.00625     cross validation accuracy: 0.993651 0.348018
gamma: 1E-05    nu: 0.03125     cross validation accuracy: 0.447619 0.446402
gamma: 1E-05    nu: 0.15625     cross validation accuracy: 0.774603  0.28928
gamma: 5E-05    nu: 1E-05     cross validation accuracy: 0.857143 0.309104
gamma: 5E-05    nu: 5E-05     cross validation accuracy: 0.857143 0.309104
gamma: 5E-05    nu: 0.00025     cross validation accuracy: 0.857143 0.309104
gamma: 5E-05    nu: 0.00125     cross validation accuracy: 0.857143 0.309104
gamma: 5E-05    nu: 0.00625     cross validation accuracy: 0.993651 0.346549
gamma: 5E-05    nu: 0.03125     cross validation accuracy: 0.450794 0.446402
gamma: 5E-05    nu: 0.15625     cross validation accuracy: 0.812698 0.281204
gamma: 0.00025    nu: 1E-05     cross validation accuracy: 0.857143 0.309104
gamma: 0.00025    nu: 5E-05     cross validation accuracy: 0.857143 0.309104
gamma: 0.00025    nu: 0.00025     cross validation accuracy: 0.857143 0.309104
gamma: 0.00025    nu: 0.00125     cross validation accuracy: 0.580952 0.422173
gamma: 0.00025    nu: 0.00625     cross validation accuracy: 0.996825 0.422907
gamma: 0.00025    nu: 0.03125     cross validation accuracy: 0.711111  0.45815
gamma: 0.00025    nu: 0.15625     cross validation accuracy: 0.796825 0.273862
gamma: 0.00125    nu: 1E-05     cross validation accuracy: 0.857143 0.309104
gamma: 0.00125    nu: 5E-05     cross validation accuracy: 0.857143 0.309104
gamma: 0.00125    nu: 0.00025     cross validation accuracy: 0.660317 0.364905
gamma: 0.00125    nu: 0.00125     cross validation accuracy: 0.463492 0.547724
gamma: 0.00125    nu: 0.00625     cross validation accuracy: 0.777778 0.349486
gamma: 0.00125    nu: 0.03125     cross validation accuracy: 0.946032 0.627019
gamma: 0.00125    nu: 0.15625     cross validation accuracy: 0.949206 0.772394
gamma: 0.00625    nu: 1E-05     cross validation accuracy: 0.857143 0.309104
gamma: 0.00625    nu: 5E-05     cross validation accuracy: 0.704762 0.461087
gamma: 0.00625    nu: 0.00025     cross validation accuracy: 0.584127 0.683554
gamma: 0.00625    nu: 0.00125     cross validation accuracy: 0.584127 0.683554
gamma: 0.00625    nu: 0.00625     cross validation accuracy: 0.936508 0.790015
gamma: 0.00625    nu: 0.03125     cross validation accuracy: 0.942857 0.934655
gamma: 0.00625    nu: 0.15625     cross validation accuracy: 0.955556 0.978708
gamma: 0.03125    nu: 1E-05     cross validation accuracy: 0.844444 0.419971
gamma: 0.03125    nu: 5E-05     cross validation accuracy: 0.714286 0.633627
gamma: 0.03125    nu: 0.00025     cross validation accuracy: 0.714286 0.633627
gamma: 0.03125    nu: 0.00125     cross validation accuracy: 0.898413 0.903084
gamma: 0.03125    nu: 0.00625     cross validation accuracy:  0.91746 0.950073
gamma: 0.03125    nu: 0.03125     cross validation accuracy: 0.961905 0.988987
gamma: 0.03125    nu: 0.15625     cross validation accuracy: 0.980952 0.991924
gamma: 0.15625    nu: 1E-05     cross validation accuracy: 0.879365 0.765051
gamma: 0.15625    nu: 5E-05     cross validation accuracy: 0.768254 0.842144
gamma: 0.15625    nu: 0.00025     cross validation accuracy: 0.390476 0.377386
gamma: 0.15625    nu: 0.00125     cross validation accuracy: 0.733333 0.713656
gamma: 0.15625    nu: 0.00625     cross validation accuracy: 0.974603 0.995595
gamma: 0.15625    nu: 0.03125     cross validation accuracy:  0.95873 0.991189
gamma: 0.15625    nu: 0.15625     cross validation accuracy: 0.977778 0.994126
gamma: 0.78125    nu: 1E-05     cross validation accuracy: 0.844444 0.562408
gamma: 0.78125    nu: 5E-05     cross validation accuracy: 0.733333 0.956681
gamma: 0.78125    nu: 0.00025     cross validation accuracy: 0.593651 0.640969
gamma: 0.78125    nu: 0.00125     cross validation accuracy: 0.673016 0.667401
gamma: 0.78125    nu: 0.00625     cross validation accuracy: 0.987302 0.998532
gamma: 0.78125    nu: 0.03125     cross validation accuracy: 0.984127 0.996329
gamma: 0.78125    nu: 0.15625     cross validation accuracy: 0.933333 0.998532

This is a +1 class example, the classifier output is 2.24853853116537
This is a +1 class example, the classifier output is 0.103011761915319
This is a -1 class example, the classifier output is -3.3244730232147
This is a -1 class example, the classifier output is -1.59309791671972

This +1 class example should have high probability.  Its probability is: 0.999997818043895
This +1 class example should have high probability.  Its probability is: 0.623394484264643
This -1 class example should have low probability.  Its probability is: 3.34830112030211E-09
This -1 class example should have low probability.  Its probability is: 8.25165672051498E-05

cross validation accuracy with only 10 support vectors: 0.977778 0.993392
cross validation accuracy with all the original support vectors: 0.977778 0.994126
````