#!/bin/bash
dotnet add package DlibDotNet --version 19.16.99.20190105-rc1 --source https://www.myget.org/F/nuget-dotnet/api/v3/index.json --framework netcoreapp2.0
dotnet build -c Release -f netcoreapp2.0 -r linux-x64