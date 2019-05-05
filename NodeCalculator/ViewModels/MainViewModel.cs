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
        public NodeGroupViewModel NodeGroup { get; }

        public MainViewModel()
        {
            Model = new MainModel();
            NodeGroup = new NodeGroupViewModel(Model);
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


            Model.End();
        }
    }
}
