#!/bin/bash
dotnet restore ../src/DlibDotNet -r linux-x64
dotnet build -c Release -r linux-x64 ../src/DlibDotNet
cp ../src/DlibDotNet/bin/Release/netstandard2.0/linux-x64/DlibDotNet.dll lib/netcoreapp2.0/DlibDotNet.dll
dotnet pack DlibDotNet.proj -o .
mv DlibDotNet*.nupkg ~/.nuget
rm bin/ -r
rm obj/ -r
ls ~/.nuget/DlibDotNet.*.nupkg
dotnet nuget push -s https://www.myget.org/F/nuget-dotnet/api/v3/index.json ~/.nuget/DlibDotNet.19.16.99.20190105-rc1.nupkg -k 23105fce-ba5b-4e50-90a3-e26d71737457
