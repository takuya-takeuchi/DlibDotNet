$targets = @(
   "CPU",
   "CUDA-92",
   "CUDA-100",
   "CUDA-101",
   "CUDA-102",
   "CUDA-110",
   "CUDA-111",
   "CUDA-112",
   "MKL",
   "UWP"
)

$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent

$source = Join-Path $DlibDotNetRoot src | `
          Join-Path -ChildPath DlibDotNet
dotnet restore ${source}
dotnet build -c Release ${source}

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}

# $nugetPath = Join-Path $PSScriptRoot nuget.exe
# if ($global:IsWindows)
# {
#    Invoke-Expression "${nugetPath} pack nuspec\DlibDotNet.Extensions.nuspec"
# }
# else
# {
#    Invoke-Expression "mono ${nugetPath} pack nuspec\DlibDotNet.Extensions.nuspec"
# }