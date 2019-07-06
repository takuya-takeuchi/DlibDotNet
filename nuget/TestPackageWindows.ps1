#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version
)

Set-StrictMode -Version Latest

$OperatingSystem="windows"
$OperatingSystemVersion="10"

# Store current directory
$Current = Get-Location

$BuildTargets = ( "DlibDotNet",
                  # "DlibDotNet.CUDA90",
                  # "DlibDotNet.CUDA91",
                  "DlibDotNet.CUDA92",
                  "DlibDotNet.CUDA100",
                  "DlibDotNet.CUDA101",
                  "DlibDotNet.MKL"
                )

foreach($BuildTarget in $BuildTargets)
{
   $command = ".\\TestPackage.ps1 -Package $BuildTarget -Version $Version -OperatingSystem $OperatingSystem -OperatingSystemVersion $OperatingSystemVersion"
   Invoke-Expression $command

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}