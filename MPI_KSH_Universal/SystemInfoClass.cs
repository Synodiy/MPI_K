using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MPI_KSH_Universal
{
    public class SystemInfoClass
    {
        public string Name;
        public string KitNum;

        private string YCXadrString;
        private string YCYadrString;
        private string YCZadrString;
        public bool YCXuse = false;
        public bool YCYuse = false;
        public bool YCZuse = false;

        private string PXadrString;
        private string PYadrString;
        private string PZadrString;
        public bool PXuse = false;
        public bool PYuse = false;
        public bool PZuse = false;

        private string VRKadrString;
        public bool VRKuse = false;

        public int ScaleM1;
        public int ScaleM2;

        private string TYCXadrString;
        private string TYCYZadrString;
        public bool TYCXuse = false;
        public bool TYCYZuse = false;
        public int TYCXprizn = -1;
        public int TYCYZprizn = -1;

        private string TPXadrString;
        private string TPYadrString;
        private string TPZadrString;
        public bool TPXuse = false;
        public bool TPYuse = false;
        public bool TPZuse = false;
        public int TPXprizn = -1;
        public int TPYprizn = -1;
        public int TPZprizn = -1;

        private string TGIBadrString;
        public bool TGIBuse = false;
        public int TGIBprizn = -1;

        public double TempKoef;            

        public byte[] UsingAdressess;

        public byte[] RKVadrByte = new byte[2];
        private string RKVadrString;
        public bool RKVuse = false;

        public byte[] K1adrByte = new byte[2];
        public byte[] K2adrByte = new byte[2];
        public byte[] ResetKadrByte = new byte[2];
        private string K1adrString;
        private string K2adrString;
        private string ResetKadrString;
        public bool K1use = false;
        public bool K2use = false;
        public bool ResetKuse = false;

        public int TempWords;               //count of temperature channels
        public int OtherWords;              //count of other channels (YC, P, status)
        public int WordsInPack;             //count of words in one interrupt (other words + one from temperature + zpr time + stop-word)
        public int CountOfPack;             //count of packs that make up a complete package (Pack * TempWords * 2, one temperature gives from two packs and next temperature from next two packs) 
        public int ZeroSignalCount;         //count of packages in 10 ms (zero signal)

        public int CountOfPackagesForZeroSignal;
        public bool NeedCalculateZero = false;

        private SQLiteCommand sqlCommand;
        private SQLiteConnection sqlConnection;
        public bool sqlError;

        public SystemInfoClass(int sysID, string kitN)
        {
            KitNum = kitN;
            sqlError = false;

            sqlConnection = new SQLiteConnection();
            sqlCommand = new SQLiteCommand();

            #region Parse DB

            try
            {
                //connect to db file
                sqlConnection = new SQLiteConnection(sqLiteClass.sqlConnectString);
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;

                if (sqlConnection.State != ConnectionState.Open)
                {
                    MessageBox.Show("Database not open!");
                    sqlError = true;
                    return;
                }

                sqlCommand.CommandText = "SELECT * FROM SystemProfiles WHERE id = '" + sysID + "'";
                SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();

                sqlRead.Read();

                Name = sqlRead["name"].ToString();

                #region YC
                if (sqlRead["ycxuse"].ToString() == "use") YCXuse = true;
                YCXadrString = sqlRead["ycx"].ToString();

                if (sqlRead["ycyuse"].ToString() == "use") YCYuse = true;
                YCYadrString = sqlRead["ycy"].ToString();

                if (sqlRead["yczuse"].ToString() == "use") YCZuse = true;
                YCZadrString = sqlRead["ycz"].ToString();
                #endregion
                #region P
                if (sqlRead["pxuse"].ToString() == "use") PXuse = true;
                PXadrString = sqlRead["px"].ToString();

                if (sqlRead["pyuse"].ToString() == "use") PYuse = true;
                PYadrString = sqlRead["py"].ToString();

                if (sqlRead["pzuse"].ToString() == "use") PZuse = true;
                PZadrString = sqlRead["pz"].ToString();
                #endregion
                #region VRK
                if (sqlRead["vrkuse"].ToString() == "use") VRKuse = true;
                VRKadrString = sqlRead["vrk"].ToString();
                #endregion
                #region T YC
                if (sqlRead["tycxuse"].ToString() == "use") TYCXuse = true;
                TYCXadrString = sqlRead["tycx"].ToString();

                if (sqlRead["tycyzuse"].ToString() == "use") TYCYZuse = true;
                TYCYZadrString = sqlRead["tycyz"].ToString();
                #endregion
                #region T P
                if (sqlRead["tpxuse"].ToString() == "use") TPXuse = true;
                TPXadrString = sqlRead["tpx"].ToString();

                if (sqlRead["tpyuse"].ToString() == "use") TPYuse = true;
                TPYadrString = sqlRead["tpy"].ToString();

                if (sqlRead["tpzuse"].ToString() == "use") TPZuse = true;
                TPZadrString = sqlRead["tpz"].ToString();
                #endregion
                #region T GIB
                if (sqlRead["tgibuse"].ToString() == "use") TGIBuse = true;
                TGIBadrString = sqlRead["tgib"].ToString();
                #endregion
                TempKoef = Convert.ToDouble(sqlRead["KT"]);
                #region Scale
                if (sqlRead["rkvuse"].ToString() == "use") RKVuse = true;
                RKVadrString = sqlRead["rkv"].ToString();

                ScaleM1 = Convert.ToUInt16(sqlRead["M1"]);
                ScaleM2 = Convert.ToUInt16(sqlRead["M2"]);
                #endregion
                #region VK
                if (sqlRead["k1use"].ToString() == "use") K1use = true;
                K1adrString = sqlRead["k1"].ToString();

                if (sqlRead["k2use"].ToString() == "use") K2use = true;
                K2adrString = sqlRead["k2"].ToString();

                if (sqlRead["resetkuse"].ToString() == "use") ResetKuse = true;
                ResetKadrString = sqlRead["resetk"].ToString();
                #endregion

                sqlRead.Close();

                sqlConnection.Close();
                sqlCommand.Connection = sqlConnection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                sqlError = true;
                return;
            }
            #endregion

            TempWords = OtherWords = WordsInPack = 0;
            if (YCXuse) OtherWords++;
            if (YCYuse) OtherWords++;
            if (YCZuse) OtherWords++;
            if (PXuse) OtherWords++;
            if (PYuse) OtherWords++;
            if (PZuse) OtherWords++;
            if (VRKuse) OtherWords++;
            if (TYCXuse) TempWords++;
            if (TYCYZuse) TempWords++;
            if (TPXuse) TempWords++;
            if (TPYuse) TempWords++;
            if (TPZuse) TempWords++;
            if (TGIBuse) TempWords++;

            if (YCXuse || YCYuse || YCZuse || PXuse || PYuse || PZuse) NeedCalculateZero = true;

            if (RKVuse)
            {
                RKVadrByte[0] = (byte)UInt16.Parse(RKVadrString, NumberStyles.HexNumber);
                RKVadrByte[1] = (byte)(UInt16.Parse(RKVadrString, NumberStyles.HexNumber) >> 8);
            }
            if (K1use)
            {
                K1adrByte[0] = (byte)UInt16.Parse(K1adrString, NumberStyles.HexNumber);
                K1adrByte[1] = (byte)(UInt16.Parse(K1adrString, NumberStyles.HexNumber) >> 8);
            }
            if (K2use)
            {
                K2adrByte[0] = (byte)UInt16.Parse(K2adrString, NumberStyles.HexNumber);
                K2adrByte[1] = (byte)(UInt16.Parse(K2adrString, NumberStyles.HexNumber) >> 8);
            }
            if (ResetKuse)
            {
                ResetKadrByte[0] = (byte)UInt16.Parse(ResetKadrString, NumberStyles.HexNumber);
                ResetKadrByte[1] = (byte)(UInt16.Parse(ResetKadrString, NumberStyles.HexNumber) >> 8);
            }
            
            WordsInPack = OtherWords;
            if (TempWords != 0) WordsInPack++;
            WordsInPack += 2;  //tzpr + stop
            CountOfPack = TempWords == 0 ? 1 : TempWords * 2;

            #region Array of adressess for MPI
            UsingAdressess = new byte[(OtherWords + TempWords) * 2];
            int k = 0;
            if (YCXuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(YCXadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(YCXadrString, NumberStyles.HexNumber) >> 8);
            }
            if (YCYuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(YCYadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(YCYadrString, NumberStyles.HexNumber) >> 8);
            }
            if (YCZuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(YCZadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(YCZadrString, NumberStyles.HexNumber) >> 8);
            }
            if (PXuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(PXadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(PXadrString, NumberStyles.HexNumber) >> 8);
            }
            if (PYuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(PYadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(PYadrString, NumberStyles.HexNumber) >> 8);
            }
            if (PZuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(PZadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(PZadrString, NumberStyles.HexNumber) >> 8);
            }
            if (VRKuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(VRKadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(VRKadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TYCXuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TYCXadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TYCXadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TYCYZuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TYCYZadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TYCYZadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TPXuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TPXadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TPXadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TPYuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TPYadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TPYadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TPZuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TPZadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TPZadrString, NumberStyles.HexNumber) >> 8);
            }
            if (TGIBuse)
            {
                UsingAdressess[k++] = (byte)UInt16.Parse(TGIBadrString, NumberStyles.HexNumber);
                UsingAdressess[k++] = (byte)(UInt16.Parse(TGIBadrString, NumberStyles.HexNumber) >> 8);
            }
            #endregion

            if (TempWords == 0) return;

            k = 0;
            if (TYCXuse) TYCXprizn = k++;
            if (TYCYZuse) TYCYZprizn = k++;
            if (TPXuse) TPXprizn = k++;
            if (TPYuse) TPYprizn = k++;
            if (TPZuse) TPZprizn = k++;
            if (TGIBuse) TGIBprizn = k++;
        }
    }
}
