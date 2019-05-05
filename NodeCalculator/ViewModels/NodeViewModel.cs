using NodeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using GongSolutions.Wpf.DragDrop;

namespace NodeCalculator.ViewModels
{
    class NodeViewModel : ViewModelBase
    {
        public ReactiveProperty<double> PositionX { get; }
        public ReactiveProperty<double> PositionY { get; }

        public ReactiveProperty<string> Name { get; }



        private NodeBase InnerModel;

        public NodeViewModel(NodeBase NodeModel)
        {
            InnerModel = NodeModel;

            PositionX = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionX).AddTo(container);
            PositionY = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionY).AddTo(container);
            Name = InnerModel.ToReactivePropertyAsSynchronized(x => x.Name).AddTo(container);
        }


    }
}
