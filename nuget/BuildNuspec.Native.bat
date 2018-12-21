dotnet build -c Release ..\src\DlibDotNet -r win-x64
xcopy ..\src\DlibDotNet.Native\build_cpu\Release\DlibDotNet.Native.dll runtimes\win-x64\native\ /y
xcopy ..\src\DlibDotNet.Native.Dnn\build_cpu\Release\DlibDotNet.Native.Dnn.dll runtimes\win-x64\native\ /y
nuget pack DlibDotNet.Native.nuspec
move DlibDotNet.Native*.nupkg %USERPROFILE%\.nuget