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
   "Xamarin"
)

$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent

$source = Join-Path $DlibDotNetRoot src | `
          Join-Path -ChildPath DlibDotNet
dotnet restore ${source}

# build for iOS
# https://github.com/dotnet/msbuild/issues/471#issuecomment-366268743
$customProperties = @{
   "DLIB_NO_GUI_SUPPORT%2cLIB_STATIC" = "Release_Static";
   "DLIB_NO_GUI_SUPPORT"              = "Release_NoGUI";
   ""                                 = "Release_General";
}
foreach ($key in $customProperties.keys)
{
   $customProperty = $key
   $dirname = $customProperties[$key]

   Write-Host "Build ${output}" -ForegroundColor Blue
   dotnet build -c Release -p:CustomDefinition="${customProperty}" ${source} /nowarn:CS1591
   $output = Join-Path $source bin | `
            Join-Path -ChildPath Release
   $dest = Join-Path $source bin | `
         Join-Path -ChildPath ${dirname}
   
   if (Test-path($dest))
   {
      Write-Host "Remove ${dest}" -ForegroundColor Blue
      Remove-Item -Path "${dest}" -Recurse -Force > $null
   }
   Move-Item "${output}" "${dest}"
}

# build for general
dotnet build -c Release ${source} /nowarn:CS1591

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}