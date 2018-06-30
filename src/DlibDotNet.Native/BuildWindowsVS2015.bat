set CURDIR=%cd%
set DIRLINK=F:\dlib\build
mkdir %DIRLINK%
mklink /d build %DIRLINK%
cd build
set DLIBVER=19.13
cmake -G "Visual Studio 14 2015 Win64" ^
           -DDLIB_PATH=D:/Works/Lib/DLib/%DLIBVER% ^
           ..
cmake --build . --config %1
cd %CURDIR%