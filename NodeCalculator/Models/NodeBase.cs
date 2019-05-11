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

        public ObservableCollection<NodeConnectModel> PrevNodes { get; } = new ObservableCollection<NodeConnectModel>();
        public ObservableCollection<NodeConnectModel> NextNodes { get; } = new ObservableCollection<NodeConnectModel>();

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

            PrevNodes.CollectionChanged += (s, e) => Do(null);
            NextNodes.CollectionChanged += (s, e) => Do(null);
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

            for (int i = 0; i < PrevNodes.Count; ++i)
            {
                double? prevNodeResult = null;

                var prevNode = PrevNodes[i];

                if (prevNode.ConnectNode != null)
                {
                    prevNodeResult = prevNode.ConnectNode.Result;
                    if (prevNodeResult == null)
                    { 
                        prevNodeResult = prevNode.ConnectNode.Do(this);
                    }
                }

                results.Add(prevNodeResult);
            }

            Result = Culculate(results);

            if (Caller == null)
            {
                foreach(var nextNode in NextNodes)
                {
                    nextNode.ConnectNode?.Do(null);
                }

            }

            return Result;
        }

        protected abstract double? Culculate(List<double?> PrevResults);

        public void ChangeConnectNodeNum(ObservableCollection<NodeConnectModel> Nodes, int NewNodeNum)
        {
            if (NewNodeNum < 0) return;

            if (Nodes.Count > NewNodeNum)
            {
                while (Nodes.Count != NewNodeNum)
                {
                    Nodes.RemoveAt(Nodes.Count - 1);
                }
            }
            else
            {
                while (Nodes.Count != NewNodeNum)
                {
                    Nodes.Add(new NodeConnectModel()
                    {
                        ConnectIndex = Nodes.Count
                    });
                }
            }
        }
    }
}
