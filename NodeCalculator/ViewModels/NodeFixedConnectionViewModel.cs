using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using NodeCalculator.ViewModels.Nodes;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace NodeCalculator.ViewModels
{
    class NodeFixedConnectionViewModel
    {
        public ReactiveProperty<double> LineFromX { get; }
        public ReactiveProperty<double> LineFromY { get; }

        public ReactiveProperty<double> LineToX { get; }
        public ReactiveProperty<double> LineToY { get; }

        public ReactiveProperty<Visibility> Visible { get; }

        public SolidColorBrush Color { get; } = new SolidColorBrush(Colors.DarkGray);

        protected NodeViewModel Parent;
        private List<IDisposable> disposables = new List<IDisposable>();

        public NodeFixedConnectionViewModel(NodeViewModel Node, int Index)
        {
            Parent = Node;

            LineFromX = new ReactiveProperty<double>(0);
            LineFromY = new ReactiveProperty<double>(0);
            LineToX = new ReactiveProperty<double>(0);
            LineToY = new ReactiveProperty<double>(0);
            Visible = new ReactiveProperty<Visibility>(Visibility.Hidden);

            double oneAreaWidth = Node.Width.Value / Node.Out.Length;
            Node.PositionX.Subscribe(x => LineFromX.Value = oneAreaWidth * Index + oneAreaWidth / 2 + x);
            Node.PositionY.Subscribe(x => LineFromY.Value = Node.Height.Value + x);

            Node.Out[Index].ConnectNode.Subscribe(x => 
            {
                if (x == null)
                {
                    Visible.Value = Visibility.Hidden;

                    foreach(var disposable in disposables)
                    {
                        disposable.Dispose();
                    }
                }
                else
                {
                    Visible.Value = Visibility.Visible;

                    var connectNode = x.Parent;
                    double oneAreaWidth = connectNode.Width.Value / connectNode.In.Length;
                    connectNode.PositionX.Subscribe(pos => LineToX.Value = oneAreaWidth * x.Index + oneAreaWidth / 2 + pos).AddTo(disposables);
                    connectNode.PositionY.Subscribe(pos => LineToY.Value = 0 + pos).AddTo(disposables);
                }

                Node.InnerModel.NextNodes[Index] = x?.Parent.InnerModel;
                if (x != null)
                {
                    x.Parent.InnerModel.PrevNodes[x.Index] = Node.InnerModel;
                    x.Parent.InnerModel.Do(null);
                }
            });
        }
    }
}
