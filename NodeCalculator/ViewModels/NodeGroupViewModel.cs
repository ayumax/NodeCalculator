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

        public void DragOver(IDropInfo dropInfo)
        {
            var node = dropInfo.Data as NodeViewModel;
            if (node == null) return;

            dropInfo.Effects = DragDropEffects.Move;

            node.PositionX.Value = dropInfo.DropPosition.X - 50;
            node.PositionY.Value = dropInfo.DropPosition.Y - 25;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var node = dropInfo.Data as NodeViewModel;

        }
    }
}
