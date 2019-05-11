using NodeCalculator.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using GongSolutions.Wpf.DragDrop;

namespace NodeCalculator.ViewModels.Nodes
{
    class NodeViewModel : ViewModelBase
    {
        public ReactiveProperty<double> PositionX { get; }
        public ReactiveProperty<double> PositionY { get; }
        public ReactiveProperty<double> Width { get; }
        public ReactiveProperty<double> Height { get; }

        public ReactiveProperty<string> Name { get; }


        public ReadOnlyReactiveCollection<NodeInConnectionViewModel> In { get; }
        public ReadOnlyReactiveCollection<NodeOutConnectionViewModel> Out { get; }

        public ReadOnlyReactiveCollection<NodeFixedConnectionViewModel> FixedConnection { get; }

        public ReactiveProperty<double?> Result { get; }

        public ReactiveProperty<bool> IsInputOpen { get; }

        public NodeBase InnerModel { get; private set; }

        public NodeViewModel(NodeBase NodeModel)
        {
            InnerModel = NodeModel;

            PositionX = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionX).AddTo(container);
            PositionY = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionY).AddTo(container);
            Width = new ReactiveProperty<double>(100);
            Height = new ReactiveProperty<double>(60);

            Name = InnerModel.ToReactivePropertyAsSynchronized(x => x.Name).AddTo(container);

            In = InnerModel.PrevNodes.ToReadOnlyReactiveCollection(x => new NodeInConnectionViewModel(this, x));

            Out = InnerModel.NextNodes.ToReadOnlyReactiveCollection(x => new NodeOutConnectionViewModel(this, x));


            FixedConnection = InnerModel.NextNodes.ToReadOnlyReactiveCollection(x => new NodeFixedConnectionViewModel(this, x));

            Result = InnerModel.ToReactivePropertyAsSynchronized(x => x.Result).AddTo(container);

            IsInputOpen = new ReactiveProperty<bool>(false);
        }


    }
}
