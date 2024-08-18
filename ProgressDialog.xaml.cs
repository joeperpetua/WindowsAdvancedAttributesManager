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

        public void UpdateMessage(string header, string message, long items, long start, long seconds)
        {
            Header.Text = header;
            Message.Text = message;
            if (items > 0) {
                long elapsed = seconds - start == 0 ? 1 : seconds - start;
                Items.Text = $"Processed items: {items.ToString()} ({items / elapsed}/s)";
            }
        }
    }
}
