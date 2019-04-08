using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Automation.BDaq;

namespace MPI_KSH_Universal
{
    public partial class Work_Form : Form
    {
        private readonly int UpdateScreenInterval = 100;

        private bool BackToMainMenu;

        private SystemInfoClass SysInfo;

        private InstantDoCtrl PCI_1751 = new InstantDoCtrl();
        private SerialPort RS422_Port;

        private string BinPath;
        private string CSVPath;

        private bool VKGroupIsEnable;

        private bool GIBgotov;
        private long StartPosForZeroSignal;

        private int BrokenPackages;
        private bool ZeroSignalDisplayed;
        private int[] ZeroSignals = new int[6];
        private int[] LastData_TempWords;
        private int[] LastData_OtherWords;
        private bool Scale_YCX_M2 = false, Scale_YCY_M2 = false, Scale_YCZ_M2 = false, Scale_changed = false;

        public Work_Form(SystemInfoClass sysInfo)
        {
            InitializeComponent();

            SysInfo = sysInfo;

            PrepareForm();

            if (!Connect1751()) return;

            if (ConnectMOXA())
            {
                START_button.Enabled = true;
                STOP_button.Enabled = false;
                FormConsole.AppendText("МПИ подключен.\n");
                FormConsole.ScrollToCaret();
            }
            else
            {
                START_button.Enabled = false;
                STOP_button.Enabled = false;
                FormConsole.AppendText("Проверьте подключение МПИ!\n");
                FormConsole.ScrollToCaret();
            }
        }


