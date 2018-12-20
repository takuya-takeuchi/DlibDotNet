dotnet add PackageReference.csproj package DlibDotNet -s %USERPROFILE%\.nuget\
dotnet restore -r win-x64
dotnet build -c Release -r win-x64