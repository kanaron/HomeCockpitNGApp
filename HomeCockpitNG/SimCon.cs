using Microsoft.FlightSimulator.SimConnect;
using pmdgTests;
using System;
using System.Runtime.InteropServices;

namespace SimConModels
{
    public enum DEFINITION
    {
        Dummy = 0,
        PMDG_NG3_DATA_DEFINITION = 0x4E473332,
        PMDG_NG3_CONTROL_DEFINITION = 0x4E473334,
        PMDG_NG3_CDU_0_DEFINITION = 0x4E473338
    };

    public enum REQUEST
    {
        Dummy = 0,
    };

    public enum DATA_ID
    {
        PMDG_NG3_DATA_ID = 0x4E473331,
        PMDG_NG3_CONTROL_ID = 0x4E473333,
        PMDG_NG3_CDU_0_ID = 0x4E473335
    };

    public enum DATA_REQUEST_ID
    {
        DATA_REQUEST = 100,
        CONTROL_REQUEST,
        CDU_REQUEST
    }

    /*public enum EVENT
    {
        Dummy, KEY_TOGGLE_VACUUM_FAILURE, KEY_TOGGLE_ENGINE1_FAILURE, KEY_TOGGLE_ENGINE2_FAILURE, KEY_TOGGLE_ENGINE3_FAILURE, KEY_TOGGLE_ENGINE4_FAILURE,
        KEY_TOGGLE_ELECTRICAL_FAILURE, KEY_TOGGLE_PITOT_BLOCKAGE, KEY_TOGGLE_STATIC_PORT_BLOCKAGE, KEY_TOGGLE_HYDRAULIC_FAILURE,
        KEY_TOGGLE_TOTAL_BRAKE_FAILURE, KEY_TOGGLE_LEFT_BRAKE_FAILURE, KEY_TOGGLE_RIGHT_BRAKE_FAILURE
    };*/

    public class SimCon
    {
        private static readonly SimCon instance = new();

        public event EventHandler<string>? SimException;

        public bool Connected { get; private set; } = false;

        private string state = "Sim not found";
        public event EventHandler<string>? StateChanged;

        public const int WM_USER_SIMCONNECT = 0x0402;
        public IntPtr MHWnd { get; set; }
        SimConnect? simconnect = null;

        PMDG_SDK.PMDG_NG3_Data pmdgData;
        PMDG_SDK.PMDG_NG3_Control pmdgControl;
        PMDG_SDK.PMDG_NGX_CDU_Screen pmdgCDU;


        private SimCon()
        {

        }

        public void SetHandle(IntPtr _ptr)
        {
            MHWnd = _ptr;
        }

        public IntPtr ProcessSimCon(IntPtr _hwnd, int msg, IntPtr _wParam, IntPtr _lParam, ref bool handled)
        {
            handled = false;

            if (msg == 0x0402)
            {
                if (simconnect != null)
                {
                    simconnect.ReceiveMessage();
                    handled = true;
                }
            }
            return (IntPtr)0;
        }

        /*public void RegisterList(List<SimVarModel> list)
        {
            foreach (SimVarModel simVarModel in list)
            {
                if (simVarModel.IsEvent == false)
                {
                    simconnect!.AddToDataDefinition(simVarModel.eDef, simVarModel.SimVariable, simVarModel.Unit, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                    simconnect!.RegisterDataDefineStruct<double>(simVarModel.eDef);
                }
            }
        }*/

        public bool Connect()
        {
            Console.WriteLine("Connect");
            if (simconnect == null)
                try
                {
                    simconnect = new SimConnect("RandFailuresFS2020", MHWnd, WM_USER_SIMCONNECT, null, 0);

                    simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                    simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);
                    simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);
                    simconnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(SimConnect_OnRecvEvent);
                    simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
                    simconnect.OnRecvClientData += new SimConnect.RecvClientDataEventHandler(SimConnect_OnRecvClientData);

                    simconnect.MapClientDataNameToID(PMDG_SDK.PMDG_NG3_DATA_NAME, DATA_ID.PMDG_NG3_DATA_ID);
                    simconnect.MapClientDataNameToID(PMDG_SDK.PMDG_NG3_CONTROL_NAME, DATA_ID.PMDG_NG3_CONTROL_ID);
                    simconnect.MapClientDataNameToID(PMDG_SDK.PMDG_NG3_CDU_0_NAME, DATA_ID.PMDG_NG3_CDU_0_ID);

                    simconnect.AddToClientDataDefinition(DEFINITION.PMDG_NG3_DATA_DEFINITION, 0, (uint)Marshal.SizeOf(typeof(PMDG_SDK.PMDG_NG3_Data)), 0, SimConnect.SIMCONNECT_UNUSED);
                    simconnect.AddToClientDataDefinition(DEFINITION.PMDG_NG3_CONTROL_DEFINITION, 0, (uint)Marshal.SizeOf(typeof(PMDG_SDK.PMDG_NG3_Control)), 0, SimConnect.SIMCONNECT_UNUSED);
                    simconnect.AddToClientDataDefinition(DEFINITION.PMDG_NG3_CDU_0_DEFINITION, 0, (uint)Marshal.SizeOf(typeof(PMDG_SDK.PMDG_NGX_CDU_Screen)), 0, SimConnect.SIMCONNECT_UNUSED);

