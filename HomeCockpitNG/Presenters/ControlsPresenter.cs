using HomeCockpitNG.Views;
using HomeCockpitNG.Models;

namespace HomeCockpitNG.Presenters
{
    public class ControlsPresenter
    {
        public ControlsModel? ControlsModel { get; set; }
        public ControlsView? ControlsView { get; set; }

        public ControlsPresenter()
        {
            ControlsView = new();
            ControlsModel = new();

            ControlsView.DataContext = ControlsModel;

            ControlsView.ControlClicked += ControlsView_ControlClicked;
        }

        private void ControlsView_ControlClicked(object? sender, PMDG_Control e)
        {
            ControlsModel!.SendControl(e);
        }
    }
}
