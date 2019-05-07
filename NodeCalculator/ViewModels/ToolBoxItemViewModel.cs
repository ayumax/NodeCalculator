using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.ViewModels
{
    class ToolBoxItemViewModel
    {
        public Type NodeModelType { get; private set; }

        public ToolBoxItemViewModel(Type NodeModelType)
        {
            this.NodeModelType = NodeModelType;
        }
    }
}
