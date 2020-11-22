dotnet restore ..\src\DlibDotNet
dotnet build -c Release ..\src\DlibDotNet

pwsh CreatePackage.ps1 CPU
pwsh CreatePackage.ps1 CUDA-92
pwsh CreatePackage.ps1 CUDA-100
pwsh CreatePackage.ps1 CUDA-101
pwsh CreatePackage.ps1 CUDA-102
pwsh CreatePackage.ps1 CUDA-110
pwsh CreatePackage.ps1 CUDA-111
pwsh CreatePackage.ps1 MKL
pwsh CreatePackage.ps1 UWP

nuget pack nuspec\DlibDotNet.Extensions.nuspec