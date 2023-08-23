using HomeCockpitNG.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCockpitNG.Presenters
{
    public class OverviewPresenter
    {
        public OverViewView overViewView { get; set; }



        public OverviewPresenter()
        {
            overViewView = new();
        }
    }
}
