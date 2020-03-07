dotnet restore ..\src\DlibDotNet
dotnet build -c Release ..\src\DlibDotNet

pwsh CreatePackage.ps1 CPU
pwsh CreatePackage.ps1 CUDA-92
pwsh CreatePackage.ps1 CUDA-100
pwsh CreatePackage.ps1 CUDA-101
pwsh CreatePackage.ps1 MKL
pwsh CreatePackage.ps1 UWP