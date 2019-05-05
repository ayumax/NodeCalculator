using NodeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace NodeCalculator.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private MainModel Model;

        public ReadOnlyReactiveCollection<NodeViewModel> Nodes { get; }

        public MainViewModel()
        {
            Model = new MainModel();

            Nodes = Model.Nodes.ToReadOnlyReactiveCollection(x => new NodeViewModel(x));
        }

        public override void Start()
        {
            base.Start();

            Model.Start();
        }

        public override void End()
        {
            base.End();

            Dispose();

            foreach(var node in Nodes)
            {
                node.Dispose();
            }

            Model.End();
        }
    }
}
