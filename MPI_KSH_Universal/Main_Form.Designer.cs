namespace MPI_KSH_Universal
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MF_create_button = new System.Windows.Forms.Button();
            this.MF_change_button = new System.Windows.Forms.Button();
            this.MF_Ok_button = new System.Windows.Forms.Button();
            this.MF_system_combo = new System.Windows.Forms.ComboBox();
            this.KitNum_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MF_create_button
            // 
            this.MF_create_button.Location = new System.Drawing.Point(12, 46);
            this.MF_create_button.Name = "MF_create_button";
            this.MF_create_button.Size = new System.Drawing.Size(120, 40);
            this.MF_create_button.TabIndex = 0;
            this.MF_create_button.Text = "Создать";
            this.MF_create_button.UseVisualStyleBackColor = true;
            this.MF_create_button.Click += new System.EventHandler(this.MF_create_button_Click);
            // 
            // MF_change_button
            // 
            this.MF_change_button.Location = new System.Drawing.Point(138, 46);
            this.MF_change_button.Name = "MF_change_button";
            this.MF_change_button.Size = new System.Drawing.Size(120, 40);
            this.MF_change_button.TabIndex = 0;
            this.MF_change_button.Text = "Просмотр";
            this.MF_change_button.UseVisualStyleBackColor = true;
            this.MF_change_button.Click += new System.EventHandler(this.MF_change_button_Click);
            // 
            // MF_Ok_button
            // 
            this.MF_Ok_button.Location = new System.Drawing.Point(264, 46);
            this.MF_Ok_button.Name = "MF_Ok_button";
            this.MF_Ok_button.Size = new System.Drawing.Size(120, 40);
            this.MF_Ok_button.TabIndex = 0;
            this.MF_Ok_button.Text = "Выбрать";
            this.MF_Ok_button.UseVisualStyleBackColor = true;
            this.MF_Ok_button.Click += new System.EventHandler(this.MF_Ok_button_Click);
            // 
            // MF_system_combo
            // 
            this.MF_system_combo.FormattingEnabled = true;
            this.MF_system_combo.Location = new System.Drawing.Point(12, 12);
            this.MF_system_combo.Name = "MF_system_combo";
            this.MF_system_combo.Size = new System.Drawing.Size(246, 28);
            this.MF_system_combo.TabIndex = 1;
            this.MF_system_combo.DropDown += new System.EventHandler(this.MF_system_combo_DropDown);
            this.MF_system_combo.SelectionChangeCommitted += new System.EventHandler(this.MF_system_combo_SelectionChangeCommitted);
            // 
            // KitNum_text
            // 
            this.KitNum_text.Location = new System.Drawing.Point(264, 12);
            this.KitNum_text.Name = "KitNum_text";
            this.KitNum_text.Size = new System.Drawing.Size(120, 26);
            this.KitNum_text.TabIndex = 2;
            this.KitNum_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KitNum_text.Enter += new System.EventHandler(this.KitNum_text_Enter);
            this.KitNum_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KitNum_text_KeyPress);
            this.KitNum_text.Leave += new System.EventHandler(this.KitNum_text_Leave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 93);
            this.Controls.Add(this.KitNum_text);
            this.Controls.Add(this.MF_system_combo);
            this.Controls.Add(this.MF_Ok_button);
            this.Controls.Add(this.MF_change_button);
            this.Controls.Add(this.MF_create_button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MF_create_button;
        private System.Windows.Forms.Button MF_change_button;
        private System.Windows.Forms.Button MF_Ok_button;
        private System.Windows.Forms.ComboBox MF_system_combo;
        private System.Windows.Forms.TextBox KitNum_text;
    }
}

