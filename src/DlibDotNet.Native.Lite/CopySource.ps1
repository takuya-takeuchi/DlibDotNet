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
    "ostringstream.cpp",
    "ostringstream.h",
    "pair.cpp",
    "pair.h",
    "pixel.cpp",
    "pixel.h",
    "stdvector.cpp",
    "stdvector.h",
    "stdstring.cpp",
    "stdstring.h",
    "stdlib.cpp",
    "stdlib.h",
    "serialize.cpp",
    "serialize.h",
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

# cstd
$directory = "cstd"
$sources = @(
    "string.h",
    "string.cpp"
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
    "rectangle.cpp",
    "point_transforms.h",
    "point_transforms.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_loader
$directory = "image_loader"
$sources = @(
    "load_bmp.cpp",
    "load_bmp.h",
    "load_dng.cpp",
    "load_dng.h",
    "load_image.cpp",
    "load_image.h",
    "load_jpeg.cpp",
    "load_jpeg.h",
    "load_png.cpp",
    "load_png.h"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_processing
$directory = "image_processing"
$sources = @(
    "box_overlap_testing.h",
    "box_overlap_testing.cpp",
    "frontal_face_detector.h",
    "frontal_face_detector.cpp",
    "object_detector.h",
    "object_detector.cpp",
    "shape_predictor.h",
    "shape_predictor.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_transforms
$directory = "image_transforms"
$sources = @(
    "assign_image.h",
    "assign_image.cpp",
    "fhog.h",
    "fhog.cpp",
    "image_pyramid.h",
    "image_pyramid.cpp",
    "interpolation.h",
    "interpolation.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_transforms/interpolations
$directory = "image_transforms/interpolations"
$sources = @(
    "resize_image.h",
    "resize_image.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_saver
$directory = "image_saver"
$sources = @(
    "save_bmp.cpp",
    "save_bmp.h",
    "save_dng.cpp",
    "save_dng.h",
    "save_jpeg.cpp",
    "save_jpeg.h",
    "save_png.cpp",
    "save_png.h"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_processing
$directory = "image_processing"
$sources = @(
    "full_object_detection.h",
    "full_object_detection.cpp",
    "scan_fhog_pyramid.h",
    "scan_fhog_pyramid.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# image_processing/object_detector
$directory = "image_processing/object_detector"
$sources = @(
    "scan_fhog_pyramid.h",
    "scan_fhog_pyramid.cpp"
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
    "matrix_common.cpp",
    "matrix_expressions.h",
    "matrix_expressions.cpp",
    "matrix_mat.h",
    "matrix_mat.cpp",
    "matrix_utilities.h",
    "matrix_utilities.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# matrix/matrix_op
$directory = "matrix/matrix_op"
$sources = @(
    "op_heatmap.h",
    "op_heatmap.cpp",
    "op_jet.h",
    "op_jet.cpp",
    "op_join_rows.h",
    "op_join_rows.cpp",
    "op_trans.h",
    "op_trans.cpp",
    "op_array2d_to_mat.h",
    "op_array2d_to_mat.cpp",
    "op_std_vect_to_mat.h",
    "op_std_vect_to_mat.cpp",
    "op_std_vect_to_mat_value.h",
    "op_std_vect_to_mat_value.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# svm
$directory = "svm"
$sources = @(
    "function.h",
    "function.cpp",
    "krls.h",
    "krls.cpp",
    "template.h"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# svm/cross_validate_object_detection_trainer
$directory = "svm/cross_validate_object_detection_trainer"
$sources = @(
    "scan_fhog_pyramid.h",
    "scan_fhog_pyramid.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# svm/kernel
$directory = "svm/kernel"
$sources = @(
    "radial_basis_kernel.h",
    "radial_basis_kernel.cpp",
    "histogram_intersection_kernel.h",
    "histogram_intersection_kernel.cpp",
    "linear_kernel.h",
    "linear_kernel.cpp",
    "polynomial_kernel.h",
    "polynomial_kernel.cpp",
    "sigmoid_kernel.h",
    "sigmoid_kernel.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# svm/kernel
$directory = "svm/structural_object_detection_trainer"
$sources = @(
    "scan_fhog_pyramid.h",
    "scan_fhog_pyramid.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# rand
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

# dnn
$directory = "dnn"
$sources = @(
    "dnn"
)
$deletes = @(
    # "solvers/adam.cpp",
    # "solvers/adam.h",
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