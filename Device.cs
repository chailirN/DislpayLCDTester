using NICL001;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DislpayLCDTester {
    internal class Device {
        internal static int InitialReader(INIReader reader, string portName) {
            string readerSN = string.Empty, readerFrimware = string.Empty, refMessage = string.Empty;
            int retVal = -1;
            refMessage = string.Empty;
            if (!reader.IsConnected) {
                reader.SetPortReader(portName);
                retVal = reader.InitReader();
                if (retVal == 0) {
                    readerFrimware = readerSN = string.Empty;
                    readerFrimware = reader.GetReaderVersion();
                    readerSN = reader.GetReaderSN();
                    if (!string.IsNullOrEmpty(readerFrimware) && !string.IsNullOrEmpty(readerSN)) {
                        refMessage = "Connected";
                    } else {
                        retVal = 4;
                        refMessage = string.Format("Disconnected | Error: Get Reader {0} Firmware or Reader {0} SN is not found", portName);
                    }
                } else {
                    refMessage = string.Format("Disconnected | Error: Initial Reader {0} Failed", portName);

                    if (retVal == 3) refMessage = string.Format("Disconnected | Error: Reader is not found or connected", portName);
                }
            } else {
                retVal = 1;
                refMessage = string.Format("Disconnected | Error: Reader {0} Already Open", portName);
            }

            return retVal;
        }

        internal static int CheckingRedaerStatus(INIReader reader, string portName, ref string readerSN, ref string readerFrimware, ref string refMessage) {
            int retVal = -1;
            try {
                retVal = 2;
                if (reader.IsConnected) {
                    readerSN = reader.GetReaderSN();
                    readerFrimware = reader.GetReaderVersion();
                    if (!string.IsNullOrEmpty(readerSN)) {
                        retVal = 0;
                        refMessage = "Connected";
                    } else {
                        retVal = 1;
                        refMessage = "Disconnected";
                    }
                } else {
                    retVal = reader.InitReader();
                    if (retVal == 0) {
                        readerFrimware = readerSN = string.Empty;
                        readerFrimware = reader.GetReaderVersion();
                        readerSN = reader.GetReaderSN();
                        if (!string.IsNullOrEmpty(readerFrimware) && !string.IsNullOrEmpty(readerSN)) {
                            refMessage = "Connected";
                        } else {
                            retVal = 4;
                            refMessage = string.Format("Disconnected | Error: Get Reader {0} Firmware or Reader {0} SN is not found", portName);
                        }
                    } else {
                        refMessage = string.Format("Disconnected | Error: Initial Reader {0} Failed", portName);
                    }
                }
            } catch (Exception ex) {
                retVal = 2;
                refMessage = string.Format("Disconnected | Exception Error: {0}", ex.Message);

                if (reader.IsConnected) {
                    retVal = reader.InitReader();
                    if (retVal == 0) {
                        readerFrimware = readerSN = string.Empty;
                        readerFrimware = reader.GetReaderVersion();
                        readerSN = reader.GetReaderSN();
                        if (!string.IsNullOrEmpty(readerFrimware) && !string.IsNullOrEmpty(readerSN)) {
                            refMessage = "Connected";
                        } else {
                            retVal = 4;
                            refMessage = string.Format("Disconnected | Error: Get Reader {0} Firmware or Reader {0} SN is not found", portName);
                        }
                    } else {
                        refMessage = string.Format("Disconnected | Error: Initial Reader {0} Failed", portName);
                    }
                }
            }

            if (retVal != 0) {
                readerSN = readerFrimware = string.Empty;
            }

            return retVal;
        }

        internal static int InitialLCDReader(INIReader reader, DeviceType deviceType, LCDTextParam notificaitonMessage, string deviceName, string stationName, string appVersion) {
            int retVal = -1;
            if (reader.IsConnected) {
                retVal = reader.SetLCDInitDisplay(deviceType, notificaitonMessage, deviceName, stationName, appVersion);
            }

            return retVal;
        }
        
    }
}
