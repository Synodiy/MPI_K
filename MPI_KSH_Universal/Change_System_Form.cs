using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MPI_KSH_Universal
{
    public partial class Change_System_Form : Form
    {
        private bool[] CheckMassive = new bool[17];
        private int NameMaxLength = 50;
        private int AddrMaxLength = 4;
        private int ScaleMaxLength = 3; // _ _ _
        private int TKMaxLength = 7;  // _._ _ _ _ _

        private bool NewProfile;
        private int SystemID;
        private XmlNode Node;
        
        private SQLiteCommand sqlCommand;
        private SQLiteConnection sqlConnection;
        
        #region Constructor
        public Change_System_Form(bool newP, int num = 0)
        {
            NewProfile = newP;
            SystemID = num;

            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            Text = "Редактор адресов профиля";

            Name_text.MaxLength = NameMaxLength;

            YCX_text.MaxLength = YCY_text.MaxLength = YCZ_text.MaxLength =
                PX_text.MaxLength = PY_text.MaxLength = PZ_text.MaxLength =
                    TYCX_text.MaxLength = TYCYZ_text.MaxLength = TGIB_text.MaxLength =
                        TPX_text.MaxLength = TPY_text.MaxLength = TPZ_text.MaxLength =
                            K1_text.MaxLength = K2_text.MaxLength = ResetK_text.MaxLength =
                             VRK_text.MaxLength = RKV_text.MaxLength = AddrMaxLength;

            M1_text.MaxLength = M2_text.MaxLength = ScaleMaxLength;
            Ktemp_text.MaxLength = TKMaxLength;

            sqlConnection = new SQLiteConnection();
            sqlCommand = new SQLiteCommand();

            CS_back_button.Enabled = true;
            CS_remove_button.Enabled = false;
            CS_save_button.Enabled = false;
            
            if (NewProfile) CreateProfile();
            else LoadProfile();
        }
        #endregion

        #region Buttons
        private void CS_remove_button_Click(object sender, EventArgs e)
        {
            try
            {
                //connect to db file
                sqlConnection = new SQLiteConnection(sqLiteClass.sqlConnectString);
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;

                if (sqlConnection.State != ConnectionState.Open)
                {
                    MessageBox.Show("Database not open!");
                    return;
                }

                sqlCommand.CommandText = "SELECT lock, name FROM SystemProfiles WHERE id = '" + SystemID + "'";
                SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();

                sqlRead.Read();

                if (sqlRead["lock"].ToString() == "Lock")
                {
                    if (MessageBox.Show("Профиль заблокирован! Вы действительно хотите удалить профиль " + sqlRead["Name"].ToString() + "?", "Удаление профиля",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        sqlRead.Close();
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("Удалить профиль " + sqlRead["Name"].ToString() + "?", "Удаление профиля",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                    {
                        sqlRead.Close();
                        return;
                    }
                }

                sqlRead.Close();

                sqlCommand.CommandText = "DELETE FROM SystemProfiles WHERE id = '" + SystemID + "'";
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                sqlCommand.Connection = sqlConnection;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            Form frm = Application.OpenForms[0];
            frm.Show();
            Close();
        }

        private void CS_back_button_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms[0];
            frm.Show();
            Close();
        }

        private void CS_save_button_Click(object sender, EventArgs e)
        {
            #region Check fields
            bool EmptyField = false;
            bool NoCheckedAddr = false;
            bool M_error = false;
            bool KT_empty = false;
            
            Name_text.Text = Name_text.Text.TrimStart();
            Name_text.Text = Name_text.Text.TrimEnd();
            if (Name_text.TextLength == 0) EmptyField = true;

            if ((YCX_text.TextLength < AddrMaxLength) && YCX_check.Checked) EmptyField = true;
            if ((YCY_text.TextLength < AddrMaxLength) && YCY_check.Checked) EmptyField = true;
            if ((YCZ_text.TextLength < AddrMaxLength) && YCZ_check.Checked) EmptyField = true;

            if ((PX_text.TextLength < AddrMaxLength) && PX_check.Checked) EmptyField = true;
            if ((PY_text.TextLength < AddrMaxLength) && PY_check.Checked) EmptyField = true;
            if ((PZ_text.TextLength < AddrMaxLength) && PZ_check.Checked) EmptyField = true;

            if ((TYCX_text.TextLength < AddrMaxLength) && TYCX_check.Checked) EmptyField = true;
            if ((TYCYZ_text.TextLength < AddrMaxLength) && TYCYZ_check.Checked) EmptyField = true;
            if ((TGIB_text.TextLength < AddrMaxLength) && TGIB_check.Checked) EmptyField = true;
            if ((TPX_text.TextLength < AddrMaxLength) && TPX_check.Checked) EmptyField = true;
            if ((TPY_text.TextLength < AddrMaxLength) && TPY_check.Checked) EmptyField = true;
            if ((TPZ_text.TextLength < AddrMaxLength) && TPZ_check.Checked) EmptyField = true;

            if ((VRK_text.TextLength < AddrMaxLength) && VRK_check.Checked) EmptyField = true;
            if ((RKV_text.TextLength < AddrMaxLength) && RKV_check.Checked) EmptyField = true;

            if ((K1_text.TextLength < AddrMaxLength) && K1_check.Checked) EmptyField = true;
            if ((K2_text.TextLength < AddrMaxLength) && K2_check.Checked) EmptyField = true;
            if ((ResetK_text.TextLength < AddrMaxLength) && ResetK_check.Checked) EmptyField = true;

            if (!(YCX_check.Checked || YCY_check.Checked || YCZ_check.Checked || PX_check.Checked || PY_check.Checked ||
                PZ_check.Checked || TYCX_check.Checked || TYCYZ_check.Checked || TGIB_check.Checked || TPX_check.Checked ||
                TPY_check.Checked || TPZ_check.Checked || VRK_check.Checked)) NoCheckedAddr = true;

            if (RKV_check.Checked)
            {
                if (M1_text.TextLength == 0 || M1_text.Text == "0" || M2_text.TextLength == 0 || M2_text.Text == "0" ||
                    Convert.ToUInt16(M1_text.Text) >= Convert.ToUInt16(M2_text.Text))
                {
                    M_error = true;
                }
            }

            if (Ktemp_group.Enabled)
            {
                if (Ktemp_text.TextLength == 0 || Convert.ToDouble(Ktemp_text.Text) == 0) KT_empty = true;
            }

            if (EmptyField)
            {
                MessageBox.Show("Заполните все поля!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (NoCheckedAddr)
            {
                MessageBox.Show("Не выбран ни один адрес с данными!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (M_error)
            {
                MessageBox.Show("Некорректно заполнены поля переключения масштаба!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KT_empty)
            {
                MessageBox.Show("Некорректно введен коэффициент температуры!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            #region Question to user
            if (!NewProfile)
            {
                try
                {
                    //connect to db file
                    sqlConnection = new SQLiteConnection(sqLiteClass.sqlConnectString);
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Database not open!");
                        return;
                    }

                    sqlCommand.CommandText = "SELECT name FROM SystemProfiles WHERE id = '" + SystemID + "'";
                    SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();

                    sqlRead.Read();

                    if (sqlRead["Name"].ToString() != Name_text.Text)
                    {
                        if (MessageBox.Show("Создать новый профиль " + Name_text.Text + "?", "Создание профиля",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        {
                            sqlRead.Close();
                            return;
                        }

                        NewProfile = true;
                    }
                    else
                    {
                        if (MessageBox.Show("Изменить профиль " + Name_text.Text + "?", "Изменение профиля",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                        {
                            sqlRead.Close();
                            return;
                        }
                    }

                    sqlRead.Close();

                    sqlConnection.Close();
                    sqlCommand.Connection = sqlConnection;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex);
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Создать новый профиль " + Name_text.Text + "?", "Создание профиля", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No) return;
            }
            #endregion

            #region Fill DB

            if (NewProfile)
            {
                try
                {
                    //connect to db file
                    sqlConnection = new SQLiteConnection(sqLiteClass.sqlConnectString);
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Database not open!");
                        return;
                    }

                    sqlCommand.CommandText = "SELECT name FROM SystemProfiles";
                    SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();
                    while (sqlRead.Read())
                    {
                        if (sqlRead["name"].ToString() != Name_text.Text) continue;
                        MessageBox.Show("Данное имя профиля уже используется!", "Внимание!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        sqlRead.Close();
                        return;
                    }
                    sqlRead.Close();

                    string sqlQuery =
                    "INSERT INTO SystemProfiles ('lock', 'name', " +
                    "'ycxuse', 'ycyuse', 'yczuse', 'pxuse', 'pyuse', 'pzuse', 'vrkuse', " +
                    "'tycxuse', 'tycyzuse', 'tpxuse', 'tpyuse', 'tpzuse', 'tgibuse', " +
                    "'rkvuse', 'k1use', 'k2use', 'resetkuse', " +
                    "'ycx', 'ycy', 'ycz', 'px', 'py', 'pz', 'vrk', " +
                    "'tycx', 'tycyz', 'tpx', 'tpy', 'tpz', 'tgib', " +
                    "'rkv', 'k1', 'k2', 'resetk', 'M1', 'M2', 'KT') values ('";

                    sqlQuery += (Ctrl_check.Checked ? "Lock" : "Open") + "' , '";
                    sqlQuery += Name_text.Text + "' , '";

                    sqlQuery += (YCX_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (YCY_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (YCZ_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (PX_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (PY_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (PZ_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (VRK_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TYCX_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TYCYZ_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TPX_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TPY_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TPZ_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (TGIB_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (RKV_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (K1_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (K2_check.Checked ? "use" : "---") + "' , '";
                    sqlQuery += (ResetK_check.Checked ? "use" : "---") + "' , '";

                    sqlQuery += YCX_text.Text + "' , '";
                    sqlQuery += YCY_text.Text + "' , '";
                    sqlQuery += YCZ_text.Text + "' , '";
                    sqlQuery += PX_text.Text + "' , '";
                    sqlQuery += PY_text.Text + "' , '";
                    sqlQuery += PZ_text.Text + "' , '";
                    sqlQuery += VRK_text.Text + "' , '";
                    sqlQuery += TYCX_text.Text + "' , '";
                    sqlQuery += TYCYZ_text.Text + "' , '";
                    sqlQuery += TPX_text.Text + "' , '";
                    sqlQuery += TPY_text.Text + "' , '";
                    sqlQuery += TPZ_text.Text + "' , '";
                    sqlQuery += TGIB_text.Text + "' , '";
                    sqlQuery += RKV_text.Text + "' , '";
                    sqlQuery += K1_text.Text + "' , '";
                    sqlQuery += K2_text.Text + "' , '";
                    sqlQuery += ResetK_text.Text + "' , '";

                    sqlQuery += Convert.ToUInt16(M1_text.Text) + "' , '";
                    sqlQuery += Convert.ToUInt16(M2_text.Text) + "' , '";

                    sqlQuery += Convert.ToDouble(Ktemp_text.Text) + "')";
                    
                    sqlCommand.CommandText = sqlQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                    sqlCommand.Connection = sqlConnection;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    //connect to db file
                    sqlConnection = new SQLiteConnection(sqLiteClass.sqlConnectString);
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Database not open!");
                        return;
                    }

                    string sqlQuery = "UPDATE SystemProfiles SET ";

                    sqlQuery += "lock = '" + (Ctrl_check.Checked ? "Lock" : "Open") + "', ";
                    sqlQuery += "name = '" + Name_text.Text + "', ";

                    sqlQuery += "ycxuse = '" + (YCX_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "ycyuse = '" + (YCY_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "yczuse = '" + (YCZ_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "pxuse = '" + (PX_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "pyuse = '" + (PY_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "pzuse = '" + (PZ_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "vrkuse = '" + (VRK_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tycxuse = '" + (TYCX_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tycyzuse = '" + (TYCYZ_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tpxuse = '" + (TPX_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tpyuse = '" + (TPY_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tpzuse = '" + (TPZ_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "tgibuse = '" + (TGIB_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "rkvuse = '" + (RKV_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "k1use = '" + (K1_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "k2use = '" + (K2_check.Checked ? "use" : "---") + "', ";
                    sqlQuery += "resetkuse = '" + (ResetK_check.Checked ? "use" : "---") + "', ";

                    sqlQuery += "ycx = '" + YCX_text.Text + "', ";
                    sqlQuery += "ycy = '" + YCY_text.Text + "', ";
                    sqlQuery += "ycz = '" + YCZ_text.Text + "', ";
                    sqlQuery += "px = '" + PX_text.Text + "', ";
                    sqlQuery += "py = '" + PY_text.Text + "', ";
                    sqlQuery += "pz = '" + PZ_text.Text + "', ";
                    sqlQuery += "vrk = '" + VRK_text.Text + "', ";
                    sqlQuery += "tycx = '" + TYCX_text.Text + "', ";
                    sqlQuery += "tycyz = '" + TYCYZ_text.Text + "', ";
                    sqlQuery += "tpx = '" + TPX_text.Text + "', ";
                    sqlQuery += "tpy = '" + TPY_text.Text + "', ";
                    sqlQuery += "tpz = '" + TPZ_text.Text + "', ";
                    sqlQuery += "tgib = '" + TGIB_text.Text + "', ";
                    sqlQuery += "rkv = '" + RKV_text.Text + "', ";
                    sqlQuery += "k1 = '" + K1_text.Text + "', ";
                    sqlQuery += "k2 = '" + K2_text.Text + "', ";
                    sqlQuery += "resetk = '" + ResetK_text.Text + "', ";

                    sqlQuery += "M1 = '" + Convert.ToUInt16(M1_text.Text) + "', ";
                    sqlQuery += "M2 = '" + Convert.ToUInt16(M2_text.Text) + "', ";

                    sqlQuery += "KT = '" + Convert.ToDouble(Ktemp_text.Text) + "' ";
                    
                    sqlQuery += "WHERE id = '" + SystemID + "'";

                    sqlCommand.CommandText = sqlQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                    sqlCommand.Connection = sqlConnection;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }
            #endregion

            Form frm = Application.OpenForms[0];
            frm.Show();
            Close();
        }
        #endregion

        #region Base Methods
        private void CreateProfile()
        {
            CS_remove_button.Enabled = false;
            CS_save_button.Enabled = true;
            CS_back_button.Text = "Отмена";

            YCX_text.Text = "EC02";
            YCY_text.Text = "EC04";
            YCZ_text.Text = "EC06";
            PX_text.Text = "EC42";
            PY_text.Text = "EC44";
            PZ_text.Text = "EC46";

            TYCX_text.Text = "EE00";
            TYCYZ_text.Text = "EE02";
            TPX_text.Text = "ECE6";
            TPY_text.Text = "ECE8";
            TPZ_text.Text = "ECEA";

            VRK_text.Text = "EC80";
            RKV_text.Text = "ECA0";

            K1_text.Text = "ECE2";
            K2_text.Text = "ECE4";
            ResetK_text.Text = "ECE6";

            TGIB_text.Text = "ECE0";

            M1_text.Text = "17";
            M2_text.Text = "92";

            Ktemp_text.Text = "1,121";

            for (int i = 0; i < CheckMassive.Length; i++) CheckMassive[i] = true;
            CheckBoxChecker();
        }
        private void LoadProfile()
        {
            string LockStatus;

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
                    return;
                }

                sqlCommand.CommandText = "SELECT * FROM SystemProfiles WHERE id = '" + SystemID + "'";
                SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();

                sqlRead.Read();

                Name_text.Text = sqlRead["name"].ToString();

                YCX_text.Text = sqlRead["ycx"].ToString();
                YCY_text.Text = sqlRead["ycy"].ToString();
                YCZ_text.Text = sqlRead["ycz"].ToString();
                PX_text.Text = sqlRead["px"].ToString();
                PY_text.Text = sqlRead["py"].ToString();
                PZ_text.Text = sqlRead["pz"].ToString();

                TYCX_text.Text = sqlRead["tycx"].ToString();
                TYCYZ_text.Text = sqlRead["tycyz"].ToString();
                TPX_text.Text = sqlRead["tpx"].ToString();
                TPY_text.Text = sqlRead["tpy"].ToString();
                TPZ_text.Text = sqlRead["tpz"].ToString();

                TGIB_text.Text = sqlRead["tgib"].ToString();

                VRK_text.Text = sqlRead["vrk"].ToString();
                RKV_text.Text = sqlRead["rkv"].ToString();

                K1_text.Text = sqlRead["k1"].ToString();
                K2_text.Text = sqlRead["k2"].ToString();
                ResetK_text.Text = sqlRead["resetk"].ToString();

                int k = 0;
                CheckMassive[k++] = sqlRead["ycxuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["ycyuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["yczuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["pxuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["pyuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["pzuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["tycxuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["tycyzuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["tpxuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["tpyuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["tpzuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["vrkuse"].ToString() == "use";
                CheckMassive[k++] = sqlRead["rkvuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["k1use"].ToString() == "use";
                CheckMassive[k++] = sqlRead["k2use"].ToString() == "use";
                CheckMassive[k++] = sqlRead["resetkuse"].ToString() == "use";

                CheckMassive[k++] = sqlRead["tgibuse"].ToString() == "use";

                M1_text.Text = sqlRead["M1"].ToString();
                M2_text.Text = sqlRead["M2"].ToString();

                Ktemp_text.Text = sqlRead["KT"].ToString();
                
                LockStatus = sqlRead["lock"].ToString();
                sqlRead.Close();

                sqlConnection.Close();
                sqlCommand.Connection = sqlConnection;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            #endregion

            #region Visualisation
            CheckBoxChecker();

            if (LockStatus != "Lock")
            {
                CS_remove_button.Enabled = true;
                CS_save_button.Enabled = true;
                CS_back_button.Text = "Отмена";
                return;
            }

            Ctrl_check.AutoCheck = false;
            Ctrl_check.Checked = true;

            Name_text.ReadOnly = true;

            YCX_text.ReadOnly = true;
            YCY_text.ReadOnly = true;
            YCZ_text.ReadOnly = true;
            PX_text.ReadOnly = true;
            PY_text.ReadOnly = true;
            PZ_text.ReadOnly = true;

            TYCX_text.ReadOnly = true;
            TYCYZ_text.ReadOnly = true;

            TPX_text.ReadOnly = true;
            TPY_text.ReadOnly = true;
            TPZ_text.ReadOnly = true;

            TGIB_text.ReadOnly = true;

            Ktemp_text.ReadOnly = true;

            VRK_text.ReadOnly = true;
            RKV_text.ReadOnly = true;

            M1_text.ReadOnly = true;
            M2_text.ReadOnly = true;

            K1_text.ReadOnly = true;
            K2_text.ReadOnly = true;
            ResetK_text.ReadOnly = true;

            YCX_check.AutoCheck = false;
            YCY_check.AutoCheck = false;
            YCZ_check.AutoCheck = false;

            PX_check.AutoCheck = false;
            PY_check.AutoCheck = false;
            PZ_check.AutoCheck = false;

            TYCX_check.AutoCheck = false;
            TYCYZ_check.AutoCheck = false;
            TPX_check.AutoCheck = false;
            TPY_check.AutoCheck = false;
            TPZ_check.AutoCheck = false;

            VRK_check.AutoCheck = false;
            RKV_check.AutoCheck = false;

            K1_check.AutoCheck = false;
            K2_check.AutoCheck = false;
            ResetK_check.AutoCheck = false;

            TGIB_check.AutoCheck = false;
            
            CS_remove_button.Enabled = true;
            CS_save_button.Enabled = false;
            CS_back_button.Text = "Назад";
            #endregion
        }
        private void CheckBoxChecker()
        {
            int i = 0;
            YCX_text.Enabled = YCX_check.Checked = CheckMassive[i++];
            YCY_text.Enabled = YCY_check.Checked = CheckMassive[i++];
            YCZ_text.Enabled = YCZ_check.Checked = CheckMassive[i++];

            PX_text.Enabled = PX_check.Checked = CheckMassive[i++];
            PY_text.Enabled = PY_check.Checked = CheckMassive[i++];
            PZ_text.Enabled = PZ_check.Checked = CheckMassive[i++];

            TYCX_text.Enabled = TYCX_check.Checked = CheckMassive[i++];
            TYCYZ_text.Enabled = TYCYZ_check.Checked = CheckMassive[i++];
            TPX_text.Enabled = TPX_check.Checked = CheckMassive[i++];
            TPY_text.Enabled = TPY_check.Checked = CheckMassive[i++];
            TPZ_text.Enabled = TPZ_check.Checked = CheckMassive[i++];

            VRK_text.Enabled = VRK_check.Checked = CheckMassive[i++];
            VK_group.Enabled = VRK_check.Checked;

            M1_text.Enabled = M2_text.Enabled = RKV_text.Enabled = RKV_check.Checked = CheckMassive[i++];
            
            K1_text.Enabled = K1_check.Checked = CheckMassive[i++];
            K2_text.Enabled = K2_check.Checked = CheckMassive[i++];
            ResetK_text.Enabled = ResetK_check.Checked = CheckMassive[i++];

            if (!ResetK_check.Checked)
            {
                K1_text.Enabled = K1_check.Checked = K1_check.AutoCheck = false;
                K2_text.Enabled = K2_check.Checked = K2_check.AutoCheck = false;
            }

            TGIB_text.Enabled = TGIB_check.Checked = CheckMassive[i];

            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                 TPZ_check.Checked | TGIB_check.Checked;
        }
        #endregion

        #region CheckBoxes
        private void YCX_check_CheckedChanged(object sender, EventArgs e)
        {
            YCX_text.Enabled = YCX_check.Checked;
            Scale_group.Enabled = YCX_check.Checked | YCY_check.Checked | YCZ_check.Checked;
        }

        private void YCY_check_CheckedChanged(object sender, EventArgs e)
        {
            YCY_text.Enabled = YCY_check.Checked;
            Scale_group.Enabled = YCX_check.Checked | YCY_check.Checked | YCZ_check.Checked;
        }

        private void YCZ_check_CheckedChanged(object sender, EventArgs e)
        {
            YCZ_text.Enabled = YCZ_check.Checked;
            Scale_group.Enabled = YCX_check.Checked | YCY_check.Checked | YCZ_check.Checked;
        }

        private void PX_check_CheckedChanged(object sender, EventArgs e)
        {
            PX_text.Enabled = PX_check.Checked;
        }

        private void PY_check_CheckedChanged(object sender, EventArgs e)
        {
            PY_text.Enabled = PY_check.Checked;
        }

        private void PZ_check_CheckedChanged(object sender, EventArgs e)
        {
            PZ_text.Enabled = PZ_check.Checked;
        }

        private void TYCX_check_CheckedChanged(object sender, EventArgs e)
        {
            TYCX_text.Enabled = TYCX_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void TYCYZ_check_CheckedChanged(object sender, EventArgs e)
        {
            TYCYZ_text.Enabled = TYCYZ_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void TPX_check_CheckedChanged(object sender, EventArgs e)
        {
            TPX_text.Enabled = TPX_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void TPY_check_CheckedChanged(object sender, EventArgs e)
        {
            TPY_text.Enabled = TPY_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void TPZ_check_CheckedChanged(object sender, EventArgs e)
        {
            TPZ_text.Enabled = TPZ_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void TGIB_check_CheckedChanged(object sender, EventArgs e)
        {
            TGIB_text.Enabled = TGIB_check.Checked;
            Ktemp_group.Enabled = TYCX_check.Checked | TYCYZ_check.Checked | TPX_check.Checked | TPY_check.Checked |
                                  TPZ_check.Checked | TGIB_check.Checked;
        }

        private void K1_check_CheckedChanged(object sender, EventArgs e)
        {
            K1_text.Enabled = K1_check.Checked;
        }

        private void K2_check_CheckedChanged(object sender, EventArgs e)
        {
            K2_text.Enabled = K2_check.Checked;
        }

        private void ResetK_check_CheckedChanged(object sender, EventArgs e)
        {
            ResetK_text.Enabled = ResetK_check.Checked;

            if (ResetK_check.Checked)
            {
                K1_check.AutoCheck = true;
                K2_check.AutoCheck = true;
            }
            else
            {
                K1_text.Enabled = K1_check.Checked = K1_check.AutoCheck = false;
                K2_text.Enabled = K2_check.Checked = K2_check.AutoCheck = false;
            }
        }
        private void VRK_check_CheckedChanged(object sender, EventArgs e)
        {
            VRK_text.Enabled = VK_group.Enabled = VRK_check.Checked;
        }

        private void RKV_check_CheckedChanged(object sender, EventArgs e)
        {
            RKV_text.Enabled = M1_text.Enabled = M2_text.Enabled = RKV_check.Checked;
        }
        #endregion

        #region Formated Input
        private void TextBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char input = e.KeyChar;

            if (Char.IsDigit(input) || (input == 'A') || (input == 'B') || (input == 'C') || (input == 'D') ||
                (input == 'E') || (input == 'F') || (input == 8)) { }
            else e.Handled = true;
        }

        private void Name_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Name_text.TextLength == 0) e.Handled = e.KeyChar == ' ';

            if (e.KeyChar == '\\' || e.KeyChar == '/' || e.KeyChar == ':' || e.KeyChar == '*' || e.KeyChar == '?'
                || e.KeyChar == '"' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '|') e.Handled = true;
        }

        private void Scale_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }

        private void TempKoef_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.') e.KeyChar = ',';

            //1 точка и не в начале
            if (e.KeyChar == ',' &&
                ((sender as TextBox).Text.IndexOf(',') != -1 || (sender as TextBox).SelectionStart == 0))
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != ',') e.Handled = true;

            //если при удалении символа точка окажется в начале, то подставить в начало ноль
            if (e.KeyChar == 8 && (sender as TextBox).SelectionStart == 1 && (sender as TextBox).Text.IndexOf(',') == 1)
            {
                (sender as TextBox).Text = "0" + (sender as TextBox).Text.Remove(0, 1);
                e.Handled = true;
            }
                
        }
        #endregion

        private void Change_System_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frm = Application.OpenForms[0];
            frm.Show();
        }


    }
}
