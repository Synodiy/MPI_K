using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MPI_KSH_Universal
{
    partial class Work_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Work_Form));
            this.Back_button = new System.Windows.Forms.Button();
            this.STOP_button = new System.Windows.Forms.Button();
            this.ZeroGroup = new System.Windows.Forms.GroupBox();
            this.ZeroYCXgroup = new System.Windows.Forms.GroupBox();
            this.ZeroYCXtext = new System.Windows.Forms.TextBox();
            this.ZeroYCYgroup = new System.Windows.Forms.GroupBox();
            this.ZeroYCYtext = new System.Windows.Forms.TextBox();
            this.ZeroYCZgroup = new System.Windows.Forms.GroupBox();
            this.ZeroYCZtext = new System.Windows.Forms.TextBox();
            this.ZeroPXgroup = new System.Windows.Forms.GroupBox();
            this.ZeroPXtext = new System.Windows.Forms.TextBox();
            this.ZeroPZgroup = new System.Windows.Forms.GroupBox();
            this.ZeroPZtext = new System.Windows.Forms.TextBox();
            this.ZeroPYgroup = new System.Windows.Forms.GroupBox();
            this.ZeroPYtext = new System.Windows.Forms.TextBox();
            this.StatusGroup = new System.Windows.Forms.GroupBox();
            this.Statustext = new System.Windows.Forms.Label();
            this.ZPRGroup = new System.Windows.Forms.GroupBox();
            this.ZPRtext = new System.Windows.Forms.TextBox();
            this.TPZgroup = new System.Windows.Forms.GroupBox();
            this.TPZtext = new System.Windows.Forms.TextBox();
            this.PZgroup = new System.Windows.Forms.GroupBox();
            this.PZtext = new System.Windows.Forms.TextBox();
            this.TPYgroup = new System.Windows.Forms.GroupBox();
            this.TPYtext = new System.Windows.Forms.TextBox();
            this.TYCYZgroup = new System.Windows.Forms.GroupBox();
            this.TYCYZtext = new System.Windows.Forms.TextBox();
            this.PYgroup = new System.Windows.Forms.GroupBox();
            this.PYtext = new System.Windows.Forms.TextBox();
            this.TYCXgroup = new System.Windows.Forms.GroupBox();
            this.TYCXtext = new System.Windows.Forms.TextBox();
            this.TPXgroup = new System.Windows.Forms.GroupBox();
            this.TPXtext = new System.Windows.Forms.TextBox();
            this.YCZgroup = new System.Windows.Forms.GroupBox();
            this.YCZtext = new System.Windows.Forms.TextBox();
            this.PXgroup = new System.Windows.Forms.GroupBox();
            this.PXtext = new System.Windows.Forms.TextBox();
            this.YCYgroup = new System.Windows.Forms.GroupBox();
            this.YCYtext = new System.Windows.Forms.TextBox();
            this.YCXgroup = new System.Windows.Forms.GroupBox();
            this.YCXtext = new System.Windows.Forms.TextBox();
            this.CurrentGroup = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.TemperatureGroup = new System.Windows.Forms.GroupBox();
            this.TGIBgroup = new System.Windows.Forms.GroupBox();
            this.TGIBtext = new System.Windows.Forms.TextBox();
            this.VKGroup = new System.Windows.Forms.GroupBox();
            this.K1_button = new System.Windows.Forms.Button();
            this.K2_button = new System.Windows.Forms.Button();
            this.ResetK_button = new System.Windows.Forms.Button();
            this.ConvertToCSV_Button = new System.Windows.Forms.Button();
            this.FormConsole = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.START_button = new System.Windows.Forms.Button();
            this.ButtonsGroup = new System.Windows.Forms.GroupBox();
            this.ZeroGroup.SuspendLayout();
            this.ZeroYCXgroup.SuspendLayout();
            this.ZeroYCYgroup.SuspendLayout();
            this.ZeroYCZgroup.SuspendLayout();
            this.ZeroPXgroup.SuspendLayout();
            this.ZeroPZgroup.SuspendLayout();
            this.ZeroPYgroup.SuspendLayout();
            this.StatusGroup.SuspendLayout();
            this.ZPRGroup.SuspendLayout();
            this.TPZgroup.SuspendLayout();
            this.PZgroup.SuspendLayout();
            this.TPYgroup.SuspendLayout();
            this.TYCYZgroup.SuspendLayout();
            this.PYgroup.SuspendLayout();
            this.TYCXgroup.SuspendLayout();
            this.TPXgroup.SuspendLayout();
            this.YCZgroup.SuspendLayout();
            this.PXgroup.SuspendLayout();
            this.YCYgroup.SuspendLayout();
            this.YCXgroup.SuspendLayout();
            this.CurrentGroup.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.TemperatureGroup.SuspendLayout();
            this.TGIBgroup.SuspendLayout();
            this.VKGroup.SuspendLayout();
            this.ButtonsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Back_button
            // 
            this.Back_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Back_button.Location = new System.Drawing.Point(6, 416);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(130, 100);
            this.Back_button.TabIndex = 0;
            this.Back_button.Text = "Назад";
            this.Back_button.UseVisualStyleBackColor = true;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // STOP_button
            // 
            this.STOP_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.STOP_button.Location = new System.Drawing.Point(6, 134);
            this.STOP_button.Name = "STOP_button";
            this.STOP_button.Size = new System.Drawing.Size(130, 100);
            this.STOP_button.TabIndex = 2;
            this.STOP_button.Text = "СТОП";
            this.STOP_button.UseVisualStyleBackColor = true;
            this.STOP_button.Click += new System.EventHandler(this.STOP_button_Click);
            // 
            // ZeroGroup
            // 
            this.ZeroGroup.Controls.Add(this.ZeroYCXgroup);
            this.ZeroGroup.Controls.Add(this.ZeroYCYgroup);
            this.ZeroGroup.Controls.Add(this.ZeroYCZgroup);
            this.ZeroGroup.Controls.Add(this.ZeroPXgroup);
            this.ZeroGroup.Controls.Add(this.ZeroPZgroup);
            this.ZeroGroup.Controls.Add(this.ZeroPYgroup);
            this.ZeroGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroGroup.Location = new System.Drawing.Point(12, 12);
            this.ZeroGroup.Name = "ZeroGroup";
            this.ZeroGroup.Size = new System.Drawing.Size(915, 110);
            this.ZeroGroup.TabIndex = 35;
            this.ZeroGroup.TabStop = false;
            this.ZeroGroup.Text = "Нулевые сигналы";
            // 
            // ZeroYCXgroup
            // 
            this.ZeroYCXgroup.Controls.Add(this.ZeroYCXtext);
            this.ZeroYCXgroup.Location = new System.Drawing.Point(6, 27);
            this.ZeroYCXgroup.Name = "ZeroYCXgroup";
            this.ZeroYCXgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroYCXgroup.TabIndex = 10;
            this.ZeroYCXgroup.TabStop = false;
            this.ZeroYCXgroup.Text = "УСX";
            // 
            // ZeroYCXtext
            // 
            this.ZeroYCXtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroYCXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroYCXtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroYCXtext.Name = "ZeroYCXtext";
            this.ZeroYCXtext.ReadOnly = true;
            this.ZeroYCXtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroYCXtext.TabIndex = 0;
            this.ZeroYCXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ZeroYCYgroup
            // 
            this.ZeroYCYgroup.Controls.Add(this.ZeroYCYtext);
            this.ZeroYCYgroup.Location = new System.Drawing.Point(157, 27);
            this.ZeroYCYgroup.Name = "ZeroYCYgroup";
            this.ZeroYCYgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroYCYgroup.TabIndex = 9;
            this.ZeroYCYgroup.TabStop = false;
            this.ZeroYCYgroup.Text = "УСY";
            // 
            // ZeroYCYtext
            // 
            this.ZeroYCYtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroYCYtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroYCYtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroYCYtext.Name = "ZeroYCYtext";
            this.ZeroYCYtext.ReadOnly = true;
            this.ZeroYCYtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroYCYtext.TabIndex = 0;
            this.ZeroYCYtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ZeroYCZgroup
            // 
            this.ZeroYCZgroup.Controls.Add(this.ZeroYCZtext);
            this.ZeroYCZgroup.Location = new System.Drawing.Point(308, 27);
            this.ZeroYCZgroup.Name = "ZeroYCZgroup";
            this.ZeroYCZgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroYCZgroup.TabIndex = 8;
            this.ZeroYCZgroup.TabStop = false;
            this.ZeroYCZgroup.Text = "УСZ";
            // 
            // ZeroYCZtext
            // 
            this.ZeroYCZtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroYCZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroYCZtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroYCZtext.Name = "ZeroYCZtext";
            this.ZeroYCZtext.ReadOnly = true;
            this.ZeroYCZtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroYCZtext.TabIndex = 0;
            this.ZeroYCZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ZeroPXgroup
            // 
            this.ZeroPXgroup.Controls.Add(this.ZeroPXtext);
            this.ZeroPXgroup.Location = new System.Drawing.Point(459, 27);
            this.ZeroPXgroup.Name = "ZeroPXgroup";
            this.ZeroPXgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroPXgroup.TabIndex = 8;
            this.ZeroPXgroup.TabStop = false;
            this.ZeroPXgroup.Text = "ПX";
            // 
            // ZeroPXtext
            // 
            this.ZeroPXtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroPXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroPXtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroPXtext.Name = "ZeroPXtext";
            this.ZeroPXtext.ReadOnly = true;
            this.ZeroPXtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroPXtext.TabIndex = 0;
            this.ZeroPXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ZeroPZgroup
            // 
            this.ZeroPZgroup.Controls.Add(this.ZeroPZtext);
            this.ZeroPZgroup.Location = new System.Drawing.Point(761, 27);
            this.ZeroPZgroup.Name = "ZeroPZgroup";
            this.ZeroPZgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroPZgroup.TabIndex = 8;
            this.ZeroPZgroup.TabStop = false;
            this.ZeroPZgroup.Text = "ПZ";
            // 
            // ZeroPZtext
            // 
            this.ZeroPZtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroPZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroPZtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroPZtext.Name = "ZeroPZtext";
            this.ZeroPZtext.ReadOnly = true;
            this.ZeroPZtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroPZtext.TabIndex = 0;
            this.ZeroPZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ZeroPYgroup
            // 
            this.ZeroPYgroup.Controls.Add(this.ZeroPYtext);
            this.ZeroPYgroup.Location = new System.Drawing.Point(610, 27);
            this.ZeroPYgroup.Name = "ZeroPYgroup";
            this.ZeroPYgroup.Size = new System.Drawing.Size(145, 75);
            this.ZeroPYgroup.TabIndex = 8;
            this.ZeroPYgroup.TabStop = false;
            this.ZeroPYgroup.Text = "ПY";
            // 
            // ZeroPYtext
            // 
            this.ZeroPYtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZeroPYtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZeroPYtext.Location = new System.Drawing.Point(6, 28);
            this.ZeroPYtext.Name = "ZeroPYtext";
            this.ZeroPYtext.ReadOnly = true;
            this.ZeroPYtext.Size = new System.Drawing.Size(130, 35);
            this.ZeroPYtext.TabIndex = 0;
            this.ZeroPYtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StatusGroup
            // 
            this.StatusGroup.Controls.Add(this.Statustext);
            this.StatusGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatusGroup.Location = new System.Drawing.Point(18, 353);
            this.StatusGroup.Name = "StatusGroup";
            this.StatusGroup.Size = new System.Drawing.Size(145, 75);
            this.StatusGroup.TabIndex = 34;
            this.StatusGroup.TabStop = false;
            this.StatusGroup.Text = "Статус";
            // 
            // Statustext
            // 
            this.Statustext.AutoSize = true;
            this.Statustext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Statustext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Statustext.Location = new System.Drawing.Point(7, 31);
            this.Statustext.Name = "Statustext";
            this.Statustext.Size = new System.Drawing.Size(121, 25);
            this.Statustext.TabIndex = 11;
            this.Statustext.Text = "ГИБ готов";
            // 
            // ZPRGroup
            // 
            this.ZPRGroup.Controls.Add(this.ZPRtext);
            this.ZPRGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZPRGroup.Location = new System.Drawing.Point(773, 353);
            this.ZPRGroup.Name = "ZPRGroup";
            this.ZPRGroup.Size = new System.Drawing.Size(145, 75);
            this.ZPRGroup.TabIndex = 22;
            this.ZPRGroup.TabStop = false;
            this.ZPRGroup.Text = "Время ЗПР";
            // 
            // ZPRtext
            // 
            this.ZPRtext.BackColor = System.Drawing.SystemColors.Window;
            this.ZPRtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ZPRtext.Location = new System.Drawing.Point(6, 28);
            this.ZPRtext.Name = "ZPRtext";
            this.ZPRtext.ReadOnly = true;
            this.ZPRtext.Size = new System.Drawing.Size(130, 35);
            this.ZPRtext.TabIndex = 0;
            this.ZPRtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TPZgroup
            // 
            this.TPZgroup.Controls.Add(this.TPZtext);
            this.TPZgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPZgroup.Location = new System.Drawing.Point(459, 28);
            this.TPZgroup.Name = "TPZgroup";
            this.TPZgroup.Size = new System.Drawing.Size(145, 75);
            this.TPZgroup.TabIndex = 23;
            this.TPZgroup.TabStop = false;
            this.TPZgroup.Text = "ТПZ";
            // 
            // TPZtext
            // 
            this.TPZtext.BackColor = System.Drawing.SystemColors.Window;
            this.TPZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPZtext.Location = new System.Drawing.Point(6, 28);
            this.TPZtext.Name = "TPZtext";
            this.TPZtext.ReadOnly = true;
            this.TPZtext.Size = new System.Drawing.Size(130, 35);
            this.TPZtext.TabIndex = 0;
            this.TPZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PZgroup
            // 
            this.PZgroup.Controls.Add(this.PZtext);
            this.PZgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PZgroup.Location = new System.Drawing.Point(761, 28);
            this.PZgroup.Name = "PZgroup";
            this.PZgroup.Size = new System.Drawing.Size(145, 75);
            this.PZgroup.TabIndex = 24;
            this.PZgroup.TabStop = false;
            this.PZgroup.Text = "ПZ";
            // 
            // PZtext
            // 
            this.PZtext.BackColor = System.Drawing.SystemColors.Window;
            this.PZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PZtext.Location = new System.Drawing.Point(6, 28);
            this.PZtext.Name = "PZtext";
            this.PZtext.ReadOnly = true;
            this.PZtext.Size = new System.Drawing.Size(130, 35);
            this.PZtext.TabIndex = 0;
            this.PZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TPYgroup
            // 
            this.TPYgroup.Controls.Add(this.TPYtext);
            this.TPYgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPYgroup.Location = new System.Drawing.Point(308, 28);
            this.TPYgroup.Name = "TPYgroup";
            this.TPYgroup.Size = new System.Drawing.Size(145, 75);
            this.TPYgroup.TabIndex = 25;
            this.TPYgroup.TabStop = false;
            this.TPYgroup.Text = "ТПY";
            // 
            // TPYtext
            // 
            this.TPYtext.BackColor = System.Drawing.SystemColors.Window;
            this.TPYtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPYtext.Location = new System.Drawing.Point(6, 28);
            this.TPYtext.Name = "TPYtext";
            this.TPYtext.ReadOnly = true;
            this.TPYtext.Size = new System.Drawing.Size(130, 35);
            this.TPYtext.TabIndex = 0;
            this.TPYtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TYCYZgroup
            // 
            this.TYCYZgroup.Controls.Add(this.TYCYZtext);
            this.TYCYZgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TYCYZgroup.Location = new System.Drawing.Point(761, 28);
            this.TYCYZgroup.Name = "TYCYZgroup";
            this.TYCYZgroup.Size = new System.Drawing.Size(145, 75);
            this.TYCYZgroup.TabIndex = 26;
            this.TYCYZgroup.TabStop = false;
            this.TYCYZgroup.Text = "ТУСYZ";
            // 
            // TYCYZtext
            // 
            this.TYCYZtext.BackColor = System.Drawing.SystemColors.Window;
            this.TYCYZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TYCYZtext.Location = new System.Drawing.Point(6, 28);
            this.TYCYZtext.Name = "TYCYZtext";
            this.TYCYZtext.ReadOnly = true;
            this.TYCYZtext.Size = new System.Drawing.Size(130, 35);
            this.TYCYZtext.TabIndex = 0;
            this.TYCYZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PYgroup
            // 
            this.PYgroup.Controls.Add(this.PYtext);
            this.PYgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PYgroup.Location = new System.Drawing.Point(610, 28);
            this.PYgroup.Name = "PYgroup";
            this.PYgroup.Size = new System.Drawing.Size(145, 75);
            this.PYgroup.TabIndex = 27;
            this.PYgroup.TabStop = false;
            this.PYgroup.Text = "ПY";
            // 
            // PYtext
            // 
            this.PYtext.BackColor = System.Drawing.SystemColors.Window;
            this.PYtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PYtext.Location = new System.Drawing.Point(6, 28);
            this.PYtext.Name = "PYtext";
            this.PYtext.ReadOnly = true;
            this.PYtext.Size = new System.Drawing.Size(130, 35);
            this.PYtext.TabIndex = 0;
            this.PYtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TYCXgroup
            // 
            this.TYCXgroup.Controls.Add(this.TYCXtext);
            this.TYCXgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TYCXgroup.Location = new System.Drawing.Point(610, 28);
            this.TYCXgroup.Name = "TYCXgroup";
            this.TYCXgroup.Size = new System.Drawing.Size(145, 75);
            this.TYCXgroup.TabIndex = 28;
            this.TYCXgroup.TabStop = false;
            this.TYCXgroup.Text = "ТУСХ";
            // 
            // TYCXtext
            // 
            this.TYCXtext.BackColor = System.Drawing.SystemColors.Window;
            this.TYCXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TYCXtext.Location = new System.Drawing.Point(6, 28);
            this.TYCXtext.Name = "TYCXtext";
            this.TYCXtext.ReadOnly = true;
            this.TYCXtext.Size = new System.Drawing.Size(130, 35);
            this.TYCXtext.TabIndex = 0;
            this.TYCXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TPXgroup
            // 
            this.TPXgroup.Controls.Add(this.TPXtext);
            this.TPXgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPXgroup.Location = new System.Drawing.Point(157, 28);
            this.TPXgroup.Name = "TPXgroup";
            this.TPXgroup.Size = new System.Drawing.Size(145, 75);
            this.TPXgroup.TabIndex = 29;
            this.TPXgroup.TabStop = false;
            this.TPXgroup.Text = "ТПХ";
            // 
            // TPXtext
            // 
            this.TPXtext.BackColor = System.Drawing.SystemColors.Window;
            this.TPXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TPXtext.Location = new System.Drawing.Point(6, 28);
            this.TPXtext.Name = "TPXtext";
            this.TPXtext.ReadOnly = true;
            this.TPXtext.Size = new System.Drawing.Size(130, 35);
            this.TPXtext.TabIndex = 0;
            this.TPXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // YCZgroup
            // 
            this.YCZgroup.Controls.Add(this.YCZtext);
            this.YCZgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCZgroup.Location = new System.Drawing.Point(308, 28);
            this.YCZgroup.Name = "YCZgroup";
            this.YCZgroup.Size = new System.Drawing.Size(145, 75);
            this.YCZgroup.TabIndex = 30;
            this.YCZgroup.TabStop = false;
            this.YCZgroup.Text = "УСZ";
            // 
            // YCZtext
            // 
            this.YCZtext.BackColor = System.Drawing.SystemColors.Window;
            this.YCZtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCZtext.Location = new System.Drawing.Point(6, 28);
            this.YCZtext.Name = "YCZtext";
            this.YCZtext.ReadOnly = true;
            this.YCZtext.Size = new System.Drawing.Size(130, 35);
            this.YCZtext.TabIndex = 0;
            this.YCZtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PXgroup
            // 
            this.PXgroup.Controls.Add(this.PXtext);
            this.PXgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PXgroup.Location = new System.Drawing.Point(459, 28);
            this.PXgroup.Name = "PXgroup";
            this.PXgroup.Size = new System.Drawing.Size(145, 75);
            this.PXgroup.TabIndex = 31;
            this.PXgroup.TabStop = false;
            this.PXgroup.Text = "ПX";
            // 
            // PXtext
            // 
            this.PXtext.BackColor = System.Drawing.SystemColors.Window;
            this.PXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PXtext.Location = new System.Drawing.Point(6, 28);
            this.PXtext.Name = "PXtext";
            this.PXtext.ReadOnly = true;
            this.PXtext.Size = new System.Drawing.Size(130, 35);
            this.PXtext.TabIndex = 0;
            this.PXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // YCYgroup
            // 
            this.YCYgroup.Controls.Add(this.YCYtext);
            this.YCYgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCYgroup.Location = new System.Drawing.Point(157, 28);
            this.YCYgroup.Name = "YCYgroup";
            this.YCYgroup.Size = new System.Drawing.Size(145, 75);
            this.YCYgroup.TabIndex = 32;
            this.YCYgroup.TabStop = false;
            this.YCYgroup.Text = "УСY";
            // 
            // YCYtext
            // 
            this.YCYtext.BackColor = System.Drawing.SystemColors.Window;
            this.YCYtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCYtext.Location = new System.Drawing.Point(6, 28);
            this.YCYtext.Name = "YCYtext";
            this.YCYtext.ReadOnly = true;
            this.YCYtext.Size = new System.Drawing.Size(130, 35);
            this.YCYtext.TabIndex = 0;
            this.YCYtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // YCXgroup
            // 
            this.YCXgroup.Controls.Add(this.YCXtext);
            this.YCXgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCXgroup.Location = new System.Drawing.Point(6, 28);
            this.YCXgroup.Name = "YCXgroup";
            this.YCXgroup.Size = new System.Drawing.Size(145, 75);
            this.YCXgroup.TabIndex = 33;
            this.YCXgroup.TabStop = false;
            this.YCXgroup.Text = "УСX";
            // 
            // YCXtext
            // 
            this.YCXtext.BackColor = System.Drawing.SystemColors.Window;
            this.YCXtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YCXtext.Location = new System.Drawing.Point(6, 28);
            this.YCXtext.Name = "YCXtext";
            this.YCXtext.ReadOnly = true;
            this.YCXtext.Size = new System.Drawing.Size(130, 35);
            this.YCXtext.TabIndex = 0;
            this.YCXtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CurrentGroup
            // 
            this.CurrentGroup.Controls.Add(this.groupBox12);
            this.CurrentGroup.Controls.Add(this.YCXgroup);
            this.CurrentGroup.Controls.Add(this.YCYgroup);
            this.CurrentGroup.Controls.Add(this.PXgroup);
            this.CurrentGroup.Controls.Add(this.YCZgroup);
            this.CurrentGroup.Controls.Add(this.PYgroup);
            this.CurrentGroup.Controls.Add(this.PZgroup);
            this.CurrentGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentGroup.Location = new System.Drawing.Point(12, 128);
            this.CurrentGroup.Name = "CurrentGroup";
            this.CurrentGroup.Size = new System.Drawing.Size(915, 110);
            this.CurrentGroup.TabIndex = 36;
            this.CurrentGroup.TabStop = false;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.groupBox14);
            this.groupBox12.Controls.Add(this.groupBox15);
            this.groupBox12.Controls.Add(this.groupBox16);
            this.groupBox12.Controls.Add(this.groupBox17);
            this.groupBox12.Controls.Add(this.groupBox18);
            this.groupBox12.Controls.Add(this.groupBox19);
            this.groupBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox12.Location = new System.Drawing.Point(0, 109);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(915, 110);
            this.groupBox12.TabIndex = 37;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Нулевые сигналы";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.textBox12);
            this.groupBox14.Location = new System.Drawing.Point(6, 27);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(145, 75);
            this.groupBox14.TabIndex = 10;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "УСX";
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.SystemColors.Window;
            this.textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox12.Location = new System.Drawing.Point(6, 25);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(130, 35);
            this.textBox12.TabIndex = 0;
            this.textBox12.Text = "-40965550";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.textBox14);
            this.groupBox15.Location = new System.Drawing.Point(157, 27);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(145, 75);
            this.groupBox15.TabIndex = 9;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "УСY";
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Window;
            this.textBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox14.Location = new System.Drawing.Point(6, 25);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(130, 35);
            this.textBox14.TabIndex = 0;
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.textBox15);
            this.groupBox16.Location = new System.Drawing.Point(308, 27);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(145, 75);
            this.groupBox16.TabIndex = 8;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "УСZ";
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.SystemColors.Window;
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox15.Location = new System.Drawing.Point(6, 25);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(130, 35);
            this.textBox15.TabIndex = 0;
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.textBox16);
            this.groupBox17.Location = new System.Drawing.Point(459, 27);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(145, 75);
            this.groupBox17.TabIndex = 8;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "ПX";
            // 
            // textBox16
            // 
            this.textBox16.BackColor = System.Drawing.SystemColors.Window;
            this.textBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox16.Location = new System.Drawing.Point(6, 25);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(130, 35);
            this.textBox16.TabIndex = 0;
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.textBox17);
            this.groupBox18.Location = new System.Drawing.Point(761, 27);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(145, 75);
            this.groupBox18.TabIndex = 8;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "ПZ";
            // 
            // textBox17
            // 
            this.textBox17.BackColor = System.Drawing.SystemColors.Window;
            this.textBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox17.Location = new System.Drawing.Point(6, 25);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(130, 35);
            this.textBox17.TabIndex = 0;
            this.textBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.textBox18);
            this.groupBox19.Location = new System.Drawing.Point(610, 27);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(145, 75);
            this.groupBox19.TabIndex = 8;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "ПY";
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.SystemColors.Window;
            this.textBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox18.Location = new System.Drawing.Point(6, 25);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(130, 35);
            this.textBox18.TabIndex = 0;
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TemperatureGroup
            // 
            this.TemperatureGroup.Controls.Add(this.TGIBgroup);
            this.TemperatureGroup.Controls.Add(this.TPXgroup);
            this.TemperatureGroup.Controls.Add(this.TYCXgroup);
            this.TemperatureGroup.Controls.Add(this.TYCYZgroup);
            this.TemperatureGroup.Controls.Add(this.TPYgroup);
            this.TemperatureGroup.Controls.Add(this.TPZgroup);
            this.TemperatureGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TemperatureGroup.Location = new System.Drawing.Point(12, 237);
            this.TemperatureGroup.Name = "TemperatureGroup";
            this.TemperatureGroup.Size = new System.Drawing.Size(915, 110);
            this.TemperatureGroup.TabIndex = 37;
            this.TemperatureGroup.TabStop = false;
            this.TemperatureGroup.Text = "Температура";
            // 
            // TGIBgroup
            // 
            this.TGIBgroup.Controls.Add(this.TGIBtext);
            this.TGIBgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TGIBgroup.Location = new System.Drawing.Point(6, 28);
            this.TGIBgroup.Name = "TGIBgroup";
            this.TGIBgroup.Size = new System.Drawing.Size(145, 75);
            this.TGIBgroup.TabIndex = 30;
            this.TGIBgroup.TabStop = false;
            this.TGIBgroup.Text = "Тгиб";
            // 
            // TGIBtext
            // 
            this.TGIBtext.BackColor = System.Drawing.SystemColors.Window;
            this.TGIBtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TGIBtext.Location = new System.Drawing.Point(6, 28);
            this.TGIBtext.Name = "TGIBtext";
            this.TGIBtext.ReadOnly = true;
            this.TGIBtext.Size = new System.Drawing.Size(130, 35);
            this.TGIBtext.TabIndex = 0;
            this.TGIBtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VKGroup
            // 
            this.VKGroup.Controls.Add(this.K1_button);
            this.VKGroup.Controls.Add(this.K2_button);
            this.VKGroup.Controls.Add(this.ResetK_button);
            this.VKGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VKGroup.Location = new System.Drawing.Point(169, 353);
            this.VKGroup.Name = "VKGroup";
            this.VKGroup.Size = new System.Drawing.Size(447, 75);
            this.VKGroup.TabIndex = 21;
            this.VKGroup.TabStop = false;
            this.VKGroup.Text = "Встроенный контроль";
            // 
            // K1_button
            // 
            this.K1_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.K1_button.Location = new System.Drawing.Point(6, 28);
            this.K1_button.Name = "K1_button";
            this.K1_button.Size = new System.Drawing.Size(130, 40);
            this.K1_button.TabIndex = 0;
            this.K1_button.Text = "K1";
            this.K1_button.UseVisualStyleBackColor = true;
            this.K1_button.Click += new System.EventHandler(this.K1_button_Click);
            // 
            // K2_button
            // 
            this.K2_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.K2_button.Location = new System.Drawing.Point(157, 28);
            this.K2_button.Name = "K2_button";
            this.K2_button.Size = new System.Drawing.Size(130, 40);
            this.K2_button.TabIndex = 2;
            this.K2_button.Text = "K2";
            this.K2_button.UseVisualStyleBackColor = true;
            this.K2_button.Click += new System.EventHandler(this.K2_button_Click);
            // 
            // ResetK_button
            // 
            this.ResetK_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetK_button.Location = new System.Drawing.Point(308, 28);
            this.ResetK_button.Name = "ResetK_button";
            this.ResetK_button.Size = new System.Drawing.Size(130, 40);
            this.ResetK_button.TabIndex = 0;
            this.ResetK_button.Text = "Сброс \r";
            this.ResetK_button.UseVisualStyleBackColor = true;
            this.ResetK_button.Click += new System.EventHandler(this.ResetK_button_Click);
            // 
            // ConvertToCSV_Button
            // 
            this.ConvertToCSV_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConvertToCSV_Button.Location = new System.Drawing.Point(6, 240);
            this.ConvertToCSV_Button.Name = "ConvertToCSV_Button";
            this.ConvertToCSV_Button.Size = new System.Drawing.Size(130, 100);
            this.ConvertToCSV_Button.TabIndex = 15;
            this.ConvertToCSV_Button.Text = "Сохранить файл";
            this.ConvertToCSV_Button.UseVisualStyleBackColor = true;
            this.ConvertToCSV_Button.Click += new System.EventHandler(this.ConvertToCSV_Button_Click);
            // 
            // FormConsole
            // 
            this.FormConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormConsole.Location = new System.Drawing.Point(12, 434);
            this.FormConsole.Name = "FormConsole";
            this.FormConsole.ReadOnly = true;
            this.FormConsole.Size = new System.Drawing.Size(906, 100);
            this.FormConsole.TabIndex = 38;
            this.FormConsole.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // START_button
            // 
            this.START_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.START_button.Location = new System.Drawing.Point(6, 28);
            this.START_button.Name = "START_button";
            this.START_button.Size = new System.Drawing.Size(130, 100);
            this.START_button.TabIndex = 0;
            this.START_button.Text = "ПУСК";
            this.START_button.UseVisualStyleBackColor = true;
            this.START_button.Click += new System.EventHandler(this.START_button_Click);
            // 
            // ButtonsGroup
            // 
            this.ButtonsGroup.Controls.Add(this.START_button);
            this.ButtonsGroup.Controls.Add(this.Back_button);
            this.ButtonsGroup.Controls.Add(this.STOP_button);
            this.ButtonsGroup.Controls.Add(this.ConvertToCSV_Button);
            this.ButtonsGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonsGroup.Location = new System.Drawing.Point(933, 12);
            this.ButtonsGroup.Name = "ButtonsGroup";
            this.ButtonsGroup.Size = new System.Drawing.Size(142, 522);
            this.ButtonsGroup.TabIndex = 39;
            this.ButtonsGroup.TabStop = false;
            // 
            // Work_Form
            // 
            this.ClientSize = new System.Drawing.Size(1085, 543);
            this.Controls.Add(this.ButtonsGroup);
            this.Controls.Add(this.FormConsole);
            this.Controls.Add(this.TemperatureGroup);
            this.Controls.Add(this.CurrentGroup);
            this.Controls.Add(this.ZeroGroup);
            this.Controls.Add(this.StatusGroup);
            this.Controls.Add(this.ZPRGroup);
            this.Controls.Add(this.VKGroup);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Work_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Work_Form_FormClosed);
            this.ZeroGroup.ResumeLayout(false);
            this.ZeroYCXgroup.ResumeLayout(false);
            this.ZeroYCXgroup.PerformLayout();
            this.ZeroYCYgroup.ResumeLayout(false);
            this.ZeroYCYgroup.PerformLayout();
            this.ZeroYCZgroup.ResumeLayout(false);
            this.ZeroYCZgroup.PerformLayout();
            this.ZeroPXgroup.ResumeLayout(false);
            this.ZeroPXgroup.PerformLayout();
            this.ZeroPZgroup.ResumeLayout(false);
            this.ZeroPZgroup.PerformLayout();
            this.ZeroPYgroup.ResumeLayout(false);
            this.ZeroPYgroup.PerformLayout();
            this.StatusGroup.ResumeLayout(false);
            this.StatusGroup.PerformLayout();
            this.ZPRGroup.ResumeLayout(false);
            this.ZPRGroup.PerformLayout();
            this.TPZgroup.ResumeLayout(false);
            this.TPZgroup.PerformLayout();
            this.PZgroup.ResumeLayout(false);
            this.PZgroup.PerformLayout();
            this.TPYgroup.ResumeLayout(false);
            this.TPYgroup.PerformLayout();
            this.TYCYZgroup.ResumeLayout(false);
            this.TYCYZgroup.PerformLayout();
            this.PYgroup.ResumeLayout(false);
            this.PYgroup.PerformLayout();
            this.TYCXgroup.ResumeLayout(false);
            this.TYCXgroup.PerformLayout();
            this.TPXgroup.ResumeLayout(false);
            this.TPXgroup.PerformLayout();
            this.YCZgroup.ResumeLayout(false);
            this.YCZgroup.PerformLayout();
            this.PXgroup.ResumeLayout(false);
            this.PXgroup.PerformLayout();
            this.YCYgroup.ResumeLayout(false);
            this.YCYgroup.PerformLayout();
            this.YCXgroup.ResumeLayout(false);
            this.YCXgroup.PerformLayout();
            this.CurrentGroup.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.TemperatureGroup.ResumeLayout(false);
            this.TGIBgroup.ResumeLayout(false);
            this.TGIBgroup.PerformLayout();
            this.VKGroup.ResumeLayout(false);
            this.ButtonsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button Back_button;
        private Button STOP_button;
        private GroupBox ZeroGroup;
        private GroupBox ZeroYCXgroup;
        private TextBox ZeroYCXtext;
        private GroupBox ZeroYCYgroup;
        private TextBox ZeroYCYtext;
        private GroupBox ZeroYCZgroup;
        private TextBox ZeroYCZtext;
        private GroupBox ZeroPXgroup;
        private TextBox ZeroPXtext;
        private GroupBox ZeroPZgroup;
        private TextBox ZeroPZtext;
        private GroupBox ZeroPYgroup;
        private TextBox ZeroPYtext;
        private GroupBox StatusGroup;
        private Label Statustext;
        private GroupBox ZPRGroup;
        private TextBox ZPRtext;
        private GroupBox TPZgroup;
        private TextBox TPZtext;
        private GroupBox PZgroup;
        private TextBox PZtext;
        private GroupBox TPYgroup;
        private TextBox TPYtext;
        private GroupBox TYCYZgroup;
        private TextBox TYCYZtext;
        private GroupBox PYgroup;
        private TextBox PYtext;
        private GroupBox TYCXgroup;
        private TextBox TYCXtext;
        private GroupBox TPXgroup;
        private TextBox TPXtext;
        private GroupBox YCZgroup;
        private TextBox YCZtext;
        private GroupBox PXgroup;
        private TextBox PXtext;
        private GroupBox YCYgroup;
        private TextBox YCYtext;
        private GroupBox YCXgroup;
        private TextBox YCXtext;
        private GroupBox CurrentGroup;
        private GroupBox groupBox12;
        private GroupBox groupBox14;
        private TextBox textBox12;
        private GroupBox groupBox15;
        private TextBox textBox14;
        private GroupBox groupBox16;
        private TextBox textBox15;
        private GroupBox groupBox17;
        private TextBox textBox16;
        private GroupBox groupBox18;
        private TextBox textBox17;
        private GroupBox groupBox19;
        private TextBox textBox18;
        private GroupBox TemperatureGroup;
        private GroupBox TGIBgroup;
        private TextBox TGIBtext;
        private GroupBox VKGroup;
        private Button K1_button;
        private Button K2_button;
        private Button ResetK_button;
        private Button ConvertToCSV_Button;
        private RichTextBox FormConsole;
        private Timer timer1;
        private Button START_button;
        private GroupBox ButtonsGroup;
    }
}