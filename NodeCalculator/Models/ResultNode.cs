﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class ResultNode : NodeBase
    {
        public ResultNode()
        {
            Name = "Result";

            ChangeConnectNodeNum(PrevNodes, 1);
        }

        protected override double? Culculate(List<double?> PrevResults)
        {
            if (PrevResults.Count == 0) return null;

            return PrevResults[0];
        }
    }
}
