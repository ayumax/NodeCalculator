using NodeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace NodeCalculator.ViewModels.Nodes
{
    class ConstantNodeViewModel : NodeViewModel
    {
        public ReactiveProperty<double> InputValue { get; }

        public ConstantNodeViewModel(ConstantNode constantNode)
            :base(constantNode)
        {
            InputValue = constantNode.ToReactivePropertyAsSynchronized(x => x.InputValue).AddTo(container);
        }
    }
}
