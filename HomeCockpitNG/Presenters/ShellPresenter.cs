using HomeCockpitNG.Views;
using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace HomeCockpitNG.Presenters
{
    public class ShellPresenter
    {
        public ShellView ShellView { get; set; }

        public ShellPresenter()
        {
            ShellView = new();

            ShellView.Overview_Clicked += ShellView_Overview_Clicked;
            ShellView.Aft_Overhead_Clicked += ShellView_Aft_Overhead_Clicked;
            ShellView.Forward_Overhead_Clicked += ShellView_Forward_Overhead_Clicked;
            ShellView.Glareshield_Clicked += ShellView_Glareshield_Clicked;
            ShellView.Forward_Panel_Clicked += ShellView_Forward_Panel_Clicked;
            ShellView.Lower_Forward_Panel_Clicked += ShellView_Lower_Forward_Panel_Clicked;
            ShellView.Control_Stand_Clicked += ShellView_Control_Stand_Clicked;
            ShellView.FMS_Clicked += ShellView_FMS_Clicked;
            ShellView.General_Misc_Clicked += ShellView_General_Misc_Clicked;
            ShellView.CDU_Clicked += ShellView_CDU_Clicked;

            ShellView.Show();


            SimCon.GetSimCon().SetHandle(new WindowInteropHelper(ShellView).Handle);

            HwndSource lHwndSource = HwndSource.FromHwnd(new WindowInteropHelper(ShellView).Handle);
            lHwndSource.AddHook(new HwndSourceHook(SimCon.GetSimCon().ProcessSimCon));
        }

        private void ShellView_Overview_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_CDU_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_General_Misc_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_FMS_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Control_Stand_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Lower_Forward_Panel_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Forward_Panel_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Glareshield_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Forward_Overhead_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_Aft_Overhead_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
