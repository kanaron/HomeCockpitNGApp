namespace HomeCockpitNG
{
    public enum CONTROL_MODULES
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

    public class PMDG_Control
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public MODULES Module { get; set; }
        public uint? Value { get; set; }
    }
}
