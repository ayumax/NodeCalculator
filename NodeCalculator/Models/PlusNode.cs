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

            ChangeConnectNodeNum(PrevNodes, 2);
            ChangeConnectNodeNum(NextNodes, 1);
        }

        protected override double? Culculate(List<double?> PrevResults)
        {
            if (PrevResults[0] == null || PrevResults[1] == null) return null;

            return PrevResults[0] + PrevResults[1];
        }
    }
}
