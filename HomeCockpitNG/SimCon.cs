using DataAccess;
using HomeCockpitNG;
using Microsoft.FlightSimulator.SimConnect;
using pmdgSDK;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Timers;

namespace SimCon
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

    public class SimCon
    {
        private static readonly SimCon instance = new();

        public event EventHandler<string>? SimException;

        private Timer? ConnectionTimer;

        public bool Connected { get; private set; } = false;

        private string state = "Sim not found";
        public event EventHandler<string>? StateChanged;

        public const int WM_USER_SIMCONNECT = 0x0402;
        public IntPtr MHWnd { get; set; }
        SimConnect? simconnect = null;

        public PMDG_SDK.PMDG_NG3_Data pmdgData;
        public PMDG_SDK.PMDG_NG3_Control pmdgControl;
        public PMDG_SDK.PMDG_NGX_CDU_Screen pmdgCDU;

        public List<PMDG_Data> PmdgDataList;


        private SimCon()
        {
            StartTimers();
            PmdgDataList = SQLPmdgData.LoadData();
        }

        public void StartTimers()
        {
            ConnectionTimer = new Timer
            {
                Interval = 1000
            };
            ConnectionTimer.Elapsed += ConnectionTimer_Elapsed;
            ConnectionTimer.AutoReset = true;
            ConnectionTimer.Enabled = true;
            ConnectionTimer.Start();
        }

        private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (Connect())
            {
                ConnectionTimer!.Stop();
            }
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
                    //simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
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

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
            }

            Connected = false;
            ConnectionTimer!.Start();
            StateChanged?.Invoke(this, "Sim not found");
        }

        private void SimConnect_OnRecvClientData(SimConnect sender, SIMCONNECT_RECV_CLIENT_DATA data)
        {
            switch ((DATA_REQUEST_ID)data.dwRequestID)
            {
                case DATA_REQUEST_ID.DATA_REQUEST:
                    {
                        //PMDG_SDK.PMDG_NG3_Data sData = (PMDG_SDK.PMDG_NG3_Data)data.dwData[0];
                        pmdgData = (PMDG_SDK.PMDG_NG3_Data)data.dwData[0];

                        FieldInfo[] members = pmdgData.GetType().GetFields();

                        foreach (FieldInfo fi in members)
                        {
                            string tempv = "";
                            if (fi.FieldType == typeof(byte[]))
                            {
                                foreach (byte b in (byte[])fi.GetValue(pmdgData))
                                {
                                    if (b > 10)
                                    {
                                        tempv += System.Text.Encoding.UTF8.GetString(new[] { b });
                                    }
                                    else
                                    {
                                        tempv += Convert.ToInt32(b).ToString() + "|";
                                    }
                                }
                            }
                            else if (fi.FieldType == typeof(float[]))
                            {
                                foreach (float f in (float[])fi.GetValue(pmdgData))
                                {
                                    tempv += f.ToString() + "|";
                                }
                            }
                            else if (fi.FieldType == typeof(ushort[]))
                            {
                                foreach (float f in (ushort[])fi.GetValue(pmdgData))
                                {
                                    tempv += f.ToString() + "|";
                                }
                            }
                            else
                                tempv = fi.GetValue(pmdgData).ToString();


                            PmdgDataList.Find(x => x.Name == fi.Name).Value = tempv.ToString();
                        }

                        break;
                    }
                case DATA_REQUEST_ID.CONTROL_REQUEST:
                    {
                        //PMDG_SDK.PMDG_NG3_Control conData = (PMDG_SDK.PMDG_NG3_Control)data.dwData[0];
                        pmdgControl = (PMDG_SDK.PMDG_NG3_Control)data.dwData[0];



                        break;
                    }
                case DATA_REQUEST_ID.CDU_REQUEST:
                    {
                        //PMDG_SDK.PMDG_NGX_CDU_Screen cduData = (PMDG_SDK.PMDG_NGX_CDU_Screen)data.dwData[0];
                        pmdgCDU = (PMDG_SDK.PMDG_NGX_CDU_Screen)data.dwData[0];



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
