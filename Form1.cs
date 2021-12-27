using NICL001;
using NIRLK003;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayLCDTester {
    public partial class Form1 : Form {

        INIReader reader_1;
        INIReader reader_2;
        string appVersion = "V" + Application.ProductVersion.Replace(".", "");

        const string gateIn = "GATE-IN", gateOut = "GATE_OUT", connect = "connect", disconnect = "disconnect";

        string direct_1;
        string direct_2;

        string idleNotif = $"silakan tempel|kartu atau pindai|kode qr anda".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string inSuccessNotif = $"sukses masuk|sisa saldo|".ToUpper() + $"Rp.1.234.567";
        string outSuccessNotif = $"sukses keluar|".ToUpper() + $"Tarif ".ToUpper() + $"Rp.123.456|" + " sisa saldo|".ToUpper() + $"Rp.1.234.567";
        string failedNotif_1 = $"tidak dikenal|silakan hubungi|petugas".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string failedNotif_2 = $"transaksi gagal|silakan diulang|kembali".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string maintenanceNotif_1 = $"gate tidak dapat|digunakan|sedang ada proses|perbaikan".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string maintenanceNotif_2 = $"sedang ada|proses perbaikan|silakan gunakan|gate lain".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string warningNotif_1 = $"gate tidak dapat|digunakan|silakan hubungi|petugas".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string warningNotif_2 = $"gate tidak dapat|digunakan|silakan gunakan|gate lain".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string emergencyNotif_1 = $"gate tidak dapat|digunakan dalam|kondisi darurat|silakan hubungi|petugas".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);
        string emergencyNotif_2 = $"kondisi darurat|gate tidak dapat|digunakan".ToUpper().Replace("|", LCDTextParam.LCDTextDelimiter);

        public Form1() {
            InitializeComponent();

            inSuccessNotif = inSuccessNotif.Replace("|", LCDTextParam.LCDTextDelimiter);
            outSuccessNotif = outSuccessNotif.Replace("|", LCDTextParam.LCDTextDelimiter);

            reader_1 = new NIKenetics(true);
            reader_2 = new NIKenetics(true);

            string[] directions = { gateIn, gateOut};
            string[] fontTypes = { "CIRCULAR STD 11x19", "ARIAL 8x19", "CIRCULAR STD 15x23" };
            string[] notifTypes = { "IDLE", "SUCCESS", "FAILED" };

            cbxDirect_1.Items.AddRange(directions);
            cbxDirect_1.SelectedIndex = 0;

            cbxFontType_1.Items.AddRange(fontTypes);
            cbxFontType_1.SelectedIndex = 0;

            cbxNotifType_1.Items.AddRange(notifTypes);
            cbxNotifType_1.SelectedIndex = 0;

            gbxNotification_1.Enabled = false;

            cbxDirect_2.Items.AddRange(directions);
            cbxDirect_2.SelectedIndex = 1;

            cbxFontType_2.Items.AddRange(fontTypes);
            cbxFontType_2.SelectedIndex = 0;

            cbxNotifType_2.Items.AddRange(notifTypes);
            cbxNotifType_2.SelectedIndex = 0;

            gbxNotification_2.Enabled = false;
        }

        #region LCD 1

        int initialReader_1 = -1;
        bool isReaderStatus_1;

        private void btnConnect_1_Click(object sender, EventArgs e) {
            if (reader_1.IsConnected) {
                reader_1.CloseReader();

                btnConnect_1.Text = connect;
            } else {
                initialReader_1 = Device.InitialReader(reader_1, tbxPort_1.Text);
                if (initialReader_1 == 0) {
                    reader_1.SetLCDInitDisplayWithDelimeter(DeviceType.GATE, idleNotif , direct_1, tbxStationName_1.Text, appVersion);
                    btnConnect_1.Text = disconnect;
                }
            }

            isReaderStatus_1 = reader_1.IsConnected;

            gbxNotification_1.Enabled = isReaderStatus_1;
            tbxPort_1.Enabled = !isReaderStatus_1;
            cbxDirect_1.Enabled = !isReaderStatus_1;
            tbxStationName_1.Enabled = !isReaderStatus_1;
        }

        private void cbxDirect_1_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxDirect_1.SelectedIndex) {
                case 0:
                direct_1 = gateIn;
                break;

                case 1:
                direct_1 = gateOut;
                break;

                default:
                break;
            }
        }

        private void btnIdle_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = idleNotif;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.IDLE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }


        private void btnSuccess_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = direct_1 == gateIn ? inSuccessNotif : outSuccessNotif;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.SUCCESS, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnFailed_1_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = failedNotif_1;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.FAILED, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnFailed_2_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = failedNotif_2;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.FAILED, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }


        private void btnMaintenance_1_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = maintenanceNotif_1;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.MAINTENANCE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnMaintenance_2_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = maintenanceNotif_2;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.MAINTENANCE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

       
        private void btnWarning_1_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = warningNotif_1;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.WARNING, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }


        private void btnWarning_2_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = warningNotif_2;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.WARNING, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }
        private void btnEmergency_1_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = emergencyNotif_1;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.EMERGENCY, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnEmergency_2_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = emergencyNotif_2;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.EMERGENCY, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }


        private void btnErrorNetwork_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                reader_1.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_NETWORK, tbxNetworkErrorCode_1.Text.Substring(0, 2));
            }
        }

        private void btnErrorDevice_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                reader_1.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_DEVICE, tbxDeviceErrorCode_1.Text.Substring(0, 2));
            }
        }

        private void btnReset_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = idleNotif;
                reader_1.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.IDLE, FontType.DEFAULT, AlignType.CENTER, notif);

                reader_1.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_DEVICE, "00");
                reader_1.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_NETWORK, "00");
            }
        }


        FontType fontType_1 = FontType.DEFAULT;
        NotifMessageType notifType_1 = NotifMessageType.IDLE;

        private void cbxFontType_1_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxFontType_1.SelectedIndex) {
                case 0:
                fontType_1 = FontType.DEFAULT;
                break;

                case 1:
                fontType_1 = FontType.ARIAL_8x19;
                break;

                case 2:
                fontType_1 = FontType.CIRCULAR_15x23;
                break;

                default:
                break;
            }
        }

        private void cbxNotifType_1_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxNotifType_1.SelectedIndex) {
                case 0:
                notifType_1 = NotifMessageType.IDLE;
                break;

                case 1:
                notifType_1 = NotifMessageType.SUCCESS;
                break;

                case 2:
                notifType_1 = NotifMessageType.FAILED;
                break;

                default:
                break;
            }
        }

        private void btnMessageDynamic_1_Click(object sender, EventArgs e) {
            if (isReaderStatus_1) {
                string notif = tbxMessageDynamic_1.Text.Replace("|", LCDTextParam.LCDTextDelimiter);
                reader_1.SetLCDDisplayNotificationWithDelimeter(notifType_1, fontType_1, AlignType.CENTER, notif);
            }
        }

        #endregion

        #region LCD 2

        int initialReader_2 = -1;
        bool isReaderStatus_2;

        private void btnConnect_2_Click(object sender, EventArgs e) {
            if (reader_2.IsConnected) {
                reader_2.CloseReader();

                btnConnect_2.Text = connect;
            } else {
                initialReader_2 = Device.InitialReader(reader_2, tbxPort_2.Text);
                if (initialReader_2 == 0) {
                    reader_2.SetLCDInitDisplayWithDelimeter(DeviceType.GATE, idleNotif, direct_2, tbxStationName_2.Text, appVersion);
                    btnConnect_2.Text = disconnect;
                }
            }

            isReaderStatus_2 = reader_2.IsConnected;

            gbxNotification_2.Enabled = isReaderStatus_2;
            tbxPort_2.Enabled = !isReaderStatus_2;
            cbxDirect_2.Enabled = !isReaderStatus_2;
            tbxStationName_2.Enabled = !isReaderStatus_2;
        }

        private void cbxDirect_2_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxDirect_2.SelectedIndex) {
                case 0:
                direct_2 = gateIn;
                break;

                case 1:
                direct_2 = gateOut;
                break;

                default:
                break;
            }
        }

        private void btnIdle_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = idleNotif;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.IDLE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnSuccess_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = direct_2 == gateIn ? inSuccessNotif : outSuccessNotif;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.SUCCESS, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnFailed_1_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = failedNotif_1;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.FAILED, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnFailed_2_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = failedNotif_2;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.FAILED, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnMaintenance_1_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = maintenanceNotif_1;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.MAINTENANCE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnMaintenance_2_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = maintenanceNotif_2;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.MAINTENANCE, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnWarning_1_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = warningNotif_1;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.WARNING, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnWarning_2_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = warningNotif_2;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.WARNING, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnEmergency_1_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = emergencyNotif_1;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.EMERGENCY, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }

        private void btnEmergency_2_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = emergencyNotif_2;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.EMERGENCY, FontType.DEFAULT, AlignType.CENTER, notif);
            }
        }     

        private void btnErrorNetwork_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                reader_2.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_NETWORK, tbxNetworkErrorCode_2.Text.Substring(0, 2));
            }
        }

        private void btnErrorDevice_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                reader_2.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_DEVICE, tbxDeviceErrorCode_2.Text.Substring(0, 2));
            }
        }

        private void btnReset_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = idleNotif;
                reader_2.SetLCDDisplayNotificationWithDelimeter(NotifMessageType.IDLE, FontType.DEFAULT, AlignType.CENTER, notif);

                reader_2.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_DEVICE, "00");
                reader_2.SetLCDDisplayErrorCode(ErrorCodeType.ERROR_NETWORK, "00");
            }
        }

        FontType fontType_2 = FontType.DEFAULT;
        NotifMessageType notifType_2 = NotifMessageType.IDLE;

        private void cbxFontType_2_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxFontType_2.SelectedIndex) {
                case 0:
                fontType_2 = FontType.DEFAULT;
                break;

                case 1:
                fontType_2 = FontType.ARIAL_8x19;
                break;

                case 2:
                fontType_2 = FontType.CIRCULAR_15x23;
                break;

                default:
                break;
            }

        }

        private void cbxNotifType_2_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cbxNotifType_2.SelectedIndex) {
                case 0:
                notifType_2 = NotifMessageType.IDLE;
                break;

                case 1:
                notifType_2 = NotifMessageType.SUCCESS;
                break;

                case 2:
                notifType_2 = NotifMessageType.FAILED;
                break;

                default:
                break;
            }

        }

        private void btnMessageDynamic_2_Click(object sender, EventArgs e) {
            if (isReaderStatus_2) {
                string notif = tbxMessageDynamic_2.Text.Replace("|", LCDTextParam.LCDTextDelimiter);
                reader_2.SetLCDDisplayNotificationWithDelimeter(notifType_2, fontType_2, AlignType.CENTER, notif);
            }
        }
        #endregion
    }
}
