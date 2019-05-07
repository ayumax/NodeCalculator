using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class PlusNode : NodeBase
    {
        public PlusNode()
        {
            Name = "PlusNode";

            PrevNodes = new NodeBase[2];
            NextNodes = new NodeBase[1];
        }

        public override void Do()
        {
            base.Do();


        }
    }
}
