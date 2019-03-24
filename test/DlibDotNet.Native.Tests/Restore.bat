rd %USERPROFILE%\.nuget\packages\dlibdotnet\ /Q /S
rd %USERPROFILE%\.nuget\packages\dlibdotnet.native\ /Q /S
rem nuget config -set repositoryPath=%USERPROFILE%\.nuget -configfile nuget.config
rem dotnet add PackageReference.csproj package "DlibDotNet" -f net461 -s %USERPROFILE%\.nuget\

dotnet add package DlibDotNet --version 19.16.99.20190105-rc1 -f netcoreapp2.1 --source https://www.myget.org/F/nuget-dotnet/api/v3/index.json
dotnet build -c Release -f netcoreapp2.1 -r win-x64

dotnet add package DlibDotNet --version 19.16.99.20190105-rc1 -f net461 --source https://www.myget.org/F/nuget-dotnet/api/v3/index.json
dotnet build -c Release -f net461 -r win-x64