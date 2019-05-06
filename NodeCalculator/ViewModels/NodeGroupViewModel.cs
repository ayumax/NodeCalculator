using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using NodeCalculator.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace NodeCalculator.ViewModels
{
    class NodeGroupViewModel : ViewModelBase, IDropTarget
    {
        public ReadOnlyReactiveCollection<NodeViewModel> Nodes { get; }

        public NodeGroupViewModel(MainModel mainModel)
        {
            Nodes = mainModel.Nodes.ToReadOnlyReactiveCollection(x => new NodeViewModel(x));
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var node in Nodes)
            {
                node.Dispose();
            }
        }

        private NodeViewModel? nowDragItem = null;
        private Size dragDiff;

        public void DragOver(IDropInfo dropInfo)
        {
            switch(dropInfo.Data)
            {
                case NodeViewModel node:
                    {
                        if (nowDragItem == null)
                        {
                            // ドラッグ開始時にノードとクリック位置の差を求める
                            dragDiff.Width = dropInfo.DropPosition.X - node.PositionX.Value;
                            dragDiff.Height = dropInfo.DropPosition.Y - node.PositionY.Value;
                        }

                        nowDragItem = node;

                        dropInfo.Effects = DragDropEffects.Move;

                        // ノードを移動
                        node.PositionX.Value = dropInfo.DropPosition.X - dragDiff.Width;
                        node.PositionY.Value = dropInfo.DropPosition.Y - dragDiff.Height;
                    }
                    break;

                case NodeConnectionViewModel connection:
                    {
                        dropInfo.Effects = DragDropEffects.Move;

                        connection.LineToX.Value = dropInfo.DropPosition.X;
                        connection.LineToY.Value = dropInfo.DropPosition.Y;
                        connection.Visible.Value = Visibility.Visible;
                    }
                    break;

                default:
                    nowDragItem = null;
                    break;
            }
            
        }

        public void Drop(IDropInfo dropInfo)
        {
            switch (dropInfo.Data)
            {
                case NodeViewModel node:
                    {
                        nowDragItem = null;
                    }
                    break;

                case NodeConnectionViewModel connection:
                    {
                        connection.LineToX.Value = 0;
                        connection.LineToY.Value = 0;
                        connection.Visible.Value = Visibility.Hidden;
                    }
                    break;

                default:
                    nowDragItem = null;
                    break;
            }
        }
    }
}
