using NodeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.ViewModels
{
    class ToolBoxViewModel
    {
        public ToolBoxItemViewModel Constant { get; }
        public ToolBoxItemViewModel Plus { get; }
        public ToolBoxItemViewModel Minus { get; }
        public ToolBoxItemViewModel Result { get; }

        public ToolBoxViewModel()
        {
            Constant = new ToolBoxItemViewModel(typeof(ConstantNode));
            Plus = new ToolBoxItemViewModel(typeof(PlusNode));
            Minus = new ToolBoxItemViewModel(typeof(MinusNode));
            Result = new ToolBoxItemViewModel(typeof(ResultNode));
        }

    }
}
