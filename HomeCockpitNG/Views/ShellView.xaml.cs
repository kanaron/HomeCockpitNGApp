using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HomeCockpitNG.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public event EventHandler? Overview_Clicked;
        public event EventHandler? Aft_Overhead_Clicked;
        public event EventHandler? Forward_Overhead_Clicked;
        public event EventHandler? Glareshield_Clicked;
        public event EventHandler? Forward_Panel_Clicked;
        public event EventHandler? Lower_Forward_Panel_Clicked;
        public event EventHandler? Control_Stand_Clicked;
        public event EventHandler? FMS_Clicked;
        public event EventHandler? General_Misc_Clicked;
        public event EventHandler? CDU_Clicked;

        public ShellView()
        {
            InitializeComponent();
        }

        private void Aft_Overhead_Click(object sender, RoutedEventArgs e)
        {
            Aft_Overhead_Clicked?.Invoke(this, e);
        }

        private void Forward_Overhead_Click(object sender, RoutedEventArgs e)
        {
            Forward_Overhead_Clicked?.Invoke(this, e);
        }

        private void Glareshield_Click(object sender, RoutedEventArgs e)
        {
            Glareshield_Clicked?.Invoke(this, e);
        }

        private void Forward_Panel_Click(object sender, RoutedEventArgs e)
        {
            Forward_Panel_Clicked?.Invoke(this, e);
        }

        private void Lower_Forward_Panel_Click(object sender, RoutedEventArgs e)
        {
            Lower_Forward_Panel_Clicked?.Invoke(this, e);
        }

        private void Control_Stand_Click(object sender, RoutedEventArgs e)
        {
            Control_Stand_Clicked?.Invoke(this, e);
        }

        private void FMS_Click(object sender, RoutedEventArgs e)
        {
            FMS_Clicked?.Invoke(this, e);
        }

        private void General_Misc_Click(object sender, RoutedEventArgs e)
        {
            General_Misc_Clicked?.Invoke(this, e);
        }

        private void CDU_Click(object sender, RoutedEventArgs e)
        {
            CDU_Clicked?.Invoke(this, e);
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            Overview_Clicked?.Invoke(this, e);
        }
    }
}
