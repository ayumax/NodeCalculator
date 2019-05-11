using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NodeCalculator.Models
{
    class MinusNode : NodeBase
    {
        public MinusNode()
        {
            Name = "MinusNode";

            ChangeConnectNodeNum(PrevNodes, 2);
            ChangeConnectNodeNum(NextNodes, 1);
        }

        protected override double? Culculate(List<double?> PrevResults)
        {
            if (PrevResults.Count == 0) return null;

            double resultValue = PrevResults.First() ?? 0;

            for (int i = 1; i < PrevResults.Count; ++i)
            {
                resultValue -= PrevResults[i] ?? 0;
            }

            return resultValue;
        }
    }
   
}
