using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static DPoint InputTensorToOutputTensor(Net net, DPoint point)
        {
            if (net == null)
                throw new ArgumentNullException(nameof(net));

            net.ThrowIfDisposed();
            return net.InputTensorToOutputTensor(point);
        }

    }

}
