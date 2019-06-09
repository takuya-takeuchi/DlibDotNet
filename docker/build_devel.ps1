Param()

$dockerfiles = Get-ChildItem  -Recurse devel -include Dockerfile

foreach($dockerfile in $dockerfiles)
{
   $relativePath = Resolve-Path $dockerfile -Relative
   $dockerfileDirectory = $relativePath.Replace('\Dockerfile', '')
   $tag = "dlibdotnet" + (Resolve-Path $dockerfileDirectory -Relative).Trim('.').Replace('\', '/')

   Write-Host "Start docker build -t $tag $dockerfileDirectory" -ForegroundColor Green
   docker build -t $tag $dockerfileDirectory
}