                    simconnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, PMDG_SDK.PMDG_NG3_Data>(DEFINITION.PMDG_NG3_DATA_DEFINITION);
                    simconnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, PMDG_SDK.PMDG_NG3_Control>(DEFINITION.PMDG_NG3_CONTROL_DEFINITION);
                    simconnect.RegisterStruct<SIMCONNECT_RECV_CLIENT_DATA, PMDG_SDK.PMDG_NGX_CDU_Screen>(DEFINITION.PMDG_NG3_CDU_0_DEFINITION);

                    simconnect.RequestClientData(DATA_ID.PMDG_NG3_DATA_ID, DATA_REQUEST_ID.DATA_REQUEST, DEFINITION.PMDG_NG3_DATA_DEFINITION,
                        SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
                    simconnect.RequestClientData(DATA_ID.PMDG_NG3_CONTROL_ID, DATA_REQUEST_ID.CONTROL_REQUEST, DEFINITION.PMDG_NG3_CONTROL_DEFINITION,
                        SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
                    simconnect.RequestClientData(DATA_ID.PMDG_NG3_CDU_0_ID, DATA_REQUEST_ID.CDU_REQUEST, DEFINITION.PMDG_NG3_CDU_0_DEFINITION,
                        SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
                }
                catch (COMException ex)
                {
                    Console.WriteLine("Connection to FS failed: " + ex.Message);
                }

            return Connected;
        }

        /*public void UpdateData()
        {
            //simconnect!.RequestClientData(DATA_ID.PMDG_NG3_CDU_0_ID, DATA_REQUEST_ID.CDU_REQUEST, DEFINITION.PMDG_NG3_CDU_0_DEFINITION, SIMCONNECT_CLIENT_DATA_PERIOD.ON_SET, SIMCONNECT_CLIENT_DATA_REQUEST_FLAG.CHANGED, 0, 0, 0);
            //simconnect.RequestDataOnSimObjectType(DATA_REQUEST_ID.CDU_REQUEST, DEFINITION.PMDG_NG3_CDU_0_DEFINITION, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }*/

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
            }

            //SimConHelper.GetSimConHelper().SimConnectClosed();

            Connected = false;
        }

        /// <summary>
        /// Sends request to update every element on list
        /// </summary>
        /// <param name="list"></param>
        /*public void UpdateData(List<SimVarModel> list)
        {
            foreach (SimVarModel simVarModel in list)
            {
                if (simVarModel.IsEvent == false)
                {
                    try
                    {
                        simconnect!.RequestDataOnSimObjectType(simVarModel.eRequest, simVarModel.eDef, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }*/

        /// <summary>
        /// When data is received it searches through Failable list and updates its value 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            uint iRequest = data.dwRequestID;
            double dValue = (double)data.dwData[0];

            //if (SimVarLists.GetSimVarLists().GetFailuresList() != null)
            //foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetFailuresList())
            //{
            /*if (iRequest == (uint)oSimvarRequest.eRequest)
            {
                double dValue = (double)data.dwData[0];
                oSimvarRequest.Value = dValue;
                break;
            }*/
            //}

            /*foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetDataList())
            {
                if (iRequest == (uint)oSimvarRequest.eRequest)
                {
                    double dValue = (double)data.dwData[0];
                    oSimvarRequest.Value = dValue;
                    break;
                }
            }*/
        }

        private void SimConnect_OnRecvClientData(SimConnect sender, SIMCONNECT_RECV_CLIENT_DATA data)
        {
            switch ((DATA_REQUEST_ID)data.dwRequestID)
            {
                case DATA_REQUEST_ID.DATA_REQUEST:
                    {
                        PMDG_SDK.PMDG_NG3_Data sData = (PMDG_SDK.PMDG_NG3_Data)data.dwData[0];

                        foreach (var s in typeof(PMDG_SDK.PMDG_NG3_Data).GetFields(System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField))
                        {

                        }

                        break;
                    }
                case DATA_REQUEST_ID.CONTROL_REQUEST:
                    {
                        PMDG_SDK.PMDG_NG3_Control conData = (PMDG_SDK.PMDG_NG3_Control)data.dwData[0];



                        break;
                    }
                case DATA_REQUEST_ID.CDU_REQUEST:
                    {
                        PMDG_SDK.PMDG_NGX_CDU_Screen cduData = (PMDG_SDK.PMDG_NGX_CDU_Screen)data.dwData[0];



                        break;
                    }
                default:
                    //label1.Text = ("Unknown pmdg request ID: " + data.dwRequestID);
                    break;
            }
        }

        //Event received. Should trigger a request for the actual data.
        void SimConnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent)
        {
            /*switch (recEvent.uEventID)
            {
                case (uint)EVENTS.AP_MASTER:
                    simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_MCP, DEFINITION.MCP, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    break;
            }*/
        }

        public static int GetUserSimConnectWinEvent()
        {
            return WM_USER_SIMCONNECT;
        }

        public void ReceiveSimConnectMessage()
        {
            simconnect?.ReceiveMessage();
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Connected = true;
            state = "Sim connected";
            StateChanged?.Invoke(this, state);

            Console.WriteLine("SimConnect_OnRecvOpen");
        }

        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Console.WriteLine("SimConnect_OnRecvQuit");

            Disconnect();
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
            Console.WriteLine("SimConnect_OnRecvException: " + eException.ToString());

            SimException?.Invoke(this, eException.ToString());
        }

        public static SimCon GetSimCon() => instance;

        public SimConnect GetSimConnect() => simconnect!;
    }
}
