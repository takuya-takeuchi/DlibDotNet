rd %USERPROFILE%\.nuget\packages\dlibdotnet\ /Q /S
rd %USERPROFILE%\.nuget\packages\dlibdotnet.native\ /Q /S
nuget config -set repositoryPath=%USERPROFILE%\.nuget -configfile nuget.config
dotnet add PackageReference.csproj package "DlibDotNet" -f net472 -s %USERPROFILE%\.nuget\
rem dotnet add PackageReference.csproj package "DlibDotNet" -s https://www.myget.org/F/nuget-dotnet/api/v3/index.json
dotnet restore --force-evaluate -v detailed --configfile nuget.config
dotnet build -c Release