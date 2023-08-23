using System;
using System.Windows;
using System.Windows.Controls;

namespace HomeCockpitNG.Views
{
    /// <summary>
    /// Interaction logic for OverViewView.xaml
    /// </summary>
    public partial class OverViewView : UserControl
    {
        public event EventHandler<bool>? CheckboxChecked;

        public OverViewView()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckboxChecked?.Invoke(this, (bool)OfflineModeCheckbox.IsChecked!);
        }
    }
}
