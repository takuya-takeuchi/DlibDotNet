#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$False,
      Position = 1
      )][string]
      $Version
)

if ($BuildTarget.CUDA -ne 0)
{
   $dockerAPIVersion = docker version --format '{{.Server.APIVersion}}'
   Write-Host "Docker API Version: $dockerAPIVersion" -ForegroundColor Yellow
   if ($dockerAPIVersion -ge 1.40)
   {
      Write-Host "Start docker run --gpus all --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $versionStr $package $platformTarget $rid" -ForegroundColor Green
      docker run --gpus all --rm `
                  -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -t "$dockername" $versionStr $package $platformTarget $rid
   }
   else
   {
      Write-Host "Start docker run --runtime=nvidia --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $versionStr $package $platformTarget $rid" -ForegroundColor Green
      docker run --runtime=nvidia --rm `
                  -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -t "$dockername" $versionStr $package $platformTarget $rid
   }
}
else
{
   Write-Host "Start docker run --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $versionStr $package $platformTarget $rid" -ForegroundColor Green
   docker run --rm `
               -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
               -e "LOCAL_UID=$(id -u $env:USER)" `
               -e "LOCAL_GID=$(id -g $env:USER)" `
               -t "$dockername" $versionStr $package $platformTarget $rid
}