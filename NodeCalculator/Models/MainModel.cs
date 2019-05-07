using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NodeCalculator.Models
{
    class MainModel
    {
        public ObservableCollection<NodeBase> Nodes { get; private set; }

        public MainModel()
        {
            Nodes = new ObservableCollection<NodeBase>();  
        }

        public void Start()
        {
        }

        public void End()
        {

        }
    }
}
