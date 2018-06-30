set CURDIR=%cd%
set DIRLINK=F:\dlib\build_cuda
mkdir %DIRLINK%
mklink /d build_cuda %DIRLINK%
cd build_cuda
set DLIBVER=19.13
cmake -G "Visual Studio 14 2015 Win64" ^
           -DDLIB_PATH=D:/Works/Lib/DLib/%DLIBVER% ^
           -DCMAKE_PREFIX_PATH=D:/Works/Lib/NVIDIA/cuDNN/9.1/7.0/Win10 ^
           ..
cmake --build . --config %1
cd %CURDIR%