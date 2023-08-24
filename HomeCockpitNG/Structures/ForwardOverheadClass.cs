using pmdgSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCockpitNG.Structures
{
    public class ForwardOverheadClass
    {
        // Flight Controls              
        public byte[] FCTL_annunFC_LOW_PRESSURE = new byte[2];      // FLT CONTROL
        public byte FCTL_annunYAW_DAMPER;
        public byte FCTL_annunLOW_QUANTITY;
        public byte FCTL_annunLOW_PRESSURE;
        public byte FCTL_annunLOW_STBY_RUD_ON;
        public byte FCTL_annunFEEL_DIFF_PRESS;
        public byte FCTL_annunSPEED_TRIM_FAIL;
        public byte FCTL_annunMACH_TRIM_FAIL;
        public byte FCTL_annunAUTO_SLAT_FAIL;

        // Fuel
        public float FUEL_FuelTempNeedle;              // Value
        public byte[] FUEL_AuxFwd = new byte[2];                        // aux fwd A and aux fwd B ???????????
        public byte[] FUEL_AuxAft = new byte[2];                        //aux aft A and aux aft B ???????????????
        public byte FUEL_FWDBleed;
        public byte FUEL_AFTBleed;
        public byte FUEL_GNDXfr;   //???????????????????????
        public byte[] FUEL_annunENG_VALVE_CLOSED = new byte[2];        // 0: Closed  1: Open  2: In transit (bright)
        public byte[] FUEL_annunSPAR_VALVE_CLOSED = new byte[2];       // 0: Closed  1: Open  2: In transit (bright)
        public byte[] FUEL_annunFILTER_BYPASS = new byte[2];
        public byte FUEL_annunXFEED_VALVE_OPEN;                                                                // 0: Closed  1: Open  2: In transit (dim)
        public byte[] FUEL_annunLOWPRESS_Fwd = new byte[2];
        public byte[] FUEL_annunLOWPRESS_Aft = new byte[2];
        public byte[] FUEL_annunLOWPRESS_Ctr = new byte[2];
        public float FUEL_QtyCenter;                       // LBS   ????????
        public float FUEL_QtyLeft;                     // LBS   ????????????
        public float FUEL_QtyRight;                        // LBS   ???????????

        // Electrical
        public byte ELEC_annunBAT_DISCHARGE;
        public byte ELEC_annunTR_UNIT;
        public byte ELEC_annunELEC;
        public byte[] ELEC_annunDRIVE = new byte[2];
        public byte ELEC_annunSTANDBY_POWER_OFF;
        public byte ELEC_annunGRD_POWER_AVAILABLE;
        public byte[] ELEC_annunTRANSFER_BUS_OFF = new byte[2];
        public byte[] ELEC_annunSOURCE_OFF = new byte[2];
        public byte[] ELEC_annunGEN_BUS_OFF = new byte[2];
        public byte ELEC_annunAPU_GEN_OFF_BUS;
        public byte[] ELEC_MeterDisplayTop = new byte[13];          // Top line of the display: 3 groups of 4 digits (or symbols) + terminating zero 
        public byte[] ELEC_MeterDisplayBottom = new byte[13];       // Bottom line of the display
        public byte[] ELEC_BusPowered = new byte[16];               // True if the corresponding bus is powered: ??????????????????????????
                                                                    // DC HOT BATT			0	
                                                                    // DC HOT BATT SWITCHED	1	
                                                                    // DC BATT BUS			2	
                                                                    // DC STANDBY BUS		3	
                                                                    // DC BUS 1				4	
                                                                    // DC BUS 2				5	
                                                                    // DC GROUND SVC		6
                                                                    // AC TRANSFER 1		7
                                                                    // AC TRANSFER 2		8
                                                                    // AC GROUND SVC 1		9
                                                                    // AC GROUND SVC 2		10
                                                                    // AC MAIN 1			11
                                                                    // AC MAIN 2			12
                                                                    // AC GALLEY 1			13
                                                                    // AC GALLEY 2			14
                                                                    // AC STANDBY			15

        // APU
        public float APU_EGTNeedle;                        // Value
        public byte APU_annunMAINT;
        public byte APU_annunLOW_OIL_PRESSURE;
        public byte APU_annunFAULT;
        public byte APU_annunOVERSPEED;

        // Center overhead controls & indicators
        public byte AIR_annunEquipCoolingSupplyOFF;
        public byte AIR_annunEquipCoolingExhaustOFF;
        public byte LTS_annunEmerNOT_ARMED;
        public byte COMM_annunCALL;
        public byte COMM_annunPA_IN_USE;

        // Anti-ice
        public byte[] ICE_annunOVERHEAT = new byte[4];
        public byte[] ICE_annunON = new byte[4];
        public byte ICE_annunCAPT_PITOT;
        public byte ICE_annunL_ELEV_PITOT;
        public byte ICE_annunL_ALPHA_VANE;
        public byte ICE_annunL_TEMP_PROBE;
        public byte ICE_annunFO_PITOT;
        public byte ICE_annunR_ELEV_PITOT;
        public byte ICE_annunR_ALPHA_VANE;
        public byte ICE_annunAUX_PITOT;
        public byte[] ICE_annunVALVE_OPEN = new byte[2];
        public byte[] ICE_annunCOWL_ANTI_ICE = new byte[2];
        public byte[] ICE_annunCOWL_VALVE_OPEN = new byte[2];

        // Hydraulics
        public byte[] HYD_annunLOW_PRESS_eng = new byte[2];
        public byte[] HYD_annunLOW_PRESS_elec = new byte[2];
        public byte[] HYD_annunOVERHEAT_elec = new byte[2];

        // Air systems
        public byte[] AIR_annunZoneTemp = new byte[3];
        public byte AIR_annunDualBleed;
        public byte AIR_annunRamDoorL;
        public byte AIR_annunRamDoorR;
        public byte[] AIR_annunPackTripOff = new byte[2];
        public byte[] AIR_annunWingBodyOverheat = new byte[2];
        public byte[] AIR_annunBleedTripOff = new byte[2];
        public byte AIR_annunAUTO_FAIL;
        public byte AIR_annunOFFSCHED_DESCENT;
        public byte AIR_annunALTN;
        public byte AIR_annunMANUAL;
        public float[] AIR_DuctPress = new float[2];                 // PSI ?????????????????
        public float[] AIR_DuctPressNeedle = new float[2];               // Value - PSI
        public float AIR_CabinAltNeedle;                   // Value - ft ???????????
        public float AIR_CabinDPNeedle;                    // Value - PSI ?????????
        public float AIR_CabinVSNeedle;                    // Value - ft/min ?????????
        public float AIR_CabinValveNeedle;             // Value - 0 (closed) .. 1 (open) ?????????
        public float AIR_TemperatureNeedle;                // Value - degrees C   ???????
        public byte[] AIR_DisplayFltAlt = new byte[6];              // Pressurization system FLT ALT window, zero terminated, can be blank or show dashes or show test pattern
        public byte[] AIR_DisplayLandAlt = new byte[6];             // Pressurization system LAND ALT window, zero terminated, can be blank or show dashes or show test pattern

        // Doors
        public byte DOOR_annunFWD_ENTRY;
        public byte DOOR_annunFWD_SERVICE;
        public byte DOOR_annunAIRSTAIR;
        public byte DOOR_annunLEFT_FWD_OVERWING;
        public byte DOOR_annunRIGHT_FWD_OVERWING;
        public byte DOOR_annunFWD_CARGO;
        public byte DOOR_annunEQUIP;
        public byte DOOR_annunLEFT_AFT_OVERWING;
        public byte DOOR_annunRIGHT_AFT_OVERWING;
        public byte DOOR_annunAFT_CARGO;
        public byte DOOR_annunAFT_ENTRY;
        public byte DOOR_annunAFT_SERVICE;


        public void GetDataFromStruct(PMDG_SDK.PMDG_NG3_Data data)
        {
            FCTL_annunYAW_DAMPER = data.FCTL_annunYAW_DAMPER;
        }
    }
}
