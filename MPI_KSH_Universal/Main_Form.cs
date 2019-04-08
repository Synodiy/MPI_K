using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPI_KSH_Universal
{
    public partial class MainForm : Form
    {
        private string[] SystemName;
        private int[] SystemID;
        
        private SQLiteCommand sqlCommand;
        private SQLiteConnection sqlConnection;


        public MainForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            Text = "Выбор профиля";

            KitNum_text.Text = "№ комплекта";
            KitNum_text.ForeColor = Color.Gray;
            KitNum_text.TextAlign = HorizontalAlignment.Center;
            KitNum_text.MaxLength = 10;

            MF_system_combo.DropDownStyle = ComboBoxStyle.DropDownList;

            sqlConnection = new SQLiteConnection();
            sqlCommand = new SQLiteCommand();

            MF_change_button.Enabled = false;
            MF_Ok_button.Enabled = false;
            MF_create_button.Enabled = false;

            if (!Directory.Exists(sqLiteClass.sqlDirectory)) Directory.CreateDirectory(sqLiteClass.sqlDirectory);
            if (!File.Exists(sqLiteClass.sqlFileName)) SQLiteConnection.CreateFile(sqLiteClass.sqlFileName);

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

                //try create table
                sqlCommand.CommandText =
                    "CREATE TABLE IF NOT EXISTS SystemProfiles (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "lock TEXT, " +
                    "name TEXT, " +
                    "ycxuse TEXT, " +
                    "ycyuse TEXT, " +
                    "yczuse TEXT, " +
                    "pxuse TEXT, " +
                    "pyuse TEXT, " +
                    "pzuse TEXT, " +
                    "vrkuse TEXT, " +
                    "tycxuse TEXT, " +
                    "tycyzuse TEXT, " +
                    "tpxuse TEXT, " +
                    "tpyuse TEXT, " +
                    "tpzuse TEXT, " +
                    "tgibuse TEXT, " +
                    "rkvuse TEXT, " +
                    "k1use TEXT, " +
                    "k2use TEXT, " +
                    "resetkuse TEXT, " +
                    "ycx TEXT, " +
                    "ycy TEXT, " +
                    "ycz TEXT, " +
                    "px TEXT, " +
                    "py TEXT, " +
                    "pz TEXT, " +
                    "vrk TEXT, " +
                    "tycx TEXT, " +
                    "tycyz TEXT, " +
                    "tpx TEXT, " +
                    "tpy TEXT, " +
                    "tpz TEXT, " +
                    "tgib TEXT, " +
                    "rkv TEXT, " +
                    "k1 TEXT, " +
                    "k2 TEXT, " +
                    "resetk TEXT, " +
                    "M1 INTEGER, " +
                    "M2 INTEGER, " +
                    "KT TEXT" +
                    ")";
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                sqlCommand.Connection = sqlConnection;

                MF_create_button.Enabled = true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
        }

        private void MF_create_button_Click(object sender, EventArgs e)
        {
            Change_System_Form crtForm = new Change_System_Form(true);
            crtForm.Show();

            Hide();
        }

        private void MF_change_button_Click(object sender, EventArgs e)
        {
            Change_System_Form crtForm = new Change_System_Form(false, SystemID[MF_system_combo.SelectedIndex]);
            crtForm.Show();

            MF_system_combo.Items.Clear();
            MF_change_button.Enabled = false;
            MF_Ok_button.Enabled = false;
            Hide();
        }

        private void MF_Ok_button_Click(object sender, EventArgs e)
        {
            SystemInfoClass sysInfo = new SystemInfoClass(SystemID[MF_system_combo.SelectedIndex], KitNum_text.Text == "№ комплекта" ? "Общий" : KitNum_text.Text);

            if (sysInfo.sqlError) return;

            Work_Form wrkForm = new Work_Form(sysInfo);
            wrkForm.Show();

            Hide();
        }

        private void MF_system_combo_DropDown(object sender, EventArgs e)
        {
            MF_change_button.Enabled = false;
            MF_Ok_button.Enabled = false;

            #region Parse DataBase
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

                sqlCommand.CommandText = "SELECT id FROM SystemProfiles";
                SQLiteDataReader sqlRead = sqlCommand.ExecuteReader();
                int sqlCounter = 0;
                while (sqlRead.Read()) sqlCounter++;
                sqlRead.Close();

                sqlCommand.CommandText = "SELECT id, name FROM SystemProfiles";
                sqlRead = sqlCommand.ExecuteReader();
                SystemID = new int[sqlCounter];
                SystemName = new string[sqlCounter];
                for (int i = 0; i < sqlCounter; i ++)
                {
                    sqlRead.Read();
                    SystemID[i] = int.Parse(sqlRead["id"].ToString());
                    SystemName[i] = sqlRead["name"].ToString();
                }
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

            MF_system_combo.Items.Clear();
            MF_system_combo.Items.AddRange(SystemName);
        }

        private void MF_system_combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MF_change_button.Enabled = true;
            MF_Ok_button.Enabled = true;
        }

        private void KitNum_text_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(KitNum_text.Text)) return;
            KitNum_text.Text = "№ комплекта";
            KitNum_text.ForeColor = Color.Gray;
        }

        private void KitNum_text_Enter(object sender, EventArgs e)
        {
            if (KitNum_text.Text != "№ комплекта") return;
            KitNum_text.Clear();
            KitNum_text.ForeColor = Color.Black;
        }

        private void KitNum_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\\' || e.KeyChar == '/' || e.KeyChar == ':' || e.KeyChar == '*' || e.KeyChar == '?'
                || e.KeyChar == '"' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '|') e.Handled = true;
        }
    }
}
