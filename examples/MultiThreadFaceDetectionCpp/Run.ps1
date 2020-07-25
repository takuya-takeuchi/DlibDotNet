Param
(
   [Parameter(
   Mandatory=$True,
   Position = 1
   )][int]
   $ThreadNum
)

$current = Get-Location

if ($IsWindows)
{
    $taget_dir = "build_win/Release"
}
elseif ($IsLinux)
{
    $taget_dir = "build_linux"
}
elseif ($IsMacOS)
{
    $taget_dir = "build_osx"
}

Copy-Item data/mmod_human_face_detector.dat $taget_dir
Copy-Item data/2007_007763.jpg $taget_dir
Set-Location $taget_dir

if ($IsWindows)
{
    cmd.exe /c MultiThreadFaceDetection.exe mmod_human_face_detector.dat `
                                            $ThreadNum `
                                            2007_007763.jpg
}
elseif ($IsLinux)
{
    ./MultiThreadFaceDetection mmod_human_face_detector.dat `
                               $ThreadNum `
                               2007_007763.jpg
}
elseif ($IsMacOS)
{
    ./MultiThreadFaceDetection mmod_human_face_detector.dat `
                               $ThreadNum `
                               2007_007763.jpg
}

Set-Location $current