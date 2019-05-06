using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
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

        public SolidColorBrush Color { get; } = new SolidColorBrush(Colors.Red);

        protected NodeViewModel Parent;
        private List<IDisposable> disposables = new List<IDisposable>();

        public NodeFixedConnectionViewModel(NodeViewModel Node)
        {
            Parent = Node;

            LineFromX = new ReactiveProperty<double>(0);
            LineFromY = new ReactiveProperty<double>(0);
            LineToX = new ReactiveProperty<double>(0);
            LineToY = new ReactiveProperty<double>(0);
            Visible = new ReactiveProperty<Visibility>(Visibility.Hidden);

            Node.PositionX.Subscribe(x => LineFromX.Value = 50 + x);
            Node.PositionY.Subscribe(x => LineFromY.Value = 0 + x);

            Node.NextNode.Subscribe(x => 
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

                    x.PositionX.Subscribe(x => LineToX.Value = 50 + x).AddTo(disposables);
                    x.PositionY.Subscribe(x => LineToY.Value = 50 + x).AddTo(disposables);
                }
            });
        }
    }
}
