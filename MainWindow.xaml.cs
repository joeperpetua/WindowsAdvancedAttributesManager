using System.Diagnostics;
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

namespace AdvancedAttributesChanger
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TogglePanel(object sender, RoutedEventArgs e) {
            RadioButton selected = (RadioButton)sender;
            
            if (selected == null) { return; }

            if (selected.Name.Equals("RunFile")) {
                DirectoryPanel.Visibility = Visibility.Collapsed;
                SingleFilePanel.Visibility = Visibility.Visible;
            } else {
                DirectoryPanel.Visibility = Visibility.Visible;
                SingleFilePanel.Visibility = Visibility.Collapsed;
            }

            e.Handled = true;
        }
    }
}