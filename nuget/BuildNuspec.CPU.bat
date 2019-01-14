dotnet restore ..\src\DlibDotNet
dotnet build -c Release ..\src\DlibDotNet
nuget pack DlibDotNet.CPU.nuspec