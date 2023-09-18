using HomeCockpitNG.Views;
using HomeCockpitNG.Models;

namespace HomeCockpitNG.Presenters
{
    public class DataViewPresenter
    {
        public DataViewModel? DataViewModel { get; set; }
        public DataViewView? DataViewView { get; set; }

        public DataViewPresenter(MODULES module, string name)
        {
            DataViewView = new();
            DataViewModel = new(module, name);

            DataViewView.DataContext = DataViewModel;
        }
    }
}
