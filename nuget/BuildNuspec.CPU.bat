dotnet restore ..\src\DlibDotNet
dotnet build -c Release ..\src\DlibDotNet
xcopy ..\src\DlibDotNet\bin\Release\netstandard2.0\DlibDotNet.dll lib\netstandard2.0\DlibDotNet.dll /F /Y
nuget pack DlibDotNet-CPU.nuspec
move DlibDotNet*.nupkg %USERPROFILE%\.nuget