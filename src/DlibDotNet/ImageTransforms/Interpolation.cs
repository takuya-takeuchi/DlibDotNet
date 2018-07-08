using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void AddImageLeftRightFlips<T>(IList<Matrix<T>> images, IList<IEnumerable<Rectangle>> objects)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            images.ThrowIfDisposed();

            using (var vecImage = new StdVector<Matrix<T>>(images))
            {
                var tmp = objects.Select(rectangles => new StdVector<Rectangle>(rectangles));
                using (var vecObject = new StdVector<StdVector<Rectangle>>(tmp))
                {
                    Matrix<T>.TryParse<T>(out var matrixElementType);
                    var ret = Native.add_image_left_right_flips_rectangle(matrixElementType.ToNativeMatrixElementType(),
                                                                          vecImage.NativePtr,
                                                                          vecObject.NativePtr);
                    switch (ret)
                    {
                        case Native.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{matrixElementType} is not supported.");
                    }

                    images.Clear();
                    foreach (var matrix in vecImage.ToArray())
                        images.Add(matrix);
                    objects.Clear();
                    foreach (var list in vecObject.ToArray())
                        objects.Add(list);
                }
            }
        }

        public static Array<Array2D<T>> ExtractImageChips<T>(Array2DBase image, IEnumerable<ChipDetails> chipLocations)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (chipLocations == null)
                throw new ArgumentNullException(nameof(chipLocations));
            if (chipLocations.Any(chip => chip == null || chip.IsDisposed || !chip.IsValid()))
                throw new ArgumentException($"{nameof(chipLocations)} includes invalid item.");

            image.ThrowIfDisposed();

            using (var vectorOfChips = new StdVector<ChipDetails>(chipLocations))
            {
                var array = new Array<Array2D<T>>();
                var array2DType = image.ImageType.ToNativeArray2DType();
                var ret = Native.extract_image_chips(array2DType,
                                                    image.NativePtr,
                                                    vectorOfChips.NativePtr,
                                                    array.ImageType.ToNativeArray2DType(),
                                                    array.NativePtr);
                switch (ret)
                {
                    case Native.ErrorType.InputArrayTypeNotSupport:
                        throw new ArgumentException($"{image.ImageType} is not supported.");
                    case Native.ErrorType.OutputArrayTypeNotSupport:
                        throw new ArgumentException($"{array.ImageType} is not supported.");
                }

                return array;
            }
        }

        public static Array2D<T> ExtractImageChip<T>(Array2DBase image, ChipDetails chipLocation, InterpolationTypes type = InterpolationTypes.NearestNeighbor)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (chipLocation == null)
                throw new ArgumentNullException(nameof(chipLocation));

            image.ThrowIfDisposed();
            chipLocation.ThrowIfDisposed();

            if (!chipLocation.IsValid())
                throw new ArgumentException($"{nameof(chipLocation)} is invalid item.");

            var chip = new Array2D<T>();
            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.extract_image_chip2(array2DType,
                                                 image.NativePtr,
                                                 chipLocation.NativePtr,
                                                 chip.ImageType.ToNativeArray2DType(),
                                                 type.ToNativeInterpolationTypes(),
                                                 chip.NativePtr);

            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"{chip.ImageType} is not supported.");
            }

            return chip;
        }

        public static Matrix<T> ExtractImageChip<T>(MatrixBase image, ChipDetails chipLocation, InterpolationTypes type = InterpolationTypes.NearestNeighbor)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (chipLocation == null)
                throw new ArgumentNullException(nameof(chipLocation));

            image.ThrowIfDisposed();
            chipLocation.ThrowIfDisposed();

            if (!chipLocation.IsValid())
                throw new ArgumentException($"{nameof(chipLocation)} is invalid item.");

            var chip = new Matrix<T>();
            var elementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.extract_image_chip_matrix2(elementType,
                                                        image.NativePtr,
                                                        chipLocation.NativePtr,
                                                        chip.MatrixElementType.ToNativeMatrixElementType(),
                                                        type.ToNativeInterpolationTypes(),
                                                        chip.NativePtr);

            switch (ret)
            {
                case Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"{image.MatrixElementType} is not supported.");
                case Native.ErrorType.OutputElementTypeNotSupport:
                    throw new ArgumentException($"{chip.MatrixElementType} is not supported.");
            }

            return chip;
        }

        public static void FlipImageLeftRight(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.flip_image_left_right(array2DType, image.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
            }
        }

        public static void FlipImageLeftRight(Array2DBase inputImage, Array2DBase outputImage)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (outputImage == null)
                throw new ArgumentNullException(nameof(outputImage));
            if (inputImage == outputImage)
                throw new ArgumentException();

            inputImage.ThrowIfDisposed(nameof(inputImage));
            outputImage.ThrowIfDisposed(nameof(outputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var outType = outputImage.ImageType.ToNativeArray2DType();
            var ret = Native.flip_image_left_right2(inType, inputImage.NativePtr, outType, outputImage.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inputImage.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outputImage.ImageType} is not supported.");
            }
        }

        public static void FlipImageUpDown(Array2DBase inputImage, Array2DBase outputImage)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (outputImage == null)
                throw new ArgumentNullException(nameof(outputImage));
            if (inputImage == outputImage)
                throw new ArgumentException();

            inputImage.ThrowIfDisposed(nameof(inputImage));
            outputImage.ThrowIfDisposed(nameof(outputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var outType = outputImage.ImageType.ToNativeArray2DType();
            var ret = Native.flip_image_up_down(inType, inputImage.NativePtr, outType, outputImage.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inputImage.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outputImage.ImageType} is not supported.");
            }
        }

        public static ChipDetails[] GetFaceChipDetails(IEnumerable<FullObjectDetection> dets, uint size = 200, double padding = 0.2d)
        {
            if (dets == null)
                throw new ArgumentNullException(nameof(dets));
            if (size <= 0)
                throw new ArgumentException();
            if (padding < 0)
                throw new ArgumentException();
            if (dets.Any(detection => detection == null || detection.IsDisposed || detection.Parts != 68))
                throw new ArgumentException($"{nameof(dets)} includes invalid item.");

            using (var vector = new StdVector<FullObjectDetection>(dets))
            using (var vectorOfChips = new StdVector<ChipDetails>())
            {
                Native.get_face_chip_details(vector.NativePtr, size, padding, vectorOfChips.NativePtr);
                return vectorOfChips.ToArray();
            }
        }

        public static ChipDetails GetFaceChipDetails(FullObjectDetection det, uint size = 200, double padding = 0.2d)
        {
            if (det == null)
                throw new ArgumentNullException(nameof(det));
            if (size <= 0)
                throw new ArgumentException();
            if (padding < 0)
                throw new ArgumentException();

            det.ThrowIfDisposed();

            if (det.Parts != 68 && det.Parts != 5)
                throw new ArgumentException($"{nameof(det)} is invalid item.");

            Native.get_face_chip_details2(det.NativePtr, size, padding, out var ret);
            return new ChipDetails(ret);
        }

        public static Matrix<T> JitterImage<T>(Matrix<T> image, Rand random)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (random == null)
                throw new ArgumentNullException(nameof(random));

            image.ThrowIfDisposed();
            random.ThrowIfDisposed();

            var chip = new Matrix<T>();
            var elementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.jitter_image(elementType,
                                          image.NativePtr,
                                          random.NativePtr,
                                          out var retImage);

            switch (ret)
            {
                case Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"{image.MatrixElementType} is not supported.");
                case Native.ErrorType.OutputElementTypeNotSupport:
                    throw new ArgumentException($"{chip.MatrixElementType} is not supported.");
            }

            return new Matrix<T>(retImage);
        }

        public static void PyramidUp(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.pyramid_up(array2DType, image.NativePtr);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void PyramidUp(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var type = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.pyramid_up_matrix(type, image.NativePtr);
            if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");
        }

        public static void ResizeImage(Array2DBase inputImage, double scale)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (scale <= 0)
                throw new ArgumentException($"{nameof(scale)} is less than or equal to zero.", nameof(scale));

            inputImage.ThrowIfDisposed(nameof(inputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var ret = Native.resize_image3(inType, inputImage.NativePtr, scale);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{inputImage.ImageType} is not supported.");
            }
        }

        public static void ResizeImage(Array2DBase inputImage, Array2DBase outputImage, InterpolationTypes interpolationTypes = InterpolationTypes.Bilinear)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (outputImage == null)
                throw new ArgumentNullException(nameof(outputImage));
            if (inputImage == outputImage)
                throw new ArgumentException();

            inputImage.ThrowIfDisposed(nameof(inputImage));
            outputImage.ThrowIfDisposed(nameof(outputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var outType = outputImage.ImageType.ToNativeArray2DType();
            var ret = Native.resize_image2(inType, inputImage.NativePtr, outType, outputImage.NativePtr, interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inputImage.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outputImage.ImageType} is not supported.");
            }
        }

        public static void RotateImage(Array2DBase inputImage, Array2DBase outputImage, double angle, InterpolationTypes interpolationTypes = InterpolationTypes.Quadratic)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (outputImage == null)
                throw new ArgumentNullException(nameof(outputImage));
            if (inputImage == outputImage)
                throw new ArgumentException();

            inputImage.ThrowIfDisposed(nameof(inputImage));
            outputImage.ThrowIfDisposed(nameof(outputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var outType = outputImage.ImageType.ToNativeArray2DType();
            var ret = Native.rotate_image2(inType, inputImage.NativePtr, outType, outputImage.NativePtr, angle, interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inputImage.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outputImage.ImageType} is not supported.");
            }
        }

        public static void TransformImage(Array2DBase inputImage, Array2DBase outputImage, PointTransformBase pointTransform, InterpolationTypes interpolationTypes = InterpolationTypes.Quadratic)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (outputImage == null)
                throw new ArgumentNullException(nameof(outputImage));
            if (pointTransform == null)
                throw new ArgumentNullException(nameof(pointTransform));
            if (inputImage == outputImage)
                throw new ArgumentException();

            inputImage.ThrowIfDisposed(nameof(inputImage));
            outputImage.ThrowIfDisposed(nameof(outputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var outType = outputImage.ImageType.ToNativeArray2DType();
            var ret = Native.transform_image(inType, inputImage.NativePtr, outType, outputImage.NativePtr, pointTransform.GetNativePointMappingTypes(), pointTransform.NativePtr, interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inputImage.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outputImage.ImageType} is not supported.");
            }
        }

        public static void UpsampleImageDataset<T>(uint pyramidRate,
                                                   IList<Matrix<T>> images,
                                                   IList<IEnumerable<Rectangle>> objects)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            images.ThrowIfDisposed();

            var imageCount = images.Count();
            var objectCount = objects.Count();

            if (imageCount != objectCount)
                throw new ArgumentException();

            using (var vecImage = new StdVector<Matrix<T>>(images))
            {
                var tmp = objects.Select(rectangles => new StdVector<Rectangle>(rectangles));
                using (var vecObject = new StdVector<StdVector<Rectangle>>(tmp))
                {
                    Matrix<T>.TryParse<T>(out var matrixElementType);
                    var ret = Native.upsample_image_dataset_pyramid_down(pyramidRate,
                                                                         matrixElementType.ToNativeMatrixElementType(),
                                                                         vecImage.NativePtr,
                                                                         vecObject.NativePtr);
                    switch (ret)
                    {
                        case Native.ErrorType.PyramidNotSupportRate:
                            throw new ArgumentException($"{pyramidRate} is not supported.");
                        case Native.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{matrixElementType} is not supported.");
                    }

                    images.Clear();
                    foreach (var matrix in vecImage.ToArray())
                        images.Add(matrix);
                    objects.Clear();
                    foreach (var list in vecObject.ToArray())
                        objects.Add(list);
                }
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType add_image_left_right_flips_rectangle(MatrixElementType elementType, IntPtr images, IntPtr objects);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType flip_image_left_right(Array2DType type, IntPtr img);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType flip_image_left_right2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType flip_image_up_down(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType pyramid_up(Array2DType type, IntPtr img);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType pyramid_up_matrix(MatrixElementType type, IntPtr img);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType resize_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType resize_image2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, InterpolationTypes interpolationTypes);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType resize_image3(Array2DType inType, IntPtr inImg, double scaleSize);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rotate_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, double angle);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rotate_image2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, double angle, InterpolationTypes interpolationTypes);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType transform_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, PointMappingTypes pointMappingTypes, IntPtr mappingObj, InterpolationTypes interpolationTypes);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType get_face_chip_details(IntPtr dets, uint size, double padding, IntPtr vectoChips);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType get_face_chip_details2(IntPtr det, uint size, double padding, out IntPtr chips);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_image_chip(Array2DType img_type, IntPtr in_img, IntPtr chip_location, Array2DType array_type, IntPtr out_chip);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_image_chip2(Array2DType img_type, IntPtr in_img, IntPtr chip_location, Array2DType array_type, InterpolationTypes type, IntPtr out_chip);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_image_chips(Array2DType img_type, IntPtr in_img, IntPtr chip_locations, Array2DType array_type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_image_chip_matrix(MatrixElementType img_type, IntPtr in_img, IntPtr chip_location, MatrixElementType array_type, IntPtr out_chip);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_image_chip_matrix2(MatrixElementType img_type, IntPtr in_img, IntPtr chip_location, MatrixElementType array_type, InterpolationTypes type, IntPtr out_chip);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType jitter_image(MatrixElementType in_type, IntPtr in_img, IntPtr rand, out IntPtr out_img);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType upsample_image_dataset_pyramid_down(uint pyramid_rate, MatrixElementType elementType, IntPtr images, IntPtr objects);

        }

    }

}