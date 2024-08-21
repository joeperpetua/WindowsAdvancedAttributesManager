using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
using static System.Net.WebRequestMethods;

namespace AdvancedAttributesChanger
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
            String? ver = Environment.GetEnvironmentVariable("ClickOnce_CurrentVersion");
            CurrentVersion.Text = ver == null ? "Version cannot be parsed" : $"Version: {ver}";
        }

        private void Gh_Profile_Click(object sender, RequestNavigateEventArgs e)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = e.Uri.ToString(),
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void Gh_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = "https://github.com/joeperpetua/AdvancedAttributesChanger",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        
    }
}
