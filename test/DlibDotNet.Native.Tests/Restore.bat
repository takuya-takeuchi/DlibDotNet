
nuget config -set repositoryPath=%USERPROFILE%\.nuget -configfile nuget.config
dotnet add PackageReference.csproj package "DlibDotNet" -f net472 -s %USERPROFILE%\.nuget\
dotnet restore --force-evaluate -v detailed --configfile nuget.config
dotnet build -c Release