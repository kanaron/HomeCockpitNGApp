using HomeCockpitNG.Models;
using HomeCockpitNG.Views;

namespace HomeCockpitNG.Presenters
{
    public class OverviewPresenter
    {
        public OverViewView OverViewView { get; set; }
        public OvervievModel OvervievModel { get; set; }

        public OverviewPresenter()
        {
            OverViewView = new();
            OvervievModel = new();

            OverViewView.CheckboxChecked += OverViewView_CheckboxChecked;

            OverViewView.DataContext = OvervievModel;
        }

        private void OverViewView_CheckboxChecked(object? sender, bool e)
        {
            OvervievModel.IsOfflineMode = e;
        }
    }
}
