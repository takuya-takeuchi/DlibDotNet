dotnet restore ..\src\DlibDotNet
dotnet build -c Release ..\src\DlibDotNet

@set target=101 100 92

for %%t in (%target%) do (
  nuget pack DlibDotNet.CUDA-%%t.nuspec
)