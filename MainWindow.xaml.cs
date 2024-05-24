using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
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
            SingleAddCombo.Command = AddItem;
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e) {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dialog = new();

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                FilePathSelection.Text = dialog.FileName;
                RunFile.IsChecked = true;
            }

            e.Handled = true;
        }

        private void OpenFolderDialog(object sender, RoutedEventArgs e) {
            // Configure open folder dialog box
            Microsoft.Win32.OpenFolderDialog dialog = new();
            dialog.Multiselect = false;
            dialog.Title = "Select a folder";

            // Show open folder dialog box
            bool? result = dialog.ShowDialog();

            // Process open folder dialog box results
            if (result == true) {
                // Get the selected folder
                DirectoryPathSelection.Text = dialog.FolderName;
                RunDirectory.IsChecked = true;
            }

            e.Handled = true;
        }

        private void RenderAttributeViewerList(object sender, RoutedEventArgs e) {
            TextBox textBox = (TextBox)sender;
            if (textBox == null) { return; }
            Trace.WriteLine(textBox.Name);
            String textBoxPath = textBox.Text.ToString();

            if (textBox.Name.Equals("FilePathSelection") && textBoxPath != "") {
                if (File.Exists(textBoxPath) == false) { return; }

                StackPanel itemToAdd = CreateViewerItem(textBoxPath, GetAttributeNames(textBoxPath));
                AttributesPreviewList.Items.Clear();
                AttributesPreviewList.Items.Add(itemToAdd);
                DirectoryPathSelection.Text = "";
            }

            if (textBox.Name.Equals("DirectoryPathSelection") && textBoxPath != "") {
                if (Directory.Exists(textBoxPath) == false) { return; }
                String[] directoryChildren = Directory.GetFiles(textBoxPath);
                AttributesPreviewList.Items.Clear();
                foreach (String childrenPath in directoryChildren)
                {
                    StackPanel itemToAdd = CreateViewerItem(childrenPath, GetAttributeNames(childrenPath));
                    AttributesPreviewList.Items.Add(itemToAdd);
                }
                FilePathSelection.Text = "";
            }
        }

        static string GetAttributeNames(String filePath) {
            FileAttributes fileAttrib = File.GetAttributes(filePath);
            List<string> attributeNames = new List<string>();

            foreach (FileAttributes currentAttrib in Enum.GetValues(fileAttrib.GetType())) {
                if (fileAttrib.HasFlag(currentAttrib)) {
                    attributeNames.Add(currentAttrib.ToString());
                }
            }

            return string.Join(", ", attributeNames);
        }

        private static StackPanel CreateViewerItem(String filePath, String fileAttributes){
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, MinWidth = 290 };
            TextBlock path = new TextBlock { Text = filePath };
            TextBlock separator = new TextBlock { Text = "=", Margin = new Thickness(5,0,5,0)};
            TextBlock attributes = new TextBlock { Text = fileAttributes };

            stackPanel.Children.Add(path);
            stackPanel.Children.Add(separator);
            stackPanel.Children.Add(attributes);

            return stackPanel;
        }

        private DockPanel CreateItem(String text)
        {
            DockPanel dockPanel = new DockPanel { Width = 155, Margin = new Thickness(5) };

            // button is added first so that it takes the rightmost position.
            Button button = new Button
            {
                Content = "❌",
                VerticalAlignment = VerticalAlignment.Center,
                Width = 20,
                Cursor = Cursors.Hand,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };
            button.Click += RemoveItem;
            button.SetResourceReference(Control.TemplateProperty, "ListButtonTemplate");
            DockPanel.SetDock(button, Dock.Right);

            TextBlock textBlock = new TextBlock { Text = text, VerticalAlignment = VerticalAlignment.Center };
            DockPanel.SetDock(textBlock, Dock.Left);

            dockPanel.Children.Add(button);
            dockPanel.Children.Add(textBlock);

            return dockPanel;
        }

        private void AddItem(object sender, RoutedEventArgs e) {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (comboBox == null || selectedItem == null) { return; }

            // Get the parent of comboBox
            DependencyObject parent = VisualTreeHelper.GetParent(comboBox);
            while (parent != null && parent is not AttributesComboBox)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent == null) { return; }

            String selectedItemContent = (String)selectedItem.Content;
            AttributesComboBox parentAttributeCombo = (AttributesComboBox)parent;

            DockPanel itemToAdd = CreateItem(selectedItemContent);
            switch (parentAttributeCombo.Name) {
                case "SingleAddCombo":
                    if (IsInList(itemToAdd, SingleAddList)) { break; }
                    SingleAddList.Items.Add(itemToAdd);
                    break;
                case "SingleRemoveCombo":
                    if (IsInList(itemToAdd, SingleRemoveList)) { break; }
                    SingleRemoveList.Items.Add(itemToAdd);
                    break;
                case "FolderAddCombo":
                    if (IsInList(itemToAdd, FolderAddList)) { break; }
                    FolderAddList.Items.Add(itemToAdd);
                    break;
                case "FolderRemoveCombo":
                    if (IsInList(itemToAdd, FolderRemoveList)) { break; }
                    FolderRemoveList.Items.Add(itemToAdd);
                    break;
                default: break;
            }

            // Reset selection to default value, not needed anymore
            // comboBox.SelectedIndex = -1;
        }

        private void RemoveItem(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            if (button == null) { return; }

            // Get DockPanel ref from Button
            DependencyObject parentDockPanel = VisualTreeHelper.GetParent(button);
            while (parentDockPanel != null && parentDockPanel is not DockPanel) {
                parentDockPanel = VisualTreeHelper.GetParent(parentDockPanel);
            }
            if (parentDockPanel == null) { return; }
            DockPanel itemToDelete = (DockPanel)parentDockPanel;

            // Get ListBox ref from StackPanel
            DependencyObject parentList = VisualTreeHelper.GetParent(itemToDelete);
            while (parentList != null && parentList is not ItemsControl)
            {
                parentList = VisualTreeHelper.GetParent(parentList);
            }
            if (parentList == null) { return; }
            ItemsControl list = (ItemsControl)parentList;

            list.Items.Remove(itemToDelete);

            e.Handled = true;
        }

        private static bool IsInList(DockPanel toFind, ItemsControl list) {
            bool alreadyInList = false;

            foreach (DockPanel item in list.Items)
            {
                TextBlock toFindText = (TextBlock)toFind.Children[1];
                TextBlock currentListItem = (TextBlock)item.Children[1];
                if (toFindText.Text.Equals(currentListItem.Text)) { alreadyInList = true; }
            }

            return alreadyInList;
        }
    }
}