        #region Base Methods
        private bool Connect1751()
        {
            try
            {
                //присваивание объектам классов InstantDi(о)Ctrl параметров платы, имеющей ID = DeviceNumber
                PCI_1751.SelectedDevice = new DeviceInformation(Find1751());

                PCI_1751.Write(5, 0xFF);                    //SW-POW (PC14) off

                //настройка портов на выход/вход
                PortDirection[] portDirs = PCI_1751.PortDirection;
                portDirs[5].Direction = DioPortDir.LinHout; //PC10 - input (ZPR), PC14 - output (SW-POW) 

                portDirs[0].Direction = DioPortDir.Input;   //Data_0-7
                portDirs[1].Direction = DioPortDir.Input;   //Data_8-15
                portDirs[2].Direction = DioPortDir.LinHout; //PC00 - input (ZPR), PC04 - INIT, PC05 - WA, PC06 - RD, PC07 - WD 
            }
            catch (Exception)
            {
                FormConsole.AppendText("Не удалось сконфигурировать PCI-1751! Проверьте подключение!\n");
                FormConsole.ScrollToCaret();
                return false;
            }

            return true;
        }
        private int Find1751()
        {
            //поиск платы 1751 перебором различных ID (от 0 до 16)
            int[] NoDevice = new int[16];
            int DeviceNumber = 0;
            for (int i = 0; i < 16; i++)
            {
                try
                {
                    PCI_1751.SelectedDevice = new DeviceInformation(i);
                    PortDirection[] portDirs = PCI_1751.PortDirection;
                    portDirs[0].Direction = DioPortDir.Input;
                }
                catch (Exception)
                {
                    NoDevice[i] = 1;
                }

                if (NoDevice[i] == 0)
                {
                    DeviceNumber = i;
                }
            }
            return DeviceNumber;
        }
        private bool ConnectMOXA()
        {
            bool MPI_Find = false;

            string[] Ports = SerialPort.GetPortNames();
            RS422_Port = new SerialPort
            {
                BaudRate = 921600,
                Parity = Parity.Even,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            foreach (string port in Ports)
            {
                RS422_Port.PortName = port;

                try
                {
                    RS422_Port.Open();  //пытаемся открыть порт
                }
                catch (Exception)
                {
                    continue;
                }

                PCI_1751.Write(5, 0xEF);        //SW-POW (PC14) on    
                RS422_Port.DiscardInBuffer();
                RS422_Port.DiscardOutBuffer();

                int[] Income = new int[10];

                try
                {
                    byte[] Command = { 0x46, 0x00, 0x00, 0x00, 0xFF };      //reset
                    RS422_Port.Write(Command, 0, Command.Length);

                    Income[0] = RS422_Port.ReadByte();
                    for (int k = 0; k < 10; k++)
                    {
                        Income[k + 1] = RS422_Port.ReadByte();

                        if ((Income[k] == 0x52) && (Income[k + 1] == 0x00))
                        {
                            MPI_Find = true;
                            break;
                        }
                    }

                    if (MPI_Find) return true;

                    RS422_Port.Close();
                    PCI_1751.Write(5, 0xFF);        //SW-POW (PC14) off
                }
                catch (Exception)
                {
                    RS422_Port.Close();
                    PCI_1751.Write(5, 0xFF);        //SW-POW (PC14) off
                }
            }

            return false;
        }
        private void MPIReset()
        {
            AllMPIbuttonsOFF();

            timer1.Stop();

            RS422_Port.DataReceived -= DataReceivedHandler;
            RS422_Port.DiscardInBuffer();
            RS422_Port.DiscardOutBuffer();

            byte[] Command = { 0x46, 0x00, 0x00, 0x00, 0xFF };
            RS422_Port.Write(Command, 0, Command.Length);

            AllMPIbuttonsON();
            START_button.Enabled = true;
            STOP_button.Enabled = false;
            Back_button.Enabled = true;
            K1_button.Enabled = false;
            K2_button.Enabled = false;
            ResetK_button.Enabled = false;
            DisplayClear();
        }
        private void MPIError(byte code1, byte code2)
        {
            if (code1 == 0x45)
            {
                switch (code2)
                {
                    case 0x00:
                        ConsoleWriter("Ошибка отсутствует!", Color.Red);
                        break;
                    case 0x01:
                        ConsoleWriter("Ошибка обмена по RS-422!", Color.Red);
                        break;
                    case 0x02:
                        ConsoleWriter("Недопустимая команда!", Color.Red);
                        break;
                    case 0x03:
                        ConsoleWriter("Ошибка дешифрации команды!", Color.Red);
                        break;
                    case 0x04:
                        ConsoleWriter("Ошибка выбора интерфейса обмена ГИБ - БЦВУ!", Color.Red);
                        break;
                    case 0x05:
                        ConsoleWriter("Ошибка выбора температурной точки!", Color.Red);
                        break;
                    case 0x06:
                        ConsoleWriter("Ошибка настройки температуры!", Color.Red);
                        break;
                    case 0x07:
                        ConsoleWriter("Ошибка настройки опорного напряжения!", Color.Red);
                        break;
                    case 0x08:
                        ConsoleWriter("Ошибка записи ППЗУ!", Color.Red);
                        break;
                    case 0x0A:
                        ConsoleWriter("Ошибка выделения памяти!", Color.Red);
                        break;
                    default:
                        ConsoleWriter("Неизвестная ошибка!", Color.Red);
                        break;
                }
            }
            else
            {
                ConsoleWriter("Неизвестная ошибка!", Color.Red);
            }


            MPIReset();
        }

        #endregion

        #region Buttons
        private void START_button_Click(object sender, EventArgs e)
        {
            DisplayClear();
            FormConsole.Clear();

            AllMPIbuttonsOFF();

            BrokenPackages = 0;

            string DirectoryPath = Application.StartupPath + "\\data\\";
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            BinPath = Path.Combine(DirectoryPath, "temporary.bin");
            using (new BinaryWriter(File.Create(BinPath))) { }

            ZeroSignalDisplayed = false;
            GIBgotov = false;

            LastData_TempWords = new int[SysInfo.TempWords * 2];
            LastData_OtherWords = new int[SysInfo.OtherWords];

            RS422_Port.DiscardInBuffer();
            RS422_Port.DiscardOutBuffer();

            byte[] Command = { 0x4D, 0x4B, (byte)SysInfo.OtherWords, (byte)SysInfo.TempWords, 0xFF };
            RS422_Port.Write(Command, 0, Command.Length);

            byte[] answer = new byte[2];
            RS422_Port.Read(answer, 0, 2);

            if ((answer[0] == 0x59) && (answer[1] == 0x00))
            {
                AllMPIbuttonsON();
                START_button.Enabled = false;
                STOP_button.Enabled = true;
                Back_button.Enabled = false;
                ConvertToCSV_Button.Enabled = false;
                if (VKGroupIsEnable) VKGroup.Enabled = true;

                if (SysInfo.RKVuse)
                {
                    if (SysInfo.YCXuse) YCXtext.BackColor = Color.GreenYellow; //масштаб УСХ 
                    if (SysInfo.YCYuse) YCYtext.BackColor = Color.GreenYellow; //масштаб УСY 
                    if (SysInfo.YCZuse) YCZtext.BackColor = Color.GreenYellow; //масштаб УСZ
                }

                ConsoleWriter("Для прекращения работы нажмите \"СТОП\"");

                RS422_Port.Write(SysInfo.UsingAdressess, 0, SysInfo.UsingAdressess.Length);

                RS422_Port.DiscardInBuffer();
                RS422_Port.DataReceived += DataReceivedHandler;

                if (SysInfo.WordsInPack != 0) timer1.Start();
            }
            else MPIError(answer[0], answer[1]);
        }
        private void STOP_button_Click(object sender, EventArgs e)
        {
            MPIReset();
            FormConsole.Clear();

            long endFilePos;
            using (BinaryReader dataFile = new BinaryReader(File.OpenRead(BinPath)))
            {
                endFilePos = dataFile.BaseStream.Length;
            }

            if (endFilePos == 0)
            {
                File.Delete(BinPath);
                ConvertToCSV_Button.Enabled = false;
            }
            else
            {
                ConvertToCSV_Button.Enabled = true;
            }
        }
        private void K1_button_Click(object sender, EventArgs e)
        {
            byte[] Command = { 0x56, SysInfo.K1adrByte[0], SysInfo.K1adrByte[1], 0x01, 0xFF };
            RS422_Port.Write(Command, 0, Command.Length);

            K1_button.Enabled = false;
            K2_button.Enabled = false;

            ResetK_button.Focus();
        }
        private void K2_button_Click(object sender, EventArgs e)
        {
            byte[] Command = { 0x56, SysInfo.K2adrByte[0], SysInfo.K2adrByte[1], 0x02, 0xFF };
            RS422_Port.Write(Command, 0, Command.Length);

            K1_button.Enabled = false;
            K2_button.Enabled = false;

            ResetK_button.Focus();
        }
        private void ResetK_button_Click(object sender, EventArgs e)
        {
            byte[] Command = { 0x56, SysInfo.ResetKadrByte[0], SysInfo.ResetKadrByte[1], 0x00, 0xFF };
            RS422_Port.Write(Command, 0, Command.Length);

            if (SysInfo.K1use) K1_button.Enabled = true;
            if (SysInfo.K2use) K2_button.Enabled = true;
        }
        private void ConvertToCSV_Button_Click(object sender, EventArgs e)
        {
            string directoryPath = Application.StartupPath + "\\data\\" + SysInfo.Name + "\\" + SysInfo.KitNum + "\\" + DateTime.Today.ToShortDateString();
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            SaveFileDialog saveCSV = new SaveFileDialog
            {
                FileName = DateTime.Now.ToLongTimeString().Replace(':', '_') + ".csv",
                RestoreDirectory = true,  //без этой сраной строчки на хп отваливается база данных
                InitialDirectory = directoryPath,
                AddExtension = true,
                DefaultExt = "csv",
                Filter = "CSV |*.csv"
            };

            if (saveCSV.ShowDialog() == DialogResult.OK)
            {
                CSVPath = saveCSV.FileName;

                Thread thread = new Thread(ConvertToCSV_Thread); //Создаем новый объект потока (Thread)
                thread.Start(); //запускаем поток
            }
        }
        private void Back_button_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms[0];
            frm.Show();
            BackToMainMenu = true;
            Close();
        }
        #endregion

