if (!(Test-Path ${env:JAVA_HOME}))
{
    Write-Host "[Error] env JAVA_HOME is empty or does not exist" -Foreground Red
    return
}

$java = Join-Path ${env:JAVA_HOME} "bin" | `
        Join-Path -Child "java"
if ($global:IsWindows)
{
    $java = $java + ".exe"
}

if (!(Test-Path ${java}))
{
    Write-Host "[Error] ${java} does not exist" -Foreground Red
    return
}

if ((Test-Path swagger.json))
{
    Write-Host "[Info] delete existing swagger.json" -Foreground Yellow
    Remove-Item swagger.json
}

$swaggerUrl = "https://oss.sonatype.org/content/repositories/snapshots/io/swagger/codegen/v3/swagger-codegen-cli/3.0.24-SNAPSHOT/swagger-codegen-cli-3.0.24-20201118.130427-14.jar"
# $swaggerUrl = "https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.23/swagger-codegen-cli-3.0.23.jar"
if (!(Test-Path "swagger-codegen-cli.jar"))
{
    Write-Host "[Info] Download swagger-codegen-cli.jar" -Foreground Yellow
    Invoke-WebRequest -Uri "${swaggerUrl}" `
                      -outfile swagger-codegen-cli.jar
}

Invoke-WebRequest -Uri "http://localhost:5000/swagger/v1/swagger.json" `
                  -outfile swagger.json

if (!(Test-Path swagger.json))
{
    Write-Host "[Error] swagger.json does not exist" -Foreground Red
    return
}

Write-Host "[Info] Succeed to get swagger.json" -Foreground Green

$current = Get-Location
$executable = Join-Path $current "swagger-codegen-cli.jar"
$arguments = "generate " + 
             "-i swagger.json " + 
             "-l csharp " + 
             "-o Server " + 
             "-DpackageName=`"FaceDetection.Server`" " + 
             "-DnetCoreProjectFile=true " + 
             "-DtargetFramework=`"v5.0`""
Invoke-Expression "& '${java}' -jar ${executable} ${arguments}"
Remove-Item swagger.json

# $source = Join-Path "Server" "src" | `
#           Join-Path -Child "IO.Swagger"

# if (!(Test-Path $source))
# {
#     Write-Host "[Error] Failed to generate client lib" -Foreground Red
#     return
# }

# Set-Location $source
# Invoke-Expression "& dotnet build -c Release"