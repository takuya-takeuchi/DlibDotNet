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
          Join-Path -ChildPath NcnnDotNet
dotnet restore ${source}
# build for iOS
dotnet build -c Release -p:CustomDefinition=LIB_STATIC ${source} /nowarn:CS1591
$output = Join-Path $source bin | `
          Join-Path -ChildPath Release
$dest = Join-Path $source bin | `
        Join-Path -ChildPath Release_Static
if (Test-path($dest))
{
   Remove-Item -Path "${dest}" -Recurse -Force > $null
}
Move-Item "${output}" "${dest}"
# build for general
dotnet build -c Release ${source} /nowarn:CS1591

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}