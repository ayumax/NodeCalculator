using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeCalculator.Views.NodeParts
{
    /// <summary>
    /// NodeConnector.xaml の相互作用ロジック
    /// </summary>
    public partial class NodeConnector : UserControl
    {
        public static readonly DependencyProperty ConnctColorProperty =
    DependencyProperty.Register(
                                "ConnctColor", 
                                typeof(SolidColorBrush),
                                typeof(NodeConnector),
                                new PropertyMetadata(
                                new SolidColorBrush(Colors.DarkBlue),
                                ConnctColorPropertyChanged));
        public SolidColorBrush ConnctColor
        {
            get { return (SolidColorBrush)GetValue(ConnctColorProperty); }
            set { SetValue(ConnctColorProperty, value); }
        }


        private static void ConnctColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisInstance = d as NodeConnector;
            if (thisInstance != null)
            {
                thisInstance.ConnectBorder.Background = e.NewValue as SolidColorBrush;
            }
        }

        public NodeConnector()
        {
            InitializeComponent();
        }
    }
}
