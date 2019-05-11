using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.Models
{
    class NodeConnectModel : BindableBase
    {
        private NodeBase? _ConnectNode = null;
        public NodeBase? ConnectNode
        {
            get => _ConnectNode;
            set { if (_ConnectNode == value) return; _ConnectNode = value; RaizePropertyChanged(); }
        }

        private int _ConnectIndex = 0;
        public int ConnectIndex
        {
            get => _ConnectIndex;
            set { if (_ConnectIndex == value) return; _ConnectIndex = value; RaizePropertyChanged(); }
        }
    }
}
