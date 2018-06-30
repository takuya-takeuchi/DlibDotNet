set CURDIR=%cd%
set DIRLINK=F:\dlib\build
mkdir %DIRLINK%
mklink /d build %DIRLINK%

set DIRLINK=F:\dlib\build_cuda
mkdir %DIRLINK%
mklink /d build_cuda %DIRLINK%