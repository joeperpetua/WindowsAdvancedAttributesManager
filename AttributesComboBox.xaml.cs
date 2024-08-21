using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsAdvancedAttributesManager
{
    /// <summary>
    /// Interaction logic for AttributesComboBox.xaml
    /// </summary>
    public partial class AttributesComboBox : UserControl
    {
        public AttributesComboBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(RoutedEventHandler), typeof(AttributesComboBox), new PropertyMetadata(null));

        public RoutedEventHandler Command
        {
            get { return (RoutedEventHandler)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Command?.Invoke(sender, new RoutedEventArgs());
        }
    }
}