        #region Form Methods
        private void PrepareForm()
        {
            Text = SysInfo.Name;

            timer1.Interval = UpdateScreenInterval;

            ZeroGroup.Enabled = CurrentGroup.Enabled = SysInfo.OtherWords != 0;
            TemperatureGroup.Enabled = SysInfo.TempWords != 0;
            StatusGroup.Enabled = SysInfo.VRKuse;

            VKGroupIsEnable = VKGroup.Enabled = SysInfo.VRKuse && (SysInfo.K1use || SysInfo.K2use || SysInfo.ResetKuse);

            K1_button.Enabled = false;
            K2_button.Enabled = false;
            ResetK_button.Enabled = false;

            ZeroGroup.Enabled = SysInfo.VRKuse;

            ZeroYCXgroup.Enabled = YCXgroup.Enabled = SysInfo.YCXuse;
            ZeroYCYgroup.Enabled = YCYgroup.Enabled = SysInfo.YCYuse;
            ZeroYCZgroup.Enabled = YCZgroup.Enabled = SysInfo.YCZuse;

            ZeroPXgroup.Enabled = PXgroup.Enabled = SysInfo.PXuse;
            ZeroPYgroup.Enabled = PYgroup.Enabled = SysInfo.PYuse;
            ZeroPZgroup.Enabled = PZgroup.Enabled = SysInfo.PZuse;


            TYCXgroup.Enabled = SysInfo.TYCXuse;
            TYCYZgroup.Enabled = SysInfo.TYCYZuse;

            TPXgroup.Enabled = SysInfo.TPXuse;
            TPYgroup.Enabled = SysInfo.TPYuse;
            TPZgroup.Enabled = SysInfo.TPZuse;

            TGIBgroup.Enabled = SysInfo.TGIBuse;

            START_button.Enabled = false;
            STOP_button.Enabled = false;
            ConvertToCSV_Button.Enabled = false;

            ZeroYCXtext.GotFocus += DropFocus;
            ZeroYCYtext.GotFocus += DropFocus;
            ZeroYCZtext.GotFocus += DropFocus;
            ZeroPXtext.GotFocus += DropFocus;
            ZeroPYtext.GotFocus += DropFocus;
            ZeroPZtext.GotFocus += DropFocus;
            YCXtext.GotFocus += DropFocus;
            YCYtext.GotFocus += DropFocus;
            YCZtext.GotFocus += DropFocus;
            PXtext.GotFocus += DropFocus;
            PYtext.GotFocus += DropFocus;
            PZtext.GotFocus += DropFocus;
            TYCXtext.GotFocus += DropFocus;
            TYCYZtext.GotFocus += DropFocus;
            TGIBtext.GotFocus += DropFocus;
            TPXtext.GotFocus += DropFocus;
            TPYtext.GotFocus += DropFocus;
            TPZtext.GotFocus += DropFocus;
            ZPRtext.GotFocus += DropFocus;

            DisplayClear();
        }
        public void AllMPIbuttonsOFF()
        {
            ButtonsGroup.Invoke((MethodInvoker)delegate { ButtonsGroup.Enabled = false; });
            VKGroup.Invoke((MethodInvoker)delegate { VKGroup.Enabled = false; });
        }
        public void AllMPIbuttonsON()
        {
            ButtonsGroup.Invoke((MethodInvoker)delegate { ButtonsGroup.Enabled = true; });
            if (VKGroupIsEnable) VKGroup.Invoke((MethodInvoker)delegate { VKGroup.Enabled = true; });
        }
        private void DisplayClear()
        {
            YCXtext.Text = "";
            YCYtext.Text = "";
            YCZtext.Text = "";
            PXtext.Text = "";
            PYtext.Text = "";
            PZtext.Text = "";
            TGIBtext.Text = "";
            TYCXtext.Text = "";
            TYCYZtext.Text = "";
            TPXtext.Text = "";
            TPYtext.Text = "";
            TPZtext.Text = "";
            ZeroPXtext.Text = "";
            ZeroPYtext.Text = "";
            ZeroPZtext.Text = "";
            ZeroYCXtext.Text = "";
            ZeroYCYtext.Text = "";
            ZeroYCZtext.Text = "";
            ZPRtext.Text = "";

            Statustext.Enabled = false;

            if (SysInfo.YCXuse) YCXtext.BackColor = Color.White;   //масштаб УСХ   
            if (SysInfo.YCYuse) YCYtext.BackColor = Color.White;   //масштаб УСУ
            if (SysInfo.YCZuse) YCZtext.BackColor = Color.White;   //масштаб УСZ
        }
        private void ConsoleWriter(string text, Color color = default(Color))
        {
            FormConsole.Invoke((MethodInvoker)delegate
            {
                FormConsole.AppendText(text + "\n");
                FormConsole.ScrollToCaret();
                if (color == default(Color)) return;
                FormConsole.Select(FormConsole.Text.Length - text.Length - 1, text.Length);
                FormConsole.SelectionColor = color;
                FormConsole.Select(FormConsole.Text.Length - 1, 1);
                FormConsole.ScrollToCaret();
            });
        }
        private void Work_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            PCI_1751.Write(5, 0xFF);        //SW-POW (PC14) off
            PCI_1751.Dispose();

