using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class ResultNode : NodeBase
    {
        public ResultNode()
        {
            Name = "Result";

            PrevNodes = new NodeBase[1];
            NextNodes = new NodeBase[0];
        }

        public override void Do()
        {
            base.Do();


        }
    }
}
