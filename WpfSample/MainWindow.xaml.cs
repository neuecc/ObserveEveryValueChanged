using System;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ObserveEveryValueChanged(x => x.Width).Subscribe(x => WidthText.Text = x.ToString());
            this.ObserveEveryValueChanged(x => x.Height).Subscribe(x => HeightText.Text = x.ToString());
        }
    }
}
