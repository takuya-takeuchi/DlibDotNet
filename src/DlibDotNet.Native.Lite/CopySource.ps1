$current = $PSScriptRoot
$src = Split-Path $current -Parent
$native = Join-Path $src DlibDotNet.Native | `
          Join-Path -ChildPath "dlib"
$nativeDnn = Join-Path $src DlibDotNet.Native.Dnn | `
             Join-Path -ChildPath "dlib"
$root = Join-Path $current dlib

# coomon
$sources = @(
    "shared.h",
    "template.h",
    "export.h",
    "vector_streambuf.cpp",
    "vector_streambuf.h"
)
foreach ($f in $sources)
{
    $s = Join-Path $native $f
    $d = Join-Path $root $f
    Copy-Item $s $d
}

# matrix
$directory = "matrix"
$sources = @(
    "matrix.h",
    "matrix.cpp",
    "matrix_common.h",
    "matrix_common.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d
}

# dnn
$directory = "dnn"
$sources = @(
    "dnn"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $nativeDnn $directory | Join-Path -ChildPath "*"
    $d = Join-Path $root      $directory
    Copy-Item $s $d -Recurse
}