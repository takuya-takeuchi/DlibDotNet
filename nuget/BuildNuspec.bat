dotnet build -c Release ..\src\DlibDotNet

nuget pack DlibDotNet-CPU.nuspec
nuget pack DlibDotNet-CUDA.nuspec

mkdir Release
mkdir Release\DlibDotNet-CPU
xcopy ..\src\DlibDotNet.Native\build_cpu\Release\DlibDotNet.Native.dll Release\DlibDotNet-CPU /y
xcopy ..\src\DlibDotNet.Native.Dnn\build_cpu\Release\DlibDotNet.Native.Dnn.dll Release\DlibDotNet-CPU /y
mkdir Release\DlibDotNet-CUDA
xcopy ..\src\DlibDotNet.Native\build_cuda\Release\DlibDotNet.Native.dll Release\DlibDotNet-CUDA /y
xcopy ..\src\DlibDotNet.Native.Dnn\build_cuda\Release\DlibDotNet.Native.Dnn.dll Release\DlibDotNet-CUDA /y