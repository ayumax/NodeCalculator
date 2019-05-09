using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace NodeCalculator.Models
{
    abstract class NodeBase : BindableBase
    {
        private double _PositionX = 0;
        public double PositionX
        {
            get => _PositionX;
            set { if (_PositionX == value) return;  _PositionX = value; RaizePropertyChanged(); }
        }

        private double _PositionY = 0;
        public double PositionY
        {
            get => _PositionY;
            set { if (_PositionY == value) return;  _PositionY = value; RaizePropertyChanged(); }
        }

        private string _Name = "";
        public string Name
        {
            get => _Name;
            set { if (_Name == value) return; _Name = value; RaizePropertyChanged(); }
        }

        public NodeBase?[] PrevNodes { get; set; } = new NodeBase?[0];
        public NodeBase?[] NextNodes { get; set; } = new NodeBase?[0];

        private double? _Result = null;
        [XmlIgnore]
        public double? Result
        {
            get => _Result;
            protected set { if (_Result == value) return; _Result = value; RaizePropertyChanged(); }
        }

        public NodeBase()
        {
            this.PropertyChanged += NodeBase_PropertyChanged;
        }

        private void NodeBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Name))
            {
                Do(null);
            }
        }

        public virtual double? Do(NodeBase? Caller)
        {
            List<double?> results = new List<double?>();

            for (int i = 0; i < PrevNodes.Length; ++i)
            {
                double? prevNodeResult = null;

                var prevNode = PrevNodes[i];

                if (prevNode != null)
                {
                    prevNodeResult = prevNode.Result;
                    if (prevNodeResult == null)
                    { 
                        prevNodeResult = prevNode.Do(this);
                    }
                }

                results.Add(prevNodeResult);
            }

            Result = Culculate(results);

            if (Caller == null)
            {
                foreach(var nextNode in NextNodes)
                {
                    nextNode?.Do(null);
                }

            }

            return Result;
        }

        protected abstract double? Culculate(List<double?> PrevResults);

    }
}
