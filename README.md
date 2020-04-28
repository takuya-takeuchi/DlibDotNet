# ![Alt text](nuget/ml48.png "DlibDotNet") DlibDotNet [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)]()

Dlib wrapper written in C++ and C# for Windows, MacOS and Linux

#### DlibDotNet

|Package|OS|x86|x64|ARM|ARM64|Nuget|
|---|---|---|---|---|---|---|
|DlibDotNet (CPU)|Windows|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.svg)](https://www.nuget.org/packages/DlibDotNet)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.svg)](https://www.nuget.org/packages/DlibDotNet)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.svg)](https://www.nuget.org/packages/DlibDotNet)|
|DlibDotNet for CUDA 9.2|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA92.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA92)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA92.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA92)|
||OSX|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA92.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA92)|
|DlibDotNet for CUDA 10.0|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA100.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA100)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA100.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA100)|
||OSX|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA100.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA100)|
|DlibDotNet for CUDA 10.1|Windows|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA101.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA101)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA101.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA101)|
||OSX|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.CUDA101.svg)](https://www.nuget.org/packages/DlibDotNet.CUDA101)|
|DlibDotNet for Intel MKL|Windows|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.MKL.svg)](https://www.nuget.org/packages/DlibDotNet.MKL)|
||Linux|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.MKL.svg)](https://www.nuget.org/packages/DlibDotNet.MKL)|
||OSX|-|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.MKL.svg)](https://www.nuget.org/packages/DlibDotNet.MKL)|
|DlibDotNet for UWP|Windows|✓|✓|✓|✓|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.UWP.svg)](https://www.nuget.org/packages/DlibDotNet.UWP)|
||Linux|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.UWP.svg)](https://www.nuget.org/packages/DlibDotNet.UWP)|
||OSX|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.UWP.svg)](https://www.nuget.org/packages/DlibDotNet.UWP)|
|DlibDotNet for ARM|Windows|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet-ARM.svg)](https://www.nuget.org/packages/DlibDotNet-ARM)|
||Linux|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet-ARM.svg)](https://www.nuget.org/packages/DlibDotNet-ARM)|
||OSX|-|-|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet-ARM.svg)](https://www.nuget.org/packages/DlibDotNet-ARM)|

##### :warning: DlibDotNet for ARM  is not tested yet

#### DlibDotNet.Extensions

|Package|OS|x86|x64|ARM|ARM64|Nuget|
|---|---|---|---|---|---|---|
|All Architectures|Windows|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.Extensions.svg)](https://www.nuget.org/packages/DlibDotNet.Extensions)|
||Linux|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.Extensions.svg)](https://www.nuget.org/packages/DlibDotNet.Extensions)|
||OSX|✓|✓|-|-|[![NuGet version](https://img.shields.io/nuget/v/DlibDotNet.Extensions.svg)](https://www.nuget.org/packages/DlibDotNet.Extensions)|

## Demo

### Linux
<img src="images/linux.gif?raw=true" width="400x300" title="Video Tracking on Ubuntu"/>

### MacOS
<img src="images/mac.gif?raw=true" width="400x300" title="Video Tracking on MacOS"/>

### Windows
<img src="images/win.gif?raw=true" width="400x200" title="Video Tracking on Windows"/>

## Related Projects

- [FaceRecognition.Net](https://github.com/takuya-takeuchi/FaceRecognitionDotNet)
  - Face recognition .NET library uses DlibDotNet

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