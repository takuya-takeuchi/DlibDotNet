Param()

$requirementFiles = Get-ChildItem  -Recurse base -include Requirement.ps1
$dockerfiles = Get-ChildItem  -Recurse devel -include Dockerfile

foreach($requirement in $requirementFiles)
{
   $relativePath = Resolve-Path $requirement -Relative
   Write-Host "pwsh ${relativePath}" -ForegroundColor Green
   pwsh $relativePath
}

foreach($dockerfile in $dockerfiles)
{
   $relativePath = Resolve-Path $dockerfile -Relative
   $dockerfileDirectory = Resolve-Path ((Get-ChildItem $relativePath).Directory.FullName) -Relative
   $tag = "dlibdotnet" + (Resolve-Path $dockerfileDirectory -Relative).Trim('.').Replace('\', '/')

   Write-Host "Start 'docker build -t $tag $dockerfileDirectory'" -ForegroundColor Green
   docker build --force-rm=true -t $tag $dockerfileDirectory

   if ($lastexitcode -ne 0)
   {
      Write-Host "Failed 'docker build -t $tag $dockerfileDirectory'" -ForegroundColor Red
      exit -1
   }
}