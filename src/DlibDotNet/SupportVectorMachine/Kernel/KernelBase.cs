using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class KernelBase : DlibObject
    {

        #region Constructors

        protected KernelBase(SvmKernelType kernelType, int templateRow, int templateColumn, bool isEnabledDispose = true)
        {
            this.KernelType = kernelType;
            this.TemplateRows = templateRow;
            this.TemplateColumns = templateColumn;
        }

        #endregion

        #region Properties

        public SvmKernelType KernelType
        {
            get;
        }

        public MatrixElementTypes SampleType
        {
            get;
            protected set;
        }

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
        {
            get;
        }

        #endregion

    }

}