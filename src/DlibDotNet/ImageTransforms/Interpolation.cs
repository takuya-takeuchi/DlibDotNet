#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void AddImageLeftRightFlips<T>(IList<Matrix<T>> images, IList<IList<Rectangle>> objects)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            images.ThrowIfDisposed();

            using (var vecImage = new StdVector<Matrix<T>>(images))
            using (var disposer = new EnumerableDisposer<StdVector<Rectangle>>(objects.Select(r => new StdVector<Rectangle>(r))))
            using (var vecObject = new StdVector<StdVector<Rectangle>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<Rectangle>>(vecObject))
            {
                Matrix<T>.TryParse<T>(out var matrixElementType);
                var ret = NativeMethods.add_image_left_right_flips_rectangle(matrixElementType.ToNativeMatrixElementType(),
                                                                             vecImage.NativePtr,
                                                                             vecObject.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixElementType} is not supported.");
                }

                images.Clear();
                foreach (var matrix in vecImage.ToArray())
                    images.Add(matrix);
                objects.Clear();
                foreach (var list in vecObject.ToArray())
                    objects.Add(list.ToArray());
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
                var ret = NativeMethods.extract_image_chips(array2DType,
                                                            image.NativePtr,
                                                            vectorOfChips.NativePtr,
                                                            array.ImageType.ToNativeArray2DType(),
                                                            array.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException("Output or input type is not supported.");
                }

                return array;
            }
        }

        public static Array<Matrix<T>> ExtractImageChips<T>(MatrixBase image, IEnumerable<ChipDetails> chipLocations)
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
                var array = new Array<Matrix<T>>();
                var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.extract_image_chips_matrix(matrixElementType,
                                                                   image.NativePtr,
                                                                   vectorOfChips.NativePtr,
                                                                   array.MatrixElementTypes.ToNativeMatrixElementType(),
                                                                   array.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{image.MatrixElementType} is not supported.");
                }

                return array;
            }
        }

        public static Array2D<T> ExtractImageChip<T>(Array2DBase image, ChipDetails chipLocation, InterpolationTypes type = InterpolationTypes.Bilinear)
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
            var ret = NativeMethods.extract_image_chip2(array2DType,
                                                        image.NativePtr,
                                                        chipLocation.NativePtr,
                                                        chip.ImageType.ToNativeArray2DType(),
                                                        type.ToNativeInterpolationTypes(),
                                                        chip.NativePtr);

            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
                case NativeMethods.ErrorType.GeneralInvalidParameter:
                    throw new ArgumentException($"{type} is not supported for {array2DType}.");
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
            var ret = NativeMethods.extract_image_chip_matrix2(elementType,
                                                               image.NativePtr,
                                                               chipLocation.NativePtr,
                                                               chip.MatrixElementType.ToNativeMatrixElementType(),
                                                               type.ToNativeInterpolationTypes(),
                                                               chip.NativePtr);

            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{image.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.GeneralInvalidParameter:
                    throw new ArgumentException($"{type} is not supported for {elementType}.");
            }

            return chip;
        }

        public static void FlipImageLeftRight(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.flip_image_left_right(array2DType, image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
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
            var ret = NativeMethods.flip_image_left_right2(inType, inputImage.NativePtr, outType, outputImage.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
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
            var ret = NativeMethods.flip_image_up_down(inType, inputImage.NativePtr, outType, outputImage.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static Rectangle FlipRectLeftRight(Rectangle rect, Rectangle window)
        {
            using (var rectNative = rect.ToNative())
            using (var windowNative = window.ToNative())
            {
                var ret = NativeMethods.flip_rect_left_right(rectNative.NativePtr, windowNative.NativePtr);
                return new Rectangle(ret);
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
                NativeMethods.get_face_chip_details(vector.NativePtr, size, padding, vectorOfChips.NativePtr);
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

            NativeMethods.get_face_chip_details2(det.NativePtr, size, padding, out var ret);
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

            var elementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.jitter_image(elementType,
                                                 image.NativePtr,
                                                 random.NativePtr,
                                                 out var retImage);

            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{image.MatrixElementType} is not supported.");
            }

            return new Matrix<T>(retImage);
        }

        public static void PyramidUp(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.pyramid_up(array2DType, image.NativePtr);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void PyramidUp(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var type = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.pyramid_up_matrix(type, image.NativePtr);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");
        }

        public static void PyramidUp<T>(Matrix<T> image, PyramidDown pyramid, out Matrix<T> matrix)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (pyramid == null)
                throw new ArgumentNullException(nameof(pyramid));

            image.ThrowIfDisposed(nameof(image));
            pyramid.ThrowIfDisposed(nameof(pyramid));

            var type = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.pyramid_up_matrix2(type, image.NativePtr, pyramid.NativePtr, pyramid.PyramidRate, out var ptr);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            matrix = new Matrix<T>(ptr);
        }

        public static void PyramidUp<T>(MatrixBase image, uint pyramidRate)
            where T : Pyramid
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            var type = image.MatrixElementType.ToNativeMatrixElementType();
            Pyramid.TryGetSupportPyramidType<T>(out var pyramidType);
            var ret = NativeMethods.pyramid_up_pyramid_matrix(pyramidType,
                                                       pyramidRate,
                                                       type,
                                                       image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{image.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.PyramidNotSupportType:
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
            }
        }

        public static void ResizeImage(Array2DBase inputImage, double scale)
        {
            if (inputImage == null)
                throw new ArgumentNullException(nameof(inputImage));
            if (scale <= 0)
                throw new ArgumentException($"{nameof(scale)} is less than or equal to zero.", nameof(scale));

            inputImage.ThrowIfDisposed(nameof(inputImage));

            var inType = inputImage.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.resize_image3(inType, inputImage.NativePtr, scale);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{inputImage.ImageType} is not supported.");
            }
        }

        public static Matrix<T> ResizeImage<T>(Matrix<T> matrix, uint row, uint column, InterpolationTypes interpolationTypes = InterpolationTypes.Bilinear)
            where T : struct
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed(nameof(matrix));

            var templateRows = (uint)matrix.TemplateRows;
            var templateColumns = (uint)matrix.Columns;

            var result = Matrix<T>.CreateTemplateParameterizeMatrix(templateRows, templateColumns);
            result.SetSize((int)row, (int)column);

            ResizeImage(matrix, result, interpolationTypes);

            return result;
        }

        public static void ResizeImage<T>(Matrix<T> src, Matrix<T> dst, InterpolationTypes interpolationTypes = InterpolationTypes.Bilinear)
            where T : struct
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            src.ThrowIfDisposed(nameof(src));
            dst.ThrowIfDisposed(nameof(dst));

            var templateRows = (uint)src.TemplateRows;
            var templateColumns = (uint)src.TemplateColumns;
            if (templateRows != dst.TemplateRows || templateColumns != dst.TemplateColumns)
                throw new ArgumentException();

            var inType = src.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.resize_image_matrix(inType,
                                                        src.NativePtr,
                                                        templateRows,
                                                        templateColumns,
                                                        dst.NativePtr,
                                                        interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(src.TemplateColumns)} or {nameof(src.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{src.MatrixElementType} is not supported.");
            }
        }

        public static void ResizeImage(MatrixBase matrix, double scale)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (scale <= 0)
                throw new ArgumentException($"{nameof(scale)} is less than or equal to zero.", nameof(scale));

            matrix.ThrowIfDisposed(nameof(matrix));

            var inType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.resize_image_matrix_scale(inType,
                                                              matrix.NativePtr,
                                                              matrix.TemplateRows,
                                                              matrix.TemplateColumns,
                                                              scale);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
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
            var ret = NativeMethods.resize_image2(inType, inputImage.NativePtr, outType, outputImage.NativePtr, interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void RotateImage(Array2DBase inputImage, Array2DBase outputImage, double radian, InterpolationTypes interpolationTypes = InterpolationTypes.Quadratic)
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
            var ret = NativeMethods.rotate_image2(inType, inputImage.NativePtr, outType, outputImage.NativePtr, radian, interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
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
            var ret = NativeMethods.transform_image(inType,
                                                    inputImage.NativePtr,
                                                    outType,
                                                    outputImage.NativePtr,
                                                    pointTransform.GetNativePointMappingTypes(),
                                                    pointTransform.NativePtr,
                                                    interpolationTypes.ToNativeInterpolationTypes());
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void UpsampleImageDataset<T>(uint pyramidRate,
                                                   IList<Matrix<T>> images,
                                                   IList<IList<Rectangle>> objects,
                                                   uint maxImageSize = uint.MaxValue)
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
            using (var disposer = new EnumerableDisposer<StdVector<Rectangle>>(objects.Select(r => new StdVector<Rectangle>(r))))
            using (var vecObject = new StdVector<StdVector<Rectangle>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<Rectangle>>(vecObject))
            {
                Matrix<T>.TryParse<T>(out var matrixElementType);
                var ret = NativeMethods.upsample_image_dataset_pyramid_down_rect(pyramidRate,
                                                                                 matrixElementType.ToNativeMatrixElementType(),
                                                                                 vecImage.NativePtr,
                                                                                 vecObject.NativePtr,
                                                                                 maxImageSize);
                switch (ret)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new ArgumentException($"{pyramidRate} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixElementType} is not supported.");
                }

                images.Clear();
                foreach (var matrix in vecImage.ToArray())
                    images.Add(matrix);
                objects.Clear();
                foreach (var list in vecObject.ToArray())
                    objects.Add(list.ToArray());
            }
        }


        public static void UpsampleImageDataset<T>(uint pyramidRate,
                                                   IList<Matrix<T>> images,
                                                   IList<IList<MModRect>> objects,
                                                   uint maxImageSize = uint.MaxValue)
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
            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(objects.Select(r => new StdVector<MModRect>(r))))
            using (var vecObject = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(vecObject))
            {
                Matrix<T>.TryParse<T>(out var matrixElementType);
                var ret = NativeMethods.upsample_image_dataset_pyramid_down_mmod_rect(pyramidRate,
                                                                                      matrixElementType.ToNativeMatrixElementType(),
                                                                                      vecImage.NativePtr,
                                                                                      vecObject.NativePtr,
                                                                                      maxImageSize);
                switch (ret)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new ArgumentException($"{pyramidRate} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixElementType} is not supported.");
                }

                images.Clear();
                foreach (var matrix in vecImage.ToArray())
                    images.Add(matrix);
                objects.Clear();
                foreach (var list in vecObject.ToArray())
                    objects.Add(list.ToArray());
            }
        }

        #endregion

    }

}
#endif
