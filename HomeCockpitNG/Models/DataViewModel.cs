using System.Collections.Generic;
using System.Timers;

namespace HomeCockpitNG.Models
{
    public class DataViewModel : BaseModel
    {
        private readonly MODULES _module;
        private readonly Timer timer;

        private List<PMDG_Data>? _dataList;

        public List<PMDG_Data> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                NotifyPropertyChanged();
            }
        }

        private string? _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public DataViewModel(MODULES module, string name)
        {
            timer = new()
            {
                Interval = 1000,
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            _module = module;
            Name = name;
            
            DataList = SimCon.SimCon.GetSimCon().PmdgDataList.FindAll(x => x.Module == _module).FindAll(x => x.IsData == 1);
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            DataList = SimCon.SimCon.GetSimCon().PmdgDataList.FindAll(x => x.Module == _module).FindAll(x => x.IsData == 1);
        }
    }
}
