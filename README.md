# ![Alt text](nuget/ml48.png "Dlib.Net") Dlib.Net [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)]()

Dlib wrapper written in C++ and C# for Windows, MacOS and Linux

|Package|NuGet|
|---|---|
|DlibDotNet|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.svg)](https://www.nuget.org/packages/DlibDotNet)|
|DlibDotNet With CUDA|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet-WithCUDA.svg)](https://www.nuget.org/packages/DlibDotNet-WithCUDA)|

## Demo

### Linux
<img src="images/linux.gif?raw=true" width="400x300" title="Video Tracking on Ubuntu"/>

### MacOS
<img src="images/mac.gif?raw=true" width="400x300" title="Video Tracking on MacOS"/>

### Windows
<img src="images/win.gif?raw=true" width="400x200" title="Video Tracking on Windows"/>

## Related Projects

- [FaceRecognition.Net](https://github.com/takuya-takeuchi/FaceRecognitionDotNet)
  - Face recognition .NET library uses Dlib.Net

## Usage
 
Please refer [wiki](https://github.com/takuya-takeuchi/DlibDotNet/wiki)
 
## Dependencies Libraries and Products

#### [dlib](http://dlib.net/)

> **License:** Boost Software License
>
> **Author:** Davis E. King
> 
> **Principal Use:** A toolkit for making real world machine learning and data analysis applications in C++. Main goal of DlibDotNet is what wraps dlib by C#.

#### [giflib](http://giflib.sourceforge.net/)

> **License:** giflib License
>
> **Author:** Eric S. Raymond
> 
> **Principal Use:** To read and write gif image file. DlibDotNet.Native links this library.

#### [libjpeg](http://www.ijg.org/)

> **License:** Independent JPEG Group's License
>
> **Author:** Independent JPEG Group
> 
> **Principal Use:** To read and write jpeg image file. DlibDotNet.Native is based in part on the work of
the Independent JPEG Group.

#### [libpng](http://libpng.org/pub/png/libpng.html)

> **License:** libpng License
>
> **Author:** Glenn Randers-Pehrson
> 
> **Principal Use:** To read and write png image file. DlibDotNet.Native links this library.

#### [zlib](https://zlib.net/)

> **License:** zlib License
>
> **Author:** Jean-loup Gailly and Mark Adler
> 
> **Principal Use:** To use libpng and DlibDotNet.Native links this library.