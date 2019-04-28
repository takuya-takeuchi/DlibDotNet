using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class KernelBase : DlibObject
    {

        #region Constructors

        protected KernelBase(KernelType kernelType, int templateRow, int templateColumn)
        {
            this.KernelType = kernelType;
            this.TemplateRows = templateRow;
            this.TemplateColumns = templateColumn;
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