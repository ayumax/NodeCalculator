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
            Nodes.Add(new ConstantNode() { PositionX = 10, PositionY = 100 });
            Nodes.Add(new ConstantNode() { PositionX = 30, PositionY = 200 });
            Nodes.Add(new ConstantNode() { PositionX = 200, PositionY = 300 });
        }

        public void End()
        {

        }
    }
}
