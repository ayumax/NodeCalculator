﻿using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media;
using NodeCalculator.Models;
using NodeCalculator.ViewModels.Nodes;

namespace NodeCalculator.ViewModels
{
    class NodeConnectionViewModel : ViewModelBase, IDropTarget
    {
        public ReactiveProperty<double> LineFromX { get; }
        public ReactiveProperty<double> LineFromY { get; }

        public ReactiveProperty<double> LineToX { get; }
        public ReactiveProperty<double> LineToY { get; }

        public ReactiveProperty<string> BeziePathData { get; }

        public ReactiveProperty<Visibility> Visible { get; }

        public SolidColorBrush Color { get; } = new SolidColorBrush(Colors.LightGray);

        public NodeViewModel Parent { get; private set; }

        public NodeConnectModel InnerModel { get; private set; }

        public ReactiveProperty<NodeConnectionViewModel> ConnectNode { get; } = new ReactiveProperty<NodeConnectionViewModel>();

        public ReactiveProperty<int> ColumnIndex { get; }

        public NodeConnectionViewModel(NodeViewModel Node, NodeConnectModel nodeConnectModel)
        {
            Parent = Node;
            InnerModel = nodeConnectModel;

            LineFromX = new ReactiveProperty<double>(0);
            LineFromY = new ReactiveProperty<double>(0);
            LineToX = new ReactiveProperty<double>(0);
            LineToY = new ReactiveProperty<double>(0);
            Visible = new ReactiveProperty<Visibility>(Visibility.Hidden);

            ColumnIndex = new ReactiveProperty<int>(nodeConnectModel.ConnectIndex);

            BeziePathData = new ReactiveProperty<string>();
            Observable.Merge(LineFromX, LineFromY, LineToX, LineToY).Subscribe(x =>
            {
                BeziePathData.Value = $"M {LineFromX.Value},{LineFromY.Value} C {LineFromX.Value},{LineToY.Value} {LineToX.Value},{LineFromY.Value} {LineToX.Value},{LineToY.Value}";
            }).AddTo(container);

            Node.Width.Subscribe(x => UpdateLineFromX());
        }

        public virtual void DragOver(IDropInfo dropInfo)
        {
           
        }

        public virtual void Drop(IDropInfo dropInfo)
        {
        }

        protected virtual void UpdateLineFromX()
        {

        }
    }

    class NodeInConnectionViewModel : NodeConnectionViewModel
    {
        public NodeInConnectionViewModel(NodeViewModel Node, NodeConnectModel nodeConnectModel)
            : base(Node, nodeConnectModel)
        {
            ConnectNode.Subscribe(x => InnerModel.ConnectNode = x?.Parent.InnerModel);

            Node.PositionX.Subscribe(x => UpdateLineFromX());
            Node.PositionY.Subscribe(x => LineFromY.Value = 0 + x + 5);
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

            Visible.Value = Visibility.Hidden;
            connection.Visible.Value = Visibility.Hidden;

            if (this.Parent == connection.Parent) return;

            connection.ConnectNode.Value = this;
        }

        protected override void UpdateLineFromX()
        {
            double oneAreaWidth = Parent.Width.Value / Parent.InnerModel.PrevNodes.Count;
            LineFromX.Value = oneAreaWidth * InnerModel.ConnectIndex + oneAreaWidth / 2 + Parent.PositionX.Value;
        }
    }

    class NodeOutConnectionViewModel : NodeConnectionViewModel
    {
        public NodeOutConnectionViewModel(NodeViewModel Node, NodeConnectModel nodeConnectModel)
           : base(Node, nodeConnectModel)
        {
            ConnectNode.Subscribe(x => InnerModel.ConnectNode = x?.Parent.InnerModel);

            Node.PositionX.Subscribe(x => UpdateLineFromX());
            Node.PositionY.Subscribe(x => LineFromY.Value = Parent.Height.Value + x - 5);
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


            Visible.Value = Visibility.Hidden;
            connection.Visible.Value = Visibility.Hidden;

            if (this.Parent == connection.Parent) return;

            ConnectNode.Value = connection;
        }

        protected override void UpdateLineFromX()
        {
            double oneAreaWidth = Parent.Width.Value / Parent.InnerModel.NextNodes.Count;
            LineFromX.Value = oneAreaWidth * InnerModel.ConnectIndex + oneAreaWidth / 2 + Parent.PositionX.Value;
        }
    }
}
