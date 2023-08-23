using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HomeCockpitNG.Models
{
    public class OvervievModel : BaseModel
    {
        private string? _stateText;
        private Brush? _stateColor;
        private bool _isOfflineMode;

        public string StateText
        {
            get { return _stateText!; }
            set
            {
                _stateText = value;
                NotifyPropertyChanged();
            }
        }
        public Brush StateColor
        {
            get { return _stateColor!; }
            set
            {
                _stateColor = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsOfflineMode
        {
            get { return _isOfflineMode; }
            set
            {
                _isOfflineMode = value;
                NotifyPropertyChanged();
            }
        }

        public OvervievModel()
        {
            SimCon.GetSimCon().StateChanged += OvervievModel_StateChanged;
            StateText = "Sim not found";
            StateColor = Brushes.Red;
            IsOfflineMode = true;
        }

        private void OvervievModel_StateChanged(object? sender, string e)
        {
            StateText = e;
            switch (e)
            {
                case "Sim not found":
                    {
                        StateColor = Brushes.Red;
                        StartStopEnabled = false;
                        ResetEnabled = false;
                        PresetListEnabled = true;
                        break;
                    }
                case "Sim connected":
                    {
                        StateColor = Brushes.Yellow;
                        StartStopEnabled = true;
                        ResetEnabled = true;
                        StartStopText = "Start";
                        PresetListEnabled = true;
                        break;
                    }
            }
        }
    }
}
