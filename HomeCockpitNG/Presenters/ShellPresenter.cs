using HomeCockpitNG.Views;
using SimCon;
using System;
using System.Windows.Interop;

namespace HomeCockpitNG.Presenters
{
    public class ShellPresenter
    {
        public ShellView ShellView { get; set; }

        private readonly OverviewPresenter? _overviewPresenter;
        private DataViewPresenter? _dataViewPresenter;
        private ControlsPresenter? _controlsPresenter;

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
            ShellView.Controls_Clicked += ShellView_Controls_Clicked;

            _overviewPresenter = new();

            ShellView.ActiveItem.Content = _overviewPresenter.OverViewView;
            ShellView.Show();

            SimCon.SimCon.GetSimCon().SetHandle(new WindowInteropHelper(ShellView).Handle);

            HwndSource lHwndSource = HwndSource.FromHwnd(new WindowInteropHelper(ShellView).Handle);
            lHwndSource.AddHook(new HwndSourceHook(SimCon.SimCon.GetSimCon().ProcessSimCon));
        }

        private void ShellView_Controls_Clicked(object? sender, EventArgs e)
        {
            _controlsPresenter = null;
            _controlsPresenter = new();
            ShellView.ActiveItem.Content = _controlsPresenter!.ControlsView;
        }

        private void ShellView_Overview_Clicked(object? sender, EventArgs e)
        {
            ShellView.ActiveItem.Content = _overviewPresenter!.OverViewView;
        }

        private void ShellView_CDU_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShellView_General_Misc_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.GENERAL_AND_MISC, "General and Misc");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_FMS_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.FMS, "FMS");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Control_Stand_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.CONTROL_PANEL, "Control Stand");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Lower_Forward_Panel_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.LOWER_FORWARD_PANEL, "Lower Forward Panel");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Forward_Panel_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.FORWARD_PANEL, "Forward Panel");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Glareshield_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.GLARESHIELD, "Glareshield");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Forward_Overhead_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.FORWARD_OVERHEAD, "Forward Overhead");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }

        private void ShellView_Aft_Overhead_Clicked(object? sender, EventArgs e)
        {
            _dataViewPresenter = null;
            _dataViewPresenter = new(MODULES.AFT_OVERHEAD, "Aft Overhead");
            ShellView.ActiveItem.Content = _dataViewPresenter!.DataViewView;
        }
    }
}
