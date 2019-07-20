#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version,

      [Parameter(
      Mandatory=$True,
      Position = 2
      )][string]
      $Thumbprint,
      
      [Parameter(
      Mandatory=$True,
      Position = 3
      )][string]
      $CertificateKeyFile
)

Set-StrictMode -Version Latest

# import class and function
$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$NugetPath = Join-Path $DlibDotNetRoot "nuget"


$OperatingSystem="windows"
$OperatingSystemVersion="10"
$TargetTestProject = "DlibDotNet.UWP.Tests.csproj"
$MSBuildDir = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\amd64"
$AppcertDir = "C:\Program Files (x86)\Windows Kits\10\App Certification Kit"
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{ Target = "cpu";  Architecture = "x64"; CUDA = 0;   Package = "DlibDotNet.UWP" }

function Invoke-Admin()
{
   param ( [string]$program = $(throw "Please specify a program" ),
           [string]$argumentString = "",
           [switch]$waitForExit )

   $psi = new-object "Diagnostics.ProcessStartInfo"
   $psi.FileName = $program
   $psi.Arguments = $argumentString
   $psi.Verb = "runas"
   $proc = [Diagnostics.Process]::Start($psi)

   if ( $waitForExit )
   {
       $proc.WaitForExit();
   }

   return $proc.ExitCode
}

function Update-Version([string]$TestProjectDir, [string]$Version)
{
   $projectFile = Join-Path $TestProjectDir $TargetTestProject

   # revert
   git checkout $projectFile

   $xml = [XML](Get-Content $projectFile)

   $ns = New-Object Xml.XmlNamespaceManager $xml.NameTable
   $ns.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003")

   $nodes = $xml.SelectNodes("//msb:ItemGroup/msb:PackageReference[contains(./@Include , 'DlibDotNet.UWP')]", $ns)

   foreach($node in $nodes)
   {
      $node.Version = $Version
   }

   $xml.Save($projectFile)
}

function Build([string]$Architecture, [string]$OutputDir, [string]$Thumbprint, [string]$CertificateKeyFile)
{
   if (Test-Path $OutputDir)
   {
      Remove-Item -Path "$OutputDir" -Recurse -Force
   }
   
   if (!(Test-Path ${CertificateKeyFile}))
   {
      Write-Host "Error: ${CertificateKeyFile} does not exist" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   $msbuild = Join-Path $MSBuildDir msbuild.exe
   $command = """${msbuild}"""
   $command += " ${TargetTestProject}"
   $command += " /t:restore"
   $command += " /t:Rebuild"
   $command += " /p:RestoreAdditionalProjectSources=${NugetPath}"
   $command += " /p:RestoreNoCache=true"
   $command += " /p:Configuration=Release"
   $command += " /p:Platform=""${Architecture}"""
   $command += " /p:OutDir=${OutputDir}"
   $command += " /p:AppxBundle=Always"
   $command += " /p:AppxBundlePlatforms=""${Architecture}"""
   $command += " /p:AppxPackageSigningEnabled=true"
   $command += " /p:PackageCertificateThumbprint=${Thumbprint}"
   $command += " /p:PackageCertificateKeyFile=""${CertificateKeyFile}"""

   Invoke-Expression "cmd.exe /c $command"
   if ($lastexitcode -ne 0)
   {
     Write-Host "Error: Failed Build" -ForegroundColor Red
     Set-Location -Path $Current
     exit -1
   }
}

function AppCert([string]$Architecture, [string]$OutputDir)
{
   $current = Get-Location
   $appx  = Join-Path $OutputDir DlibDotNet.UWP.Tests |
            Join-Path -ChildPath DlibDotNet.UWP.Tests_1.0.0.0_${Architecture}.appx
   $report   = Join-Path $OutputDir DlibDotNet.UWP.Tests |
               Join-Path -ChildPath AppCertReport.xml

   if (Test-Path $report)
   {
      Remove-Item $report
   }

   $exitCode = Invoke-Admin "appcert.exe" "reset" -waitForExit
   if ($exitCode -ne 0)
   {
      Write-Host "Error: Failed AppCert reset" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   $command = "test"
   $command += " -appxpackagepath ""${appx}"""
   $command += " -apptype WindowsStoreApp"
   $command += " -reportoutputpath ""${report}"""
   $exitCode = Invoke-Admin "appcert.exe" "$command" -waitForExit
   if ($lastexitcode -ne 0)
   {
      Write-Host "Error: Failed AppCert test" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   # $command = "finalizereport"
   # $command += " -reportfilepath ""${report}"""
   # $exitCode = Invoke-Admin "appcert.exe" "$command" -waitForExit
   # if ($exitCode -ne 0)
   # {
   #    Write-Host "Error: Failed AppCert finalizereport" -ForegroundColor Red
   #    Set-Location -Path $Current
   #    exit -1
   # }

   return $report
}

function Check-Report([string]$Report)
{
   if (!(Test-Path ${Report}))
   {
      Write-Host "Error: ${Report} does not exist" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   $notPassCount = 0
   $xml = [XML](Get-Content ${Report})
   foreach($test in $xml.REPORT.REQUIREMENTS.REQUIREMENT.TEST)
   {
      Write-Host $test.NAME
      if ($test.RESULT.InnerText -eq "PASS")
      {
         Write-Host "Result:"$test.RESULT.InnerText -ForegroundColor Green
      }
      else
      {
         Write-Host "Result:"$test.RESULT.InnerText -ForegroundColor Red
         $notPassCount += 1
      }
      Write-Host ""
   }

   if ($notPassCount -ne 0)
   {
      Write-Host "Error: ${Report} contains fail tests" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }
}
function main()
{
   $Env:Path += "$MSBuildDir;"
   $Env:Path += "$AppcertDir;"

   # Write-Host $Env:Path

   foreach($BuildTarget in $BuildTargets)
   {
      $testDirectory =  Join-Path $DlibDotNetRoot test |
                        Join-Path -ChildPath DlibDotNet.UWP.Tests
      Set-Location $testDirectory

      $OutputDir = Join-Path $testDirectory Package

      Update-Version $testDirectory $Version
      Build $BuildTarget.Architecture $OutputDir $Thumbprint, $CertificateKeyFile
      $report = AppCert $BuildTarget.Architecture $OutputDir
      
      # copy report as artifacts
      $TestDir = Join-Path $NugetPath artifacts | `
                  Join-Path -ChildPath test | `
                  Join-Path -ChildPath $BuildTarget.Package | `
                  Join-Path -ChildPath $Version | `
                  Join-Path -ChildPath $OperatingSystem | `
                  Join-Path -ChildPath $OperatingSystemVersion
      if (!(Test-Path "$TestDir")) {
         New-Item "$TestDir" -ItemType Directory > $null
      }
      Copy-Item $report $TestDir

      Check-Report $report
   }

   Set-Location -Path $Current
}

# Store current directory
$Current = Get-Location

main