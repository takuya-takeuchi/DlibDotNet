$current = $PSScriptRoot
$src = Split-Path $current -Parent
$native = Join-Path $src DlibDotNet.Native | `
          Join-Path -ChildPath "dlib"
$nativeDnn = Join-Path $src DlibDotNet.Native.Dnn | `
             Join-Path -ChildPath "dlib"
$root = Join-Path $current dlib

# common
$sources = @(
    "shared.h",
    "template.h",
    "export.h",
    "stdvector.cpp",
    "stdvector.h",
    "vector_streambuf.cpp",
    "vector_streambuf.h"
)
foreach ($f in $sources)
{
    $s = Join-Path $native $f
    $d = Join-Path $root $f
    Copy-Item $s $d -Force
}

# array
$directory = "array"
$sources = @(
    "array.h",
    "array.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# array2d
$directory = "array2d"
$sources = @(
    "array2d.h",
    "array2d.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# extensions
$directory = "extensions"
$sources = @(
    "extensions.h",
    "extensions.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# geometry
$directory = "geometry"
$sources = @(
    "common.h",
    "common.cpp",
    "dpoint.h",
    "dpoint.cpp",
    "drectangle.h",
    "drectangle.cpp",
    "point.h",
    "point.cpp",
    "rectangle.h",
    "rectangle.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
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
    Copy-Item $s $d -Force
}

# geometry
$directory = "rand"
$sources = @(
    "rand_kernel_1.h",
    "rand_kernel_1.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# cuda
$directory = "cuda"
$sources = @(
    "cuda.h",
    "cuda.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# dnn
$directory = "dnn"
$sources = @(
    "dnn"
)
$deletes = @(
    "input.cpp",
    "input.h",
    "validation.cpp",
    "validation.h",
    # "solvers/adam.cpp",
    # "solvers/adam.h",
    "solvers/sgd.cpp",
    "solvers/sgd.h",
    "loss/multiclass_log_per_pixel"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $nativeDnn $directory | Join-Path -ChildPath "*"
    $d = Join-Path $root      $directory
    Copy-Item "${s}" "${d}" -Recurse -Force
}
foreach ($f in $deletes)
{
    $d = Join-Path $root $directory | Join-Path -ChildPath $f
    $item = Get-Item "${d}"
    if ($item -is [System.IO.DirectoryInfo])
    {
        Remove-Item "${d}" -Recurse
    }
    else
    {
        Remove-Item "${d}"
    }
}

# cuda
$directory = "cuda"
$newDirectory = "cuda_dnn"
$sources = @(
    "cuda.h",
    "cuda.cpp"
)
New-Item (Join-Path $root $newDirectory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $nativeDnn $directory    | Join-Path -ChildPath $f
    $d = Join-Path $root      $newDirectory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}