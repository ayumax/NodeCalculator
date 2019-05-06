using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media;

namespace NodeCalculator.ViewModels
{
    class NodeConnectionViewModel : IDropTarget
    {
        public ReactiveProperty<double> LineFromX { get; }
        public ReactiveProperty<double> LineFromY { get; }

        public ReactiveProperty<double> LineToX { get; }
        public ReactiveProperty<double> LineToY { get; }

        public ReactiveProperty<Visibility> Visible { get; }

        public SolidColorBrush Color { get; } = new SolidColorBrush(Colors.LightGray);

        public NodeViewModel Parent { get; private set; }

        protected Point LineFromOffset;


        public NodeConnectionViewModel(NodeViewModel Node)
        {
            Parent = Node;

            LineFromX = new ReactiveProperty<double>(0);
            LineFromY = new ReactiveProperty<double>(0);
            LineToX = new ReactiveProperty<double>(0);
            LineToY = new ReactiveProperty<double>(0);
            Visible = new ReactiveProperty<Visibility>(Visibility.Hidden);

            Node.PositionX.Subscribe(x => LineFromX.Value = LineFromOffset.X + x);
            Node.PositionY.Subscribe(x => LineFromY.Value = LineFromOffset.Y + x);
        }

        public virtual void DragOver(IDropInfo dropInfo)
        {
           
        }

        public virtual void Drop(IDropInfo dropInfo)
        {
        }
    }

    class NodeInConnectionViewModel : NodeConnectionViewModel
    {
        public NodeInConnectionViewModel(NodeViewModel Node)
            : base(Node)
        {
            LineFromOffset = new Point(50, 0);
        }

        public override void DragOver(IDropInfo dropInfo)
        {
            var connection = dropInfo.Data as NodeOutConnectionViewModel;
            if (connection == null) return;

            dropInfo.Effects = DragDropEffects.Move;
        }

        public override void Drop(IDropInfo dropInfo)
        {
            var connection = dropInfo.Data as NodeOutConnectionViewModel;
            if (connection == null) return;

            Parent.NextNode.Value = connection.Parent;

            Visible.Value = Visibility.Hidden;
            connection.Visible.Value = Visibility.Hidden;
        }
    }

    class NodeOutConnectionViewModel : NodeConnectionViewModel
    {
        public NodeOutConnectionViewModel(NodeViewModel Node)
           : base(Node)
        {
            LineFromOffset = new Point(50, 50);
        }

        public override void DragOver(IDropInfo dropInfo)
        {
            var connection = dropInfo.Data as NodeInConnectionViewModel;
            if (connection == null) return;

            dropInfo.Effects = DragDropEffects.Move;
        }

        public override void Drop(IDropInfo dropInfo)
        {
            var connection = dropInfo.Data as NodeInConnectionViewModel;
            if (connection == null) return;

            connection.Parent.NextNode.Value = Parent;

            Visible.Value = Visibility.Hidden;
            connection.Visible.Value = Visibility.Hidden;
        }
    }
}
