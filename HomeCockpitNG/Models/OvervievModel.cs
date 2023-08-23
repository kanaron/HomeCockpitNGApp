using SimCon;
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
            SimCon.SimCon.GetSimCon().StateChanged += OvervievModel_StateChanged;
            StateText = "Sim not found";
            StateColor = Brushes.Red;
            IsOfflineMode = false;
        }

        private void OvervievModel_StateChanged(object? sender, string e)
        {
            StateText = e;
            switch (e)
            {
                case "Sim not found":
                    {
                        StateColor = Brushes.Red;
                        break;
                    }
                case "Sim connected":
                    {
                        StateColor = Brushes.Yellow;
                        break;
                    }
            }
        }
    }
}