            if ((RS422_Port != null) && (RS422_Port.IsOpen))
            {
                RS422_Port.DataReceived -= DataReceivedHandler;
                RS422_Port.Close();
            }

            timer1.Stop();

            if (BinPath != null && File.Exists(BinPath))
            {
                File.Delete(BinPath);
            }

            if (!BackToMainMenu) Application.Exit();
        }

        #endregion

        #region RS422 interrupt handler
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort RS422 = (SerialPort)sender;
            int BytesInRS422buffer = RS422.BytesToRead;

            if (BytesInRS422buffer == 0) return;

            byte[] Incoming = new byte[BytesInRS422buffer];
            RS422.Read(Incoming, 0, BytesInRS422buffer);

            if (Incoming.Length % (SysInfo.WordsInPack * 2) != 0)
            {
                BrokenPackages++;
                return;
            }
            using (BinaryWriter dataFile = new BinaryWriter(File.Open(BinPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)))
            {
                dataFile.Write(Incoming);
            }
        }
        #endregion

        #region Update data in screen
        private void timer1_Tick(object sender, EventArgs e)
        {
            long CurrentFileLength;
            using (BinaryReader dataFile = new BinaryReader(File.Open(BinPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                CurrentFileLength = dataFile.BaseStream.Length;
            }

            #region Zero Signals
            if (!ZeroSignalDisplayed && SysInfo.NeedCalculateZero && GIBgotov && (CurrentFileLength - StartPosForZeroSignal > SysInfo.ZeroSignalCount))
            {
                ZeroSignalDisplayed = true;

                Thread thread = new Thread(ZeroSignalDisplay_Thread); //Создаем новый объект потока (Thread)
                thread.Start(); //запускаем поток
            }
            #endregion

            if (CurrentFileLength < SysInfo.CountOfPack * SysInfo.WordsInPack * 2) return;

            ushort tempZPR;
            int k;

            #region Binary Reader
            using (BinaryReader dataFile = new BinaryReader(File.Open(BinPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                dataFile.BaseStream.Position = CurrentFileLength - 2;

                while (dataFile.ReadUInt16() != 0x5555) dataFile.BaseStream.Position -= 4;

                if (SysInfo.OtherWords != 0)
                {
                    if (!GIBgotov) StartPosForZeroSignal = dataFile.BaseStream.Position;

                    dataFile.BaseStream.Position -= SysInfo.WordsInPack * 2;

                    k = 0;

                    if (SysInfo.YCXuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.YCYuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.YCZuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.PXuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.PYuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.PZuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();
                    if (SysInfo.VRKuse) LastData_OtherWords[k++] = dataFile.ReadUInt16();

                    while (dataFile.ReadUInt16() != 0x5555) { }
                }

                dataFile.BaseStream.Position -= 4;
                tempZPR = dataFile.ReadUInt16();
                dataFile.BaseStream.Position += 2;

                if (SysInfo.TempWords != 0)
                {
                    dataFile.BaseStream.Position -= SysInfo.CountOfPack * SysInfo.WordsInPack * 2;

                    for (int i = 0; i < LastData_TempWords.Length; i++)
                    {
                        dataFile.BaseStream.Position += SysInfo.OtherWords * 2;
                        LastData_TempWords[i] = dataFile.ReadUInt16();
                        while (dataFile.ReadUInt16() != 0x5555) { }
                    }
                }
            }
            #endregion


            k = 0;
            int CurrentData;

            #region YC
            if (SysInfo.YCXuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF;    //если положительное значение -> как есть

                YCXtext.Text = CurrentData.ToString();      //УСХ

                if (SysInfo.RKVuse)
                {
                    if ((Math.Abs(CurrentData) <= SysInfo.ScaleM1) && Scale_YCX_M2)
                    {
                        Scale_YCX_M2 = false;
                        YCXtext.BackColor = Color.GreenYellow; //масштаб УСХ 
                        Scale_changed = true;
                    }
                    else if ((Math.Abs(CurrentData) >= SysInfo.ScaleM2) && !Scale_YCX_M2)
                    {
                        Scale_YCX_M2 = true;
                        YCXtext.BackColor = Color.OrangeRed; //масштаб УСХ 
                        Scale_changed = true;
                    }
                }
            }
            if (SysInfo.YCYuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF;    //если положительное значение -> как есть

                YCYtext.Text = CurrentData.ToString();      //УСY

                if (SysInfo.RKVuse)
                {
                    if ((Math.Abs(CurrentData) <= SysInfo.ScaleM1) && Scale_YCY_M2)
                    {
                        Scale_YCY_M2 = false;
                        YCYtext.BackColor = Color.GreenYellow; //масштаб УСY
                        Scale_changed = true;
                    }
                    else if ((Math.Abs(CurrentData) >= SysInfo.ScaleM2) && !Scale_YCY_M2)
                    {
                        Scale_YCY_M2 = true;
                        YCYtext.BackColor = Color.OrangeRed; //масштаб УСY 
                        Scale_changed = true;
                    }
                }
            }
            if (SysInfo.YCZuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00); //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF; //если положительное значение -> как есть

                YCZtext.Text = CurrentData.ToString(); //УСZ

                if (SysInfo.RKVuse)
                {
                    if ((Math.Abs(CurrentData) <= SysInfo.ScaleM1) && Scale_YCZ_M2)
                    {
                        Scale_YCZ_M2 = false;
                        YCZtext.BackColor = Color.GreenYellow; //масштаб УСZ 
                        Scale_changed = true;
                    }
                    else if ((Math.Abs(CurrentData) >= SysInfo.ScaleM2) && !Scale_YCZ_M2)
                    {
                        Scale_YCZ_M2 = true;
                        YCZtext.BackColor = Color.OrangeRed; //масштаб УСZ 
                        Scale_changed = true;
                    }
                }
            }
            #endregion
            #region Scale
            if (SysInfo.RKVuse)
            {
                if (Scale_changed)
                {
                    byte temp = 0x00;

                    temp = Scale_YCX_M2 ? (byte)(temp | 0x01) : (byte)(temp & 0xFE);
                    temp = Scale_YCY_M2 ? (byte)(temp | 0x02) : (byte)(temp & 0xFD);
                    temp = Scale_YCZ_M2 ? (byte)(temp | 0x04) : (byte)(temp & 0xFB);

                    byte[] Command = { 0x4E, SysInfo.RKVadrByte[0], SysInfo.RKVadrByte[1], temp, 0xFF };
                    RS422_Port.Write(Command, 0, Command.Length);

                    Scale_changed = false;
                }
            }
            #endregion
            #region P
            if (SysInfo.PXuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF;    //если положительное значение -> как есть

                PXtext.Text = CurrentData.ToString();      //PХ
            }
            if (SysInfo.PYuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF;    //если положительное значение -> как есть

                PYtext.Text = CurrentData.ToString();      //PХ
            }
            if (SysInfo.PZuse)
            {
                CurrentData = LastData_OtherWords[k++];
                if ((CurrentData & 0x200) == 0x200)
                    CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                else
                    CurrentData = CurrentData & 0x1FF;    //если положительное значение -> как есть

                PZtext.Text = CurrentData.ToString();      //PХ
            }
            #endregion
            #region Status
            if (SysInfo.VRKuse)
            {
                bool LastStatus = Statustext.Enabled;

                Statustext.Enabled = (LastData_OtherWords[k++] & 0x0001) != 0;    //гиб готов

                if (!GIBgotov && Statustext.Enabled)
                {
                    GIBgotov = true;
                    SysInfo.CountOfPackagesForZeroSignal = 100000000 / tempZPR;
                    SysInfo.ZeroSignalCount = SysInfo.WordsInPack * 2 * SysInfo.CountOfPackagesForZeroSignal;
                }

                if (LastStatus != Statustext.Enabled)
                {
                    if (SysInfo.K1use) K1_button.Enabled = Statustext.Enabled;
                    if (SysInfo.K2use) K2_button.Enabled = Statustext.Enabled;
                    if (SysInfo.ResetKuse) ResetK_button.Enabled = Statustext.Enabled;
                }
            }
            #endregion
            #region Temperature
            if (SysInfo.TempWords != 0)
            {
                foreach (int temper in LastData_TempWords)
                {
                    CurrentData = temper;
                    int prizn_temp = CurrentData & 0x7;

                    double temperature = ((CurrentData >> 7) & 0x1FF) * SysInfo.TempKoef - 60;

                    if (prizn_temp == SysInfo.TYCXprizn)
                        TYCXtext.Text = temperature.ToString("F1");
                    else if (prizn_temp == SysInfo.TYCYZprizn)
                        TYCYZtext.Text = temperature.ToString("F1");
                    else if (prizn_temp == SysInfo.TPXprizn)
                        TPXtext.Text = temperature.ToString("F1");
                    else if (prizn_temp == SysInfo.TPYprizn)
                        TPYtext.Text = temperature.ToString("F1");
                    else if (prizn_temp == SysInfo.TPZprizn)
                        TPZtext.Text = temperature.ToString("F1");
                    else if (prizn_temp == SysInfo.TGIBprizn)
                        TGIBtext.Text = temperature.ToString("F1");
                }
            }
            #endregion
            #region tzpr
            ZPRtext.Text = ((double)tempZPR / 10).ToString("F1");
            #endregion
        }

        public void DropFocus(object sender, EventArgs e)
        {
            STOP_button.Focus();
        }
        #endregion

        #region Threads
        public void ZeroSignalDisplay_Thread()
        {
            int ZeroSignalTemp;

            using (BinaryReader dataFile = new BinaryReader(File.Open(BinPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                Array.Clear(ZeroSignals, 0, ZeroSignals.Length);

                dataFile.BaseStream.Position = StartPosForZeroSignal;

                for (int k = 0; k < SysInfo.CountOfPackagesForZeroSignal; k++)
                {
                    while (dataFile.ReadUInt16() != 0x5555) { }

                    dataFile.BaseStream.Position -= SysInfo.WordsInPack * 2;

                    if (SysInfo.YCXuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[0] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[0] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    if (SysInfo.YCYuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[1] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[1] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    if (SysInfo.YCZuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[2] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[2] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    if (SysInfo.PXuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[3] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[3] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    if (SysInfo.PYuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[4] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[4] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    if (SysInfo.PZuse)
                    {
                        ZeroSignalTemp = dataFile.ReadUInt16();
                        if ((ZeroSignalTemp & 0x200) == 0x200)
                            ZeroSignals[5] += (int)((ZeroSignalTemp & 0x1FF) | 0xFFFFFE00);      //если отрицательное значение -> в доп.код
                        else
                            ZeroSignals[5] += (int)(ZeroSignalTemp & 0x1FF);            //если положительное значение -> как есть
                    }

                    dataFile.BaseStream.Position += SysInfo.WordsInPack * 2;
                }

                if (!STOP_button.Enabled) return;
                if (SysInfo.YCXuse) ZeroYCXtext.Invoke((MethodInvoker)delegate { ZeroYCXtext.Text = ZeroSignals[0].ToString(); });
                if (SysInfo.YCYuse) ZeroYCYtext.Invoke((MethodInvoker)delegate { ZeroYCYtext.Text = ZeroSignals[1].ToString(); });
                if (SysInfo.YCZuse) ZeroYCZtext.Invoke((MethodInvoker)delegate { ZeroYCZtext.Text = ZeroSignals[2].ToString(); });
                if (SysInfo.PXuse) ZeroPXtext.Invoke((MethodInvoker)delegate { ZeroPXtext.Text = ZeroSignals[3].ToString(); });
                if (SysInfo.PYuse) ZeroPYtext.Invoke((MethodInvoker)delegate { ZeroPYtext.Text = ZeroSignals[4].ToString(); });
                if (SysInfo.PZuse) ZeroPZtext.Invoke((MethodInvoker)delegate { ZeroPZtext.Text = ZeroSignals[5].ToString(); });
            }
        }

        public void ConvertToCSV_Thread()
        {
            long rowInFile, wordsInRow;
            ushort[,] dataFromFile;

            AllMPIbuttonsOFF();

            #region Parse Binary file
            using (BinaryReader DataFile = new BinaryReader(File.OpenRead(BinPath)))
            {
                long startPos = 0;
                long endPos = DataFile.BaseStream.Length;

                #region Find begin of data
                DataFile.BaseStream.Position = startPos;
                bool startFind = false;
                while (DataFile.BaseStream.Position < DataFile.BaseStream.Length)
                {
                    if (DataFile.ReadInt16() != 0x5555) continue;

                    startFind = true;

                    if ((DataFile.BaseStream.Position - SysInfo.WordsInPack * 2) < startPos)
                    {
                        startPos = DataFile.BaseStream.Position;
                        break;
                    }
                    else
                    {
                        startPos = DataFile.BaseStream.Position - SysInfo.WordsInPack * 2;
                        break;
                    }
                }
                if (!startFind)
                {
                    ConsoleWriter("Файл с исходными данными пустой!");
                    AllMPIbuttonsON();
                    return;
                }
                #endregion

                #region Find end of data
                DataFile.BaseStream.Position = DataFile.BaseStream.Length - 2;
                while (DataFile.BaseStream.Position > startPos)
                {
                    if (DataFile.ReadInt16() == 0x5555)
                    {
                        endPos = DataFile.BaseStream.Position;
                        break;
                    }
                    else
                    {
                        DataFile.BaseStream.Position -= 4;
                    }
                }

                if ((endPos - startPos) < SysInfo.WordsInPack * 2)
                {
                    ConsoleWriter("Файл пустой!");
                    AllMPIbuttonsON();
                    return;
                }
                #endregion

                rowInFile = (endPos - startPos) / (SysInfo.WordsInPack * 2);
                wordsInRow = SysInfo.WordsInPack - 1;   //stop-word kicked

                dataFromFile = new ushort[rowInFile, wordsInRow];

                DataFile.BaseStream.Position = startPos;

                for (int i = 0; i < rowInFile; i++)
                {
                    for (int k = 0; k < wordsInRow; k++)
                    {
                        dataFromFile[i, k] = DataFile.ReadUInt16();
                    }
                    DataFile.ReadInt16(); //stop-word out
                }
            }
            File.Delete(BinPath);
            #endregion

            #region Write CSV file
            using (TextWriter csvFile = new StreamWriter(CSVPath))
            {
                string newRow;
                int CurrentData;
                double zprTime;

                newRow = "time, s;";
                if (SysInfo.YCXuse) newRow += "YCX;";
                if (SysInfo.YCYuse) newRow += "YCY;";
                if (SysInfo.YCZuse) newRow += "YCZ;";
                if (SysInfo.PXuse) newRow += "PX;";
                if (SysInfo.PYuse) newRow += "PY;";
                if (SysInfo.PZuse) newRow += "PZ;";
                if (SysInfo.VRKuse) newRow += "Gotov;";
                if (SysInfo.TYCXuse) newRow += "TYCX;";
                if (SysInfo.TYCYZuse) newRow += "TYCYZ;";
                if (SysInfo.TPXuse) newRow += "TPX;";
                if (SysInfo.TPYuse) newRow += "TPY;";
                if (SysInfo.TPZuse) newRow += "TPZ;";
                if (SysInfo.TGIBuse) newRow += "TGIB;";

                newRow += "Tzpr;";
                csvFile.WriteLine(newRow);

                for (int i = 0; i < rowInFile; i++)
                {
                    int k = 0;
                    newRow = null;

                    if (SysInfo.YCXuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.YCYuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.YCZuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.PXuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.PYuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.PZuse)
                    {
                        CurrentData = dataFromFile[i, k++];
                        if ((CurrentData & 0x200) == 0x200) CurrentData = (int)((CurrentData & 0x1FF) | 0xFFFFFE00);
                        else CurrentData = CurrentData & 0x1FF;
                        newRow += CurrentData + ";";
                    }
                    if (SysInfo.VRKuse)
                    {
                        CurrentData = dataFromFile[i, k++] & 0x0001;
                        newRow += CurrentData + ";";
                    }


                    if (SysInfo.TempWords != 0)
                    {
                        CurrentData = dataFromFile[i, k++];
                        int prizn_temp = CurrentData & 0x7;
                        double temperature = ((CurrentData >> 7) & 0x1FF) * SysInfo.TempKoef - 60;

                        if (SysInfo.TYCXuse)
                        {
                            if (prizn_temp == SysInfo.TYCXprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                        if (SysInfo.TYCYZuse)
                        {
                            if (prizn_temp == SysInfo.TYCYZprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                        if (SysInfo.TPXuse)
                        {
                            if (prizn_temp == SysInfo.TPXprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                        if (SysInfo.TPYuse)
                        {
                            if (prizn_temp == SysInfo.TPYprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                        if (SysInfo.TPZuse)
                        {
                            if (prizn_temp == SysInfo.TPZprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                        if (SysInfo.TGIBuse)
                        {
                            if (prizn_temp == SysInfo.TGIBprizn) newRow += temperature + ";";
                            else newRow += ";";
                        }
                    }

                    zprTime = (double)dataFromFile[i, k] / 10;

                    newRow += zprTime.ToString("F1") + ";";

                    csvFile.WriteLine((i * (zprTime / 1000000)).ToString("F6") + ";" + newRow);
                }
            }
            #endregion

            //ConsoleWriter("Файл CSV подготовлен!");
            ConvertToCSV_Button.Invoke((MethodInvoker)delegate { ConvertToCSV_Button.Enabled = false; });
            AllMPIbuttonsON();

            Process plot = new Process();
            plot.StartInfo.FileName = CSVPath;
            plot.Start();
        }
        #endregion
    }
}
