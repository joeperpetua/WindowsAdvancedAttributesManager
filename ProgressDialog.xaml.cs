using System.Windows;

namespace AdvancedAttributesChanger
{
    public partial class ProgressDialog : Window
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        public void UpdateMessage(string title, string message)
        {
            Title.Text = title;
            Message.Text = message;
        }
    }
}
