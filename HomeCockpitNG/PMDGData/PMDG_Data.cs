using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCockpitNG
{
    public enum MODULES
    {
        AFT_OVERHEAD,
        FORWARD_OVERHEAD,
        GLARESHIELD,
        FORWARD_PANEL,
        LOWER_FORWARD_PANEL,
        CONTROL_PANEL,
        FMS,
        GENERAL_AND_MISC,
        CDU
    }

    /// <summary>
    /// missing:
    /// nav
    /// trims
    /// comms
    /// adf
    /// 
    /// </summary>

    public class PMDG_Data
    {
        public int ID { get; set; }
        public int IsData { get; set; }
        public string? Name { get; set; }
        public MODULES Module { get; set; }
        public string? Value { get; set; }
        public int HW_Module { get; set; }
        public int HW_ID { get; set; }
    }
}
