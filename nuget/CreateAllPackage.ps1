$targets = @(
   "CPU",
   "CUDA-92",
   "CUDA-100",
   "CUDA-101",
   "CUDA-102",
   "CUDA-110",
   "CUDA-111",
   "MKL",
   "UWP"
)

$ScriptPath = $PSScriptRoot
$OpenJijDotNetRoot = Split-Path $ScriptPath -Parent

$source = Join-Path $OpenJijDotNetRoot src | `
          Join-Path -ChildPath OpenJijDotNet
dotnet restore ${source}
dotnet build -c Release ${source}

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}

nuget pack nuspec\DlibDotNet.Extensions.nuspec