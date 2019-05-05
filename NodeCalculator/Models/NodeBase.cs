using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class NodeBase : BindableBase
    {
        public double PositionX { get; set; } = 0;
        public double PositionY { get; set; } = 0;

        public string Name { get; set; } = "";

        public NodeBase? PrevNode { get; set; }
        public NodeBase? NextNode { get; set; }

        public virtual void Do()
        {

        }
    }
}
