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

            PrevNodes = new NodeBase?[1];
        }

        protected override double? Culculate(List<double?> PrevResults)
        {
            return PrevResults[0];
        }
    }
}
