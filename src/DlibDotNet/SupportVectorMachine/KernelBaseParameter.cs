// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed class KernelBaseParameter : IParameter
    {

        #region Constructors

        public KernelBaseParameter(KernelBase kernelBase):
            this(kernelBase.KernelType, kernelBase.SampleType, kernelBase.TemplateRows, kernelBase.TemplateColumns)
        {
        }

        public KernelBaseParameter(KernelType kernelType, MatrixElementTypes sampleType, int templateRows, int templateColumns)
        {
            this.KernelType = kernelType;
            this.SampleType = sampleType;
            this.TemplateRows = templateRows;
            this.TemplateColumns = templateColumns;
        }

        #endregion

        #region Properties

        public KernelType KernelType
        {
            get;
        }

        public MatrixElementTypes SampleType
        {
            get;
        }

        public int TemplateRows
        {
            get;
        }

        public int TemplateColumns
        {
            get;
        }

        #endregion

    }

}