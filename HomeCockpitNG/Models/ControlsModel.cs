using pmdgSDK;
using System;
using System.Collections.Generic;
using System.Timers;

namespace HomeCockpitNG.Models
{
    public class ControlsModel : BaseModel
    {
        private List<PMDG_Control>? _controlsList;

        public List<PMDG_Control> ControlsList
        {
            get { return _controlsList; }
            set
            {
                _controlsList = value;
                NotifyPropertyChanged();
            }
        }

        public ControlsModel()
        {
            ControlsList = SQLPmdgControls.LoadControls();
        }

        public void SendControl(PMDG_Control control)
        {
            if (control.Value != null)
                SimCon.SimCon.GetSimCon().SetPMDGEvent((PMDG_SDK.PMDGEvents)Enum.Parse(typeof(PMDG_SDK.PMDGEvents), control.Name!), (uint)control.Value!);
        }
    }
}
