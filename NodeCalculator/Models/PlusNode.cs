using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            return PrevResults.Sum();
        }
    }
}
