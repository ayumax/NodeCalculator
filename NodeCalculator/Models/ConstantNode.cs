using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class ConstantNode : NodeBase
    {
        private double _InputValue = 0;
        public double InputValue
        {
            get => _InputValue;
            set { if (_InputValue == value) return;  _InputValue = value; RaizePropertyChanged(); }
        }

        public ConstantNode()
        {
            Name = "Constant";

            NextNodes = new NodeBase?[1];
        }

        protected override double? Culculate(List<double?> PrevResults)
        {
            return InputValue;
        }
    }
}
