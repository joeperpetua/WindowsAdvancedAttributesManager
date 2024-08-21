using System.Reflection.PortableExecutable;
using System.Windows;

namespace AdvancedAttributesChanger
{
    public partial class ProgressDialog : Window
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        public void UpdateMessage(string header, string message, long items = 0, long start = 0, long seconds = 0, bool spin = true, bool allowClose = false)
        {
            Header.Text = header;
            Message.Text = message;
            if (items > 0) {
                long elapsed = seconds - start == 0 ? 1 : seconds - start;
                Items.Text = $"Processed items: {items} ({items / elapsed}/s)";
            }

            if (!spin)
            {
                Spinner.Visibility = Visibility.Hidden;
            }

            if (allowClose)
            {
                Close_Button.Visibility = Visibility.Visible;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
