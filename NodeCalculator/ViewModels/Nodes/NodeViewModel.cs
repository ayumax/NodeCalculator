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


        public NodeInConnectionViewModel[] In { get; }
        public NodeOutConnectionViewModel[] Out { get; }

        public NodeFixedConnectionViewModel[] FixedConnection { get; }

        public ReactiveProperty<double?> Result { get; }

        public NodeBase InnerModel { get; private set; }

        public NodeViewModel(NodeBase NodeModel)
        {
            InnerModel = NodeModel;

            PositionX = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionX).AddTo(container);
            PositionY = InnerModel.ToReactivePropertyAsSynchronized(x => x.PositionY).AddTo(container);
            Width = new ReactiveProperty<double>(100);
            Height = new ReactiveProperty<double>(50);

            Name = InnerModel.ToReactivePropertyAsSynchronized(x => x.Name).AddTo(container);

            In = InnerModel.PrevNodes.Select((x, i) => new NodeInConnectionViewModel(this, i)).ToArray();

            Out = InnerModel.NextNodes.Select((x, i) => new NodeOutConnectionViewModel(this, i)).ToArray();


            FixedConnection = InnerModel.NextNodes.Select((x, i) => new NodeFixedConnectionViewModel(this, i)).ToArray();

            Result = InnerModel.ToReactivePropertyAsSynchronized(x => x.Result).AddTo(container);
        }


    }
}
