using System;
using System.Collections.Generic;
using DlibDotNet.Dnn;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class StdVectorElementTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static StdVectorElementTypesRepository()
        {
            var types = new[]
            {
                new { Type = typeof(int),                                  ElementType = ElementTypes.Int32 },
                new { Type = typeof(uint),                                 ElementType = ElementTypes.UInt32 },
                new { Type = typeof(long),                                 ElementType = ElementTypes.Long  },
                new { Type = typeof(float),                                ElementType = ElementTypes.Float  },
                new { Type = typeof(double),                               ElementType = ElementTypes.Double  },
                new { Type = typeof(Rectangle),                            ElementType = ElementTypes.Rectangle },
                new { Type = typeof(Point),                                ElementType = ElementTypes.Point },
                new { Type = typeof(DPoint),                               ElementType = ElementTypes.DPoint },
                new { Type = typeof(ChipDetails),                          ElementType = ElementTypes.ChipDetails  },
                new { Type = typeof(StdString),                            ElementType = ElementTypes.StdString  },
                new { Type = typeof(FullObjectDetection),                  ElementType = ElementTypes.FullObjectDetection  },
                new { Type = typeof(RectDetection),                        ElementType = ElementTypes.RectDetection  },
#if !LITE
                new { Type = typeof(ImageWindow.OverlayLine),              ElementType = ElementTypes.ImageWindowOverlayLine  },
                new { Type = typeof(PerspectiveWindow.OverlayDot),         ElementType = ElementTypes.PerspectiveWindowOverlayDot  },
#endif
                new { Type = typeof(MModRect),                             ElementType = ElementTypes.MModRect  },
#if !LITE
                new { Type = typeof(SurfPoint),                            ElementType = ElementTypes.SurfPoint  },
                new { Type = typeof(SamplePair),                           ElementType = ElementTypes.SamplePair  },
                new { Type = typeof(ImageDatasetMetadata.Image),           ElementType = ElementTypes.ImageDatasetMetadataImage },
                new { Type = typeof(ImageDatasetMetadata.Box),             ElementType = ElementTypes.ImageDatasetMetadataBox },
                new { Type = typeof(Vector<double>),                       ElementType = ElementTypes.VectorDouble       },
#endif
                new { Type = typeof(StdVector<double>),                    ElementType = ElementTypes.StdVectorDouble },
                new { Type = typeof(StdVector<Rectangle>),                 ElementType = ElementTypes.StdVectorRectangle },
                new { Type = typeof(StdVector<MModRect>),                  ElementType = ElementTypes.StdVectorMModRect  },
                new { Type = typeof(StdVector<FullObjectDetection>),       ElementType = ElementTypes.StdVectorFullObjectDetection  },
                new { Type = typeof(MModOptions.DetectorWindowDetails),    ElementType = ElementTypes.DetectorWindowDetails  },
#if !LITE
                new { Type = typeof(ImageDisplay.OverlayRect),             ElementType = ElementTypes.OverlayRect  }
#endif
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Int32,

            UInt32,

            Long,

            Float,

            Double,

            Rectangle,

            Point,

            DPoint,

            PerspectiveWindowOverlayDot,

            ImageWindowOverlayLine,

            FullObjectDetection,

            RectDetection,

            ChipDetails,

            StdString,

            Matrix,

            MModRect,

            SurfPoint,

            SamplePair,

            VectorDouble,

            StdVectorDouble,

            StdVectorRectangle,

            StdVectorMModRect,

            StdVectorFullObjectDetection,

            ImageDatasetMetadataImage,

            ImageDatasetMetadataBox,

            DetectorWindowDetails,

            OverlayRect

        }

    }

}
